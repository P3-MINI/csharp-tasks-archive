using System;
using System.Collections;
using System.Collections.Generic;

namespace lab09_a
{
    public interface IPriorityQueue<T> where T : struct
    {
        void Put(T item);
        T Get();
        int Count { get; }
        T Peek { get; }
    }

    // Etap 3
    //public class PriorityQueueExtensions { }

    public static class PriorityQueueExtensions
    {
        public static bool Exist<T>(this MinPriorityQueue<T> queue, T item) where T : struct, IComparable<T>
        {
            foreach (var queueItem in queue)
            {
                if (item.CompareTo(queueItem) == 0)
                {
                    return true;
                }
            }

            return false;
        }

        public static T MaxItem<T>(this MinPriorityQueue<T> queue) where T : struct, IComparable<T>
        {
            T maxItem = queue.Peek;
            foreach (var queueItem in queue)
            {
                if (maxItem.CompareTo(queueItem) < 0)
                {
                    maxItem = queueItem;
                }
            }

            return maxItem;
        }
    }

    public class Item<T>
    {
        public Item(T value, Item<T> next)
        {
            Next = next;
            Value = value;
        }

        public Item<T> Next { get; set; }
        public T Value { get; private set; }
    }

    public class MinPriorityQueue<T> : IPriorityQueue<T>, /*<Etap 2>*/IEnumerable<T>/*</Etap 2>*/ where T : struct, IComparable<T>
    {
        private Item<T> _root;

        public void Put(T item)
        {
            Count++;
            _root = _root == null
                ? new Item<T>(item, null)
                : new Item<T>(item, _root);
        }

        public T Get()
        {
            if (_root == null)
                throw new InvalidOperationException();

            var (minItem, nextToMin) = GetMinItem();
            if (nextToMin == null)
                _root = minItem.Next;
            else
                nextToMin.Next = minItem?.Next;

            Count--;
            return minItem.Value;
        }

        public int Count { get; private set; }

        public T Peek
        {
            get
            {
                if (_root == null)
                    throw new InvalidOperationException();
                var (minItem, nextToMin) = GetMinItem();
                return minItem.Value;
            }
        }

        private (Item<T> minItem, Item<T> nextToMin) GetMinItem()
        {
            if (_root == null)
                return (null, null);

            var current = _root;
            Item<T> minItem = _root;
            Item<T> nextToMin = null;

            while (current.Next != null)
            {
                if (minItem.Value.CompareTo(current.Next.Value) > 0)
                {
                    minItem = current.Next;
                    nextToMin = current;
                }

                current = current.Next;
            }

            return (minItem, nextToMin);
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = _root;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
