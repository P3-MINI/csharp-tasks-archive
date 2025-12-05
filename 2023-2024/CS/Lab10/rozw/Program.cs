#define BINARY_TREE
#define DICTIONARY_EXTENSIONS
#define ENUMERABLE_EXTENSIONS
#define BENCHMARKING
#define NUMERICS

using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Lab10EN;

public static class Program
{
    public static void Main()
    {
#if BINARY_TREE
        BinaryTreeTests();
#endif
#if BENCHMARKING
        BenchmarkingTests();
#endif
#if DICTIONARY_EXTENSIONS
        DictionaryTests();
#endif
#if ENUMERABLE_EXTENSIONS
        EnumerableTests();
#endif
#if NUMERICS
        NumericsTests();
#endif
    }

#if BINARY_TREE
    private static void BinaryTreeTests()
    {
        Console.WriteLine($"=== {"BINARY TREE TESTS (1 point)".CenteredString()} ===");
        var tree =
            new BinaryTree<int>(
                new BinaryTree<int>.Node(-1,
                    new BinaryTree<int>.Node(0,
                        new BinaryTree<int>.Node(6,
                            new BinaryTree<int>.Node(1),
                            new BinaryTree<int>.Node(6)),
                        new BinaryTree<int>.Node(9)),
                    new BinaryTree<int>.Node(-1,
                        new BinaryTree<int>.Node(9,
                            new BinaryTree<int>.Node(9),
                            new BinaryTree<int>.Node(9)),
                        new BinaryTree<int>.Node(6,
                            new BinaryTree<int>.Node(3),
                            new BinaryTree<int>.Node(9,
                                new BinaryTree<int>.Node(9),
                                new BinaryTree<int>.Node(9)))))
                );

        Console.WriteLine($"Value -1 found {tree.ValueCount(-1)} times in a tree");
        Console.WriteLine($"Value  1 found {tree.ValueCount(1)} times in a tree");
        Console.WriteLine($"Value  6 found {tree.ValueCount(6)} times in a tree");
        Console.WriteLine($"Value  9 found {tree.ValueCount(9)} times in a tree");
    }
#endif

#if BENCHMARKING
    private static void BenchmarkingTests()
    {
        Console.WriteLine($"=== {CenteredString("BENCHMARKING TESTS (1 point)")} ===");
        Console.WriteLine("Running benchmarks:");
        foreach (var benchmark in Benchmarking.CreateBenchmarks())
        {
            benchmark();
        }
    }
#endif

#if DICTIONARY_EXTENSIONS
    private static void DictionaryTests()
    {
        Console.WriteLine($"=== {CenteredString("DICTIONARY TESTS (1 point)")} ===");
        var log301 = new Dictionary<DateTime, string>
        {
            [new DateTime(2023, 12, 14, 18, 5, 0)] = "301: Opened the laboratory\n",
            [new DateTime(2023, 12, 14, 18, 7, 0)] = "301: First student came in\n",
            [new DateTime(2023, 12, 14, 18, 15, 0)] = "301: The task showed up on the Leon platform\n",
            [new DateTime(2023, 12, 14, 18, 20, 0)] = "301: Student 318874 asked about BinaryTree stage\n",
            [new DateTime(2023, 12, 14, 18, 21, 0)] = "301: Student 315731 asked about Numerics stage\n",
            [new DateTime(2023, 12, 14, 18, 26, 0)] = "301: Student 318048 returned BinaryTree stage\n",
            [new DateTime(2023, 12, 14, 18, 37, 0)] = "301: Student 319956 returned EnumerableExtensions stage\n",
            [new DateTime(2023, 12, 14, 18, 44, 0)] = "301: Student 321875 returned Numerics stage\n",
            [new DateTime(2023, 12, 14, 18, 53, 0)] = "301: Student 316975 returned Benchmarking stage\n",
            [new DateTime(2023, 12, 14, 19, 02, 0)] = "301: Student 320485 returned DictionaryExtensions stage\n",
            [new DateTime(2023, 12, 14, 19, 11, 0)] = "301: Student 318874 returned EnumerableExtensions stage\n",
            [new DateTime(2023, 12, 14, 19, 18, 0)] = "301: Student 317889 returned Numerics stage\n",
            [new DateTime(2023, 12, 14, 19, 25, 0)] = "301: Student 321267 returned BinaryTree stage\n",
            [new DateTime(2023, 12, 14, 19, 32, 0)] = "301: Student 319204 returned Numerics stage\n",
            [new DateTime(2023, 12, 14, 19, 39, 0)] = "301: Student 322301 returned Numerics stage\n",
            [new DateTime(2023, 12, 14, 19, 44, 0)] = "301: Student 316412 returned BinaryTree stage\n",
            [new DateTime(2023, 12, 14, 19, 46, 0)] = "301: Student 318874 returned Numerics stage\n",
            [new DateTime(2023, 12, 14, 19, 47, 0)] = "301: All students finished and uploaded their sources on the Leon platform\n",
            [new DateTime(2023, 12, 14, 19, 53, 0)] = "301: Last student left the laboratory\n"
        };
        var log302 = new Dictionary<DateTime, string>
        {
            [new DateTime(2023, 12, 14, 18, 12, 0)] = "302: Opened the laboratory\n",
            [new DateTime(2023, 12, 14, 18, 13, 0)] = "302: First student came in\n",
            [new DateTime(2023, 12, 14, 18, 15, 0)] = "302: The task showed up on the Leon platform\n",
            [new DateTime(2023, 12, 14, 18, 16, 0)] = "302: Student 318239 appears to be missing\n",
            [new DateTime(2023, 12, 14, 18, 20, 0)] = "302: Student 321076 asked about Numerics stage\n",
            [new DateTime(2023, 12, 14, 18, 26, 0)] = "302: Student 317225 asked about FactoryMethods stage\n",
            [new DateTime(2023, 12, 14, 18, 37, 0)] = "302: Student 318239 came in late\n",
            [new DateTime(2023, 12, 14, 18, 46, 0)] = "302: Student 319847 returned DictionaryExtensions stage\n",
            [new DateTime(2023, 12, 14, 18, 49, 0)] = "302: Student 318032 returned EnumerableExtensions stage\n",
            [new DateTime(2023, 12, 14, 18, 53, 0)] = "302: Student 317225 returned Numerics stage\n",
            [new DateTime(2023, 12, 14, 18, 57, 0)] = "302: Student 315689 returned BinaryTree stage\n",
            [new DateTime(2023, 12, 14, 19, 02, 0)] = "302: Student 320512 returned BinaryTree stage\n",
            [new DateTime(2023, 12, 14, 19, 08, 0)] = "302: Student 319008 returned Benchmarking stage\n",
            [new DateTime(2023, 12, 14, 19, 14, 0)] = "302: Student 321076 returned Numerics stage\n",
            [new DateTime(2023, 12, 14, 19, 19, 0)] = "302: Student 316714 returned FactoryMethods stage\n",
            [new DateTime(2023, 12, 14, 19, 25, 0)] = "302: Student 318921 returned Numerics stage\n",
            [new DateTime(2023, 12, 14, 19, 30, 0)] = "302: Student 321493 returned Benchmarking stage\n",
            [new DateTime(2023, 12, 14, 19, 35, 0)] = "302: Student 316958 returned DictionaryExtensions stage\n",
            [new DateTime(2023, 12, 14, 19, 44, 0)] = "302: Student 320512 returned DictionaryExtensions stage\n",
            [new DateTime(2023, 12, 14, 19, 45, 0)] = "302: Student 317225 returned Benchmarking stage\n",
            [new DateTime(2023, 12, 14, 19, 46, 0)] = "302: Student 318032 returned DictionaryExtensions stage\n",
            [new DateTime(2023, 12, 14, 19, 47, 0)] = "302: All students finished and uploaded their sources on the Leon platform\n",
            [new DateTime(2023, 12, 14, 19, 50, 0)] = "302: Last student left the laboratory\n"
        };
        var log = DictionaryExtensions.MergeLogs(log301, log302);
        Console.WriteLine("Merged log:");
        foreach (var (dt, message) in log.OrderBy(t => t.Key))
        {
            Console.Write($"{dt.ToString("F", CultureInfo.InvariantCulture)}:\n{message}");
        }
    }
#endif

#if ENUMERABLE_EXTENSIONS
    private static void EnumerableTests()
    {
        Console.WriteLine($"=== {CenteredString("ENUMERABLE TESTS (1 point)")} ===");
        var texts = new List<object>
        {
            DateTime.Now,
            "In the futuristic realm of binary dreams and algorithmic adventures, a group of aspiring computer science students embarked on a quest for knowledge. Their journey led them through the enchanted halls of code, where the syntax whispered secrets of logic and the algorithms danced in elegant choreography. ",
            2137,
            "In the algorithms' ballet, the students found themselves entranced by the beauty of efficiency and the elegance of well-optimized solutions. Each line of code they wrote was a stroke on the canvas of virtual reality, painting a picture of innovation and creativity. ",
            new Stopwatch(),
            "One day, the students encountered a mythical bug, a creature that lurked in the shadows of their software. Undeterred, they delved into the debugger's lair, armed with the sword of logical reasoning and the shield of error handling. Through the forest of edge cases and the swamp of unexpected inputs, they bravely battled until the bug was vanquished, and their code stood victorious. ",
            Console.Error,
            "As the moon of deadline approached, the students huddled around the fire of caffeine, sharing tales of recursive adventures and the legends of the great software architects. They spoke in the ancient tongue of pseudocode, and their laughter echoed in the halls of the virtual machine. ",
            () => "Not a string",
            "In the end, the computer science students emerged triumphant, their projects compiled, and their programs executed flawlessly. And so, their story became a legend in the digital kingdom, inspiring future generations to embark on their own quests for code mastery. ",
            Random.Shared,
            "And thus, in the kingdom of algorithms and syntax, the saga of the computer science students continued, an eternal loop of learning and discovery. ",
            new StringBuilder()
        };
        Console.WriteLine("Concatenated story: ");
        Console.WriteLine(texts.ConcatStrings());
    }
#endif

#if NUMERICS
    private static void NumericsTests()
    {
        Console.WriteLine($"=== {CenteredString("NUMERICS TESTS (1 point)")} ===");
        var linear = Numerics.FindLinearRoot();
        var quadratic = Numerics.FindQuadraticRoot();
        var log = Numerics.FindLogRoot();

        Console.WriteLine($"Linear function root: {linear:0.####}");
        Console.WriteLine($"Quadratic function root: {quadratic:0.####}");
        Console.WriteLine($"Natural logarithm function root: {log:0.####}");
    }
#endif

    private static string CenteredString(this string s, int width = 32)
    {
        if (s.Length >= width)
        {
            return s;
        }

        var leftPadding = (width - s.Length) / 2;
        var rightPadding = width - s.Length - leftPadding;

        return new string(' ', leftPadding) + s + new string(' ', rightPadding);
    }
}