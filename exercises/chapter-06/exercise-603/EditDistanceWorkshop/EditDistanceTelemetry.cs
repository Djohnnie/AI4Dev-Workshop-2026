using System.Diagnostics.Metrics;

namespace EditDistanceWorkshop;

public static class EditDistanceTelemetry
{
    public const string MeterName = "EditDistanceWorkshop";

    private static readonly Meter Meter = new(MeterName, "1.0.0");

    public static readonly Counter<long> Requests = Meter.CreateCounter<long>("editdistance.requests");
    public static readonly Counter<long> RecursiveCalls = Meter.CreateCounter<long>("editdistance.recursive_calls");
    public static readonly Counter<long> StatesExamined = Meter.CreateCounter<long>("editdistance.states_examined");
    public static readonly Histogram<double> DurationMilliseconds = Meter.CreateHistogram<double>("editdistance.duration_ms");
}
