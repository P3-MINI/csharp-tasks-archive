using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace LAB_2021_CS_10A
{
    class Program
    {
        static void Main(string[] args)
        {
            //Stage 1. - 1 point
            {
                Console.WriteLine("STAGE 1");
                List<int> ints = new List<int> {1, 23, -56, 4, -563, 1241, -1, 0};
                Console.Write("List: ");
                PrintList(ints);
                //Use lambda function here
                ints.Sort((x, y) =>
                {
                    if (x >= 0)
                    {
                        if (y >= 0)
                            return y - x;
                        return -1;
                    }
                    else
                    {
                        if (y >= 0)
                            return 1;
                        if (y < x)
                            return 1;
                        return -1;
                    }
                });
                Console.Write("Sorted list: ");
                PrintList(ints);
                Console.WriteLine("\n\n");

                int absSum = 0;
                //Use lambda function here
                ints.ForEach(i => absSum += i < 0 ? Math.Abs(i) : 0);
                Console.Write("List: ");
                PrintList(ints);
                Console.WriteLine($"Absolute value of the sum of negative elements: {absSum}");
            }

            //Stage 2. - 1 point 
            {
                Console.WriteLine("STAGE 2");
                Console.WriteLine("Constant function");
                var funC = BaseFunctions.ConstantFunction(5);
                for (int i = 13; i < 27; i++)
                {
                    Console.WriteLine((i, funC(i)));
                }
                Console.WriteLine("\n\n");

                Console.WriteLine("Quadratic function");
                var funQ = BaseFunctions.QuadraticFunction(10, 6, 8);
                for (int i = 0; i <= 10; i++)
                {
                    Console.WriteLine((i, funQ(i)));
                }
                Console.WriteLine("\n\n");

                Console.WriteLine("Polynomial function");
                var funP = BaseFunctions.PolynomialFunction(10, 6, 8, -3, -14, 1);
                for (int i = 0; i <= 10; i++)
                {
                    Console.WriteLine((i, funP(i)));
                }
                Console.WriteLine("\n\n");
            }

            //Stage 3. - 1 point
            {
                Console.WriteLine("STAGE 3");
                Console.WriteLine("Max function");
                var f = BaseFunctions.ConstantFunction(-3);
                var g = BaseFunctions.QuadraticFunction(0, 1, -2);
                var funM = FunctionsManipulator.Max(f, g);
                for (int i = -5; i <= 5; i++)
                {
                    Console.WriteLine((i, funM(i)));
                }
                Console.WriteLine("\n\n");

                Console.WriteLine("Difference function");
                f = BaseFunctions.ConstantFunction(0);
                g = BaseFunctions.QuadraticFunction(1, 0, -4);
                var funD = FunctionsManipulator.Difference(f, g);
                for (int i = -5; i <= 5; i++)
                {
                    Console.WriteLine((i, funD(i)));
                }
                Console.WriteLine("\n\n");
                
                Console.WriteLine("Combine functions");
                f = BaseFunctions.QuadraticFunction(1, 0, 0);
                g = BaseFunctions.QuadraticFunction(0, 1, -4);
                var funC = FunctionsManipulator.CombineFunctions(f, g);
                for (int i = -5; i <= 5; i++)
                {
                    Console.WriteLine((i, funC(i)));
                }
                Console.WriteLine("\n\n");
            }

            //Stage 4. - 1.5 points
            {
                Console.WriteLine("STAGE 4");
                Random random = new Random(0);
                Console.WriteLine("Invoke action extension");
                var ints = new List<int>();
                for (int i = 0; i < 10; i++)
                {
                    ints.Add(i);
                }
                PrintList(ints);
                
                //Action to fill
                ints.InvokeAction(x =>
                {
                    if (x % 2 == 0)
                        Console.WriteLine($"{x} is even");
                    else
                        Console.WriteLine($"{x} is odd");
                });
                Console.WriteLine("\n\n");
                
                Console.WriteLine("Transform extension");
                var doubles = new List<double>();
                for (int i = 0; i < 10; i++)
                {
                    doubles.Add(random.NextDouble());
                }
                PrintList(doubles);

                //Transform function to fill
                var transformedDoubles = doubles.Transform(x => x >= 0.5 ? 1 : 0);
                PrintList(transformedDoubles);
            }
        }

        private static void PrintList<T>(List<T> ints)
        {
            foreach (var i in ints)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine();
        }
    }
}
