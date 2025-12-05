using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Lab9A
{
    interface IMyDictionary<TKey, TValue>
    {
        int Count { get; }
        void Add(TKey key, TValue value);
        bool Contains(TKey key);
        bool TryGetValue(TKey key, out TValue value);
        bool Remove(TKey key);
    }
}
