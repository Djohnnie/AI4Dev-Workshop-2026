# Exercise 602 — Expression Evaluator Test Lab

> **Chapter:** Chapter 6, Exercise 2  
> **Skill focus:** Comparing four Copilot-assisted testing approaches on a small but meaningful codebase  
> **Difficulty:** ⭐⭐⭐

← Back to [Exercise Index](../../README.md)

---

## 🎯 Overview

This exercise uses a compact but test-friendly problem: an **infix/postfix expression evaluator**.

The sample app takes arithmetic expressions such as `1+2*3`, converts them to postfix notation, and evaluates the result. That makes it a good fit for testing because it has:

- clear happy paths
- operator-precedence rules
- parentheses handling
- edge cases and invalid input paths

Instead of using only one testing style, participants should experiment with **four approaches** and compare how Copilot helps in each one.

---

## ✅ Your Task

Use Copilot to explore all four approaches against the same evaluator scenario:

1. **Writing tests on existing code**  
   Start from a working implementation and write characterization tests that describe what it already does.

2. **TDD**  
   Write a failing test first for a small behaviour, then ask Copilot for the smallest implementation change that makes it pass.

3. **BDD with Reqnroll**  
   Express behaviour as readable Reqnroll scenarios in a `.feature` file and check whether the scenarios still tell a useful story to non-implementers.

4. **Getting code coverage**  
   Run coverage, find weak branches and error paths, then ask Copilot to close the meaningful gaps instead of generating random extra tests.

---

## 🗂️ Suggested Workflow

### Phase 1 — Read the problem

Start with this system idea:

> Build and test a .NET 10 console application that evaluates arithmetic expressions written in infix notation. The app should produce the postfix form as well as the numeric result.

### Phase 2 — Try the four testing approaches

Use Copilot to:

- write tests around existing behaviour
- add or refine tests in a TDD loop
- restate important behaviours as Reqnroll scenarios
- inspect coverage and target the risky gaps

### Phase 3 — Compare what each approach gave you

Discuss:

- Which prompts helped most with characterization tests?
- When did TDD produce better focus than writing tests after the fact?
- Did Reqnroll improve the language of the tests or only rename them?
- Which coverage gaps were actually important?

### Phase 4 — Compare with the sample solution

A possible finished solution lives in:

`..\exercise-602-solution\`

It includes:

- a runnable console app
- tests grouped by the four approaches
- commands for running build, tests, and coverage

---

## 🤖 Prompt Starters

```text
Act as a test engineer. Looking at this expression evaluator, write characterization tests for the current behaviour before we change any code.
```

```text
Use TDD. Suggest one small failing test for the next behaviour we should support, then stop before writing implementation code.
```

```text
Write a Reqnroll `.feature` file for this evaluator so the scenarios read like behaviour descriptions instead of implementation notes.
```

```text
The coverage report shows weak branches in the evaluator. Suggest the highest-value tests to improve confidence, not just the percentage.
```

---

## 🏁 Stretch Goals

1. Add support for additional operators or stricter validation.
2. Compare a Reqnroll scenario with a plain unit test and decide which is clearer.
3. Extend the evaluator and use coverage to prove the new branch is exercised.

---

← Back to [Exercise Index](../../README.md) | Related: [Slide deck](../../../content/2-day/chapter-06/SLIDES.md)
