using System;
using System.Collections.Generic;

namespace LAB_2021_CS_10A
{
    //Stage 2. - 1.5 points
    public static class BaseFunctions
    {
        public static Func<double, double> ConstantFunction(double a)
        {
            return delegate(double x) { return a; };
        }

        public static Func<double, double> QuadraticFunction(double a, double b, double c)
        {
            return delegate(double x) { return a * x * x + b * x + c;};
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
    
    //Stage 3. - 1 point
    public static class FunctionsManipulator
    {
        public static Func<double, double> Max(Func<double, double> f, Func<double, double> g) =>
            x => Math.Max(f(x), g(x));

        public static Func<double, double> Difference(Func<double, double> f, Func<double, double> g) => 
            x => { return Math.Abs(f(x) - g(x)); };

        public static Func<double, double> CombineFunctions(Func<double, double> f, Func<double, double> g) => 
            x => f(g(x));
    }
    
    //Stage 4. - 1.5 points
    public static class ExtensionMethods
    {
        public static void InvokeAction(this IEnumerable<int> collection, Action<int> action)
        {
            foreach (var elem in collection)
            {
                action(elem);
            }
        }

        public static List<double> Transform(this IEnumerable<double> collection, Func<double, double> transformFunc)
        {
            var ret = new List<double>();
            foreach (var elem in collection)
            {
                ret.Add(transformFunc(elem));
            }

            return ret;
        }
    }
}