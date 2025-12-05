using System;
using System.Collections;

namespace P3_2023_Lab08_EN
{
	public interface IIntegral
	{
		IEnumerable Solve(double from, double to);
	}

	public class NumericalIntegrator : IIntegral
	{
		private IFunction function;

		public NumericalIntegrator(IFunction function)
		{
			this.function = function;
		}

		public IEnumerable Solve(double from, double to)
		{
			int n = 1;
			double total = function.Evaluate(from) * (to - from);

			while (true)
			{
				n *= 2;
				total *= 0.5;

				double width = (to - from) / n;
				for (int i = 1; i < n; i+=2)
				{
					double x = from + i * width;
					total += width * function.Evaluate(x);
				}
				yield return total;
			}
		}
	}

	public class MonteCarloIntegrator : IIntegral
	{
		private IFunction function;
		private int samples;
		private Random random = new Random();
		private double maxY = 0;

		public MonteCarloIntegrator(IFunction function,  int samples, double maxY)
		{
			this.function = function;
			this.samples = samples;
			this.maxY = maxY;
		}

		public IEnumerable Solve(double from, double to)
		{
			double minY = 0;

			double area = (to - from) * (maxY - minY);
			long total = 0, below = 0;

			while (true)
			{
				for (int i = 0; i < samples; i++)
				{
					double x = random.NextDouble() * (to - from) + (from);
					double y = random.NextDouble() * (maxY - minY) + (minY);
					double fy = function.Evaluate(x);

					if (fy > y) below++;
				}
				total += samples;

				double value = ((double)below / total) * area;
				yield return value;
			}
		}
	}
}
