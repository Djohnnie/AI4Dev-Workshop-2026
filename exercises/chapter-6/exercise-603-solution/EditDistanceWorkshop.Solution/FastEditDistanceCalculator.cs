using System.Diagnostics;

namespace EditDistanceWorkshop.Solution;

public sealed class FastEditDistanceCalculator(Action<string>? log = null)
{
    private readonly Action<string>? _log = log;

    public EditDistanceResult Calculate(EditDistanceCase input)
    {
        ArgumentNullException.ThrowIfNull(input);

        EditDistanceTelemetry.Requests.Add(1);
        _log?.Invoke($"Starting dynamic-programming edit distance for '{input.Source}' -> '{input.Target}'.");

        var source = input.Source;
        var target = input.Target;
        var rows = source.Length + 1;
        var columns = target.Length + 1;

        var stopwatch = Stopwatch.StartNew();
        long evaluationCount = 0;
        long statesExamined = 0;

        var table = new int[rows, columns];

        for (var row = 0; row < rows; row++)
        {
            table[row, 0] = row;
        }

        for (var column = 0; column < columns; column++)
        {
            table[0, column] = column;
        }

        for (var row = 1; row < rows; row++)
        {
            for (var column = 1; column < columns; column++)
            {
                evaluationCount++;
                statesExamined++;

                var substitutionCost = source[row - 1] == target[column - 1] ? 0 : 1;
                var deleteCost = table[row - 1, column] + 1;
                var insertCost = table[row, column - 1] + 1;
                var replaceCost = table[row - 1, column - 1] + substitutionCost;

                table[row, column] = Math.Min(deleteCost, Math.Min(insertCost, replaceCost));
            }
        }

        stopwatch.Stop();
        var distance = table[source.Length, target.Length];

        EditDistanceTelemetry.RecursiveCalls.Add(evaluationCount);
        EditDistanceTelemetry.StatesExamined.Add(statesExamined);
        EditDistanceTelemetry.DurationMilliseconds.Record(stopwatch.Elapsed.TotalMilliseconds);

        _log?.Invoke(
            $"Finished dynamic programming. Distance={distance}, TableCells={evaluationCount}, States={statesExamined}, Duration={stopwatch.Elapsed.TotalMilliseconds:F2} ms.");

        return new EditDistanceResult(
            source,
            target,
            distance,
            new OptimizationDiagnostics(
                evaluationCount,
                statesExamined,
                stopwatch.Elapsed.TotalMilliseconds,
                "Bottom-up dynamic-programming Levenshtein distance"));
    }
}
