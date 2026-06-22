using System.Diagnostics.Metrics;
using EditDistanceWorkshop.Solution;

var input = EditDistanceSampleData.CreateBenchmarkCase();
var metricSnapshot = new Dictionary<string, double>(StringComparer.Ordinal);

Console.WriteLine("Slow baseline");
var slowResult = RunAndPrint(
    "slow",
    () => new SlowEditDistanceCalculator(message => Console.WriteLine($"[slow] {message}")).Calculate(input));

Console.WriteLine();
Console.WriteLine("Optimized solution");
var fastResult = RunAndPrint(
    "fast",
    () => new FastEditDistanceCalculator(message => Console.WriteLine($"[fast] {message}")).Calculate(input));

Console.WriteLine();
Console.WriteLine($"Distance parity: {slowResult.Distance == fastResult.Distance}");
Console.WriteLine($"Call reduction:  {slowResult.Diagnostics.EvaluationCount} -> {fastResult.Diagnostics.EvaluationCount}");

EditDistanceResult RunAndPrint(string label, Func<EditDistanceResult> run)
{
    using var listener = CreateListener();
    var result = run();

    Console.WriteLine($"Source:   {result.Source}");
    Console.WriteLine($"Target:   {result.Target}");
    Console.WriteLine($"Distance: {result.Distance}");
    Console.WriteLine($"Strategy: {result.Diagnostics.Strategy}");
    Console.WriteLine($"Calls:    {result.Diagnostics.EvaluationCount}");
    Console.WriteLine($"States:   {result.Diagnostics.StatesExamined}");
    Console.WriteLine($"Elapsed:  {result.Diagnostics.ElapsedMilliseconds:F2} ms");

    foreach (var metric in metricSnapshot.OrderBy(entry => entry.Key))
    {
        Console.WriteLine($"- {label}:{metric.Key}: {metric.Value:F2}");
    }

    metricSnapshot.Clear();
    return result;
}

MeterListener CreateListener()
{
    var listener = new MeterListener();

    listener.InstrumentPublished = (instrument, meterListener) =>
    {
        if (instrument.Meter.Name == EditDistanceTelemetry.MeterName)
        {
            meterListener.EnableMeasurementEvents(instrument);
        }
    };

    listener.SetMeasurementEventCallback<long>((instrument, measurement, _, _) =>
    {
        metricSnapshot[instrument.Name] = metricSnapshot.TryGetValue(instrument.Name, out var current)
            ? current + measurement
            : measurement;
    });

    listener.SetMeasurementEventCallback<double>((instrument, measurement, _, _) =>
    {
        metricSnapshot[instrument.Name] = measurement;
    });

    listener.Start();
    return listener;
}
