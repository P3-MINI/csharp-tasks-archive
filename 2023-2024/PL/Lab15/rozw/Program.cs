#define STAGE_1
#define STAGE_2
#define STAGE_3
#define STAGE_4
#define STAGE_5

using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Lab15Retake
{
    internal static class Program
    {
        private static async Task Main()
        {
#if STAGE_1
            Console.WriteLine("----------------- STAGE_1 (1.0) -----------------");

            string expressionsPath = Path.Combine(Directory.GetCurrentDirectory(), "expressions");

            foreach (string filename in Directory.EnumerateFiles(expressionsPath, "*.dat"))
            {
                FileSource expressionSource = new FileSource(filename);

                foreach (char character in expressionSource)
                    Console.Write(character);

                Console.WriteLine();
            }
#endif
#if STAGE_2
            Console.WriteLine("----------------- STAGE_2 (2.0) -----------------");

            ValueTreeNode<int> valueNodeIntA = new ValueTreeNode<int>(5);
            ValueTreeNode<int> valueNodeIntB = new ValueTreeNode<int>(9);

            Console.WriteLine($"ValueTreeNode with value of: {valueNodeIntA.Evaluate()}");
            Console.WriteLine($"ValueTreeNode with value of: {valueNodeIntB.Evaluate()}");

            Console.WriteLine($"AdditionTreeNode with value of: {new AdditionTreeNode<int>(valueNodeIntA, valueNodeIntB).Evaluate()}");
            Console.WriteLine($"SubtractionTreeNode with value of: {new SubtractionTreeNode<int>(valueNodeIntA, valueNodeIntB).Evaluate()}");
            Console.WriteLine($"MultiplicationTreeNode with value of: {new MultiplicationTreeNode<int>(valueNodeIntA, valueNodeIntB).Evaluate()}");
            Console.WriteLine($"DivisionTreeNode with value of: {new DivisionTreeNode<int>(valueNodeIntA, valueNodeIntB).Evaluate()}");

            Console.WriteLine();

            ValueTreeNode<float> valueNodeFloatA = new ValueTreeNode<float>(6.0f);
            ValueTreeNode<float> valueNodeFloatB = new ValueTreeNode<float>(9.0f);

            Console.WriteLine($"ValueTreeNode with value of: {valueNodeFloatA.Evaluate()}");
            Console.WriteLine($"ValueTreeNode with value of: {valueNodeFloatB.Evaluate()}");

            Console.WriteLine($"AdditionTreeNode with value of: {new AdditionTreeNode<float>(valueNodeFloatA, valueNodeFloatB).Evaluate()}");
            Console.WriteLine($"SubtractionTreeNode with value of: {new SubtractionTreeNode<float>(valueNodeFloatA, valueNodeFloatB).Evaluate()}");
            Console.WriteLine($"MultiplicationTreeNode with value of: {new MultiplicationTreeNode<float>(valueNodeFloatA, valueNodeFloatB).Evaluate()}");
            Console.WriteLine($"DivisionTreeNode with value of: {new DivisionTreeNode<float>(valueNodeFloatA, valueNodeFloatB).Evaluate()}");
#endif
#if STAGE_3
            Console.WriteLine("----------------- STAGE_3 (1.0) -----------------");

            FileSource expressionSourceA = new FileSource(Path.Combine(expressionsPath, "expression_0.dat"));
            FileSource expressionSourceB = new FileSource(Path.Combine(expressionsPath, "expression_1.dat"));
            FileSource expressionSourceC = new FileSource(Path.Combine(expressionsPath, "expression_2.dat"));
            FileSource expressionSourceD = new FileSource(Path.Combine(expressionsPath, "expression_3.dat"));

            ExpressionTree<int> expressionTreeA = new ExpressionTree<int>(expressionSourceA);
            ExpressionTree<int> expressionTreeB = new ExpressionTree<int>(expressionSourceB);
            ExpressionTree<int> expressionTreeC = new ExpressionTree<int>(expressionSourceC);
            ExpressionTree<int> expressionTreeD = new ExpressionTree<int>(expressionSourceD);

            void PrintExpressionTreeWithResult(FileSource fileSource, ExpressionTree<int> expressionTree)
            {
                StringBuilder stringBuilder = new StringBuilder();

                foreach (char character in fileSource)
                    stringBuilder.Append(character);

                Console.WriteLine($"Expression: {stringBuilder} = {expressionTree.Root.Evaluate()}");
            }

            PrintExpressionTreeWithResult(expressionSourceA, expressionTreeA);
            PrintExpressionTreeWithResult(expressionSourceB, expressionTreeB);
            PrintExpressionTreeWithResult(expressionSourceC, expressionTreeC);
            PrintExpressionTreeWithResult(expressionSourceD, expressionTreeD);

            Console.WriteLine();

            List<FileSource> invalidFileSources = new List<FileSource>()
            {
                new FileSource(Path.Combine(expressionsPath, "expression_11.dat")),
                new FileSource(Path.Combine(expressionsPath, "expression_12.dat")),
                new FileSource(Path.Combine(expressionsPath, "expression_13.dat")),
                new FileSource(Path.Combine(expressionsPath, "expression_14.dat")),
                new FileSource(Path.Combine(expressionsPath, "expression_15.dat")),
                new FileSource(Path.Combine(expressionsPath, "expression_16.dat")),
            };

            foreach (FileSource fileSource in invalidFileSources)
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
#endif
#if STAGE_4
            Console.WriteLine("----------------- STAGE_4 (0.5) -----------------");

            ExpressionTree<int> expressionTreePlus = expressionTreeA + expressionTreeB;
            ExpressionTree<int> expressionTreeMinus = expressionTreeA - expressionTreeC;
            ExpressionTree<int> expressionTreeMultiply = expressionTreeC * expressionTreeD;
            ExpressionTree<int> expressionTreeDivide = expressionTreeD / expressionTreeB;

            Console.WriteLine($"ExpressionTree + Operator: {(int)expressionTreeA} + {(int)expressionTreeB} = {(int)expressionTreePlus}");
            Console.WriteLine($"ExpressionTree - Operator: {(int)expressionTreeA} - {(int)expressionTreeC} = {(int)expressionTreeMinus}");
            Console.WriteLine($"ExpressionTree * Operator: {(int)expressionTreeC} * {(int)expressionTreeD} = {(int)expressionTreeMultiply}");
            Console.WriteLine($"ExpressionTree / Operator: {(int)expressionTreeD} / {(int)expressionTreeB} = {(int)expressionTreeDivide}");
#endif
#if STAGE_5
            Console.WriteLine("----------------- STAGE_5 (0.5) -----------------");

            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            Stopwatch sw = new Stopwatch();

            sw.Start();
            List<long> primesAsync = await Range(117119000000, 117120000000).FindPrimesAsync();
            sw.Stop();
            Console.WriteLine($"Primes async: {primesAsync.Count} + {sw.Elapsed.TotalSeconds:0.00} seconds.");

            sw.Reset();

            sw.Start();
            List<long> primes = Range(117119000000, 117120000000).FindPrimes();
            sw.Stop();
            Console.WriteLine($"Primes: {primes.Count} + {sw.Elapsed.TotalSeconds:0.00} seconds.");
#endif
        }

        private static IEnumerable<long> Range(long from, long to)
        {
            for (var i = from; i < to; i++)
            {
                yield return i;
            }
        }
    }
}
