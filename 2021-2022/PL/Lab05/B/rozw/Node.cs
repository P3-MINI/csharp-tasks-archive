using System.Diagnostics;

namespace PL_Lab05
{
    public abstract class Node
    {
        private static int _nextNodeId=0;
        public Node left;
        public Node right;
        protected readonly string _uniqueId;

        protected Node(string uniqueId)
        {
            _uniqueId = uniqueId + "-" + _nextNodeId++;
        }

        public abstract Node Clone();

        public abstract double Evaluate();
    }
}