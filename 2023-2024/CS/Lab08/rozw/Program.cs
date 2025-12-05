#define PART_1
#define PART_2
#define PART_3
#define PART_4

using System.Collections;

namespace P3_2023_Lab08_EN
{
	public static class Program
	{
		static void Main()
		{

#if PART_1
			Console.WriteLine(" -== PART 1 (1.5 pts) ==-");
			var randomSequence = new RandomSequence(0, 100);
			randomSequence.Print();
			Console.WriteLine();

			var sequences = new List<ISequence>();
			sequences.Add(new RandomSequence(0, 10, 2137));
			sequences.Add(new GeometricSequence(1, 2));
			sequences.Add(new TribonacciSequence());

			foreach (var sequence in sequences)
			{
				sequence.Print();
				Console.WriteLine();
			}
#endif
#if PART_2
			Console.WriteLine(" -== PART 2 (1.5 pts) ==-");
			var functions = new List<(string, IFunction)>();
			functions.Add(("Quadratic", new QuadraticFunction()));
			functions.Add(("Sin", new SinFunction()));
			functions.Add(("HalfCircle", new HalfCircleFunction()));

			foreach (var (name, fun) in functions)
			{
				var funSeq = new FunctionalSequence(fun, 0, 0.1, name);
				Console.WriteLine($"Name: {funSeq.Name}");

				var values = new List<string>();
				foreach (var t in funSeq.FirstNElements(10))
				{
					(double x, double y) = ((double, double))t;
					values.Add($"({x:0.00} {y:0.00})");
				};

				Console.WriteLine($"  [ {string.Join(", ", values)} ]");
			}
#endif
#if PART_3
			Console.WriteLine();
			Console.WriteLine(" -== PART 3 (2 pts) ==-");
			{
				var numericalIntegrator = new NumericalIntegrator(new SinFunction());
				Console.WriteLine("NumericalIntegrator:");
				int index = 1;
				foreach (var value in numericalIntegrator.Solve(0, Math.PI).FirstNElements(15))
				{
					Console.WriteLine($"{index++,3}: {value}");
				}
			}
			Console.WriteLine();
			{
				int samplesPerIteration = 25;
				double maxY = 1.0;
				var monteCarloIntegrator = new MonteCarloIntegrator(new SinFunction(), samplesPerIteration, maxY);
				Console.WriteLine("MonteCarloIntegrator:");
				int index = 1;
				foreach (var value in monteCarloIntegrator.Solve(0, Math.PI).FirstNElements(15))
				{
					Console.WriteLine($"{index++,3}: {value}");
				}
			}
#endif
		}

		public static IEnumerable FirstNElements(this IEnumerable enumeration, int n)
		{
			int i = 0;
			foreach (var el in enumeration)
			{
				if (i++ >= n) break;
				yield return el;
			}
		}
		public static IEnumerable<string> StringFormat(this IEnumerable enumeration, string format = "{0}")
		{
			foreach (var el in enumeration)
			{
				yield return string.Format(format, el);
			}
		}

		public static void Print(this IEnumerable enumeration, int limit = 15, string format = "{0}")
		{
			if (enumeration is ISequence sequence)
			{
				Console.WriteLine($"Name: {sequence.Name}");
			}
			Console.WriteLine($"  [ {string.Join(", ", enumeration.FirstNElements(limit).StringFormat(format))} ]");
		}
	}
}
