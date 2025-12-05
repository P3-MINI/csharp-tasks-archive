namespace Lab10;

/// <summary>
/// Dodaj metodę DepthFirstSearch, która przyjmuje trzy funkcje
/// typu Action z parametrem typu Node (preVisit, inVisit, postVisit).<br/>
/// Metoda powinna rekurencyjnie przejść po drzewie,
/// wywołując odpowiednie akcje w odpowiednich momentach:<br/>
/// - preVisit powinno wywołać się przed zejściami rekurencyjnymi.<br/>
/// - inVisit powinno wywołać się pomiędzy zejściami rekurencyjnymi.<br/>
/// - postVisit powinno wywołać się po zejściach rekurencyjnych.<br/>
/// Dodaj implementację metody PrintInOrder, wykorzystując poprzednio zaimplementowaną metodę:<br/>
/// - Metoda ta ma wypisywać wartości węzłów w kolejności In-Order z odpowiednimi wcięciami.<br/>
/// - Długość wcięcia ma być proporcjonalna do poziomu węzła w drzewie.<br/>
/// - W metodach preVisit i postVisit można pilnować poziomu rekurencji.<br/>
/// - W metodzie inVisit należy wypisać wartość węzła wraz z odpowiednim wcięciem.
/// </summary>
public class BinaryTree<T>
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

    public void DepthFirstSearch(Action<Node>? preVisit, Action<Node>? inVisit, Action<Node>? postVisit)
    {
        if (Root != null) Traverse(Root, preVisit, inVisit, postVisit);
    }

    private static void Traverse(Node node, Action<Node>? preVisit, Action<Node>? inVisit, Action<Node>? postVisit)
    {
        preVisit?.Invoke(node);
        if (node.Left != null) Traverse(node.Left, preVisit, inVisit, postVisit);
        inVisit?.Invoke(node);
        if (node.Right != null) Traverse(node.Right, preVisit, inVisit, postVisit);
        postVisit?.Invoke(node);
    }

    public void PrintInOrder()
    {
        var level = 0;
        DepthFirstSearch(
            _ => level++,
            node => Console.WriteLine($"{new string('\t', level - 1)}{node.Value}"),
            _ => level--);
    }
}