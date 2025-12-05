namespace Lab15_Retake;

// TODO: Stage #1
public class BinaryTree<T>(BinaryTree<T>.Node? root) where T : notnull
{
    public class Node(T value, Node? left = null, Node? right = null)
    {
        public T Value { get; set; } = value;
        public Node? Left { get; set; } = left;
        public Node? Right { get; set; } = right;
    }

    public Node? Root { get; set; } = root;

    // TODO: BreadthFirstSearch

    public int ValueCount(T value)
    {
        // TODO: ValueCount
        return default;
    }
}