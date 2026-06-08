# 🏋️ Exercises

Welcome to the hands-on exercise collection for the **AI for Developers Workshop 2026**. Each exercise is paired with a workshop chapter and gives you a focused, self-contained coding challenge designed to practise a specific GitHub Copilot skill or AI-assisted development technique.

Most exercises are written in **C# (.NET 10)** and follow the same project layout: a library project containing the implementation and a companion test project using **xUnit**. Open the solution file [`exercises.slnx`](exercises.slnx) in Visual Studio or Rider to load the project-based exercises at once. Some chapter 3 exercises are intentionally lighter-weight workflow labs that start from an earlier exercise instead of shipping a separate solution.

---

## 📋 Exercise Index

| # | Exercise | Chapter | Copilot Skill | Difficulty |
|---|----------|---------|---------------|------------|
| [101](chapter-01/101/README.md) | [Token Visualizer](chapter-01/101/README.md) | Chapter 1, Exercise 1 | Exploration / understanding tokens | ⭐⭐ |
| [102](chapter-01/102/README.md) | [Stateless LLM Chat](chapter-01/102/README.md) | Chapter 1, Exercise 2 | Building a first chat agent with Azure OpenAI | ⭐⭐ |
| [103](chapter-01/103/README.md) | [Chat History & Roles](chapter-01/103/README.md) | Chapter 1, Exercise 3 | Stateful chat and persona prompts | ⭐⭐ |
| [104](chapter-01/104/README.md) | [Tool Calls](chapter-01/104/README.md) | Chapter 1, Exercise 4 | In-process function calling | ⭐⭐ |
| [105](chapter-01/105/README.md) | [MCP Tool Calls](chapter-01/105/README.md) | Chapter 1, Exercise 5 | Out-of-process tools with MCP | ⭐⭐⭐ |
| [106](chapter-01/106/README.md) | [Agent Orchestration](chapter-01/106/README.md) | Chapter 1, Exercise 6 | Multi-agent routing and reply synthesis | ⭐⭐⭐ |
| [201](chapter-02/201/README.md) | [Factorial Calculator](chapter-02/201/README.md) | Chapter 2, Exercise 1 | Inline completions & autocomplete | ⭐ |
| [202](chapter-02/202/README.md) | [Palindrome Checker](chapter-02/202/README.md) | Chapter 2, Exercise 2 | Code generation with review | ⭐ |
| [203](chapter-02/203/README.md) | [Mystery Processor](chapter-02/203/README.md) | Chapter 2, Exercise 3 | Understanding obfuscated code | ⭐⭐⭐ |
| [204](chapter-02/204/README.md) | [Shortest Path](chapter-02/204/README.md) | Chapter 2, Exercise 4 | Bug detection with `/fix` | ⭐⭐⭐ |
| [205](chapter-02/205/README.md) | [Caesar Cipher](chapter-02/205/README.md) | Chapter 2, Exercise 5 | Test generation with `/tests` | ⭐⭐ |
| [206](chapter-02/206/README.md) | [String Search](chapter-02/206/README.md) | Chapter 2, Exercise 6 | Documentation & code review | ⭐⭐⭐ |
| [301](chapter-03/301/README.md) | [Who Does Copilot Picture?](chapter-03/301/README.md) | Chapter 3, Exercise 1 | Fairness; spotting implicit bias in generated output | ⭐⭐ |
| [302](chapter-03/302/README.md) | [Tool Calls with Ollama](chapter-03/302/README.md) | Chapter 3, Exercise 2 | Local models and tool calling | ⭐⭐ |
| [303](chapter-03/303/README.md) | [Code Obfuscator MCP Tool](chapter-03/303/README.md) | Chapter 3, Exercise 3 | Building an MCP tool | ⭐⭐⭐⭐ |

---

## 🗺️ Exercises by Chapter

| Chapter | Exercises |
|---------|-----------|
| Chapter 1 | [101 Token Visualizer](chapter-01/101/README.md) · [102 Stateless LLM Chat](chapter-01/102/README.md) · [103 Chat History & Roles](chapter-01/103/README.md) · [104 Tool Calls](chapter-01/104/README.md) · [105 MCP Tool Calls](chapter-01/105/README.md) · [106 Agent Orchestration](chapter-01/106/README.md) |
| Chapter 2 | [201 Factorial Calculator](chapter-02/201/README.md) · [202 Palindrome Checker](chapter-02/202/README.md) · [203 Mystery Processor](chapter-02/203/README.md) · [204 Shortest Path](chapter-02/204/README.md) · [205 Caesar Cipher](chapter-02/205/README.md) · [206 String Search](chapter-02/206/README.md) |
| Chapter 3 | [301 Who Does Copilot Picture?](chapter-03/301/README.md) · [302 Tool Calls with Ollama](chapter-03/302/README.md) · [303 Code Obfuscator MCP Tool](chapter-03/303/README.md) |

---

## 🚀 Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/) with the C# Dev Kit
- GitHub Copilot subscription (Free, Pro, or Business)
- For Exercises 101 through 106: an Azure OpenAI resource with a deployed model
- For Exercise 302: [Ollama](https://ollama.com/) running locally with a downloaded model

### Running Any Exercise

```bash
# Open the full solution
cd exercises
dotnet build

# Run the tests for a specific exercise (e.g. 205)
cd chapter-02/205/CaesarCipher.Tests
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
