using PromptPatternsPlayground.Services;

namespace PromptPatternsPlayground.Endpoints;

public static class PatternLabEndpoints
{
    public static IEndpointRouteBuilder MapPatternLabEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/scenarios", () => ScenarioCatalog.All);

        app.MapGet("/api/release-windows", (ReleaseWindowCalculator calculator) => new[]
        {
            calculator.Calculate("Hotfix", DateOnly.FromDateTime(DateTime.Today.AddDays(1)), openIncidents: 0, hasSchemaChange: false),
            calculator.Calculate("Monthly rollout", DateOnly.FromDateTime(DateTime.Today.AddDays(3)), openIncidents: 4, hasSchemaChange: false),
            calculator.Calculate("Ledger migration", DateOnly.FromDateTime(DateTime.Today.AddDays(5)), openIncidents: 1, hasSchemaChange: true)
        });

        app.MapGet("/api/headers-preview", (SecurityHeadersPolicy policy) => policy.BuildDefaultHeaders());

        app.MapGet("/api/log-samples", (
            LegacyAuditLogger legacyAuditLogger,
            StructuredAuditLogger structuredAuditLogger,
            LegacyBillingLogger legacyBillingLogger) => new
        {
            auditBefore = legacyAuditLogger.Format("order-77", "approved"),
            auditAfter = structuredAuditLogger.Format("order-77", "approved"),
            billingNeedsRefactor = legacyBillingLogger.Format("invoice-42", "sent")
        });

        app.MapGet("/api/release-summary", (ReleaseSummaryService service) =>
            service.BuildSummary("Sprint 18", ["Auth hardening", "Ledger retry fix", "Prompt arena polish"]));

        app.MapGet("/api/import-preview", (ImportWorkflowService service) =>
            service.PlanImport("nightly-orders.csv", dryRun: true, hasSchemaDrift: false));

        return app;
    }
}
