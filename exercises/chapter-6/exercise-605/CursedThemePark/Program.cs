using CursedThemePark;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ScenarioCatalog>();
builder.Services.AddSingleton<CursedCheckoutService>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/health", () => Results.Ok(new { status = "haunted-but-running" }));

app.MapGet("/api/scenarios", (ScenarioCatalog catalog) =>
    Results.Ok(catalog.GetAll().Select(s => new
    {
        s.Id,
        s.Title,
        s.GuestReport,
        s.ExpectedException
    })));

app.MapPost("/api/demo/{scenarioId}", async (string scenarioId, ScenarioCatalog catalog, CursedCheckoutService service, CancellationToken cancellationToken) =>
{
    var scenario = catalog.Get(scenarioId);
    var receipt = await service.CheckoutAsync(scenario.Request, cancellationToken);
    return Results.Ok(receipt);
});

app.MapPost("/api/checkout", async (ParkCheckoutRequest request, CursedCheckoutService service, CancellationToken cancellationToken) =>
{
    var receipt = await service.CheckoutAsync(request, cancellationToken);
    return Results.Ok(receipt);
});

app.Run();

public partial class Program;
