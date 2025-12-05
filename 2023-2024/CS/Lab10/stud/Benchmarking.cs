using System.Collections.Immutable;
using System.Diagnostics;
using System.Text;

namespace Lab10EN;

public static class Benchmarking
{
    private const int Size = 1_000;

    public static void StringAdd()
    {
        string _ = String.Empty;
        for (int i = 0; i < Size; i++)
        {
            _ += 'a';
        }
    }

    public static void StringBuilder()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < Size; i++)
        {
            sb.Append('a');
        }

        string _ = sb.ToString();
    }

    public static void StringJoin()
    {
        string _ = string.Join(string.Empty, Enumerable.Repeat('a', Size));
    }

    // TODO: CreateBenchmark

    public static IEnumerable<Action> CreateBenchmarks()
    {
        // TODO: CreateBenchmarks
        return ImmutableList.Create(StringAdd, StringBuilder, StringJoin);
    }
}