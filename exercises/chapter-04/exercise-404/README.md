# Exercise 404 — Vibe-Code a Slot Machine

> **Chapter:** Chapter 4, Exercise 4  
> **Skill focus:** Vibe-coding with GitHub Copilot CLI — prompting from guardrails instead of hand-writing UI code  
> **Difficulty:** ⭐⭐

← Back to [Exercise Index](../../README.md)

---

## 🎯 Overview

Your job is to use **GitHub Copilot CLI** to create a brand-new **.NET 10 WinForms slot machine** from scratch.

You are not building from a starter project. Instead, you will give Copilot CLI a clear goal, a few non-negotiable requirements, and enough guardrails that it can scaffold the app, write the UI, implement the game logic, and iterate until it runs.

---

## 🗂️ Expected Project Shape

Create a fresh app inside this exercise folder. A good target structure is:

```text
404/
├── README.md
└── LuckySlots/
    ├── LuckySlots.csproj
    ├── Program.cs
    ├── MainForm.cs
    └── MainForm.Designer.cs
```

If Copilot CLI chooses slightly different file names, that is fine. The important part is that it creates a working **WinForms** project in a new folder and not another console app.

A finished reference implementation is available in [`exercise-404-solution`](../exercise-404-solution/README.md).

---

## ✅ Your Task

### Phase 1 — Set up the challenge

1. Open a terminal in the `404` folder.
2. Launch Copilot CLI.
3. Tell it to create a fresh **.NET 10 WinForms** app called `LuckySlots`.

### Phase 2 — Give Copilot CLI guardrails

4. Use a prompt like this, or write your own version:

   ```text
   Create a new .NET 10 WinForms app called LuckySlots in the current directory.
   Build a simple slot machine game from scratch.

   Requirements:
   - Use Windows Forms, not WPF, MAUI, Blazor, console, or a game engine
   - Show 3 reels with emoji or plain text symbols such as CHERRY, LEMON, BELL, and 7
   - Start the player with 100 credits
   - Let the player choose a bet of 1, 5, or 10 credits
   - Add a Spin button and a Reset Game button
   - Disable Spin while a spin is in progress
   - Use a short timer-based reel animation so the reels visibly change before stopping
   - Show the current credits, the selected bet, and a result/status message after every spin
   - Payout rules:
     - 3x 7 pays 50x the bet
     - 3 matching symbols pays 10x the bet
     - 2 matching symbols pays 3x the bet
     - No match loses the bet
   - Prevent betting when the player does not have enough credits
   - When credits reach 0, clearly show Game Over until Reset Game is used
   - Keep the UI in a single main window and avoid external image assets
   - Use clear class/method names and keep the code easy for workshop participants to read

   At the end, run dotnet build and fix any errors.
   ```

5. Review the plan before approving commands. Make sure Copilot CLI is about to scaffold a **WinForms** project and not drift into a different stack.

### Phase 3 — Watch and approve

6. Approve commands one at a time.
7. Pay attention to:
   - whether Copilot uses `dotnet new winforms`
   - how it creates the form and wires up event handlers
   - how it reacts if the build fails or the UI code needs a second pass

### Phase 4 — Run and verify

8. Start the app and test the guardrails:

   - the app opens as a desktop window
   - credits start at 100
   - you can choose 1, 5, or 10 as the bet
   - Spin is disabled during the animation
   - credits increase or decrease based on the payout rules
   - Reset Game restores the starting state
   - the UI clearly shows when the game is over

### Phase 5 — Extend

9. After the first version works, ask Copilot CLI for one improvement. Ideas:

   - add a small spin counter
   - add a win/loss history list
   - add keyboard shortcuts
   - add a dark/light theme toggle
   - add a tiny “near miss” message when 2 reels match and the third is close

---

## 🤖 Copilot Skills to Practise

| Task | How |
|------|-----|
| Vibe-code from constraints | Give Copilot CLI a goal plus guardrails, not line-by-line implementation steps |
| Keep the agent on track | Correct it if it picks the wrong UI stack or ignores a payout rule |
| Approve shell commands | Watch the project scaffolding, restore, build, and code edits happen live |
| Iterate conversationally | Add polish with one follow-up prompt at a time |

---

## 💡 Why This Exercise

This exercise shows that vibe coding works best when you pair freedom with a few sharp constraints. “Build a slot machine” is too vague on its own; “build a .NET 10 WinForms slot machine with these rules, this UI, and these payout constraints” is enough structure for Copilot CLI to stay useful.

It also forces the CLI to do real project setup work: choose the right template, scaffold the form, wire up events, manage timer-driven UI behaviour, and keep iterating until the build succeeds.

---

## 🏁 Stretch Goals

1. **Sound effects without assets.** Ask Copilot CLI to use simple system sounds for spin, win, and lose events.
2. **Payout table panel.** Add a small panel in the form that explains the win rules.
3. **Best run tracker.** Track the highest credit total reached during the session.
4. **Cheat mode.** Add a hidden debug checkbox that makes wins more frequent for demos.

---

← Back to [Exercise Index](../../README.md) | Related: [Slide deck](../../../content/2-day/chapter-04/SLIDES.md) | Previous: [Exercise 403](../exercise-403/README.md)
