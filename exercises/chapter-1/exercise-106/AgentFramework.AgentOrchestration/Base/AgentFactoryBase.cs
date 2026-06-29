using AgentOrchestration.Agents;
using Azure.AI.OpenAI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using ModelContextProtocol.Client;
using System.ClientModel;

namespace AgentOrchestration.Base;

internal abstract class AgentFactoryBase
{
    private readonly IConfiguration _configuration;
    private McpClient? _mcpClient;

    protected virtual string AgentName => "AGENT_NAME";
    public virtual string AgentDescription => "AGENT_DESCRIPTION";
    protected virtual string AgentInstruction => "AGENT_INSTRUCTIONS";
    protected virtual bool HasPlugin => false;
    protected virtual string PluginName => "PLUGIN_NAME";
    protected virtual string McpName => "MCP_NAME";
    protected virtual string McpEndpointConfig => "MCP_ENDPOINT";
    protected virtual string McpToolPrefix => string.Empty;

    protected AgentFactoryBase(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<CopilotAgent> Create()
    {
        var deployment = _configuration["AZUREOPENAI_DEPLOYMENT"] ?? "gpt-4o";
        var endpoint = _configuration["AZUREOPENAI_ENDPOINT"] ?? string.Empty;
        var key = _configuration["AZUREOPENAI_KEY"] ?? string.Empty;

        var tools = new List<AITool>();

        if (HasPlugin)
        {
            var mcpTools = await InitializeTools();
            tools.AddRange(mcpTools.Cast<AITool>());
        }

        var client = new AzureOpenAIClient(new Uri(endpoint), new ApiKeyCredential(key));
        var chatClient = client.GetChatClient(deployment).AsIChatClient();
        var agentClient = chatClient.AsAIAgent(
            name: AgentName, description: AgentDescription, instructions: AgentInstruction,
            tools: tools);

        return new CopilotAgent(agentClient);
    }

    protected virtual async Task InitializeMcpClient()
    {
        var endpoint = _configuration[McpEndpointConfig];

        if (!string.IsNullOrWhiteSpace(endpoint))
        {
            _mcpClient = await McpClient.CreateAsync(
                new HttpClientTransport(new()
                {
                    Name = McpName,
                    Endpoint = new Uri(endpoint)
                }));
        }
    }

    protected virtual async ValueTask<IList<McpClientTool>> InitializeTools()
    {
        var endpoint = _configuration[McpEndpointConfig];
        if (string.IsNullOrWhiteSpace(endpoint))
        {
            return new List<McpClientTool>();
        }

        await InitializeMcpClient();
        if (_mcpClient == null) return new List<McpClientTool>();

        var tools = await _mcpClient.ListToolsAsync();
        return tools
            .Where(x => string.IsNullOrEmpty(McpToolPrefix) || x.Name.StartsWith(McpToolPrefix))
            .ToList();
    }
}
