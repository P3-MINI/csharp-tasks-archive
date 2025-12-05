using System.Text;
using System.Numerics;

namespace EN_Lab09
{
    internal static class StringExtension
    {
        public static string Random(string availableChars, int seed = 12345, int length = 10)
        {
            Random random = new Random(seed);

            StringBuilder result = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                int randomIndex = random.Next(availableChars.Length);

                char character = availableChars[randomIndex];

                result.Append(character);
            }

            return result.ToString();
        }

        public static string Rot18(this string @string)
        {
            StringBuilder result = new StringBuilder(@string.Length);

            foreach (char character in @string)
            {
                result.Append(character switch
                {
                    char _ when char.IsAsciiDigit(character) => (char)((character - '0' + 5) % 10 + '0'),
                    char _ when char.IsAsciiLetterLower(character) => (char)((character - 'a' + 13) % 26 + 'a'),
                    char _ when char.IsAsciiLetterUpper(character) => (char)((character - 'A' + 13) % 26 + 'A'),
                    _ => character, /* Default */
                });
            }

            return result.ToString();
        }
    }

    internal static class EnumerableExtension
    {
        public static T Mean<T, K>(this IEnumerable<T> sequence, IWeights<K> weights) where T : INumber<T> where K : INumber<K>
        {
            T result = T.Zero; K weightSum = K.Zero;

            using IEnumerator<K> weight = weights.GetEnumerator(); // IEnumerator Implements IDisposable

            foreach (T iValue in sequence)
            {
                weight.MoveNext(); /* Remember That MoveNext Needs To Be Invoked Before First Usage Of Current */

                K currentWeight = weight.Current;

                weightSum += currentWeight;

                result += iValue * T.CreateChecked(currentWeight);
            }

            return result / T.CreateChecked(weightSum);
        }

        public static T StandardDeviation<T, K>(this IEnumerable<T> sequence, IWeights<K> weights) where T : INumber<T> where K : INumber<K>
        {
            T meanValue = sequence.Mean(weights);

            T deviationValue = T.Zero;

            foreach (T currentValue in sequence)
            {
                deviationValue += (currentValue - meanValue) * (currentValue - meanValue);
            }

            return T.CreateSaturating(Math.Sqrt(double.CreateSaturating(deviationValue / T.CreateSaturating(sequence.Count()))));
        }

        public static IEnumerable<T> MovingAverage<T>(this IEnumerable<T> sequence, int period = 3) where T : INumber<T>
        {
            int processedCount = 0;

            while (true)
            {
                T currentValue = T.Zero;

                using IEnumerator<T> currentEnumerator = sequence.GetEnumerator();

                for (int i = 0; i < processedCount; i++)
                {
                    currentEnumerator.MoveNext();
                }

                for (int i = 0; i < period; i++)
                {
                    if (currentEnumerator.MoveNext() == false)
                        yield break;

                    currentValue += currentEnumerator.Current;
                }

                processedCount += 1;

                yield return currentValue / T.CreateSaturating(period);
            }
        }
    }
}
