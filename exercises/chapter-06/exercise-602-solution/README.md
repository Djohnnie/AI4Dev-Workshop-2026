# Exercise 602 Solution — Expression Evaluator Test Lab

This folder contains a possible finished solution for the testing exercise.

It uses a small **infix/postfix expression evaluator** as the code under test and demonstrates four testing angles:

1. tests on existing code
2. TDD-style tests
3. BDD with Reqnroll
4. coverage-focused tests

## Structure

```text
exercise-602-solution/
├── README.md
├── ExpressionEvaluatorWorkshop/
│   ├── ExpressionEvaluatorWorkshop.csproj
│   ├── Program.cs
│   └── ExpressionEvaluator.cs
└── ExpressionEvaluatorWorkshop.Tests/
    ├── ExpressionEvaluatorWorkshop.Tests.csproj
    ├── ExistingCodeCharacterizationTests.cs
    ├── TddExpressionEvaluatorTests.cs
    ├── ExpressionEvaluatorBDD.feature
    ├── ExpressionEvaluatorFeatureSteps.cs
    └── CoverageFocusedExpressionEvaluatorTests.cs
```

## Run the app

```bash
dotnet run --project ExpressionEvaluatorWorkshop/ExpressionEvaluatorWorkshop.csproj
```

## Run the tests

```bash
dotnet test ExpressionEvaluatorWorkshop.Tests/ExpressionEvaluatorWorkshop.Tests.csproj
```

## Generate coverage

```bash
dotnet test ExpressionEvaluatorWorkshop.Tests/ExpressionEvaluatorWorkshop.Tests.csproj --collect:"XPlat Code Coverage"
```

## Reqnroll note

The BDD portion of the solution uses **Reqnroll** directly:

- scenarios live in `ExpressionEvaluatorBDD.feature`
- bindings live in `ExpressionEvaluatorFeatureSteps.cs`

That keeps the BDD example distinct from the regular xUnit tests and mirrors the slide guidance about Reqnroll.
