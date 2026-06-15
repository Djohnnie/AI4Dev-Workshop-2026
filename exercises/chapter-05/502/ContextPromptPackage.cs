using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace ContextPromptExtension;

[PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
[InstalledProductRegistration("Context Prompt Demo", "Visual Studio prompt-context demo", "1.0")]
[ProvideMenuResource("Menus.ctmenu", 1)]
[ProvideBindingPath]
[Guid(PackageGuidString)]
public sealed class ContextPromptPackage : AsyncPackage
{
    public const string PackageGuidString = "2D3E6AC3-4B4B-4E3D-9A6F-7E0F0B82D5B1";

    protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
    {
        await base.InitializeAsync(cancellationToken, progress);
        await ContextPromptCommand.InitializeAsync(this);
    }
}
