using System.Text;

namespace Lab10EN;

public static class EnumerableExtensions
{
    public static TAccumulate Collect<TSource, TAccumulate>(
        this IEnumerable<TSource> source,
        TAccumulate collection,
        Action<TAccumulate, TSource> accumulator)
    {
        foreach (var element in source)
        {
            accumulator(collection, element);
        }
        return collection;
    }

    public static IEnumerable<T> Filter<T>(this IEnumerable<T> source, Predicate<T> predicate)
    {
        foreach (var t in source)
        {
            if (predicate(t)) yield return t;
        }
    }

    public static string ConcatStrings<T>(this IEnumerable<T> source)
    {
        return source.Filter(t => t is string)
            .Collect(
            new StringBuilder(),
            (sb, str) => sb.Append(str))
            .ToString();
    }
}