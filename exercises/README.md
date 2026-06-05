# 🏋️ Exercises

Welcome to the hands-on exercise collection for the **AI for Developers Workshop 2026**. Each exercise is paired with a workshop chapter and gives you a focused, self-contained coding challenge designed to practise a specific GitHub Copilot skill or AI-assisted development technique.

All exercises are written in **C# (.NET 10)** and follow the same project layout: a library project containing the implementation and a companion test project using **xUnit**. Open the solution file [`exercises.slnx`](exercises.slnx) in Visual Studio or Rider to load all projects at once.

---

## 📋 Exercise Index

| # | Exercise | Chapter | Copilot Skill | Difficulty |
|---|----------|---------|---------------|------------|
| [101](101/README.md) | [Token Visualizer](101/README.md) | Chapter 1, Exercise 1 | Exploration / understanding tokens | ⭐⭐ |
| [102](102/README.md) | [Stateless LLM Chat](102/README.md) | Chapter 1, Exercise 2 | Building a first chat agent with Azure OpenAI | ⭐⭐ |
| [103](103/README.md) | [Chat History & Roles](103/README.md) | Chapter 1, Exercise 3 | Stateful chat and persona prompts | ⭐⭐ |
| [104](104/README.md) | [Tool Calls](104/README.md) | Chapter 1, Exercise 4 | In-process function calling | ⭐⭐ |
| [105](105/README.md) | [MCP Tool Calls](105/README.md) | Chapter 1, Exercise 5 | Out-of-process tools with MCP | ⭐⭐⭐ |
| [106](106/README.md) | [Agent Orchestration](106/README.md) | Chapter 1, Exercise 6 | Multi-agent routing and reply synthesis | ⭐⭐⭐ |
| [201](201/README.md) | [Factorial Calculator](201/README.md) | Chapter 2, Exercise 1 | Inline completions & autocomplete | ⭐ |
| [202](202/README.md) | [Palindrome Checker](202/README.md) | Chapter 2, Exercise 2 | Code generation with review | ⭐ |
| [203](203/README.md) | [Mystery Processor](203/README.md) | Chapter 2, Exercise 3 | Understanding obfuscated code | ⭐⭐⭐ |
| [204](204/README.md) | [Shortest Path](204/README.md) | Chapter 2, Exercise 4 | Bug detection with `/fix` | ⭐⭐⭐ |
| [205](205/README.md) | [Caesar Cipher](205/README.md) | Chapter 2, Exercise 5 | Test generation with `/tests` | ⭐⭐ |
| [206](206/README.md) | [String Search](206/README.md) | Chapter 2, Exercise 6 | Documentation & code review | ⭐⭐⭐ |
| [310](310/README.md) | [Code Obfuscator MCP Tool](310/README.md) | Chapter 3, Exercise 10 | Building an MCP tool | ⭐⭐⭐⭐ |

---

## 🗺️ Exercises by Chapter

| Chapter | Exercises |
|---------|-----------|
| Chapter 1 | [101 Token Visualizer](101/README.md) · [102 Stateless LLM Chat](102/README.md) · [103 Chat History & Roles](103/README.md) · [104 Tool Calls](104/README.md) · [105 MCP Tool Calls](105/README.md) · [106 Agent Orchestration](106/README.md) |
| Chapter 2 | [201 Factorial Calculator](201/README.md) · [202 Palindrome Checker](202/README.md) · [203 Mystery Processor](203/README.md) · [204 Shortest Path](204/README.md) · [205 Caesar Cipher](205/README.md) · [206 String Search](206/README.md) |
| Chapter 3 | [310 Code Obfuscator MCP Tool](310/README.md) |

---

## 🚀 Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/) with the C# Dev Kit
- GitHub Copilot subscription (Free, Pro, or Business)
- For Exercises 101 through 106: an Azure OpenAI resource with a deployed model

### Running Any Exercise

```bash
# Open the full solution
cd exercises
dotnet build

# Run the tests for a specific exercise (e.g. 205)
cd 205/CaesarCipher.Tests
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
