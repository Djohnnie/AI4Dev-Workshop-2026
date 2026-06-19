const canvas = document.getElementById("game");
const context = canvas.getContext("2d");

const playerNameInput = document.getElementById("player-name");
const roomCodeInput = document.getElementById("room-code");
const createRoomButton = document.getElementById("create-room");
const joinRoomButton = document.getElementById("join-room");
const toggleRoomButton = document.getElementById("toggle-room");

const connectedRoomElement = document.getElementById("connected-room");
const playerRoleElement = document.getElementById("player-role");
const roomPhaseElement = document.getElementById("room-phase");
const foodStateElement = document.getElementById("food-state");
const playerScoreElement = document.getElementById("player-score");
const statusTextElement = document.getElementById("status-text");
const playersListElement = document.getElementById("players-list");

const state = {
    socket: null,
    playerId: null,
    roomCode: null,
    board: { width: 30, height: 18, tileSize: 28 },
    phase: "waiting",
    players: [],
    food: { x: 0, y: 0 },
    message: "Create a room or join one with a code.",
    isHost: false,
    isAlive: true,
    score: 0
};

createRoomButton.addEventListener("click", () => createRoom());
joinRoomButton.addEventListener("click", () => joinRoom());
toggleRoomButton.addEventListener("click", () => sendToggle());
window.addEventListener("keydown", handleKeyDown);

render();
renderSidebar();

async function createRoom() {
    const playerName = playerNameInput.value.trim();
    const result = await postJson("/api/rooms/create", { playerName });

    if (!result.ok) {
        setStatus(result.error);
        return;
    }

    await connectToRoom(result.payload.roomCode, result.payload.playerId, result.payload.isHost);
}

async function joinRoom() {
    const playerName = playerNameInput.value.trim();
    const roomCode = roomCodeInput.value.trim();
    const result = await postJson("/api/rooms/join", { playerName, roomCode });

    if (!result.ok) {
        setStatus(result.error);
        return;
    }

    await connectToRoom(result.payload.roomCode, result.payload.playerId, result.payload.isHost);
}

async function postJson(url, payload) {
    try {
        const response = await fetch(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(payload)
        });

        const data = await response.json();

        if (!response.ok) {
            return { ok: false, error: data.error ?? "Request failed." };
        }

        return { ok: true, payload: data };
    } catch {
        return { ok: false, error: "Could not reach the local server." };
    }
}

async function connectToRoom(roomCode, playerId, isHost) {
    if (state.socket) {
        state.socket.close();
    }

    state.roomCode = roomCode;
    state.playerId = playerId;
    state.isHost = isHost;

    const protocol = window.location.protocol === "https:" ? "wss" : "ws";
    const url = `${protocol}://${window.location.host}/ws?roomCode=${encodeURIComponent(roomCode)}&playerId=${encodeURIComponent(playerId)}`;
    const socket = new WebSocket(url);
    state.socket = socket;

    socket.addEventListener("open", () => {
        connectedRoomElement.textContent = roomCode;
        playerRoleElement.textContent = isHost ? "Host" : "Player";
        toggleRoomButton.disabled = !isHost;
        setStatus(isHost
            ? "Room created. Share the code, then press Space to start."
            : `Joined room ${roomCode}. Wait for the host to start.`);
    });

    socket.addEventListener("message", event => {
        const payload = JSON.parse(event.data);

        if (payload.type !== "state") {
            return;
        }

        state.board = payload.board;
        state.phase = payload.room.phase;
        state.players = payload.players;
        state.food = payload.food;
        state.message = payload.message;
        state.isHost = payload.you.isHost;
        state.isAlive = payload.you.isAlive;
        state.score = payload.you.score;

        connectedRoomElement.textContent = payload.room.code;
        playerRoleElement.textContent = payload.you.isHost ? "Host" : "Player";
        roomPhaseElement.textContent = payload.room.phase;
        playerScoreElement.textContent = `score ${payload.you.score}`;
        statusTextElement.textContent = payload.message;
        toggleRoomButton.disabled = !payload.you.isHost;

        renderSidebar();
        render();
    });

    socket.addEventListener("close", () => {
        if (state.socket === socket) {
            state.socket = null;
            state.phase = "disconnected";
            setStatus("Connection closed. Refresh or rejoin the room.");
            render();
        }
    });
}

function handleKeyDown(event) {
    switch (event.code) {
        case "Space":
            event.preventDefault();
            sendToggle();
            break;
        case "ArrowUp":
            event.preventDefault();
            sendDirection("up");
            break;
        case "ArrowDown":
            event.preventDefault();
            sendDirection("down");
            break;
        case "ArrowLeft":
            event.preventDefault();
            sendDirection("left");
            break;
        case "ArrowRight":
            event.preventDefault();
            sendDirection("right");
            break;
    }
}

function sendDirection(direction) {
    sendSocketMessage({ type: "direction", direction });
}

function sendToggle() {
    sendSocketMessage({ type: "toggle" });
}

function sendSocketMessage(payload) {
    if (!state.socket || state.socket.readyState !== WebSocket.OPEN) {
        return;
    }

    state.socket.send(JSON.stringify(payload));
}

function renderSidebar() {
    playersListElement.innerHTML = "";
    foodStateElement.textContent = "1 snack";

    for (const player of state.players) {
        const listItem = document.createElement("li");
        listItem.classList.toggle("dead", !player.isAlive);

        const nameRow = document.createElement("div");
        nameRow.className = "player-name-row";

        const name = document.createElement("strong");
        name.textContent = player.name;
        name.style.color = player.color;

        const tag = document.createElement("span");
        tag.className = "player-tag";
        tag.textContent = player.isHost ? "Host" : (player.isAlive ? "Alive" : "Out");

        const detail = document.createElement("div");
        detail.className = "player-detail";
        detail.textContent = `Score ${player.score} · ${player.segments.length} segments`;

        nameRow.append(name, tag);
        listItem.append(nameRow, detail);
        playersListElement.append(listItem);
    }
}

function render() {
    resizeCanvas();
    drawBackground();
    drawFood();
    drawSnakes();
    drawOverlay();
}

function resizeCanvas() {
    const width = state.board.width * state.board.tileSize;
    const height = state.board.height * state.board.tileSize;

    if (canvas.width !== width || canvas.height !== height) {
        canvas.width = width;
        canvas.height = height;
    }
}

function drawBackground() {
    context.clearRect(0, 0, canvas.width, canvas.height);
    context.fillStyle = "#0e1124";
    context.fillRect(0, 0, canvas.width, canvas.height);

    context.strokeStyle = "rgba(139, 127, 212, 0.16)";
    context.lineWidth = 1;

    for (let x = 0; x <= state.board.width; x += 1) {
        context.beginPath();
        context.moveTo(x * state.board.tileSize, 0);
        context.lineTo(x * state.board.tileSize, canvas.height);
        context.stroke();
    }

    for (let y = 0; y <= state.board.height; y += 1) {
        context.beginPath();
        context.moveTo(0, y * state.board.tileSize);
        context.lineTo(canvas.width, y * state.board.tileSize);
        context.stroke();
    }
}

function drawFood() {
    context.fillStyle = "#ff7b72";
    context.beginPath();
    context.arc(
        state.food.x * state.board.tileSize + state.board.tileSize / 2,
        state.food.y * state.board.tileSize + state.board.tileSize / 2,
        state.board.tileSize * 0.28,
        0,
        Math.PI * 2);
    context.fill();
}

function drawSnakes() {
    for (const player of state.players) {
        player.segments.forEach((segment, index) => {
            context.fillStyle = index === 0 ? brighten(player.color) : player.color;
            context.fillRect(
                segment.x * state.board.tileSize + 3,
                segment.y * state.board.tileSize + 3,
                state.board.tileSize - 6,
                state.board.tileSize - 6);
        });
    }
}

function drawOverlay() {
    if (state.phase === "running") {
        return;
    }

    context.fillStyle = "rgba(14, 17, 36, 0.58)";
    context.fillRect(0, 0, canvas.width, canvas.height);

    context.textAlign = "center";
    context.fillStyle = "#f8f7ff";
    context.font = "700 42px Segoe UI";
    context.fillText(overlayTitle(), canvas.width / 2, canvas.height / 2 - 10);

    context.fillStyle = "#c4baea";
    context.font = "400 22px Segoe UI";
    context.fillText(state.message, canvas.width / 2, canvas.height / 2 + 28);
}

function overlayTitle() {
    switch (state.phase) {
        case "waiting":
            return state.isHost ? "Press Space to start the room" : "Waiting for the host";
        case "paused":
            return "Paused";
        case "gameover":
            return "Round finished";
        case "disconnected":
            return "Disconnected";
        default:
            return "Ultimate Snake Rooms";
    }
}

function setStatus(text) {
    state.message = text;
    statusTextElement.textContent = text;
}

function brighten(hexColor) {
    return `${hexColor}cc`;
}
