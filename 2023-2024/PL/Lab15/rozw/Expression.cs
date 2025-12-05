using System.Numerics;
using System.Globalization;

namespace Lab15Retake
{
    internal enum Operation
    {
        Addition, Subtraction, Multiplication, Division
    }

    internal abstract class TreeNode<T> where T : INumber<T>
    {
        public abstract T Evaluate();
    }

    internal class ValueTreeNode<T> : TreeNode<T> where T : INumber<T>
    {
        private T Value { get; set; }

        public ValueTreeNode(T value)
        {
            this.Value = value;
        }

        public override T Evaluate()
        {
            return this.Value;
        }
    }

    internal abstract class ArithmeticTreeNode<T> : TreeNode<T> where T : INumber<T>
    {
        protected TreeNode<T> Left { get; set; }
        protected TreeNode<T> Right { get; set; }

        public ArithmeticTreeNode(TreeNode<T> left, TreeNode<T> right)
        {
            this.Left = left; this.Right = right;
        }

        public static ArithmeticTreeNode<T> Create(Operation operation, TreeNode<T> left, TreeNode<T> right)
        {
            return operation switch
            {
                Operation.Addition => new AdditionTreeNode<T>(left, right),
                Operation.Subtraction => new SubtractionTreeNode<T>(left, right),
                Operation.Multiplication => new MultiplicationTreeNode<T>(left, right),
                Operation.Division => new DivisionTreeNode<T>(left, right),

                _ => throw new ArgumentException($"Invalid operation: {nameof(operation)} was {operation}!"),
            };
        }
    }

    internal class AdditionTreeNode<T> : ArithmeticTreeNode<T> where T : INumber<T>
    {
        public AdditionTreeNode(TreeNode<T> left, TreeNode<T> right) : base(left, right) { }

        public override T Evaluate()
        {
            return base.Left.Evaluate() + base.Right.Evaluate();
        }
    }

    internal class SubtractionTreeNode<T> : ArithmeticTreeNode<T> where T : INumber<T>
    {
        public SubtractionTreeNode(TreeNode<T> left, TreeNode<T> right) : base(left, right) { }

        public override T Evaluate()
        {
            return base.Left.Evaluate() - base.Right.Evaluate();
        }
    }

    internal class MultiplicationTreeNode<T> : ArithmeticTreeNode<T> where T : INumber<T>
    {
        public MultiplicationTreeNode(TreeNode<T> left, TreeNode<T> right) : base(left, right) { }

        public override T Evaluate()
        {
            return base.Left.Evaluate() * base.Right.Evaluate();
        }
    }

    internal class DivisionTreeNode<T> : ArithmeticTreeNode<T> where T : INumber<T>
    {
        public DivisionTreeNode(TreeNode<T> left, TreeNode<T> right) : base(left, right) { }

        public override T Evaluate()
        {
            return base.Left.Evaluate() / base.Right.Evaluate();
        }
    }

    internal static class CharacterExtension
    {
        public static bool IsArithmeticOperator(this char character, out Operation operation)
        {
            switch (character)
            {
                case '+': { operation = Operation.Addition; return true; }
                case '-': { operation = Operation.Subtraction; return true; }
                case '*': { operation = Operation.Multiplication; return true; }
                case '/': { operation = Operation.Division; return true; }
            }

            operation = Operation.Addition; return false;
        }
    }

    internal class ExpressionTree<T> where T : INumber<T>
    {
        public TreeNode<T> Root { get; private init; }

        public ExpressionTree(FileSource expressionSource)
        {
            Stack<TreeNode<T>> stack = new Stack<TreeNode<T>>();

            try
            {
                foreach (char character in expressionSource)
                {
                    if (character.IsArithmeticOperator(out Operation operation) == true)
                    {
                        TreeNode<T> rightNode = stack.Pop();
                        TreeNode<T> leftNode = stack.Pop();

                        stack.Push(ArithmeticTreeNode<T>.Create(operation, leftNode, rightNode));
                    }
                    else
                    {
                        stack.Push(new ValueTreeNode<T>(T.Parse(character.ToString(), NumberStyles.Number, NumberFormatInfo.InvariantInfo)));
                    }
                }
            }
            catch (InvalidOperationException)
            {
                throw new ArgumentException("Invalid expression source stream - not enough elements or wrong expression!");
            }
            catch (FormatException)
            {
                throw new ArgumentException("Invalid expression source stream - invalid symbol found!");
            }

            this.Root = stack.Pop();

            if (stack.Count != 0)
                throw new ArgumentException("Invalid expression source stream - too many elements!");
        }

        private ExpressionTree(TreeNode<T> root)
        {
            this.Root = root;
        }

        public static ExpressionTree<T> operator +(ExpressionTree<T> left, ExpressionTree<T> right)
        {
            return new ExpressionTree<T>(new AdditionTreeNode<T>(left.Root, right.Root));
        }

        public static ExpressionTree<T> operator -(ExpressionTree<T> left, ExpressionTree<T> right)
        {
            return new ExpressionTree<T>(new SubtractionTreeNode<T>(left.Root, right.Root));
        }

        public static ExpressionTree<T> operator *(ExpressionTree<T> left, ExpressionTree<T> right)
        {
            return new ExpressionTree<T>(new MultiplicationTreeNode<T>(left.Root, right.Root));
        }

        public static ExpressionTree<T> operator /(ExpressionTree<T> left, ExpressionTree<T> right)
        {
            return new ExpressionTree<T>(new DivisionTreeNode<T>(left.Root, right.Root));
        }

        public static implicit operator T(ExpressionTree<T> expressionTree)
        {
            return expressionTree.Root.Evaluate();
        }
    }
}
