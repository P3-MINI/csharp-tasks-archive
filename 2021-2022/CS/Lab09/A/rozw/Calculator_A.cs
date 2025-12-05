using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace A
{

    interface IFormula<T>
    {
        double Calculate(T x);
        string PrintFormula();
    }
    class QuadraticFormula : IFormula<double>
    {
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }
        public QuadraticFormula(double _a, double _b, double _c)
        {
            A = _a;
            B = _b;
            C = _c;
        }
        public double Calculate(double x)
        {
            return A * x * x + B * x + C;
        }

        public string PrintFormula()
        {
            return $"{A}*x^2+{B}*x+{C}";
        }
    }
    class ArithmeticSequenceSumFormula : IFormula<uint>
    {
        public double A1 { get; set; }
        public double D { get; set; }
        public ArithmeticSequenceSumFormula(double a1, double d)
        {
            A1 = a1;
            D = d;
        }
        public double Calculate(uint n)
        {
            double a_n = A1 + (n - 1) * D;
            return n * (A1 + a_n) / 2;
        }

        public string PrintFormula()
        {
            return $"n*({A1}+a_n)/2";
        }
    }
    static class Calculator
    {
        public static double CalculateDerivative(this QuadraticFormula formula, double x)
        {
            return 2 * formula.A * x + formula.B;
        }
    }
    class FibonnaciGenerator<T> : IEnumerable
    {
        public IFormula<T> Formula { get; }
        public FibonnaciGenerator(IFormula<T> _formula)
        {
            Formula = _formula;
        }
        public IEnumerator GetEnumerator()
        {
            dynamic f_n2 = 0;
            dynamic f_n1 = 1;
            dynamic f_n;
            yield return Formula.Calculate(f_n2);
            yield return Formula.Calculate(f_n1);
            while (true)
            {
                f_n = f_n2 + f_n1;
                yield return Formula.Calculate(f_n);
                f_n2 = f_n1;
                f_n1 = f_n;
            }
        }
    }
}
