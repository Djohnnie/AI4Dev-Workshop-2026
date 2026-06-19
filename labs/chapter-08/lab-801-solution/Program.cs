using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<GameRoomManager>();
builder.Services.AddHostedService<GameLoopService>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseWebSockets();

app.MapPost("/api/rooms/create", (CreateRoomRequest request, GameRoomManager manager) =>
{
    var playerName = NameHelpers.Normalize(request.PlayerName);

    if (playerName is null)
    {
        return Results.BadRequest(new { error = "Enter a player name before creating a room." });
    }

    var result = manager.CreateRoom(playerName);
    return Results.Ok(result);
});

app.MapPost("/api/rooms/join", (JoinRoomRequest request, GameRoomManager manager) =>
{
    var playerName = NameHelpers.Normalize(request.PlayerName);

    if (playerName is null)
    {
        return Results.BadRequest(new { error = "Enter a player name before joining a room." });
    }

    var result = manager.JoinRoom(request.RoomCode, playerName);
    return result is null
        ? Results.BadRequest(new { error = "Room code not found, already running, or full." })
        : Results.Ok(result);
});

app.Map("/ws", async (HttpContext context, GameRoomManager manager) =>
{
    if (!context.WebSockets.IsWebSocketRequest)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        return;
    }

    var roomCode = context.Request.Query["roomCode"].ToString();
    var playerId = context.Request.Query["playerId"].ToString();

    if (string.IsNullOrWhiteSpace(roomCode) || string.IsNullOrWhiteSpace(playerId))
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        return;
    }

    using var socket = await context.WebSockets.AcceptWebSocketAsync();
    await manager.AttachSocketAsync(roomCode, playerId, socket, context.RequestAborted);
});

app.Run();

record CreateRoomRequest(string PlayerName);
record JoinRoomRequest(string RoomCode, string PlayerName);
record RoomRegistrationResult(string RoomCode, string PlayerId, bool IsHost);
record Position(int X, int Y);

sealed class GameLoopService(GameRoomManager manager) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(GameRoom.TickDelayMs));

        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            await manager.AdvanceRoomsAsync(stoppingToken);
        }
    }
}

sealed class GameRoomManager
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
    private static readonly string[] SnakeColors = ["#ffd866", "#58a6ff", "#3fb950", "#ff7b72"];

    private readonly object gate = new();
    private readonly Random random = new();
    private readonly Dictionary<string, GameRoom> rooms = new(StringComparer.OrdinalIgnoreCase);

    public RoomRegistrationResult CreateRoom(string playerName)
    {
        lock (gate)
        {
            var roomCode = GenerateRoomCode();
            var room = new GameRoom(roomCode);
            var host = CreatePlayer(playerName, isHost: true, playerIndex: 0);

            room.Players.Add(host);
            ResetRoom(room);
            room.Message = $"{host.Name} created room {room.Code}. Share the code and wait for more players.";
            rooms.Add(room.Code, room);

            return new RoomRegistrationResult(room.Code, host.PlayerId, true);
        }
    }

    public RoomRegistrationResult? JoinRoom(string roomCode, string playerName)
    {
        roomCode = RoomCodeHelpers.Normalize(roomCode);
        var shouldBroadcast = false;
        RoomRegistrationResult? result = null;
        string? broadcastRoomCode = null;

        lock (gate)
        {
            if (!rooms.TryGetValue(roomCode, out var room) ||
                room.Phase != "waiting" ||
                room.Players.Count >= GameRoom.MaxPlayers)
            {
                return null;
            }

            var player = CreatePlayer(
                playerName,
                isHost: false,
                playerIndex: room.Players.Count == 0 ? 0 : room.Players.Max(existing => existing.JoinOrder) + 1);

            room.Players.Add(player);
            ResetRoom(room);
            room.Message = $"{player.Name} joined room {room.Code}. Host can start when everyone is ready.";
            shouldBroadcast = true;
            broadcastRoomCode = room.Code;
            result = new RoomRegistrationResult(room.Code, player.PlayerId, false);
        }

        if (shouldBroadcast && broadcastRoomCode is not null)
        {
            _ = BroadcastRoomAsync(broadcastRoomCode, CancellationToken.None);
        }

        return result;
    }

    public async Task AttachSocketAsync(string roomCode, string playerId, WebSocket socket, CancellationToken cancellationToken)
    {
        roomCode = RoomCodeHelpers.Normalize(roomCode);

        PlayerSession? session;

        lock (gate)
        {
            if (!TryGetPlayer(roomCode, playerId, out _, out var player))
            {
                return;
            }

            session = new PlayerSession(socket)
            {
                PlayerId = player.PlayerId
            };
            player.Session = session;
        }

        await BroadcastRoomAsync(roomCode, cancellationToken);

        try
        {
            while (socket.State == WebSocketState.Open && !cancellationToken.IsCancellationRequested)
            {
                var message = await ReceiveMessageAsync(socket, cancellationToken);

                if (message is null)
                {
                    break;
                }

                await HandleClientMessageAsync(roomCode, playerId, message, cancellationToken);
            }
        }
        finally
        {
            await DisconnectPlayerAsync(roomCode, playerId, CancellationToken.None);
        }
    }

    public async Task AdvanceRoomsAsync(CancellationToken cancellationToken)
    {
        List<string> changedRooms = [];

        lock (gate)
        {
            foreach (var room in rooms.Values)
            {
                if (AdvanceRoom(room))
                {
                    changedRooms.Add(room.Code);
                }
            }
        }

        foreach (var roomCode in changedRooms)
        {
            await BroadcastRoomAsync(roomCode, cancellationToken);
        }
    }

    private async Task HandleClientMessageAsync(string roomCode, string playerId, string message, CancellationToken cancellationToken)
    {
        string? messageType;
        string? directionName = null;
        var shouldBroadcast = false;

        using var document = JsonDocument.Parse(message);
        messageType = document.RootElement.TryGetProperty("type", out var typeElement)
            ? typeElement.GetString()
            : null;

        if (string.Equals(messageType, "direction", StringComparison.OrdinalIgnoreCase))
        {
            directionName = document.RootElement.TryGetProperty("direction", out var directionElement)
                ? directionElement.GetString()
                : null;
        }

        lock (gate)
        {
            if (!TryGetPlayer(roomCode, playerId, out var room, out var player))
            {
                return;
            }

            switch (messageType)
            {
                case "direction":
                    if (DirectionHelpers.TryParse(directionName, out var direction) &&
                        !DirectionHelpers.IsReverse(player.Direction, direction))
                    {
                        player.NextDirection = direction;
                    }
                    break;

                case "toggle":
                    if (!player.IsHost)
                    {
                        room.Message = "Only the host can start, pause, or restart the room.";
                        shouldBroadcast = true;
                        break;
                    }

                    switch (room.Phase)
                    {
                        case "waiting":
                            room.Phase = "running";
                            room.Message = "Room started. Shared snack is live.";
                            shouldBroadcast = true;
                            break;
                        case "running":
                            room.Phase = "paused";
                            room.Message = "Paused. Host can resume with Space.";
                            shouldBroadcast = true;
                            break;
                        case "paused":
                            room.Phase = "running";
                            room.Message = "Back in motion.";
                            shouldBroadcast = true;
                            break;
                        case "gameover":
                            ResetRoom(room);
                            room.Phase = "running";
                            room.Message = "New round started.";
                            shouldBroadcast = true;
                            break;
                    }
                    break;
            }
        }

        if (shouldBroadcast)
        {
            await BroadcastRoomAsync(roomCode, cancellationToken);
        }
    }

    private async Task DisconnectPlayerAsync(string roomCode, string playerId, CancellationToken cancellationToken)
    {
        var shouldBroadcast = false;
        var removeRoom = false;

        lock (gate)
        {
            if (!TryGetPlayer(roomCode, playerId, out var room, out var player))
            {
                return;
            }

            player.Session = null;
            room.Players.Remove(player);

            if (room.Players.Count == 0)
            {
                removeRoom = rooms.Remove(room.Code);
            }
            else
            {
                if (!room.Players.Any(existing => existing.IsHost))
                {
                    room.Players[0].IsHost = true;
                }

                if (room.Phase == "running")
                {
                    EvaluateRoomEnd(room);
                }

                if (room.Phase != "gameover")
                {
                    room.Message = $"{player.Name} left room {room.Code}.";
                }

                shouldBroadcast = true;
            }
        }

        if (!removeRoom && shouldBroadcast)
        {
            await BroadcastRoomAsync(roomCode, cancellationToken);
        }
    }

    private bool AdvanceRoom(GameRoom room)
    {
        if (room.Phase != "running")
        {
            return false;
        }

        var alivePlayers = room.Players
            .Where(player => player.Alive)
            .OrderBy(player => player.JoinOrder)
            .ToArray();

        if (alivePlayers.Length == 0)
        {
            room.Phase = "gameover";
            room.Message = "Nobody is left alive. Host can restart the room.";
            return true;
        }

        var plannedMoves = new List<PlannedMove>(alivePlayers.Length);

        foreach (var player in alivePlayers)
        {
            player.Direction = player.NextDirection;
            var head = player.Snake[0];
            var nextHead = WrapPosition(new Position(
                head.X + player.Direction.X,
                head.Y + player.Direction.Y));

            plannedMoves.Add(new PlannedMove(player, nextHead));
        }

        var foodWinner = plannedMoves
            .FirstOrDefault(move => move.NextHead == room.Food)?
            .Player;

        foreach (var move in plannedMoves)
        {
            var grows = foodWinner == move.Player;
            var nextSnake = new List<Position>(move.Player.Snake.Count + (grows ? 1 : 0))
            {
                move.NextHead
            };

            nextSnake.AddRange(move.Player.Snake);

            if (!grows)
            {
                nextSnake.RemoveAt(nextSnake.Count - 1);
            }

            move.Player.Snake = nextSnake;
        }

        var defeatedPlayers = new HashSet<string>(StringComparer.Ordinal);
        var headGroups = plannedMoves
            .GroupBy(move => move.NextHead)
            .Where(group => group.Count() > 1)
            .SelectMany(group => group.Select(move => move.Player.PlayerId));

        foreach (var playerId in headGroups)
        {
            defeatedPlayers.Add(playerId);
        }

        var occupiedPositions = new Dictionary<Position, List<(string playerId, int segmentIndex)>>();

        foreach (var player in alivePlayers)
        {
            for (var index = 0; index < player.Snake.Count; index += 1)
            {
                var segment = player.Snake[index];

                if (!occupiedPositions.TryGetValue(segment, out var segments))
                {
                    segments = [];
                    occupiedPositions.Add(segment, segments);
                }

                segments.Add((player.PlayerId, index));
            }
        }

        foreach (var player in alivePlayers)
        {
            var head = player.Snake[0];
            var collisions = occupiedPositions[head];
            var hitsBody = collisions.Any(collision =>
                collision.playerId != player.PlayerId || collision.segmentIndex > 0);

            if (hitsBody)
            {
                defeatedPlayers.Add(player.PlayerId);
            }
        }

        foreach (var player in alivePlayers)
        {
            player.Alive = !defeatedPlayers.Contains(player.PlayerId);
        }

        if (foodWinner is not null && foodWinner.Alive)
        {
            foodWinner.Score += 1;
            room.Message = $"{foodWinner.Name} claimed the shared snack.";

            if (!TrySpawnFood(room))
            {
                room.Phase = "gameover";
                room.Message = "The board is full. Host can restart the room.";
            }
        }
        else if (occupiedPositions.ContainsKey(room.Food))
        {
            TrySpawnFood(room);
        }

        EvaluateRoomEnd(room);
        return true;
    }

    private void EvaluateRoomEnd(GameRoom room)
    {
        var alivePlayers = room.Players.Where(player => player.Alive).ToArray();

        if (room.Players.Count == 1)
        {
            if (alivePlayers.Length == 0)
            {
                room.Phase = "gameover";
                room.Message = "You crashed. Host can press Space to restart.";
            }

            return;
        }

        if (alivePlayers.Length == 1)
        {
            room.Phase = "gameover";
            room.Message = $"{alivePlayers[0].Name} wins the room. Host can press Space to restart.";
        }
        else if (alivePlayers.Length == 0)
        {
            room.Phase = "gameover";
            room.Message = "No snakes survived the collision. Host can restart the room.";
        }
    }

    private void ResetRoom(GameRoom room)
    {
        room.Phase = "waiting";

        foreach (var player in room.Players.OrderBy(player => player.JoinOrder))
        {
            var spawn = SpawnLayouts.ForIndex(player.JoinOrder % GameRoom.MaxPlayers);
            player.Snake = spawn.Segments.ToList();
            player.Direction = spawn.Direction;
            player.NextDirection = spawn.Direction;
            player.Alive = true;
            player.Score = 0;
        }

        TrySpawnFood(room);
    }

    private bool TrySpawnFood(GameRoom room)
    {
        var occupied = room.Players
            .SelectMany(player => player.Snake)
            .ToHashSet();

        var availableCells = new List<Position>(GameRoom.BoardWidth * GameRoom.BoardHeight - occupied.Count);

        for (var y = 0; y < GameRoom.BoardHeight; y += 1)
        {
            for (var x = 0; x < GameRoom.BoardWidth; x += 1)
            {
                var candidate = new Position(x, y);

                if (!occupied.Contains(candidate))
                {
                    availableCells.Add(candidate);
                }
            }
        }

        if (availableCells.Count == 0)
        {
            return false;
        }

        room.Food = availableCells[random.Next(availableCells.Count)];
        return true;
    }

    private async Task BroadcastRoomAsync(string roomCode, CancellationToken cancellationToken)
    {
        List<(PlayerSession session, string payload)> recipients = [];

        lock (gate)
        {
            if (!rooms.TryGetValue(roomCode, out var room))
            {
                return;
            }

            foreach (var player in room.Players)
            {
                if (player.Session is null)
                {
                    continue;
                }

                var snapshot = CreateSnapshot(room, player.PlayerId);
                recipients.Add((player.Session, JsonSerializer.Serialize(snapshot, JsonOptions)));
            }
        }

        var disconnected = new ConcurrentBag<string>();

        foreach (var recipient in recipients)
        {
            var sent = await recipient.session.SendAsync(recipient.payload, cancellationToken);

            if (!sent)
            {
                disconnected.Add(recipient.session.PlayerId);
            }
        }

        foreach (var disconnectedPlayerId in disconnected)
        {
            await DisconnectPlayerAsync(roomCode, disconnectedPlayerId, cancellationToken);
        }
    }

    private object CreateSnapshot(GameRoom room, string viewerPlayerId)
    {
        var players = room.Players
            .OrderBy(player => player.JoinOrder)
            .Select(player => new
            {
                playerId = player.PlayerId,
                player.Name,
                player.Color,
                isHost = player.IsHost,
                isAlive = player.Alive,
                player.Score,
                segments = player.Snake
            })
            .ToArray();

        var viewer = room.Players.First(player => player.PlayerId == viewerPlayerId);
        var hostPlayerId = room.Players.FirstOrDefault(player => player.IsHost)?.PlayerId ?? viewerPlayerId;

        return new
        {
            type = "state",
            board = new
            {
                width = GameRoom.BoardWidth,
                height = GameRoom.BoardHeight,
                tileSize = GameRoom.TileSize
            },
            room = new
            {
                room.Code,
                room.Phase,
                hostPlayerId,
                tickDelayMs = GameRoom.TickDelayMs
            },
            you = new
            {
                playerId = viewer.PlayerId,
                isHost = viewer.IsHost,
                isAlive = viewer.Alive,
                viewer.Score
            },
            players,
            food = room.Food,
            message = room.Message
        };
    }

    private static async Task<string?> ReceiveMessageAsync(WebSocket socket, CancellationToken cancellationToken)
    {
        var buffer = new byte[4096];
        using var stream = new MemoryStream();

        while (true)
        {
            var result = await socket.ReceiveAsync(buffer, cancellationToken);

            if (result.MessageType == WebSocketMessageType.Close)
            {
                return null;
            }

            stream.Write(buffer, 0, result.Count);

            if (result.EndOfMessage)
            {
                break;
            }
        }

        return Encoding.UTF8.GetString(stream.ToArray());
    }

    private bool TryGetPlayer(string roomCode, string playerId, out GameRoom room, out PlayerState player)
    {
        if (rooms.TryGetValue(roomCode, out room!) &&
            room.Players.FirstOrDefault(existing => existing.PlayerId == playerId) is { } existingPlayer)
        {
            player = existingPlayer;
            return true;
        }

        room = null!;
        player = null!;
        return false;
    }

    private string GenerateRoomCode()
    {
        const string alphabet = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";

        while (true)
        {
            var codeChars = new char[4];

            for (var index = 0; index < codeChars.Length; index += 1)
            {
                codeChars[index] = alphabet[random.Next(alphabet.Length)];
            }

            var code = new string(codeChars);

            if (!rooms.ContainsKey(code))
            {
                return code;
            }
        }
    }

    private static PlayerState CreatePlayer(string name, bool isHost, int playerIndex) =>
        new()
        {
            PlayerId = Guid.NewGuid().ToString("N"),
            Name = name,
            IsHost = isHost,
            JoinOrder = playerIndex,
            Color = SnakeColors[playerIndex % SnakeColors.Length]
        };

    private static Position WrapPosition(Position position) =>
        new(
            (position.X + GameRoom.BoardWidth) % GameRoom.BoardWidth,
            (position.Y + GameRoom.BoardHeight) % GameRoom.BoardHeight);
}

sealed class GameRoom(string code)
{
    public const int BoardWidth = 30;
    public const int BoardHeight = 18;
    public const int TileSize = 28;
    public const int TickDelayMs = 180;
    public const int MaxPlayers = 4;

    public string Code { get; } = code;
    public string Phase { get; set; } = "waiting";
    public string Message { get; set; } = "Create a room and wait for players.";
    public Position Food { get; set; } = new(0, 0);
    public List<PlayerState> Players { get; } = [];
}

sealed class PlayerState
{
    public required string PlayerId { get; init; }
    public required string Name { get; init; }
    public required string Color { get; init; }
    public required int JoinOrder { get; init; }
    public bool IsHost { get; set; }
    public bool Alive { get; set; } = true;
    public int Score { get; set; }
    public Position Direction { get; set; } = new(1, 0);
    public Position NextDirection { get; set; } = new(1, 0);
    public List<Position> Snake { get; set; } = [];
    public PlayerSession? Session { get; set; }
}

sealed class PlayerSession(WebSocket socket)
{
    private readonly SemaphoreSlim sendGate = new(1, 1);

    public string PlayerId { get; set; } = string.Empty;

    public async Task<bool> SendAsync(string payload, CancellationToken cancellationToken)
    {
        if (socket.State != WebSocketState.Open)
        {
            return false;
        }

        await sendGate.WaitAsync(cancellationToken);

        try
        {
            if (socket.State != WebSocketState.Open)
            {
                return false;
            }

            var data = Encoding.UTF8.GetBytes(payload);
            await socket.SendAsync(data, WebSocketMessageType.Text, true, cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            sendGate.Release();
        }
    }
}

sealed record PlannedMove(PlayerState Player, Position NextHead);
sealed record SpawnLayout(Position Direction, IReadOnlyList<Position> Segments);

static class SpawnLayouts
{
    public static SpawnLayout ForIndex(int index) =>
        index switch
        {
            0 => new(new Position(1, 0), [new Position(5, 4), new Position(4, 4), new Position(3, 4)]),
            1 => new(new Position(-1, 0), [new Position(24, 13), new Position(25, 13), new Position(26, 13)]),
            2 => new(new Position(1, 0), [new Position(5, 13), new Position(4, 13), new Position(3, 13)]),
            _ => new(new Position(-1, 0), [new Position(24, 4), new Position(25, 4), new Position(26, 4)])
        };
}

static class DirectionHelpers
{
    public static bool TryParse(string? name, out Position direction)
    {
        direction = (name ?? string.Empty).ToLowerInvariant() switch
        {
            "up" => new Position(0, -1),
            "down" => new Position(0, 1),
            "left" => new Position(-1, 0),
            "right" => new Position(1, 0),
            _ => default
        };

        return direction != default;
    }

    public static bool IsReverse(Position current, Position next) =>
        current.X == -next.X && current.Y == -next.Y;
}

static class NameHelpers
{
    public static string? Normalize(string? value)
    {
        var trimmed = value?.Trim();
        return string.IsNullOrWhiteSpace(trimmed) ? null : trimmed[..Math.Min(trimmed.Length, 18)];
    }
}

static class RoomCodeHelpers
{
    public static string Normalize(string? value) => (value ?? string.Empty).Trim().ToUpperInvariant();
}
