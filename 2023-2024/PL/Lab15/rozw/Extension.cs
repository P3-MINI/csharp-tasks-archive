using System.Collections.Concurrent;

namespace Lab15Retake;

internal static class EnumerableExtension
{
    public static async Task<List<long>> FindPrimesAsync(this IEnumerable<long> sequence)
    {
        int processors = Environment.ProcessorCount;
        List<Task<List<long>>> tasks = new();
        var queue = new BlockingCollection<long>();
        for (int i = 0; i < processors; i++)
        {
            tasks.Add(Task.Run(() => queue.GetConsumingEnumerable().FindPrimes()));
        }

        foreach (var number in sequence)
        {
            queue.Add(number);
        }
        queue.CompleteAdding();

        List<long> primes = new List<long>();
        while (tasks.Count > 0)
        {
            Task<List<long>> task = await Task.WhenAny(tasks);
            primes.AddRange(task.Result);
            tasks.Remove(task);
        }

        return primes;
    }

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
