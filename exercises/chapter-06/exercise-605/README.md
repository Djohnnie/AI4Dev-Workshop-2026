# Exercise 605 — Hunt the Cursed Theme Park Checkout Bug

> **Chapter:** Chapter 6, Exercise 5  
> **Skill focus:** Debugging multiple runtime exceptions with Copilot, logs, HTTP evidence, and browser-driven reproduction  
> **Difficulty:** ⭐⭐⭐

← Back to [Exercise Index](../../README.md)

---

## 🎯 Overview

Welcome to the **Cursed Theme Park**.

Guests are trying to book haunted rides, VIP night passes, and cursed snacks through a .NET 10 checkout flow. Unfortunately, the park is haunted by several runtime exceptions.

The starter app includes:

- a small ASP.NET Core web app with a browser UI
- multiple reproducible runtime failures
- structured logging and lightweight telemetry hooks
- an HTTP file for direct API reproduction
- a unit-test project with **repro tests** that capture the current failure paths

Your job is to use Copilot to **reproduce, narrow, and fix** the issues one by one.

---

## ✅ What Participants Should Do

### Phase 1 — Run the cursed system

Start the app and click through the demo scenarios in the browser.

Notice:

- which scenarios succeed
- which ones crash
- what the UI shows versus what the code likely intended
- what the console logs reveal about the failing step

### Phase 2 — Reproduce the failure precisely

You can reproduce each bug in several ways:

- from the browser UI
- with the `CursedThemePark.http` file
- with tests in `CursedThemePark.Tests`
- with Playwright if you want the agent to watch the failure happen

The goal is to turn a spooky report into a **repeatable bug**.

### Phase 3 — Narrow the fault

Use Copilot to inspect:

- the request data for the failing scenario
- the checkout pipeline
- logs and stack traces
- which assumption breaks first

Look for the difference between:

- the **guest story** ("I tried to buy haunted popcorn for the VIP ride")
- and the **technical trigger** (null operator, missing snack inventory, weather check failure, divide-by-zero, double dispatch)

### Phase 4 — Fix safely

Pick one cursed scenario at a time.

For each one:

1. reproduce it
2. isolate the fault
3. implement the smallest safe fix
4. convert or extend the repro test into a regression test

### Phase 5 — Leave the system better than you found it

After each fix, improve at least one of these:

- exception clarity
- logs
- validation
- scenario coverage
- regression tests

---

## 🗂️ Repro Scenarios

The starter includes these haunted checkout failures:

1. **Ghost Train VIP** — a `NullReferenceException` caused by a missing ride operator
2. **Thunder Loop Weather** — an `HttpRequestException` when the weather safety check fails
3. **Haunted Popcorn** — a `KeyNotFoundException` from missing snack inventory
4. **Zero Group Discount** — a `DivideByZeroException` in pricing
5. **Encore Dispatch** — an `InvalidOperationException` when the same ride is started twice

There is also one **happy path** scenario so you can compare healthy behaviour with cursed behaviour.

---

## ▶️ Run the starter

From this folder:

```bash
dotnet run --project CursedThemePark/CursedThemePark.csproj
dotnet test CursedThemePark.Tests/CursedThemePark.Tests.csproj
```

Then open the local URL shown in the terminal and trigger the scenarios from the UI.

---

## 🤖 Prompt Starters

```text
Act as a production debugging assistant. Read this failing checkout flow and identify which runtime exception is happening first, what input triggers it, and what evidence I should capture.
```

```text
I have a reproducible haunted checkout bug. Use the logs, request payload, and stack trace to explain the most likely broken assumption before suggesting a fix.
```

```text
Turn this repro test into a regression test. Keep the scenario realistic and make the expected behaviour explicit.
```

```text
Use Playwright to reproduce this checkout scenario, inspect the visible failure, and summarize what the user saw versus what the system should have done.
```

---

## 🏁 Stretch Goals

1. Replace one runtime exception with a clearer domain validation failure.
2. Add richer metrics or trace tags for each scenario.
3. Add one more cursed checkout scenario of your own.

---

← Back to [Exercise Index](../../README.md) | Related: [Slide deck](../../../content/2-day/chapter-06/SLIDES.md)
