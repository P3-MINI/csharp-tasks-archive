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

    public class MyDictionary<TKey, TValue> : IMyDictionary<TKey, TValue>, IEnumerable<(TKey, TValue)>
        where TKey : struct
    {
        private class Item
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }

            public Item(TKey key, TValue value)
            {
                this.Key = key;
                this.Value = value;
            }
        }

        Item[] items;
        int count;

        public int Count => count;

        public MyDictionary()
        {
            items = new Item[4];
            count = 0;
        }

        public void Add(TKey key, TValue value)
        {
            Item item = SearchItem(key);
            if(item != null)
            {
                item.Value = value;
            }
            else
            {
                if(count == items.Length)
                {
                    Item[] temp = new Item[2 * items.Length];
                    items.CopyTo(temp, 0);
                    items = temp;
                }

                items[count++] = new Item(key, value);
            }
        }

        public bool Contains(TKey key)
        {
            return SearchItem(key) != null;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            Item item = SearchItem(key);

            if (item != null)
            {
                value = item.Value;
                return true;
            }
            else
            {
                value = default(TValue);
                return false;
            }
        }

        public bool Remove(TKey key)
        {
            for (int index1 = 0; index1 < count; ++index1)
            {
                if (items[index1].Key.Equals(key))
                {
                    --count;
                    for (int index2 = index1; index2 < count; ++index2)
                    {
                        items[index2] = items[index2 + 1];
                    }
                    items[count] = null;
                    return true;
                }
            }

            return false;
        }

        public override string ToString()
        {
            StringBuilder text = new StringBuilder();

            for(int index = 0; index < count; ++index)
            {
                Item item = items[index];
                text.Append($"[{item.Key}:{item.Value}]");
            }

            return text.ToString();
        }

        private Item SearchItem(TKey key)
        {
            for (int index = 0; index < count; ++index)
            {
                if(items[index].Key.Equals(key))
                {
                    Item temp = items[index];

                    if (index > 0)
                    {
                        items[index] = items[index - 1];
                        items[index - 1] = temp;
                    }
                    return temp;
                }
            }

            return null;
        }

        public IEnumerator<(TKey, TValue)> GetEnumerator()
        {
            for (int index = 0; index < count; ++index)
            {
                Item item = items[index];
                yield return (item.Key, item.Value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public static class MyDictionaryExtensions
    {
        public static TKey[] GetKeys<TKey, TValue>(this MyDictionary<TKey, TValue> dictionary)
            where TKey : struct
        {
            TKey[] result = new TKey[dictionary.Count];
            int index = 0;

            foreach (var element in dictionary)
            {
                result[index++] = element.Item1;
            }

            return result;
        }

        public static TValue MaxValue<TKey, TValue>(this MyDictionary<TKey, TValue> dictionary)
            where TKey : struct
            where TValue : IComparable<TValue>
        {
            var iter = dictionary.GetEnumerator();
            TValue maxValue;

            if (iter.MoveNext())
            {
                maxValue = iter.Current.Item2;
            }
            else
            {
                return default(TValue);
            }

            while (iter.MoveNext())
            {
                if (iter.Current.Item2.CompareTo(maxValue) > 0)
                {
                    maxValue = iter.Current.Item2;
                }
            }

            return maxValue;
        }
    }
}
