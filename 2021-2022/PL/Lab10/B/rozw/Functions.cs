using System;
using System.Collections.Generic;
using System.Linq;

namespace LAB_2021_PL_10B
{
    //Stage 1 - 1.5 points
    public static class ListExtender
    {
        public static void AddElementsFromFunction<T>(this Dictionary<int, T> dictionary, int elementsNum,
            Func<(int, T)> fillMethod)
        {
            for (int i = 0; i < elementsNum; i++)
            {
                (int key, T value) tuple = fillMethod();
                dictionary.Add(tuple.key, tuple.value);
            }
        }
    }

    //Stage 2 - 1 point
    public static class Generators
    {
        public static Func<int> NextInteger(int startingValue, int increaseValue)
        {
            int i = startingValue;
            return delegate()
            {
                var last = i;
                i += increaseValue;
                return last;
            };
        }

        public static Func<int, T> LookUpKey<T>(IDictionary<int, T> dictionary, int keyIncreaseValue)
        {
            return delegate(int i)
            {
                if (dictionary.ContainsKey(i))
                {
                    T value = dictionary[i];
                    dictionary.Remove(i);
                    dictionary.Add(i+keyIncreaseValue, value);
                    return value;
                }
                
                dictionary.Add(i, default);
                return default;
            };
        }
    }

    //Stage 3 - 1 point
    public static class FunctionsManipulator
    {
        public static Func<double, double> Distance(Func<double, double> f, Func<double, double> g) =>
            x => Math.Abs(f(x) - g(x));

        public static Func<double, double, double> Integral(Func<double, double> f) =>
            (x1, x2) => (f(x2) - f(x1)) * (x2-x1);
    }


    //Stage 4 - 1.5 points
    public static class ExtensionMethods
    {
        public static (IEnumerable<T>, IEnumerable<T>) Partition<T>(this IEnumerable<T> collection, Func<T, bool> partitionFunction)
        {
            List<T> firstHalf = new List<T>();
            List<T> secondHalf = new List<T>();
            foreach (var elem in collection)
            {
                if(partitionFunction(elem))
                    firstHalf.Add(elem);
                else
                    secondHalf.Add(elem);
            }

            return (firstHalf, secondHalf);
        }

        public static List<T> ReplaceIf<T>(this IEnumerable<T> collection, Func<T, bool> chooseFunc, Func<T, T> replaceFunc)
        {
            var ret = new List<T>();
            foreach (var elem in collection)
            {
                if (chooseFunc(elem))
                    ret.Add(replaceFunc(elem));
                else
                    ret.Add(elem);
            }
            
            return ret;
        }
    }
}