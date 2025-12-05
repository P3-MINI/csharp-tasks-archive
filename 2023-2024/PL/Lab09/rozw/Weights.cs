using System.Numerics;
using System.Collections;

namespace PL_Lab09
{
    internal interface IWeights<T> : IEnumerable<T> where T : INumber<T>
    {

    }

    internal class UniformWeights<T> : IWeights<T> where T : INumber<T>
    {
        private readonly T Value;

        public UniformWeights(int value = 1)
        {
            this.Value = T.CreateSaturating(value);
        }

        public IEnumerator<T> GetEnumerator()
        {
            while (true)
            {
                yield return this.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    internal class ExponentialWeights<T> : IWeights<T> where T : INumber<T>
    {
        public IEnumerator<T> GetEnumerator()
        {
            int value = 1;

            while (true)
            {
                yield return T.CreateChecked(Math.Pow(Math.E, -(value++)));
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    internal class RandomWeights<T> : IWeights<T> where T : INumber<T>
    {
        private int Start { get; set; }
        private int End { get; set; }

        public RandomWeights(int start, int end)
        {
            this.Start = start; this.End = end;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Random random = new Random();

            while (true)
            {
                yield return T.CreateChecked(random.NextDouble() * (End - Start) + Start);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
