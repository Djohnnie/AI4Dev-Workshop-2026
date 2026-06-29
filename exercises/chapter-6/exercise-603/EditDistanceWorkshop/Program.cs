using System.Diagnostics.Metrics;
using EditDistanceWorkshop;

var input = EditDistanceSampleData.CreateWorkshopCase();
var metricSnapshot = new Dictionary<string, double>(StringComparer.Ordinal);

using var listener = CreateListener();
var calculator = new SlowEditDistanceCalculator(message => Console.WriteLine($"[log] {message}"));
var result = calculator.Calculate(input);

Console.WriteLine();
Console.WriteLine($"Source:   {result.Source}");
Console.WriteLine($"Target:   {result.Target}");
Console.WriteLine($"Distance: {result.Distance}");
Console.WriteLine($"Strategy: {result.Diagnostics.Strategy}");
Console.WriteLine($"Calls:    {result.Diagnostics.EvaluationCount}");
Console.WriteLine($"States:   {result.Diagnostics.StatesExamined}");
Console.WriteLine($"Elapsed:  {result.Diagnostics.ElapsedMilliseconds:F2} ms");

Console.WriteLine();
Console.WriteLine("Metrics");
foreach (var metric in metricSnapshot.OrderBy(entry => entry.Key))
{
    Console.WriteLine($"- {metric.Key}: {metric.Value:F2}");
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
