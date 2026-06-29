using TaskManager.Entities;

namespace TaskManager.Repositories;

public interface ITaskRepository
{
    IEnumerable<TaskItem> GetAll();
    TaskItem? GetById(Guid id);
    void Add(TaskItem task);
    void Update(TaskItem task);
    bool Remove(Guid id);
}
