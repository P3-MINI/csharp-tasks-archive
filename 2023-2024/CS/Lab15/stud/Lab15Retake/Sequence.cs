using System.Collections;
using System.Collections.Concurrent;

namespace Lab15Retake
{
    public class PrimesSequence
    {
        private long Start { get; }
        private long End { get; }

        public PrimesSequence(long start, long end)
        {
            Start = start;
            End = end;
        }

        /* Stage_5 */

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
