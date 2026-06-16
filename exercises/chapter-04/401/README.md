# Exercise 401 — Rename a Field with Agent Mode

> **Chapter:** Chapter 4, Exercise 1  
> **Skill focus:** Multi-file refactoring with Agent Mode  
> **Difficulty:** ⭐⭐

← Back to [Exercise Index](../../README.md)

---

## 🎯 Overview

When a field name is wrong in a real codebase it touches more than one file. Renaming `Name` to `Title` on your `TaskItem` entity means updating the entity definition, the create-request model, the service layer, and the endpoint handler — four files, multiple call sites, nothing hard to do manually but easy to miss one.

This is exactly the kind of sweep Agent Mode was built for. Instead of hunting and editing manually, you give it a single instruction, watch it read the relevant files, plan its edits, and apply them one by one. The goal of this exercise is to see that loop in action and build intuition for when to reach for it.

---

## 🗂️ Project Structure

```
401/
└── TaskManager/
    ├── TaskManager.csproj
    ├── Program.cs                   ← entry point, DI wiring
    ├── Entities/
    │   └── TaskItem.cs              ← Name property lives here
    ├── Models/
    │   └── CreateTaskRequest.cs     ← Name property also here
    ├── Repositories/
    │   ├── ITaskRepository.cs
    │   └── TaskRepository.cs        ← in-memory store
    ├── Services/
    │   ├── ITaskService.cs
    │   └── TaskService.cs           ← sets Name when creating a task
    └── Endpoints/
        └── TaskEndpoints.cs         ← reads request.Name, one method per endpoint
```

The API exposes four endpoints through extension methods on `RouteGroupBuilder`, a service layer that owns business logic, and an in-memory repository. The field `Name` on `TaskItem` is intentionally generic — your job is to rename it to `Title`.

---

## ✅ Your Task

### Phase 1 — Get the project running

1. Open a terminal in `exercises/chapter-04/401/TaskManager`.
2. Restore and run:

   ```bash
   dotnet run
   ```

3. Confirm the API starts — you will see `Now listening on http://localhost:5000`.
4. Verify the endpoint responds (empty array is fine):

   ```bash
   curl http://localhost:5000/tasks
   ```

### Phase 2 — Rename with Agent Mode

5. Open VS Code in `exercises/chapter-04/401/TaskManager` and enable **Agent Mode** in the Copilot chat panel (the toggle in the chat input area).
6. Type a single prompt:

   ```
   Rename the Name field to Title across the entire codebase.
   ```

7. Watch the loop — narrate what you see to a partner:
   - Agent Mode reads the entity and models first to understand the shape.
   - It decides which files need edits and in what order.
   - It opens each file in the diff view and applies the change.
   - It may run `dotnet build` to confirm nothing is broken.

8. When it finishes, **review every diff** before accepting.

### Phase 3 — Verify

9. Rebuild to confirm no compiler errors:

   ```bash
   dotnet build
   ```

10. Create a task and check that the JSON response now uses `title`:

    ```bash
    curl -s -X POST http://localhost:5000/tasks \
         -H "Content-Type: application/json" \
         -d '{"title": "Buy groceries"}' | cat
    ```

    You should see `"title":"Buy groceries"` in the response, not `"name"`.

---

## 🤖 Copilot Skills to Practise

| Task | How |
|------|-----|
| Multi-file rename via Agent Mode | Single chat prompt in Agent Mode panel |
| Review generated diffs | Inspect each changed file in the diff view before accepting |
| Confirm correctness | `dotnet build` + `curl` to the running endpoint |
| Follow-up correction | If a spot was missed: *"You didn't update the Name parameter in TaskEndpoints — fix it."* |

---

## 💡 Why This Matters

A manual find-and-replace catches exact string matches. It misses the semantic relationship between a property name and the call sites that read it. Agent Mode reads the code, understands the structure, and targets only the right occurrences — exactly what you would do yourself, just without the context-switching.

The rename here is small on purpose. The same pattern scales to forty files.

---

## 🏁 Stretch Goals

1. **Add a Description field.** Ask Agent Mode to add a `string Description` property to `TaskItem` and wire it through the create-request model, service, and endpoint — without you touching a file.
2. **Add a priority.** Ask it to add a `Priority` enum (`Low`, `Medium`, `High`) with a default of `Medium`, exposed in both the create request and the response body.
3. **Write a README snippet.** After the rename, ask Copilot Chat: *"Write a two-paragraph description of this API suitable for a project README."*

---

← Back to [Exercise Index](../../README.md) | Related: [Slide deck](../../../content/2-day/chapter-04/SLIDES.md)
