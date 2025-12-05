using System;
using System.Collections.Generic;

namespace Lab_10
{
    public static class Functions /* 1.0 Pts */
    {
        /*
            public static Func<double, double> Constant(double constantValue)
            {
                return (double xValue) => { return constantValue; };
            }

            public static Func<double, double> Identity()
            {
                return (double xValue) => { return xValue; };
            }

            public static Func<double, double> Exp(double coefficientValue)
            {
                return (double xValue) => { return coefficientValue * (double)Math.Exp(xValue); };
            }
        */

        public static Func<double, double> Constant(double constantValue) => xValue => constantValue;

        public static Func<double, double> Identity() => xValue => xValue;

        public static Func<double, double> Exp(double coefficientValue) => xValue => coefficientValue * (double)Math.Exp(xValue);
    }

    public class Function /* 1.0 Pts */
    {
        private Func<double, double> CurrentFunction { get; set; }

        public Function(Func<double, double> dotnetFunction)
        {
            this.CurrentFunction = dotnetFunction;
        }

        public static implicit operator Function(Func<double, double> dotnetFunction)
        {
            return new Function(dotnetFunction);
        }

        public double Value(double xValue)
        {
            return this.CurrentFunction(xValue);
        }

        public IEnumerable<double> GetValues(double aValue, double bValue, int nValue)
        {
            double hValue = (bValue - aValue) / nValue;

            for (int i = 0; i <= nValue; i++)
            {
                yield return this.Value(aValue + i * hValue);
            }
        }
    }

    public class Polynomial : Function /* 1.5 Pts */
    {
        private double[] coefficients;

        public static Func<double, double> ToFunction(double[] coefficientValues)
        {
            return xValue =>
            {
                if (coefficientValues.Length == 0)
                    return 0.0;

                double resultValue = coefficientValues[coefficientValues.Length-1];

                for (int index = coefficientValues.Length - 1 - 1; index >= 0; index--)
                {
                    resultValue = resultValue * xValue + coefficientValues[index];
                }

                return resultValue;
            };
        }

        public Polynomial(double[] coefficientValues) : base(ToFunction(coefficientValues))
        {
            this.coefficients = (double[])coefficientValues.Clone();
        }

        public double Derivative(double xValue)
        {
            double[] derivativeCoefficients = new double[this.coefficients.Length - 1];

            for (int i = 0; i < derivativeCoefficients.Length; ++i)
                derivativeCoefficients[i] = (i + 1) * this.coefficients[i + 1];

            return ToFunction(derivativeCoefficients)(xValue);
        }
    }

    public static class NumericalMethods /* 1.5 Pts */
    {
        public static double Derivative(this Function currentFunction, double xValue, double hValue = 0.001f)
        {
            double xValueP = currentFunction.Value(xValue + hValue);
            double xValueM = currentFunction.Value(xValue - hValue);

            return (xValueP - xValueM) / (2.0 * hValue);
        }

        public static double Integral(this Function currentFunction, double aValue, double bValue, int nValue = 100)
        {
            double hValue = (bValue - aValue) / nValue;
            double xValue = aValue;
            double resultValue = 0.0;
            for (int i=0; i<nValue; xValue += hValue, i++)
            {
                resultValue += currentFunction.Value(xValue);
            }

            return resultValue * hValue;
        }
    }
}