using System.Collections.Concurrent;

namespace Lab15Retake;

internal static class EnumerableExtension
{
    /* Etap_5 */

    public static List<long> FindPrimes(this IEnumerable<long> sequence)
    {
        List<long> primes = new List<long>();
        foreach (var number in sequence)
        {
            if (number.IsPrime()) primes.Add(number);
        }

        return primes;
    }

    private static bool IsPrime(this long number)
    {
        if (number <= 1) return false;
        if (number == 2) return true;
        if (number % 2 == 0) return false;

        var boundary = (long)Math.Floor(Math.Sqrt(number));

        for (long i = 3; i <= boundary; i += 2)
            if (number % i == 0)
                return false;

        return true;
    }
}
