using ExpressionEvaluatorLab;

ExpressionScenario[] scenarios =
[
    new("1+2*3", "1 2 3 * +", 7),
    new("(1+2)*3", "1 2 + 3 *", 9),
    new("8/2+5", "8 2 / 5 +", 9),
    new("7-(2+1)", "7 2 1 + -", 4)
];

Console.WriteLine("=== Exercise 302: Infix and postfix with Ask mode ===");
Console.WriteLine("Use Copilot to explain the algorithm before you trust the code.");
Console.WriteLine();

foreach (var scenario in scenarios)
{
    RunScenario(scenario);
}

Console.WriteLine();
Console.WriteLine("If you cannot explain precedence, parentheses, and the postfix value stack, keep asking Copilot to teach it back.");

static void RunScenario(ExpressionScenario scenario)
{
    try
    {
        var actual = ExpressionEvaluator.Evaluate(scenario.InfixExpression);
        var postfixOk = actual.PostfixExpression == scenario.ExpectedPostfix;
        var valueOk = actual.Value == scenario.ExpectedValue;
        var status = postfixOk && valueOk ? "OK" : "CHECK";

        Console.WriteLine($"  {scenario.InfixExpression,-10} postfix: {actual.PostfixExpression,-15} value: {actual.Value} [{status}]");
    }
    catch (NotImplementedException)
    {
        Console.WriteLine("  [stub not completed yet - use Copilot Ask mode in ExpressionEvaluator.cs]");
    }
}

internal readonly record struct ExpressionScenario(
    string InfixExpression,
    string ExpectedPostfix,
    int ExpectedValue);
