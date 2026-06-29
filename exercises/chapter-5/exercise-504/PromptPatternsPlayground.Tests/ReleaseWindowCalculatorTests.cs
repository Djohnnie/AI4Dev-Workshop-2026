using PromptPatternsPlayground.Services;

namespace PromptPatternsPlayground.Tests;

public sealed class ReleaseWindowCalculatorTests
{
    private readonly ReleaseWindowCalculator _calculator = new();

    [Fact]
    public void Calculate_WithSchemaChange_RequiresManualReview()
    {
        var decision = _calculator.Calculate("Ledger migration", new DateOnly(2026, 6, 24), openIncidents: 0, hasSchemaChange: true);

        Assert.Equal("Manual review", decision.Strategy);
    }

    [Fact]
    public void Calculate_OnWeekend_DelaysToWeekday()
    {
        var decision = _calculator.Calculate("Weekend deploy", new DateOnly(2026, 6, 27), openIncidents: 0, hasSchemaChange: false);

        Assert.Equal("Delay to weekday", decision.Strategy);
    }

    [Fact]
    public void Calculate_WithBusyIncidentQueue_StabilizesFirst()
    {
        var decision = _calculator.Calculate("Dashboard polish", new DateOnly(2026, 6, 25), openIncidents: 4, hasSchemaChange: false);

        Assert.Equal("Stabilize first", decision.Strategy);
    }

    [Fact]
    public void Calculate_WithNormalWeekdayChange_AutoDeploys()
    {
        var decision = _calculator.Calculate("Copy tweak", new DateOnly(2026, 6, 25), openIncidents: 1, hasSchemaChange: false);

        Assert.Equal("Auto-deploy", decision.Strategy);
    }
}
