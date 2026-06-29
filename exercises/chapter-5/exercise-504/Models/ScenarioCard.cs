namespace PromptPatternsPlayground.Models;

public sealed record ScenarioCard(
    string Pattern,
    string Goal,
    string FileHint,
    string SuggestedPrompt);
