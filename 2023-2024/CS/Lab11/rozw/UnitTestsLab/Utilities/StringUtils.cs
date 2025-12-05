namespace Utilities;

public static class StringUtils
{
    public static string ReverseString(string s)
    {
        var chars = s.ToCharArray();
        for (int i = 0; i < chars.Length / 2; i++)
        {
            (chars[i], chars[s.Length - i - 1]) = (chars[s.Length - i - 1], chars[i]);
        }

        return new string(chars);
    }
}