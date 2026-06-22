# Exercise 105 — MCP Tool Calls

> **Chapter:** Chapter 1, Exercise 5  
> **Skill focus:** MCP, client/server tool boundaries, remote tool discovery  
> **Difficulty:** ⭐⭐⭐

← Back to [Exercise Index](../../README.md)

---

## 🎯 Goal

Move the date and time tools out of the chat process and expose them through an **MCP server**. The chat client then discovers those tools over HTTP and uses them remotely.

## 💡 Idea

Exercise 104 showed in-process tools. This exercise introduces the next step: **Model Context Protocol (MCP)** as a reusable boundary between an AI app and external tools.

The setup has two parts:

- **Server:** hosts the clock tools over HTTP
- **Client:** connects to the MCP endpoint and gives those tools to the agent

That makes the tools reusable by other apps without copying their implementation into every agent.

## 🗂️ Project Structure

```text
105/
├── AgentFramework.McpSseServer/
│   ├── Program.cs
│   └── Properties/
│       └── launchSettings.json
└── AgentFramework.McpSseClient/
    ├── Program.cs
    └── Properties/
        └── launchSettings.json
```

## ✅ Your Task

1. Start the MCP server in one terminal:

   ```bash
   cd exercises/chapter-01/exercise-105/AgentFramework.McpSseServer
   dotnet run
   ```

2. In a second terminal, configure `OPENAI_ENDPOINT` and `OPENAI_KEY`, then start the client:

   ```bash
   cd exercises/chapter-01/exercise-105/AgentFramework.McpSseClient
   dotnet run
   ```

3. Ask the client for the current date or time.

## 🔍 What to Notice

- The client connects to `https://localhost:54723/`, so the server must be running first.
- The client lists the available MCP tools before entering the chat loop.
- The agent still feels like a normal assistant, but its tools now live in a separate process.

---

← Back to [Exercise Index](../../README.md) | Previous: [Exercise 104](../exercise-104/README.md) | Next: [Exercise 106](../exercise-106/README.md)
