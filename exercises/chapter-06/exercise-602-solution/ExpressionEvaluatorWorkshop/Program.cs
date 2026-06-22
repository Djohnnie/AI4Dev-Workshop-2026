using ExpressionEvaluatorWorkshop;

ExpressionScenario[] scenarios =
[
    new("1+2*3", "1 2 3 * +", 7),
    new("(1+2)*3", "1 2 + 3 *", 9),
    new("12 + 8 / 2", "12 8 2 / +", 16),
    new("7-(2+1)", "7 2 1 + -", 4)
];

Console.WriteLine("=== Exercise 603 Solution: Expression Evaluator Test Lab ===");
Console.WriteLine("Use this app as the subject for characterization tests, TDD, BDD scenarios, and coverage-driven follow-up.");
Console.WriteLine();

foreach (var scenario in scenarios)
{
    var result = ExpressionEvaluator.Evaluate(scenario.InfixExpression);
    var status = result.PostfixExpression == scenario.ExpectedPostfix && result.Value == scenario.ExpectedValue
        ? "OK"
        : "CHECK";

    Console.WriteLine($"  {scenario.InfixExpression,-14} postfix: {result.PostfixExpression,-18} value: {result.Value,-4} [{status}]");
}

internal readonly record struct ExpressionScenario(string InfixExpression, string ExpectedPostfix, int ExpectedValue);
