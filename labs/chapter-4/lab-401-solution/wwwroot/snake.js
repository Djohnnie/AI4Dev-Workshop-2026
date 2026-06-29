const boardWidth = 24;
const boardHeight = 14;
const tileSize = 32;
const frameDelayMs = 150;

const canvas = document.getElementById("game");
const context = canvas.getContext("2d");
const scoreElement = document.getElementById("score");
const stateElement = document.getElementById("state");
const messageElement = document.getElementById("message");

const state = {
    snake: [],
    food: { x: 0, y: 0 },
    score: 0,
    phase: "waiting",
    direction: { x: 1, y: 0 },
    nextDirection: { x: 1, y: 0 }
};

resetGame();
window.setInterval(tick, frameDelayMs);
window.addEventListener("keydown", handleKeyDown);
render();

function tick() {
    if (state.phase === "running") {
        updateGame();
    }

    render();
}

function handleKeyDown(event) {
    switch (event.code) {
        case "Space":
            event.preventDefault();
            toggleGameState();
            break;
        case "ArrowUp":
            event.preventDefault();
            queueDirection(0, -1);
            break;
        case "ArrowDown":
            event.preventDefault();
            queueDirection(0, 1);
            break;
        case "ArrowLeft":
            event.preventDefault();
            queueDirection(-1, 0);
            break;
        case "ArrowRight":
            event.preventDefault();
            queueDirection(1, 0);
            break;
    }
}

function toggleGameState() {
    switch (state.phase) {
        case "waiting":
            state.phase = "running";
            messageElement.textContent = "Game on.";
            break;
        case "running":
            state.phase = "paused";
            messageElement.textContent = "Paused. Press Space to continue.";
            break;
        case "paused":
            state.phase = "running";
            messageElement.textContent = "Back in motion.";
            break;
        case "gameover":
            resetGame();
            messageElement.textContent = "New round ready. Press Space to start.";
            break;
    }
}

function queueDirection(x, y) {
    if (state.direction.x === -x && state.direction.y === -y) {
        return;
    }

    state.nextDirection = { x, y };

    if (state.phase === "waiting") {
        messageElement.textContent = "Direction set. Press Space to start.";
    }
}

function updateGame() {
    state.direction = state.nextDirection;
    const head = state.snake[0];

    // The board wraps, so moving off one side enters from the opposite edge.
    const nextHead = wrapPosition({
        x: head.x + state.direction.x,
        y: head.y + state.direction.y
    });

    const grows = positionsEqual(nextHead, state.food);

    // Stepping onto the current tail is allowed when the snake is not growing,
    // because that tail segment disappears during the same update.
    if (hitsSnake(nextHead, !grows)) {
        state.phase = "gameover";
        messageElement.textContent = "You crashed into yourself. Press Space to restart.";
        return;
    }

    state.snake.unshift(nextHead);

    if (grows) {
        state.score += 1;

        if (state.snake.length === boardWidth * boardHeight) {
            state.phase = "gameover";
            messageElement.textContent = "You filled the board. Press Space to play again.";
            return;
        }

        spawnFood();
        messageElement.textContent = "Snack collected. Keep going.";
    } else {
        state.snake.pop();
    }
}

function resetGame() {
    const centerX = Math.floor(boardWidth / 2);
    const centerY = Math.floor(boardHeight / 2);

    state.snake = [
        { x: centerX, y: centerY },
        { x: centerX - 1, y: centerY },
        { x: centerX - 2, y: centerY }
    ];
    state.score = 0;
    state.phase = "waiting";
    state.direction = { x: 1, y: 0 };
    state.nextDirection = { x: 1, y: 0 };
    spawnFood();
}

function spawnFood() {
    do {
        state.food = {
            x: Math.floor(Math.random() * boardWidth),
            y: Math.floor(Math.random() * boardHeight)
        };
    } while (state.snake.some(segment => positionsEqual(segment, state.food)));
}

function hitsSnake(position, ignoreTail) {
    const lastIndex = state.snake.length - 1;

    return state.snake.some((segment, index) => {
        if (ignoreTail && index === lastIndex) {
            return false;
        }

        return positionsEqual(segment, position);
    });
}

function wrapPosition(position) {
    return {
        x: (position.x + boardWidth) % boardWidth,
        y: (position.y + boardHeight) % boardHeight
    };
}

function positionsEqual(a, b) {
    return a.x === b.x && a.y === b.y;
}

function render() {
    drawBackground();
    drawFood();
    drawSnake();
    drawOverlay();
    updateHud();
}

function drawBackground() {
    context.clearRect(0, 0, canvas.width, canvas.height);
    context.fillStyle = "#0e1124";
    context.fillRect(0, 0, canvas.width, canvas.height);

    context.strokeStyle = "rgba(139, 127, 212, 0.18)";
    context.lineWidth = 1;

    for (let x = 0; x <= boardWidth; x += 1) {
        context.beginPath();
        context.moveTo(x * tileSize, 0);
        context.lineTo(x * tileSize, boardHeight * tileSize);
        context.stroke();
    }

    for (let y = 0; y <= boardHeight; y += 1) {
        context.beginPath();
        context.moveTo(0, y * tileSize);
        context.lineTo(boardWidth * tileSize, y * tileSize);
        context.stroke();
    }
}

function drawFood() {
    context.fillStyle = "#ff7b72";
    context.beginPath();
    context.arc(
        state.food.x * tileSize + tileSize / 2,
        state.food.y * tileSize + tileSize / 2,
        tileSize * 0.28,
        0,
        Math.PI * 2);
    context.fill();
}

function drawSnake() {
    state.snake.forEach((segment, index) => {
        context.fillStyle = index === 0 ? "#ffd866" : "#3fb950";
        context.fillRect(
            segment.x * tileSize + 3,
            segment.y * tileSize + 3,
            tileSize - 6,
            tileSize - 6);
    });
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
    context.fillText(overlayTitle(), canvas.width / 2, canvas.height / 2 - 12);

    context.fillStyle = "#c4baea";
    context.font = "400 22px Segoe UI";
    context.fillText(overlaySubtitle(), canvas.width / 2, canvas.height / 2 + 26);
}

function overlayTitle() {
    switch (state.phase) {
        case "waiting":
            return "Ultimate Snake";
        case "paused":
            return "Paused";
        case "gameover":
            return "Game Over";
        default:
            return "";
    }
}

function overlaySubtitle() {
    switch (state.phase) {
        case "waiting":
            return "Press Space to start";
        case "paused":
            return "Press Space to continue";
        case "gameover":
            return "Press Space to restart";
        default:
            return "";
    }
}

function updateHud() {
    scoreElement.textContent = String(state.score);
    stateElement.textContent = capitalize(state.phase);
}

function capitalize(value) {
    return value.charAt(0).toUpperCase() + value.slice(1);
}
