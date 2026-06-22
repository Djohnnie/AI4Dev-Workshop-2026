# Exercise 603 — Optimize Edit Distance

> **Chapter:** Chapter 6, Exercise 3  
> **Skill focus:** Measuring, profiling, and improving a slow algorithm with Copilot  
> **Difficulty:** ⭐⭐⭐

← Back to [Exercise Index](../../README.md)

---

## 🎯 Overview

This exercise starts with a deliberately slow **edit distance** implementation.

The code calculates **Levenshtein distance**: the minimum number of insertions, deletions, and substitutions needed to turn one string into another. It is correct, but the starter uses a naive recursive implementation that repeats huge amounts of work as the strings grow.

Your job is to use Copilot to:

1. measure the current behaviour
2. inspect the logs and metrics
3. identify the real bottleneck
4. refactor the implementation into something faster without changing the result

The starter includes a runnable app, unit tests, and a BenchmarkDotNet project so participants can compare evidence before and after the refactor.

---

## ✅ Your Task

Take the starter solution and improve the calculator.

The current version already exposes useful signals:

- structured console logging
- `System.Diagnostics.Metrics` counters and histograms
- a unit-test suite that protects the behaviour
- a BenchmarkDotNet runner for repeatable measurements

Use those signals to drive the refactor instead of guessing.

---

## 🗂️ Suggested Workflow

### Phase 1 — Understand the problem

Read the calculator and run the sample app.

Notice:

- how many evaluation steps the recursive version performs
- which metrics it emits
- how the log messages describe the workload

### Phase 2 — Measure before changing code

Run the tests and benchmark first.

Useful questions:

- Is the algorithm slow because of recursion, repeated work, allocations, or all three?
- Which metric would you show Copilot to help it suggest a better approach?
- How large does the input need to be before the cost is obvious?

### Phase 3 — Refactor for performance

Ask Copilot to propose a more efficient implementation while keeping the same outputs.

Good directions include:

- memoization
- dynamic programming
- reducing repeated subproblem evaluation
- making the implementation easier to measure and compare

### Phase 4 — Prove the improvement

After the refactor:

1. run the tests again
2. rerun the benchmark
3. compare diagnostics, logs, and metrics

### Phase 5 — Compare with the sample solution

A possible finished version lives in:

`..\exercise-603-solution\`

---

## ▶️ Run the starter

From this folder:

```bash
dotnet run --project EditDistanceWorkshop/EditDistanceWorkshop.csproj
dotnet test EditDistanceWorkshop.Tests/EditDistanceWorkshop.Tests.csproj
dotnet run --project EditDistanceWorkshop.Benchmarks/EditDistanceWorkshop.Benchmarks.csproj -c Release
```

---

## 🤖 Prompt Starters

```text
Act as a performance engineer. Read this naive Levenshtein implementation and explain where it is doing repeated work. Use the current logs and metrics as evidence.
```

```text
Suggest a faster implementation for this edit distance calculator, but keep the external behaviour the same and explain how to prove that with the tests and benchmark.
```

```text
Look at this BenchmarkDotNet result and these metrics. What algorithmic change would you make first for Levenshtein distance, and why?
```

```text
Refactor this recursive edit distance calculator into a more efficient approach. Keep the public API stable and preserve the same distances for the existing tests.
```

---

## 🏁 Stretch Goals

1. Add extra metrics that distinguish recursive calls from matrix cell evaluations.
2. Compare memoization with a bottom-up dynamic-programming approach.
3. Add one more benchmark input pair and discuss when naive recursion becomes unacceptable.

---

← Back to [Exercise Index](../../README.md) | Related: [Slide deck](../../../content/2-day/chapter-06/SLIDES.md)
