using TaskManager.Entities;
using TaskManager.Repositories;

namespace TaskManager.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository) => _repository = repository;

    public IEnumerable<TaskItem> GetAllTasks() => _repository.GetAll();

    public TaskItem CreateTask(string title)
    {
        var task = new TaskItem
        {
            Id = Guid.NewGuid(),
            Title = title,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };
        _repository.Add(task);
        return task;
    }

    public TaskItem? CompleteTask(Guid id)
    {
        var task = _repository.GetById(id);
        if (task is null) return null;
        task.IsCompleted = true;
        _repository.Update(task);
        return task;
    }

    public bool RemoveTask(Guid id) => _repository.Remove(id);
}
