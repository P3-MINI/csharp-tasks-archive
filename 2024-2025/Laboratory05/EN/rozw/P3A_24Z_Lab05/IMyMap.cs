using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3A_24Z_Lab05
{
    public interface IMyMap<TKey, TValue> where TKey : IComparable<TKey>
    {
        int Count { get; }

        bool Add(TKey key, TValue value);

        MyPair<TKey, TValue>? Find(TKey key);
    }
}
