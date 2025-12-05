
using System;

namespace ConsoleApp1
{
    class Program
    {
        static double EvaluatePolynomial(double[] coefficients, double x)
        {
            double value = 0;
            double xx = 1.0;

            for (int i = 0; i < coefficients.Length; i++)
            {
                value += coefficients[i] * xx;
                xx *= x;
            }

            return value;
        }

        static double HornersMethod(double[] coefficients, double x)
        {
            double value = 0;

            for (int i = coefficients.Length - 1; i >= 0 ; i-- )
            {
                value += coefficients[i];
                if (i > 0)
                {
                    value *= x;
                }
            }

            return value;
        }

        static void Main(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine(args[i]);
            }

            Console.WriteLine("Hello World!");

            Console.Write("Provide degree of a polynomial: ");
            int numberOfCoefficients = int.Parse(Console.ReadLine()) + 1;
            double[] coefficients = new double[numberOfCoefficients];

            for (int i = 0; i < numberOfCoefficients; i++)
            {
                Console.Write("Provide coefficient for x^" + (numberOfCoefficients - i - 1) + ": ");
                coefficients[numberOfCoefficients - i - 1] = double.Parse(Console.ReadLine());
            }

            Console.Write("\nPolynomial: ");

            for (int i = numberOfCoefficients - 1 ; i >= 0 ; i--)
            {
                if (coefficients[i] == 0) { continue; }
                if (coefficients[i] > 0 && i < numberOfCoefficients - 1 )
                {
                    Console.Write("+");
                }
                Console.Write(coefficients[i]);
                Console.Write("x^" + i + " ");
            }

            Console.Write("\nProvide x-value: ");
            double x = double.Parse(Console.ReadLine());

            // Najpierw piszemy, że 
            Console.WriteLine("Polynomial value in point {0} is {1} - iterative method ", x, EvaluatePolynomial(coefficients, x));
            Console.WriteLine("Polynomial value in point {0} is {1} - horner's method", x, HornersMethod(coefficients, x));


            Console.WriteLine("Factorial\n");

            for (var i =0;i<=10;i++)
            {
                Console.WriteLine("Factorial of {0} is {1}", i, Factorial(i));
            }


            // try catch dodajemy, aby pokazać różnicę w zachowaniu Exception Settings
            try
            {
                Console.WriteLine("Factorial of 20 is {0}", Factorial(20));
            }
            catch
            {
                throw;
            }
        }

        static int Factorial(int value)
        {
            if (value == 0)
            {
                return 1;
            }
            return value * Factorial(value - 1);
        }

        static int FactorialIterative(int value)
        {
            int result = 1;
            for (int i =2;i<=value;i++ )
            {
                result *= value;
            }
            return result;
        }
    }
}
    