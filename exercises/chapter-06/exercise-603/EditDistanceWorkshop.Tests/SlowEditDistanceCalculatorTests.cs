using EditDistanceWorkshop;

namespace EditDistanceWorkshop.Tests;

public class SlowEditDistanceCalculatorTests
{
    [Fact]
    public void Calculate_ReturnsExpectedDistance_ForWorkshopScenario()
    {
        var calculator = new SlowEditDistanceCalculator();

        var result = calculator.Calculate(EditDistanceSampleData.CreateWorkshopCase());

        Assert.Equal(3, result.Distance);
        Assert.Equal("kitten", result.Source);
        Assert.Equal("sitting", result.Target);
    }

    [Fact]
    public void Calculate_ReturnsTargetLength_WhenSourceIsEmpty()
    {
        var calculator = new SlowEditDistanceCalculator();

        var result = calculator.Calculate(new EditDistanceCase(string.Empty, "trace"));

        Assert.Equal(5, result.Distance);
    }

    [Fact]
    public void Calculate_RecordsDiagnostics()
    {
        var calculator = new SlowEditDistanceCalculator();

        var result = calculator.Calculate(EditDistanceSampleData.CreateWorkshopCase());

        Assert.True(result.Diagnostics.EvaluationCount > 0);
        Assert.True(result.Diagnostics.StatesExamined > 0);
        Assert.Equal("Naive recursive Levenshtein distance", result.Diagnostics.Strategy);
    }
}
