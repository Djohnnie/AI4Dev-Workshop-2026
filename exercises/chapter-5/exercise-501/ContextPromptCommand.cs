using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System.ComponentModel.Design;
using System.Windows.Interop;

namespace ContextPromptExtension;

internal sealed class ContextPromptCommand
{
    public const int CommandId = 0x0100;
    public static readonly Guid CommandSet = new("D0F4C6C0-7C4D-4D36-8A8D-1D1BCEB7B7DE");

    private readonly AsyncPackage package;

    private ContextPromptCommand(AsyncPackage package)
    {
        this.package = package;
    }

    public static async Task InitializeAsync(AsyncPackage package)
    {
        await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

        var commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService
            ?? throw new InvalidOperationException("Could not load the Visual Studio command service.");

        var commandId = new CommandID(CommandSet, CommandId);
        var command = new ContextPromptCommand(package);
        var menuItem = new OleMenuCommand(command.Execute, commandId);
        commandService.AddCommand(menuItem);
    }

    private void Execute(object? sender, EventArgs e)
    {
        _ = package.JoinableTaskFactory.RunAsync(ExecuteAsync);
    }

    private async Task ExecuteAsync()
    {
        try
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            var dialog = new ContextPromptDialog(new ContextPromptOrchestrator(package));

            if (await package.GetServiceAsync(typeof(SVsUIShell)) is IVsUIShell shell &&
                shell.GetDialogOwnerHwnd(out var ownerHwnd) == VSConstants.S_OK)
            {
                new WindowInteropHelper(dialog) { Owner = ownerHwnd };
            }

            dialog.ShowDialog();
        }
        catch (Exception ex)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            VsShellUtilities.ShowMessageBox(
                package,
                ex.Message,
                "Context Prompt Demo",
                OLEMSGICON.OLEMSGICON_CRITICAL,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }
    }
}
