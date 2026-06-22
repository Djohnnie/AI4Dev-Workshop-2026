using PromptPatternsPlayground.Models;

namespace PromptPatternsPlayground.Services;

public sealed class ReleaseWindowCalculator
{
    public ReleaseWindowDecision Calculate(string changeName, DateOnly plannedDate, int openIncidents, bool hasSchemaChange)
    {
        if (hasSchemaChange)
        {
            return new ReleaseWindowDecision(changeName, "Manual review", "Schema changes always require a staffed release window.");
        }

        if (plannedDate.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
        {
            return new ReleaseWindowDecision(changeName, "Delay to weekday", "Weekend releases are paused unless they are emergency fixes.");
        }

        if (openIncidents >= 3)
        {
            return new ReleaseWindowDecision(changeName, "Stabilize first", "Too many open incidents are competing for operator attention.");
        }

        return new ReleaseWindowDecision(changeName, "Auto-deploy", "The change is safe for the standard weekday release path.");
    }
}
