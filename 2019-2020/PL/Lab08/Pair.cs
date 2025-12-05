using System;

namespace PL_Lab_08
{
    public class Pair<TKey, TValue> : IComparable<Pair<TKey, TValue>> where TKey : IComparable<TKey>
    {
        public TKey Key { get; }
        public TValue Value { get; set; }
        public Pair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
        public int CompareTo(Pair<TKey, TValue> other)
        {
            return Key.CompareTo(other.Key);
        }
    }
}
