using System.Text.Json;
using System.Text.RegularExpressions;
using Azure.AI.OpenAI;
using Microsoft.Agents.AI;
using OpenAI.Chat;
using System.ClientModel;

namespace PromptArena;

internal sealed class PromptScoringService(IConfiguration configuration)
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public async Task<PromptEvaluation> ScoreAsync(string participantPrompt, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(participantPrompt);

        var endpoint = configuration["OPENAI_ENDPOINT"];
        var key = configuration["OPENAI_KEY"];
        var model = configuration["OPENAI_MODEL"] ?? "gpt-4o";

        if (string.IsNullOrWhiteSpace(endpoint) || string.IsNullOrWhiteSpace(key))
        {
            throw new InvalidOperationException(
                "Set OPENAI_ENDPOINT and OPENAI_KEY before running Prompt Arena. OPENAI_MODEL is optional and defaults to gpt-4o.");
        }

        var client = new AzureOpenAIClient(new Uri(endpoint), new ApiKeyCredential(key));
        var chatClient = client.GetChatClient(model);
        var scoreResult = await ScorePromptAsync(chatClient, participantPrompt, cancellationToken);
        var hints = await GenerateHintsAsync(chatClient, participantPrompt, scoreResult, cancellationToken);

        return scoreResult with
        {
            Hints = hints
        };
    }

    private static async Task<PromptEvaluation> ScorePromptAsync(
        ChatClient chatClient,
        string participantPrompt,
        CancellationToken cancellationToken)
    {
        var judge = chatClient.AsAIAgent(
            name: "Prompt Arena Judge",
            description: "Scores prompts against the workshop rubric.",
            instructions: PromptScoringPrompts.ScoringSystemPrompt);

        var session = await judge.CreateSessionAsync(cancellationToken);

        var evaluationRequest =
            $$"""
            Rate the following participant prompt for the GitHub Copilot workshop.

            Participant prompt:
            ```text
            {{participantPrompt}}
            ```
            """;

        var response = await judge.RunAsync(
            evaluationRequest,
            session,
            new ChatClientAgentRunOptions(),
            cancellationToken);

        return ParseEvaluation(response.Text ?? string.Empty);
    }

    private static async Task<IReadOnlyList<string>> GenerateHintsAsync(
        ChatClient chatClient,
        string participantPrompt,
        PromptEvaluation evaluation,
        CancellationToken cancellationToken)
    {
        var coach = chatClient.AsAIAgent(
            name: "Prompt Arena Coach",
            description: "Gives prompt-specific improvement hints.",
            instructions: PromptScoringPrompts.HintingSystemPrompt);

        var session = await coach.CreateSessionAsync(cancellationToken);

        var hintRequest =
            $$"""
            Participant prompt:
            ```text
            {{participantPrompt}}
            ```

            Score result:
            {{JsonSerializer.Serialize(evaluation, SerializerOptions)}}
            """;

        var response = await coach.RunAsync(
            hintRequest,
            session,
            new ChatClientAgentRunOptions(),
            cancellationToken);

        var parsed = JsonSerializer.Deserialize<PromptHintsResponse>(
            ExtractJson(response.Text ?? string.Empty),
            SerializerOptions) ?? throw new JsonException("The coach returned an empty payload.");

        return NormalizeStringList(parsed.Hints);
    }

    private static PromptEvaluation ParseEvaluation(string responseText)
    {
        var json = ExtractJson(responseText);
        var evaluation = JsonSerializer.Deserialize<PromptEvaluation>(json, SerializerOptions)
            ?? throw new JsonException("The judge returned an empty payload.");

        var ingredients = (evaluation.Ingredients ?? [])
            .Select(ingredient => new IngredientAssessment(
                string.IsNullOrWhiteSpace(ingredient.Name) ? "Unknown" : ingredient.Name.Trim(),
                ingredient.Present,
                string.IsNullOrWhiteSpace(ingredient.Notes) ? "No notes returned." : ingredient.Notes.Trim()))
            .ToArray();

        return evaluation with
        {
            Score = Math.Clamp(evaluation.Score, 0, 100),
            Verdict = string.IsNullOrWhiteSpace(evaluation.Verdict) ? "No verdict returned." : evaluation.Verdict.Trim(),
            PromptingStyle = NormalizePromptingStyle(evaluation.PromptingStyle),
            Ingredients = ingredients,
            Strengths = NormalizeStringList(evaluation.Strengths),
            AntiPatterns = NormalizeStringList(evaluation.AntiPatterns),
            Suggestions = NormalizeStringList(evaluation.Suggestions),
            Hints = NormalizeStringList(evaluation.Hints)
        };
    }

    private static string ExtractJson(string responseText)
    {
        var match = Regex.Match(responseText, "\\{[\\s\\S]*\\}");

        if (!match.Success)
        {
            throw new JsonException("The judge did not return a JSON object.");
        }

        return match.Value;
    }

    private static string NormalizePromptingStyle(string? promptingStyle)
    {
        if (string.IsNullOrWhiteSpace(promptingStyle))
        {
            return "none";
        }

        return promptingStyle.Trim().ToLowerInvariant() switch
        {
            "one-shot" => "one-shot",
            "few-shot" => "few-shot",
            _ => "none"
        };
    }

    private static IReadOnlyList<string> NormalizeStringList(IReadOnlyList<string>? values)
    {
        return (values ?? [])
            .Where(static item => !string.IsNullOrWhiteSpace(item))
            .Select(static item => item.Trim())
            .ToArray();
    }
}
