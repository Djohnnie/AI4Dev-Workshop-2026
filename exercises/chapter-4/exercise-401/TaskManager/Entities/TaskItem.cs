namespace TaskManager.Entities;

public class TaskItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
}
