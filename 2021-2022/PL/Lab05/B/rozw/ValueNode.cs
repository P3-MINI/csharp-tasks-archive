using System.Diagnostics;

namespace PL_Lab05
{
    [DebuggerDisplay("{_value}")]
    public class ValueNode : Node
    {
        private static int _nextValueNodeId = 0;
        private double _value;

        public ValueNode(double value=0) : base("Value-Node-Id-" + _nextValueNodeId++)
        {
            _value = value;
        }

        private ValueNode(ValueNode node) : this(node._value)
        {
            
        }

        public override string ToString()
        {
            return $"Id:{_uniqueId}\nValue:{_value}\n";
        }

        public override Node Clone()
        {
            return new ValueNode(this);
        }

        public override double Evaluate()
        {
            return _value;
        }

        public void SetValue(double value)
        {
            _value = value;
        }

        public static Node[] Sort(ValueNode[] nodes)
        {
            for (int i = 0; i < nodes.Length; i++)
            {
                for (int j = i; j < nodes.Length; j++)
                {
                    if (nodes[i].Evaluate() > nodes[j].Evaluate())
                    {
                        (nodes[i], nodes[j]) = (nodes[j], nodes[i]);
                    }
                }
            }

            return nodes;
        }
    }
}