using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab07B
{
    class BST
    {
        public class Node
        {
            public int Key { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }


            public Node(int key, Node left = null, Node right = null)
            {
                Key = key;
                Left = left;
                Right = right;
            }

            public override int GetHashCode()
            {
                int res = Key;
                int leftHashCode = Left != null ? Left.GetHashCode() : 0;
                int rightHashCode = Right != null ? Right.GetHashCode() : 0;
                res ^= leftHashCode;
                res ^= rightHashCode;
                return res;
            }
        }

        public void Insert(int k)
        {
            Node n = new Node(k);
            if (root == null)
            {
                root = n;
                count++;
                return;
            }

            Node prev = null;
            Node tmp = root;
            while (tmp != null)
            {
                if (k < tmp.Key)
                {
                    prev = tmp;
                    tmp = tmp.Left;
                }
                else if (k > tmp.Key)
                {
                    prev = tmp;
                    tmp = tmp.Right;
                }
                else
                    return;
            }

            if (k < prev.Key)
            {
                prev.Left = n;
            }
            else
            {
                prev.Right = n;
            }
            count++;
        }

        public bool Contains(int k)
        {
            Node n = new Node(k);
            Node tmp = root;
            while (tmp != null)
            {
                if (k < tmp.Key)
                {
                    tmp = tmp.Left;
                }
                else if (k > tmp.Key)
                {
                    tmp = tmp.Right;
                }
                else
                    return true;
            }
            return false;
        }

        

        private Node root;

        // etap 1
        public Node Root
        {
            get
            {
                return root;
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
                return root == null;
            }
        }

        public BST()
        {
            root = null;
        }

        public BST(int[] array)
        {
            root = null;
            for (int i = 0; i < array.Length; i++)
            {
                Insert(array[i]);
            }
        }

        // etap 2
        private static void rec(int[] array, ref int index, Node node)
        {
            if (node == null)
                return;

            rec(array, ref index, node.Left);
            array[index] = node.Key;
            index++;
            rec(array, ref index, node.Right);
        }

        public static explicit operator int[] (BST bst)
        {
            int[] array = new int[bst.count];
            int index = 0;
            rec(array, ref index, bst.root);
            return array;
        }

        public static implicit operator BST(int[] array)
        {
            return new BST(array);
        }

        public BST Clone()
        {
            int[] arr = (int[])this;
            BST res = arr;
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
        public static BST operator +(BST l1, BST l2)
        {
            if (l1.IsEmpty) return l2.Clone();

            BST l1Clone = l1.Clone();

            int[] arr = (int[])l2;

            for (int i = 0; i < arr.Length; i++)
            {
                l1Clone.Insert(arr[i]);
            }
            return l1Clone;
        }

        public static BST operator *(BST l1, BST l2)
        {
            BST res = new BST();
            int[] arr = (int[])l1;

            for (int i = 0; i < arr.Length; i++)
            {
                if (l2.Contains(arr[i]))
                    res.Insert(arr[i]);
            }
            return res;
        }

        public static bool operator ==(BST l1, BST l2)
        {
            var o1 = (object)l1;
            var o2 = (object)l2;

            if (o1 == null || o2 == null)
                return o1 == null && o2 == null;

            if (l1.Count != l2.Count)
                return false;

            int[] arr1 = (int[])l1;
            int[] arr2 = (int[])l2;

            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i] != arr2[i])
                    return false;
            }

            return true;
        }

        public static bool operator !=(BST l1, BST l2)
        {
            return !(l1 == l2);
        }

        public override bool Equals(object obj)
        {
            return this == (BST)obj;
        }

        public override int GetHashCode()
        {
            return root != null ? root.GetHashCode() : 0;
        }

        //etap 4
        public void Deconstruct(out BST smaller, out BST greater)
        {
            smaller = new BST();
            greater = new BST();

            int[] arr = (int[])this;

            for (int i = 0; i < arr.Length / 2; i++)
            {
                smaller.Insert(arr[i]);
            }

            for (int i = arr.Length / 2; i < arr.Length; i++)
            {
                greater.Insert(arr[i]);
            }
        }

        public int this[int index]
        {
            get
            {
                Node tmp = root;
                if (index == 0)
                {
                    while (tmp.Left != null)
                        tmp = tmp.Left;
                }
                else if (index == 1)
                {
                    while (tmp.Right != null)
                        tmp = tmp.Right;
                }
                return tmp.Key;
            }
        }

    }
}
