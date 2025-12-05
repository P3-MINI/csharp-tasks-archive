using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3A_24Z_Lab05
{
    public class MyBinaryTree<TKey, TValue> : IMyMap<TKey, TValue> where TKey : IComparable<TKey>
    {
        private class Node
        {
            public MyPair<TKey, TValue> Pair { get; set; }
            public Node? Left { get; set; }
            public Node? Right { get; set; }

            public Node(MyPair<TKey, TValue> pair)
            {
                Pair = pair;
                Left = null;
                Right = null;
            }
        }

        private Node? root = null;
        public int Count { get; private set; } = 0;


        public bool Add(TKey key, TValue value)
        {
            if (root == null)
            {
                root = new Node(new MyPair<TKey, TValue>(key, value));
                Count++;
                return true;
            }
            return AddRecursively(root, key, value);
        }

        private bool AddRecursively(Node current, TKey key, TValue value)
        {
            if (key.CompareTo(current.Pair.Key) == 0)
                return false; // Duplicated key

            if (key.CompareTo(current.Pair.Key) < 0)
            {
                if (current.Left == null)
                {
                    current.Left = new Node(new MyPair<TKey, TValue>(key, value));
                    Count++;
                    return true;
                }

                return AddRecursively(current.Left, key, value);
            }
            else
            {
                if (current.Right == null)
                {
                    current.Right = new Node(new MyPair<TKey, TValue>(key, value));
                    Count++;
                    return true;
                }

                return AddRecursively(current.Right, key, value);
            }
        }

        public MyPair<TKey, TValue>? Find(TKey key)
        {
            return FindRecursively(root, key);
        }

        private MyPair<TKey, TValue>? FindRecursively(Node? current, TKey key)
        {
            if (current == null)
                return null;

            if (key.CompareTo(current.Pair.Key) == 0)
                return current.Pair;

            if (key.CompareTo(current.Pair.Key) < 0)
                return FindRecursively(current.Left, key);

            return FindRecursively(current.Right, key);

        }
    }
}
