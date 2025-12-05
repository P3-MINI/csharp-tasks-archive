using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab07A
{
    class MyLinkedList
    {
        // dane
        public class Node
        {
            public int Value { get; set; }
            public Node Next { get; set; }


            public Node(int value, Node next=null)
            {
                Value = value;
                Next = next;
            }
        }

        private Node head;

        //etap 1

        public Node Head
        {
            get
            {
                return head;
            }
        }


        private int count;
        public int Count
        {
            get
            {
                return count;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return head == null;
            }
        }

        public MyLinkedList()
        {
            head = null;
        }

        public MyLinkedList(int[] array)
        {
            head = null;
            for (int i = 0; i < array.Length; i++)
            {
                PushFront(array[i]);
            }
        }

        public void PushFront(int v)
        {
            head = new Node(v, head);
            count++;
        }

        // etap 2
        public static explicit operator int[] (MyLinkedList l)
        {
            int[] array = new int[l.count];
            Node l1Node = l.head;
            for (int i = 0; i < l.count; i++, l1Node = l1Node.Next)
            {
                array[i] = l1Node.Value;
            }
            return array;
        }

        public static implicit operator MyLinkedList(int[] array)
        {
            Array.Reverse(array);
            return new MyLinkedList(array);
        }

        public MyLinkedList Clone()
        {
            int[] arr = (int[])this;
            MyLinkedList res = arr;
            return res;
        }

        public override string ToString()
        {
            int[] arr = (int[])this;
            string result = "[";
            result += string.Join(";", arr);
            result += "]";

            return result;
        }

        //etap 3
        public static MyLinkedList operator +(MyLinkedList l1, MyLinkedList l2)
        {
            if (l1.IsEmpty) return l2.Clone();

            MyLinkedList l1Clone = l1.Clone();
            MyLinkedList l2Clone = l2.Clone();

            Node head = l1Clone.Head;
            while (head.Next != null)
            {
                head = head.Next;
            }
            Node lastL1 = head;
            lastL1.Next = l2Clone.Head;
            l1Clone.count += l2Clone.count;
            return l1Clone;
        }

        public static MyLinkedList operator -(MyLinkedList l1, MyLinkedList l2)
        {
            if (l2.IsEmpty) return l1.Clone();

            MyLinkedList l1Clone = l1.Clone();

            Node l2Node = l2.Head;

            while (l2Node != null)
            {
                Node previous = null;
                Node l1Node = l1Clone.Head;
                while (l1Node != null && l1Node.Value != l2Node.Value)
                {
                    previous = l1Node;
                    l1Node = l1Node.Next;
                }
                if (l1Node != null)
                {
                    if (previous == null)
                    {
                        l1Clone.head = l1Node.Next;
                    }
                    else
                    {
                        previous.Next = l1Node.Next;
                    }
                    l1Clone.count--;
                }
                l2Node = l2Node.Next;
            }
            return l1Clone;
        }

        public static bool operator ==(MyLinkedList l1, MyLinkedList l2)
        {
            var o1 = (object)l1;
            var o2 = (object)l2;

            if (o1 == null || o2 == null)
                return o1 == null && o2 == null;

            if (l1.Count != l2.Count)
                return false;

            Node l1Node = l1.Head;
            Node l2Node = l2.Head;

            while (l1Node != null && l2Node != null)
            {
                if (l1Node.Value != l2Node.Value)
                    return false;
                l1Node = l1Node.Next;
                l2Node = l2Node.Next;
            }
            return true;
        }

        public static bool operator !=(MyLinkedList l1, MyLinkedList l2)
        {
            return !(l1 == l2);
        }

        public override bool Equals(object obj)
        {
            return this == (MyLinkedList)obj;
        }

        public override int GetHashCode()
        {
            int res = 0;
            Node l1Node = head;
            while (l1Node != null)
            {
                res ^= l1Node.Value.GetHashCode();
                l1Node = l1Node.Next;
            }
            return res;
        }

        //etap 4
        public void Deconstruct(out MyLinkedList even, out MyLinkedList odd)
        {
            even = new MyLinkedList();
            odd = new MyLinkedList();

            Node l1Node = Head;
            while (l1Node != null)
            {
                if (l1Node.Value % 2 == 0)
                    even.PushFront(l1Node.Value);
                else
                    odd.PushFront(l1Node.Value);
                l1Node = l1Node.Next;
            }
        }

        public int this[int index]
        {
            get
            {
                Node l1Node = head;
                for (int i = 0; i < index; i++, l1Node = l1Node.Next)
                {
                    if (l1Node == null)
                    {
                        throw new IndexOutOfRangeException();
                    }
                }
                return l1Node.Value;
            }
            set
            {
                Node l1Node = head;
                for (int i = 0; i < index; i++, l1Node = l1Node.Next)
                {
                    if (l1Node == null)
                    {
                        throw new IndexOutOfRangeException();
                    }
                }
                l1Node.Value = value;
            }
        }
    }
}
