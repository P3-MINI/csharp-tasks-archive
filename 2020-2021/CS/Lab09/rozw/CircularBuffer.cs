using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab09_EN
{
    class CircularBuffer<T> : IBuffer<T>, IEnumerable
        where T : IComparable<T>
    {
        T[] values;
        private uint end;
        private uint start;             // Index of first empty field in array

        public CircularBuffer(uint size)
        {
            this.Size = size;
            start = 0;
            end = size;
            values = new T[size + 1];  // Array has 1 additional value for separation of beginning from the end
                                       // It can be alternatively be implemented with additional flag for full state for distinction from empty state
        }

        public uint Size { get; }

        public uint Count
        {
            get
            {
                return start > end ? (start - end - 1) : (Size + start - end);
            }
        }

        public void Put(T value)
        {
            if (Full)
            {
                throw new Exception("Full buffer");
            }

            values[start] = value;
            start = (start + 1) % (Size + 1);
        }

        public T Get()
        {
            if (Empty)
            {
                throw new Exception("Empty buffer");
            }

            end = (end + 1) % (Size + 1);
            return values[end];
        }

        public bool Empty
        {
            get
            {
                return start == ((end + 1) % (Size + 1));
            }
        }

        public bool Full
        {
            get
            {
                return start == end;
            }
        }

        public void Reset()
        {
            start = 0;
            end = Size;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            var index = (end + 1) % (Size + 1);
            while (index != start)
            {
                yield return values[index];
                index = (index + 1) % (Size + 1);
            }
        }

        public IEnumerable FilterLowerThan(T value)
        {
            foreach (T v in this)
            {
                if (v.CompareTo(value) < 0)
                {
                    yield return v;
                }
            }
        }
    }
}
