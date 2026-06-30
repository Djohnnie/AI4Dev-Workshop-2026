namespace PromptArena;

internal sealed record IngredientAssessment(string Name, bool Present, string Notes);

internal sealed record TokenUsage(int InputTokens, int OutputTokens)
{
    public int TotalTokens => InputTokens + OutputTokens;
}

internal sealed record CopilotAnswer(string Text, TokenUsage Usage);

internal sealed record PromptEvaluation(
    int Score,
    string Verdict,
    string PromptingStyle,
    IReadOnlyList<IngredientAssessment> Ingredients,
    IReadOnlyList<string> Strengths,
    IReadOnlyList<string> AntiPatterns,
    IReadOnlyList<string> Suggestions,
    IReadOnlyList<string> Hints,
    int InputTokens,
    int OutputTokens,
    int TokenEfficiencyScore,
    string TokenAssessment,
    string Answer);
