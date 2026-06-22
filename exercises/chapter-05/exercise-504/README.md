# Exercise 504 — Prompt Pattern Playground

> **Chapter:** Chapter 5, Exercise 4  
> **Skill focus:** Practising advanced prompting patterns on an already-built .NET app  
> **Difficulty:** ⭐⭐

← Back to [Exercise Index](../../README.md)

---

## 🎯 Overview

This exercise ends section 4 with a **playground** for advanced prompting patterns. The app and tests are already built. Participants do not need to add new features. Instead, they use the existing code to experiment with:

- comment-driven development
- test-first prompting
- persona prompts
- stepwise decomposition
- diff-driven prompting

The goal is to try the **same codebase** with different prompting strategies and compare how the conversations change.

---

## 🗂️ Project Structure

```
504/
├── PromptPatternsPlayground.csproj
├── Program.cs
├── Endpoints/
│   └── PatternLabEndpoints.cs
├── Models/
│   ├── ImportPlan.cs
│   ├── ReleaseSummary.cs
│   ├── ReleaseWindowDecision.cs
│   └── ScenarioCard.cs
├── Services/
│   ├── ImportWorkflowService.cs
│   ├── LegacyAuditLogger.cs
│   ├── LegacyBillingLogger.cs
│   ├── ReleaseSummaryService.cs
│   ├── ReleaseWindowCalculator.cs
│   ├── ScenarioCatalog.cs
│   ├── SecurityHeadersPolicy.cs
│   └── StructuredAuditLogger.cs
├── PromptPatternsPlayground.Tests/
│   ├── PromptPatternsPlayground.Tests.csproj
│   ├── ReleaseWindowCalculatorTests.cs
│   └── SecurityHeadersPolicyTests.cs
├── docs/
│   └── pattern-scenarios.md
└── wwwroot/
    ├── index.html
    └── site.css
```

---

## ✅ Your Task

### Phase 1 — Run the playground

1. Open a terminal in `exercises/chapter-05/exercise-504`.
2. Run the app:

   ```bash
   dotnet run
   ```

3. In a second terminal, run the tests:

   ```bash
   dotnet test PromptPatternsPlayground.Tests/PromptPatternsPlayground.Tests.csproj
   ```

4. Open the URL shown in the terminal. The page lists the built-in scenarios and useful endpoints.

### Phase 2 — Use the code as a prompt lab

Do **not** build new features unless you want extra practice. The default workshop use is to experiment with prompts against the code already there.

Try these:

1. Comment-driven:

   ```text
   #file:Services/ReleaseSummaryService.cs explain how the leading comment constrains the implementation.
   ```

2. Test-first:

   ```text
   #file:Services/ReleaseWindowCalculator.cs list the tests you would want before changing this calculator.
   ```

3. Persona:

   ```text
   Act as a security reviewer. #file:Services/SecurityHeadersPolicy.cs what concerns or follow-up questions do you have?
   ```

4. Stepwise:

   ```text
   Break a refactor of #file:Services/ImportWorkflowService.cs into five small steps, then do only step 1.
   ```

5. Diff-driven:

   ```text
   Compare #file:Services/LegacyAuditLogger.cs and #file:Services/StructuredAuditLogger.cs. Apply the same logging transform to #file:Services/LegacyBillingLogger.cs.
   ```

### Phase 3 — Compare patterns

Discuss which strategy felt strongest for each problem:

- Which prompt gave the clearest next action?
- When was a reviewer persona more useful than a direct code-generation ask?
- Did the diff-driven prompt remove ambiguity faster than describing the change in prose?

---

## 🤖 Copilot Skills to Practise

| Task | How |
|------|-----|
| Ground code generation in comments | Use the annotated summary service as a comment-driven prompt target |
| Ask for tests before edits | Use the release window calculator and compare Copilot's suggested cases with the included tests |
| Add a reviewer lens | Use a security persona against the header policy |
| Decompose a workflow | Ask for stepwise plans on the import service |
| Show, don't tell | Use the logger pair as a diff-driven transformation example |

---

## 🏁 Stretch Goals

1. Run the same scenario once in Chat and once in Agent Mode.
2. Ask Copilot to critique which pattern is the wrong fit for a given file.
3. Turn one of the included prompt ideas into a reusable prompt file.

---

## Notes

- This is a **playground**. The code is intentionally ready to explore without any setup work beyond `dotnet run`.
- The tests are included so participants have a stable target when discussing test-first prompting.

---

← Back to [Exercise Index](../../README.md)
