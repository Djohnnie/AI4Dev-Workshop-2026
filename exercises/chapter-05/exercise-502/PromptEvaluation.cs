namespace PromptArena;

internal sealed record IngredientAssessment(string Name, bool Present, string Notes);

internal sealed record PromptEvaluation(
    int Score,
    string Verdict,
    string PromptingStyle,
    IReadOnlyList<IngredientAssessment> Ingredients,
    IReadOnlyList<string> Strengths,
    IReadOnlyList<string> AntiPatterns,
    IReadOnlyList<string> Suggestions,
    IReadOnlyList<string> Hints);
