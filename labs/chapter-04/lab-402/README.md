# Lab 402 — Ultimate Snake from Scratch with GitHub Copilot CLI

> **Chapter:** Chapter 4  
> **Skill focus:** Building a small web app **from an empty folder** with **GitHub Copilot CLI**  
> **Difficulty:** ⭐⭐⭐⭐

← Back to [Labs Index](../../README.md)

---

## 🎯 Goal

Start from this nearly empty folder and build the same **web-based Ultimate Snake** game as Lab 401, but this time with a **terminal-first Copilot workflow**.

This starter intentionally contains **no project files** and **no app code**. The point of the exercise is to use Copilot CLI to scaffold, explain, and iterate from scratch.

---

## ✅ Expectations

Participants should use **GitHub Copilot CLI** as the main workflow driver.

The final app should:

- run locally in the browser
- show an **Ultimate Snake** title and simple HUD
- move with **arrow keys**
- start and pause with **spacebar**
- wrap through the edges
- grow after eating food
- end on self-collision

---

## 🛠️ Suggested workflow

1. Install the GitHub Copilot CLI (`winget install GitHub.Copilot`, `brew install copilot-cli`, or `npm install -g @github/copilot`) and run `/login` on first launch.
2. Open a terminal in this folder and start the CLI.
3. Describe the goal in plain language and let the CLI propose the shell commands to scaffold a minimal .NET 10 web app that serves static files.
4. **Read every command before approving it** — the CLI runs `dotnet`, file, and shell commands itself once you confirm.
5. Iterate: add HTML, CSS, and JavaScript for the browser game, and ask the CLI for the next step whenever you are unsure what to do.

---

## 💬 Example prompts for Copilot CLI

```text
Create a new .NET 10 web app in the current folder that serves static files from wwwroot.
Add a wwwroot folder with index.html, styles.css, and snake.js, then run the app locally.
Explain what "dotnet new web --framework net10.0" does before you run it.
```

---

## ▶️ Completion criteria

- The app runs with `dotnet run`
- The browser shows the Ultimate Snake UI
- The game loop works
- Keyboard input works
- Wrap-around movement works
- Self-collision ends the round

---

## 🏁 Reference

A complete solution is available in:

```text
../lab-402-solution
```

---

← Back to [Labs Index](../../README.md) | Previous: [Lab 401 — Ultimate Snake Web](../../chapter-04/lab-401/README.md) | Next: [Lab 501 — Ultimate Snake with Instructions, Prompt Files, and Skills](../../chapter-05/lab-501/README.md)
