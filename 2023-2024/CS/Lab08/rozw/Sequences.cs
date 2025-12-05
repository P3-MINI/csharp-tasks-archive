using System;
using System.Collections;

namespace P3_2023_Lab08_EN
{
	public interface ISequence : IEnumerable
	{
		string Name { get; }
	}

	public class RandomSequence : ISequence
	{
		private Random random;
		private readonly int a, b;
		private int? seed;
		public string Name => $"Random Sequence (range = [{a}, {b}), seed={seed?.ToString() ?? "null"})";

		public RandomSequence(int a, int b, int? seed = null)
		{
			this.seed = seed;
			this.a = a;
			this.b = b;
			this.random = (seed == null) ? new Random() : new Random(seed.Value);
		}

		public void SetSeed(int seed) { random = new Random(seed); }

		public IEnumerator GetEnumerator()
		{
			while (true)
			{
				yield return random.Next(a, b);
			}
		}
	}

	public class GeometricSequence : ISequence
	{
		private readonly double a_0, q;
		public string Name => $"Geometric Sequence (a = {a_0}, q = {q})";

		public GeometricSequence(double a_0, double q)
		{
			this.a_0 = a_0;
			this.q = q;
		}

		public IEnumerator GetEnumerator()
		{
			double a = a_0;
			while(true)
			{
				yield return a;
				a *= q;
			}
		}
	}

	public class TribonacciSequence : ISequence
	{
		public string Name { get => "Tribonacci Sequence"; }

		public IEnumerator GetEnumerator()
		{
			int i = 0, j = 0, k = 1;

			while (true)
			{
				yield return i;
				(k, j, i) = (i + j + k, k, j);
			}
		}

	}

	public class FunctionalSequence : ISequence
	{
		private readonly string name;
		private readonly IFunction function;
		private readonly double start, step;
		public string Name => $"Functional Sequence: {name}";

		public FunctionalSequence(IFunction function, double start, double step, string name)
		{ 
			this.name = name;
			this.function = function;
			this.start = start;
			this.step = step;
		}

		public IEnumerator GetEnumerator()
		{
			double x = start;
			while(true)
			{
				yield return (x, function.Evaluate(x));
				x += step;
			}
		}
	}
}
