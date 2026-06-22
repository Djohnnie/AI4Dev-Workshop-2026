using ExpressionEvaluatorWorkshop;

namespace ExpressionEvaluatorWorkshop.Tests;

public sealed class ExistingCodeCharacterizationTests
{
    [Theory]
    [InlineData("1+2*3", "1 2 3 * +", 7)]
    [InlineData("(1+2)*3", "1 2 + 3 *", 9)]
    [InlineData("7-(2+1)", "7 2 1 + -", 4)]
    public void Evaluate_ExistingExpressions_PreservesCurrentBehaviour(string infix, string expectedPostfix, int expectedValue)
    {
        var result = ExpressionEvaluator.Evaluate(infix);

        Assert.Equal(expectedPostfix, result.PostfixExpression);
        Assert.Equal(expectedValue, result.Value);
    }
}
