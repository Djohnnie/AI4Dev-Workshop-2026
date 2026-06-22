using ContextVariablesPlayground.Models;
using ContextVariablesPlayground.Repositories;

namespace ContextVariablesPlayground.Services;

public sealed class KnowledgeMapService
{
    private readonly AuthGateway _authGateway;
    private readonly IWorkItemRepository _repository;
    private readonly SprintHealthService _sprintHealthService;

    public KnowledgeMapService(
        AuthGateway authGateway,
        IWorkItemRepository repository,
        SprintHealthService sprintHealthService)
    {
        _authGateway = authGateway;
        _repository = repository;
        _sprintHealthService = sprintHealthService;
    }

    public object BuildSummary()
    {
        var snapshot = _sprintHealthService.BuildSnapshot();
        var workItems = _repository.GetAll();

        return new
        {
            app = "Context Variables Playground",
            purpose = "A prebuilt codebase for experimenting with @workspace and #file prompts.",
            workItemCount = workItems.Count,
            teams = workItems.Select(item => item.Team).Distinct(StringComparer.OrdinalIgnoreCase).OrderBy(team => team).ToArray(),
            authEntryPoint = nameof(AuthGateway),
            authSummary = _authGateway.DescribeContract(),
            riskSnapshot = snapshot,
            keyFiles = new[]
            {
                "Program.cs",
                "Endpoints/PlaygroundEndpoints.cs",
                "Services/AuthGateway.cs",
                "Services/SprintHealthService.cs",
                "Services/IncidentDigestService.cs",
                "docs/context-questions.md"
            },
            variableTips = new[]
            {
                "Use @workspace to get the repo map before opening files.",
                "Use #file when you want a precise explanation of one service or endpoint.",
                "Repeat the same question with and without variables to compare the answer quality."
            },
            todoLocations = new[]
            {
                "Services/AuthGateway.cs",
                "Services/SprintHealthService.cs"
            }
        };
    }
}
