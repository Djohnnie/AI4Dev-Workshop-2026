# Exercise 503 — Context Variables Playground

> **Chapter:** Chapter 5, Exercise 3  
> **Skill focus:** Practising `@workspace` and `#file` on a prebuilt multi-file .NET app  
> **Difficulty:** ⭐⭐

← Back to [Exercise Index](../../README.md)

---

## 🎯 Overview

This exercise is a **playground**, not a build-from-scratch task. The app is already there. Participants open the codebase and use GitHub Copilot Chat to compare how answers change when they ask with:

- no variable at all
- `@workspace`
- `#file`

The goal is to build intuition for **when repo-wide context helps** and **when a precise file reference is better**.

---

## 🗂️ Project Structure

```
503/
├── ContextVariablesPlayground.csproj
├── Program.cs
├── Endpoints/
│   └── PlaygroundEndpoints.cs
├── Models/
│   ├── ContextQuestion.cs
│   ├── IncidentDigest.cs
│   ├── SprintHealthSnapshot.cs
│   └── WorkItem.cs
├── Repositories/
│   ├── IWorkItemRepository.cs
│   └── InMemoryWorkItemRepository.cs
├── Services/
│   ├── AuthGateway.cs
│   ├── ContextQuestionCatalog.cs
│   ├── IncidentDigestService.cs
│   ├── KnowledgeMapService.cs
│   └── SprintHealthService.cs
├── docs/
│   └── context-questions.md
└── wwwroot/
    ├── index.html
    └── site.css
```

The codebase is intentionally small but connected: endpoints call services, services depend on repository data, and a couple of TODOs are hidden in the implementation so participants can discover them through prompts instead of manual scanning.

---

## ✅ Your Task

### Phase 1 — Run the app locally

1. Open a terminal in `exercises/chapter-05/exercise-503`.
2. Run the app:

   ```bash
   dotnet run
   ```

3. Open the URL shown in the terminal. The page lists suggested context-variable experiments and API endpoints.

### Phase 2 — Use it as a context-variable playground

Do **not** add new features. The exercise is already complete. Your job is to experiment with the prompts against the existing code.

Try these in Copilot Chat:

1. Repo map:

   ```text
   @workspace map this project and tell me which files I should inspect first to understand sprint health and incident digests.
   ```

2. Exact-file zoom-in:

   ```text
   #file:Services/AuthGateway.cs explain how dashboard access is validated and whether this looks production-ready.
   ```

3. Hot-path tracing:

   ```text
   @workspace trace how a work item becomes part of the incident digest response.
   ```

4. TODO discovery:

   ```text
   @workspace are there any TODO comments in this exercise, and which one matters most for production quality?
   ```

5. Compare variable impact:

   ```text
   Explain what determines the risk band in this app.
   ```

   Then ask the same question again with `@workspace`.

### Phase 3 — Discuss what changed

Compare answers in pairs:

- Which prompt produced the most accurate repo map?
- When did `#file` beat `@workspace`?
- Which answers became more specific once Copilot saw the right context?

---

## 🤖 Copilot Skills to Practise

| Task | How |
|------|-----|
| Explore an unfamiliar repo quickly | Start with `@workspace` instead of opening random files |
| Zoom in without overloading context | Switch to `#file` when one file becomes central |
| Compare answer quality | Ask the same question with and without variables |
| Surface hidden implementation details | Use Copilot to find TODOs, auth flow, and data paths |

---

## 🏁 Stretch Goals

1. Try the same exploration tasks in VS Code and on GitHub.com.
2. Ask Copilot to rank the files from “best first read” to “detail only.”
3. Use `#file` on one service and ask Copilot which other file it would inspect next.

---

## Notes

- This is a **playground**. Participants do not need to code anything to complete it.
- The built-in dashboard exists only to make the repo feel like a real application and to give you stable files to reference in prompts.

---

← Back to [Exercise Index](../../README.md)
