# Exercise 408 — Bug Bash with Custom Agents

> **Chapter:** Chapter 4, Exercise 8  
> **Skill focus:** Comparing specialist custom agents on the same small repository  
> **Difficulty:** ⭐⭐

← Back to [Exercise Index](../../README.md)

---

## 🎯 Overview

This exercise is about making **custom agents feel different in practice**, not just in theory.

You are given a tiny intentionally messy repository plus **four ready-made custom agent files**. Your job is to copy the agent files into the right `.github/agents/` location, run the same "bug bash" through different agents, and compare what each specialist notices first.

The repo is small on purpose: it has a few security smells, weak test coverage, rough docs, and some code that wants a cleanup pass. The fun is seeing how the **same codebase** produces very different output depending on which agent profile you choose.

---

## 🗂️ What is included

Inside this exercise folder you will find:

```text
exercise-408/
├── README.md
├── agents-to-copy/
│   └── .github/
│       └── agents/
│           ├── docs-improver.agent.md
│           ├── refactor-reviewer.agent.md
│           ├── security-reviewer.agent.md
│           └── test-gap-spotter.agent.md
└── starter-repo/
    ├── README.md
    ├── docs/
    ├── src/
    ├── tests/
    └── package.json
```

The `agents-to-copy` folder is there so participants can **copy the files directly** into the correct GitHub Copilot path inside a disposable repo.

---

## ✅ Your Task

### Phase 1 — Prepare the sandbox

1. Open the `starter-repo` folder as a small disposable project.
2. Copy the contents of `agents-to-copy/.github/agents/` into:

   ```text
   starter-repo/.github/agents/
   ```

3. Confirm the four agent files are now in the repository path Copilot expects.

### Phase 2 — Run the bug bash with specialist agents

4. Use the **security-reviewer** agent and ask it to inspect the repo for the top security risks.
5. Use the **test-gap-spotter** agent and ask where coverage is weak or misleading.
6. Use the **docs-improver** agent and ask what documentation is stale, confusing, or missing.
7. Use the **refactor-reviewer** agent and ask where the code can be simplified without changing behaviour.

### Phase 3 — Compare the outputs

8. Compare what each agent focused on:
   - Which files did it care about?
   - Did it suggest edits, findings, or both?
   - Did the tool boundary change its behaviour?
   - Which recommendations felt most useful?

9. Keep the best output from at least **one** of the agents:
   - accept a doc improvement
   - keep a test addition
   - note a security finding to fix manually
   - apply a small safe refactor

### Phase 4 — Reflect

10. Decide which agent profile felt most valuable for:
    - fast review
    - safe edits
    - teaching a repeatable team workflow
    - keeping Copilot focused instead of broad and generic

---

## 🗣️ Prompt Strategy for Participants

Use **both** a shared prompt and agent-specific follow-ups.

### Round 1 — Same prompt, different agents

Start by giving all four agents the **same core prompt**:

> **Do a focused bug bash on this repository. Find the most important issues in your area of expertise, explain why they matter, and suggest the best next action.**

This is the most important comparison in the exercise. When the prompt stays the same, participants can clearly see how much the **agent file itself** changes the outcome.

### Round 2 — Tailored follow-up prompt for each agent

Then give each specialist a prompt that matches its intended role:

- **Security reviewer:** *"Prioritize the top 3 security or trust-boundary issues. Do not edit files."*
- **Test gap spotter:** *"Identify the highest-value missing tests and add them only under `tests/`."*
- **Docs improver:** *"Update the README and docs so they match the code as it exists today."*
- **Refactor reviewer:** *"Apply 1–2 small safe refactors that improve clarity without changing behavior."*

This second round shows how to get the **best practical value** from each specialist once participants have seen the baseline differences.

### Why this order works

1. **Same prompt first** shows specialization clearly.
2. **Tailored prompts second** show how to collaborate well with each agent.

That combination makes the lesson stronger than using only one shared prompt or only custom prompts.

---

## 🤖 The four agents

| Agent | What it is meant to do |
|------|-----|
| `security-reviewer` | Read-only reviewer that looks for unsafe patterns, injection risks, auth/role mistakes, and hardcoded secrets |
| `test-gap-spotter` | Test-focused specialist that identifies missing scenarios and can improve files under `tests/` |
| `docs-improver` | Documentation-focused specialist that updates README and docs to match reality |
| `refactor-reviewer` | Cleanup specialist for duplication, naming, and simplification without changing intended behaviour |

Two important teaching points are built into the files:

1. **The prompts are different.** Each agent has a distinct lens on the same repo.
2. **The tool access is different.** Some agents are intentionally read-only, while others are allowed to edit only the surfaces they are meant to improve.

---

## 🧭 Guardrails

- Keep this repo disposable. It exists to compare agent behaviour, not to become a polished product.
- Do **not** paste real credentials into the sample files. Any token-like values in the starter repo are fake on purpose.
- Prefer asking the same or very similar prompt to multiple agents so the differences are easier to see.
- Do not try to "win" by using only the strongest all-purpose agent. The point is specialization.
- If an agent suggests a risky or broad change, narrow the scope and rerun it.

---

## 🧪 Suggested prompts

Try prompts like:

- **Security reviewer:** *"Inspect this repo and report the three most important security or trust issues. Do not edit files."*
- **Test gap spotter:** *"Review the current tests and add the most valuable missing coverage without touching production files."*
- **Docs improver:** *"Update the README and docs so they match what the code actually does today."*
- **Refactor reviewer:** *"Suggest and apply one or two small refactors that improve clarity without changing behaviour."*

Then repeat the same prompt with a different agent and compare the result.

---

## 💡 Why This Exercise

Custom agents are most useful when a team wants **repeatable specialist behaviour** instead of a single general-purpose assistant doing everything the same way.

This exercise makes that visible quickly. A security specialist should not sound like a docs specialist. A test specialist should care about very different evidence than a refactoring specialist. By running all four on one tiny repo, participants can feel the difference between:

- changing the **prompt**
- changing the **agent**
- changing the **tool boundary**

That is the real lesson.

---

## 🏁 Stretch Goals

1. **Create a fifth agent.** Add your own release-notes, accessibility, or performance specialist.
2. **Tighten a prompt.** Rewrite one agent file so it becomes more reliable or more constrained.
3. **Swap tool access.** Give a read-only agent edit access, or remove edit access from one that currently has it; compare the result.
4. **Run the same repo in CLI.** Try one of the agents in GitHub Copilot CLI after using it in the IDE.

---

← Back to [Exercise Index](../../README.md) | Related: [Slide deck](../../../content/2-day/chapter-04/SLIDES.md) | Previous: [Exercise 407](../exercise-407/README.md)
