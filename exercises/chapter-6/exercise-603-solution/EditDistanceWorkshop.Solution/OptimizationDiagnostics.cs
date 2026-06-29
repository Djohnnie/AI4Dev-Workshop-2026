namespace EditDistanceWorkshop.Solution;

public sealed record OptimizationDiagnostics(
    long EvaluationCount,
    long StatesExamined,
    double ElapsedMilliseconds,
    string Strategy);
