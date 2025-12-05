// #define STAGE_1
// #define STAGE_2
// #define STAGE_3

namespace Lab8_EN
{
    class Program
    {
        static void Main(string[] args)
        {
            // STAGE 1
    #if STAGE_1
            List<uint> test_uints = new List<uint>() { 1, 4, 5, 6 };
            List<IFormula> formulas2 = new List<IFormula>() { new ArithmeticSequenceSumFormula(1, 2), new ArithmeticSequenceSumFormula(3, 5), new ArithmeticSequenceSumFormula(3, 2), new GeometricSequenceSumFormula(1, 2), new GeometricSequenceSumFormula(3, 5), new GeometricSequenceSumFormula(3, 2) };

            Console.WriteLine("");
            Console.WriteLine("STAGE 1 (2 PKT)");
            Console.WriteLine("");

            foreach (var formula in formulas2)
            {
                Console.WriteLine($"f(n)={formula.PrintFormula()}");
                foreach (var n in test_uints)
                    Console.WriteLine($"f({n}) = {formula.Calculate(n)}");
            }
    #endif
            // STAGE 2
    #if STAGE_2

            Console.WriteLine("");
            Console.WriteLine("STAGE 2 (2 PKT)");
            Console.WriteLine("");
            FibonacciGenerator fbg = new FibonacciGenerator(new GeometricSequenceSumFormula(2, 3));

            Console.WriteLine($"f(n) = f(n-2)+f(n-1) for n >= 2; f(2) = 1; f(1) = 0");
            Console.WriteLine($"g(n) = {fbg.Formula.Replace("n", "f(n)")}");
            Console.WriteLine("");

            int i = 1;
            foreach (var f in fbg)
            {
                Console.WriteLine($"g({i}) = {f}");
                if (i > 10) break;
                i++;
            }

            WeirdFibonacciGenerator wfbg = new WeirdFibonacciGenerator(new ArithmeticSequenceSumFormula(4, 5));

            Console.WriteLine($"f(n) = 2*f(n-3)+3*f(n-2)+f(n-1) for n >= 2; f(3) = 3; f(2) = 1; f(1) = 2");
            Console.WriteLine($"g(n) = {wfbg.Formula.Replace("n", "f(n)")}");
            Console.WriteLine("");

            int k = 1;
            foreach (var f in wfbg)
            {
                Console.WriteLine($"g({k}) = {f}");
                if (k > 10) break;
                k++;
            }
    #endif
            // STAGE 3
    #if STAGE_3
            Console.WriteLine("");
            Console.WriteLine("STAGE 3 (1 PKT)");
            Console.WriteLine("");

            FibonacciGenerator fbg3 = new FibonacciGenerator();

            Console.WriteLine($"f(n) = f(n-2)+f(n-1) for n >= 2; f(2) = 1; f(1) = 0");
            Console.WriteLine($"g(n) = {fbg3.Formula.Replace("n", "f(n)")}");
            Console.WriteLine("");

            int i3 = 1;
            foreach (var f in fbg3)
            {
                Console.WriteLine($"g({i3}) = {f}");
                if (i3 > 10) break;
                i3++;
            }

            WeirdFibonacciGenerator wfbg3 = new WeirdFibonacciGenerator();

            Console.WriteLine($"f(n) = 2*f(n-3)+3*f(n-2)+f(n-1) for n >= 2; f(3) = 3; f(2) = 1; f(1) = 2");
            Console.WriteLine($"g(n) = {wfbg3.Formula.Replace("n", "f(n)")}");
            Console.WriteLine("");

            int k3 = 1;
            foreach (var f in wfbg3)
            {
                Console.WriteLine($"g({k3}) = {f}");
                if (k3 > 10) break;
                k3++;
            }
    #endif
        }
    }
}
