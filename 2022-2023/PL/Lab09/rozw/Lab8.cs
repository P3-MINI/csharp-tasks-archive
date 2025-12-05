using System;

namespace Lab8_PL
{
    interface IFormula
    {
        double Calculate(double x)
        {
            return x;
        }
        string PrintFormula()
        {
            return "x";
        }

        public double CalculateDerivative(double x)
        {
            return 1.0;
        }
    }
    class DefaultFormula : IFormula {

    }

    class CubicFormula : IFormula
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

        public double CalculateDerivative(double x)
        {
            return 3 * A * x * x + 2 * B * x + C;
        }
    }

    class QuadraticFormula : IFormula
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
        
        public double CalculateDerivative(double x)
        {
            return 2 * A * x + B;
        }
    }

    abstract class Generator : IEnumerable
    {
        protected IFormula formula;
        public Generator(IFormula? formula = null)
        {
            this.formula = formula ?? new DefaultFormula();
        }
        public string Formula { get => formula.PrintFormula(); }
        abstract public IEnumerator GetEnumerator();
    }

    class FibonacciGenerator : Generator
    {
        public FibonacciGenerator(IFormula? formula = null) : base(formula)
        {
        }

        public override IEnumerator GetEnumerator()
        {
            int f_n2 = 0;
            int f_n1 = 1;
            int f_n;
            yield return formula.Calculate(f_n2);
            yield return formula.Calculate(f_n1);
            while (true)
            {
                f_n = f_n2 + f_n1;
                yield return formula.Calculate(f_n);
                f_n2 = f_n1;
                f_n1 = f_n;
            }
        }
    }

    class WeirdFibonacciGenerator : Generator
    {
        public WeirdFibonacciGenerator(IFormula? formula = null) : base(formula)
        {
        }

        public override IEnumerator GetEnumerator()
        {
            int f_n3 = 1;
            int f_n2 = 2;
            int f_n1 = 3;
            int f_n;
            yield return formula.Calculate(f_n3);
            yield return formula.Calculate(f_n2);
            yield return formula.Calculate(f_n1);
            while (true)
            {
                f_n = 2*f_n3 + f_n2 + f_n1;
                yield return formula.Calculate(f_n);
                f_n3 = f_n2;
                f_n2 = f_n1;
                f_n1 = f_n;
            }
        }
    }
}
