using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;


namespace Lab9B
{
    interface IMyDictionary<TKey, TValue>
    {
        int Count { get; }
        void Add(TKey key, TValue value);
        bool Contains(TKey key);
        bool TryGetValue(TKey key, out TValue value);
        bool Remove(TKey key);
    }

    public class MyDictionary<TKey, TValue> : IMyDictionary<TKey, TValue>, IEnumerable<(TKey, TValue, int)>
        where TKey : struct
    {
        private class Item
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public int Counter { get; set; }

            public Item(TKey key, TValue value)
            {
                this.Key = key;
                this.Value = value;
                this.Counter = 0;
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
            if (item != null)
            {
                item.Value = value;
            }
            else
            {
                if (count == items.Length)
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

            for (int index = 0; index < count; ++index)
            {
                Item item = items[index];
                text.Append($"[{item.Key}:{item.Value}({item.Counter})]");
            }

            return text.ToString();
        }

        private Item SearchItem(TKey key)
        {
            for (int index1 = 0; index1 < count; ++index1)
            {
                if (items[index1].Key.Equals(key))
                {
                    Item temp = items[index1];
                    ++temp.Counter;

                    if (index1 > 0)
                    {
                        int index2 = index1 - 1;
                        while (index2 >= 0 && items[index2].Counter < temp.Counter)
                        {
                            --index2;
                        }

                        int newIndex = index2 + 1;
                        items[index1] = items[newIndex];
                        items[newIndex] = temp;
                    }
                    return temp;
                }
            }

            return null;
        }

        public IEnumerator<(TKey, TValue, int)> GetEnumerator()
        {
            for (int index = 0; index < count; ++index)
            {
                Item item = items[index];
                yield return (item.Key, item.Value, item.Counter);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public static class MyDictionaryExtensions
    {
        public static TValue[] GetValues<TKey, TValue>(this MyDictionary<TKey, TValue> dictionary)
            where TKey : struct
        {
            TValue[] result = new TValue[dictionary.Count];
            int index1 = 0;

            foreach (var element in dictionary)
            {
                bool found = false;
                for(int index2 = 0; index2 < index1; ++index2)
                {
                    if(result[index2].Equals(element.Item2))
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    result[index1++] = element.Item2;
                }
            }

            return result[..index1];
        }

        public static TValue MinValue<TKey, TValue>(this MyDictionary<TKey, TValue> dictionary)
            where TKey : struct
            where TValue : IComparable<TValue>
        {
            var iter = dictionary.GetEnumerator();
            TValue minValue;

            if (iter.MoveNext())
            {
                minValue = iter.Current.Item2;
            }
            else
            {
                return default(TValue);
            }

            while (iter.MoveNext())
            {
                if (iter.Current.Item2.CompareTo(minValue) < 0)
                {
                    minValue = iter.Current.Item2;
                }
            }

            return minValue;
        }
    }
}
