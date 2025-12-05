using System;
using System.Threading;
using System.Threading.Tasks;

namespace _2_parallel
{
    public static class PrimeNumberChecker
    {
        private static bool IsPrime(int number)
        {
            if (number < 2) { return false; }
            if (number == 2) { return true; }
            for (var i = 2; i < number; i++)
            {
                if (number % i == 0) { return false; }
            }
            return true;
        }

        public static bool[] AreNumberPrimesSequential(int[] numbers)
        {
            var result = new bool[numbers.Length];

            for (var i = 0; i < result.Length; i++)
            {
                result[i] = IsPrime(numbers[i]);
            }

            return result;
        }

        public static bool[] AreNumberPrimesParallel(int[] numbers)
        {
            var result = new bool[numbers.Length];

            Parallel.ForEach(numbers, (number, state, index) =>
            {
                result[index] = IsPrime(number);
            });

            return result;
        }
    }
}
