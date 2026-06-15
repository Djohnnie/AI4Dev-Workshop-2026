using Azure.AI.OpenAI;
using Microsoft.Agents.AI;
using OpenAI.Chat;
using System.ClientModel;

namespace ContextPromptExtension;

internal sealed record ChatServiceResult(string Response, int InputTokens, int OutputTokens);

internal sealed class AzureOpenAiChatService
{
    //private const string Endpoint = "https://your-resource.openai.azure.com/";
    //private const string Key = "your-api-key";
    //private const string Model = "gpt-5.4";

    // ↓↓↓ FILL IN: cost in USD per 1 million tokens for your model/deployment ↓↓↓
    // https://azure.microsoft.com/en-us/pricing/details/azure-openai/
    internal const decimal InputCostPer1MTokens = 2.16m;   // e.g. 2.50m for gpt-4o input
    internal const decimal OutputCostPer1MTokens = 12.91m;  // e.g. 10.00m for gpt-4o output
    // ↑↑↑ FILL IN ↑↑↑

    public async Task<ChatServiceResult> GetResponseAsync(string prompt, CancellationToken cancellationToken)
    {
        if (Endpoint.Contains("your-resource", StringComparison.OrdinalIgnoreCase) ||
            Key.Equals("your-api-key", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Update the hardcoded Azure OpenAI endpoint and key before running the extension.");
        }

        var client = new AzureOpenAIClient(new Uri(Endpoint), new ApiKeyCredential(Key));
        var chatClient = client.GetChatClient(Model);
        var agent = chatClient.AsAIAgent(name: "ContextPromptDemo", instructions: "You are a precise Visual Studio coding assistant.");
        var session = await agent.CreateSessionAsync();
        var response = await agent.RunAsync(prompt, session, new ChatClientAgentRunOptions(), cancellationToken);

        var inputTokens = (int)(response.Usage?.InputTokenCount ?? 0);
        var outputTokens = (int)(response.Usage?.OutputTokenCount ?? 0);

        return new ChatServiceResult(response.Text ?? response.ToString(), inputTokens, outputTokens);
    }
}
