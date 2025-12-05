

using System.Collections;

namespace P3Z_24Z_Lab05
{
    public class MySortedLinkedList<T> : IEnumerable<T> where T : IComparable<T>
    {
        private class Node
        {
            public T Value { get; set; }
            public Node? Next { get; set; }

            public Node(T value)
            {
                Value = value;
                Next = null;
            }
        }

        private Node? head;
        public int Count { get; private set; }

        public MySortedLinkedList()
        {
            head = null;
            Count = 0;
        }

        public bool Add(T value)
        {
            var newNode = new Node(value);
            if (head == null || head.Value.CompareTo(value) > 0)
            {
                newNode.Next = head;
                head = newNode;
            }
            else
            {
                Node current = head;
                while (current.Next != null && current.Next.Value.CompareTo(value) < 0)
                {
                    current = current.Next;
                }

                if (current.Value.CompareTo(value) == 0) 
                    return false;

                newNode.Next = current.Next;
                current.Next = newNode;
            }

            Count++;
            return true;
        }

        public bool Contains(T value)
        {
            Node? current = head;
            while (current != null)
            {
                if (current.Value.CompareTo(value) == 0)
                {
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public T PopFront()
        {
            if (head == null) 
                throw new IndexOutOfRangeException();

            var value = head.Value;
            head = head.Next;
            Count--;

            return value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node? current = head;
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
