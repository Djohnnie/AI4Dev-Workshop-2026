# Exercise 206 — String Search

> **Chapter:** Chapter 2, Exercise 6  
> **Skill focus:** Documentation generation with `/doc`; using Copilot as a code-review partner; evaluating complex algorithmic code  
> **Difficulty:** ⭐⭐⭐

← Back to [Exercise Index](../../README.md)

---

## 🎯 Overview

Not all code is easy to read. Advanced algorithms — efficient but non-obvious — are exactly the kind of code that benefits most from good documentation and a careful review pass. This exercise presents a complete implementation of the **Knuth-Morris-Pratt (KMP)** string search algorithm and asks you to use Copilot to document it thoroughly, review it for correctness, and deepen your own understanding of how it works.

This exercise turns the Chapter 2 `/doc` slide into practice: start by understanding the algorithm, then use Copilot to draft documentation that would genuinely help the next developer.

---

## 📚 Background: The KMP Algorithm

### The Problem

Given a text string `T` of length `n` and a pattern string `P` of length `m`, find all starting positions in `T` where `P` occurs.

### Naive Solution — O(n × m)

The naive approach compares the pattern against every position in the text. When a mismatch occurs, it backs up and retries from scratch:

```
Text:    A B C A B C A B D
Pattern: A B C A B D

Position 0: A B C A B C ← mismatch at index 5
Position 1: B ← mismatch at index 0
...
Position 3: A B C A B D ← match!
```

For short patterns this is fine, but for long repeated patterns (common in DNA sequences or compressed data), it is very slow.

### KMP's Key Insight — O(n + m)

KMP avoids redundant comparisons by precomputing a **failure function** (also called the **partial match table** or **prefix table**) for the pattern. The failure function tells us: *"if a mismatch occurs at position `i` in the pattern, how far back can we shift the pattern without missing a valid match?"*

### The Failure Function

For a pattern `P`, `failure[i]` is the length of the longest proper prefix of `P[0..i]` that is also a suffix of `P[0..i]`.

```
Pattern:   A  B  C  A  B  D
Index:     0  1  2  3  4  5
Failure:   0  0  0  1  2  0
```

Reading this: after matching `A B C A B` and then finding a mismatch at position 5, we know that `A B` (length 2) at the start of the pattern matches the last two characters we already passed, so we can continue matching from pattern position 2 rather than restarting from 0.

### The Search

The search itself is a single left-to-right pass through the text. When a mismatch occurs at pattern position `j`, set `j = failure[j - 1]` (or `j = 0` if already at the start). This guarantees each character in the text is examined at most twice total.

### Why This Matters

KMP is a benchmark algorithm for understanding:

- **Amortised analysis** — why the total work is O(n + m) despite the apparent loop-within-loop structure
- **Preprocessing** — investing computation upfront to make the main search cheaper
- **The prefix/suffix duality** — a core concept in string algorithms (also used in Z-function, Boyer-Moore, and Aho-Corasick)

---

## 🗂️ Project Structure

```
206/
├── StringSearch/
│   ├── Kmp.cs                 ← Complete KMP implementation — to be documented and reviewed
│   └── StringSearch.csproj   ← Library project (.NET 10)
└── StringSearch.Tests/
    ├── KmpTests.cs            ← Comprehensive xUnit tests
    └── StringSearch.Tests.csproj
```

### `Kmp.cs`

Contains the `Kmp` class with two methods:

```csharp
// Builds the failure (prefix) table for a given pattern
private static int[] BuildFailureTable(string pattern)

// Returns all starting indices in text where pattern occurs
public static IEnumerable<int> Search(string text, string pattern)
```

The code is correct and complete but has minimal documentation.

### `KmpTests.cs`

Tests cover:

- Single occurrence
- Multiple non-overlapping occurrences
- Overlapping occurrences (pattern `"ABA"` in text `"ABABABA"`)
- Pattern not found → empty result
- Pattern equals text
- Empty pattern or empty text edge cases
- Case-sensitive behaviour

---

## ✅ Your Task

### Phase 1 — Understand

1. Open `StringSearch/Kmp.cs`.
2. Select the `BuildFailureTable` method and use **Copilot Chat → `/explain`**:
   - *"Explain what this failure table is and why it is built this way."*
   - *"Walk me through this line by line."*
3. Do the same for the `Search` method.
4. Convince yourself you understand both methods well enough to explain them to a colleague.

### Phase 2 — Document

5. Select the entire `Kmp` class and use **Copilot Chat → `/doc`** to generate XML documentation.
6. Review what was generated:
   - Is the `<summary>` accurate?
   - Are the `<param>` descriptions correct and useful?
   - Does the `<returns>` element clearly describe what is returned?
   - Are there `<remarks>` explaining the algorithm's time complexity?
7. Refine any documentation that is vague or incorrect.
8. Ask Copilot to add **inline comments** at the key steps inside `BuildFailureTable` and `Search` — the parts that are hardest to understand at a glance.

### Phase 3 — Review

9. Use Copilot as a **code-review partner**. Select the `Search` method and ask:
   - *"Review this implementation for correctness. Are there any edge cases it might handle incorrectly?"*
   - *"Is there a more idiomatic way to write this in C# 13?"*
   - *"What would happen if `text` or `pattern` is null?"*
10. Run the tests to verify everything still passes:

```bash
cd 206/StringSearch.Tests
dotnet test
```

---

## 🤖 Copilot Skills to Practise

| Task | How |
|------|-----|
| Understand dense algorithmic code | Select method → Chat → `/explain` |
| Generate XML documentation | Select class → Chat → `/doc` |
| Add inline comments | Ask: *"Add a comment above each significant step"* |
| Code review | Ask: *"Review this for correctness, readability, and edge cases"* |
| Complexity analysis | Ask: *"What is the time and space complexity of this implementation?"* |
| Idiomatic rewrite | Ask: *"Rewrite using LINQ / Span<char> / ReadOnlySpan for better performance"* |

---

## 💡 Best Practices Reinforced

This exercise reinforces the Chapter 2 documentation and review workflow:

| Practice | How the exercise addresses it |
|----------|------------------------------|
| **Code is read more than written** | Good documentation is not optional for algorithm code |
| **AI as reviewer, not rubber stamp** | Copilot's review suggestions must be evaluated critically |
| **You own the code** | You should understand every line before merging generated docs |
| **Complexity awareness** | Knowing *why* KMP is O(n+m) helps you choose algorithms wisely |
| **Null-safety** | Production code must handle unexpected inputs gracefully |

---

## 🏁 Stretch Goals

1. Ask Copilot to implement the **Boyer-Moore** string search algorithm, then compare it to KMP in terms of readability, implementation complexity, and performance characteristics.
2. Ask: *"How would you extend this to search for multiple patterns simultaneously?"* (Hint: Aho-Corasick automaton.)
3. Use the `#codebase` context variable in Copilot Chat to ask if KMP is used anywhere else in the exercises project.

---

← Back to [Exercise Index](../../README.md) | Previous: [Exercise 205](../205/README.md) | Next: [Exercise 301](../../chapter-03/301/README.md)
