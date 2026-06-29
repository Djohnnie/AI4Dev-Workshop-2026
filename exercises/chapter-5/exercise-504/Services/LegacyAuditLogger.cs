namespace PromptPatternsPlayground.Services;

public sealed class LegacyAuditLogger
{
    public string Format(string entityId, string action) =>
        "Audit item " + entityId + " changed to " + action;
}
