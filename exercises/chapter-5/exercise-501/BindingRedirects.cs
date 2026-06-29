using Microsoft.VisualStudio.Shell;

[assembly: ProvideBindingRedirection(
    AssemblyName = "OpenAI",
    PublicKeyToken = "b4187f3e65366280",
    Culture = "neutral",
    OldVersionLowerBound = "0.0.0.0",
    OldVersionUpperBound = "2.10.0.0",
    NewVersion = "2.10.0.0")]
[assembly: ProvideBindingRedirection(
    AssemblyName = "System.ClientModel",
    PublicKeyToken = "92742159e12e44c8",
    Culture = "neutral",
    OldVersionLowerBound = "0.0.0.0",
    OldVersionUpperBound = "1.12.0.0",
    NewVersion = "1.12.0.0")]
