# Exercise 603 Solution — Optimize Edit Distance

This folder contains a possible finished solution for the edit distance performance lab.

The key improvement is replacing the naive recursive **Levenshtein distance** implementation with a **bottom-up dynamic-programming calculator** that keeps the same behaviour while dramatically reducing repeated work.

## Structure

```text
exercise-603-solution/
├── README.md
├── EditDistanceWorkshop.Solution/
│   ├── EditDistanceWorkshop.Solution.csproj
│   ├── Program.cs
│   ├── EditDistanceCase.cs
│   ├── EditDistanceResult.cs
│   ├── EditDistanceSampleData.cs
│   ├── EditDistanceTelemetry.cs
│   ├── OptimizationDiagnostics.cs
│   ├── SlowEditDistanceCalculator.cs
│   └── FastEditDistanceCalculator.cs
├── EditDistanceWorkshop.Solution.Tests/
│   ├── EditDistanceWorkshop.Solution.Tests.csproj
│   └── OptimizerTests.cs
└── EditDistanceWorkshop.Solution.Benchmarks/
    ├── EditDistanceWorkshop.Solution.Benchmarks.csproj
    └── Program.cs
```

## Run the app

```bash
dotnet run --project EditDistanceWorkshop.Solution/EditDistanceWorkshop.Solution.csproj
```

## Run the tests

```bash
dotnet test EditDistanceWorkshop.Solution.Tests/EditDistanceWorkshop.Solution.Tests.csproj
```

## Run the benchmarks

```bash
dotnet run --project EditDistanceWorkshop.Solution.Benchmarks/EditDistanceWorkshop.Solution.Benchmarks.csproj -c Release
```

## Improvement summary

The solution keeps the public behaviour but improves performance by:

1. replacing repeated recursive exploration with dynamic programming
2. evaluating each `(sourceIndex, targetIndex)` state once
3. computing the full answer from a compact matrix instead of recomputing subproblems
4. preserving logs, metrics, and deterministic output for easy comparison
