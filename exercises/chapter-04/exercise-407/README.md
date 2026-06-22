# Exercise 407 — Experiment with Prompt Files and Skill Files

> **Chapter:** Chapter 4, Exercise 7  
> **Skill focus:** Comparing manually invoked prompt files with automatically invoked skill files  
> **Difficulty:** ⭐⭐

← Back to [Exercise Index](../../README.md)

---

## 🎯 Overview

This exercise is about turning good one-off prompts into **reusable assets** for GitHub Copilot.

You will create both:

- a **prompt file** in `.github/prompts/`
- a **skill file** in `.github/skills/<name>/SKILL.md`

and then experiment with how each one changes Copilot's behaviour.

There is **no starter project** for this exercise. The goal is to play, compare, and learn when each mechanism is the better fit.

---

## ✅ Your Task

### Phase 1 — Create a tiny playground repo

1. Create a new repository or use a disposable sandbox repo.
2. Add a `.github/prompts/` folder.
3. Add a `.github/skills/` folder.

### Phase 2 — Add one prompt file

4. Create one reusable prompt file such as:
   - `write-test-cases.prompt.md`
   - `scaffold-readme.prompt.md`
   - `review-small-refactor.prompt.md`
5. Give it clear front matter:
   - `title`
   - `description`
   - `mode`
6. Write the prompt body as if you were capturing one of your best reusable prompts for the team.

### Phase 3 — Add one skill file

7. Create a skill folder such as `.github/skills/pr-review-helper/`.
8. Add a `SKILL.md` file with:
   - `name`
   - `description`
   - optional tool guidance if relevant
9. Describe an expert process Copilot should follow automatically when a matching task appears.

### Phase 4 — Run experiments

10. Invoke the prompt file directly and see how it behaves.
11. Ask Copilot to perform a task that should naturally trigger the skill.
12. Compare what changes when **you choose the file** versus **the agent chooses the process**.

---

## 🧪 Experiment Ideas

Try several of these:

1. **Same task, two mechanisms.** Encode the same task once as a prompt file and once as a skill; compare the outputs.
2. **Short description vs. precise description.** Rewrite the skill description and see whether Copilot starts using it more reliably.
3. **Tight vs. loose prompt.** Make one prompt file very prescriptive and another more open-ended; compare the output quality.
4. **Context-rich prompt file.** Add file references or explicit constraints and see whether the result becomes more consistent.
5. **Review workflow.** Create a skill for reviewing a small code change and compare it with a manually invoked review prompt file.
6. **Scaffolding workflow.** Use a prompt file for “generate the first draft” and a skill for “apply the team process when editing”.

---

## 🧭 Guardrails

- Keep the repo small — this is about experimenting with Copilot behaviour, not building a full app.
- Start with one prompt file and one skill file before adding more.
- Prefer tasks you can repeat quickly so the differences are easy to observe.
- Focus on **when each approach is useful**, not on trying to prove one is always better.
- Revise descriptions and instructions after each experiment; iteration is part of the exercise.

---

## 📝 Suggested Starter Structure

```text
.github/
├── prompts/
│   └── write-test-cases.prompt.md
└── skills/
    └── pr-review-helper/
        └── SKILL.md
```

---

## 🤖 Copilot Skills to Practise

| Task | How |
|------|-----|
| Capture a reusable prompt | Turn a strong one-off prompt into a `.prompt.md` file |
| Teach an expert process | Write a `SKILL.md` file that Copilot can choose automatically |
| Compare control models | Notice the difference between manual invocation and automatic skill selection |
| Improve descriptions | Refine file descriptions so Copilot loads the right behaviour more often |

---

## 💡 Why This Exercise

Prompt files and skill files solve similar problems from different angles.

Prompt files are best when **you want to choose a reusable prompt deliberately**. Skill files are best when **you want the agent to recognise a class of work and follow a repeatable process automatically**.

The point of the exercise is to feel that difference in practice, not just understand it in theory.

---

## 🏁 Stretch Goals

1. **Add a second prompt file.** Create one for scaffolding and one for review.
2. **Improve trigger quality.** Rewrite the skill description until Copilot selects it more consistently.
3. **Bundle extra guidance.** Add examples or a small helper script beside `SKILL.md`.
4. **Team-share the repo.** Ask someone else to try your files and report which instructions felt ambiguous.

---

← Back to [Exercise Index](../../README.md) | Related: [Slide deck](../../../content/2-day/chapter-04/SLIDES.md) | Previous: [Exercise 406](../exercise-406/README.md)
