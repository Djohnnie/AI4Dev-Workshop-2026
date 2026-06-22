# Lab 601 — Ultimate Snake Across the Entire Lifecycle

> **Chapter:** Chapter 6  
> **Skill focus:** Rebuilding Ultimate Snake while deliberately using the full AI-assisted lifecycle from analysis through review  
> **Difficulty:** ⭐⭐⭐⭐

← Back to [Labs Index](../../README.md)

---

## 🎯 Goal

Build **Ultimate Snake** again, but this time do it with **everything you learned in chapter 6**.

This lab reuses the same gameplay target from the earlier Snake labs:

- browser-based UI
- canvas-based board and HUD
- arrow-key movement
- spacebar start, pause, and restart
- wrap-around movement
- growth on food
- self-collision game over

The difference is the workflow.

You should not jump straight into implementation. Use Copilot to work through the full lifecycle:

1. analyse the problem
2. plan the smallest useful slice
3. implement the game
4. validate and debug it
5. document it
6. package it like a reviewable pull request

Like lab 501, this folder starts with **workflow assets instead of app code**. The code is intentionally missing so the lifecycle is the exercise.

---

## 🧭 What “everything from this chapter” means

Your finished lab should reflect the chapter 6 themes:

1. **Analysis first**  
   Turn the Snake brief into requirements, architecture decisions, risks, and a delivery plan before you scaffold anything.

2. **Implementation with intent**  
   Build the smallest working web version first instead of asking Copilot for a giant one-shot app.

3. **Validation and test thinking**  
   Identify the game rules most likely to break and create a clear verification approach for them.

4. **Diagnostics and debugging**  
   Add enough visibility that you can explain failures or weird gameplay behavior quickly.

5. **Documentation and diagramming**  
   Leave behind a short explanation of the app plus a small diagram artifact that would help the next developer.

6. **PR-quality output**  
   Summarize the work like a pull request: scope, risks, reviewer checklist, and what still needs human judgment.

---

## ✅ Suggested workflow

### Phase 1 — Analyse before coding

Start by turning the Snake target into:

- functional requirements
- non-functional requirements
- architecture decisions
- implementation phases
- risks and open questions

You can invoke:

```text
#file:.github/prompts/analyze-ultimate-snake-plan.prompt.md
```

### Phase 2 — Scaffold the host

Create the .NET 10 ASP.NET Core host and the static file shell.

You can invoke:

```text
#file:.github/prompts/scaffold-ultimate-snake-host.prompt.md
```

### Phase 3 — Finish the gameplay

Implement the browser game so it matches the earlier Snake labs.

You can invoke:

```text
#file:.github/prompts/finish-ultimate-snake-gameplay.prompt.md
```

### Phase 4 — Harden it with chapter 6 thinking

Before you declare success, use Copilot to:

- identify fragile gameplay rules
- add lightweight diagnostics or debug visibility
- create a verification checklist
- capture likely failure scenarios

You can invoke:

```text
#file:.github/prompts/harden-ultimate-snake-quality.prompt.md
```

### Phase 5 — Document it

Produce a small set of artifacts that explain the result:

- a short README update or notes section
- a tiny architecture or flow explanation
- a diagram saved as `*.drawio.png` or `*.drawio`

### Phase 6 — Package it like a PR

Ask Copilot to create:

- a PR title
- a PR body
- a reviewer checklist
- a short release-note style summary
- a note about what still needs human review

You can invoke:

```text
#file:.github/prompts/package-ultimate-snake-pr.prompt.md
```

---

## 🛠️ Deliverables

By the end of the lab, participants should have:

1. a working .NET 10 web-based Snake game
2. a short analysis/plan captured before implementation
3. some form of validation checklist or regression approach
4. lightweight diagnostics that make debugging easier
5. a small documentation artifact, ideally including a `*.drawio.png` or `*.drawio` file
6. a PR-ready summary of the change

---

## 💬 Example follow-up prompts

```text
What is the smallest vertical slice of Ultimate Snake that proves the app architecture before I build the full gameplay loop?
```

```text
List the gameplay rules most likely to break in a Snake implementation and tell me how to validate each one.
```

```text
Suggest a tiny debug overlay or status mechanism that would help me explain movement, collision, and restart bugs during a workshop demo.
```

```text
Draft a pull request summary for this Snake implementation, including reviewer checklist, risk notes, and what still needs human judgment.
```

---

## ▶️ Completion criteria

- The app runs with `dotnet run`
- The browser shows the Ultimate Snake UI
- The gameplay matches the earlier Snake target
- The work shows analysis, validation, diagnostics, documentation, and review packaging instead of only code generation

---

## 🏁 References

Gameplay reference:

```text
..\..\chapter-04\lab-401-solution
```

Context-asset reference:

```text
..\..\chapter-05\lab-501-solution
```

---

← Back to [Labs Index](../../README.md) | Previous: [Lab 501 — Ultimate Snake with Instructions, Prompt Files, and Skills](../../chapter-05/lab-501/README.md) | Next: [Lab 801 — Multiplayer Ultimate Snake](../../chapter-08/lab-801/README.md)
