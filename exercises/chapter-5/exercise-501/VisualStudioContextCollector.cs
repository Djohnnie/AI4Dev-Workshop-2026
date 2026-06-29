using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System.Runtime.InteropServices;
using System.Text;

namespace ContextPromptExtension;

internal sealed class VisualStudioContextCollector
{
    private const int FimLines = 12;
    private readonly AsyncPackage package;

    public VisualStudioContextCollector(AsyncPackage package)
    {
        this.package = package;
    }

    public async Task<EditorContextSnapshot> CollectAsync(ContextLevel level, CancellationToken cancellationToken)
    {
        if (level == ContextLevel.NoContext)
        {
            return EditorContextSnapshot.Empty;
        }

        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
        var dte = await package.GetServiceAsync(typeof(DTE)) as DTE
            ?? throw new InvalidOperationException("Could not access Visual Studio's automation model.");

        return CollectOnUiThread(level, dte);
    }

    private EditorContextSnapshot CollectOnUiThread(ContextLevel level, DTE dte)
    {
        ThreadHelper.ThrowIfNotOnUIThread();

        var activeDocument = dte.ActiveDocument
            ?? throw new InvalidOperationException("Open a text file in the editor before running the command.");

        var textDocument = GetTextDocument(activeDocument)
            ?? throw new InvalidOperationException("The active document is not a text document.");

        var activeFilePath = string.IsNullOrWhiteSpace(activeDocument.FullName)
            ? activeDocument.Name
            : activeDocument.FullName;

        var activeFileContent = ReadDocumentText(textDocument);
        var selection = textDocument.Selection;
        var cursorLine = selection.ActivePoint.Line;
        var cursorColumn = selection.ActivePoint.LineCharOffset;
        var selectedText = selection.Text ?? string.Empty;

        var fimContext = BuildFimContext(activeFileContent, cursorLine, cursorColumn);

        var openTabs = new List<DocumentSnapshot>();
        foreach (Document document in dte.Documents)
        {
            var textDoc = GetTextDocument(document);
            if (textDoc is null)
            {
                continue;
            }

            var path = string.IsNullOrWhiteSpace(document.FullName) ? document.Name : document.FullName;
            if (string.Equals(path, activeFilePath, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            openTabs.Add(new DocumentSnapshot(path, ReadDocumentText(textDoc)));
        }

        return new EditorContextSnapshot(
            activeFilePath,
            activeFileContent,
            selectedText,
            cursorLine,
            cursorColumn,
            fimContext.BeforeCursor,
            fimContext.AfterCursor,
            openTabs);
    }

    private static TextDocument? GetTextDocument(Document document)
    {
        ThreadHelper.ThrowIfNotOnUIThread();

        try
        {
            return document.Object("TextDocument") as TextDocument;
        }
        catch (COMException)
        {
            return null;
        }
    }

    private static string ReadDocumentText(TextDocument textDocument)
    {
        ThreadHelper.ThrowIfNotOnUIThread();

        var editPoint = textDocument.StartPoint.CreateEditPoint();
        var charCount = (int)(textDocument.EndPoint.AbsoluteCharOffset - textDocument.StartPoint.AbsoluteCharOffset);
        return editPoint.GetText(charCount);
    }

    private static FimContextSnippet BuildFimContext(string content, int cursorLine, int cursorColumn)
    {
        var lines = SplitLines(content);
        if (lines.Length == 0)
        {
            return new FimContextSnippet(string.Empty, string.Empty);
        }

        var currentLineIndex = Clamp(cursorLine - 1, 0, lines.Length - 1);
        var currentLine = lines[currentLineIndex];
        var cursorIndex = Clamp(cursorColumn - 1, 0, currentLine.Length);
        var beforeLineStart = Math.Max(0, currentLineIndex - FimLines);
        var afterLineEnd = Math.Min(lines.Length - 1, currentLineIndex + FimLines);

        var before = new StringBuilder();
        for (var i = beforeLineStart; i < currentLineIndex; i++)
        {
            before.AppendLine($"{i + 1}: {lines[i]}");
        }

        before.AppendLine($"{currentLineIndex + 1}: {currentLine[..cursorIndex]}");

        var after = new StringBuilder();
        after.AppendLine($"{currentLineIndex + 1}: {currentLine[cursorIndex..]}");
        for (var i = currentLineIndex + 1; i <= afterLineEnd; i++)
        {
            after.AppendLine($"{i + 1}: {lines[i]}");
        }

        return new FimContextSnippet(before.ToString().TrimEnd(), after.ToString().TrimEnd());
    }

    private static string[] SplitLines(string text)
    {
        return text
            .Replace("\r\n", "\n")
            .Replace("\r", "\n")
            .Split('\n');
    }

    private static int Clamp(int value, int min, int max)
    {
        if (value < min)
        {
            return min;
        }

        if (value > max)
        {
            return max;
        }

        return value;
    }

    private sealed record FimContextSnippet(string BeforeCursor, string AfterCursor);
}

internal sealed record DocumentSnapshot(string FilePath, string Content);

internal sealed record EditorContextSnapshot(
    string ActiveFilePath,
    string ActiveFileContent,
    string SelectedText,
    int CursorLine,
    int CursorColumn,
    string FimBeforeCursor,
    string FimAfterCursor,
    IReadOnlyList<DocumentSnapshot> OpenTabs)
{
    public static EditorContextSnapshot Empty { get; } = new(
        string.Empty,
        string.Empty,
        string.Empty,
        0,
        0,
        string.Empty,
        string.Empty,
        Array.Empty<DocumentSnapshot>());
}
