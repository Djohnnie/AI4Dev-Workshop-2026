# Exercise 404 — Build a Dev Dashboard

> **Chapter:** Chapter 4, Exercise 4  
> **Skill focus:** Vibe-coding with GitHub Copilot CLI — shell commands as agentic tools  
> **Difficulty:** ⭐⭐

← Back to [Exercise Index](../../README.md)

---

## 🎯 Overview

You have a .NET console app with a placeholder message. Your job is to turn it into **DevDash** — a developer workspace inspector that, when run from any project directory, shows a live status report by running real shell commands.

The twist: you will not write a single line of code yourself. You will describe what you want to Copilot CLI, approve each command it proposes, and let it scaffold, install packages, write code, and build the app for you. This is vibe coding with an actual agentic tool.

---

## 🗂️ Project Structure

```
404/
└── DevDash/
    ├── DevDash.csproj
    └── Program.cs       ← placeholder only — Copilot CLI fills this in
```

---

## ✅ Your Task

### Phase 1 — Run the starter

1. Open a terminal in the `DevDash` folder.
2. Run the app to see the placeholder:

   ```bash
   dotnet run
   ```

3. Confirm it compiles and prints the placeholder message. This is your baseline.

### Phase 2 — Describe the feature to Copilot CLI

4. Open a new terminal session and launch Copilot CLI (or use the one you already have).
5. Paste the following prompt — or write your own version of it:

   ```
   I have a .NET 10 console app called DevDash in the current directory.
   Turn it into a developer workspace inspector. When run from any directory it should:

   - Show the current git branch and whether there are uncommitted changes
   - Print the last 3 commit messages with their short hashes
   - Check if a .NET project exists in the folder and whether dotnet build succeeds
   - List any running Docker containers and their status

   Use clear section headers and ✓ / ✗ status symbols.
   Run dotnet build at the end to verify the result compiles.
   ```

6. Review the plan Copilot CLI proposes. You will see it intend to run commands like `dotnet add package`, write code to `Program.cs`, and run `dotnet build`.

### Phase 3 — Watch and approve

7. Approve each command one at a time. Notice:
   - Which shell commands Copilot CLI proposes (e.g. `dotnet add package`, file writes, `dotnet build`)
   - How it reads the output of each command before deciding the next step
   - How it adapts if a build fails — that is the agentic loop in action

8. Once it finishes, run the app from a real project folder that has git, .NET, and Docker available:

   ```bash
   dotnet run
   ```

### Phase 4 — Extend

9. The app works. Now push further — give Copilot CLI a follow-up prompt to add at least one more feature. Some ideas:

   - *"Add a section showing the five most recently modified files."*
   - *"Show the installed .NET SDK versions using `dotnet --list-sdks`."*
   - *"Add a `--watch` flag that re-runs the inspector every 10 seconds."*
   - *"Add colour: green for ✓, red for ✗, using ANSI escape codes."*
   - *"Add a kubectl section that shows pods in the default namespace if kubectl is available."*

---

## 🤖 Copilot Skills to Practise

| Task | How |
|------|-----|
| Vibe-code from a description | Give Copilot CLI a natural-language goal, not a spec |
| Watch the agentic loop | Observe reason → command → output → adapt in real time |
| Approve shell commands | Confirm each proposed command before it executes |
| Iterate with follow-up prompts | Extend the app with additional conversational prompts |

---

## 💡 Why This Exercise

Exercises 401–403 used Agent Mode and Plan Mode *inside* VS Code — the IDE was always in the loop. This exercise moves the work into the terminal. Copilot CLI reasons about your codebase, picks the right tools (dotnet, git, docker), runs them in your shell with your approval, reads the output, and adapts. There is no "Keep/Undo" dialog — just the agentic loop running against your real toolchain.

The app you are building is also a concrete example of the pattern: it calls shell commands programmatically and presents their output. You experience both sides of the coin in one exercise.

---

## 🏁 Stretch Goals

1. **Port scanner.** Add a section that checks whether ports 5000, 3000, and 8080 are in use, using `netstat` or `ss`.
2. **README summariser.** If a README.md exists in the directory, use Copilot CLI to add a section that prints the first paragraph.
3. **Cross-platform.** Ask Copilot CLI to make the git and docker commands work on both Windows (PowerShell) and Linux/macOS (bash).
4. **Export to Markdown.** Add a `--export` flag that writes the dashboard output to `devdash-report.md`.

---

← Back to [Exercise Index](../../README.md) | Related: [Slide deck](../../../content/2-day/chapter-04/SLIDES.md) | Previous: [Exercise 403](../403/README.md)
