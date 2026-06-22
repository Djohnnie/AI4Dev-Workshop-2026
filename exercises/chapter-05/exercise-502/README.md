# Exercise 502 — Prompt Arena

> **Chapter:** Chapter 5, Exercise 2  
> **Skill focus:** Turning prompt engineering rules into a shareable web game with a custom scoring chatbot  
> **Difficulty:** ⭐⭐⭐

← Back to [Exercise Index](../../README.md)

---

## 🎯 Overview

This exercise builds **Prompt Arena**, a small **ASP.NET Core web app** you can deploy and share with workshop participants. Players paste a prompt into the app, and a custom LLM judge returns a score from **0% to 100%** based on how well the prompt follows **section 2 of chapter 5**:

- **Task**
- **Context**
- **Examples**
- **Constraints**
- whether the prompt is **one-shot** or **few-shot**
- whether it avoids prompt **anti-patterns**

The judge is itself a custom chatbot with a strong **system prompt** that forces it to score prompts consistently and explain what is missing.

---

## 🗂️ Project Structure

```
exercise-502/
├── PromptArena.csproj
├── Program.cs
├── ChallengeCatalog.cs
├── PromptEvaluation.cs
├── PromptScoringService.cs
├── PromptScoringPrompts.cs
├── PromptScoreRequest.cs
└── wwwroot/
    ├── app.js
    ├── index.html
    └── site.css
└── README.md
```

### What the app does

- Serves a browser UI that participants can open from any device
- Lets players paste prompts and get a **0–100** score
- Shows which of the **four ingredients** were detected
- Flags anti-patterns like vagueness or conflicting constraints
- Distinguishes between **one-shot** and **few-shot** prompting
- Includes several fun workshop challenge modes

---

## ✅ Your Task

### Phase 1 — Run the app locally

1. Configure your model settings:

```powershell
$env:OPENAI_ENDPOINT = "https://your-resource.openai.azure.com/"
$env:OPENAI_KEY = "your-api-key"
$env:OPENAI_MODEL = "gpt-4o"
```

2. Run the web app:

```bash
cd 502
dotnet build
dotnet run
```

3. Open the URL shown in the terminal.
4. Paste prompts into the game and see how the score changes as you improve them.

### Phase 2 — Tune the scoring chatbot

5. Open `PromptScoringPrompts.cs` and inspect the judge's **system prompt**.
6. Refine the instructions so the score better matches the workshop rubric.
7. Try prompts that deliberately test edge cases:
   - strong task, weak context
   - good examples, missing constraints
   - vague wording with anti-patterns
   - one-shot vs few-shot comparisons

### Phase 3 — Make it workshop-ready

8. Deploy the app so participants can use it in a browser.
9. Add one or two of the challenge formats below.
10. Compare how teams improve their score over multiple rounds.

---

## 🎮 Fun Exercise Modes

1. **Prompt Makeover Sprint**  
   Give every team the same terrible prompt. They get 5 minutes to improve it and must break **80%**.

2. **Few-Shot Smackdown**  
   Start with a decent one-shot prompt, then add one or two examples and see whether the score jumps.

3. **Anti-Pattern Bingo**  
   Hand out prompts containing vague asks, missing context, or conflicting constraints. Participants must identify the anti-pattern before fixing it.

4. **Constraint Cage Match**  
   Ask teams to write a prompt with a strong task and context but very specific constraints like “no new packages” or “keep the signature unchanged.”

5. **Leaderboard Relay**  
   One participant writes the first version, the next adds context, the next adds examples, and the last removes anti-patterns. Score the prompt after each handoff.

6. **Judge the Judge**  
   Participants try to trick the scoring chatbot with borderline prompts and discuss whether the rubric is fair.

---

## 🤖 Copilot Skills to Practise

| Task | How |
|------|-----|
| Build a web UI quickly | Ask Copilot to scaffold an ASP.NET Core app with static assets |
| Design a strong system prompt | Ask Copilot to turn workshop rules into a strict scoring rubric |
| Work with structured output | Ask Copilot to request and parse JSON from the model |
| Iterate on product behaviour | Tune the judge until the scores match facilitator expectations |

---

## 🏁 Stretch Goals

1. Add a public **leaderboard** backed by in-memory state or SQLite.
2. Save each attempt and show how the score improved after each rewrite.
3. Add a **team code name** and a timer for tournament mode.
4. Add a “suggest an improved version” button that asks the LLM judge for a rewritten prompt.
5. Add a deployment target such as Azure App Service or Container Apps.

---

## 🧠 The Scoring Rubric

The custom chatbot should score prompts with these ideas in mind:

- **Task**: Is the ask clear and verb-first?
- **Context**: Does the model know the environment, language, or constraints?
- **Examples**: Is there a one-shot or few-shot example, sample input/output, or shape hint?
- **Constraints**: Does the prompt say what must or must not happen?
- **Anti-pattern avoidance**: Is the prompt specific, non-conflicting, and focused?

The app's system prompt enforces the structure and returns a JSON scorecard to the UI.

---

## 🚀 Deployment Ideas

- **Fastest**: deploy as an Azure App Service web app
- **Portable**: containerize it and push to Azure Container Apps
- **Workshop-friendly**: run it once centrally and give participants the URL

As long as the deployment has `OPENAI_ENDPOINT`, `OPENAI_KEY`, and optionally `OPENAI_MODEL`, the app is ready to share.

---

← Back to [Exercise Index](../../README.md)
