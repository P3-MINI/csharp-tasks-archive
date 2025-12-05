namespace Lab10EN;

public static class DictionaryExtensions
{
    public static TValue? Merge<TKey, TValue>(
        this IDictionary<TKey, TValue> dictionary,
        TKey key,
        TValue value,
        Func<TValue, TValue, TValue> remappingFunction)
        where TKey : notnull
    {
        if (!dictionary.TryGetValue(key, out var oldValue) || oldValue == null)
            return dictionary[key] = value;

        var newValue = remappingFunction(oldValue, value);

        if (newValue != null)
            return dictionary[key] = newValue;

        dictionary.Remove(key);
        return default;
    }

    public static IDictionary<DateTime, string> MergeLogs(IDictionary<DateTime, string> source, IDictionary<DateTime, string> appendix)
    {
        foreach (var (key, value) in appendix)
        {
            source.Merge(key, value, string.Concat);
        }

        return source;
    }
}