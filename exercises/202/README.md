# Exercise 202 — Palindrome Checker

> **Chapter:** Chapter 2, Exercise 2  
> **Skill focus:** Steering ghost-text with line-by-line comments; reviewing AI output critically before committing  
> **Difficulty:** ⭐

← Back to [Exercise Index](../README.md)

---

## 🎯 Overview

A palindrome is a word, phrase, or sequence that reads the same forwards and backwards. This exercise uses a simple, well-understood problem to practise two Chapter 2 habits at once: **guide Copilot with clear line-by-line comments** and **review every generated line before accepting it**.

You will sketch the implementation intent as short comments, let Copilot turn those comments into code, then scrutinise the output, reason about edge cases, and make sure the logic is actually correct — not just *apparently* correct.

---

## 📚 Background: Palindrome Checking

The naive definition — "reversed string equals original string" — misses the richness of real-world palindrome checking. A robust implementation must address:

### Case Sensitivity

`"Racecar"` and `"racecar"` are the same palindrome. A correct checker normalises case before comparing.

### Non-Alphanumeric Characters

The phrase `"A man, a plan, a canal: Panama"` is a classic palindrome, but only if you strip spaces, commas, and colons first. A correct checker filters the string down to letters and digits only.

### Algorithm

The cleanest approach:

1. Normalise to lowercase.
2. Remove all non-alphanumeric characters.
3. Compare the cleaned string with its reverse.

```csharp
var cleaned = new string(input
    .ToLowerInvariant()
    .Where(char.IsLetterOrDigit)
    .ToArray());
return cleaned == new string(cleaned.Reverse().ToArray());
```

An alternative — and slightly more efficient — approach uses two pointers walking inward from both ends, skipping non-alphanumeric characters without allocating a new string.

---

## 🗂️ Project Structure

```
202/
├── Palindrome/
│   ├── Checker.cs             ← Implementation — to be generated with Copilot
│   └── Palindrome.csproj      ← Library project (.NET 10)
└── Palindrome.Tests/
    ├── CheckerTests.cs        ← xUnit tests — your verification suite
    └── Palindrome.Tests.csproj
```

### `Checker.cs`

Contains the `Checker` class with a single method:

```csharp
public static bool IsPalindrome(string input)
```

The method signature and XML doc comment are present; the body is empty.

### `CheckerTests.cs`

Tests cover:

- Empty string → `true`
- Single character → `true`
- Simple palindromes: `"racecar"`, `"level"`, `"madam"`
- Mixed case: `"Racecar"`
- Phrase with punctuation: `"A man, a plan, a canal: Panama"`
- Non-palindromes: `"hello"`, `"world"`
- Numeric palindromes: `"12321"`

---

## ✅ Your Task

1. Open `Palindrome/Checker.cs` and place your cursor inside `IsPalindrome`.
2. Add short, step-by-step comments such as:

   - `// lower-case the input`
   - `// remove non-alphanumeric characters`
   - `// compare the cleaned string with its reverse`

3. Let Copilot autocomplete each step with ghost text instead of asking for the whole method in one go.

4. **Review the generated code carefully:**
   - Does it handle `null` input? What should happen?
   - Does it correctly handle Unicode letters (e.g. accented characters)?
   - Is the LINQ chain readable, or would a two-pointer approach be clearer?
   - Would a colleague understand it at a glance?

5. Run the tests:

```bash
cd 202/Palindrome.Tests
dotnet test
```

---

## 🔍 Responsible AI Review Checklist

This exercise reinforces a core Chapter 2 habit. Before committing any AI-generated code, ask:

| Question | What to look for |
|----------|-----------------|
| **Is it correct?** | Does it pass all tests, including edge cases? |
| **Is it readable?** | Could you explain every line to a colleague? |
| **Is it safe?** | Any unexpected behaviour with null, empty, or unusual input? |
| **Is it appropriate?** | Right level of complexity for the problem? |
| **Do you own it?** | Could you rewrite it from scratch if asked? |

If the answer to any of these is "I'm not sure", dig deeper before accepting.

---

## 🏁 Stretch Goals

1. Ask Copilot Chat: *"What is the time and space complexity of this palindrome implementation? Is there a more efficient approach?"* — then implement the two-pointer solution and compare.
2. Use `/doc` to generate XML documentation for the `IsPalindrome` method.
3. Ask Copilot to add a method `LongestPalindromicSubstring(string input)` and review its output with the same checklist.

---

← Back to [Exercise Index](../README.md) | Previous: [Exercise 201](../201/README.md) | Next: [Exercise 203](../203/README.md)
