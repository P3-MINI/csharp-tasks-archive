//#define STAGE01
//#define STAGE02

namespace Graphs;

class Program
{
    static void Main(string[] args)
    {
#if STAGE01
            Graph g1 = new AdjacencyListGraph(4);
            
            g1.AddEdge(0, 1);
            g1.AddEdge(0, 2);
            g1.AddEdge(1, 2);
            g1.AddEdge(2, 3);
            g1.AddEdge(3, 1);
            
            SearchPath(g1, 0, 3);
            
            Graph g2 = new AdjacencyListGraph(4);
            
            g2.AddEdge(0, 2);
            g2.AddEdge(1, 2);
            g2.AddEdge(2, 1);
            g2.AddEdge(3, 0);
            g2.AddEdge(3, 1);
            
            SearchPath(g2, 0, 3);
            
            Graph g3 = new AdjacencyMatrixGraph(6);
            g3.AddEdge(0, 1);
            g3.AddEdge(0, 2);
            g3.AddEdge(1, 2);
            g3.AddEdge(2, 4);
            g3.AddEdge(3, 5);
            g3.AddEdge(4, 5);
            
            SearchPath(g3, 0, 5);
            
            Graph g4 = new AdjacencyListGraph(10);
            g4.AddEdge(0, 2);
            g4.AddEdge(0, 4);
            g4.AddEdge(0, 7);
            g4.AddEdge(1, 0);
            g4.AddEdge(1, 2);
            g4.AddEdge(1, 3);
            g4.AddEdge(1, 9);
            g4.AddEdge(2, 2);
            g4.AddEdge(2, 4);
            g4.AddEdge(2, 6);
            g4.AddEdge(2, 7);
            g4.AddEdge(3, 4);
            g4.AddEdge(3, 8);
            g4.AddEdge(4, 3);
            g4.AddEdge(5, 4);
            g4.AddEdge(5, 7);
            g4.AddEdge(5, 8);
            g4.AddEdge(6, 9);
            g4.AddEdge(7, 0);
            g4.AddEdge(7, 2);
            g4.AddEdge(7, 3);
            g4.AddEdge(7, 5);
            g4.AddEdge(8, 2);
            g4.AddEdge(8, 3);
            g4.AddEdge(8, 6);
            g4.AddEdge(9, 6);
            
            SearchPath(g4, 0, 9);
#endif
    }
#if STAGE01
    static void SearchPath(Graph graph, int startVertex, int endVertex)
    {
#if STAGE02
        Console.WriteLine($"Searching for path from {startVertex} to {endVertex}...");
#endif
        graph.Display();
#if STAGE02
        var pathFinder = new PathFinderVisitor(endVertex);
        graph.DepthFirstSearch(startVertex, pathFinder);
        
        Console.WriteLine($"Path: {pathFinder}");
#endif
        Console.WriteLine();
#if STAGE01
    }
#endif
#endif
}