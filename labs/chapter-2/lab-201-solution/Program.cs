using Spectre.Console;
using Spectre.Console.Rendering;

await new SnakeGame().RunAsync();

internal sealed class SnakeGame
{
    private const int BoardWidth = 24;
    private const int BoardHeight = 14;
    private const int FrameDelayMs = 135;

    private readonly LinkedList<Position> snake = [];
    private readonly Random random = new();

    private Direction currentDirection = Direction.Right;
    private Direction requestedDirection = Direction.Right;
    private Position food;
    private GameState state = GameState.WaitingToStart;
    private string statusMessage = "Press [bold]Space[/] to start.";
    private int score;
    private bool exitRequested;

    public async Task RunAsync()
    {
        AnsiConsole.Clear();
        DrawTitle();
        DrawInstructions();
        ResetGame();

        var canManageCursor = OperatingSystem.IsWindows();
        var previousCursorVisible = canManageCursor && Console.CursorVisible;

        if (canManageCursor)
        {
            Console.CursorVisible = false;
        }

        try
        {
            await AnsiConsole.Live(BuildFrame())
                .Overflow(VerticalOverflow.Crop)
                .StartAsync(async context =>
                {
                    while (!exitRequested)
                    {
                        // Input is polled every frame so the render loop never blocks on Console.ReadKey.
                        HandleInput();

                        if (state == GameState.Running)
                        {
                            UpdateGame();
                        }

                        context.UpdateTarget(BuildFrame());
                        await Task.Delay(FrameDelayMs);
                    }
                });
        }
        finally
        {
            if (canManageCursor)
            {
                Console.CursorVisible = previousCursorVisible;
            }
        }
    }

    private static void DrawTitle()
    {
        AnsiConsole.Write(new FigletText("Ultimate Snake").Centered().Color(Color.Green1));
        AnsiConsole.Write(new Rule("[yellow]Reference Solution[/]").RuleStyle("grey").LeftJustified());
        AnsiConsole.WriteLine();
    }

    private static void DrawInstructions()
    {
        var instructions = new Panel(new Markup(
            "[bold]Controls[/]\n" +
            "- Arrow keys move the snake\n" +
            "- [bold]Space[/] starts, pauses, and restarts after game over\n" +
            "- Crossing an edge wraps to the opposite side\n" +
            "- [bold]Esc[/] exits"))
        {
            Border = BoxBorder.Rounded,
            BorderStyle = new Style(Color.Green1),
            Header = new PanelHeader("How to play")
        };

        AnsiConsole.Write(instructions);
        AnsiConsole.WriteLine();
    }

    private IRenderable BuildFrame()
    {
        return new Rows(
            BuildStatusPanel(),
            BuildBoardPanel(),
            BuildFooterPanel());
    }

    private Panel BuildStatusPanel()
    {
        var stateLabel = state switch
        {
            GameState.WaitingToStart => "[yellow]Waiting[/]",
            GameState.Running => "[green]Running[/]",
            GameState.Paused => "[orange1]Paused[/]",
            GameState.GameOver => "[red]Game Over[/]",
            _ => "[grey]Unknown[/]"
        };

        return new Panel(new Markup(
            $"[bold]Score:[/] {score}    [bold]State:[/] {stateLabel}\n{statusMessage}"))
        {
            Border = BoxBorder.Rounded,
            BorderStyle = new Style(Color.Blue),
            Header = new PanelHeader("Status")
        };
    }

    private Panel BuildBoardPanel()
    {
        return new Panel(new Markup(RenderBoard()))
        {
            Border = BoxBorder.Rounded,
            BorderStyle = new Style(Color.Green1),
            Header = new PanelHeader("Arena")
        };
    }

    private Panel BuildFooterPanel()
    {
        return new Panel(new Markup(
            "[grey]Tip:[/] edge wrapping is safe, but reversing directly into yourself is blocked."))
        {
            Border = BoxBorder.Rounded,
            BorderStyle = new Style(Color.Grey),
            Header = new PanelHeader("Hint")
        };
    }

    private void HandleInput()
    {
        while (Console.KeyAvailable)
        {
            var key = Console.ReadKey(intercept: true).Key;

            switch (key)
            {
                case ConsoleKey.Escape:
                    exitRequested = true;
                    return;

                case ConsoleKey.Spacebar:
                    ToggleGameState();
                    break;

                case ConsoleKey.UpArrow:
                    QueueDirection(Direction.Up);
                    break;

                case ConsoleKey.DownArrow:
                    QueueDirection(Direction.Down);
                    break;

                case ConsoleKey.LeftArrow:
                    QueueDirection(Direction.Left);
                    break;

                case ConsoleKey.RightArrow:
                    QueueDirection(Direction.Right);
                    break;
            }
        }
    }

    private void ToggleGameState()
    {
        switch (state)
        {
            case GameState.WaitingToStart:
                state = GameState.Running;
                statusMessage = "Game on.";
                break;

            case GameState.Running:
                state = GameState.Paused;
                statusMessage = "Paused. Press Space to continue.";
                break;

            case GameState.Paused:
                state = GameState.Running;
                statusMessage = "Back in motion.";
                break;

            case GameState.GameOver:
                ResetGame();
                statusMessage = "New round ready. Press Space to start.";
                break;
        }
    }

    private void QueueDirection(Direction nextDirection)
    {
        if (IsOpposite(currentDirection, nextDirection))
        {
            return;
        }

        requestedDirection = nextDirection;

        if (state == GameState.WaitingToStart)
        {
            statusMessage = "Direction set. Press Space to start.";
        }
    }

    private void UpdateGame()
    {
        currentDirection = requestedDirection;
        var head = snake.First!.Value;
        // Crossing an edge wraps the head to the opposite side instead of ending the round.
        var nextHead = WrapPosition(head.Move(currentDirection));

        var grows = nextHead == food;
        // Moving into the current tail is allowed when not growing because that tail segment
        // will be removed during the same update.
        if (HitsSnake(nextHead, ignoreTail: !grows))
        {
            state = GameState.GameOver;
            statusMessage = "You crashed into yourself. Press Space to restart.";
            return;
        }

        snake.AddFirst(nextHead);

        if (grows)
        {
            score++;

            if (snake.Count == BoardWidth * BoardHeight)
            {
                state = GameState.GameOver;
                statusMessage = "You filled the board. Press Space to play again.";
                return;
            }

            SpawnFood();
            statusMessage = "Snack collected. Keep going.";
        }
        else
        {
            snake.RemoveLast();
        }
    }

    private void ResetGame()
    {
        snake.Clear();

        var centerX = BoardWidth / 2;
        var centerY = BoardHeight / 2;

        snake.AddLast(new Position(centerX, centerY));
        snake.AddLast(new Position(centerX - 1, centerY));
        snake.AddLast(new Position(centerX - 2, centerY));

        currentDirection = Direction.Right;
        requestedDirection = Direction.Right;
        score = 0;
        state = GameState.WaitingToStart;
        exitRequested = false;
        SpawnFood();
        statusMessage = "Press [bold]Space[/] to start.";
    }

    private void SpawnFood()
    {
        do
        {
            food = new Position(
                random.Next(0, BoardWidth),
                random.Next(0, BoardHeight));
        }
        // Food must always land on an empty tile, never inside the snake body.
        while (snake.Contains(food));
    }

    private bool HitsSnake(Position nextHead, bool ignoreTail)
    {
        var node = snake.First;
        while (node is not null)
        {
            var isTail = node.Next is null;
            if (!(ignoreTail && isTail) && node.Value == nextHead)
            {
                return true;
            }

            node = node.Next;
        }

        return false;
    }

    private static bool IsOpposite(Direction current, Direction next)
    {
        return (current, next) switch
        {
            (Direction.Up, Direction.Down) => true,
            (Direction.Down, Direction.Up) => true,
            (Direction.Left, Direction.Right) => true,
            (Direction.Right, Direction.Left) => true,
            _ => false
        };
    }

    private static Position WrapPosition(Position position)
    {
        var wrappedX = position.X switch
        {
            < 0 => BoardWidth - 1,
            >= BoardWidth => 0,
            _ => position.X
        };

        var wrappedY = position.Y switch
        {
            < 0 => BoardHeight - 1,
            >= BoardHeight => 0,
            _ => position.Y
        };

        return new Position(wrappedX, wrappedY);
    }

    private string RenderBoard()
    {
        var lines = new List<string>();
        var head = snake.First!.Value;

        for (var y = -1; y <= BoardHeight; y++)
        {
            var row = new List<string>();

            for (var x = -1; x <= BoardWidth; x++)
            {
                if (x == -1 || y == -1 || x == BoardWidth || y == BoardHeight)
                {
                    row.Add("[grey]#[/] ");
                }
                else
                {
                    var position = new Position(x, y);

                    if (position == head)
                    {
                        row.Add("[bold yellow]@[/] ");
                    }
                    else if (position == food)
                    {
                        row.Add("[red]*[/] ");
                    }
                    else if (snake.Skip(1).Contains(position))
                    {
                        row.Add("[green]o[/] ");
                    }
                    else
                    {
                        // A trailing space makes horizontal movement feel more consistent with
                        // vertical movement in a terminal where characters are taller than wide.
                        row.Add("[dim].[/] ");
                    }
                }
            }

            lines.Add(string.Concat(row));
        }

        return string.Join(Environment.NewLine, lines);
    }
}

internal enum Direction
{
    Up,
    Down,
    Left,
    Right
}

internal enum GameState
{
    WaitingToStart,
    Running,
    Paused,
    GameOver
}

internal readonly record struct Position(int X, int Y)
{
    public Position Move(Direction direction)
    {
        return direction switch
        {
            Direction.Up => this with { Y = Y - 1 },
            Direction.Down => this with { Y = Y + 1 },
            Direction.Left => this with { X = X - 1 },
            Direction.Right => this with { X = X + 1 },
            _ => this
        };
    }
}
