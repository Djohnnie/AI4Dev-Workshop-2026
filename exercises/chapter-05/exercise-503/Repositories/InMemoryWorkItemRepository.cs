using ContextVariablesPlayground.Models;

namespace ContextVariablesPlayground.Repositories;

public sealed class InMemoryWorkItemRepository : IWorkItemRepository
{
    private static readonly IReadOnlyList<WorkItem> Items =
    [
        new WorkItem(101, "Backfill audit trail gaps", "Platform", "Anika", "In Progress", 8, 2, false, ["audit", "compliance"]),
        new WorkItem(102, "Repair delayed invoice export", "Billing", "Matteo", "Blocked", 13, 5, true, ["billing", "export"]),
        new WorkItem(103, "Stabilize retry worker", "Platform", "Nina", "In Review", 6, 1, false, ["worker", "reliability"]),
        new WorkItem(104, "Refresh release dashboard", "Experience", "Luca", "Ready", 4, 0, false, ["ui", "dashboard"]),
        new WorkItem(105, "Reduce false-positive risk alerts", "Insights", "Sara", "In Progress", 11, 3, true, ["alerts", "risk"]),
        new WorkItem(106, "Add tenant filter to incident digest", "Platform", "Joost", "Ready", 3, 0, false, ["api", "digest"])
    ];

    public IReadOnlyList<WorkItem> GetAll() => Items;
}
