using Xunit;
using _Lib;

namespace _Lib.Tests;

public class C0Tests
{
    [Fact]
    public void M1_IdenticalInputs_ReturnsZero()
    {
        Assert.Equal(0, C0.M1("copilot", "copilot"));
    }

    [Fact]
    public void M1_FirstInputEmpty_ReturnsLengthOfSecond()
    {
        Assert.Equal(5, C0.M1("", "hello"));
    }

    [Fact]
    public void M1_SecondInputEmpty_ReturnsLengthOfFirst()
    {
        Assert.Equal(5, C0.M1("world", ""));
    }

    [Fact]
    public void M1_BothInputsEmpty_ReturnsZero()
    {
        Assert.Equal(0, C0.M1("", ""));
    }

    [Theory]
    [InlineData("kitten", "sitting", 3)]
    [InlineData("saturday", "sunday", 3)]
    [InlineData("abc", "abc", 0)]
    [InlineData("abc", "ab", 1)]
    [InlineData("a", "b", 1)]
    [InlineData("intention", "execution", 5)]
    public void M1_KnownInputs_ReturnsExpected(string a0, string a1, int v0)
    {
        Assert.Equal(v0, C0.M1(a0, a1));
    }

    [Fact]
    public void M1_SingleCharacterMatch_ReturnsZero()
    {
        Assert.Equal(0, C0.M1("x", "x"));
    }

    [Fact]
    public void M1_SingleCharacterMismatch_ReturnsOne()
    {
        Assert.Equal(1, C0.M1("a", "b"));
    }
}
