using ContextVariablesPlayground.Models;

namespace ContextVariablesPlayground.Repositories;

public interface IWorkItemRepository
{
    IReadOnlyList<WorkItem> GetAll();
}
