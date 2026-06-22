# Exercise 205 — Caesar Cipher

> **Chapter:** Chapter 2, Exercise 5  
> **Skill focus:** Test generation with `/tests`; evaluating and extending AI-generated test suites  
> **Difficulty:** ⭐⭐

← Back to [Exercise Index](../../README.md)

---

## 🎯 Overview

Writing tests is one of the most time-consuming parts of software development — and one of the areas where GitHub Copilot adds the most immediate value. In this exercise, the **implementation is already complete and correct**. Your job is to use Copilot's `/tests` command to generate a comprehensive test suite for it, then critically evaluate the coverage and fill in any gaps.

The Caesar cipher is the implementation vehicle because it is simple enough to reason about manually, yet rich enough to have interesting edge cases that reveal whether the generated tests are superficial or thorough.

---

## 📚 Background: The Caesar Cipher

The **Caesar cipher** (also known as ROT-N or a shift cipher) is one of the oldest and simplest encryption techniques. Each letter in the plaintext is shifted a fixed number of positions along the alphabet:

```
shift = 3
A → D,  B → E,  C → F, ...,  X → A,  Y → B,  Z → C
```

### Encryption

For each character `c` in the input:

- If `c` is a letter, shift it by `shift` positions (wrapping around using modular arithmetic).
- If `c` is not a letter (digit, space, punctuation), leave it unchanged.

### Decryption

Decryption is simply encryption with a negative shift:

```
shift = -3   (or equivalently, shift = 26 - 3 = 23)
D → A,  E → B,  F → C, ...
```

### Modular Arithmetic

The wrap-around is handled with modulo. For uppercase letters (ASCII 65–90):

```
encrypted = (char)(((c - 'A' + shift) % 26 + 26) % 26 + 'A')
```

The `+ 26) % 26` trick handles negative shifts without producing a negative modulus result — important in C# where the `%` operator can return negative values for negative operands.

### Security Note

The Caesar cipher provides essentially **zero real security** — there are only 25 possible shifts, so brute-force is trivial. It is historically significant but should never be used for any real encryption purpose. For educational use only.

---

## 🗂️ Project Structure

```
205/
├── CaesarCipher/
│   ├── Cipher.cs              ← Complete, correct implementation
│   └── CaesarCipher.csproj   ← Library project (.NET 10)
└── CaesarCipher.Tests/
    ├── CipherTests.cs         ← EMPTY — you will populate this with Copilot
    └── CaesarCipher.Tests.csproj
```

### `Cipher.cs`

Contains the `Cipher` class with two public methods:

```csharp
public static string Encrypt(string plaintext, int shift)
public static string Decrypt(string ciphertext, int shift)
```

Both handle upper and lowercase letters independently, leave non-letter characters unchanged, and correctly wrap around the alphabet for any positive or negative shift value.

### `CipherTests.cs`

**This file is intentionally empty** (aside from the `using` statements). You will fill it in during this exercise.

---

## ✅ Your Task

### Phase 1 — Generate Tests with Copilot

1. Open `CaesarCipher/Cipher.cs` and **select the entire class** (or the `Encrypt` method first).
2. Open **Copilot Chat** and run:
   ```
   /tests
   ```
3. Copilot will generate a set of xUnit test methods. Review them in the Chat panel.
4. Click **Insert into file** (or copy-paste) into `CaesarCipher.Tests/CipherTests.cs`.
5. Repeat for `Decrypt` if Copilot didn't cover it.

### Phase 2 — Run and Evaluate

6. Run the generated tests:

```bash
cd 205/CaesarCipher.Tests
dotnet test
```

7. Evaluate the quality of what Copilot generated. Use this checklist:

### Test Quality Checklist

| Category | Did Copilot cover it? |
|----------|-----------------------|
| Basic encryption (lowercase) | ✅ / ❌ |
| Basic encryption (uppercase) | ✅ / ❌ |
| Non-letter characters preserved | ✅ / ❌ |
| Shift = 0 (identity) | ✅ / ❌ |
| Full round-trip: `Decrypt(Encrypt(text, n), n) == text` | ✅ / ❌ |
| Wrap-around at Z/z | ✅ / ❌ |
| Negative shift values | ✅ / ❌ |
| Shift > 26 (should work via modulo) | ✅ / ❌ |
| Empty string input | ✅ / ❌ |
| Classic example: `"HELLO"` with shift 13 → `"URYYB"` (ROT13) | ✅ / ❌ |

### Phase 3 — Fill the Gaps

8. For any category marked ❌, write the missing test yourself — or prompt Copilot more specifically:
   ```
   Write a test for Caesar cipher where shift = 27 (one full rotation plus one) 
   — the result should be the same as shift = 1.
   ```

---

## 🤖 Copilot Skills to Practise

| Task | How |
|------|-----|
| Generate a full test suite | Select implementation → Chat → `/tests` |
| Generate tests for a specific scenario | Chat: *"Write a test for Cipher.Encrypt that verifies wrap-around at the letter Z"* |
| Improve an existing test | Select test → Chat → *"This test only checks one case. Make it a theory with multiple inputs."* |
| Convert single tests to `[Theory]` | Ask: *"Convert these `[Fact]` tests to `[Theory]` with `[InlineData]`"* |

---

## 💡 What Makes a Good Test Suite?

Copilot's `/tests` command tends to cover the **happy path** well but may miss:

- **Boundary conditions** — the last letter of the alphabet, shift = 0, shift = 26
- **Round-trip invariants** — encrypt then decrypt should always return the original
- **Large or unusual inputs** — a shift of 10,000, an empty string, a string of only spaces
- **Negative shifts** — mathematically valid but often forgotten

Your job is to be the quality gate. Copilot generates the first draft; you ensure it is thorough.

---

## 🏁 Stretch Goals

1. Ask Copilot to convert the `[Fact]` tests to `[Theory]` with `[InlineData]` parameters — one test method covering multiple cases.
2. Ask: *"What mutation testing techniques could I apply to verify these tests actually catch bugs?"* Then manually introduce a bug (e.g. change `% 26` to `% 25`) and confirm the tests catch it.
3. Use `/doc` to generate XML documentation for the `Encrypt` and `Decrypt` methods.

---

← Back to [Exercise Index](../../README.md) | Previous: [Exercise 204](../exercise-204/README.md) | Next: [Exercise 206](../exercise-206/README.md)
