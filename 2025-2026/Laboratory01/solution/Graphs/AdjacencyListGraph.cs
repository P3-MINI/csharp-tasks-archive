namespace Graphs;

public class AdjacencyListGraph : Graph
{
    private List<int>[] _adjLists;

    public AdjacencyListGraph(int vertexCount) : base(vertexCount)
    {
        _adjLists = new List<int>[vertexCount];
        for (int i = 0; i < vertexCount; i++)
        {
            _adjLists[i] = [];
        }
    }

    public override List<int> GetOutVertices(int vertex)
    {
        return new List<int>(_adjLists[vertex]);
    }

    public override void AddEdge(int from, int to)
    {
        _adjLists[from].Add(to);
    }
}