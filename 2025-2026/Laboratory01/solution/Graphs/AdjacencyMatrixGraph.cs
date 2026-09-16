namespace Graphs;

public class AdjacencyMatrixGraph : Graph
{
    private bool[,] _adjMatrix;
    
    public AdjacencyMatrixGraph(int vertexCount) : base(vertexCount)
    {
        _adjMatrix =  new bool[vertexCount, vertexCount];
    }

    public override List<int> GetOutVertices(int vertex)
    {
        List<int> result = new List<int>();
        for (int i = 0; i < VertexCount; i++)
        {
            if (_adjMatrix[vertex, i]) result.Add(i);
        }
        return result;
    }

    public override void AddEdge(int from, int to)
    {
        _adjMatrix[from, to] = true;
    }
}