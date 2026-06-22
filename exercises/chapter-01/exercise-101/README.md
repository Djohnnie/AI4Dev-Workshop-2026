# Exercise 101 — Token Visualizer

> **Chapter:** Chapter 1, Exercise 1  
> **Skill focus:** Understanding how LLMs tokenise text; exploring the Azure OpenAI API  
> **Difficulty:** ⭐⭐

← Back to [Exercise Index](../../README.md)

---

## 🎯 Overview

Before you can use AI coding tools effectively, it helps to understand what is actually happening inside a large language model when it reads your code or prompt. This exercise gives you a live, interactive view of one of the most fundamental concepts in LLMs: **tokenisation**.

You will run a console application that calls the Azure OpenAI API and the `Microsoft.ML.Tokenizers` library to show you, in real time:

- How any text you type is split into tokens
- How many tokens the text consumes
- How the token count relates to cost and context-window limits

---

## 📚 Background: What Is a Token?

LLMs do not read text character-by-character or word-by-word. They read **tokens** — chunks of text produced by a tokeniser. A tokeniser is trained alongside the model and learns to split text in a way that balances vocabulary size against coverage.

Some rough rules of thumb for GPT-family models:

| Text | Approximate tokens |
|------|--------------------|
| 1 average English word | ~1.3 tokens |
| `"Hello, world!"` | 4 tokens |
| A typical line of C# code | 5–15 tokens |
| 1,000 words of English prose | ~750 tokens |

Key implications for developers:

- **Context window limits** are measured in tokens, not words or characters. GPT-4o has a 128,000-token context window.
- **Cost** on pay-per-use APIs is billed per token (input + output).
- **Hallucination patterns** often occur at token boundaries — the model predicts the *next token*, not the next word.
- Code tokens differ from English tokens: identifiers, brackets, and operators each become their own tokens, so verbose code consumes more context than compact code.

The tokeniser used by GPT-4, GPT-4o, and GPT-4o-mini is called **cl100k_base** and has a vocabulary of ~100,000 tokens. The newer **o200k_base** (used by o-series models) has ~200,000.

---

## 🗂️ Project Structure

```
101/
├── Program.cs              ← Entry point; drives the interactive UI
├── TokenVisualizer.csproj  ← Project file (.NET 10, console app)
└── Properties/
    └── launchSettings.json ← Launch profiles for running with dotnet run
```

### Key Dependencies

| Package | Purpose |
|---------|---------|
| `Azure.AI.OpenAI` | Official Azure OpenAI SDK |
| `Microsoft.Extensions.AI.OpenAI` | Abstraction layer over the OpenAI SDK |
| `Microsoft.ML.Tokenizers` | Local, offline tokenisation matching the GPT-4/o family tokeniser |
| `Spectre.Console` | Rich terminal UI: colours, tables, panels |

---

## ✅ Your Task

This exercise is exploratory rather than implementation-focused. The application is already complete. Your job is to:

1. **Configure** the application by supplying your Azure OpenAI endpoint and API key (via environment variables or `launchSettings.json`).
2. **Run** the application and interact with it.
3. **Experiment** — paste in different kinds of text and observe the token counts:
   - Plain English sentences
   - C# code snippets
   - SQL queries
   - Long variable names vs. short ones
   - A prompt you might actually send to Copilot Chat
4. **Reflect** — use what you observe to answer the questions in the [Discussion Prompts](#-discussion-prompts) section below.

---

## 🚀 Getting Started

### 1. Set Your Azure OpenAI Credentials

The application reads its configuration from environment variables:

```bash
# Windows (PowerShell)
$env:AZURE_OPENAI_ENDPOINT = "https://<your-resource>.openai.azure.com/"
$env:AZURE_OPENAI_KEY      = "<your-api-key>"
$env:AZURE_OPENAI_DEPLOYMENT = "<your-deployment-name>"   # e.g. "gpt-4o"
```

Alternatively, edit `Properties/launchSettings.json` to add these as `environmentVariables`.

### 2. Run the Application

```bash
cd exercises/chapter-01/101
dotnet run
```

You will see a rich terminal UI powered by Spectre.Console. Type any text at the prompt and press **Enter** to see the token breakdown.

---

## 💡 Discussion Prompts

Use the visualiser to investigate these questions — they will come up during the Chapter 1 session:

1. Type the same sentence in English and then in another language (e.g. French or German). Does the token count differ? Why?
2. Type a long camelCase identifier like `GetUserAccountTransactionHistoryAsync`. How many tokens is it?
3. Type a typical Copilot Chat prompt (e.g. *"Write a C# method that reads a JSON file and returns a list of User objects"*). How many tokens does your prompt consume?
4. How many prompts like that could fit in a 128,000-token context window?
5. Type a block of minified JavaScript. Compare the token count with the same code formatted nicely. What does this imply for how you share code with Copilot?

---

## 🔗 Further Reading

- [OpenAI Tokeniser (browser tool)](https://platform.openai.com/tokenizer)
- [Azure OpenAI pricing](https://azure.microsoft.com/pricing/details/cognitive-services/openai-service/)
- [Microsoft.ML.Tokenizers on NuGet](https://www.nuget.org/packages/Microsoft.ML.Tokenizers)

---

← Back to [Exercise Index](../../README.md) | Next: [Exercise 102 — Stateless LLM Chat](../exercise-102/README.md)
