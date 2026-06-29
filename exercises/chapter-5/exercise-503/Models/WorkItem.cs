namespace ContextVariablesPlayground.Models;

public sealed record WorkItem(
    int Id,
    string Title,
    string Team,
    string Owner,
    string Status,
    int LeadTimeDays,
    int OpenBugs,
    bool Blocked,
    string[] Tags);
