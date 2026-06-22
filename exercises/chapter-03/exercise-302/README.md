# Exercise 302 — Infix/Postfix with Ask Mode

> **Chapter:** Chapter 3, Exercise 2  
> **Skill focus:** Using Copilot Ask mode to understand unfamiliar code before accepting it; the Reliability & Safety principle in practice  
> **Difficulty:** ⭐⭐⭐

← Back to [Exercise Index](../../README.md)

---

## 🎯 Overview

This exercise is about **responsible use of GitHub Copilot**, not about typing the fastest. You are given one empty method and a deliberately non-obvious algorithm pair: **convert infix expressions to postfix notation, then evaluate the postfix expression**.

An expression like `1+2*3` looks simple, but there is a lot going on underneath: operator precedence, parentheses, stack behavior, and the difference between what humans write (**infix**) and what is easy for a machine to evaluate (**postfix**). It is exactly the kind of short algorithm that can feel mysterious until someone walks through it carefully.

That makes it perfect for this chapter. You will use **Copilot Ask mode** to:

1. make Copilot explain the algorithm,
2. make it help implement the TODO,
3. make it explain the result back to you until you truly understand it.

> 🧠 This is the **Reliability & Safety** principle in practice: if you accept code you cannot explain, you are outsourcing judgement instead of using AI responsibly.

---

## 📚 Background: What the Algorithm Does

Humans usually write arithmetic in **infix** form:

```text
1 + 2 * 3
```

Machines often prefer **postfix** form (also called Reverse Polish Notation):

```text
1 2 3 * +
```

Why? Because postfix can be evaluated with a simple value stack:

1. read a number → push it
2. read an operator → pop the right and left values
3. apply the operator
4. push the result back

For `1 2 3 * +`:

| Token | Stack after processing |
|---|---|
| `1` | `1` |
| `2` | `1, 2` |
| `3` | `1, 2, 3` |
| `*` | `1, 6` |
| `+` | `7` |

The tricky part is converting infix to postfix correctly:

- `*` and `/` must take priority over `+` and `-`
- `(` and `)` temporarily override precedence
- operators are managed on a separate **operator stack**

That idea is elegant, but it can feel slippery until someone walks through it carefully. Your job is to make Copilot be that guide — and then verify that you actually learned it.

---

## 🗂️ Project Structure

```text
exercise-302/
├── ExpressionEvaluatorLab.csproj
├── Program.cs               ← Runs sample cases and checks postfix + result
├── ExpressionEvaluator.cs   ← Contains the single TODO method
└── README.md
```

`ExpressionEvaluator.cs` contains exactly one empty method:

- `ExpressionEvaluator.Evaluate(string infixExpression)`

`Program.cs` already exercises the algorithm with known examples and prints a simple status next to each result.

---

## ✅ Your Task

### Phase 1 — Ask for understanding first

1. Open `ExpressionEvaluator.cs`.
2. Do **not** jump straight to inline completion.
3. In **Copilot Ask mode**, start with explanation prompts such as:
   - *"Explain how to convert `1+2*3` from infix to postfix."*
   - *"Why does `*` stay above `+` on the operator stack?"*
   - *"Walk through postfix evaluation step by step for `1 2 3 * +`."*

Before moving on, you should be able to explain:

- what **operator precedence** means,
- why the **operator stack** is needed during conversion,
- how the **value stack** evaluates postfix.

### Phase 2 — Implement the TODO with Ask mode

4. Ask Copilot to help implement `ExpressionEvaluator.Evaluate`.
5. Paste or adapt the suggested code into the TODO.
6. If the suggestion uses variable names you do not like, rename them until the code reads clearly.

Good prompt examples:

- *"Implement this TODO by converting infix to postfix and then evaluating the postfix expression, but keep the code readable for a workshop exercise."*
- *"Write the expression evaluator without extra helper classes and explain the purpose of the operator stack and value stack."*

### Phase 3 — Make Copilot teach the code back to you

7. Now ask Copilot to explain **your actual implementation** line by line.
8. Ask follow-up questions until the algorithm stops feeling magical:
   - *"Why do we pop operators of higher or equal precedence here?"*
   - *"What changes when we hit a closing parenthesis?"*
   - *"Why does postfix evaluation need two popped operands?"*
   - *"Trace `1+2*3` through the operator stack and then through the value stack."*

### Phase 4 — Run it and verify it

9. Run the program:

   ```bash
   cd exercises/chapter-03/exercise-302
   dotnet run
   ```

10. Confirm the sample cases report `OK`.
11. If any case reports `CHECK`, use Ask mode to debug the implementation rather than guessing blindly.

---

## 🤖 Copilot Skills to Practise

| Task | How |
|---|---|
| Understand an unfamiliar algorithm | Ask: *"Explain precedence, the operator stack, and postfix evaluation in plain English."* |
| Generate a safe first draft | Ask: *"Implement the TODO, but keep the variables readable and workshop-friendly."* |
| Audit generated code | Ask: *"Explain this implementation line by line and point out any edge cases."* |
| Debug with intent | Ask: *"Why does this expression produce the wrong postfix sequence or the wrong result?"* |

---

## 🏁 Completion Criteria

You have completed the exercise when:

- [ ] `ExpressionEvaluator.Evaluate` is implemented.
- [ ] `dotnet run` reports `OK` for the sample cases.
- [ ] You can explain operator precedence without reading the code.
- [ ] You can explain why the operator stack is needed during conversion.
- [ ] You can explain how the value stack evaluates postfix.

---

## 💡 Why This Exercise Belongs in Responsible AI

Copilot can absolutely help with algorithms like this. The risk is not that the generated code is always wrong — the risk is that it looks convincing enough that you stop thinking.

This exercise trains the habit you want in real work:

- ask for explanation before acceptance,
- use AI to increase understanding, not replace it,
- verify behaviour with concrete examples,
- refuse to merge code you cannot defend.

That is responsible AI usage in day-to-day development.

---

## 🏁 Stretch Goals

1. Ask Copilot to extend the implementation to support **multi-digit integers** instead of just single-digit ones.
2. Ask Copilot to add support for the **power operator** and discuss what changes for precedence and associativity.
3. Add one or two extra expressions of your own and ask Copilot to predict the postfix form before you run it.

---

← Back to [Exercise Index](../../README.md) | Related: [Slide deck](../../../content/2-day/chapter-03/SLIDES.md) | Previous: [Exercise 301](../exercise-301/README.md) | Next: [Exercise 303](../exercise-303/README.md)
