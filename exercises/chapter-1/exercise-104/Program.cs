using Azure.AI.OpenAI;
using Microsoft.Extensions.AI;
using OpenAI.Chat;
using System.ClientModel;
using System.ComponentModel;

var endpoint = Environment.GetEnvironmentVariable("OPENAI_ENDPOINT") ?? string.Empty;
var key = Environment.GetEnvironmentVariable("OPENAI_KEY") ?? string.Empty;

var client = new AzureOpenAIClient(new Uri(endpoint), new ApiKeyCredential(key));
var chatClient = client.GetChatClient("gpt-4o");

// Define tools (functions) that the agent can call
var tools = new List<AITool>
{
    AIFunctionFactory.Create(GetTime),
    AIFunctionFactory.Create(GetDate),
};

var agentClient = chatClient.AsAIAgent(name: "ToolCalls", description: "An agent with tool calling capabilities", tools: tools);

var chatSession = await agentClient.CreateSessionAsync();

while (true)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("User > ");
    Console.ForegroundColor = ConsoleColor.White;
    var request = Console.ReadLine();

    Console.ForegroundColor = ConsoleColor.Cyan;

    var response = await agentClient.RunAsync(request!, chatSession);
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
