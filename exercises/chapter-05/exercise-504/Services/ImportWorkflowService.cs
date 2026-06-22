using PromptPatternsPlayground.Models;

namespace PromptPatternsPlayground.Services;

public sealed class ImportWorkflowService
{
    public ImportPlan PlanImport(string fileName, bool dryRun, bool hasSchemaDrift)
    {
        var mode = dryRun ? "preview" : "execute";
        var validationAction = hasSchemaDrift
            ? "Pause and regenerate the column map before any writes."
            : "Validate required columns, row count, and tenant markers.";

        var executionAction = dryRun
            ? "Produce a sample of transformed rows without writing to storage."
            : "Write valid rows in batches and capture failures for replay.";

        var followUpAction = hasSchemaDrift
            ? "Notify the integration owner with the schema diff."
            : "Emit the import summary and archive the source file.";

        return new ImportPlan(fileName, mode, validationAction, executionAction, followUpAction);
    }
}
