namespace Lab15_Retake;

public class BinaryTree<T> where T : notnull
{
    public class Node
    {
        public T Value { get; set; }
        public Node? Left { get; set; }
        public Node? Right { get; set; }

        public Node(T value)
        {
            Value = value;
        }

        public Node(T value, Node? left, Node? right)
        {
            Value = value;
            Left = left;
            Right = right;
        }
    }

    public Node? Root { get; set; }

    public BinaryTree(Node? root)
    {
        Root = root;
    }
    public void BreadthFirstSearch(Action<Node>? visit)
    {
        if (Root != null)
        {
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(Root);
            while (queue.Count > 0)
            {
                Node node = queue.Dequeue();
                if (visit != null) visit(node);
                if (node.Left != null) queue.Enqueue(node.Left);
                if (node.Right != null) queue.Enqueue(node.Right);
            }
        }
    }

    public int ValueCount(T value)
    {
        int count = 0;
        BreadthFirstSearch((Node n) => { 
            if (n.Value.Equals(value)) count++; 
        });
        return count;
    }
}