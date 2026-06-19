# Lab 402 — Ultimate Snake from Scratch with GitHub Copilot CLI

> **Chapter:** Chapter 4  
> **Skill focus:** Building a small web app **from an empty folder** with **GitHub Copilot CLI**  
> **Difficulty:** ⭐⭐⭐⭐

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

1. Use `gh copilot suggest` to generate the initial scaffold commands.
2. Use `gh copilot explain` whenever a generated command is unfamiliar.
3. Create a minimal .NET 10 web app that serves static files.
4. Add HTML, CSS, and JavaScript for the browser game.
5. Re-run Copilot CLI for the next command when you are unsure what to do next.

---

## 💬 Example prompts for Copilot CLI

```bash
gh copilot suggest "create a new .NET 10 web app in the current folder"
gh copilot suggest "show me the PowerShell command to create a wwwroot folder with index.html styles.css and snake.js"
gh copilot suggest "run this ASP.NET Core web app locally"
gh copilot explain "dotnet new web --framework net10.0"
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
