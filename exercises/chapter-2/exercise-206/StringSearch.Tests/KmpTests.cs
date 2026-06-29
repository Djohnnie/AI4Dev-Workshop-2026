using Xunit;
using StringSearch;

namespace StringSearch.Tests;

public class KmpTests
{
    [Fact]
    public void Search_SingleOccurrence_ReturnsCorrectIndex()
    {
        Assert.Equal(new[] { 3 }, Kmp.Search("abcdabcabc", "dabc"));
    }

    [Fact]
    public void Search_MultipleOccurrences_ReturnsAllIndices()
    {
        Assert.Equal(new[] { 0, 5 }, Kmp.Search("abcxxabcxx", "abcx"));
    }

    [Fact]
    public void Search_OverlappingOccurrences_ReturnsAllIndices()
    {
        Assert.Equal(new[] { 0, 1, 2 }, Kmp.Search("aaaa", "aa"));
    }

    [Fact]
    public void Search_PatternNotFound_ReturnsEmpty()
    {
        Assert.Empty(Kmp.Search("hello world", "xyz"));
    }

    [Fact]
    public void Search_EmptyPattern_ReturnsEmpty()
    {
        Assert.Empty(Kmp.Search("hello", ""));
    }

    [Fact]
    public void Search_PatternEqualsText_ReturnsSingleZeroIndex()
    {
        Assert.Equal(new[] { 0 }, Kmp.Search("abc", "abc"));
    }

    [Fact]
    public void Search_PatternLongerThanText_ReturnsEmpty()
    {
        Assert.Empty(Kmp.Search("ab", "abcdef"));
    }

    [Fact]
    public void Search_EmptyText_ReturnsEmpty()
    {
        Assert.Empty(Kmp.Search("", "abc"));
    }

    [Fact]
    public void BuildFailureTable_NoRepeatingPrefix_ReturnsAllZeros()
    {
        Assert.Equal(new[] { 0, 0, 0, 0 }, Kmp.BuildFailureTable("abcd"));
    }

    [Fact]
    public void BuildFailureTable_RepeatingPrefix_ReturnsCorrectTable()
    {
        Assert.Equal(new[] { 0, 0, 0, 1, 2 }, Kmp.BuildFailureTable("abcab"));
    }

    [Fact]
    public void BuildFailureTable_AllSameChars_ReturnsIncrementingValues()
    {
        Assert.Equal(new[] { 0, 1, 2, 3 }, Kmp.BuildFailureTable("aaaa"));
    }
}
