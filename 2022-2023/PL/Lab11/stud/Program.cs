#define STAGE1
#define STAGE2
#define STAGE3
#define STAGE4

using System;
using System.Collections.Generic;


namespace LAB11_PL
{
    class Program
    {
        static void Main(string[] args)
        {
            //Etap 1 - 1.5 punkta
#if STAGE1
            {
                Console.WriteLine("STAGE 1");
                var ints = new List<int>();
                for (int i = 0; i < 100; i++)
                {
                    ints.Add(i+1);
                }

                //Wykorzystaj funkcję lambda
                int CountOdd = 0;
                var result = ints.CopyIf(/*uzupełnij*/,/*uzupełnij*/);
                
                Console.Write("List: ");
                PrintIEnumerable(result);
                Console.WriteLine($"CountOdd: {CountOdd}\n");

                var words = new List<string>()
                {
                    "Lorem",
                    "ipsum",
                    "dolor",
                    "sit",
                    "amet",
                    "consectetur",
                    "adipiscing",
                    "elit",
                    "seddoeiusmod",
                    "temporincididunt",
                    "utlabore",
                    "etdolore",
                    "quis",
                    "nostrud",
                    "exercitation ullamco laboris",
                };
                
                var samogloski = new List<char>() {'a', 'e', 'i', 'u', 'e'};
                //Wykorzystaj funkcję lambda
                words.Sort(/*uzupełnij*/);
                
                Console.Write("Sorted list: ");
                PrintIEnumerable(words);
                Console.WriteLine("\n\n");
            }
#endif
            //Etap 2 - 1.5 punkta
#if STAGE2
            {
                Console.WriteLine("STAGE 2");
                //Stwórz funkcję f
                
                var randomPoint = Generators.RandomPointFromFunction(521, 14089, f);
                Console.WriteLine("Random point from function");
                for (int i = 0; i < 10; i++)
                {
                    Console.Write(randomPoint() + "\n");
                }

                Console.WriteLine("\n\n");


                List<double> doubles = new List<double> {0.2, 5.6f, 2, 4, -24, 16.2134124, 22, 82, 8.6, 12.4};
                var projekcja = Generators.ReturnElement(doubles);
                Console.WriteLine("Return element");
                for (int i = 0; i <= doubles.Count / 2; i++)
                {
                    Console.WriteLine((i, projekcja(i)));
                }

                for (int i = 1; i <= doubles.Count / 2 + 1; i++)
                {
                    Console.WriteLine((^i, projekcja(^i)));
                }

                Console.WriteLine("\n\n");
            }
#endif
            //Etap 3 - 0.5 punkta
#if STAGE3
            {
                Console.WriteLine("Stage 3");
                //f = x +3
                
                //g = x*x
                
                Console.WriteLine("Combine");
                var combine = FunctionsManipulator.Combine(f, g);
                for (int i = -5; i <= 5; i++)
                {
                    Console.WriteLine($"x: {i}, f(g(x)): {combine(i)}");
                }

                Console.WriteLine("\n\n");

                //f = (x+1)^2
                
                var dfdx = FunctionsManipulator.Derivative(f);
                Console.WriteLine("Derivative");
                for (int i = -5; i <= 5; i++)
                {
                    Console.WriteLine($"x: {i}, f(x): {f(i)}, dfdx: {dfdx(i)}");
                }

                Console.WriteLine("\n\n");
            }
#endif
            //Etap 4 - 1.5 punkta
#if STAGE4
            {
                Console.WriteLine("STAGE 4");
                Random random = new Random(0);
                Console.WriteLine("Partition extension");
                var ints = new List<int>
                    {10, -5, 0, 4, 6, 2321, 92, 1, 19, 1, 21, 20, 10, 4, 3, 6, 18, 12, 5, -2452, 342, -67, 2};
                ;
                PrintIEnumerable(ints);

                //Uzupelnij
                var lists = ints.Partition(21, /*uzupełnij*/);
                var it = 0;
                foreach (var list in lists)
                {
                    Console.Write($"List {it++}:\n");
                    foreach (var elem in list)
                    {
                        Console.Write($"{elem}, ");
                    }

                    Console.WriteLine("\n");
                }

                Console.WriteLine("\n\n\n");


                Console.WriteLine("Merge extension");
                var doubles = new List<double>();
                var doubles2 = new List<double>();
                for (int i = 0; i < 10; i++)
                {
                    doubles.Add(random.NextDouble() * 20);
                }

                for (int i = 0; i < 20; i++)
                {
                    var next = random.Next() / 10e7;
                    if (i % 2 == 0)
                    {
                        next = -next;
                    }
                    doubles2.Add(next);
                }

                Console.WriteLine("Lista A:");
                PrintIEnumerable(doubles);
                Console.WriteLine("Lista B:");
                PrintIEnumerable(doubles2);

                //uzupełnij
                var mergedDoubles = doubles.Merge(doubles2, /*uzupełnij*/);
                Console.WriteLine("Lista wynikowa:");
                PrintIEnumerable(mergedDoubles);
            }
#endif
        }

        private static void PrintIEnumerable<T>(IEnumerable<T> ints)
        {
            foreach (var i in ints)
            {
                Console.Write(i + ", ");
            }

            Console.WriteLine();
        }
    }
}