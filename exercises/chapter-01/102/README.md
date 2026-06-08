# Exercise 102 — Stateless LLM Chat

> **Chapter:** Chapter 1, Exercise 2  
> **Skill focus:** Building a stateless chat agent with Microsoft Agent Framework and Azure OpenAI  
> **Difficulty:** ⭐⭐

← Back to [Exercise Index](../../README.md)

---

## 🎯 Overview

In this exercise, you will build a **console-based stateless AI chat agent** using the **Microsoft Agent Framework** and **Azure OpenAI**. Each prompt is handled on its own, which makes this the cleanest starting point before you add memory, roles, tools, and orchestration in later exercises.

The exercise is based on the `AgentFramework.Chat` project from the [EnableGenAIUsingTheMicrosoftAgentFramework-CronosDotnetCommunity-2026](https://github.com/Djohnnie/EnableGenAIUsingTheMicrosoftAgentFramework-CronosDotnetCommunity-2026) repository.

---

## 📚 Background: Microsoft Agent Framework

The **Microsoft Agent Framework** is a set of libraries and tools that enable developers to build AI agents in .NET applications. It provides:

- **Agent capabilities**: Convert chat clients into AI agents with `AsAIAgent()`
- **Streaming support**: Real-time processing of AI responses
- **Integration with Azure OpenAI**: Seamless connection to Azure OpenAI models
- **Extensible architecture**: Easy to add more complex agent behaviors

### Key Components Used

- `AzureOpenAIClient`: Client for connecting to Azure OpenAI
- `ChatClient`: Provides chat completion capabilities
- `AsAIAgent()`: Extends chat client to act as an AI agent
- `RunStreamingAsync()`: Streams responses for real-time interaction

---

## 🗂️ Project Structure

```
102/
├── AgentFramework.Chat.csproj    ← .NET 10 Console application project
└── Program.cs                   ← Main chat agent implementation
```

### `Program.cs`

The main program creates an AI chat agent that:
1. Reads user input from the console
2. Sends it to the Azure OpenAI model (gpt-4o)
3. Streams the response back to the console in real-time

### `AgentFramework.Chat.csproj`

The project references three key NuGet packages:
- `Microsoft.Extensions.AI` (v10.3.0) - Core AI extensions
- `Microsoft.Agents.AI.OpenAI` (v1.0.0-rc3) - Agent framework for OpenAI
- `Azure.AI.OpenAI` (v2.8.0-beta.1) - Azure OpenAI client library

---

## ✅ Your Task

1. **Set up Azure OpenAI environment variables:**
   - `OPENAI_ENDPOINT`: Your Azure OpenAI endpoint URL
   - `OPENAI_KEY`: Your Azure OpenAI API key

2. **Build and run the application:**
   ```bash
   cd exercises/chapter-01/102
   dotnet build
   dotnet run
   ```

3. **Test the chat agent:**
   - Type messages in the console
   - Observe the real-time streaming responses
   - The prompt shows "User > " and responses show "Assistant > "

---

## 📋 Prerequisites

### Azure OpenAI Setup

1. Create an **Azure OpenAI resource** in your Azure subscription
2. Deploy a **gpt-4o** model (or update the code for a different model)
3. Obtain your:
   - **Endpoint URL** (e.g., `https://your-resource.openai.azure.com/`)
   - **API Key** from the Azure portal

### Environment Configuration

Set the environment variables before running:

**Windows (PowerShell):**
```powershell
$env:OPENAI_ENDPOINT = "https://your-resource.openai.azure.com/"
$env:OPENAI_KEY = "your-api-key"
```

**Windows (CMD):**
```cmd
set OPENAI_ENDPOINT=https://your-resource.openai.azure.com/
set OPENAI_KEY=your-api-key
```

**Cross-platform (.NET User Secrets):**
```bash
cd exercises/chapter-01/102
dotnet user-secrets init
dotnet user-secrets set "OPENAI_ENDPOINT" "https://your-resource.openai.azure.com/"
dotnet user-secrets set "OPENAI_KEY" "your-api-key"
```

---

## 🎨 Code Walkthrough

### Initializing the Azure OpenAI Client

```csharp
var endpoint = Environment.GetEnvironmentVariable("OPENAI_ENDPOINT") ?? string.Empty;
var key = Environment.GetEnvironmentVariable("OPENAI_KEY") ?? string.Empty;

var client = new AzureOpenAIClient(new Uri(endpoint), new ApiKeyCredential(key));
```

This creates the base client for connecting to Azure OpenAI using environment variables for configuration.

### Creating the Chat Client and Agent

```csharp
var chatClient = client.GetChatClient("gpt-4o");
var agentClient = chatClient.AsAIAgent(name: "Chat", description: "Just a chat");
```

- `GetChatClient("gpt-4o")` creates a chat client targeting the gpt-4o model
- `AsAIAgent()` transforms the chat client into an AI agent with a name and description

### The Chat Loop

```csharp
while (true)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("User > ");
    Console.ForegroundColor = ConsoleColor.White;
    var request = Console.ReadLine();

    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.Write("Assistant > ");

    await foreach (var response in agentClient.RunStreamingAsync(request!))
    {
        Console.Write(response.Text);
    }

    Console.WriteLine();
}
```

This provides:
- **Color-coded input/output**: Green for user input, Cyan for assistant responses
- **Streaming**: Uses `await foreach` to process responses as they arrive
- **Continuous conversation**: Infinite loop for ongoing chat

---

## 🤖 AI Skills to Practise

| Skill | Description |
|-------|-------------|
| Agent Framework Basics | Understanding how to create AI agents from chat clients |
| Environment Configuration | Setting up and using environment variables for API secrets |
| Streaming Responses | Processing AI responses in real-time as they're generated |
| Azure OpenAI Integration | Connecting .NET applications to Azure OpenAI services |

---

## 🏁 Stretch Goals

Once the basic chat agent works, try these enhancements:

1. **Add conversation history**: Store previous messages to provide context
2. **Custom system prompt**: Configure the agent with a specific personality or role
3. **Error handling**: Add try-catch blocks for robust error management
4. **Exit command**: Add a command like "/quit" or "/exit" to gracefully end the chat
5. **Model selection**: Allow the user to choose between different models at runtime
6. **Response formatting**: Add timestamps or message formatting to the chat output

---

## 💡 Tips

- **Environment variables are required**: The application will not work without `OPENAI_ENDPOINT` and `OPENAI_KEY`
- **Check model availability**: Ensure gpt-4o is deployed in your Azure OpenAI resource
- **Monitor costs**: Remember that Azure OpenAI calls incur costs
- **Network connectivity**: Ensure your application can reach the Azure OpenAI endpoint

---

## 📖 Additional Resources

- [Microsoft Agent Framework Documentation](https://github.com/microsoft/ai-agent-framework)
- [Azure OpenAI Documentation](https://learn.microsoft.com/en-us/azure/ai-services/openai/)
- [Microsoft.Agents.AI.OpenAI NuGet Package](https://www.nuget.org/packages/Microsoft.Agents.AI.OpenAI)

---

← Back to [Exercise Index](../../README.md) | Previous: [Exercise 101](../101/README.md) | Next: [Exercise 103](../103/README.md)