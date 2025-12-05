//#define PART_1
//#define PART_2
//#define PART_3
//#define PART_4

using static Lab08.SequenceHelpers;

namespace Lab08
{
	internal class Program
	{
		static void Main(string[] args)
		{
#if PART_1
			Console.WriteLine(" -== PART 1 ==-");
			Console.WriteLine("First 5 elements:");
			foreach (var el in FirstNSeqElem(5, Enumerable.Range(1, 100)))
			{
				Console.Write($"{el} ");
			}
			Console.WriteLine("");
			foreach (var el in FirstNSeqElem(5, Enumerable.Range(3, 3)))
			{
				Console.Write($"{el} ");
			}
			Console.WriteLine("\n");

            Console.WriteLine("PrintSequnce for range(1, 15) with limit 10:");
            PrintSequence(Enumerable.Range(1, 15), 10);

			Console.WriteLine("SeqFormat:");
			string[] stringFormatedArray = SeqFormat(Enumerable.Range(1, 10)).ToArray();
			Console.WriteLine(string.Join(" ", stringFormatedArray));
			stringFormatedArray = SeqFormat(Enumerable.Range(5, 5), "|{0,6:F2}|").ToArray();
			Console.WriteLine(string.Join("~", stringFormatedArray));
			Console.WriteLine();
#endif

			int[] arr = { 1, 2, 3 };

			var seqList = new List<ISequence>();
#if PART_2
			Console.WriteLine(" -== PART 2 & 3 ==-");
			seqList.Add(new ValueSequence(arr, false));
			seqList.Add(new ValueSequence(arr, true));
			Array.Clear(arr, 0, arr.Length);
			seqList.Add(new ArithmeticSequence(0, 1));
			seqList.Add(new FibonacciSequence());
			seqList.Add(new ValueSequence(new int[] { }, true));
#endif
#if PART_3
			seqList.Add(new Exp(1));
			seqList.Add(new Sinus(1));
#endif
			foreach (var seq in seqList)
			{
				PrintSequence(seq);
				Console.WriteLine($"Preview: {seq}\n");
			}

#if PART_4
			Console.WriteLine(" -== PART 4 ==-");
			Console.WriteLine("\nAdd all:");
			var addAll = new AddAllSequence();
			arr = new int[] { 1, 2, 3 };
			PrintSequence(addAll.Process(
					new ValueSequence(arr, false),
					new ValueSequence(new int[] { 1, 1, 1, 1, 1 }, false),
					new ValueSequence(new int[] { 4, 4, 4, 4 }, false)));

			Console.WriteLine("\nAdd all exception:");
			try
			{
				PrintSequence(addAll.Process(new ValueSequence(arr, true)));
			}
			catch (ArgumentException ex)
			{
				Console.WriteLine(ex.Message);
			}

			Console.WriteLine("\nAdd all 2:");
			var seqList2 = new ISequence[] {
				new ValueSequence(new int[] { 1, 1, 1, 1, 1 }, false),
				new ArithmeticSequence(1, 2)
			};
			PrintSequence(addAll.Process(seqList2));

			Console.WriteLine("\nAdd any:");
			var addAny = new AddAnySequence();
			PrintSequence(addAny.Process(
				new ValueSequence(arr, false),
				new ValueSequence(new int[] { 1, 1, 1, 1, 1 }, false),
				new ValueSequence(new int[] { 4, 4, 4, 4 }, false)));
#endif

		}
	}
}