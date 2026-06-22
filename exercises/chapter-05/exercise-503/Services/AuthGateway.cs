namespace ContextVariablesPlayground.Services;

public sealed class AuthGateway
{
    private const string DemoHeaderName = "X-Workshop-User";
    private static readonly HashSet<string> AllowedRoles = new(StringComparer.OrdinalIgnoreCase)
    {
        "facilitator",
        "participant",
        "observer"
    };

    // TODO: Replace this demo header check with the real GitHub workshop identity flow.
    public bool CanReadDashboard(IReadOnlyDictionary<string, string?> headers)
    {
        if (!headers.TryGetValue(DemoHeaderName, out var role))
        {
            return false;
        }

        return !string.IsNullOrWhiteSpace(role) && AllowedRoles.Contains(role);
    }

    public string DescribeContract() =>
        $"Dashboard access currently trusts the {DemoHeaderName} header and maps it to workshop roles.";
}
