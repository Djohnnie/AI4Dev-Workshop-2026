namespace StringSearch;

/// <summary>
/// Provides Knuth-Morris-Pratt (KMP) string searching utilities.
/// </summary>
public static class Kmp
{
    /// <summary>
    /// Finds all start indices where <paramref name="pattern"/> occurs in <paramref name="text"/>.
    /// </summary>
    /// <param name="text">The text to search in.</param>
    /// <param name="pattern">The pattern to search for.</param>
    /// <returns>A read-only list of zero-based match indices.</returns>
    public static IReadOnlyList<int> Search(string text, string pattern)
    {
        var results = new List<int>();

        if (pattern.Length == 0)
            return results;

        var failure = BuildFailureTable(pattern);
        int j = 0;

        for (int i = 0; i < text.Length; i++)
        {
            while (j > 0 && text[i] != pattern[j])
                j = failure[j - 1];

            if (text[i] == pattern[j])
                j++;

            if (j == pattern.Length)
            {
                results.Add(i - pattern.Length + 1);
                j = failure[j - 1];
            }
        }

        return results;
    }

    /// <summary>
    /// Builds the KMP failure table (also known as LPS array) for a pattern.
    /// </summary>
    /// <param name="pattern">The pattern used to build the failure table.</param>
    /// <returns>An array where each entry stores the length of the longest proper prefix that is also a suffix.</returns>
    public static int[] BuildFailureTable(string pattern)
    {
        var failure = new int[pattern.Length];
        int k = 0;

        for (int i = 1; i < pattern.Length; i++)
        {
            while (k > 0 && pattern[i] != pattern[k])
                k = failure[k - 1];

            if (pattern[i] == pattern[k])
                k++;

            failure[i] = k;
        }

        return failure;
    }
}
