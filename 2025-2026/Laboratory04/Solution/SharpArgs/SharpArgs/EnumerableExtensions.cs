namespace SharpArgs;

public static class EnumerableExtensions
{
    public static List<T> FindDuplicates<T>(this IEnumerable<T> sourceCollection)
        where T : notnull
    {
        var duplicates = sourceCollection
            .GroupBy(item => item)
            .Where(group => group.Count() > 1)
            .Select(group => group.Key)
            .ToList();
        return duplicates;
    }
}