namespace PromptArena;

internal sealed record ChallengeMode(string Title, string Goal, string Twist);

internal static class ChallengeCatalog
{
    public static IReadOnlyList<ChallengeMode> All { get; } =
    [
        new(
            "Prompt Makeover Sprint",
            "Take a weak prompt and push it above 80%.",
            "Every team starts from the same vague prompt."),
        new(
            "Few-Shot Smackdown",
            "Compare one-shot and few-shot versions of the same task.",
            "You only win if the example actually improves the score."),
        new(
            "Anti-Pattern Bingo",
            "Hunt vague wording, missing context, and conflicting constraints.",
            "Fix the anti-patterns before adding new details."),
        new(
            "Constraint Cage Match",
            "Write a prompt that is precise without being brittle.",
            "You must include at least two real constraints."),
        new(
            "Leaderboard Relay",
            "Improve the same prompt in four small handoffs.",
            "Each teammate may only edit one ingredient category."),
        new(
            "Judge the Judge",
            "Stress-test the scoring bot with borderline prompts.",
            "Try to find prompts the rubric scores unfairly.")
    ];
}
