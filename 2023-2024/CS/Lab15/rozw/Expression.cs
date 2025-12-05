using System.Numerics;
using System.Globalization;

namespace Lab15Retake
{
    internal enum Operation
    {
        Invalid, Addition, Subtraction, Multiplication, Division
    }

    internal abstract class TreeNode<T> where T : INumber<T>
    {
        public abstract Vector3D<T> Evaluate();
    }

    internal class ValueTreeNode<T> : TreeNode<T> where T : INumber<T>
    {
        private Vector3D<T> Value { get; set; }

        public ValueTreeNode(Vector3D<T> value)
        {
            this.Value = value;
        }

        public override Vector3D<T> Evaluate()
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

        public override Vector3D<T> Evaluate()
        {
            return base.Left.Evaluate() + base.Right.Evaluate();
        }
    }

    internal class SubtractionTreeNode<T> : ArithmeticTreeNode<T> where T : INumber<T>
    {
        public SubtractionTreeNode(TreeNode<T> left, TreeNode<T> right) : base(left, right) { }

        public override Vector3D<T> Evaluate()
        {
            return base.Left.Evaluate() - base.Right.Evaluate();
        }
    }

    internal class MultiplicationTreeNode<T> : ArithmeticTreeNode<T> where T : INumber<T>
    {
        public MultiplicationTreeNode(TreeNode<T> left, TreeNode<T> right) : base(left, right) { }

        public override Vector3D<T> Evaluate()
        {
            return base.Left.Evaluate() * base.Right.Evaluate();
        }
    }

    internal class DivisionTreeNode<T> : ArithmeticTreeNode<T> where T : INumber<T>
    {
        public DivisionTreeNode(TreeNode<T> left, TreeNode<T> right) : base(left, right) { }

        public override Vector3D<T> Evaluate()
        {
            return base.Left.Evaluate() / base.Right.Evaluate();
        }
    }

    internal static class CharacterExtension
    {
        public static bool IsArithmeticOperator(this string token, out Operation operation)
        {
            if (token.Length == 1)
            {
                switch (token)
                {
                    case "+": { operation = Operation.Addition; return true; }
                    case "-": { operation = Operation.Subtraction; return true; }
                    case "*": { operation = Operation.Multiplication; return true; }
                    case "/": { operation = Operation.Division; return true; }
                }
            }

            operation = Operation.Invalid; return false;
        }
    }

    internal class ExpressionTree<T> where T : INumber<T>
    {
        public TreeNode<T> Root { get; private init; }

        public ExpressionTree(string expressionSource)
        {
            using FileStream fileStream = File.OpenRead(expressionSource);
            using StreamReader streamReader = new StreamReader(fileStream);

            Stack<TreeNode<T>> stack = new Stack<TreeNode<T>>();

            try
            {
                while (streamReader.EndOfStream == false)
                {
                    string[] tokens = streamReader.ReadLine().Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);//.Select(x => x.Trim()).ToArray();

                    for (int i = 0; i < tokens.Length; i++)
                    {
                        if (tokens[i].IsArithmeticOperator(out Operation operation) == true)
                        {
                            TreeNode<T> rightNode = stack.Pop();
                            TreeNode<T> leftNode = stack.Pop();

                            stack.Push(ArithmeticTreeNode<T>.Create(operation, leftNode, rightNode));
                        }
                        else
                        {
                            T x = T.Parse(tokens[i + 0], NumberStyles.Number, NumberFormatInfo.InvariantInfo);
                            T y = T.Parse(tokens[i + 1], NumberStyles.Number, NumberFormatInfo.InvariantInfo);
                            T z = T.Parse(tokens[i + 2], NumberStyles.Number, NumberFormatInfo.InvariantInfo);

                            i += 2; // Move that another two positions, since we've just read them.

                            stack.Push(new ValueTreeNode<T>(new Vector3D<T>(x, y, z)));
                        }
                    }
                }

            }
            catch (InvalidOperationException)
            {
                throw new ArgumentException("Invalid expression source stream - not enough elements or wrong expression!");
            }
            catch(IndexOutOfRangeException)
            {
                throw new ArgumentException("Invalid expression source stream - invalid amount of vector components per line!");
            }
            catch (FormatException)
            {
                throw new ArgumentException("Invalid expression source stream - invalid symbol found!");
            }

            this.Root = stack.Pop();

            if (stack.Count != 0)
                throw new ArgumentException("Invalid expression source stream - too many elements!");
        }

        public static implicit operator Vector3D<T>(ExpressionTree<T> expressionTree)
        {
            return expressionTree.Root.Evaluate();
        }
    }
}
