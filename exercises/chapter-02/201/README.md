# Exercise 201 — Factorial Calculator

> **Chapter:** Chapter 2, Exercise 1  
> **Skill focus:** Inline completions; ghost-text autocomplete; accepting and cycling suggestions  
> **Difficulty:** ⭐

← Back to [Exercise Index](../../README.md)

---

## 🎯 Overview

This is your first hands-on exercise with **GitHub Copilot's inline completion** feature. You will implement a simple factorial calculator — twice — using two different algorithmic approaches. The goal is not the algorithm itself (it is intentionally simple); the goal is to get comfortable with the ghost-text experience: how Copilot reads your code context, when it offers suggestions, and how to guide it toward the solution you want.

---

## 📚 Background: Factorial

The **factorial** of a non-negative integer *n*, written *n!*, is the product of all positive integers up to and including *n*:

```
0! = 1          (by definition)
1! = 1
5! = 5 × 4 × 3 × 2 × 1 = 120
10! = 3,628,800
```

Factorial grows extremely fast — 20! already exceeds the range of a 64-bit integer — so implementations must account for overflow or use `BigInteger` for large inputs.

### Iterative Approach

A loop accumulates the product from 1 up to *n*:

```csharp
long result = 1;
for (int i = 2; i <= n; i++)
    result *= i;
return result;
```

### Recursive Approach

The function calls itself with a decremented argument, relying on the base case `n == 0`:

```csharp
if (n == 0) return 1;
return n * Factorial(n - 1);
```

Both approaches are correct; the recursive version is more elegant but can stack-overflow for very large *n* (though factorial overflows `long` long before that point).

---

## 🗂️ Project Structure

```
201/
├── Factorial/
│   ├── Calculator.cs          ← Implementation — your Copilot playground
│   └── Factorial.csproj       ← Library project (.NET 10)
└── Factorial.Tests/
    ├── CalculatorTests.cs     ← xUnit tests — run these to verify your work
    └── Factorial.Tests.csproj
```

### `Calculator.cs`

Contains the `Calculator` class with two methods for you to implement:

- `CalculateIterative(int n)` — compute *n!* using a loop
- `CalculateRecursive(int n)` — compute *n!* using recursion

Both return `long`. The method signatures and XML doc comments are already in place; the bodies are empty.

### `CalculatorTests.cs`

Covers edge cases and typical values:

- `n = 0` → `1`
- `n = 1` → `1`
- `n = 5` → `120`
- `n = 10` → `3,628,800`
- Negative input → `ArgumentOutOfRangeException`

---

## ✅ Your Task

1. Open `Factorial/Calculator.cs`.
2. Place your cursor inside the body of `CalculateIterative`.
3. **Do not type the implementation yourself.** Instead:
   - Write a comment describing what you want (e.g. `// iteratively compute n!`).
   - Wait for Copilot's ghost-text suggestion to appear.
   - Accept it with **Tab**, or cycle through alternatives with **Alt+]** / **Alt+[**.
4. Repeat for `CalculateRecursive`.
5. Run the tests to confirm everything passes:

```bash
cd 201/Factorial.Tests
dotnet test
```

---

## 🤖 Copilot Skills to Practise

| Interaction | Keys |
|-------------|------|
| Accept full suggestion | `Tab` |
| Dismiss suggestion | `Esc` |
| Accept word by word | `Ctrl+→` |
| Cycle to next suggestion | `Alt+]` |
| Cycle to previous suggestion | `Alt+[` |
| Open Completions Panel (up to 10 options) | `Ctrl+Enter` |

### Tips for Getting Better Suggestions

- **Method name matters:** Copilot reads the method name. `CalculateRecursive` already hints at the approach.
- **XML doc comments help:** The `<summary>` tag describes the intent — Copilot uses this as context.
- **A single comment line** immediately above the cursor often triggers a much more targeted suggestion than an empty body.
- **Try the Completions Panel** (`Ctrl+Enter`) to see all alternatives Copilot has at once — useful when the default suggestion isn't quite right.

---

## 🏁 Stretch Goals

Once the tests pass, try these extensions with Copilot's help:

1. Add a `CalculateBigInteger(int n)` method that uses `System.Numerics.BigInteger` to support arbitrarily large inputs.
2. Add tests for `n = 20` (the largest value that fits in a `long`) and `n = 21` (overflow territory for `long`).
3. Ask Copilot Chat: *"What are the performance trade-offs between the iterative and recursive factorial implementations in C#?"*

---

← Back to [Exercise Index](../../README.md) | Previous: [Exercise 106](../../chapter-01/106/README.md) | Next: [Exercise 202](../202/README.md)
