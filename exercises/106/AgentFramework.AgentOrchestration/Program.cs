using AgentOrchestration.Agents;
using AgentOrchestration.Model;
using AgentOrchestration.Orchestration;
using Microsoft.Extensions.Configuration;
using Spectre.Console;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true)
    .AddJsonFile("appsettings.Development.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

var endpoint = configuration["AZUREOPENAI_ENDPOINT"];
var key = configuration["AZUREOPENAI_KEY"];

if (string.IsNullOrWhiteSpace(endpoint) || string.IsNullOrWhiteSpace(key))
{
    AnsiConsole.MarkupLine("[red]ERROR:[/] AZUREOPENAI_ENDPOINT and AZUREOPENAI_KEY must be configured.");
    AnsiConsole.MarkupLine("[yellow]Set them via environment variables or appsettings.Development.json.[/]");
    return;
}

var summaryFactory = new SummaryAgentFactory(configuration);
var orchestratorFactory = new OrchestratorAgentFactory(configuration);
var replyFactory = new ReplyAgentFactory(configuration);
var generalFactory = new GeneralAgentFactory(configuration);
var powerFactory = new MijnThuisPowerAgentFactory(configuration);
var solarFactory = new MijnThuisSolarAgentFactory(configuration);
var carFactory = new MijnThuisCarAgentFactory(configuration);
var heatingFactory = new MijnThuisHeatingAgentFactory(configuration);
var smartLockFactory = new MijnThuisSmartLockAgentFactory(configuration);
var saunaFactory = new MijnSaunaAgentFactory(configuration);
var photoCarouselFactory = new PhotoCarouselAgentFactory(configuration);

var orchestrationManager = new AgentOrchestrationManager(
    summaryFactory, orchestratorFactory, replyFactory,
    generalFactory, powerFactory, solarFactory, carFactory,
    heatingFactory, smartLockFactory, saunaFactory, photoCarouselFactory);

var chatHistory = new CopilotChatHistory();
var debugMode = false;

AnsiConsole.Write(new FigletText("MijnCopilot").Color(Color.DeepSkyBlue1));
AnsiConsole.MarkupLine("[yellow]Type your question, [bold]/debug[/] to toggle debug output, [bold]/help[/] for commands, [bold]/exit[/] to quit.[/]");
AnsiConsole.WriteLine();

while (true)
{
    AnsiConsole.Markup("[green]You:[/] ");
    var input = Console.ReadLine()?.Trim();

    if (string.IsNullOrWhiteSpace(input))
        continue;

    if (input.Equals("/exit", StringComparison.OrdinalIgnoreCase))
    {
        AnsiConsole.MarkupLine("[yellow]Goodbye![/]");
        break;
    }

    if (input.Equals("/debug", StringComparison.OrdinalIgnoreCase))
    {
        debugMode = !debugMode;
        AnsiConsole.MarkupLine($"[yellow]Debug mode: [bold]{(debugMode ? "ON" : "OFF")}[/][/]");
        continue;
    }

    if (input.Equals("/help", StringComparison.OrdinalIgnoreCase))
    {
        AnsiConsole.MarkupLine("[yellow]Commands:[/]");
        AnsiConsole.MarkupLine("[yellow]  /debug  - Toggle inter-agent debug output[/]");
        AnsiConsole.MarkupLine("[yellow]  /help   - Show this help[/]");
        AnsiConsole.MarkupLine("[yellow]  /exit   - Quit the application[/]");
        continue;
    }

    chatHistory.AddUserMessage(input);

    await AnsiConsole.Status()
        .Spinner(Spinner.Known.Dots)
        .SpinnerStyle(Style.Parse("deepskyblue1"))
        .StartAsync("Thinking...", async ctx =>
        {
            chatHistory = await orchestrationManager.Chat(chatHistory);
        });

    if (debugMode && chatHistory.Debug.Count > 0)
    {
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[mediumpurple3]--- Debug: Inter-Agent Communication ---[/]");

        foreach (var debug in chatHistory.Debug)
        {
            var arrow = debug.IsQuestion ? "→" : "←";
            var safeContent = Markup.Escape(debug.Content);
            var safeAgent = Markup.Escape(debug.AgentName);
            AnsiConsole.MarkupLine($"[mediumpurple3]{arrow} [[{safeAgent}]][/]");
            AnsiConsole.MarkupLine($"[grey]{safeContent}[/]");
        }

        AnsiConsole.MarkupLine("[mediumpurple3]--- End Debug ---[/]");
        AnsiConsole.WriteLine();
    }

    var safeResponse = Markup.Escape(chatHistory.LastAssistantMessage ?? "No response.");
    AnsiConsole.MarkupLine($"[deepskyblue1]Copilot:[/] {safeResponse}");

    if (chatHistory.ContributingAgents.Count > 0)
    {
        var agents = string.Join(", ", chatHistory.ContributingAgents.Select(Markup.Escape));
        AnsiConsole.MarkupLine($"[grey]Answered by: {agents}[/]");
    }

    AnsiConsole.WriteLine();
}
