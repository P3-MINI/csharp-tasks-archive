namespace PL_Lab05
{
    public abstract class Graph
    {
        protected readonly int verticesCount;
        public Graph(int verticesCount)
        {
            this.verticesCount = verticesCount;
        }
        public abstract void AddEdge(Edge e);
        public abstract void RemoveEdge(Edge e);
        public abstract int GetEdgesCount();

        public int GetVerticesCount()
        {
            return verticesCount;
        }
        public abstract Edge GetEdge(int start, int end);
    }
}