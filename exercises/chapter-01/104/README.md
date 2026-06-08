# Exercise 104 — Tool Calls

> **Chapter:** Chapter 1, Exercise 4  
> **Skill focus:** In-process tool calling, function descriptions, real-time data access  
> **Difficulty:** ⭐⭐

← Back to [Exercise Index](../../README.md)

---

## 🎯 Goal

Extend the chat agent so it can answer questions about the **current date and time** by calling local C# functions instead of guessing.

## 💡 Idea

Large language models are strong at language, but weak at live data. Tool calling solves that by letting the model invoke a function when it needs fresh information.

This exercise keeps the chat experience simple and adds two local tools:

- `GetTime()`
- `GetDate()`

The model decides when to call them, and then uses the returned values in its reply.

## 🗂️ Project Structure

```text
104/
├── AgentFramework.ToolCalls.csproj
├── Program.cs
└── Properties/
    └── launchSettings.json
```

## ✅ Your Task

1. Configure `OPENAI_ENDPOINT` and `OPENAI_KEY`.
2. Run the app:

   ```bash
   cd exercises/chapter-01/104
   dotnet run
   ```

3. Ask questions such as:
   - "What time is it?"
   - "What is today's date?"
   - "What day is tomorrow if today is ..."

## 🔍 What to Notice

- Tool calling gives the model access to **real-time values**.
- The function descriptions matter because they help the model decide which tool to use.
- The tools live in the same process as the chat app, so this is the simplest possible tool-calling setup.

---

← Back to [Exercise Index](../../README.md) | Previous: [Exercise 103](../103/README.md) | Next: [Exercise 105](../105/README.md)
