using System;
using System.Collections.Generic;

namespace B
{
    class Program
    {
        static void Main(string[] args)
        {
            // STAGE 1
            List<double> test_double = new List<double>() { 1.5, 4.3, 5.2, 6.1 };
            List<uint> test_uints = new List<uint>() { 1, 4, 5, 6 };
            List<IFormula<double>> formulas1 = new List<IFormula<double>>() { new CubicFormula(1, 2, 3, 4), new CubicFormula(3, 5, 7, 1), new CubicFormula(3, 2, 1, 2) };
            List<IFormula<uint>> formulas2 = new List<IFormula<uint>>() { new GeometricSequenceSumFormula(1, 2), new GeometricSequenceSumFormula(3, 5), new GeometricSequenceSumFormula(3, 2) };

            Console.WriteLine("");
            Console.WriteLine("STAGE 1 (2.5 PKT)");
            Console.WriteLine("");
            foreach (var formula in formulas1)
            {
                Console.WriteLine($"f(x)={formula.PrintFormula()}");
                foreach (var x in test_double)
                    Console.WriteLine($"f({x}) = {formula.Calculate(x)}");
            }
            foreach (var formula in formulas2)
            {
                Console.WriteLine($"f(x)={formula.PrintFormula()}");
                foreach (var n in test_uints)
                    Console.WriteLine($"f({n}) = {formula.Calculate(n)}");
            }

            // STAGE 2

            Console.WriteLine("");
            Console.WriteLine("STAGE 2 (0.5 PKT)");
            Console.WriteLine("");
            foreach (CubicFormula formula in formulas1)
            {
                Console.WriteLine($"dy/dx {formula.PrintFormula()}");
                foreach (var x in test_double)
                    Console.WriteLine($"f'({x}) = {formula.CalculateDerivative(x)}");
            }

            // STAGE 3

            Console.WriteLine("");
            Console.WriteLine("STAGE 3 (2 PKT)");
            Console.WriteLine("");
            LucasGenerator<double> fbg = new LucasGenerator<double>(new CubicFormula(1, 2, 3, 4));

            Console.WriteLine($"f(n) = f(n-2)+f(n-1) for n >= 2; f(2) = 1; f(1) = 2");
            Console.WriteLine($"g(n) = {fbg.Formula.PrintFormula().Replace("x", "f(n)")}");
            Console.WriteLine("");

            int i = 1;
            foreach (var f in fbg)
            {
                Console.WriteLine($"g({i}) = {f}");
                if (i > 10) break;
                i++;
            }
        }
    }
}
