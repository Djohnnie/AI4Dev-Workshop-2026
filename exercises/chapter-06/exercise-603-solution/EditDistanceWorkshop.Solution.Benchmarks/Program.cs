using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using EditDistanceWorkshop.Solution;

BenchmarkRunner.Run<EditDistanceComparisonBenchmarks>();

[MemoryDiagnoser]
[ShortRunJob]
public class EditDistanceComparisonBenchmarks
{
    private readonly SlowEditDistanceCalculator _slow = new();
    private readonly FastEditDistanceCalculator _fast = new();
    private readonly EditDistanceCase _input = EditDistanceSampleData.CreateBenchmarkCase();

    [Benchmark(Baseline = true)]
    public EditDistanceResult Slow()
    {
        return _slow.Calculate(_input);
    }

    [Benchmark]
    public EditDistanceResult Fast()
    {
        return _fast.Calculate(_input);
    }
}
