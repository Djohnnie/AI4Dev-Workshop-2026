using Microsoft.AspNetCore.Http.HttpResults;
using PromptArena;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<PromptScoringService>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapPost("/api/score", async Task<Results<Ok<PromptEvaluation>, ValidationProblem, ProblemHttpResult>> (
    PromptScoreRequest request,
    PromptScoringService scorer,
    CancellationToken cancellationToken) =>
{
    if (string.IsNullOrWhiteSpace(request.Prompt))
    {
        return TypedResults.ValidationProblem(new Dictionary<string, string[]>
        {
            ["prompt"] = ["Enter a prompt before asking the judge to score it."]
        });
    }

    try
    {
        var evaluation = await scorer.ScoreAsync(request.Prompt, cancellationToken);
        return TypedResults.Ok(evaluation);
    }
    catch (InvalidOperationException ex)
    {
        return TypedResults.Problem(
            title: "Configuration error",
            detail: ex.Message,
            statusCode: StatusCodes.Status500InternalServerError);
    }
    catch (System.Text.Json.JsonException ex)
    {
        return TypedResults.Problem(
            title: "Scoring response error",
            detail: $"The scoring model did not return valid JSON. {ex.Message}",
            statusCode: StatusCodes.Status502BadGateway);
    }
});

app.MapGet("/api/challenges", () => TypedResults.Ok(ChallengeCatalog.All));

app.Run();
