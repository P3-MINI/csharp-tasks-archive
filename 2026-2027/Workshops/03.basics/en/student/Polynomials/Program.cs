namespace Polynomials;

internal class Program
{
    private static void Main()
    {
#if STAGE3
        var poly = new Polynomial(new Term(1, 2), new Term(-4, 0));
        Console.WriteLine($"Polynomial: {poly}");
        Console.WriteLine($"Degree: {poly.Degree}");
        Console.WriteLine($"f(0) = {poly.Evaluate(0)}");
        Console.WriteLine($"f(3) = {poly.Evaluate(3)}");

        if (poly.FindRoot(0, 3, out float root))
        {
            Console.WriteLine($"Found root in [0, 3]: {root:F4}");
        }
        else
        {
            Console.WriteLine("No root found in [0, 3].");
        }
#elif STAGE2
        var p1 = new Polynomial(new Term(2, 2), new Term(3, 1));
        var p2 = new Polynomial(new Term(1, 2), new Term(4, 0));
        var sum = p1 + p2;
        Console.WriteLine($"p1: {p1}");
        Console.WriteLine($"p2: {p2}");
        Console.WriteLine($"sum: {sum}");
        Console.WriteLine($"sum(2) = {sum.Evaluate(2)}");
#elif STAGE1
        var t1 = new Term(3, 2);
        var t2 = new Term(2, 1);
        Console.WriteLine($"t1: {t1}, Evaluate(2) = {t1.Evaluate(2)}");
        Console.WriteLine($"t2: {t2}, Evaluate(2) = {t2.Evaluate(2)}");
        Console.WriteLine($"t1 * 2 = {t1 * 2}");
        Console.WriteLine($"t1 * t2 = {t1 * t2}");
#else
        Console.WriteLine("Polynomials application starter. Configure active stage in Stage.csproj.");
#endif
    }
}
