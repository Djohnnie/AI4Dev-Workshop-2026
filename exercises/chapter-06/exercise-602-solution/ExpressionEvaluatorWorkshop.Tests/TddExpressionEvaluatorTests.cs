using ExpressionEvaluatorWorkshop;

namespace ExpressionEvaluatorWorkshop.Tests;

public sealed class TddExpressionEvaluatorTests
{
    [Fact]
    public void Evaluate_WithSpacesAndMultiDigitNumbers_ReturnsExpectedResult()
    {
        var result = ExpressionEvaluator.Evaluate("12 + 8 / 2");

        Assert.Equal("12 8 2 / +", result.PostfixExpression);
        Assert.Equal(16, result.Value);
    }

    [Fact]
    public void Evaluate_WithNestedGrouping_RespectsParenthesesBeforeMultiplication()
    {
        var result = ExpressionEvaluator.Evaluate("2*(3+4)");

        Assert.Equal("2 3 4 + *", result.PostfixExpression);
        Assert.Equal(14, result.Value);
    }
}
