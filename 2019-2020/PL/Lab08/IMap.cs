using System;
using System.Collections.Generic;

namespace PL_Lab_08
{

public interface IMap<TKey, TValue> where TKey : IComparable<TKey>
    {
        int Count { get; }
        bool Add(TKey key, TValue value);
        Pair<TKey, TValue> Find(TKey key);
    }

public static class IMapExtender
    {

    public static bool ContainsAll<TKey, TValue>(this IMap<TKey, TValue> map, IEnumerable<TKey> keys) where TKey : IComparable<TKey>
        {
            foreach (var k in keys)
                if (map.Find(k) is null)
                    return false;
            return true;
        }

    public static int SumForKeys<TKey>(this IMap<TKey, int> map, IEnumerable<TKey> keys) where TKey : IComparable<TKey>
        {
            int sum = 0;
            foreach (var k in keys)
            {
                var p = map.Find(k);
                if (!(p is null))
                    sum += p.Value;
            }
            return sum;
        }

    }

}
