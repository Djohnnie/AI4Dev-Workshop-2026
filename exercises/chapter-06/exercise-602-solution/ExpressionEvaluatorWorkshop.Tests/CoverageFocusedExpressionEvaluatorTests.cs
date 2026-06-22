using ExpressionEvaluatorWorkshop;

namespace ExpressionEvaluatorWorkshop.Tests;

public sealed class CoverageFocusedExpressionEvaluatorTests
{
    [Fact]
    public void Evaluate_WithUnknownCharacter_ThrowsArgumentException()
    {
        var ex = Assert.Throws<ArgumentException>(() => ExpressionEvaluator.Evaluate("1+a"));

        Assert.Contains("Unsupported character", ex.Message);
    }

    [Fact]
    public void Evaluate_WithMismatchedParentheses_ThrowsInvalidOperationException()
    {
        var ex = Assert.Throws<InvalidOperationException>(() => ExpressionEvaluator.Evaluate("(1+2"));

        Assert.Equal("Mismatched parentheses.", ex.Message);
    }

    [Fact]
    public void Evaluate_WithDivisionByZero_ThrowsDivideByZeroException()
    {
        var ex = Assert.Throws<DivideByZeroException>(() => ExpressionEvaluator.Evaluate("8/(3-3)"));

        Assert.Equal("Division by zero is not allowed.", ex.Message);
    }

    [Fact]
    public void Evaluate_WithMissingOperand_ThrowsInvalidOperationException()
    {
        var ex = Assert.Throws<InvalidOperationException>(() => ExpressionEvaluator.Evaluate("1+"));

        Assert.Equal("The expression does not contain enough operands.", ex.Message);
    }
}
