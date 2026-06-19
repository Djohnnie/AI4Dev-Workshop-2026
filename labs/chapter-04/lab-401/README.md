# Lab 401 — Ultimate Snake Web

> **Chapter:** Chapter 4  
> **Skill focus:** Using **GitHub Copilot Agent Mode** to build a small web app end to end  
> **Difficulty:** ⭐⭐⭐

---

## 🎯 Goal

Turn this starter into a working **web-based Ultimate Snake** game.

This lab is designed for **Agent Mode**. The starter already gives you:

- a .NET 10 web app that runs with `dotnet run`
- static file hosting
- a styled page shell
- a canvas-based playfield
- starter UI wiring and TODO-marked JavaScript

Your job is to use Agent Mode to finish the game.

---

## 🗂️ Project Structure

```text
lab-401/
├── UltimateSnakeWeb.csproj
├── Program.cs
├── README.md
└── wwwroot/
    ├── index.html
    ├── styles.css
    └── snake.js
```

---

## ✅ Expectations

Use **Agent Mode** as the primary way to complete the implementation.

The finished game should:

- show an **Ultimate Snake** web UI
- render the game on the canvas
- start and pause with **spacebar**
- move with the **arrow keys**
- wrap through the edges
- grow when food is eaten
- end on self-collision
- show score and game state in the page UI

---

## ▶️ Run the starter

From this folder:

```bash
dotnet run
```

Then open the local URL shown in the terminal.

---

## 🛠️ Suggested Agent Mode prompt

```text
Finish this starter into a complete web-based Snake game.

Keep the existing UI structure and styling direction.
Use the canvas in wwwroot/index.html.
Implement keyboard input with arrow keys and spacebar.
The snake should wrap at the edges, grow when it eats food, and end on self-collision.
Update the score and status text in the page.
Do not add any external packages.
```

---

## 👀 What participants should practise

- letting Agent Mode inspect the starter before editing
- reviewing proposed diffs before accepting them
- approving terminal commands intentionally
- using follow-up prompts when the first pass is close but not quite right

---

## 🏁 Reference

A complete solution is available in:

```text
../lab-401-solution
```
