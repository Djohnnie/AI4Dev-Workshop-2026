using Microsoft.Extensions.AI;
using OllamaSharp;
using System.ComponentModel;

var endpoint = Environment.GetEnvironmentVariable("OLLAMA_ENDPOINT") ?? "http://localhost:11434";
var model = Environment.GetEnvironmentVariable("OLLAMA_MODEL") ?? "qwen3-coder:30b";

IChatClient chatClient = new OllamaApiClient(new Uri(endpoint), model);

var tools = new List<AITool>
{
    AIFunctionFactory.Create(GetTime),
    AIFunctionFactory.Create(GetDate),
};

var agentClient = chatClient.AsAIAgent(
    name: "ToolCallsWithOllama",
    description: "An agent with tool calling capabilities backed by a local Ollama model",
    tools: tools);

var chatSession = await agentClient.CreateSessionAsync();

while (true)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("User > ");
    Console.ForegroundColor = ConsoleColor.White;
    var request = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(request))
    {
        continue;
    }

    Console.ForegroundColor = ConsoleColor.Cyan;

    var response = await agentClient.RunAsync(request, chatSession);
    foreach (var message in response.Messages)
    {
        if (!string.IsNullOrWhiteSpace(message.Text))
        {
            Console.Write("Assistant > ");
            Console.WriteLine(message.Text);
        }
    }

    Console.WriteLine();
}

[Description("Gets the current time.")]
static TimeSpan GetTime()
{
    return TimeProvider.System.GetLocalNow().TimeOfDay;
}

[Description("Gets the current date.")]
static DateTime GetDate()
{
    return TimeProvider.System.GetLocalNow().Date;
}
