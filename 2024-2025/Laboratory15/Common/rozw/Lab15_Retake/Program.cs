#define Stage_BinaryTree
#define Stage_Polynomial
#define Stage_PolynomialExtension
#define Stage_RandomSequence
#define Stage_FileExtension

namespace Lab15_Retake;

internal class Program
{
    static void Main(string[] args)
    {
#if Stage_BinaryTree
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
#endif
#if Stage_Polynomial
        Console.WriteLine();

        Polynomial<float> p1 = new Polynomial<float>(1);
        Polynomial<float> p2 = new Polynomial<float>(0.5f, -1.0f, 0.75f);

        Console.WriteLine($"P1 Coefficients: {string.Join(", ", p1.Coefficients)}");
        Console.WriteLine($"P2 Coefficients: {string.Join(", ", p2.Coefficients)}");

        Polynomial<float> p3 = p1 + p2;
        Polynomial<float> p4 = p1 - p2;

        Console.WriteLine($"P3 Coefficients: {string.Join(", ", p3.Coefficients)}");
        Console.WriteLine($"P4 Coefficients: {string.Join(", ", p4.Coefficients)}");

        Console.WriteLine();

        Console.WriteLine($"P1 Get @3: {p1[3]}");
        Console.Write($"P1 Set @5: "); try { p1[5] = 3; }
        catch (ArgumentException) { Console.WriteLine("Exception Thrown, OK!"); }
#endif

#if Stage_PolynomialExtension
        Console.WriteLine();

        Console.WriteLine($"Value of P3(2.0) = {p3.Evaluate(2.0f)}");
        Console.WriteLine($"Value of P4(9.0) = {p4.Evaluate(9.0f)}");
#endif

#if Stage_RandomSequence
        Console.WriteLine();

        RandomSequence randomSequence = new RandomSequence(new Random(1234), 2, 5);

        int counter = 10; Console.Write("Random Sequence:");

        foreach (int value in randomSequence)
            if (counter-- > 0) Console.Write($" {value}"); else break;
#endif

#if Stage_FileExtension
        Console.WriteLine(); Console.WriteLine();

        Console.WriteLine("Reading Text.txt file ..");

        string[] text = File.ReadAllLines("Text.txt");

        Console.WriteLine("Mangling Text.txt file with some random values ..");

        using (FileStream fileStream = new("Mangled.txt", FileMode.Create))
        {
            fileStream.WriteMangled(text, randomSequence);
        }

        Console.WriteLine("Mangled Text.txt file to Mangled.txt ..");
#endif
    }
}
