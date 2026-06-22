using ContextVariablesPlayground.Models;
using ContextVariablesPlayground.Repositories;

namespace ContextVariablesPlayground.Services;

public sealed class IncidentDigestService
{
    private readonly IWorkItemRepository _repository;

    public IncidentDigestService(IWorkItemRepository repository)
    {
        _repository = repository;
    }

    public IncidentDigest BuildDigest()
    {
        var items = _repository.GetAll();
        var highRiskItems = items.Where(item => item.Blocked || item.OpenBugs >= 3).ToList();
        var averageLeadTime = Math.Round(items.Average(item => item.LeadTimeDays), 1);

        var bottlenecks = highRiskItems
            .OrderByDescending(item => item.OpenBugs)
            .Select(item => $"{item.Team}: {item.Title}")
            .ToList();

        var focusAreas = highRiskItems
            .SelectMany(item => item.Tags)
            .GroupBy(tag => tag, StringComparer.OrdinalIgnoreCase)
            .OrderByDescending(group => group.Count())
            .ThenBy(group => group.Key, StringComparer.OrdinalIgnoreCase)
            .Take(3)
            .Select(group => group.Key)
            .ToList();

        return new IncidentDigest(
            OpenHighRiskItems: highRiskItems.Count,
            AverageLeadTimeDays: averageLeadTime,
            Bottlenecks: bottlenecks,
            RecommendedFocusAreas: focusAreas);
    }
}
