# Exercise 106 — Agent Orchestration

> **Chapter:** Chapter 1, Exercise 6  
> **Skill focus:** Multi-agent orchestration, routing, parallel fan-out, merged replies  
> **Difficulty:** ⭐⭐⭐

← Back to [Exercise Index](../../README.md)

---

## 🎯 Goal

Build a **multi-agent assistant** that takes one user question, routes it to the right specialist agents, and combines their answers into a single response.

## 💡 Idea

This exercise moves beyond a single chat agent and demonstrates a simple orchestration pipeline:

1. A **summary agent** restates the latest question with useful context
2. An **orchestrator agent** decides which specialists should handle it
3. The selected agents run in parallel
4. A **reply agent** combines those results into one final answer

The project also supports `/debug`, which makes the internal agent-to-agent flow visible in the console.

## 🗂️ Project Structure

```text
106/
└── AgentFramework.AgentOrchestration/
    ├── Agents/
    ├── Base/
    ├── Model/
    ├── Orchestration/
    ├── Program.cs
    └── Properties/
```

## ✅ Your Task

1. Configure Azure OpenAI access:

   - `AZUREOPENAI_ENDPOINT`
   - `AZUREOPENAI_KEY`
   - `AZUREOPENAI_DEPLOYMENT` (optional, defaults to `gpt-4o`)

2. Configure the MCP endpoints used by the specialist agents for a full demo:

   - `MIJNTHUIS_MCP`
   - `MIJNSAUNA_MCP`
   - `PHOTOCAROUSEL_MCP`

3. Run the app:

   ```bash
   cd exercises/chapter-01/exercise-106/AgentFramework.AgentOrchestration
   dotnet run
   ```

4. Try `/debug`, then ask compound questions that span multiple domains.

## 🔍 What to Notice

- Routing and reply synthesis are separate responsibilities.
- Specialized agents only receive the part of the question that matters to them.
- The final user experience is one answer, even though multiple agents may have contributed behind the scenes.

---

← Back to [Exercise Index](../../README.md) | Previous: [Exercise 105](../exercise-105/README.md) | Next: [Exercise 201](../../chapter-02/exercise-201/README.md)
