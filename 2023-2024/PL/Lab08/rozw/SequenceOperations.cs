using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab08
{
	interface IMultiSeqOperation
	{
		IEnumerable Process(params ISequence[] sequence);
		string Info { get; }
	}

	static class SequenceHelpers
	{
		public static IEnumerable FirstNSeqElem(int N, IEnumerable enumeration)
		{
			int i = 0;
			foreach (var el in enumeration)
			{
				if (i++ >= N) break;
				yield return el;
			}
		}

		public static void PrintSequence(IEnumerable enumeration, int limit = 15)
		{
			if (enumeration is ISequence sequence)
			{
				Console.WriteLine($"Name: {sequence.Name}");
			}
			int index = 0;
			foreach (var el in FirstNSeqElem(limit, enumeration))
			{
				Console.WriteLine($"{index++,3}: {el}");
			}
		}

		public static IEnumerable<string> SeqFormat(IEnumerable enumeration, string format = "{0}")
		{
			foreach (var el in enumeration)
			{
				yield return string.Format(format, el);
			}
		}
	}

	class AddAllSequence : IMultiSeqOperation
	{
		public string Info { get => "Adds all value from multiple sequences"; }

		public IEnumerable Process(params ISequence[] sequence)
		{
			if (sequence.Length <= 1)
				throw new ArgumentException("Expected at least two sequences");

			var e = new IEnumerator[sequence.Length];
			for (int i = 0; i < sequence.Length; i++)
				e[i] = sequence[i].GetEnumerator();

			while (true)
			{
				bool all = true;

				for (int i = 0; i < sequence.Length; i++)
					if (!e[i].MoveNext())
					{
						all = false; break;
					}

				if (!all) break;

				int sum = 0;
				for (int i = 0; i < sequence.Length; i++)
					sum += (int)e[i].Current;

				yield return sum;
			}
		}
	}

	class AddAnySequence : IMultiSeqOperation
	{
		public string Info { get => "Adds any value from multiple sequences"; }

		public IEnumerable Process(params ISequence[] sequence)
		{
			if (sequence.Length <= 1)
				throw new ArgumentException("Expected at least two sequences");

			var e = new IEnumerator[sequence.Length];
			var b = new bool[sequence.Length];
			for (int i = 0; i < sequence.Length; i++)
				e[i] = sequence[i].GetEnumerator();

			while (true)
			{
				bool any = false;

				int sum = 0;
				for (int i = 0; i < sequence.Length; i++)
				{
					b[i] = e[i].MoveNext();
					if (b[i])
					{
						any = true;
						sum += (int)e[i].Current;
					}
				}

				if (!any) break;
				yield return sum;
			}
		}
	}
}
