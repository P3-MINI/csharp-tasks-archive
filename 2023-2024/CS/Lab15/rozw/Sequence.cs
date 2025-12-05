using System.Collections;
using System.Collections.Concurrent;

namespace Lab15Retake
{
    public class PrimesSequence : IAsyncEnumerable<long>, IEnumerable<long>
    {
        private long Start { get; }
        private long End { get; }

        public PrimesSequence(long start, long end)
        {
            Start = start;
            End = end;
        }

        public async IAsyncEnumerator<long> GetAsyncEnumerator(CancellationToken cancellationToken = new())
        {
            BlockingCollection<long> primes = new(Environment.ProcessorCount);

            Task producer = Task.Run(() =>
            {
                Parallel.For(Start, End, new ParallelOptions { CancellationToken = cancellationToken }, number =>
                {
                    if (IsPrime(number)) primes.Add(number, cancellationToken);
                });

                primes.CompleteAdding();
            }, cancellationToken);

            foreach (var prime in primes.GetConsumingEnumerable(cancellationToken))
            {
                yield return prime;
            }

            await producer;
        }

        public IEnumerator<long> GetEnumerator()
        {
            for (long i = Start; i < End; i++)
            {
                if (IsPrime(i)) yield return i;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static bool IsPrime(long number)
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
}
