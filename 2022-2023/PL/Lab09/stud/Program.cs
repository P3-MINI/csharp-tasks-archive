using System;
using System.Collections;
using System.Collections.Generic;

// #define STAGE_1
// #define STAGE_2
// #define STAGE_3

namespace Lab8_PL
{
    class Program
    {
        static void Main(string[] args)
        {
            // STAGE 1
    #if STAGE_1
            List<double> test_double = new List<double>() { 1.5, 4.3, 5.2, 6.1 };
            List<IFormula> formulas1 = new List<IFormula>() { new QuadraticFormula(1, 2, 3), new QuadraticFormula(3, 5, 7), new QuadraticFormula(3, 2, 1), new CubicFormula(1, 2, 3, 4), new CubicFormula(3, 5, 7, 1), new CubicFormula(3, 2, 1, 2) };

            Console.WriteLine("");
            Console.WriteLine("STAGE 1 (2 PKT)");
            Console.WriteLine("");
            foreach (var formula in formulas1)
            {
                Console.WriteLine($"f(x)={formula.PrintFormula()}");
                foreach (var x in test_double)
                    Console.WriteLine($"f({x}) = {formula.Calculate(x)}");
            }

            foreach (IFormula formula in formulas1)
            {
                Console.WriteLine($"dy/dx {formula.PrintFormula()}");
                foreach (var x in test_double)
                    Console.WriteLine($"f'({x}) = {formula.CalculateDerivative(x)}");
            }
    #endif        
            // STAGE 2
    #if STAGE_2
            Console.WriteLine("");
            Console.WriteLine("STAGE 2 (2 PKT)");
            Console.WriteLine("");
            FibonacciGenerator fbg = new FibonacciGenerator(new QuadraticFormula(2, 3, 4));

            Console.WriteLine($"f(n) = f(n-2)+f(n-1) for n >= 2; f(2) = 1; f(1) = 0");
            Console.WriteLine($"g(n) = {fbg.Formula.Replace("x", "f(n)")}");
            Console.WriteLine("");

            int i = 1;
            foreach (var f in fbg)
            {
                Console.WriteLine($"g({i}) = {f}");
                if (i > 10) break;
                i++;
            }

            WeirdFibonacciGenerator wfbg = new WeirdFibonacciGenerator(new CubicFormula(1, 2, 3, 4));

            Console.WriteLine($"f(n) = 2*f(n-3)+f(n-2)+f(n-1) for n >= 2; f(3) = 3; f(2) = 2; f(1) = 1");
            Console.WriteLine($"g(n) = {wfbg.Formula.Replace("x", "f(n)")}");
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
            Console.WriteLine($"g(n) = {fbg3.Formula.Replace("x", "f(n)")}");
            Console.WriteLine("");

            int i3 = 1;
            foreach (var f in fbg3)
            {
                Console.WriteLine($"g({i3}) = {f}");
                if (i3 > 10) break;
                i3++;
            }

            WeirdFibonacciGenerator wfbg3 = new WeirdFibonacciGenerator();

            Console.WriteLine($"f(n) = 2*f(n-3)+f(n-2)+f(n-1) for n >= 2; f(3) = 3; f(2) = 2; f(1) = 1");
            Console.WriteLine($"g(n) = {wfbg3.Formula.Replace("x", "f(n)")}");
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
