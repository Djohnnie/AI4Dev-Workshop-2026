using System.Diagnostics;

namespace EditDistanceWorkshop.Solution;

public sealed class SlowEditDistanceCalculator(Action<string>? log = null)
{
    private readonly Action<string>? _log = log;

    public EditDistanceResult Calculate(EditDistanceCase input)
    {
        ArgumentNullException.ThrowIfNull(input);

        EditDistanceTelemetry.Requests.Add(1);
        _log?.Invoke($"Starting naive recursive edit distance for '{input.Source}' -> '{input.Target}'.");

        var stopwatch = Stopwatch.StartNew();
        long recursiveCalls = 0;
        long statesExamined = 0;

        var distance = Explore(input.Source, input.Target, 0, 0, ref recursiveCalls, ref statesExamined);

        stopwatch.Stop();
        EditDistanceTelemetry.RecursiveCalls.Add(recursiveCalls);
        EditDistanceTelemetry.StatesExamined.Add(statesExamined);
        EditDistanceTelemetry.DurationMilliseconds.Record(stopwatch.Elapsed.TotalMilliseconds);

        _log?.Invoke(
            $"Finished naive recursion. Distance={distance}, RecursiveCalls={recursiveCalls}, States={statesExamined}, Duration={stopwatch.Elapsed.TotalMilliseconds:F2} ms.");

        return new EditDistanceResult(
            input.Source,
            input.Target,
            distance,
            new OptimizationDiagnostics(
                recursiveCalls,
                statesExamined,
                stopwatch.Elapsed.TotalMilliseconds,
                "Naive recursive Levenshtein distance"));
    }

    private static int Explore(
        string source,
        string target,
        int sourceIndex,
        int targetIndex,
        ref long recursiveCalls,
        ref long statesExamined)
    {
        recursiveCalls++;
        statesExamined++;

        if (sourceIndex == source.Length)
        {
            return target.Length - targetIndex;
        }

        if (targetIndex == target.Length)
        {
            return source.Length - sourceIndex;
        }

        if (source[sourceIndex] == target[targetIndex])
        {
            return Explore(source, target, sourceIndex + 1, targetIndex + 1, ref recursiveCalls, ref statesExamined);
        }

        var deleteCost = 1 + Explore(source, target, sourceIndex + 1, targetIndex, ref recursiveCalls, ref statesExamined);
        var insertCost = 1 + Explore(source, target, sourceIndex, targetIndex + 1, ref recursiveCalls, ref statesExamined);
        var replaceCost = 1 + Explore(source, target, sourceIndex + 1, targetIndex + 1, ref recursiveCalls, ref statesExamined);

        return Math.Min(deleteCost, Math.Min(insertCost, replaceCost));
    }
}
