namespace EditDistanceWorkshop.Solution;

public sealed record EditDistanceResult(
    string Source,
    string Target,
    int Distance,
    OptimizationDiagnostics Diagnostics);
