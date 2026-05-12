using Xunit;

namespace Palindrome.Tests;

public class CheckerTests
{
    [Theory]
    [InlineData("racecar")]
    [InlineData("level")]
    [InlineData("madam")]
    [InlineData("A")]
    [InlineData("")]
    public void IsPalindrome_PalindromeInput_ReturnsTrue(string input)
    {
        Assert.True(Checker.IsPalindrome(input));
    }

    [Theory]
    [InlineData("hello")]
    [InlineData("world")]
    [InlineData("copilot")]
    public void IsPalindrome_NonPalindromeInput_ReturnsFalse(string input)
    {
        Assert.False(Checker.IsPalindrome(input));
    }

    [Theory]
    [InlineData("Racecar")]
    [InlineData("Level")]
    [InlineData("Madam")]
    public void IsPalindrome_MixedCaseInput_ReturnsTrue(string input)
    {
        Assert.True(Checker.IsPalindrome(input));
    }
}
