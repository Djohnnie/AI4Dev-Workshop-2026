using Xunit;

namespace Factorial.Tests;

public class CalculatorTests
{
    [Fact]
    public void CalculateIteratively_Zero_ReturnsOne()
    {
        Assert.Equal(1, Factorial.CalculateIteratively(0));
    }

    [Fact]
    public void CalculateIteratively_One_ReturnsOne()
    {
        Assert.Equal(1, Factorial.CalculateIteratively(1));
    }

    [Theory]
    [InlineData(2, 2)]
    [InlineData(3, 6)]
    [InlineData(4, 24)]
    [InlineData(5, 120)]
    [InlineData(10, 3628800)]
    public void CalculateIteratively_PositiveNumber_ReturnsFactorial(int input, long expected)
    {
        Assert.Equal(expected, Factorial.CalculateIteratively(input));
    }
}