# Exercise 103 — Chat History & Roles

> **Chapter:** Chapter 1, Exercise 3  
> **Skill focus:** Stateful chat, conversation history, system prompts and personas  
> **Difficulty:** ⭐⭐

← Back to [Exercise Index](../../README.md)

---

## 🎯 Goal

Turn the simple chat agent from Exercise 102 into a **stateful assistant** that remembers earlier messages and changes its behaviour based on system prompts.

## 💡 Idea

This exercise introduces two core ideas behind useful AI apps:

- **Chat history** keeps earlier user and assistant messages available to the model
- **Roles and persona prompts** steer how the assistant should answer

In the sample app, the agent starts with a child-like persona, remembers the conversation, and updates its role prompt after every turn. That makes it easy to see how history and system messages shape later replies.

## 🗂️ Project Structure

```text
exercise-103/
├── AgentFramework.ChatHistoryAndPersona.csproj
├── Program.cs
└── Properties/
    └── launchSettings.json
```

## ✅ Your Task

1. Configure `OPENAI_ENDPOINT` and `OPENAI_KEY`.
2. Run the console app:

   ```bash
   cd exercises/chapter-01/exercise-103
   dotnet run
   ```

3. Ask multiple follow-up questions and observe:
   - the model remembers earlier turns
   - the persona is injected as a system message
   - the persona changes after each answer

## 🔍 What to Notice

- A session is created once and reused, so the conversation is no longer stateless.
- User messages are added to in-memory history before the next model call.
- System prompts are not just setup text — they actively steer tone, role, and behaviour.

---

← Back to [Exercise Index](../../README.md) | Previous: [Exercise 102](../exercise-102/README.md) | Next: [Exercise 104](../exercise-104/README.md)
