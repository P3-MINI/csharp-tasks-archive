//#define BINARY_TREE
//#define DICTIONARY_EXTENSIONS
//#define ENUMERABLE_EXTENSIONS
//#define FACTORY_METHODS
//#define NUMERICS

using System.Globalization;

namespace Lab10;

public static class Program
{
    public static void Main()
    {
#if BINARY_TREE
        BinaryTreeTests();
#endif
#if DICTIONARY_EXTENSIONS
        DictionaryTests();
#endif
#if ENUMERABLE_EXTENSIONS
        EnumerableTests();
#endif
#if FACTORY_METHODS
        FactoryMethodsTests();
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
                new BinaryTree<int>.Node(1,
                    new BinaryTree<int>.Node(2, 
                        new BinaryTree<int>.Node(3, 
                            new BinaryTree<int>.Node(4), 
                            new BinaryTree<int>.Node(5)), 
                        null), 
                    new BinaryTree<int>.Node(6, 
                        null, 
                        new BinaryTree<int>.Node(7, 
                            new BinaryTree<int>.Node(8), 
                            new BinaryTree<int>.Node(9, 
                                new BinaryTree<int>.Node(10), 
                                null))))
                );
        
        Console.WriteLine("Tree In-order:");
        tree.PrintInOrder();
    }
#endif

#if DICTIONARY_EXTENSIONS
    private static void DictionaryTests()
    {
        Console.WriteLine($"=== {CenteredString("DICTIONARY TESTS (1 point)")} ===");
        var fruits = new List<string>
        {
            "banana", "apple", "apple", "orange", "apple", "dragon fruit", "orange", "apple", "orange",
            "avocado", "banana", "blackberry", "apple", "blackcurrant", "blueberry", "orange", "cherry",
            "apple", "durian", "grape", "banana", "apple", "kiwi", "lemon", "apple", "lime", "lychee",
            "mango", "melon", "mango", "nectarines", "orange", "passion fruit", "pear", "apple", "pineapple",
            "cherry", "strawberry", "orange", "lemon", "kiwi", "plum", "pear", "apple", "pineapple", "lemon",
            "plum", "pomegranate", "raspberry pi", "rhubarb", "banana", "strawberry", "tangerine", "watermelon",
            "nectarines", "plum", "pear", "pear", "kiwi", "apple", "pear", "apple", "apple", "lemon"
        };
        var counts = DictionaryExtensions.CountWords(fruits);
        Console.WriteLine("Fruit occurrences: ");
        foreach (var (fruit, count) in counts.OrderBy(t => -t.Value))
        {
            Console.WriteLine($"{fruit,-13} : {count,2}");
        }
    }
#endif

#if ENUMERABLE_EXTENSIONS
    private static void EnumerableTests()
    {
        Console.WriteLine($"=== {CenteredString("ENUMERABLE TESTS (1 point)")} ===");
        var ints = new List<int>
        {
            3, -2, -12, 4, 5, 17, 2, 2, 0, 1, -10
        };
        Console.WriteLine("List of numbers: ");
        Console.WriteLine($"[{string.Join(", ", ints.Select(i => i.ToString()))}]");
        
        Console.WriteLine($"Sum: {ints.Sum()}");
        Console.WriteLine($"Min: {ints.Min()}");
        Console.WriteLine($"Max: {ints.Max()}");
        
        var doubles = new List<double>
        {
            3.0, -2.5, -9.0, 4.0, 5.5, 18.0, 2.0, 2.0, 0.0, 1.5, -10.5
        };
        Console.WriteLine("List of numbers: ");
        Console.WriteLine($"[{string.Join(", ", doubles.Select(i => i.ToString(CultureInfo.InvariantCulture)))}]");
        
        Console.WriteLine($"Sum: {doubles.Sum()}");
        Console.WriteLine($"Min: {doubles.Min()}");
        Console.WriteLine($"Max: {doubles.Max()}");
    }
#endif

#if FACTORY_METHODS
    private static void FactoryMethodsTests()
    {
        Console.WriteLine($"=== {CenteredString("FACTORY METHODS TESTS (1 point)")} ===");
        var piEstimation = FactoryMethods.MonteCarloPiEstimation();
        Console.WriteLine($"PI estimation: {piEstimation:#.####}");
        Console.WriteLine($"Error: {Math.Abs(piEstimation - Math.PI)}");
    }
#endif

#if NUMERICS
    private static void NumericsTests()
    {
        Console.WriteLine($"=== {CenteredString("NUMERICS TESTS (1 point)")} ===");
        var l = Numerics.FindLinearRoot();
        var q = Numerics.FindQuadraticRoot();
        var s = Numerics.FindSinRoot();

        Console.WriteLine($"Linear function root: {l:#.####}");
        Console.WriteLine($"Quadratic function root: {q:#.####}");
        Console.WriteLine($"Sinus function root: {s:#.####}");
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