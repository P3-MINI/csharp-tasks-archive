namespace OrdoMajoris;
using System.Numerics;

public static class Extensions {
    // TODO
    public static IEnumerable<T> Interleave<T>(this IEnumerable<T> a, IEnumerable<T> b) {
        // ---
        using var ea = a.GetEnumerator();
        using var eb = b.GetEnumerator();
        bool hasA = true, hasB = true;
        while (hasA || hasB) {
            if (hasA && (hasA = ea.MoveNext())) {
                yield return ea.Current;
            }
            if (hasB && (hasB = eb.MoveNext())) {
                yield return eb.Current;
            }
        }
        // ---
    }

    // TODO
    public static TResult Median<T, TResult>(this IEnumerable<T> source, Func<T, TResult> selector) where TResult : INumber<TResult> {
        // ---
        var list = source.Select(selector).ToList();
        if (list.Count == 0) {
            throw new InvalidOperationException("Empty sequence.");
        }
        list.Sort();
        int n = list.Count;
        if (n % 2 == 1) {
            return list[n / 2];
        }
        return (list[n / 2 - 1] + list[n / 2]) / TResult.CreateChecked(2);
        // ---
    }
}
