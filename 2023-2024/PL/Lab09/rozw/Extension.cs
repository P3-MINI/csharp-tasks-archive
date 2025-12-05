using System.Text;
using System.Numerics;

namespace PL_Lab09
{
    internal static class StringExtension
    {
        public static string Random(int length = 10)
        {
            StringBuilder result = new StringBuilder(length);

            foreach (char character in new RandomWeights<char>(33, 127))
            {
                if (length-- < 0) break;

                result.Append(character);
            }

            return result.ToString();
        }

        public static string AlphaNumeric(this string _string)
        {
            StringBuilder result = new StringBuilder(_string.Length);

            foreach (char character in _string)
            {
                if (char.IsLetterOrDigit(character))
                {
                    result.Append(character);
                }
            }

            return result.ToString();
        }
    }

    internal static class EnumerableExtension
    {
        public static T Median<T>(this IEnumerable<T> list) where T : INumber<T>
        {
            List<T> sortedList = list.ToList(); sortedList.Sort();

            if ((sortedList.Count ^ 1) == 0) // Even Number
            {
                int index = sortedList.Count / 2;

                return (sortedList[index] + sortedList[index + 1]) / T.CreateChecked(2.0f);
            }

            return sortedList[sortedList.Count / 2]; // Odd Number
        }

        public static T Mode<T>(this IEnumerable<T> list) where T : INumber<T>
        {
            SortedDictionary<T, int> valuesCount = new SortedDictionary<T, int>();

            foreach (T iValue in list)
            {
                valuesCount.TryGetValue(iValue, out int currentCount);

                valuesCount[iValue] = currentCount + 1;
            }

            return valuesCount.MaxBy(x => x.Value).Key;
        }

        public static T Average<T, K>(this IEnumerable<T> list, IWeights<K> weights) where T : INumber<T> where K : INumber<K>
        {
            T result = T.Zero; int count = 0;

            using IEnumerator<K> weight = weights.GetEnumerator(); // IEnumerator Implements IDisposable

            foreach (T iValue in list)
            {
                weight.MoveNext(); count += 1; /* Remember That MoveNext Needs To Be Invoked Before First Usage Of Current */

                result += iValue * T.CreateChecked(weight.Current);
            }

            return result / T.CreateChecked(count);
        }
    }
}
