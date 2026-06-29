const boardWidth = 24;
const boardHeight = 14;
const tileSize = 32;

const canvas = document.getElementById("game");
const context = canvas.getContext("2d");
const scoreElement = document.getElementById("score");
const stateElement = document.getElementById("state");
const messageElement = document.getElementById("message");

const state = {
    snake: [{ x: 12, y: 7 }, { x: 11, y: 7 }, { x: 10, y: 7 }],
    food: { x: 16, y: 5 },
    score: 0,
    phase: "waiting",
    direction: { x: 1, y: 0 },
    nextDirection: { x: 1, y: 0 }
};

render();

// TODO: Build the real game loop here.
// Suggested approach:
// 1. Register keyboard input for arrow keys and spacebar.
// 2. Advance the snake on a timer.
// 3. Wrap through the edges.
// 4. Grow on food.
// 5. End the game on self-collision.

function render() {
    drawBackground();
    drawFood();
    drawSnake();
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

function updateHud() {
    scoreElement.textContent = String(state.score);
    stateElement.textContent = capitalize(state.phase);
    messageElement.textContent = "Starter ready. Use Agent Mode to finish the game.";
}

function capitalize(value) {
    return value.charAt(0).toUpperCase() + value.slice(1);
}
