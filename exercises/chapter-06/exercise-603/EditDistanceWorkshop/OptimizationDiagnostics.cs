namespace EditDistanceWorkshop;

public sealed record OptimizationDiagnostics(
    long EvaluationCount,
    long StatesExamined,
    double ElapsedMilliseconds,
    string Strategy);
