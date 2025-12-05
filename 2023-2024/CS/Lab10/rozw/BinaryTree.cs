namespace Lab10EN;

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

    public void BreadthFirstSearch(Action<Node> onVisit)
    {
        var queue = new Queue<Node>();
        if (Root != null) queue.Enqueue(Root);

        while (queue.Count != 0)
        {
            var node = queue.Dequeue();
            onVisit.Invoke(node);
            if (node.Left != null) queue.Enqueue(node.Left);
            if (node.Right != null) queue.Enqueue(node.Right);
        }
    }

    public int ValueCount(T value)
    {
        int count = 0;
        BreadthFirstSearch(node => count = node.Value.Equals(value) ? count + 1 : count);
        return count;
    }
}