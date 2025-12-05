namespace PL_Lab05
{
    public class MatrixGraph : Graph
    {
        private int[,] _edges;
        private int _edgesCount = 0;

        public MatrixGraph(int verticesCount) : base(verticesCount)
        {
            _edges = new int[verticesCount, verticesCount];
            for (int i = 0; i < verticesCount; i++)
            for (int j = 0; j < verticesCount; j++)
                _edges[i, j] = -1;
        }

        public MatrixGraph(int verticesCount, Edge[] edges) : this(verticesCount)
        {
            foreach (Edge e in edges)
            {
                if (e.start < 0 || e.end >= verticesCount)
                    continue;
                if (_edges[e.start, e.end] == -1)
                    _edgesCount++;
                _edges[e.start, e.end] = e.GetWeight();
            }
        }

        public override void AddEdge(Edge e)
        {
            if (e.start >= 0 && e.start < verticesCount && e.end >= 0 && e.end < verticesCount)
            {
                if (_edges[e.start, e.end] == -1)
                    _edgesCount++;
                _edges[e.start, e.end] = e.GetWeight();
            }
        }

        public override void RemoveEdge(Edge e)
        {
            if (e.start >= 0 && e.start < verticesCount && e.end >= 0 && e.end < verticesCount)
            {
                if (_edges[e.start, e.end] != -1)
                    _edgesCount--;
                _edges[e.start, e.end] = -1;
            }
        }

        public override int GetEdgesCount()
        {
            return _edgesCount;
        }

        public override Edge GetEdge(int start, int end)
        {
            if (start >= 0 && start < verticesCount && end >= 0 && end < verticesCount)
            {
                if (_edges[start, end] != -1)
                    return new Edge(start, end, _edges[start, end]);
            }

            return null;
        }
    }
}