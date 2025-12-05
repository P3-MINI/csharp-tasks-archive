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

    public static Action CreateBenchmark(Action action, string name, int times = 1000)
    {
        return () =>
        {
            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < times; i++)
            {
                action();
            }
            sw.Stop();
            Console.WriteLine($"Function {name}: {sw.Elapsed.TotalMilliseconds / times:0.000000} ms on average");
        };
    }

    public static IEnumerable<Action> CreateBenchmarks()
    {
        return ImmutableList.Create(
            CreateBenchmark(StringAdd, "String+="),
            CreateBenchmark(StringBuilder, "StringBuilder"),
            CreateBenchmark(StringJoin, "StringJoin"));
    }
}