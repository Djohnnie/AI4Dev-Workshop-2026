using System.Text;
using Microsoft.VisualStudio.Shell;

namespace ContextPromptExtension;

internal sealed class ContextPromptOrchestrator
{
    private readonly VisualStudioContextCollector contextCollector;
    private readonly AzureOpenAiChatService chatService;

    public ContextPromptOrchestrator(AsyncPackage package)
    {
        contextCollector = new VisualStudioContextCollector(package);
        chatService = new AzureOpenAiChatService();
    }

    public async Task<ChatServiceResult> RunAsync(string prompt, ContextLevel level, CancellationToken cancellationToken)
    {
        var composedPrompt = await BuildComposedPromptAsync(prompt, level, cancellationToken);
        return await chatService.GetResponseAsync(composedPrompt, cancellationToken);
    }

    public async Task<string> BuildComposedPromptAsync(string prompt, ContextLevel level, CancellationToken cancellationToken)
    {
        var context = await contextCollector.CollectAsync(level, cancellationToken);
        return ContextPromptComposer.Compose(prompt, level, context);
    }
}

internal static class ContextPromptComposer
{
    private const int ContextLines = 12;

    public static string Compose(string prompt, ContextLevel level, EditorContextSnapshot snapshot)
    {
        var builder = new StringBuilder();
        builder.AppendLine("You are a helpful coding assistant inside a Visual Studio 2026 extension.");
        builder.AppendLine("Answer clearly and use the supplied context if it exists.");
        builder.AppendLine();
        builder.AppendLine("User prompt:");
        builder.AppendLine(prompt);
        builder.AppendLine();

        switch (level)
        {
            case ContextLevel.NoContext:
                builder.AppendLine("Context level: no context.");
                break;

            case ContextLevel.FimContext:
                builder.AppendLine("Context level: FIM context.");
                builder.AppendLine($"Active file: {snapshot.ActiveFilePath}");
                builder.AppendLine($"Cursor: line {snapshot.CursorLine}, column {snapshot.CursorColumn}");
                builder.AppendLine();
                builder.AppendLine($"Code before cursor ({ContextLines} lines max):");
                builder.AppendLine("```");
                builder.AppendLine(snapshot.FimBeforeCursor);
                builder.AppendLine("```");
                builder.AppendLine();
                builder.AppendLine($"Code after cursor ({ContextLines} lines max):");
                builder.AppendLine("```");
                builder.AppendLine(snapshot.FimAfterCursor);
                builder.AppendLine("```");
                break;

            case ContextLevel.IdeContext:
                builder.AppendLine("Context level: IDE context.");
                builder.AppendLine($"Active file: {snapshot.ActiveFilePath}");
                builder.AppendLine();
                builder.AppendLine("Selected file contents:");
                builder.AppendLine("```");
                builder.AppendLine(snapshot.ActiveFileContent);
                builder.AppendLine("```");
                builder.AppendLine();
                builder.AppendLine("Selection text:");
                builder.AppendLine("```");
                builder.AppendLine(string.IsNullOrWhiteSpace(snapshot.SelectedText) ? "(no selection)" : snapshot.SelectedText);
                builder.AppendLine("```");
                builder.AppendLine();
                builder.AppendLine("Other open tabs:");

                if (snapshot.OpenTabs.Count == 0)
                {
                    builder.AppendLine("(none)");
                }
                else
                {
                    foreach (var tab in snapshot.OpenTabs)
                    {
                        builder.AppendLine($"File: {tab.FilePath}");
                        builder.AppendLine("```");
                        builder.AppendLine(tab.Content);
                        builder.AppendLine("```");
                        builder.AppendLine();
                    }
                }
                break;
        }

        return builder.ToString();
    }
}
