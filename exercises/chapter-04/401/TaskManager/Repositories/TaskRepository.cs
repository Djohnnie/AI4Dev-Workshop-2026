using TaskManager.Entities;

namespace TaskManager.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly List<TaskItem> _tasks = [];

    public IEnumerable<TaskItem> GetAll() => _tasks.AsReadOnly();

    public TaskItem? GetById(Guid id) => _tasks.FirstOrDefault(t => t.Id == id);

    public void Add(TaskItem task) => _tasks.Add(task);

    public void Update(TaskItem task)
    {
        var index = _tasks.FindIndex(t => t.Id == task.Id);
        if (index >= 0)
            _tasks[index] = task;
    }

    public bool Remove(Guid id)
    {
        var task = GetById(id);
        if (task is null) return false;
        _tasks.Remove(task);
        return true;
    }
}
