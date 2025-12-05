using System;

namespace P3_2023_Lab08_EN
{
	public interface IFunction
	{
		double Evaluate(double x);
	}

	public class QuadraticFunction : IFunction
	{
		private double a, b, c;
		public QuadraticFunction(double a = 1, double b = 0, double c = 0)
		{
			this.a = a; this.b = b; this.c = c;
		}
		public double Evaluate(double x) => x * (a * x + b) + c;
	}

	public class SinFunction : IFunction
	{
		public double Evaluate(double x) => Math.Sin(x);
	}

	public class HalfCircleFunction : IFunction
	{
		public double Evaluate(double x) => Math.Sqrt(1 - (x * x));
	}
}
