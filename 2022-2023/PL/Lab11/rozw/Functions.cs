using System;
using System.Collections.Generic;

namespace LAB11_PL
{
    //Etap 1 - 1.5 punkta
    public static class ListExtender
    {
        public static List<T> CopyIf<T>(this List<T> list, Func<T, bool> predykat, Range zakres)
        {
            var ret = new List<T>();
            if (list is null)
            {
                return ret;
            }
            
            var (offset, length) = zakres.GetOffsetAndLength(list.Count);
            for (int i = offset; i < offset + length; i++)
            {
                if (predykat(list[i]))
                {
                    ret.Add(list[i]);
                }
            }

            return ret;
        }
    }

    //Etap 2 - 1.5 punkta
    public static class Generators
    {
        public static Func<(float x,float y)> RandomPointFromFunction(int seed, int mod, Func<float, float> function)
        {
            Random random = new Random(seed);
            return delegate()
            {
                float x = (random.Next()  % mod + (float) random.NextDouble());
                return (x,function(x));
            };
        }

        public static Func<Index, T> ReturnElement<T>(IEnumerable<T> collection)
        {
            return delegate(Index index)
            {
                int count = 0;
                foreach (var elem in collection)
                {
                    count++;
                }
                
                if (count < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }

                var offset = index.GetOffset(count);

                int iter = 0;
                foreach (var elem in collection)
                {
                    if (iter == offset)
                    {
                        return elem;
                    }

                    iter++;
                }


                throw new IndexOutOfRangeException();
            };
        }
    }

    //Etap 3 - 0.5 punkta
    public static class FunctionsManipulator
    {
        public static Func<double, double> Combine(Func<double, double> f, Func<double, double> g) =>
            x => f(g(x));

        public static Func<double, double> Derivative(Func<double, double> f, double h = 0.001) =>
            x => (f(x + h) - f(x - h)) / (2 * h);
    }


    //Etap 4 - 1.5 punkta
    public static class ExtensionMethods
    {
        public static List<T>[] Partition<T>(this IEnumerable<T> collection, float maxSum, Func<T,float,float> accumulationFunction)
        {
            List<List<T>> lists = new List<List<T>>();
            float currSum = 0;
            List<T> currList = new List<T>();
            foreach (var elem in collection)
            {
                currSum = accumulationFunction(elem, currSum);
                if (currSum < maxSum)
                {
                    currList.Add(elem);
                }
                else
                {
                    if (currList.Count == 0)
                    {
                        currList.Add(elem);
                        lists.Add(currList);
                        currList = new List<T>();
                        currSum = 0;
                    }
                    else
                    {
                        lists.Add(currList);
                        currList = new List<T>();
                        currList.Add(elem);
                        currSum = accumulationFunction(elem, 0);
                    }
                    
                }
            }

            if (currList.Count > 0)
            {
                lists.Add(currList);
            }
            
            return lists.ToArray();
        }

        public static IEnumerable<T> Merge<T>(this IEnumerable<T> collection, IEnumerable<T> other, Comparison<T> comparison)
        {
            List<T> merged = new List<T>();
            var enumeratorA = collection.GetEnumerator();
            var enumeratorB = other.GetEnumerator();
            enumeratorA.MoveNext();
            enumeratorB.MoveNext();

            bool endOfFirst = false;
            
            while(true)
            {
                switch (comparison.Invoke(enumeratorA.Current, enumeratorB.Current))
                {
                    case <0:
                        merged.Add(enumeratorA.Current);
                        merged.Add(enumeratorB.Current);
                        break;
                    case >0:
                        merged.Add(enumeratorB.Current);
                        merged.Add(enumeratorA.Current);
                        break;
                    case 0:
                        merged.Add(enumeratorA.Current);
                        merged.Add(enumeratorB.Current);
                        break;
                }

                if (!enumeratorA.MoveNext())
                {
                    endOfFirst = true;
                    break;
                }

                if (!enumeratorB.MoveNext())
                {
                    break;
                }
            }

            if (endOfFirst)
            {
                while (enumeratorB.MoveNext())
                {
                    merged.Add(enumeratorB.Current);
                }
            }
            else
            {
                do
                {
                    merged.Add(enumeratorA.Current);
                } while (enumeratorA.MoveNext());
            }

            return merged;
        }
    }
}