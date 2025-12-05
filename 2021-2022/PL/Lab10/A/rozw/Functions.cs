using System;
using System.Collections.Generic;
using System.Linq;

namespace LAB_2021_CS_10A
{
    //Stage 1 - 1.5 points
    public static class ListExtender
    {
        public static void FillWith<T>(this List<T> list, int elementsNum, Func<T> fillMethod)
        {
            for (int i = 0; i < elementsNum; i++)
            {
                list.Add(fillMethod());
            }
        }
    }

    //Stage 2 - 1 point
    public static class Generators
    {
        public static Func<int> RandomInteger(int seed, int mod)
        {
            Random random = new Random(seed);
            return delegate() { return random.Next() % mod; };
        }

        public static Func<int, T> ReturnElement<T>(IEnumerable<T> collection)
        {
            return delegate(int i)
            {
                T value = default;

                int j = 0;
                foreach (var elem in collection)
                {
                    if (j == i)
                        return elem;
                    j++;
                    value = elem;
                }

                return value;
            };
        }
    }

    //Stage 3 - 1 point
    public static class FunctionsManipulator
    {
        public static Func<double, double> Combine(Func<double, double> f, Func<double, double> g) =>
            x => f(g(x));

        public static Func<double, double> Derivative(Func<double, double> f, double h = 0.001) =>
            x => return (f(x + h) - f(x - h)) / (2 * h);
    }


    //Stage 4 - 1.5 points
    public static class ExtensionMethods
    {
        public static T Accumulate<T>(this IEnumerable<T> collection, T startingSum, Func<T,T,T> accumulationFunction)
        {
            foreach (var elem in collection)
            {
                startingSum = accumulationFunction(elem, startingSum);
            }
            return startingSum;
        }

        public static IEnumerable<T> Transform<T>(this IEnumerable<T> collection, Func<T, T> transformFunc)
        {
            var ret = new List<T>();
            foreach (var elem in collection)
            {
                ret.Add(transformFunc(elem));
            }

            return ret;
        }
    }
}