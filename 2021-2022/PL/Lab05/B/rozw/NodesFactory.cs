using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Windows.Markup;

namespace PL_Lab05
{
    public static class NodesFactory
    {
        public static Node[] CreateMultipleNodes(string[] codes)
        {
            if (codes == null)
                return null;
            Node[] nodes = new Node[codes.Length];
            int i = 0;
            foreach (var s in codes)
            {
                nodes[i++] = CreateNode(s);
            }
            return nodes;
        }

        public static Node CreateNode(string code)
        {
            if (code == null)
                return null;
            switch (code)
            {
                case "+":
                    return new ArithmNode(ArithmOperation.Addition);                   
                case "-":
                    return new ArithmNode(ArithmOperation.Subtraction);                   
                case "*":
                    return new ArithmNode(ArithmOperation.Multiplication);                   
                case "/":
                    return new ArithmNode(ArithmOperation.Division);
                default:
                    return new ValueNode(double.Parse(code, CultureInfo.InvariantCulture));                   
            }
        }

        public static Node CreateTree(Node[] nodes)
        {
            if (nodes.Length < 1)
                return null;
            
            int nodeToFill = 0;
            bool left = true;
            int changeNum = 2;
            for (int i = 1; i < nodes.Length; i++)
            {
                if (left)
                    nodes[nodeToFill].left = nodes[i];
                else
                    nodes[nodeToFill].right = nodes[i];
                left = !left;

                changeNum--;
                if (changeNum == 0)
                {
                    nodeToFill++;
                    changeNum = 2;
                }
            }

            return nodes[0];
        }
    }
}