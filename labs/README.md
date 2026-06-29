# 🧪 Labs

Welcome to the hands-on **lab** collection for the **AI for Developers Workshop 2026**. Where the [exercises](../exercises/README.md) are short, focused drills on a single Copilot skill, the labs are larger, build-a-real-app challenges that ask you to take something from starter (or empty folder) to a working application using everything you have learned so far.

Every lab builds the same game — **Ultimate Snake** — but each one raises the bar and introduces a new way of working with GitHub Copilot. By the capstone you go from a single-player console game to a real-time multiplayer web app, having practised completions, Agent Mode, the Copilot CLI, custom instructions, the full AI-assisted lifecycle, and finally an end-to-end build.

All labs target **C# (.NET 10)**. Open the solution file [`labs.slnx`](labs.slnx) in Visual Studio or Rider to load every lab and solution at once.

---

## 📋 Lab Index

| # | Lab | Chapter | Copilot Skill | Difficulty |
|---|-----|---------|---------------|------------|
| [201](chapter-2/lab-201/README.md) | [Ultimate Snake](chapter-2/lab-201/README.md) | [Chapter 2](../content/2-day/chapter-2/README.md) | Building a console game with Chat (Ask), inline chat, and ghost text — no Agent Mode | ⭐⭐⭐ |
| [401](chapter-4/lab-401/README.md) | [Ultimate Snake Web](chapter-4/lab-401/README.md) | [Chapter 4](../content/2-day/chapter-4/README.md) | Finishing a web app end to end with Agent Mode | ⭐⭐⭐ |
| [402](chapter-4/lab-402/README.md) | [Ultimate Snake from Scratch with Copilot CLI](chapter-4/lab-402/README.md) | [Chapter 4](../content/2-day/chapter-4/README.md) | Building from an empty folder with the GitHub Copilot CLI | ⭐⭐⭐⭐ |
| [501](chapter-5/lab-501/README.md) | [Ultimate Snake with Instructions, Prompt Files, and Skills](chapter-5/lab-501/README.md) | [Chapter 5](../content/2-day/chapter-5/README.md) | Shaping Copilot with `copilot-instructions.md`, prompt files, and a skill file | ⭐⭐⭐⭐ |
| [601](chapter-6/lab-601/README.md) | [Ultimate Snake Across the Entire Lifecycle](chapter-6/lab-601/README.md) | [Chapter 6](../content/2-day/chapter-6/README.md) | Driving the full lifecycle — analysis, implementation, validation, debugging, docs, PR | ⭐⭐⭐⭐ |
| [801](chapter-8/lab-801/README.md) | [Multiplayer Ultimate Snake](chapter-8/lab-801/README.md) | [Chapter 8](../content/2-day/chapter-8/README.md) | Capstone: a real-time multiplayer web build combining every workshop topic | ⭐⭐⭐⭐⭐ |

---

## 🗺️ Labs by Chapter

| Chapter | Labs |
|---------|------|
| [Chapter 2](../content/2-day/chapter-2/README.md) | [201 Ultimate Snake](chapter-2/lab-201/README.md) |
| [Chapter 4](../content/2-day/chapter-4/README.md) | [401 Ultimate Snake Web](chapter-4/lab-401/README.md) · [402 Ultimate Snake from Scratch with Copilot CLI](chapter-4/lab-402/README.md) |
| [Chapter 5](../content/2-day/chapter-5/README.md) | [501 Ultimate Snake with Instructions, Prompt Files, and Skills](chapter-5/lab-501/README.md) |
| [Chapter 6](../content/2-day/chapter-6/README.md) | [601 Ultimate Snake Across the Entire Lifecycle](chapter-6/lab-601/README.md) |
| [Chapter 8](../content/2-day/chapter-8/README.md) | [801 Multiplayer Ultimate Snake](chapter-8/lab-801/README.md) |

Most labs ship a companion `…-solution` folder with a complete reference implementation (Lab 601 is left open by design). Treat the solutions as a reference to compare against — not something to copy before you try the lab yourself.

---

## 🚀 Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/) with the C# Dev Kit
- GitHub Copilot subscription (Free, Pro, or Business)
- For Lab 402: the [GitHub Copilot CLI](https://docs.github.com/en/copilot/concepts/agents/about-copilot-cli) (`winget install GitHub.Copilot`, `brew install copilot-cli`, or `npm install -g @github/copilot`)

### Running a Lab

```bash
# Open the full solution in your IDE
cd labs

# Run a lab that ships a starter project (e.g. Lab 401)
cd chapter-4/lab-401
dotnet run
```

Labs 402, 501, and 601 start from an (almost) empty folder on purpose — there is no project to run until you have scaffolded one with Copilot. Build it first, then `dotnet run`.

---

## 📌 Tips for All Labs

- **Pick the workflow the lab is teaching** — each lab restricts or steers you toward a specific Copilot mode (Chat-only, Agent Mode, CLI, prompt files…). Staying inside those guardrails is the point.
- **Build the smallest working slice first** — get something on screen, then iterate. Don't ask for the whole game in one prompt.
- **Review every diff and approve commands intentionally** — the labs are deliberately big enough that blindly accepting output will bite you.
- **Compare with the solution at the end** — once your version works, open the `…-solution` folder and see what you'd do differently.

---

← Back to [Workshop Home](../README.md) | [🏋️ Exercises](../exercises/README.md)
