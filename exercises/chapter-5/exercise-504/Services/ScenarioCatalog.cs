using PromptPatternsPlayground.Models;

namespace PromptPatternsPlayground.Services;

public static class ScenarioCatalog
{
    public static IReadOnlyList<ScenarioCard> All { get; } =
    [
        new ScenarioCard(
            "Comment-driven",
            "See how precise inline comments shape the implementation and refactor discussion.",
            "Services/ReleaseSummaryService.cs",
            "#file:Services/ReleaseSummaryService.cs explain how the leading comment constrains the implementation."),
        new ScenarioCard(
            "Test-first",
            "Ask Copilot for edge cases before touching business logic.",
            "Services/ReleaseWindowCalculator.cs + PromptPatternsPlayground.Tests/ReleaseWindowCalculatorTests.cs",
            "#file:Services/ReleaseWindowCalculator.cs list the tests you would want before changing this calculator."),
        new ScenarioCard(
            "Persona",
            "Use a reviewer lens to inspect security-relevant code.",
            "Services/SecurityHeadersPolicy.cs",
            "Act as a security reviewer. #file:Services/SecurityHeadersPolicy.cs what concerns or follow-up questions do you have?"),
        new ScenarioCard(
            "Stepwise",
            "Break a non-trivial workflow into a sequence of safe edits.",
            "Services/ImportWorkflowService.cs",
            "Break a refactor of #file:Services/ImportWorkflowService.cs into five small steps, then do only step 1."),
        new ScenarioCard(
            "Diff-driven",
            "Use one before/after example to guide the same transform elsewhere.",
            "Services/LegacyAuditLogger.cs, Services/StructuredAuditLogger.cs, Services/LegacyBillingLogger.cs",
            "Compare #file:Services/LegacyAuditLogger.cs and #file:Services/StructuredAuditLogger.cs. Apply the same logging transform to #file:Services/LegacyBillingLogger.cs.")
    ];
}
