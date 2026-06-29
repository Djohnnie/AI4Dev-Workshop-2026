using CodeObfuscator.Tools;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddMcpServer()
    .WithHttpTransport(o => o.Stateless = true)
    .WithTools<CodeObfuscatorTool>();

var app = builder.Build();

app.MapMcp();

app.Run();
