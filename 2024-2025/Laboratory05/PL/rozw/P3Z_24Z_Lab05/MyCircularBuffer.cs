using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3Z_24Z_Lab05
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class MyCircularBuffer<T> : IMyCollection<T>
    {
        private T[] _buffer;
        private int _head;
        private int _tail;
        private int _count;
        private int _capacity;

        public MyCircularBuffer(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentException("Capacity must be greater than 0.");

            _capacity = capacity;
            _buffer = new T[_capacity];
            _head = 0;
            _tail = 0;
            _count = 0;
        }

        public int Count => _count;

        public bool IsFull => _count == _capacity;

        public bool IsEmpty => _count == 0;

        public void Add(T item)
        {
            _buffer[_tail] = item;

            _tail = (_tail + 1) % _capacity;

            if (IsFull)
            {
                _head = (_head + 1) % _capacity;
            }
            else
            {
                _count++;
            }
        }

        public IEnumerable<T> GetItems()
        {
            for (int i = 0; i < _count; i++)
            {
                int index = (_head + i) % _capacity;
                yield return _buffer[index];
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (IsFull)
            {
                int index = _head;
                while (true)
                {
                    yield return _buffer[index];
                    index = (index + 1) % _capacity;
                }
            }
            else 
            {
                for (int i = 0; i < _count; i++)
                {
                    int index = (_head + i) % _capacity;
                    yield return _buffer[index];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

}
