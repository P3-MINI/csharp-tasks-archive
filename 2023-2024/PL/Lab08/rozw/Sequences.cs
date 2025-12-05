using System;
using System.Collections;
using System.Collections.Generic;
using static Lab08.SequenceHelpers;

namespace Lab08
{
	interface ISequence : IEnumerable
	{
		string Name { get; }
	}

	class ValueSequence : ISequence
	{
		private int[] values;
		bool loop;

		public ValueSequence(int[] values, bool loop = true)
		{
			this.loop = loop;
			this.values = (int[])values.Clone(); // Tu zwrócić uwagę czy zrobili Clone'a
		}

		public string Name { get => loop ? "Looped Value Sequence" : "Value Sequence"; }

		public override string ToString()
		{
			return $"[{string.Join(", ", SeqFormat(FirstNSeqElem(5, this)))}, ...]";
		}

		public IEnumerator GetEnumerator()
		{
			int i = 0;
			while (values.Length > 0)
			{
				if (i >= values.Length)
				{
					if (loop) { i = 0; }
					else break;
				}
				yield return values[i++];
			}
		}
	}

	class ArithmeticSequence : ISequence
	{
		private int start;
		private int step;

		public ArithmeticSequence(int start, int step)
		{
			this.start = start;
			this.step = step;
		}

		public string Name { get => $"Arithmetic Sequence {{ Start = {start}, Step = {step} }}"; }

		public override string ToString()
		{
			return $"[{string.Join(", ", SeqFormat(FirstNSeqElem(5, this)))}, ...]";
		}

		public IEnumerator GetEnumerator()
		{
			int i = 0;
			while (true)
				yield return start + i++ * step;
		}

	}

	class FibonacciSequence : ISequence
	{
		public string Name { get => "Fibonacci Sequence"; }

		public override string ToString()
		{
			return $"[{string.Join(", ", SeqFormat(FirstNSeqElem(5, this)))}, ...]";
		}
		public IEnumerator GetEnumerator()
		{
			int i = 0;
			int j = 1;

			yield return i;
			yield return j;

			while (true)
			{
				(j, i) = (i + j, j);
				yield return j;
			}
		}

	}
	class Exp : ISequence
	{
		decimal x;
		public Exp(decimal x)
		{
			this.x = x;
		}

		public string Name { get => $"Exponent expansion sequence in {x}"; }

		public IEnumerator GetEnumerator()
		{
			decimal index = 1;
			decimal sum = 1;
			decimal value = 1;

			yield return sum;

			while (true)
			{
				value *= x / index++;
				sum += value;
				yield return sum;
			}

		}
	}

	class Sinus : ISequence
	{
		decimal x;
		public Sinus(decimal x)
		{
			this.x = x;
		}

		public string Name { get => $"Sinus expansion sequence in {x}"; }

		public IEnumerator GetEnumerator()
		{
			decimal index = 1;
			decimal sum = x;
			decimal value = x;

			yield return sum;

			while (true)
			{
				value *= -x * x / (++index * ++index);
				sum += value;
				yield return sum;
			}
		}
	}
}
