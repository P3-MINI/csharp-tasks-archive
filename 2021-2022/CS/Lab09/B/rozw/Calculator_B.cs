using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace B
{
    interface IFormula<T>
    {
        double Calculate(T x);
        string PrintFormula();
    }
    class CubicFormula : IFormula<double>
    {
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }
        public double D { get; set; }
        public CubicFormula(double _a, double _b, double _c, double _d)
        {
            A = _a;
            B = _b;
            C = _c;
            D = _d;
        }
        public double Calculate(double x)
        {
            return A * x * x *x + B * x * x + C * x + D;
        }

        public string PrintFormula()
        {
            return $"{A}*x^3+{B}*x^2+{C}*x+{D}";
        }
    }
    class GeometricSequenceSumFormula : IFormula<uint>
    {
        public double A { get; set; }
        public double R { get; set; }
        public GeometricSequenceSumFormula(double a, double r)
        {
            A = a;
            R = r;
        }
        public double Calculate(uint n)
        {
            return A * (1 - Math.Pow(R, n)) / (1 - R);
        }

        public string PrintFormula()
        {
            return $"{A}(1-{R}^n)/(1-{R})";
        }
    }
    static class Calculator
    {
        public static double CalculateDerivative(this CubicFormula formula, double x)
        {
            return 3 * formula.A * x * x + 2 * formula.B * x + formula.C;
        }
    }
    class LucasGenerator<T> : IEnumerable
    {
        public IFormula<T> Formula { get; }
        public LucasGenerator(IFormula<T> _formula)
        {
            Formula = _formula;
        }
        public IEnumerator GetEnumerator()
        {
            dynamic f_n2 = 2;
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
