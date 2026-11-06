using System.Numerics;
namespace AdeptusMechanicus;

public static class Extensions {
    // TODO
    public static IDictionary<V, List<K>> Invert<K, V>(this IDictionary<K, V> source)
        where K : notnull
        where V : notnull {
        // ---
        var result = new Dictionary<V, List<K>>();
        foreach (var (key, value) in source) {
            if (!result.TryGetValue(value, out var list)) {
                list = [];
                result[value] = list;
            }
            list.Add(key);
        }
        return result;
        // ---
    }

    // TODO
    public static TResult IQR<T, TResult>(this IEnumerable<T> source, Func<T, TResult> selector) where TResult : INumber<TResult> {
        // ---
        var list = source.Select(selector).ToList();
        if (list.Count <= 3) {
            throw new InvalidOperationException("The sequence is too short.");
        }
        list.Sort();
        int n = list.Count;
        var q1 = list[n / 4];
        var q3 = list[3 * n / 4];
        return q3 - q1;
        // ---
    }
}

