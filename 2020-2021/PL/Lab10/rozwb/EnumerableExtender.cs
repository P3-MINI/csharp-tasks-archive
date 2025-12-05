using System;
using System.Collections.Generic;

namespace Lab10b
{
    public static class EnumerableExtender
    {
        public static IEnumerable<T> GenerateN<T>(int count, Func<T> func)
        {
            for (int index = 0; index < count; index++)
                yield return func();
        }

        public static void Foreach<T>(this IEnumerable<T> source, Action<T> func)
        {
            foreach (var el in source)
                func(el);
        }

        public static void Print<T>(this IEnumerable<T> source)
        {
            source.Foreach(x => Console.Write($"{x};"));
            Console.WriteLine();
        }

        public static IEnumerable<T> RemoveIf<T>(this IEnumerable<T> source, Predicate<T> pred)
        {
            foreach (var el in source)
                if (!pred(el))
                    yield return el;
        }

        public static IEnumerable<T2> Transform<T1, T2>(this IEnumerable<T1> source, Func<T1, T2> func)
        {
            foreach (var el in source)
                yield return func(el);
        }

        public static T2 Accumulate<T1, T2>(this IEnumerable<T1> source, T2 initValue, Func<T2, T1, T2> func)
        {
            foreach (var el in source)
                initValue = func(initValue, el);
            return initValue;
        }

        public static double Accumulate(this IEnumerable<double> source, double initValue, Func<double, double, double> func = null)
        {
            if (func == null)
                func = (double sum, double x) => sum + x;

            foreach (var el in source)
                initValue = func(initValue, el);

            return initValue;
        }

        public static T[] ToArray<T>(this IEnumerable<T> source)
        {
            List<T> result = new List<T>();

            foreach (var el in source)
                result.Add(el);

            return result.ToArray();
        }

        public static T FindLastIfOrDefault<T>(this IEnumerable<T> source, Predicate<T> pred = null)
        {
            if (pred == null)
                pred = x => true;

            T last = default(T);

            foreach (var el in source)
                if (pred(el))
                    last = el;

            return last;
        }

        public static IEnumerable<T> Unique<T>(this IEnumerable<T> source, Func<T, T, bool> func = null) where T : IComparable<T>
        {
            if (func == null)
                func = (x1, x2) => x1.CompareTo(x2) == 0;

            List<T> result = new List<T>();
            IEnumerator<T> curr = source.GetEnumerator();

            if (!curr.MoveNext()) return result;

            T prev = curr.Current;
            result.Add(prev);

            while (curr.MoveNext())
            {
                if (!func(prev, curr.Current))
                {
                    prev = curr.Current;
                    result.Add(prev);
                }
            }

            return result;
        }

        public static Func<T, T> MaxFunc<T>(params Func<T, T>[] func) where T : IComparable<T>
        {
            if (func == null || func.Length == 0)
                return x => x;

            return (x) =>
            {
                T max = func[0](x);

                for (int index = 1; index < func.Length; index++)
                {
                    T temp = func[index](x);

                    if (temp.CompareTo(max) > 0)
                        max = temp;
                }

                return max;
            };
        }
    }
}
