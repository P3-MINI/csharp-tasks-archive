using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3A_24Z_Lab05
{
    public class MyPair<TKey, TValue> : IComparable<MyPair<TKey, TValue>> where TKey : IComparable<TKey>
    {
        public TKey Key { get; }
        public TValue Value { get; set; }

        public MyPair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public int CompareTo(MyPair<TKey, TValue> other)
        {
            return Key.CompareTo(other.Key);
        }
    }
}
