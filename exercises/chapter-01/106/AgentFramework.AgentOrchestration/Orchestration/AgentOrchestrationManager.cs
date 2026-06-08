using AgentOrchestration.Agents;
using AgentOrchestration.Base;
using AgentOrchestration.Model;

namespace AgentOrchestration.Orchestration;

internal class AgentOrchestrationManager
{
    private readonly SummaryAgentFactory _summaryAgentFactory;
    private readonly OrchestratorAgentFactory _orchestratorAgentFactory;
    private readonly ReplyAgentFactory _replyAgentFactory;
    private readonly Dictionary<string, AgentFactoryBase> _agents;

    public AgentOrchestrationManager(
        SummaryAgentFactory summaryAgentFactory,
        OrchestratorAgentFactory orchestratorAgentFactory,
        ReplyAgentFactory replyAgentFactory,
        GeneralAgentFactory generalAgentFactory,
        MijnThuisPowerAgentFactory mijnThuisPowerAgentFactory,
        MijnThuisSolarAgentFactory mijnThuisSolarAgentFactory,
        MijnThuisCarAgentFactory mijnThuisCarAgentFactory,
        MijnThuisHeatingAgentFactory mijnThuisHeatingAgentFactory,
        MijnThuisSmartLockAgentFactory mijnThuisSmartLockAgentFactory,
        MijnSaunaAgentFactory mijnSaunaAgentFactory,
        PhotoCarouselAgentFactory photoCarouselAgentFactory)
    {
        _summaryAgentFactory = summaryAgentFactory;
        _orchestratorAgentFactory = orchestratorAgentFactory;
        _replyAgentFactory = replyAgentFactory;

        _agents = new Dictionary<string, AgentFactoryBase>(StringComparer.OrdinalIgnoreCase)
        {
            { "General", generalAgentFactory },
            { "MijnThuisPower", mijnThuisPowerAgentFactory },
            { "MijnThuisSolar", mijnThuisSolarAgentFactory },
            { "MijnThuisCar", mijnThuisCarAgentFactory },
            { "MijnThuisHeating", mijnThuisHeatingAgentFactory },
            { "MijnThuisSmartLock", mijnThuisSmartLockAgentFactory },
            { "MijnSauna", mijnSaunaAgentFactory },
            { "PhotoCarousel", photoCarouselAgentFactory }
        };
    }

    public async Task<CopilotChatHistory> Chat(CopilotChatHistory chat)
    {
        var workingChat = chat.Copy();

        // Step 1: Summary agent refines the question
        var summaryAgent = await _summaryAgentFactory.Create();
        var summaryResponse = await summaryAgent.Chat(workingChat);
        workingChat.AddDebug(isQuestion: true, summaryResponse.Response, "SummaryAgent");

        // Step 2: Orchestrator agent determines which agents to call
        var orchestratorPrompt = BuildOrchestratorPrompt();
        var orchestratorChatHistory = new CopilotChatHistory();
        orchestratorChatHistory.AddSystemMessage(orchestratorPrompt);
        orchestratorChatHistory.AddUserMessage(summaryResponse.Response);

        var orchestratorAgent = await _orchestratorAgentFactory.Create();
        var orchestratorResponse = await orchestratorAgent.Chat(orchestratorChatHistory);
        workingChat.AddDebug(isQuestion: false, orchestratorResponse.Response, "OrchestratorAgent");

        // Step 3: Parse orchestrator output and call specialized agents in parallel
        var agentTasks = ParseOrchestratorResponse(orchestratorResponse.Response);

        var agentResponses = new List<(string Question, CopilotAgentResponse Response)>();

        if (agentTasks.Count > 0)
        {
            var tasks = agentTasks.Select(async task =>
            {
                if (!_agents.TryGetValue(task.AgentKey, out var factory))
                    factory = _agents["General"];

                var agent = await factory.Create();
                var agentChatHistory = new CopilotChatHistory(task.Question, CopilotChatRole.User);
                var response = await agent.Chat(agentChatHistory);
                return (task.Question, response);
            }).ToList();

            var results = await Task.WhenAll(tasks);
            agentResponses.AddRange(results);
        }

        foreach (var (question, response) in agentResponses)
        {
            workingChat.AddDebug(isQuestion: true, question, response.AgentName);
            workingChat.AddDebug(isQuestion: false, response.Response, response.AgentName);
        }

        // Step 4: Reply agent combines all responses into a single final answer
        var replyChatHistory = new CopilotChatHistory();
        replyChatHistory.AddUserMessage(summaryResponse.Response);

        foreach (var (_, response) in agentResponses)
        {
            replyChatHistory.AddAssistantMessage(response.Response);
        }

        var replyAgent = await _replyAgentFactory.Create();
        var replyResponse = await replyAgent.Chat(replyChatHistory);

        workingChat.AddAssistantMessage(replyResponse.Response);
        workingChat.LastAssistantMessage = replyResponse.Response;
        workingChat.AgentName = replyResponse.AgentName;
        workingChat.ContributingAgents.AddRange(agentResponses.Select(r => r.Response.AgentName));
        workingChat.InputTokenCount = replyResponse.InputTokenCount;
        workingChat.OutputTokenCount = replyResponse.OutputTokenCount;

        return workingChat;
    }

    private string BuildOrchestratorPrompt()
    {
        var agentDescriptions = string.Join(Environment.NewLine,
            _agents.Select(kvp => $"Name: {kvp.Key}; Description: {kvp.Value.AgentDescription}"));

        return $@"
You have access to the following agents:
{agentDescriptions}

For the question below, determine which agent(s) should answer and how the question should be rephrased for each agent.
Return one line per agent in the format: rewritten question;AgentName
Only include the agents that are needed to answer the question.
If no specialized agent is relevant, use General.
Do not add any explanation, only return the lines in the specified format.";
    }

    private List<(string Question, string AgentKey)> ParseOrchestratorResponse(string response)
    {
        var result = new List<(string Question, string AgentKey)>();

        var lines = response.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        foreach (var line in lines)
        {
            var trimmed = line.Trim();
            var separatorIdx = trimmed.LastIndexOf(';');
            if (separatorIdx < 0) continue;

            var question = trimmed[..separatorIdx].Trim();
            var agentKey = trimmed[(separatorIdx + 1)..].Trim();

            if (string.IsNullOrWhiteSpace(question) || string.IsNullOrWhiteSpace(agentKey))
                continue;

            result.Add((question, agentKey));
        }

        if (result.Count == 0)
        {
            result.Add((response.Trim(), "General"));
        }

        return result;
    }
}
