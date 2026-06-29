using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using EditDistanceWorkshop;

BenchmarkRunner.Run<SlowEditDistanceBenchmarks>();

[MemoryDiagnoser]
[ShortRunJob]
public class SlowEditDistanceBenchmarks
{
    private readonly SlowEditDistanceCalculator _calculator = new();
    private readonly EditDistanceCase _input = EditDistanceSampleData.CreateBenchmarkCase();

    [Benchmark]
    public EditDistanceResult Calculate()
    {
        return _calculator.Calculate(_input);
    }
}
