using System.Collections;

namespace Lab8_EN
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
    }
    class DefaultFormula : IFormula {

    }

    class GeometricSequenceSumFormula : IFormula
    {
        public double A { get; set; }
        public double R { get; set; }
        public GeometricSequenceSumFormula(double a, double r)
        {
            A = a;
            R = r;
        }
        public double Calculate(double n)
        {
            return A * (1 - Math.Pow(R, n)) / (1 - R);
        }

        public string PrintFormula()
        {
            return $"{A}(1-{R}^n)/(1-{R})";
        }

    }

    class ArithmeticSequenceSumFormula : IFormula
    {
        public double A1 { get; set; }
        public double D { get; set; }
        public ArithmeticSequenceSumFormula(double a1, double d)
        {
            A1 = a1;
            D = d;
        }
        public double Calculate(double n)
        {
            double a_n = A1 + (n - 1) * D;
            return n * (A1 + a_n) / 2;
        }

        public string PrintFormula()
        {
            return $"n*({A1}+a_n)/2";
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
            int f_n3 = 2;
            int f_n2 = 1;
            int f_n1 = 3;
            int f_n;
            yield return formula.Calculate(f_n3);
            yield return formula.Calculate(f_n2);
            yield return formula.Calculate(f_n1);
            while (true)
            {
                f_n = 2*f_n3 +3* f_n2 + f_n1;
                yield return formula.Calculate(f_n);
                f_n3 = f_n2;
                f_n2 = f_n1;
                f_n1 = f_n;
            }
        }
    }
}
