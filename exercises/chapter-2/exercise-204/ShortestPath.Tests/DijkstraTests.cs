using Xunit;

namespace ShortestPath.Tests;

public class DijkstraTests
{
    [Fact]
    public void Compute_SourceToSelf_ReturnsZero()
    {
        var result = Dijkstra.Compute(2, new[] { (0, 1, 5) }, 0);
        Assert.Equal(0, result[0]);
    }

    [Fact]
    public void Compute_DirectEdge_ReturnsEdgeWeight()
    {
        var result = Dijkstra.Compute(2, new[] { (0, 1, 7) }, 0);
        Assert.Equal(7, result[1]);
    }

    [Fact]
    public void Compute_TwoHops_ReturnsSumOfWeights()
    {
        var result = Dijkstra.Compute(3, new[] { (0, 1, 2), (1, 2, 3) }, 0);
        Assert.Equal(2, result[1]);
        Assert.Equal(5, result[2]);
    }

    [Fact]
    public void Compute_AlternativeShorterPath_ReturnsMinimumDistance()
    {
        // Direct 0→1 costs 10; path via 2 costs 1+2=3
        var edges = new[] { (0, 1, 10), (0, 2, 1), (2, 1, 2) };
        var result = Dijkstra.Compute(3, edges, 0);
        Assert.Equal(3, result[1]);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 3)]
    [InlineData(2, 2)]
    [InlineData(3, 6)]
    public void Compute_WeightedGraph_ReturnsCorrectDistances(int node, int expected)
    {
        // 0→1(4), 0→2(2), 2→1(1), 1→3(3), 2→3(8)
        // Shortest: 0=0, 1=0+2+1=3, 2=0+2=2, 3=0+2+1+3=6
        var edges = new[] { (0, 1, 4), (0, 2, 2), (2, 1, 1), (1, 3, 3), (2, 3, 8) };
        var result = Dijkstra.Compute(4, edges, 0);
        Assert.Equal(expected, result[node]);
    }

    [Fact]
    public void Compute_UnreachableNode_ReturnsMaxValue()
    {
        var result = Dijkstra.Compute(3, new[] { (0, 1, 5) }, 0);
        Assert.Equal(int.MaxValue, result[2]);
    }
}
