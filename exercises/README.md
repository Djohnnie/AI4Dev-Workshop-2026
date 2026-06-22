# 🏋️ Exercises

Welcome to the hands-on exercise collection for the **AI for Developers Workshop 2026**. Each exercise is paired with a workshop chapter and gives you a focused, self-contained coding challenge designed to practise a specific GitHub Copilot skill or AI-assisted development technique.

Most exercises are written in **C# (.NET 10)** and follow the same project layout: a library project containing the implementation and a companion test project using **xUnit**. Open the solution file [`exercises.slnx`](exercises.slnx) in Visual Studio or Rider to load the project-based exercises at once. Some chapter 3 and chapter 5 exercises are intentionally lighter-weight workflow labs or standalone console apps or VSIX extensions instead of shipping a separate solution.

---

## 📋 Exercise Index

| # | Exercise | Chapter | Copilot Skill | Difficulty |
|---|----------|---------|---------------|------------|
| [101](chapter-01/exercise-101/README.md) | [Token Visualizer](chapter-01/exercise-101/README.md) | Chapter 1, Exercise 1 | Exploration / understanding tokens | ⭐⭐ |
| [102](chapter-01/exercise-102/README.md) | [Stateless LLM Chat](chapter-01/exercise-102/README.md) | Chapter 1, Exercise 2 | Building a first chat agent with Azure OpenAI | ⭐⭐ |
| [103](chapter-01/exercise-103/README.md) | [Chat History & Roles](chapter-01/exercise-103/README.md) | Chapter 1, Exercise 3 | Stateful chat and persona prompts | ⭐⭐ |
| [104](chapter-01/exercise-104/README.md) | [Tool Calls](chapter-01/exercise-104/README.md) | Chapter 1, Exercise 4 | In-process function calling | ⭐⭐ |
| [105](chapter-01/exercise-105/README.md) | [MCP Tool Calls](chapter-01/exercise-105/README.md) | Chapter 1, Exercise 5 | Out-of-process tools with MCP | ⭐⭐⭐ |
| [106](chapter-01/exercise-106/README.md) | [Agent Orchestration](chapter-01/exercise-106/README.md) | Chapter 1, Exercise 6 | Multi-agent routing and reply synthesis | ⭐⭐⭐ |
| [201](chapter-02/exercise-201/README.md) | [Factorial Calculator](chapter-02/exercise-201/README.md) | Chapter 2, Exercise 1 | Inline completions & autocomplete | ⭐ |
| [202](chapter-02/exercise-202/README.md) | [Palindrome Checker](chapter-02/exercise-202/README.md) | Chapter 2, Exercise 2 | Code generation with review | ⭐ |
| [203](chapter-02/exercise-203/README.md) | [Mystery Processor](chapter-02/exercise-203/README.md) | Chapter 2, Exercise 3 | Understanding obfuscated code | ⭐⭐⭐ |
| [204](chapter-02/exercise-204/README.md) | [Shortest Path](chapter-02/exercise-204/README.md) | Chapter 2, Exercise 4 | Bug detection with `/fix` | ⭐⭐⭐ |
| [205](chapter-02/exercise-205/README.md) | [Caesar Cipher](chapter-02/exercise-205/README.md) | Chapter 2, Exercise 5 | Test generation with `/tests` | ⭐⭐ |
| [206](chapter-02/exercise-206/README.md) | [String Search](chapter-02/exercise-206/README.md) | Chapter 2, Exercise 6 | Documentation & code review | ⭐⭐⭐ |
| [301](chapter-03/exercise-301/README.md) | [Who Does Copilot Picture?](chapter-03/exercise-301/README.md) | Chapter 3, Exercise 1 | Fairness; spotting implicit bias in generated output | ⭐⭐ |
| [302](chapter-03/exercise-302/README.md) | [Infix/Postfix with Ask Mode](chapter-03/exercise-302/README.md) | Chapter 3, Exercise 2 | Understanding and safely implementing unfamiliar code with Copilot Ask mode | ⭐⭐⭐ |
| [303](chapter-03/exercise-303/README.md) | [Malicious Repo Prompt Trap](chapter-03/exercise-303/README.md) | Chapter 3, Exercise 3 | Threat modelling AI-assisted setup flows in untrusted repositories | ⭐⭐ |
| [304](chapter-03/exercise-304/README.md) | [Code Obfuscator MCP Tool](chapter-03/exercise-304/README.md) | Chapter 3, Exercise 4 | Building an MCP tool; malicious MCP trust awareness | ⭐⭐⭐⭐ |
| [305](chapter-03/exercise-305/README.md) | [Tool Calls with Ollama](chapter-03/exercise-305/README.md) | Chapter 3, Exercise 5 | Local models and tool calling | ⭐⭐ |
| [401](chapter-04/exercise-401/README.md) | [Rename a Field with Agent Mode](chapter-04/exercise-401/README.md) | Chapter 4, Exercise 1 | Multi-file refactoring with Agent Mode | ⭐⭐ |
| [402](chapter-04/exercise-402/README.md) | [Add Input Validation](chapter-04/exercise-402/README.md) | Chapter 4, Exercise 2 | Adding input validation to minimal API endpoints with Agent Mode | ⭐⭐ |
| [403](chapter-04/exercise-403/README.md) | [Paginate, Filter and Sort](chapter-04/exercise-403/README.md) | Chapter 4, Exercise 3 | Using Plan Mode to design and implement a multi-part feature | ⭐⭐⭐ |
| [404](chapter-04/exercise-404/README.md) | [Vibe-Code a Slot Machine](chapter-04/exercise-404/README.md) | Chapter 4, Exercise 4 | Prompting Copilot CLI from guardrails to build a WinForms app | ⭐⭐ |
| [405](chapter-04/exercise-405/README.md) | [Explore Copilot on GitHub.com](chapter-04/exercise-405/README.md) | Chapter 4, Exercise 5 | Browser-first repo chat, issue triage, PR review, and online agent workflows | ⭐⭐ |
| [406](chapter-04/exercise-406/README.md) | [Create a Repository Instruction File](chapter-04/exercise-406/README.md) | Chapter 4, Exercise 6 | Teaching Copilot your conventions, stack, tests, and output preferences through repo instructions | ⭐ |
| [407](chapter-04/exercise-407/README.md) | [Experiment with Prompt Files and Skill Files](chapter-04/exercise-407/README.md) | Chapter 4, Exercise 7 | Comparing manual prompt-file workflows with auto-invoked SKILL.md processes | ⭐⭐ |
| [501](chapter-05/exercise-501/README.md) | [Context Window Copilot Clone](chapter-05/exercise-501/README.md) | Chapter 5, Exercise 1 | Comparing GPT-4o with no context, FIM context, and open tabs context in Visual Studio | ⭐⭐⭐ |
| [502](chapter-05/exercise-502/README.md) | [Prompt Arena](chapter-05/exercise-502/README.md) | Chapter 5, Exercise 2 | A deployable web game that scores prompts against the four-ingredient rubric | ⭐⭐⭐ |
| [503](chapter-05/exercise-503/README.md) | [Context Variables Playground](chapter-05/exercise-503/README.md) | Chapter 5, Exercise 3 | A prebuilt .NET app for comparing no variable, @workspace, and #file prompts | ⭐⭐ |
| [504](chapter-05/exercise-504/README.md) | [Prompt Pattern Playground](chapter-05/exercise-504/README.md) | Chapter 5, Exercise 4 | A prebuilt .NET app for trying comment-driven, test-first, persona, stepwise, and diff-driven prompts | ⭐⭐ |
| [601](chapter-06/exercise-601/README.md) | [Short URL Discovery Sprint](chapter-06/exercise-601/README.md) | Chapter 6, Exercise 1 | Analysis-first greenfield planning and implementation for a .NET 10 short-link service | ⭐⭐ |
| [602](chapter-06/exercise-602/README.md) | [Expression Evaluator Test Lab](chapter-06/exercise-602/README.md) | Chapter 6, Exercise 2 | Comparing tests on existing code, TDD, BDD, and coverage on a .NET 10 evaluator | ⭐⭐⭐ |
| [603](chapter-06/exercise-603/README.md) | [Optimize Edit Distance](chapter-06/exercise-603/README.md) | Chapter 6, Exercise 3 | Refactoring naive recursive Levenshtein distance with logs, metrics, tests, and BenchmarkDotNet | ⭐⭐⭐ |
| [604](chapter-06/exercise-604/README.md) | [Draw.io Playground with MCP and *.drawio.png](chapter-06/exercise-604/README.md) | Chapter 6, Exercise 4 | Draw.io, DrawIO MCP, and editable diagram artifacts in Markdown-friendly repos | ⭐⭐ |
| [605](chapter-06/exercise-605/README.md) | [Hunt the Cursed Theme Park Checkout Bug](chapter-06/exercise-605/README.md) | Chapter 6, Exercise 5 | Debugging multiple runtime exceptions with Playwright, logs, and browser evidence in a haunted .NET 10 checkout flow | ⭐⭐⭐ |
| [501](chapter-05/exercise-501/README.md) | [Context Window Copilot Clone](chapter-05/exercise-501/README.md) | Chapter 5, Exercise 1 | Comparing GPT-4o with no context, FIM context, and open tabs context in Visual Studio | ⭐⭐⭐ |

---

## 🗺️ Exercises by Chapter

| Chapter | Exercises |
|---------|-----------|
| Chapter 1 | [101 Token Visualizer](chapter-01/exercise-101/README.md) · [102 Stateless LLM Chat](chapter-01/exercise-102/README.md) · [103 Chat History & Roles](chapter-01/exercise-103/README.md) · [104 Tool Calls](chapter-01/exercise-104/README.md) · [105 MCP Tool Calls](chapter-01/exercise-105/README.md) · [106 Agent Orchestration](chapter-01/exercise-106/README.md) |
| Chapter 2 | [201 Factorial Calculator](chapter-02/exercise-201/README.md) · [202 Palindrome Checker](chapter-02/exercise-202/README.md) · [203 Mystery Processor](chapter-02/exercise-203/README.md) · [204 Shortest Path](chapter-02/exercise-204/README.md) · [205 Caesar Cipher](chapter-02/exercise-205/README.md) · [206 String Search](chapter-02/exercise-206/README.md) |
| Chapter 3 | [301 Who Does Copilot Picture?](chapter-03/exercise-301/README.md) · [302 Infix/Postfix with Ask Mode](chapter-03/exercise-302/README.md) · [303 Malicious Repo Prompt Trap](chapter-03/exercise-303/README.md) · [304 Code Obfuscator MCP Tool](chapter-03/exercise-304/README.md) · [305 Tool Calls with Ollama](chapter-03/exercise-305/README.md) |
| Chapter 4 | [401 Rename a Field with Agent Mode](chapter-04/exercise-401/README.md) · [402 Add Input Validation](chapter-04/exercise-402/README.md) · [403 Paginate, Filter and Sort](chapter-04/exercise-403/README.md) · [404 Vibe-Code a Slot Machine](chapter-04/exercise-404/README.md) · [405 Explore Copilot on GitHub.com](chapter-04/exercise-405/README.md) · [406 Create a Repository Instruction File](chapter-04/exercise-406/README.md) · [407 Experiment with Prompt Files and Skill Files](chapter-04/exercise-407/README.md) |
| Chapter 5 | [501 Context Window Copilot Clone](chapter-05/exercise-501/README.md) · [502 Prompt Arena](chapter-05/exercise-502/README.md) · [503 Context Variables Playground](chapter-05/exercise-503/README.md) · [504 Prompt Pattern Playground](chapter-05/exercise-504/README.md) |
| Chapter 6 | [601 Short URL Discovery Sprint](chapter-06/exercise-601/README.md) · [602 Expression Evaluator Test Lab](chapter-06/exercise-602/README.md) · [603 Optimize Edit Distance](chapter-06/exercise-603/README.md) · [604 Draw.io Playground with MCP and *.drawio.png](chapter-06/exercise-604/README.md) · [605 Hunt the Cursed Theme Park Checkout Bug](chapter-06/exercise-605/README.md) |
| Chapter 5 | [501 Context Window Copilot Clone](chapter-05/exercise-501/README.md) · [502 Prompt Arena](chapter-05/exercise-502/README.md) · [503 Context Variables Playground](chapter-05/exercise-503/README.md) · [504 Prompt Pattern Playground](chapter-05/exercise-504/README.md) |

---

## 🚀 Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/) with the C# Dev Kit
- GitHub Copilot subscription (Free, Pro, or Business)
- For Exercises 101 through 106: an Azure OpenAI resource with a deployed model
- For Exercise 305: [Ollama](https://ollama.com/) running locally with a downloaded model

### Running Any Exercise

```bash
# Open the full solution
cd exercises
dotnet build

# Run the tests for a specific exercise (e.g. 205)
cd chapter-02/exercise-205/CaesarCipher.Tests
dotnet test
```

---

## 📌 Tips for All Exercises

- **Keep Copilot enabled** — the exercises are designed to be done *with* Copilot, not despite it.
- **Read the exercise README first** — each README describes exactly what skill to practise and gives guidance on which Copilot features to reach for.
- **The tests are your guide** — run the tests early and often; green tests mean you are done.
- **Experiment** — if Copilot's first suggestion isn't right, reject it and try a more specific comment or prompt.

---

← Back to [Workshop Home](../README.md)
