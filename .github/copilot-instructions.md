# GitHub Copilot instructions

## Build, test, and run

- Prefer **targeted project commands** from the repository root instead of a whole-repo CLI build.
  - Build a single exercise: `dotnet build exercises/chapter-04/401/TaskManager/TaskManager.csproj`
  - Run a standalone exercise: `dotnet run --project exercises/chapter-04/401/TaskManager/TaskManager.csproj`
- For **chapter 2** exercises, run the sibling test project directly.
  - Full test project: `dotnet test exercises/chapter-02/201/Factorial.Tests/Factorial.Tests.csproj`
  - Single xUnit test: `dotnet test exercises/chapter-02/201/Factorial.Tests/Factorial.Tests.csproj --filter "FullyQualifiedName~CalculateIteratively_Zero_ReturnsOne"`
- Do **not** rely on `dotnet build exercises/exercises.slnx` from the CLI. The solution currently includes `chapter-05/502/ContextPromptExtension.csproj`, and that VSIX requires Visual Studio 2026 tooling.
- **Exercise 502** must be opened directly in **Visual Studio 2026** and started with **F5**. Its debug profile launches the experimental instance with `devenv.exe /rootsuffix Exp`.
- There is **no repo-specific lint or format command** checked in today.

## High-level architecture

- The repository is split into:
  - `content/1-day` and `content/2-day`: workshop curriculum and slide/readme content; markdown only, not part of the .NET build.
  - `exercises/`: the actual sample apps and coding exercises.
  - `tools/`: small workshop utilities.
- `exercises/` is **not one shared application**. Each exercise is a self-contained sample aligned to a workshop chapter.
- **Chapter 1** contains Azure OpenAI / Agent Framework console samples:
  - `105` is intentionally split into an **MCP server** and **MCP client**.
  - `106` implements a multi-agent pipeline: **summary agent -> orchestrator agent -> selected specialist agents in parallel -> reply agent**.
- **Chapter 2** consists of small algorithm projects with a sibling `*.Tests` project. The tests are the primary executable specification for expected behavior.
- **Chapter 3** contains workflow and MCP labs. `303` is the clearest MCP server reference: a minimal ASP.NET Core app that registers tools with `AddMcpServer()` and exposes them via HTTP.
- **Chapter 4** (`401`, `402`, `403`) reuses the same **TaskManager** minimal API shape in separate exercise snapshots:
  - `Program.cs` is the composition root.
  - `Endpoints/` maps minimal API routes via extension methods.
  - `Services/` holds business logic.
  - `Repositories/` is an in-memory persistence layer.
  - `TaskManager.http` is the manual verification harness for request/response scenarios.
- **Chapter 5** mixes app types:
  - `501` is a Spectre.Console escape-room console app wired with dependency injection.
  - `502` is a Visual Studio VSIX, not a normal SDK-style console/web project.

## Key conventions

- Treat each exercise folder as **intentionally independent**, even when two exercises look nearly identical. Do not refactor shared code across exercises unless the task explicitly asks for it.
- Read the **exercise-local `README.md` first** before changing code. In this repository, the exercise README is the task brief, expected behavior, and manual test guide.
- For **chapter 4** work, keep the existing minimal API layering intact: route mapping in `Endpoints`, behavior in `Services`, storage in `Repositories`.
- For **MCP** exercises, preserve the established registration pattern:
  - server setup in `Program.cs` with `AddMcpServer()` / `WithHttpTransport()` / `WithTools<T>()`
  - endpoint exposure with `app.MapMcp()`
  - tool contracts expressed with `[McpServerToolType]`, `[McpServerTool]`, and `[Description]`
- For the **agent-based console apps**, keep `Program.cs` as a thin composition root and pull environment-specific configuration from environment variables or optional development settings rather than hardcoding secrets.
- When README guidance and the current project graph disagree, trust the **current files and build behavior**. There is at least one live mismatch today around `exercises.slnx` and exercise `502`.
