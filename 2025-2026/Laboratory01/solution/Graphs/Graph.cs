using System.Text;

namespace Graphs;

public abstract class Graph
{
    public int VertexCount { get; }

    protected Graph(int vertexCount)
    {
        VertexCount = vertexCount;
    }

    public abstract List<int> GetOutVertices(int vertex);

    public abstract void AddEdge(int from, int to);

    public void Display()
    {
        StringBuilder sb =  new StringBuilder();
        for (int i = 0; i < VertexCount; i++)
        {
            sb.Append($"{i} -> [");
            var neighbors = GetOutVertices(i);
            sb.Append(string.Join(',',  neighbors));
            sb.Append("]\n");
        }

        Console.WriteLine(sb.ToString());
    }

    public void DepthFirstSearch(int start, IVertexVisitor visitor)
    {
        bool[] visited = new bool[VertexCount];
        DepthFirstSearchRecursive(start, visited, visitor);
    }

    private void DepthFirstSearchRecursive(int vertex, bool[] visited, IVertexVisitor visitor)
    {
        visited[vertex] = true;
        visitor.PreVisit(vertex);
        foreach (var neighbor in GetOutVertices(vertex))
        {
            if (!visited[neighbor])
            {
                DepthFirstSearchRecursive(neighbor, visited, visitor);
            }
        }
        visitor.PostVisit(vertex);
    }
}