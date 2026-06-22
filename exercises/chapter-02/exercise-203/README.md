# Exercise 203 — Mystery Processor

> **Chapter:** Chapter 2, Exercise 3  
> **Skill focus:** Using Copilot to understand unfamiliar and obfuscated code; `/explain`; Agent Mode exploration  
> **Difficulty:** ⭐⭐⭐

← Back to [Exercise Index](../../README.md)

---

## 🎯 Overview

In real projects you will regularly encounter code that is difficult to understand — legacy systems, minified output, auto-generated files, or deliberately obfuscated libraries. This exercise puts you in exactly that situation.

The `Processor` project contains a class named `C0` with methods that have been **deliberately obfuscated**: all meaningful names have been replaced with single-letter or generated identifiers, all comments have been removed, and the formatting has been collapsed. Your job is to use Copilot to figure out what it does — without looking at the tests first.

---

## 📚 Background: Code Obfuscation

Obfuscation transforms source code to make it harder for humans to understand while keeping it functionally identical. Common techniques include:

| Technique | Example |
|-----------|---------|
| Identifier renaming | `CalculateDistance` → `C0`, `sourceString` → `v0` |
| Comment stripping | All `//` and `/* */` removed |
| Whitespace collapsing | Multi-line logic → single dense block |
| Magic numbers | `26` instead of a named constant `AlphabetSize` |
| Dead code insertion | Unreachable branches added to confuse readers |

Obfuscation is used in:
- **Commercial software protection** — slowing down reverse engineering
- **Malware** — hiding intent from scanners
- **Code golf** — writing the shortest possible solution
- **CTF challenges** — intentional puzzles

A developer who can read obfuscated code fluently, and who knows how to use AI tools to help, has a significant advantage in security, legacy migration, and incident response work.

---

## 📚 Background: The Hidden Algorithm

The `C0` class implements a well-known classical algorithm from computer science. Without spoiling the reveal, here are some hints:

- It operates on **two strings** and produces a **non-negative integer**.
- The integer represents a **measure of difference or distance** between the two strings.
- It is used widely in spell checkers, DNA sequence alignment, diff tools, and fuzzy search.
- The implementation uses **dynamic programming**: it builds a 2D matrix where each cell depends on values already computed in adjacent cells.
- The result for two identical strings is always **0**.
- The result increases as the strings become more different.

Once you have identified the algorithm, you will find it described thoroughly in any algorithms textbook or on Wikipedia.

---

## 🗂️ Project Structure

```
exercise-203/
├── Processor/
│   ├── C0.cs                  ← Obfuscated implementation — the puzzle
│   └── Processor.csproj       ← Library project (.NET 10)
└── Processor.Tests/
    ├── C0Tests.cs             ← xUnit tests — do NOT read until you have a hypothesis
    └── Processor.Tests.csproj
```

### `C0.cs`

The obfuscated class. Expect names like `v0`, `v1`, `i`, `j`, a 2D array, and nested loops. No comments. Dense formatting.

### `C0Tests.cs`

The tests reveal the expected output for known inputs. They are your ground truth once you are ready to verify your hypothesis.

---

## ✅ Your Task

### Phase 1 — Explore Without Looking at the Tests

1. Open `Processor/C0.cs`.
2. Select the entire class and use **Copilot Chat → `/explain`** to ask for an explanation.
3. Ask follow-up questions in Chat:
   - *"What algorithm does this implement?"*
   - *"Can you rename the variables to meaningful names?"*
   - *"What are the inputs and outputs?"*
   - *"Can you add inline comments explaining each step?"*
4. Form a hypothesis about what the method computes.

### Phase 2 — Verify

5. Run the tests to see if Copilot's interpretation was correct:

```bash
cd 203/Processor.Tests
dotnet test
```

6. If the tests pass, great — Copilot helped you understand correctly. If any fail, investigate why.

### Phase 3 — Rewrite (Stretch)

7. Using what you now know, **rewrite the `C0` class from scratch** with proper names, comments, and formatting — without looking at `C0.cs`. Use Copilot to help, but make sure you understand every line.
8. Replace the class content and re-run the tests.

---

## 🤖 Copilot Skills to Practise

| Task | How |
|------|-----|
| Explain what code does | Select code → Chat → `/explain` |
| Rename identifiers | Ask Chat: *"Rename all variables to meaningful names in this method"* |
| Add comments | Ask Chat: *"Add a comment above each line explaining what it does"* |
| Identify the algorithm | Ask Chat: *"What well-known algorithm does this implement?"* |
| Reconstruct from scratch | Open a new file and prompt: *"Implement the Levenshtein distance algorithm in C# with clear variable names"* |

---

## 💡 What to Expect from Copilot

Copilot is very good at recognising **canonical algorithms** — even in obfuscated form — because it has seen thousands of implementations during training. For a well-known dynamic programming algorithm, it will often name it correctly and explain the DP recurrence relation.

It is less reliable on truly novel obfuscation or proprietary business logic, where there is no reference implementation to pattern-match against. In those cases, `/explain` gives you a starting point, but you must reason through the logic yourself.

---

## 🏁 Stretch Goals

1. Compare Copilot's renamed version with the Wikipedia pseudocode for the algorithm. Are there any differences?
2. Ask: *"What is the time and space complexity of this implementation? How could it be optimised?"*
3. Read the [Exercise 304 README](../../chapter-03/exercise-304/README.md) — the capstone MCP tool applies this very same obfuscation technique at production scale.

---

← Back to [Exercise Index](../../README.md) | Previous: [Exercise 202](../exercise-202/README.md) | Next: [Exercise 204](../exercise-204/README.md)
