using System;

namespace Lab_10
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-------------------------- Stage_1 (1.0) --------------------------");
            {
                Func<double, double> constantFunctionZero = Functions.Constant(0.0f);
                Func<double, double> constantFunctionOne = Functions.Constant(1.0f);

                Func<double, double> identityFunctionZero = Functions.Identity();

                Func<double, double> exponentialfunctionZero = Functions.Exp(0.0f);
                Func<double, double> exponentialfunctionOne = Functions.Exp(0.15f);

                {
                    Console.WriteLine($"Constant Function #0    : {constantFunctionZero(5.0f),2:F} (Should be 0.00)");
                    Console.WriteLine($"Constant Function #1    : {constantFunctionOne(5.0f),2:F} (Should be 1.00)");

                    Console.WriteLine($"Identity Function #0    : {identityFunctionZero(5.0f),2:F} (Should be 5.00)");

                    Console.WriteLine($"Exponential Function #0 : {exponentialfunctionZero(5.0f),2:F} (Should be 0.00)");
                    Console.WriteLine($"Exponential Function #1 : {exponentialfunctionOne(5.0f),2:F} (Should be 22.26)");
                }
            }

            Console.WriteLine("-------------------------- Stage_2 (1.0) --------------------------");
            {
                Function constantFunctionZero = Functions.Constant(1.0f);
                Function identityFunctionZero = Functions.Identity();

                Function exponentialfunctionZero = new Function(Functions.Exp(1.0f));

                {
                    Console.WriteLine($"Constant Function #0    : {constantFunctionZero.Value(5.0f),2:F} (Should be 1.00)");
                    Console.WriteLine($"Identity Function #0    : {identityFunctionZero.Value(5.0f),2:F} (Should be 5.00)");

                    Console.Write($"Exponential Function #0 : ");
                    foreach (double currentValue in exponentialfunctionZero.GetValues(1.0f, 2.0f, 5))
                        Console.Write($"{currentValue,2:F} ");

                    Console.WriteLine("(Shoule be 2.72 3.32 4.06 4.95 6.05 7.39)");
                }
            }

            Console.WriteLine("-------------------------- Stage_3 (1.5) --------------------------");
            {
                Polynomial polynomialFunctionZero = new Polynomial(new double[] { 4.0f, 1.0f, 1.0f });
                Polynomial polynomialFunctionOne = new Polynomial(new double[] { 0.0f, 2.0f });

                Console.WriteLine($"Polynomial Function #0   : {polynomialFunctionZero.Value(2.0f),2:F} (Should be 10.00)");
                Console.WriteLine($"Polynomial Function #1   : {polynomialFunctionOne.Value(3.0f),2:F} (Should be 6.00)");

                Console.WriteLine($"Polynomial Derivative #0 : {polynomialFunctionOne.Derivative(2.25f),2:F} (Should be 2.00)");
            }

            Console.WriteLine("-------------------------- Stage_4 (1.5) --------------------------");
            {
                Function exponentialfunctionZero = Functions.Exp(1.0f);

                Polynomial polynomialFunctionZero = new Polynomial(new double[] { 0.0f, 2.0f });
                Polynomial polynomialFunctionOne = new Polynomial(new double[] { 4.0f, 1.5f, 1.0f, 7.5f, 12345.6f});

                Console.WriteLine($"Integral Function #0                 : {exponentialfunctionZero.Integral(0.0f, 2.0f),2:F} (Should be 6.33)");
                Console.WriteLine($"Integral Function #1                 : {polynomialFunctionZero.Integral(0.0f, 2.0f),2:F} (Should be 3.96)");

                Console.WriteLine($"Derivative Function #0               : {exponentialfunctionZero.Derivative(2.25f),2:F} (Should be 9.49)");

                Console.WriteLine($"Polynomial Derivative #1 (formula)   : {polynomialFunctionOne.Derivative(2.25f),2:F} (Should be 562616.29)");
                Console.WriteLine($"Polynomial Derivative #1 (numerical) : {NumericalMethods.Derivative(polynomialFunctionOne,2.25f),2:F} (Should be 562616.40)");

            }
        }
    }
}
