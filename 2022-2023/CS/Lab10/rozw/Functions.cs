using System;
using System.Collections.Generic;

namespace LAB10_EN
{
    //Stage 2. - 1 points
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
        public static Func<double, (double,double)> NewPoint(Func<double, double> f, Func<double, double> g) =>
            x => (f(x), g(x));

        public static Func<double, double> Power(Func<double, double> f, Func<double, double> g) => 
            x => { return Math.Pow(f(x), g(x)); };

        public static Func<double, double> CombineFunctions(Func<double, double> f, Func<double, double> g) => 
            x => f(g(x));
    }
    
    //Stage 4. - 2 points
    public static class ExtensionMethods
    {
        public static void ForEachWithBrake<T>(this IEnumerable<T> collection, Action<T> action, Func<T,bool> brakeFunction)
        {
            foreach (var elem in collection)
            {
                if (!brakeFunction(elem))
                {
                    break;
                }
                action(elem);
            }
        }

        public static List<T> Distinct<T>(this IEnumerable<T> collection, Comparison<T> comparison)
        {
            var ret = new List<T>();
            foreach (var elem in collection)
            {
                bool add = true;
                foreach (var elem2 in ret)
                {
                    if (comparison.Invoke(elem, elem2) == 0)
                    {
                        add = false;
                        break;
                    }
                }

                if (add) 
                { 
                    ret.Add(elem);
                }
            }
            

            return ret;
        }

        public static void SortRange<T>(this List<T> list, Range range, Comparison<T> comparison)
        {
            var (offset, length) = range.GetOffsetAndLength(list.Count);
            for (int i = offset; i < offset + length; i++)
            {
                for (int j = i + 1; j < offset + length; j++)
                {
                    if (comparison.Invoke(list[i], list[j])>0)
                    {
                        (list[i], list[j]) = (list[j], list[i]);
                    }
                }
            }
        }
    }
}