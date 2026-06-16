# Exercise 402 — Add Input Validation

> **Chapter:** Chapter 4, Exercise 2  
> **Skill focus:** Adding input validation to minimal API endpoints with Agent Mode  
> **Difficulty:** ⭐⭐

← Back to [Exercise Index](../../README.md)

---

## 🎯 Overview

The TaskManager API currently accepts any value for `Title`, including null and empty strings. Real APIs guard their boundaries. In this exercise you use Agent Mode to add input validation to the create endpoint so the API rejects invalid titles with a descriptive 400 response.

Validation is a cross-cutting concern — it typically touches the model, the endpoint, and sometimes shared middleware. Agent Mode can identify all the right places and add the rules consistently, without you having to remember each one.

---

## 🗂️ Project Structure

```
402/
└── TaskManager/
    ├── TaskManager.csproj
    ├── TaskManager.http             ← includes invalid-input test cases
    ├── Program.cs
    ├── Entities/
    │   └── TaskItem.cs
    ├── Models/
    │   └── CreateTaskRequest.cs     ← validation annotations go here
    ├── Repositories/
    │   ├── ITaskRepository.cs
    │   └── TaskRepository.cs
    ├── Services/
    │   ├── ITaskService.cs
    │   └── TaskService.cs
    └── Endpoints/
        └── TaskEndpoints.cs         ← validation logic goes here
```

---

## ✅ Your Task

### Phase 1 — Confirm the gap

1. Run the project:

   ```bash
   dotnet run
   ```

2. Open `TaskManager.http` and send the **"empty title"** request. It returns `201 Created`. That is the gap to fix.

### Phase 2 — Add validation with Agent Mode

3. Enable **Agent Mode** in VS Code and type a single prompt:

   ```
   Add input validation to the create task endpoint.
   Title must not be null or empty and must not exceed 200 characters.
   Return a 400 Bad Request with validation details when the input is invalid.
   ```

4. Watch Agent Mode:
   - Read `CreateTaskRequest` and `TaskEndpoints` to understand the current shape.
   - Add validation — either data annotations on the record, inline guard clauses in the endpoint, or an endpoint filter.
   - Possibly run a build to confirm the changes compile.

5. Review every diff before accepting.

### Phase 3 — Test

6. Rebuild:

   ```bash
   dotnet build
   ```

7. Run and send each test case from `TaskManager.http`:
   - Empty title → `400 Bad Request`
   - Whitespace-only title → `400 Bad Request`
   - Title over 200 characters → `400 Bad Request`
   - Valid title → `201 Created`

---

## 🤖 Copilot Skills to Practise

| Task | How |
|------|-----|
| Add validation via Agent Mode | Single prompt in Agent Mode panel |
| Review validation strategy | Ask Chat: *"Is this approach idiomatic for ASP.NET Core minimal APIs?"* |
| Verify error shape | Inspect the 400 response body — does it follow Problem Details (RFC 7807)? |
| Follow-up tweak | If Agent Mode used annotations but skipped the filter: *"The annotations are in place but the endpoint does not read them — wire up the validation filter."* |

---

## 💡 Why This Matters

Manual find-and-replace can add a null check to one place. Agent Mode reads the code, understands the architecture (annotations on the model, filter on the endpoint), and adds validation at the right layer — the same decision a developer would make, applied consistently.

---

## 🏁 Stretch Goals

1. **Whitespace guard.** If Agent Mode used `[Required]` but accepted `"   "`, add `[MinLength(1)]` combined with a `.Trim()` or ask it to use `string.IsNullOrWhiteSpace` instead.
2. **Reusable filter.** Ask Agent Mode to extract the validation into an `IEndpointFilter` that can be reused across multiple endpoints.
3. **Write tests.** Ask Copilot to generate xUnit tests that POST invalid payloads and assert the 400 responses.

---

← Back to [Exercise Index](../../README.md) | Related: [Slide deck](../../../content/2-day/chapter-04/SLIDES.md) | Previous: [Exercise 401](../401/README.md)
