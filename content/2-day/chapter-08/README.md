[🏠 Workshop Home](../../../README.md) | [📊 Slides](SLIDES.md) | [← Chapter 7 — Level Up: Best Practices for AI-Powered Development](../chapter-07/README.md)

---

# Chapter 8— Get Your Hands Dirty: Real-World AI in Action

> **Duration:** 90 minutes | Day 2, 15:00 – 16:30

The capstone session. It opens by recapping the whole workshop arc — Chapters 1 through 7 — then tests that synthesis with three integrated quizzes, and finally turns everyone loose on **Lab 801: Multiplayer Ultimate Snake**, a room-based, real-time multiplayer build that combines every skill from the two days into one demo-ready project.

---

## 🎯 Learning Objectives

By the end of this chapter, participants will be able to:

- Recall and connect the through-line of all seven prior chapters: intentional AI use, control, and engineering judgment
- Choose the strongest first move on an unfamiliar repo with an ambiguous, data-sensitive ticket
- Respond to an intermittent failure with a disciplined reproduce-investigate-fix-verify loop instead of blind regeneration
- Build a repeatable team workflow with project instructions, reusable prompts/skills, reviewable slices, and human judgment
- Build a browser-based multiplayer Snake game with rooms, shared state, and real-time synchronization
- Combine UI design, server-owned state, real-time sync, and a chosen Copilot workflow into one shippable, demo-ready capstone

---

## 📋 Content Outline

### 1. Workshop Recap — Chapters 1 to 7
A rapid synthesis of the journey so far, one slide per chapter, framing why each lesson matters for the final build.

- **Chapter 1 — Welcome to the AI Revolution:** AI is already part of the job, so learn to use it intentionally. Mindset shift toward AI-assisted workflows; judgment, tradeoffs, and ownership stay with the developer; the workshop through-line is "use AI with intent, stay in control."
- **Chapter 2 — Meet Your New Best Friend (GitHub Copilot):** Copilot as a coding companion across Ask mode, inline chat, and autocomplete. Lab 201 showed how constraining surfaces makes the interaction style visible. Good results start with precise intent and tight feedback loops.
- **Chapter 3 — Power with Purpose (Using AI Responsibly):** Use AI with standards, not just enthusiasm. Fairness and bias, privacy and trust, and human accountability — every prompt, diff, and architecture choice still gets a responsible-engineering review.
- **Chapter 4 — Let Your AI Co-Pilot Take the Wheel:** From assistance to orchestration — Agent Mode, Plan Mode and Edits, and Copilot CLI. Larger multi-file tasks make the choice of Copilot surface part of the engineering design; review and checkpoints still matter.
- **Chapter 5 — Speak AI's Language (Prompts & Context):** Better context creates better engineering output. The context window, prompt precision, and working-set selection directly affect quality — essential when a task spans a web client, a .NET server, sockets, and gameplay rules.
- **Chapter 6 — AI Across the Entire Software Lifecycle:** The assistant helps before and after coding — analysis and planning, implementation and debugging, testing and documentation. The best use of AI is sustained support across the lifecycle, not one big answer.
- **Chapter 7 — Level Up (Best Practices):** Repeatable habits make AI sustainable — define the task, choose the surface, review outputs, keep context clean, validate what matters. Consistency over novelty; a workflow teams can trust, repeat, and improve.

### 2. Interactive Quiz 19 — First Move on an Unfamiliar Repo
- Scenario: you inherit an unfamiliar repo and an ambiguous feature ticket touching internal data — what is the strongest first move with Copilot?
- **Answer (B):** clarify requirements, inspect the codebase with targeted context, check safety boundaries, and turn the work into a small plan *before* generating code.
- Reinforces that jumping straight to Agent Mode, pasting real customer data into chat, or drafting the PR first are all weaker starts.

### 3. Interactive Quiz 20 — Responding to Intermittent Failures
- Scenario: a Copilot-generated feature passes a happy-path demo but fails intermittently in CI and in the browser — what is the best integrated response?
- **Answer (D):** reproduce it with tests and browser evidence, inspect logs and traces, fix the root cause, rerun the relevant checks, and only then prepare the PR.
- Reinforces disciplined debugging over switching models, disabling flaky tests, or generating a PR description first.

### 4. Interactive Quiz 21 — A Repeatable Team Workflow
- Scenario: your team wants a repeatable way to build future features with Copilot, not just one successful demo — which approach is strongest?
- **Answer (A):** add project instructions, reusable prompt files or skills, build in reviewable slices, generate tests and docs, then use Copilot review with humans making final judgments.
- Reinforces that giant one-off prompts, web-only chat without local context, and trusting Autofix to replace human review are not sustainable.

### 5. Lab 801 — Multiplayer Ultimate Snake (the capstone)
The final lab turns the earlier single-player game into a shared, room-based experience with live updates — *Ultimate Snake Rooms*.

- **Build target:** a browser-based multiplayer Snake game with room creation, joining by a short code, and a shared board (e.g. room `AB12`, `2 / 4` players, phase `Running`, host and players visible).
- **Core multiplayer twist:** several snakes race toward one shared food item; the server decides who wins and what collides.
- **Why it is the capstone:** it combines UI design, server state, real-time sync, Copilot workflow choices, and demo-ready behavior — every skill from the two days in one build.

### 6. Lab 801 — Expectations
What "done" looks like — think in rooms, shared state, and real-time updates; a clean two-player experience is enough to prove the concept.

- **Room flow:** one player creates a room and gets a short code; another joins with that code; the UI shows the room code, player list, and room phase.
- **Shared gameplay:** all snakes move on the same shared board; there is one shared food item for everyone; the first snake to reach it gets the growth and score.
- **Synchronization rules:** clients send small input messages such as direction changes; the server owns room state and broadcasts updates; collisions against self or another snake must end that snake.
- **Good enough to ship:** `dotnet run` starts the app locally; two browsers can join the same room and see the same state; the result is playable and easy to explain in a live demo.

---

## 🧪 Chapter 8 Labs

- **[Lab 801 — Multiplayer Ultimate Snake](../../../labs/chapter-08/lab-801/README.md)** — Build a browser-based, room-based multiplayer Snake game: create/join rooms by code, share one board and one food item across several snakes, let the server own state and broadcast real-time updates, and end any snake that collides. Aim for a clean two-player experience that runs with `dotnet run` and is easy to demo.

---

## 🔗 Resources & References
- [GitHub Copilot documentation](https://docs.github.com/en/copilot)
- [.NET download](https://dot.net/download) — `dotnet run` to start the app locally
- [GitHub Copilot changelog](https://github.blog/changelog/label/copilot/)

---

## 🗒️ Facilitator Notes
- Keep the seven recap slides brief — they are a synthesis ramp into the lab, not new teaching; lean on the through-line "use AI with intent, stay in control."
- Use the three quizzes to surface the disciplined habits the lab depends on: clarify-then-plan, reproduce-then-fix, and a repeatable instructions-and-review workflow.
- Frame Lab 801 around rooms, shared state, and real-time updates; a clean two-player demo is the bar, not a polished four-player game.
- Remind participants the server owns room state and decides collisions and the food winner — the client just sends small input messages.
- This is the capstone: protect time for the build and for the live `dotnet run` two-browser demo at the end.

---

[🏠 Workshop Home](../../../README.md) | [← Chapter 7 — Level Up: Best Practices for AI-Powered Development](../chapter-07/README.md)
