# Lab 201 — Ultimate Snake

> **Chapter:** Chapter 2  
> **Skill focus:** Building a complete console game with GitHub Copilot Chat (Ask), inline chat, and ghost text  
> **Difficulty:** ⭐⭐⭐

← Back to [Labs Index](../../README.md)

---

## 🎯 Goal

Turn this starter into a working **Snake** game in a .NET 10 console app using **Spectre.Console**.

The finished game should:

- show the **Ultimate Snake** title screen
- let the player move with the **arrow keys**
- use **spacebar** to **start** and **pause**
- grow the snake when it eats food
- wrap through the board edges instead of dying on walls
- end the game on self-collision
- keep rendering the board and score in the terminal
- render the board with a space between horizontal tiles so horizontal and vertical movement feel more balanced

---

## 🗂️ Project Structure

```text
lab-201/
├── UltimateSnake.csproj
├── Program.cs
└── README.md
```

`Program.cs` already gives you:

- a .NET 10 console app
- `Spectre.Console` installed
- a styled **Ultimate Snake** title
- a starter board render
- placeholder types for direction, game state, and grid positions

---

## ✅ Rules for this lab

For this lab, participants may only use:

- **GitHub Copilot chat mode (Ask)**
- **Inline chat**
- **Autocomplete / ghost text**

Do **not** use Agent Mode, Copilot Edits, or the Copilot CLI for the implementation.

---

## ▶️ Run the starter

From this folder:

```bash
dotnet run
```

---

## 🛠️ Your Task

Implement the missing game logic in `Program.cs`.

### Suggested steps

1. Build a game loop that redraws the board on a timer.
2. Read keyboard input without freezing the loop.
3. Track the snake body and current movement direction.
4. Spawn food at random open positions.
5. Support **spacebar** for start/pause.
6. Wrap the snake from left to right and top to bottom.
7. Detect self-collisions and show a game-over state.
8. Render each tile with horizontal spacing.

### Acceptance criteria

- The game starts in a waiting state.
- Pressing **spacebar** starts the game.
- Pressing **spacebar** again pauses the game.
- Arrow keys change direction.
- The snake grows after eating food.
- Moving past an edge brings the snake out on the opposite side.
- Hitting the snake body ends the game.

---

## 🤖 Good Copilot prompts to try

- `Implement the missing game loop in this starter without splitting the file.`
- `Add non-blocking keyboard input so arrow keys change direction and space toggles start/pause.`
- `Render the snake board with Spectre.Console, keep the title screen intact, and leave a space between horizontal tiles.`
- `Add wrap-around movement, self-collision detection, score tracking, and random food spawning.`

---

## 🏁 Reference

A complete working reference implementation is available in the sibling folder:

```text
../lab-201-solution
```

---

← Back to [Labs Index](../../README.md) | Next: [Lab 401 — Ultimate Snake Web](../../chapter-04/lab-401/README.md)
