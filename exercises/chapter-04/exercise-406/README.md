# Exercise 406 — Create a Repository Instruction File

> **Chapter:** Chapter 4, Exercise 6  
> **Skill focus:** Enriching GitHub Copilot prompts with repository-specific instructions, preferences, and conventions  
> **Difficulty:** ⭐

← Back to [Exercise Index](../../README.md)

---

## 🎯 Overview

This exercise is about improving **prompt context quality** before you ask Copilot to write a single line of code.

You will create a brand-new repository and add a **`.github/copilot-instructions.md`** file that teaches Copilot how you like to work:

- your coding style and conventions
- your preferred tech stack
- your testing requirements
- your GenAI output preferences

There is **no starter project** for this exercise. The point is to practise writing the instruction file itself and understanding how better context leads to better prompts and better results.

---

## ✅ Your Task

### Phase 1 — Create the repository

1. Create a new repository on GitHub.
2. Add a `.github` folder.
3. Inside it, create a file named `copilot-instructions.md`.

### Phase 2 — Write the first version

4. Fill the file with guidance in these four categories:

- **Style and conventions**
  - naming patterns
  - import/export preferences
  - error-handling style
  - comment policy

- **Tech stack**
  - framework and language versions
  - preferred libraries
  - libraries to avoid
  - architecture preferences

- **Testing requirements**
  - test framework
  - file placement
  - minimum scenarios to cover
  - mocking/assertion style

- **GenAI output preferences**
  - concise vs. detailed
  - when to explain reasoning
  - whether to propose alternatives
  - whether to avoid placeholders or TODOs

### Phase 3 — Use it to enrich prompts

5. Ask Copilot for a small piece of code or a project scaffold.
6. Compare the result with and without the instruction file.
7. Revise the file so the output gets closer to your expectations.

---

## 🧭 Guardrails

- Keep the repository small and disposable — this is an instruction-writing exercise, not a product build.
- Be specific instead of generic. “Use TypeScript” is weaker than “Use TypeScript strict mode and never use `any`.”
- Prefer concrete examples over abstract intentions.
- Avoid trying to cover every possible rule; focus on the instructions that would change Copilot's behaviour the most.
- Treat the file as living documentation — something a team could review and improve over time.

---

## 📝 Suggested Starter Structure

```text
.github/
└── copilot-instructions.md
```

Suggested headings:

```markdown
# Copilot Instructions

## Style and conventions
## Tech stack
## Testing requirements
## GenAI output preferences
```

---

## 🤖 Copilot Skills to Practise

| Task | How |
|------|-----|
| Shape prompt context | Add reusable repo-level guidance instead of repeating it in every prompt |
| Improve output quality | Compare Copilot responses before and after adding instructions |
| Encode team preferences | Capture conventions in a shared, reviewable file |
| Refine instructions | Tighten vague rules into concrete ones |

---

## 💡 Why This Exercise

Good prompts are not only about the sentence you type in chat. They are also about the context Copilot carries into every request.

A strong repository instruction file reduces repetition, improves consistency, and helps Copilot generate output that already matches your team’s expectations. Instead of restating “use our stack, write tests this way, and be concise” every time, you teach those preferences once and let them compound.

---

## 🏁 Stretch Goals

1. **Add examples.** Include one “good” and one “bad” example of a coding convention.
2. **Make it stricter.** Add rules for naming, null handling, or dependency choices.
3. **Tailor by stack.** Write one version for a frontend repo and one for a backend repo; compare the differences.
4. **Team review.** Ask a teammate what is missing from the instructions and refine the file together.

---

← Back to [Exercise Index](../../README.md) | Related: [Slide deck](../../../content/2-day/chapter-04/SLIDES.md) | Previous: [Exercise 405](../exercise-405/README.md)
