# Exercise 403 — Paginate, Filter and Sort

> **Chapter:** Chapter 4, Exercise 3  
> **Skill focus:** Using Plan Mode to design and implement a multi-part feature  
> **Difficulty:** ⭐⭐⭐

← Back to [Exercise Index](../../README.md)

---

## 🎯 Overview

The GET /tasks endpoint currently returns every task in the store as a flat array. In production that breaks down quickly. You need to limit results, search for specific tasks, and control sort order — but adding three query parameters touches the endpoint handler, the service interface, the service implementation, and possibly a new result model. That is a lot of places to coordinate.

This is exactly the scenario Plan Mode was designed for. Instead of diving straight into code, you describe the feature in natural language, let Plan Mode produce a step-by-step blueprint, review it, and then approve execution. The goal of this exercise is to experience that review-before-you-build loop and see how it compares to jumping straight into Agent Mode.

---

## 🗂️ Project Structure

```
403/
└── TaskManager/
    ├── TaskManager.csproj
    ├── TaskManager.http             ← seed requests + post-feature test cases
    ├── Program.cs
    ├── Entities/
    │   └── TaskItem.cs
    ├── Models/
    │   └── CreateTaskRequest.cs
    ├── Repositories/
    │   ├── ITaskRepository.cs
    │   └── TaskRepository.cs        ← Plan Mode will likely extend this
    ├── Services/
    │   ├── ITaskService.cs          ← interface will need new parameters
    │   └── TaskService.cs           ← implementation will change
    └── Endpoints/
        └── TaskEndpoints.cs         ← GET handler will gain query parameters
```

---

## ✅ Your Task

### Phase 1 — Seed some data

1. Run the project:

   ```bash
   dotnet run
   ```

2. Open `TaskManager.http` and send all five **Seed** requests to populate the store.
3. Complete at least one task so you have a mix of completed and incomplete items.
4. Send **Get all tasks** — confirm a flat array comes back.

### Phase 2 — Plan the feature

5. Open the Copilot Chat panel in VS Code and switch to **Plan Mode**.
6. Paste the following prompt:

   ```
   Add pagination, filtering and sorting to the GET /tasks endpoint.

   Pagination: page (integer, default 1) and pageSize (integer, default 10) query parameters.
   Filtering: isCompleted (bool, optional) and search (string, optional, case-insensitive match on Title).
   Sorting: sortBy (title or createdAt, default createdAt) and sortOrder (asc or desc, default asc).

   Return the results wrapped in a JSON object with: items, totalCount, page and pageSize.
   ```

7. Wait for Plan Mode to produce the blueprint. Do **not** approve yet.

### Phase 3 — Review the plan

8. Read every step carefully:
   - Does it introduce a new result model or modify the existing interface?
   - Does it change the repository, the service, or both?
   - Are the default values for the query parameters what you asked for?
   - Is there anything you would change before letting it run?

9. Adjust the plan by adding a follow-up message if needed — for example: *"Use `IQueryable`-style chaining instead of multiple `if` blocks."* or *"Keep the repository interface unchanged and do all filtering in the service."*

### Phase 4 — Execute and test

10. Approve the plan and let Plan Mode implement it.
11. Rebuild:

    ```bash
    dotnet build
    ```

12. Open `TaskManager.http` and run the requests under **After Plan Mode implements the feature** to verify each capability:
    - Pagination splits results correctly across pages
    - `isCompleted` filter returns only matching tasks
    - `search` returns tasks whose title contains the keyword
    - `sortBy=title` and `sortBy=createdAt` both work in both directions
    - The combined query works as expected

---

## 🤖 Copilot Skills to Practise

| Task | How |
|------|-----|
| Describe a multi-part feature | Plan Mode prompt in the Copilot Chat panel |
| Refine the plan before running | Follow-up message in Plan Mode before approving |
| Review generated diffs | Check each changed file in the diff view |
| Spot missed edge cases | Chat: *"What happens if pageSize is 0 or negative?"* |

---

## 💡 Why Plan Mode Here

This feature touches four files — endpoint, service interface, service implementation, and a new response model. Agent Mode would start editing immediately and you would review each diff as it arrives. Plan Mode stops first, shows you the full sequence of changes, and lets you redirect before a single file is touched. For features with broad impact, the upfront review catches architectural decisions (where does the filtering live?) before they are baked in.

---

## 🏁 Stretch Goals

1. **Guard the parameters.** Ask Copilot: *"Add validation so pageSize is between 1 and 100 and page is at least 1."*
2. **Sort by completion.** Extend the `sortBy` parameter to accept `isCompleted` as a third option.
3. **Add a total pages field.** Ask Plan Mode to add `totalPages` to the response model, calculated from `totalCount` and `pageSize`.

---

← Back to [Exercise Index](../../README.md) | Related: [Slide deck](../../../content/2-day/chapter-04/SLIDES.md) | Previous: [Exercise 402](../402/README.md)
