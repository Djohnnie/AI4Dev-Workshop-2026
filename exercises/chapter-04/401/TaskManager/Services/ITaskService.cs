using TaskManager.Entities;

namespace TaskManager.Services;

public interface ITaskService
{
    IEnumerable<TaskItem> GetAllTasks();
    TaskItem CreateTask(string title);
    TaskItem? CompleteTask(Guid id);
    bool RemoveTask(Guid id);
}
