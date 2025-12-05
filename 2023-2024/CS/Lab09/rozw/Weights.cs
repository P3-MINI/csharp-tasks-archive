using System.Numerics;
using System.Collections;

namespace EN_Lab09
{
    internal interface IWeights<out T> : IEnumerable<T> where T : INumber<T>
    {

    }

    internal class UniformWeights<T> : IWeights<T> where T : INumber<T>
    {
        public IEnumerator<T> GetEnumerator()
        {
            while (true)
            {
                yield return T.One;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    internal class RandomWeights<T> : IWeights<T> where T : INumber<T>
    {
        private T Start { get; set; }
        private T End { get; set; }

        private int? Seed { get; init; }

        public RandomWeights(T start, T end, int? seed = null)
        {
            this.Start = start; this.End = end; this.Seed = seed;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Random random = this.Seed.HasValue ? new Random(this.Seed.Value) : new Random();

            double start = double.CreateSaturating(this.Start);
            double end = double.CreateSaturating(this.End);

            while (true)
            {
                yield return T.CreateSaturating(random.NextDouble() * (end - start) + start);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
