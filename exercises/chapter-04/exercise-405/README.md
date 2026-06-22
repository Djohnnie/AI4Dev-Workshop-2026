# Exercise 405 — Explore Copilot on GitHub.com

> **Chapter:** Chapter 4, Exercise 5  
> **Skill focus:** Using GitHub Copilot's browser surfaces for repo understanding, issue triage, PR review, and cloud agent workflows  
> **Difficulty:** ⭐⭐

← Back to [Exercise Index](../../README.md)

---

## 🎯 Overview

This exercise is deliberately **browser-only**. Do not clone a repository, open VS Code, or build anything locally.

Instead, spend the session inside **GitHub.com** and use GitHub Copilot where it already has page context: repositories, issues, pull requests, code search, PR review, and the online coding agent.

The goal is to build confidence with the "ask right where the work already is" workflow.

---

## ✅ Your Task

### Phase 1 — Ask about repositories

1. Open a repository that **is not yours**.
2. Use Copilot Chat on GitHub.com to ask at least two questions, for example:
   - *"What does this repository do?"*
   - *"Where is authentication handled?"*
   - *"What are the main technologies and entry points?"*

3. Open **one of your own repositories**.
4. Ask Copilot at least two repo-specific questions, for example:
   - *"Summarise the architecture of this repository."*
   - *"What parts of this repo look most active right now?"*
   - *"Where is the code that sends emails / handles payments / builds the API response?"*

### Phase 2 — Work with issues and code discovery

5. Open an issue with a real discussion thread.
6. Ask Copilot to recap it:
   - *"Summarise the discussion so far."*
   - *"What decisions have already been made?"*
   - *"What is still unresolved?"*

7. In a repository you do not know well, ask Copilot to help you find code:
   - *"Find the code that generates PDFs."*
   - *"Where is the webhook payload validated?"*
   - *"Show me where background jobs are scheduled."*

### Phase 3 — Work with pull requests

8. Open or create a **somewhat large pull request** in one of your own repositories.
9. Ask GitHub Copilot to generate the PR summary / description for you.
10. Read the generated summary and decide:
    - did it explain *what* changed?
    - did it explain *why* it changed?
    - did it give enough testing or review context?

11. Request **Copilot review** on that PR and inspect the feedback:
    - Which comments are useful?
    - Which ones are noise?
    - Would you apply any suggestion directly?

### Phase 4 — Use the online coding agent

12. Pick an issue in one of your repositories that is small enough to automate.
13. Use the **GitHub Copilot coding agent** on GitHub.com to implement it.
14. Review the result the same way you would review a human-created PR.

---

## 🧭 Guardrails

- Stay in the browser for the whole exercise.
- Do **not** manually implement code locally.
- Prefer real repositories, issues, and pull requests over toy examples.
- Treat Copilot's answers as fast context gathering, not final truth — verify anything important.
- For the coding agent task, choose an issue small enough that you can realistically review the output in one sitting.

---

## 🤖 Copilot Skills to Practise

| Task | How |
|------|-----|
| Ask about unknown codebases | Use repo chat without cloning the repo |
| Summarise issue threads | Ask for recap, decisions, and open questions |
| Find code semantically | Describe behaviour instead of searching exact symbol names |
| Summarise PRs | Let Copilot draft the PR description from the diff |
| Review pull requests | Request Copilot review and evaluate the signal |
| Delegate implementation | Use the GitHub.com coding agent on an issue |

---

## 💡 Why This Exercise

Not every Copilot workflow starts in the IDE. Sometimes the fastest path is to ask a question directly on the repo page, summarise an issue where the discussion already lives, or let GitHub generate a first-pass PR review before a teammate reads it.

This exercise is about reducing context-switching. Instead of pulling work down locally just to orient yourself, you use Copilot where the repository, issue, and pull-request context already exists.

---

## 🏁 Stretch Goals

1. **Compare answers.** Ask the same architectural question in both an unknown repo and your own repo. Which answer feels more useful, and why?
2. **Refine the prompt.** Take one weak Copilot answer and improve it with a more specific follow-up.
3. **Triage shortcut.** Ask Copilot whether an issue looks like a bug, a feature request, or a documentation gap.
4. **Review quality.** Compare Copilot review comments with a human review on the same PR.

---

← Back to [Exercise Index](../../README.md) | Related: [Slide deck](../../../content/2-day/chapter-04/SLIDES.md) | Previous: [Exercise 404](../exercise-404/README.md)
