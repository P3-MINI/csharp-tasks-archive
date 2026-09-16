namespace Graphs;

public class PathFinderVisitor : IVertexVisitor
{
    private int Target { get; }

    public List<int> Path { get;}
    public bool Found { get; private set; }

    public PathFinderVisitor(int target)
    {
        Target = target;
        Path = [];
    }

    public void PreVisit(int vertex)
    {
        if (Found) return;
        Path.Add(vertex);
        if (vertex == Target)
        {
            Found = true;
        }
    }

    public void PostVisit(int vertex)
    {
        if (Found) return;
        Path.RemoveAt(Path.Count - 1);
    }

    public override string ToString()
    {
        return Found ? string.Join(" --> ", Path) : "not found";
    }
}