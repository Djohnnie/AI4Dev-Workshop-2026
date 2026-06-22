namespace PromptPatternsPlayground.Services;

public sealed class SecurityHeadersPolicy
{
    public IReadOnlyDictionary<string, string> BuildDefaultHeaders()
    {
        return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["X-Frame-Options"] = "DENY",
            ["X-Content-Type-Options"] = "nosniff",
            ["Referrer-Policy"] = "strict-origin-when-cross-origin",
            ["Content-Security-Policy"] = "default-src 'self'; style-src 'self' 'unsafe-inline'; script-src 'self'"
        };
    }
}
