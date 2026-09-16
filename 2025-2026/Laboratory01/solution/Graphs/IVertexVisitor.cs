namespace Graphs;

public interface IVertexVisitor
{
    void PreVisit(int vertex);
    void PostVisit(int vertex);
}