//#define STAGE_1
//#define STAGE_2
//#define STAGE_3
//#define STAGE_4
//#define STAGE_5

using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Lab15Retake
{
    internal static class Program
    {
        private static async Task Main()
        {
            CultureInfo culture = CultureInfo.InvariantCulture;
#if STAGE_1
            Console.WriteLine("----------------- STAGE_1 (0.5) -----------------");

            Vector3D<float> vectorF1 = new Vector3D<float>(0.0f, 1.0f, 3.4f);
            Vector3D<float> vectorF2 = new Vector3D<float>(1.2f, 0.5f, 7.1f);

            Vector3D<int> vectorI1 = new Vector3D<int>(0, 1, 4);
            Vector3D<int> vectorI2 = new Vector3D<int>(1, 5, 1);

            Console.WriteLine($"Vector<float>: {vectorF1} + {vectorF2} = {vectorF1 + vectorF2}");
            Console.WriteLine($"Vector<float>: {vectorF1} * {vectorF2} = {vectorF1 * vectorF2}");

            Console.WriteLine($"Vector<float>: {vectorI1} - {vectorI2} = {vectorI1 - vectorI2}");
            Console.WriteLine($"Vector<float>: {vectorI1} / {vectorI2} = {vectorI1 / vectorI2}");

            Console.WriteLine();
#endif
#if STAGE_2
            Console.WriteLine("----------------- STAGE_2 (0.5) -----------------");

            string vectorsPath = Path.Combine(Directory.GetCurrentDirectory(), "vectors");

            foreach (string filename in Directory.EnumerateFiles(vectorsPath, "*.dat"))
            {
                FileSource expressionSource = new FileSource(filename);

                foreach (Vector3D<float> vector in expressionSource)
                    Console.Write(vector);

                Console.WriteLine();
            }

            Console.WriteLine();
#endif
#if STAGE_3
            Console.WriteLine("----------------- STAGE_3 (2.0) -----------------");

            ValueTreeNode<int> valueNodeIntA = new ValueTreeNode<int>(new Vector3D<int>(1, 2, 3));
            ValueTreeNode<int> valueNodeIntB = new ValueTreeNode<int>(new Vector3D<int>(3, 2, 1));

            Console.WriteLine($"ValueTreeNode with value of: {valueNodeIntA.Evaluate()}");
            Console.WriteLine($"ValueTreeNode with value of: {valueNodeIntB.Evaluate()}");

            Console.WriteLine($"AdditionTreeNode with value of: {new AdditionTreeNode<int>(valueNodeIntA, valueNodeIntB).Evaluate()}");
            Console.WriteLine($"SubtractionTreeNode with value of: {new SubtractionTreeNode<int>(valueNodeIntA, valueNodeIntB).Evaluate()}");
            Console.WriteLine($"MultiplicationTreeNode with value of: {new MultiplicationTreeNode<int>(valueNodeIntA, valueNodeIntB).Evaluate()}");
            Console.WriteLine($"DivisionTreeNode with value of: {new DivisionTreeNode<int>(valueNodeIntA, valueNodeIntB).Evaluate()}");

            Console.WriteLine();

            ValueTreeNode<float> valueNodeFloatA = new ValueTreeNode<float>(new Vector3D<float>(1.0f, 1.1f, 2.9f));
            ValueTreeNode<float> valueNodeFloatB = new ValueTreeNode<float>(new Vector3D<float>(0.4f, 3.8f, 1.3f));

            Console.WriteLine($"ValueTreeNode with value of: {valueNodeFloatA.Evaluate()}");
            Console.WriteLine($"ValueTreeNode with value of: {valueNodeFloatB.Evaluate()}");

            Console.WriteLine($"AdditionTreeNode with value of: {new AdditionTreeNode<float>(valueNodeFloatA, valueNodeFloatB).Evaluate()}");
            Console.WriteLine($"SubtractionTreeNode with value of: {new SubtractionTreeNode<float>(valueNodeFloatA, valueNodeFloatB).Evaluate()}");
            Console.WriteLine($"MultiplicationTreeNode with value of: {new MultiplicationTreeNode<float>(valueNodeFloatA, valueNodeFloatB).Evaluate()}");
            Console.WriteLine($"DivisionTreeNode with value of: {new DivisionTreeNode<float>(valueNodeFloatA, valueNodeFloatB).Evaluate()}");

            Console.WriteLine();
#endif
#if STAGE_4
            Console.WriteLine("----------------- STAGE_4 (1.0) -----------------");

            string expressionsPath = Path.Combine(Directory.GetCurrentDirectory(), "expressions");

            string expressionSourceA = Path.Combine(expressionsPath, "expression_0.txt");
            string expressionSourceB = Path.Combine(expressionsPath, "expression_1.txt");
            string expressionSourceC = Path.Combine(expressionsPath, "expression_2.txt");
            string expressionSourceD = Path.Combine(expressionsPath, "expression_3.txt");
            string expressionSourceE = Path.Combine(expressionsPath, "expression_4.txt");
            string expressionSourceF = Path.Combine(expressionsPath, "expression_5.txt");
            string expressionSourceG = Path.Combine(expressionsPath, "expression_6.txt");

            void PrintExpressionTreeWithResult(string fileSource, ExpressionTree<float> expressionTree)
            {
                using StreamReader streamReader = new StreamReader(fileSource);

                StringBuilder stringBuilder = new StringBuilder();

                while (streamReader.EndOfStream == false)
                    foreach (string @string in streamReader.ReadLine().Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
                        stringBuilder.Append(@string);

                Console.WriteLine($"Expression: {stringBuilder} = {expressionTree.Root.Evaluate()}");
            }

            ExpressionTree<float> expressionTreeA = new ExpressionTree<float>(expressionSourceA);
            ExpressionTree<float> expressionTreeB = new ExpressionTree<float>(expressionSourceB);
            ExpressionTree<float> expressionTreeC = new ExpressionTree<float>(expressionSourceC);
            ExpressionTree<float> expressionTreeD = new ExpressionTree<float>(expressionSourceD);
            ExpressionTree<float> expressionTreeE = new ExpressionTree<float>(expressionSourceE);
            ExpressionTree<float> expressionTreeF = new ExpressionTree<float>(expressionSourceF);
            ExpressionTree<float> expressionTreeG = new ExpressionTree<float>(expressionSourceG);

            PrintExpressionTreeWithResult(expressionSourceA, expressionTreeA);
            PrintExpressionTreeWithResult(expressionSourceB, expressionTreeB);
            PrintExpressionTreeWithResult(expressionSourceC, expressionTreeC);
            PrintExpressionTreeWithResult(expressionSourceD, expressionTreeD);
            PrintExpressionTreeWithResult(expressionSourceE, expressionTreeE);
            PrintExpressionTreeWithResult(expressionSourceF, expressionTreeF);
            PrintExpressionTreeWithResult(expressionSourceG, expressionTreeG);

            Console.WriteLine();

            List<string> invalidFileSources = new List<string>()
            {
                Path.Combine(expressionsPath, "expression_7.txt"),
                Path.Combine(expressionsPath, "expression_8.txt"),
                Path.Combine(expressionsPath, "expression_9.txt"),
            };

            foreach (string fileSource in invalidFileSources)
            {
                try
                {
                    ExpressionTree<int> expressionTree = new ExpressionTree<int>(fileSource);
                }
                catch (ArgumentException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }

            Console.WriteLine();
#endif
#if STAGE_5
            Console.WriteLine("----------------- STAGE_5 (1.0) -----------------");

            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            var primes = new PrimesSequence(1_000_000_000L, 1_001_000_000L);

            Stopwatch stopwatch = Stopwatch.StartNew();
            int countAsync = await primes.CountAsync();
            Console.WriteLine($"Primes count async: {countAsync} time: {stopwatch.Elapsed.TotalSeconds} seconds");

            stopwatch.Restart();
            long count = primes.Count();
            Console.WriteLine($"Primes count blocking: {count} time: {stopwatch.Elapsed.TotalSeconds} seconds");
#endif
        }

        private static IEnumerable<long> Range(long from, long to)
        {
            for (var value = from; value < to; value++)
                yield return value;
        }
    }
}
