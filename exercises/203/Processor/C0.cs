namespace _Lib;

public static class C0
{
    public static int M1(string a0, string a1)
    {
        int v0 = a0.Length;
        int v1 = a1.Length;
        int[,] v2 = new int[v0 + 1, v1 + 1];

        for (int i = 0; i <= v0; i++)
            v2[i, 0] = i;

        for (int j = 0; j <= v1; j++)
            v2[0, j] = j;

        for (int i = 1; i <= v0; i++)
        {
            for (int j = 1; j <= v1; j++)
            {
                int v3 = a0[i - 1] == a1[j - 1] ? 0 : 1;
                v2[i, j] = Math.Min(Math.Min(v2[i - 1, j] + 1, v2[i, j - 1] + 1), v2[i - 1, j - 1] + v3);
            }
        }

        return v2[v0, v1];
    }
}
