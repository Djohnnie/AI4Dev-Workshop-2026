namespace ContextVariablesPlayground.Models;

public sealed record IncidentDigest(
    int OpenHighRiskItems,
    double AverageLeadTimeDays,
    IReadOnlyList<string> Bottlenecks,
    IReadOnlyList<string> RecommendedFocusAreas);
