using System.Diagnostics;

namespace PL_Lab05
{
    public enum ArithmOperation
    {
        Addition = 0,
        Subtraction = 1,
        Multiplication = 2,
        Division = 3
    }

    [DebuggerDisplay("{left?.Evaluate()}:{_operation}:{right?.Evaluate()}")]
    public class ArithmNode : Node
    {
        private ArithmOperation _operation;

        public ArithmNode(ArithmOperation operation) : base(operation + "-Node-Id")
        {
            _operation = operation;
        }

        public ArithmNode((ArithmOperation operation, Node left, Node right) tuple) : this(tuple.operation)
        {
            left = tuple.left;
            right = tuple.right;
        }

        private ArithmNode(ArithmNode node) : this(node._operation)
        {
            left = node.left != null ? node.left.Clone() : null;
            right = node.right != null ? node.right.Clone() : null;
        }

        public override string ToString()
        {
            return $"Id:{_uniqueId}\nOperation:{_operation}";
        }

        public override Node Clone()
        {
            return new ArithmNode(this);
        }

        public override double Evaluate()
        {
            if (left == null || right == null)
                return 0;

            switch (_operation)
            {
                case ArithmOperation.Addition:
                    return left.Evaluate() + right.Evaluate();
                case ArithmOperation.Subtraction:
                    return left.Evaluate() - right.Evaluate();
                case ArithmOperation.Multiplication:
                    return left.Evaluate() * right.Evaluate();
                case ArithmOperation.Division:
                    return right.Evaluate() != 0 ? left.Evaluate() / right.Evaluate() : 0;
            }

            return 0;
        }
    }
}