using ContextVariablesPlayground.Models;
using ContextVariablesPlayground.Repositories;

namespace ContextVariablesPlayground.Services;

public sealed class SprintHealthService
{
    private readonly IWorkItemRepository _repository;

    public SprintHealthService(IWorkItemRepository repository)
    {
        _repository = repository;
    }

    public SprintHealthSnapshot BuildSnapshot()
    {
        var items = _repository.GetAll();
        var blockedItems = items.Count(item => item.Blocked);
        var highRiskItems = items.Count(IsHighRisk);
        var averageLeadTime = Math.Round(items.Average(item => item.LeadTimeDays), 1);

        return new SprintHealthSnapshot(
            TotalItems: items.Count,
            BlockedItems: blockedItems,
            HighRiskItems: highRiskItems,
            AverageLeadTimeDays: averageLeadTime,
            RiskBand: CalculateRiskBand(highRiskItems, averageLeadTime));
    }

    private static bool IsHighRisk(WorkItem item) => item.Blocked || item.OpenBugs >= 3 || item.LeadTimeDays >= 10;

    // TODO: Split this single risk rule into per-team thresholds after the workshop.
    private static string CalculateRiskBand(int highRiskItems, double averageLeadTimeDays)
    {
        if (highRiskItems >= 3 || averageLeadTimeDays >= 10)
        {
            return "High";
        }

        if (highRiskItems >= 2 || averageLeadTimeDays >= 7)
        {
            return "Medium";
        }

        return "Low";
    }
}
