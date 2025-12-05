using System;
using System.Collections;
using System.Collections.Generic;

namespace lab09_a
{
    public interface IPriorityQueue<T>
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
        public static bool Exist<T>(this MaxPriorityQueue<T> queue, T item) where T : struct, IComparable<T>
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

        public static T MinItem<T>(this MaxPriorityQueue<T> queue) where T : struct, IComparable<T>
        {
            T minItem = queue.Peek;
            foreach (var queueItem in queue)
            {
                if (minItem.CompareTo(queueItem) > 0)
                {
                    minItem = queueItem;
                }
            }

            return minItem;
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
        public T Value { get; }
    }

    public class MaxPriorityQueue<T> : IPriorityQueue<T>, /*<Etap 2>*/IEnumerable<T>/*</Etap 2>*/ where T : struct, IComparable<T>
    {
        private Item<T> _root;

        public void Put(T item)
        {
            Count++;
            var isEmptyQueue = _root == null;
            if (isEmptyQueue)
            {
                _root = new Item<T>(item, null);
                return;
            }

            var current = _root;
            Item<T> previous = null;
            do
            {
                if (item.CompareTo(current.Value) > 0)
                {
                    break;
                }
                previous = current;
                current = current.Next;
            } while (current != null);

            var isNewRoot = previous == null;
            if (isNewRoot)
            {
                var newRoot = new Item<T>(item, current);
                _root = newRoot;
            }
            else
            {
                var newCurrent = new Item<T>(item, current);
                previous.Next = newCurrent;
            }
        }

        public T Get()
        {
            if (_root == null)
                throw new InvalidOperationException();

            Count--;
            var value = _root.Value;
            _root = _root.Next;
            return value;
        }

        public int Count { get; private set; }

        public T Peek
        {
            get
            {
                if (_root == null)
                    throw new InvalidOperationException();
                return _root.Value;
            }
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
