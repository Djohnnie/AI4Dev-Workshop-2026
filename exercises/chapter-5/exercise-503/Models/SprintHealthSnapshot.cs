namespace ContextVariablesPlayground.Models;

public sealed record SprintHealthSnapshot(
    int TotalItems,
    int BlockedItems,
    int HighRiskItems,
    double AverageLeadTimeDays,
    string RiskBand);
