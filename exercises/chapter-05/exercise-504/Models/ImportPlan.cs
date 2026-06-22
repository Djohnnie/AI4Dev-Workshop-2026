namespace PromptPatternsPlayground.Models;

public sealed record ImportPlan(
    string FileName,
    string Mode,
    string ValidationAction,
    string ExecutionAction,
    string FollowUpAction);
