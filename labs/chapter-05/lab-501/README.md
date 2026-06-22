# Lab 501 — Ultimate Snake with Instructions, Prompt Files, and Skills

> **Chapter:** Chapter 5  
> **Skill focus:** Reusing `copilot-instructions.md`, prompt files, and a skill file while vibe-coding the same Snake game from chapter 4  
> **Difficulty:** ⭐⭐⭐⭐

---

## 🎯 Goal

Build **Ultimate Snake** again, but this time start by shaping Copilot before it writes code.

This lab reuses the **empty-folder feel of Lab 402** and the **same Snake target from Lab 401**, but the starter now includes:

- a repository-level **`.github/copilot-instructions.md`**
- reusable **prompt files** in `.github/prompts/`
- one **skill file** in `.github/skills/`

The code is intentionally missing. The context assets are the real starter.

---

## 🗂️ Starter Structure

```text
lab-501/
├── README.md
└── .github/
    ├── copilot-instructions.md
    ├── prompts/
    │   ├── scaffold-ultimate-snake-host.prompt.md
    │   └── finish-ultimate-snake.prompt.md
    └── skills/
        └── ultimate-snake-playtest/
            └── SKILL.md
```

---

## ✅ Expectations

Participants should use the chapter 5 techniques deliberately:

1. **Read the repo instructions first** and understand what they tell Copilot automatically.
2. **Invoke a prompt file** instead of typing the whole task from scratch.
3. **Refine the prompt** with the four ingredients from the chapter:
   - task
   - context
   - examples
   - constraints
4. **Use follow-up prompts** when the first result is close but incomplete.
5. End with the same playable Snake game from chapter 4:
   - browser-based UI
   - arrow-key movement
   - spacebar start/pause/restart
   - wrap-around movement
   - growth on food
   - self-collision game over
   - score and status HUD

---

## 🛠️ Suggested workflow

1. Open this folder in VS Code.
2. Read `.github/copilot-instructions.md`.
3. Invoke the first prompt file from the prompt picker, or reference it directly.
4. Let Copilot scaffold the .NET 10 web app and static files.
5. Invoke the second prompt file or use a follow-up prompt to complete the game loop.
6. Ask Copilot to review or validate the result using the included skill file.
7. Run the app locally and compare it with the chapter 4 Snake implementation.

---

## 💬 Example prompts

Use the prompt files first, then improve them with what you learned in chapter 5.

### Directly invoke the scaffold prompt file

```text
#file:.github/prompts/scaffold-ultimate-snake-host.prompt.md
```

### Then refine with a stronger follow-up

```text
Finish the Snake implementation.

Task: complete the game loop and HUD.
Context: this must stay a .NET 10 ASP.NET Core static-files app with plain HTML, CSS, and JavaScript.
Examples: match the same gameplay from the chapter 4 Ultimate Snake lab.
Constraints: no npm packages, no frameworks, keep the logic in wwwroot/snake.js.
```

### Or use the skill-oriented validation pass

```text
Review this Snake implementation and check whether the gameplay matches the lab expectations.
```

---

## ▶️ Completion criteria

- The app runs with `dotnet run`
- The browser shows the Ultimate Snake UI
- The game loop behaves like the chapter 4 version
- The implementation was shaped by the repo instructions and prompt assets, not only by ad-hoc prompting

---

## 🏁 Reference

A complete solution is available in:

```text
../lab-501-solution
```
