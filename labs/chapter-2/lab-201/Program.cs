using Spectre.Console;

await new SnakeGameStarter().RunAsync();

internal sealed class SnakeGameStarter
{
    private const int BoardWidth = 24;
    private const int BoardHeight = 14;

    private readonly List<Position> snake =
    [
        new(BoardWidth / 2, BoardHeight / 2)
    ];

    private Direction currentDirection = Direction.Right;
    private GameState state = GameState.WaitingToStart;
    private Position food = new(BoardWidth / 3, BoardHeight / 3);

    public Task RunAsync()
    {
        AnsiConsole.Clear();
        ShowTitle();
        ShowIntro();
        ShowStarterBoard();
        ShowNextSteps();

        AnsiConsole.MarkupLine("[grey]Press any key to close the starter and begin implementing the game.[/]");
        Console.ReadKey(true);
        return Task.CompletedTask;
    }

    private static void ShowTitle()
    {
        AnsiConsole.Write(new FigletText("Ultimate Snake").Centered().Color(Color.Green1));
        AnsiConsole.Write(new Rule("[yellow]Chapter 2 - Lab 201[/]").RuleStyle("grey").LeftJustified());
        AnsiConsole.WriteLine();
    }

    private void ShowIntro()
    {
        var intro = new Panel(new Markup(
            "[bold]Starter ready.[/]\n" +
            "Spectre.Console is already installed and the project builds.\n\n" +
            "[bold]Controls to support in your finished game:[/]\n" +
            "- Arrow keys move the snake\n" +
            "- [bold]Space[/] starts and pauses the game\n" +
            "- Crossing an edge wraps to the opposite side\n" +
            "- [bold]Esc[/] exits the app"))
        {
            Header = new PanelHeader("Mission"),
            Border = BoxBorder.Rounded,
            BorderStyle = new Style(Color.Green1)
        };

        AnsiConsole.Write(intro);
        AnsiConsole.WriteLine();
    }

    private void ShowStarterBoard()
    {
        var board = new Panel(new Markup(RenderBoard()))
        {
            Header = new PanelHeader("Starter board"),
            Border = BoxBorder.Rounded,
            BorderStyle = new Style(Color.Blue)
        };

        AnsiConsole.Write(board);
        AnsiConsole.WriteLine();
    }

    private void ShowNextSteps()
    {
        var nextSteps = new Panel(new Markup(
            "[bold]Suggested implementation steps[/]\n" +
            "1. Turn [bold]RunAsync[/] into a real game loop.\n" +
            "2. Read keyboard input without blocking the loop.\n" +
            "3. Move the snake every frame and grow it when it eats food.\n" +
            "4. Wrap through the left/right and top/bottom edges.\n" +
            "5. Detect self-collisions only.\n" +
            "6. Re-render the board, score, and state after each tick.\n\n" +
            "[bold]Starter hooks already in place[/]\n" +
            $"- State: {state}\n" +
            $"- Direction: {currentDirection}\n" +
            $"- Head: ({snake[0].X}, {snake[0].Y})\n" +
            $"- Food: ({food.X}, {food.Y})\n\n" +
            "[grey]Use Ask mode, inline chat, and ghost text only for this lab.[/]"))
        {
            Header = new PanelHeader("What to build next"),
            Border = BoxBorder.Rounded,
            BorderStyle = new Style(Color.Yellow)
        };

        AnsiConsole.Write(nextSteps);
        AnsiConsole.WriteLine();
    }

    private string RenderBoard()
    {
        var head = snake[0];
        var lines = new List<string>();

        for (var y = -1; y <= BoardHeight; y++)
        {
            var row = new List<string>();

            for (var x = -1; x <= BoardWidth; x++)
            {
                if (x == -1 || y == -1 || x == BoardWidth || y == BoardHeight)
                {
                    row.Add("[grey]#[/] ");
                }
                else if (x == head.X && y == head.Y)
                {
                    row.Add("[bold yellow]@[/] ");
                }
                else if (x == food.X && y == food.Y)
                {
                    row.Add("[red]*[/] ");
                }
                else
                {
                    row.Add("[dim].[/] ");
                }
            }

            lines.Add(string.Concat(row));
        }

        return string.Join(Environment.NewLine, lines);
    }

    // TODO: Read arrow keys and spacebar here.
    private void HandleInput(ConsoleKey key)
    {
    }

    // TODO: Move the snake, wrap around the edges, grow when it eats food, and detect self-collisions here.
    private void UpdateGame()
    {
    }
}

internal enum Direction
{
    Up,
    Down,
    Left,
    Right
}

internal enum GameState
{
    WaitingToStart,
    Running,
    Paused,
    GameOver
}

internal readonly record struct Position(int X, int Y);