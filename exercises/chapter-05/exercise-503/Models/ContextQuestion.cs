namespace ContextVariablesPlayground.Models;

public sealed record ContextQuestion(
    string Title,
    string Goal,
    string SuggestedPrompt,
    string SuggestedVariable,
    string SuggestedFile);
