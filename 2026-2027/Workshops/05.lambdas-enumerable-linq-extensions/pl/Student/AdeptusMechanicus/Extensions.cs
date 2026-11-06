using System.Numerics;
namespace AdeptusMechanicus;

public static class Extensions {
    // TODO
    public static IDictionary<V, List<K>> Invert<K, V>(this IDictionary<K, V> source)
        where K : notnull
        where V : notnull {
        throw new NotImplementedException(); // <-- zastąp swoim kodem
    }

    // TODO
    public static TResult IQR<T, TResult>(this IEnumerable<T> source, Func<T, TResult> selector) where TResult : INumber<TResult> {
        throw new NotImplementedException(); // <-- zastąp swoim kodem
    }
}

