using PromptPatternsPlayground.Endpoints;
using PromptPatternsPlayground.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ReleaseSummaryService>();
builder.Services.AddSingleton<ReleaseWindowCalculator>();
builder.Services.AddSingleton<SecurityHeadersPolicy>();
builder.Services.AddSingleton<ImportWorkflowService>();
builder.Services.AddSingleton<LegacyAuditLogger>();
builder.Services.AddSingleton<StructuredAuditLogger>();
builder.Services.AddSingleton<LegacyBillingLogger>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.MapPatternLabEndpoints();

app.Run();
