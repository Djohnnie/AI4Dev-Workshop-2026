using PromptPatternsPlayground.Models;

namespace PromptPatternsPlayground.Services;

public sealed class ReleaseSummaryService
{
    /*
        This method is the comment-driven scenario.
        Keep the output short and clear for busy stakeholders.
        The title should echo the sprint name.
        The tone should stay calm, confident, and non-hype.
        Highlights should preserve the original feature order.
        The closing line should remind readers that rollout is staged.
    */
    public ReleaseSummary BuildSummary(string sprintName, IReadOnlyList<string> shippedHighlights)
    {
        var title = $"{sprintName} release summary";
        var tone = "Calm and operational";
        var closingLine = "Rollout stays staged and we will watch telemetry before broadening exposure.";

        return new ReleaseSummary(title, tone, shippedHighlights, closingLine);
    }
}
