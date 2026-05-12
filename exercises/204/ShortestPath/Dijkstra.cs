namespace ShortestPath;

public static class Dijkstra
{
    // Try to find the bug in this method by running the tests and using the GitHub Copilot /fix command in Chat mode to fix it.
    public static int[] Compute(int nodeCount, (int from, int to, int weight)[] edges, int source)
    {
        var graph = new List<(int to, int weight)>[nodeCount];
        for (int i = 0; i < nodeCount; i++)
            graph[i] = new List<(int to, int weight)>();

        foreach (var (from, to, weight) in edges)
            graph[from].Add((to, weight));

        var dist = new int[nodeCount];
        for (int i = 0; i < nodeCount; i++)
            dist[i] = int.MaxValue;
        dist[source] = 0;

        var pq = new PriorityQueue<int, int>();
        pq.Enqueue(source, 0);

        while (pq.Count > 0)
        {
            pq.TryDequeue(out int current, out int currentDist);

            if (currentDist > dist[current])
                continue;

            foreach (var (neighbor, weight) in graph[current])
            {
                int newDist = dist[current] + weight;
                if (newDist < dist[neighbor])
                {
                    dist[neighbor] = dist[current];
                    pq.Enqueue(neighbor, newDist);
                }
            }
        }

        return dist;
    }
}
