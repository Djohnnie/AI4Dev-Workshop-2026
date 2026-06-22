namespace PromptPatternsPlayground.Models;

public sealed record ReleaseSummary(
    string Title,
    string Tone,
    IReadOnlyList<string> Highlights,
    string ClosingLine);
