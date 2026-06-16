using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.Endpoints;

public static class TaskEndpoints
{
    public static void MapTaskEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/tasks");
        group.MapGetAllTasks();
        group.MapCreateTask();
        group.MapCompleteTask();
        group.MapRemoveTask();
    }

    private static void MapGetAllTasks(this RouteGroupBuilder group)
    {
        group.MapGet("/", (ITaskService service) => Results.Ok(service.GetAllTasks()));
    }

    private static void MapCreateTask(this RouteGroupBuilder group)
    {
        group.MapPost("/", (CreateTaskRequest request, ITaskService service) =>
        {
            if (string.IsNullOrWhiteSpace(request.Title))
                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    ["title"] = ["Title is required."]
                });

            if (request.Title.Length > 200)
                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    ["title"] = ["Title must not exceed 200 characters."]
                });

            var task = service.CreateTask(request.Title);
            return Results.Created($"/tasks/{task.Id}", task);
        });
    }

    private static void MapCompleteTask(this RouteGroupBuilder group)
    {
        group.MapPut("/{id:guid}/complete", (Guid id, ITaskService service) =>
        {
            var task = service.CompleteTask(id);
            return task is null ? Results.NotFound() : Results.Ok(task);
        });
    }

    private static void MapRemoveTask(this RouteGroupBuilder group)
    {
        group.MapDelete("/{id:guid}", (Guid id, ITaskService service) =>
        {
            var removed = service.RemoveTask(id);
            return removed ? Results.NoContent() : Results.NotFound();
        });
    }
}
