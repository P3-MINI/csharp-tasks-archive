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
    }
}
