namespace PL_Lab05
{
    public class Edge
    {
        public int start;
        public int end;
        int _weight;

        public Edge(int start, int end, int weight = 0)
        {
            this.start = start <= end ? start : end;
            this.end = end > start ? end : start;
            this._weight = weight < 0 ? 0 : weight;
        }

        public Edge(int start, int end, float weight) : this(start, end, (int)weight)
        {
        }

        public Edge(Edge e) : this(e.start, e.end, e._weight)
        {
        }

        public Edge((int, int, float ) tuple) : this(tuple.Item1, tuple.Item2, tuple.Item3)
        {
        }

        public void SetWeight(float weight)
        {
            if (weight >= 0)
                _weight = (int)weight;
        }

        public int GetWeight()
        {
            return _weight;
        }
    }
}