namespace PromptPatternsPlayground.Services;

public sealed class StructuredAuditLogger
{
    public string Format(string entityId, string action) =>
        $"Audit item {{EntityId={entityId}, Action={action}}}";
}
