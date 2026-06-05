# Exercise 301 -- Tool Calls with Ollama

> **Chapter:** Chapter 3, Exercise 1  
> **Skill focus:** Local models, tool calling, comparing hosted vs local inference  
> **Difficulty:** ⭐⭐

← Back to [Exercise Index](../README.md)

---

## Goal

Recreate the idea from [Exercise 104](../104/README.md), but swap the hosted model for a **local Ollama model** running on your own machine.

The point is not to build a different app. The point is to prove that **tool calling is an application pattern** that works with different model backends.

---

## Idea

In Exercise 104, the model could answer questions about the current date and time by calling local C# functions such as:

- `GetTime()`
- `GetDate()`

In this exercise, you keep the same tool functions and the same chat flow, but replace the model connection with a local Ollama endpoint.

That lets you compare:

- hosted vs local inference
- convenience vs control
- model quality vs privacy posture

---

## Project structure

```text
301/
├── AgentFramework.ToolCalls.Ollama.csproj
├── Program.cs
└── Properties/
    └── launchSettings.json
```

---

## Suggested setup

1. Start from the code in [Exercise 104](../104/README.md).
2. Install and run Ollama locally.
3. Pull a code-capable model, for example:

   ```bash
   ollama pull qwen3-coder:30b
   ```

4. Replace the hosted model client with calls to the local Ollama API.
5. Keep the tool-calling logic and prompts as close to Exercise 104 as possible.

---

## Your task

1. Configure `OLLAMA_ENDPOINT` and `OLLAMA_MODEL`.
2. Run a local Ollama model on your machine.
3. Run the app:

   ```bash
   cd exercises/301
   dotnet run
   ```

4. Ask the same questions you used in Exercise 104:
   - "What time is it?"
   - "What is today's date?"
   - "What day is tomorrow if today is ..."
5. Compare the experience with the hosted version.

---

## What to notice

- The model backend changed, but the tool-calling pattern did not.
- Local models can improve control over where inference happens.
- Local models may have weaker reasoning, smaller context windows, or more setup friction than hosted frontier models.
- Privacy is improved only if the full stack stays local and you avoid sending data elsewhere.

---

## Debrief questions

- What did you gain by running the model locally?
- What did you lose compared with the hosted version?
- Would you choose this approach for demos, regulated environments, or everyday coding?

---

← Back to [Exercise Index](../README.md) | Related: [Exercise 104](../104/README.md) | Next: [Exercise 310](../310/README.md)
