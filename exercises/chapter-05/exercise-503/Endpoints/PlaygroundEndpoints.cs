using ContextVariablesPlayground.Repositories;
using ContextVariablesPlayground.Services;

namespace ContextVariablesPlayground.Endpoints;

public static class PlaygroundEndpoints
{
    public static IEndpointRouteBuilder MapPlaygroundEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/work-items", (IWorkItemRepository repository) => repository.GetAll());
        app.MapGet("/api/incidents", (IncidentDigestService incidentDigestService) => incidentDigestService.BuildDigest());
        app.MapGet("/api/summary", (KnowledgeMapService knowledgeMapService) => knowledgeMapService.BuildSummary());
        app.MapGet("/api/questions", () => ContextQuestionCatalog.All);

        return app;
    }
}
