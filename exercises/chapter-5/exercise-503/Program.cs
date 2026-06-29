using ContextVariablesPlayground.Endpoints;
using ContextVariablesPlayground.Repositories;
using ContextVariablesPlayground.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IWorkItemRepository, InMemoryWorkItemRepository>();
builder.Services.AddSingleton<AuthGateway>();
builder.Services.AddSingleton<IncidentDigestService>();
builder.Services.AddSingleton<SprintHealthService>();
builder.Services.AddSingleton<KnowledgeMapService>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.MapPlaygroundEndpoints();

app.Run();
