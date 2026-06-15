using Spectre.Console;

public interface IEscapeRoomGame
{
    Task RunAsync();
}

public interface IEscapeRoomScript
{
    IReadOnlyList<EscapeRoomChallenge> GetChallenges();
}

public interface IHintService
{
    string GetHint(EscapeRoomChallenge challenge, int attempt);
}

public interface IEscapeRoomUi
{
    void ShowIntro(int roomCount);
    void ShowChallenge(EscapeRoomChallenge challenge, int roomIndex, int roomCount);
    string AskForAnswer(EscapeRoomChallenge challenge, int attempt);
    void ShowHint(string hint);
    void ShowSolved(EscapeRoomChallenge challenge);
    void ShowFailure(EscapeRoomChallenge challenge);
    void ShowSeparator();
    void ShowSummary(IReadOnlyList<ChallengeOutcome> outcomes);
}

public sealed record EscapeRoomChallenge(
    string RoomName,
    string Story,
    string Riddle,
    string[] AcceptedAnswers,
    string Hint,
    string Reward)
{
    public string PrimaryAnswer => AcceptedAnswers[0];

    public bool Matches(string? answer)
    {
        var normalizedAnswer = Normalize(answer);
        return AcceptedAnswers.Any(expected => Normalize(expected) == normalizedAnswer);
    }

    private static string Normalize(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        return new string(value.Where(char.IsLetterOrDigit).ToArray()).ToUpperInvariant();
    }
}

public sealed record ChallengeOutcome(EscapeRoomChallenge Challenge, bool Solved);

public sealed class EscapeRoomScript : IEscapeRoomScript
{
    public IReadOnlyList<EscapeRoomChallenge> GetChallenges() =>
    [
        new EscapeRoomChallenge(
            "Generator Room",
            "The lights flicker. A dead generator hums behind a locked grate.",
            "What has keys but can't open locks?",
            ["piano", "keyboard"],
            "It makes music, not electricity.",
            "Silver key recovered."),
        new EscapeRoomChallenge(
            "Archive Hall",
            "Dusty shelves line the walls. A note is taped to the last bookcase.",
            "What gets wetter the more it dries?",
            ["towel"],
            "It belongs in a bathroom, not on a shelf.",
            "Brass clue recovered."),
        new EscapeRoomChallenge(
            "Exit Door",
            "The final keypad glows red. One last answer stands between you and freedom.",
            "What has a head, a tail, is brown, and has no legs?",
            ["penny", "coin"],
            "You might find it on the floor after laundry day.",
            "Door unlocked.")
    ];
}

public sealed class HintService : IHintService
{
    public string GetHint(EscapeRoomChallenge challenge, int attempt) =>
        attempt switch
        {
            1 => challenge.Hint,
            2 => $"Second clue: the answer starts with '{challenge.PrimaryAnswer[0]}'.",
            _ => $"Final clue: the answer is '{challenge.PrimaryAnswer}'."
        };
}

public sealed class SpectreEscapeRoomUi : IEscapeRoomUi
{
    public void ShowIntro(int roomCount)
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new FigletText("Escape Room").Centered().Color(Color.Cyan1));
        AnsiConsole.MarkupLine("[grey]Built with Spectre.Console and a custom DI container.[/]");
        AnsiConsole.MarkupLine($"[grey]Solve [bold]{roomCount}[/] rooms to escape.[/]");
        AnsiConsole.WriteLine();
    }

    public void ShowChallenge(EscapeRoomChallenge challenge, int roomIndex, int roomCount)
    {
        AnsiConsole.Write(new Rule($"[bold cyan]Room {roomIndex}/{roomCount}: {Markup.Escape(challenge.RoomName)}[/]")
            .RuleStyle("cyan")
            .Centered());
        AnsiConsole.WriteLine();

        AnsiConsole.Write(new Panel(new Markup(
                $"{Markup.Escape(challenge.Story)}\n\n[bold yellow]{Markup.Escape(challenge.Riddle)}[/]"))
            .BorderStyle(Style.Parse("cyan1"))
            .Expand());
        AnsiConsole.WriteLine();
    }

    public string AskForAnswer(EscapeRoomChallenge challenge, int attempt)
    {
        string answer;

        do
        {
            answer = AnsiConsole.Ask<string>($"[bold yellow]Attempt {attempt}/3 — your answer[/] > ");
        }
        while (string.IsNullOrWhiteSpace(answer));

        return answer;
    }

    public void ShowHint(string hint)
    {
        AnsiConsole.MarkupLine($"[yellow]Hint:[/] {Markup.Escape(hint)}");
    }

    public void ShowSolved(EscapeRoomChallenge challenge)
    {
        AnsiConsole.MarkupLine($"[green]Unlocked![/] {Markup.Escape(challenge.Reward)}");
    }

    public void ShowFailure(EscapeRoomChallenge challenge)
    {
        AnsiConsole.MarkupLine($"[red]The lock stays shut.[/] The answer was [bold]{Markup.Escape(challenge.PrimaryAnswer)}[/].");
    }

    public void ShowSeparator()
    {
        AnsiConsole.Write(new Rule().RuleStyle("grey35"));
        AnsiConsole.WriteLine();
    }

    public void ShowSummary(IReadOnlyList<ChallengeOutcome> outcomes)
    {
        var solved = outcomes.Count(outcome => outcome.Solved);
        var title = solved == outcomes.Count ? "[bold green]You Escaped![/]" : "[bold orange1]You Made It Out[/]";

        AnsiConsole.Write(new Rule(title).RuleStyle("green").Centered());
        AnsiConsole.WriteLine();

        AnsiConsole.Write(new Panel(
                new Markup($"{solved} of {outcomes.Count} rooms solved.\n\n" +
                           "The service graph is ready for the next prompt-based refactor."))
            .BorderStyle(Style.Parse("green"))
            .Expand());
        AnsiConsole.WriteLine();

        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderStyle(Style.Parse("green"))
            .Expand()
            .AddColumn("[bold]Room[/]")
            .AddColumn("[bold]Result[/]")
            .AddColumn("[bold]Reward[/]");

        foreach (var outcome in outcomes)
        {
            table.AddRow(
                Markup.Escape(outcome.Challenge.RoomName),
                outcome.Solved ? "[green]Solved[/]" : "[red]Stuck[/]",
                outcome.Solved ? Markup.Escape(outcome.Challenge.Reward) : "[grey]—[/]");
        }

        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();
    }
}

public sealed class EscapeRoomGame : IEscapeRoomGame
{
    private readonly IEscapeRoomScript script;
    private readonly IHintService hints;
    private readonly IEscapeRoomUi ui;

    public EscapeRoomGame(IEscapeRoomScript script, IHintService hints, IEscapeRoomUi ui)
    {
        this.script = script;
        this.hints = hints;
        this.ui = ui;
    }

    public Task RunAsync()
    {
        var challenges = script.GetChallenges();
        var outcomes = new List<ChallengeOutcome>(challenges.Count);

        ui.ShowIntro(challenges.Count);

        for (var index = 0; index < challenges.Count; index++)
        {
            var challenge = challenges[index];
            ui.ShowChallenge(challenge, index + 1, challenges.Count);

            var solved = false;

            for (var attempt = 1; attempt <= 3; attempt++)
            {
                var answer = ui.AskForAnswer(challenge, attempt);

                if (challenge.Matches(answer))
                {
                    solved = true;
                    ui.ShowSolved(challenge);
                    break;
                }

                if (attempt < 3)
                {
                    ui.ShowHint(hints.GetHint(challenge, attempt));
                }
            }

            if (!solved)
            {
                ui.ShowFailure(challenge);
            }

            outcomes.Add(new ChallengeOutcome(challenge, solved));
            ui.ShowSeparator();
        }

        ui.ShowSummary(outcomes);
        return Task.CompletedTask;
    }
}
