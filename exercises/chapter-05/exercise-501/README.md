# Exercise 501 — Escape Room DI Demo

> **Chapter:** Chapter 5, Exercise 1  
> **Skill focus:** Prompting a small .NET app into place, then replacing the default DI container with a custom one  
> **Difficulty:** ⭐⭐

← Back to [Exercise Index](../../README.md)

---

## 🎯 Overview

This exercise builds a tiny escape room console app with **Spectre.Console** and the default .NET dependency injection container. It is intentionally small: three rooms, three riddles, a hint service, and a short summary screen.

The app is designed to be a demo-friendly target for prompting. First, use Copilot to understand and extend the existing service graph. Then, later in the workshop, replace the built-in container with your own custom DI container prompt.

---

## 🗂️ Project Structure

```
501/
├── EscapeRoom.csproj
├── Program.cs
├── EscapeRoomGame.cs
└── README.md
```

### What the app does

- Shows a Spectre.Console title screen
- Walks the player through three escape-room riddles
- Gives up to three attempts per room
- Shows hints after failed attempts
- Prints a final summary table with the result of each room

---

## ✅ Your Task

### Phase 1 — Run the demo

1. Open `Program.cs` and notice the default .NET DI setup.
2. Run the app:

```bash
cd 501
dotnet run
```

3. Play through the room and see how the services work together.

### Phase 2 — Prompt for the replacement

4. Ask Copilot to replace the `ServiceCollection` setup with your own custom DI container.
5. Keep the game code unchanged if possible.
6. Ask Copilot to preserve constructor injection for:
   - `IEscapeRoomGame`
   - `IEscapeRoomScript`
   - `IHintService`
   - `IEscapeRoomUi`

### Phase 3 — Improve the prompt

7. Try a vague prompt first:

```text
build a custom di container
```

8. Then try a better prompt:

```text
Build a small custom DI container for this escape room app in C#.
It must support singleton registrations, resolve constructor dependencies,
and keep the Spectre.Console game working without changing the game logic.
```

---

## 🤖 Copilot Skills to Practise

| Task | How |
|------|-----|
| Prompt the app scaffold | Ask Copilot to build a small Spectre.Console console app |
| Replace the container | Ask Copilot to implement a custom DI container |
| Improve prompt quality | Compare vague vs specific container prompts |
| Refactor safely | Ask Copilot to keep the game logic unchanged while changing wiring |

---

## 🏁 Stretch Goals

1. Add one more room and see how much of the app Copilot can extend cleanly.
2. Ask Copilot to add transient service support to the custom container.
3. Ask Copilot to make the output look more dramatic using Spectre.Console panels and tables.

---

← Back to [Exercise Index](../../README.md)
