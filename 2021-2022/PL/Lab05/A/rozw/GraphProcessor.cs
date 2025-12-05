namespace PL_Lab05
{
    public static class GraphProcessor
    {
        public static ((int degree, int v) min, (int degree, int v) max) FindMinAndMaxDegree(Graph graph)
        {
            int min, max, minV, maxV;
            max = maxV = 0;
            int[] degrees = new int[graph.GetVerticesCount()];
            for (int i = 0; i < graph.GetVerticesCount(); i++)
                for (int j = 0; j < graph.GetVerticesCount(); j++)
                {
                    var e = graph.GetEdge(i, j);
                    if (e is not null)
                    {
                        degrees[e.start]++;
                        degrees[e.end]++;
                    }
                }

            min = degrees[0];
            minV = 0;
            for (int i = 0; i < graph.GetVerticesCount(); i++)
            {
                if (degrees[i] < min)
                {
                    min = degrees[i];
                    minV = i;
                }

                if (degrees[i] > max)
                {
                    max = degrees[i];
                    maxV = i;
                }
            }

            return ((min, minV), (max, maxV));
        }

        public static Edge[] SortEdges(Edge[] edges)
        {
            for (int i = 0; i < edges.Length; i++)
            {
                for (int j = i; j < edges.Length; j++)
                {
                    if (edges[i].GetWeight() > edges[j].GetWeight())
                    {
                        (edges[i], edges[j]) = (edges[j], edges[i]);
                    }
                }
            }

            return edges;
        }
    }
}