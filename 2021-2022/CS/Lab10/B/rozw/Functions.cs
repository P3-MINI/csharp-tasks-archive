using System;
using System.Collections.Generic;

namespace LAB_2021_CS_10B
{
    //Stage 2 - 1.5 points
    public static class BaseFunctions
    {
        public static Func<double, double> ConstantFunction(double a)
        {
            return delegate(double x) { return a; };
        }

        public static Func<double, double> ModulusFunction(double a, double b)
        {
            return delegate(double x) { return Math.Abs(a * x + b);};
        }

        public static Func<double, double> PolynomialFunction(params double[] coefficients)
        {
            return delegate(double x)
            {
                double retValue = 0;
                for (int i = 0; i < coefficients.Length; i++)
                {
                    retValue = retValue * x + coefficients[i];
                }

                return retValue;
            };
        }
    }
    
    //Stage 3 - 1 point
    public static class FunctionsManipulator
    {
        public static Func<double, double> ChooseFunction(Func<double, double> f, Func<double, double> g) =>
            x =>
            {
                if (x - (int) x > 0.5)
                    return f(x);
                return g(x);
            };

        public static Func<double, double> MultiplyFunctions(Func<double, double> f, Func<double, double> g) =>
            x => f(x) * g(x);

        public static Func<double, double> CombineFunctions(Func<double, double> f, Func<double, double> g) =>
            x => f(g(x));
    }
    
    //Stage 4 - 1.5 points
    public static class ExtensionMethods
    {
        public static void PerformAction(this IEnumerable<Person> collection, Action<Person, int> action)
        {
            int i = 0;
            foreach (var elem in collection)
            {
                action(elem, i++);
            }
        }

        public static double Accumulate(this IEnumerable<Person> collection, Func<Person, double, double> accumulateFunc, double sum)
        {
            foreach (var elem in collection)
            {
                sum = accumulateFunc(elem,sum);
            }

            return sum;
        }
    }
}