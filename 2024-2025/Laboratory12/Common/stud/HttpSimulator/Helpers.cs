namespace HttpSimulator;

public static class HttpRequestHelpers
{
    public static Dictionary<string, string> GetQueryParameters(Uri uri)
    {
        return uri
            .Query
            .TrimStart('?')
            .Split('&', StringSplitOptions.RemoveEmptyEntries)
            .Select(param =>
            {
                var parts = param.Split('=', 2);
                return new KeyValuePair<string, string>(
                    Uri.UnescapeDataString(parts[0]),
                    parts.Length > 1 ? Uri.UnescapeDataString(parts[1]) : string.Empty
                );
            })
            .ToDictionary(kv => kv.Key, kv => kv.Value);
    }
}