using ContextVariablesPlayground.Models;

namespace ContextVariablesPlayground.Services;

public static class ContextQuestionCatalog
{
    public static IReadOnlyList<ContextQuestion> All { get; } =
    [
        new ContextQuestion(
            "Map the repo first",
            "See how the main services fit together before reading files manually.",
            "@workspace map this project and tell me which files I should inspect first to understand sprint health and incident digests.",
            "@workspace",
            "Program.cs"),
        new ContextQuestion(
            "Zoom in on auth",
            "Compare a repo-wide answer with an exact file-level explanation.",
            "#file:Services/AuthGateway.cs explain how dashboard access is validated and whether this looks production-ready.",
            "#file",
            "Services/AuthGateway.cs"),
        new ContextQuestion(
            "Trace a hot path",
            "Follow data from repository to service to endpoint.",
            "@workspace trace how a work item becomes part of the incident digest response.",
            "@workspace",
            "Services/IncidentDigestService.cs"),
        new ContextQuestion(
            "Find TODOs",
            "Ask Copilot to surface unfinished ideas without scrolling through the repo.",
            "@workspace are there any TODO comments in this exercise, and which one matters most for production quality?",
            "@workspace",
            "docs/context-questions.md"),
        new ContextQuestion(
            "Compare answers",
            "Re-run the same prompt with and without variables to spot what context adds.",
            "Explain what determines the risk band in this app.",
            "none, then @workspace",
            "Services/SprintHealthService.cs")
    ];
}
