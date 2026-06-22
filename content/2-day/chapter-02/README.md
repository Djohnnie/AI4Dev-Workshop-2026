[🏠 Workshop Home](../../../README.md) | [📊 Slides](SLIDES.md) | [← Chapter 1 — Welcome to the AI Revolution!](../chapter-01/README.md) | [Chapter 3 — Power with Purpose: Using AI Responsibly →](../chapter-03/README.md)

---

# Chapter 2— Meet Your New Best Friend: GitHub Copilot

> **Duration:** 90 minutes | Day 1, 10:45 – 12:15

A hands-on introduction to GitHub Copilot in the IDE, structured around three core capabilities: code completions (ghost text), the **Ask** chat mode, and **slash commands**. The chapter opens with how Copilot is now billed — the move to token-based GitHub AI Credits — then runs participants through a series of small exercises and a closing Snake-game lab to build real prompt-steering and review discipline.

---

## 🎯 Learning Objectives

By the end of this chapter, participants will be able to:

- Explain how Copilot billing is changing to token-based **GitHub AI Credits** and why GitHub is making the move
- Distinguish **input**, **cached**, and **output** tokens and understand how each is priced
- Read model pricing examples and connect model choice and task size to cost
- Use ghost-text completions effectively — accept, dismiss, browse alternatives, and accept word by word
- Use Copilot Chat's **Ask** mode for explanation and guidance before changing code
- Apply the core slash commands — `/explain`, `/fix`, `/tests`, `/doc` — and inline chat for in-editor edits
- Steer Copilot one behaviour at a time and critically review every suggestion before accepting it

---

## 📋 Content Outline

### 1. How Copilot Billing Is Changing
- **Copilot Billing Is Changing** — starting June 1, 2026, premium request units move to **GitHub AI Credits**
  - Old model: *request-based pricing* counted premium requests; one interaction could hide many tokens, so a quick chat and long agent work looked too similar on the bill
  - New model: *AI Credit pricing* counts **token usage** — input, output, and cached tokens — so model choice and task size map more directly to cost
  - **1 GitHub AI Credit = $0.01 USD**
- **Why GitHub Is Moving** — Copilot evolved from autocomplete into agentic, multi-step development work
  - The old model was not sustainable: agentic usage makes many model calls, frontier models cost more (better reasoning, larger context = more compute), and one request was too blunt a unit
  - The new model aligns usage to real work, supports **budgets and pooled org usage** (admins can cap spend, org credits can be shared), keeps **plan prices the same**, and includes monthly AI Credits with paid plans
  - **Code completions remain unlimited and do not consume AI Credits**

### 2. Understanding Token-Based Billing
- **Input Tokens vs Cached Tokens vs Output Tokens** — different token types are counted at different rates
  - **Input tokens:** new content sent in this request — the latest user prompt, files and chat context, system and tool instructions
  - **Cached tokens:** prompt tokens reused from earlier calls — same chat history, unchanged file context — often billed more cheaply
  - **Output tokens:** what the model generates back — replies, code, explanations — usually the priciest bucket
  - One Copilot interaction = *prompt in → reused context → answer out*, which is why token-based billing is more precise than counting one request at a time
- **Model Pricing Examples** — prices vary by model and token type (all per 1M tokens; sample only — availability and rates can change)
  - Models shown span **Powerful**, **Versatile**, and **Lightweight** categories — e.g. GPT-5.5, GPT-5.3-Codex, Claude Haiku 4.5, Claude Sonnet 4.6, Claude Opus 4.8, Gemini 3.5 Flash, MAI-Code-1-Flash
  - Each model lists separate **Input / Cached input / Output** rates, reinforcing that output is consistently the most expensive bucket
  - Code completions remain unlimited for paid plans
- **Interactive poll** — *What LLM models are you using in your coding tools?*

### 3. Section 1 of 3 · Code Completions
- **Copilot Ghost-Text Suggestions** — the grey inline text is a *preview*; it is only inserted after you accept it
  - **How it appears:** while you type or pause; suggestions improve after comments or clear names
  - **How to use it:** `Tab` accepts the suggestion, `Esc` dismisses it
  - **How to steer it:** clear comments improve results; open relevant files for context
  - Best practice: use ghost text to move faster, then review it like any other proposed code
- **Browse Alternatives & Accept Incrementally** — you don't have to take the first full suggestion
  - **Browse alternatives:** `Alt+]` shows the next suggestion, `Alt+[` goes back to the previous one
  - **Accept word by word:** `Ctrl+Right` accepts the next word — useful when only the start is correct
  - **Why it matters:** keep the part that saves time and avoid accepting the wrong tail
  - Best practice: cycle when the first idea is close, then accept only what you actually want to keep
- **Exercises in this section:** Exercise 201 — Factorial Calculator and Exercise 202 — Palindrome Checker (see below)

### 4. Section 2 of 3 · Ask
- **Copilot Chat Mode: Ask** — use Ask when you want an explanation, guidance, or ideas *before* changing code
  - **What it does:** answers questions about code; explains errors, APIs, and patterns
  - **Best for:** "What does this function do?", "Which approach should I take?"
  - **Not for:** directly editing files in place or applying changes without review
  - **Good Ask prompts:** explain this regex; why is this test failing; compare a loop vs LINQ for this case
  - Mental model: **Ask first, edit second** — Ask is your AI teammate for discussion and understanding, not the mode for applying changes
- **Exercise in this section:** Exercise 203 — Mystery Processor (see below)

### 5. Section 3 of 3 · Slash Commands
- **Copilot Chat Slash Commands** — a slash command gives Chat a clear job immediately, so Copilot knows whether to explain, fix, test, or document
  - Start a prompt with a command, e.g. `/explain` or `/fix`
  - `/explain` — understand unfamiliar code; best when you select the exact block
  - `/fix` — diagnose broken code or errors; include the failure for better fixes
  - Good pattern: `/explain Why is this loop updating the matrix row by row?` and `/fix This Dijkstra test expects 3 but returns 4 for node D.`
  - Best practice: the command sets the task, but the surrounding **selection + context** is what makes the answer useful
- **Copilot Chat Slash Command: `/tests`** — select an implementation, run `/tests`, and let Copilot draft a suite you can review and extend
  - Generates a test file or block covering the happy path and common edges
  - Works best when the selected code has clear inputs and the intent/outputs are easy to infer
  - Still your job: review assertions and edge cases, and add missing business-specific checks
  - Pattern: `/tests Generate xUnit tests for this Cipher class.`; good follow-up: *"What edge cases are still missing?"*
- **Inline Chat and `/doc`**
  - **Inline chat:** opens at the cursor with `Ctrl+I` — ideal for surgical edits in place
  - **`/doc`:** generates docs for a selected method (also works for classes, modules, and files)
  - **Review matters:** check summaries, params, and returns; add remarks when the algorithm is subtle
  - Pattern: `/doc Generate XML docs for this Kmp class and explain the time complexity.`
  - Best practice: let Copilot draft the docs, then make sure the explanation would still help the next human reader
- **Exercises in this section:** Exercise 204 — Shortest Path, Exercise 205 — Caesar Cipher, Exercise 206 — String Search (see below)

### 6. Interactive Quizzes
- **Quiz 4:** Which token type is typically the most expensive in Copilot AI Credits billing? — *Output tokens (what the model generates back).*
- **Quiz 5:** Which shortcut accepts just the next word of a suggestion? — *`Ctrl+Right` (accepts the suggestion one word at a time).*
- **Quiz 6:** Which slash command best documents an undocumented KMP method quickly? — *`/doc` (generates documentation for the selected method).*

### 7. Lab 201 — Ultimate Snake
- **Goal:** build a complete, polished console Snake game with start, pause, score tracking, wrap-around movement, and self-collision
- **Copilot constraint:** participants may use **Ask mode, inline chat, and ghost text only** — no Agent Mode or CLI
- **What to ship:** a working game inside the single-file starter app, not a big refactor or multi-file architecture exercise
- **What good looks like:** Space starts/pauses; arrow keys feel responsive and cannot reverse directly into the body; the snake grows, wraps at edges, and ends only on self-collision
- **How to use Copilot well:** prompt one behaviour at a time instead of asking for the whole game at once; review every suggestion before accepting; use inline chat to refine methods, then ghost text to finish details
- **Facilitator expectation:** participants should finish with a *playable* result, not perfect polish; the reference solution is for comparison after the attempt — the learning outcome is prompt steering and review discipline

---

## 🧪 Chapter 2 Exercises

- [Exercise 201 — Factorial Calculator](../../../exercises/chapter-02/exercise-201/README.md) — practise ghost-text completions by implementing iterative and recursive factorial methods; accept with `Tab`, `Alt+[` / `Alt+]`, or word by word, then compare solutions
- [Exercise 202 — Palindrome Checker](../../../exercises/chapter-02/exercise-202/README.md) — steer ghost text with short line-by-line intent comments, then review casing, punctuation, and edge cases critically
- [Exercise 203 — Mystery Processor](../../../exercises/chapter-02/exercise-203/README.md) — use Copilot Chat and `/explain` to understand obfuscated code and form a hypothesis *before* looking at the tests
- [Exercise 204 — Shortest Path](../../../exercises/chapter-02/exercise-204/README.md) — fix a subtle Dijkstra bug by combining the failing test, algorithm context, and `/fix`, applying the smallest correct change
- [Exercise 205 — Caesar Cipher](../../../exercises/chapter-02/exercise-205/README.md) — use `/tests` to generate a starter xUnit suite, then add the missing edge cases (wrap-around, negative shifts, non-letters, round trips)
- [Exercise 206 — String Search](../../../exercises/chapter-02/exercise-206/README.md) — use `/doc` to document an existing KMP implementation so the next reader can understand the algorithm quickly
- [Lab 201 — Ultimate Snake](../../../labs/chapter-02/lab-201/README.md) — build a complete Snake game using ghost text, Ask mode, and inline chat only

---

## 🔗 Resources & References
- [Getting started with GitHub Copilot](https://docs.github.com/en/copilot/getting-started-with-github-copilot)
- [GitHub Copilot keyboard shortcuts](https://docs.github.com/en/copilot/configuring-github-copilot/configuring-github-copilot-in-your-environment)
- [Copilot Chat slash commands reference](https://docs.github.com/en/copilot/using-github-copilot/copilot-chat/asking-github-copilot-questions-in-your-ide)
- [About billing for GitHub Copilot](https://docs.github.com/en/billing/managing-billing-for-github-copilot)
- [Changing the AI model for Copilot Chat](https://docs.github.com/en/copilot/using-github-copilot/ai-models/changing-the-ai-model-for-copilot-chat)

---

## 🗒️ Facilitator Notes
- Lead with the billing change — it frames *why* model choice and task size matter, and the token quiz lands better once participants understand input vs cached vs output
- Reinforce the three-section structure (Completions → Ask → Slash Commands); each section is paired with hands-on exercises, so keep the lecture short and get participants into the IDE quickly
- For Lab 201, hold the line on the constraint: Ask mode, inline chat, and ghost text only — no Agent Mode or CLI; the point is steering and review, not finishing fast
- Emphasise throughout: accepting every suggestion uncritically is *not* the goal — review every suggestion like any other proposed code

---

[🏠 Workshop Home](../../../README.md) | [← Chapter 1 — Welcome to the AI Revolution!](../chapter-01/README.md) | [Chapter 3 — Power with Purpose: Using AI Responsibly →](../chapter-03/README.md)