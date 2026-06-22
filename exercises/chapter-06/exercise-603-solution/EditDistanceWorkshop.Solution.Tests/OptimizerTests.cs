using EditDistanceWorkshop.Solution;

namespace EditDistanceWorkshop.Solution.Tests;

public class OptimizerTests
{
    [Fact]
    public void FastCalculator_ReturnsExpectedDistance_ForWorkshopScenario()
    {
        var calculator = new FastEditDistanceCalculator();

        var result = calculator.Calculate(EditDistanceSampleData.CreateWorkshopCase());

        Assert.Equal(3, result.Distance);
        Assert.Equal("kitten", result.Source);
        Assert.Equal("sitting", result.Target);
    }

    [Fact]
    public void FastCalculator_MatchesSlowCalculator_OnBenchmarkScenario()
    {
        var input = EditDistanceSampleData.CreateBenchmarkCase();
        var slowResult = new SlowEditDistanceCalculator().Calculate(input);
        var fastResult = new FastEditDistanceCalculator().Calculate(input);

        Assert.Equal(slowResult.Distance, fastResult.Distance);
        Assert.Equal(input.Source, fastResult.Source);
        Assert.Equal(input.Target, fastResult.Target);
    }

    [Fact]
    public void FastCalculator_UsesFewerEvaluations_ThanSlowCalculator()
    {
        var input = EditDistanceSampleData.CreateBenchmarkCase();
        var slowResult = new SlowEditDistanceCalculator().Calculate(input);
        var fastResult = new FastEditDistanceCalculator().Calculate(input);

        Assert.True(fastResult.Diagnostics.EvaluationCount < slowResult.Diagnostics.EvaluationCount);
        Assert.Equal("Bottom-up dynamic-programming Levenshtein distance", fastResult.Diagnostics.Strategy);
    }
}
