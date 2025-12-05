#define STAGE_1
#define STAGE_2
#define STAGE_3
#define STAGE_4

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PL_Lab05
{
    class Program
    {
        static void Main(string[] args)
        {
            int errors = 0;

            #region ETAP_1
            Console.WriteLine("*********Etap 1.*********");
#if STAGE_1
            Console.WriteLine("Etap 1. rozpoczety");
            {
                int errors_1 = 0;

                Type node = typeof(Node);

                if (!node.IsAbstract)
                {
                    errors_1++;
                    PrintError("Klasa \"Node\" powinna byc klasa abstakcyjna");
                }

                if (node.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null,
                    new Type[] {typeof(string)}, null) is null)
                {
                    errors_1++;
                    PrintError("Klasa \"Node\" powinna mieć konstruktor przyjmujacy parametr typu \"string\"");
                }

                errors_1 += TestTypeForFunction(
                    node,
                    "Evaluate",
                    new Type[] { },
                    typeof(double),
                    "Klasa \"Node\" powinna miec metode \"Evaluate\" nieprzyjmujaca zadnego parametru",
                    "Metoda \"Evaluate\" powinna zwracac parametr typu \"double\"",
                    true);

                PrintStageSummary(errors_1, 1);
                errors += errors_1;
            }
#else
            Console.WriteLine("Etap 1. pominiety");
#endif
            #endregion
            
            #region ETAP_2
            Console.WriteLine("*********Etap 2.*********");
#if STAGE_2
            Console.WriteLine("Etap 2. rozpoczety");
            {
                int errors_2 = 0;

                Node[] nodes = new Node[]
                {
                    new ArithmNode(ArithmOperation.Addition),
                    new ArithmNode(ArithmOperation.Subtraction),
                    new ArithmNode(ArithmOperation.Multiplication),
                    new ArithmNode(ArithmOperation.Division),
                    new ValueNode(38),
                    new ValueNode(382912.21),
                    new ValueNode(-5),
                    new ValueNode(),
                    new ValueNode(17.2f),
                    new ArithmNode((ArithmOperation.Addition,new ValueNode(10), new ValueNode()))
                };


                if (nodes[0].Evaluate() != 0)
                {
                    PrintError("Dodawanie - Przy braku conajmniej jednego z poddrzew wynik powinien byc 0");
                }

                nodes[1].right = nodes[7];
                if (nodes[1].Evaluate() != 0)
                {
                    PrintError("Przy braku conajmniej jednego z poddrzew wynik powinien byc 0");
                }

                nodes[1].left = nodes[7];
                nodes[1].right = null;

                if (nodes[1].Evaluate() != 0)
                {
                    PrintError("Przy braku conajmniej jednego z poddrzew wynik powinien byc 0");
                }

                nodes[1].right = nodes[8];
                if (Math.Abs(nodes[1].Evaluate() - (nodes[7].Evaluate() - nodes[8].Evaluate())) > double.Epsilon)
                {
                    PrintError("Zly wynik dla odejmowania");
                }

                nodes[3].right = new ValueNode(0);
                nodes[3].left = new ValueNode(5.0);

                if (nodes[3].Evaluate() != 0)
                {
                    PrintError("Przy dzieleniu przez 0 wynik powinien byc 0");
                }

                nodes[4].left = new ValueNode(1);
                nodes[4].right = new ValueNode(0);

                foreach (Node n in nodes)
                {
                    if (n is ValueNode)
                    {
                        Console.WriteLine("Value node:");
                    }
                    if (n is ArithmNode)
                    {
                        Console.WriteLine("Arithm node:");
                    }
                    Console.WriteLine(n);
                    Console.WriteLine();
                }
                
                PrintStageSummary(errors_2, 2);
                errors += errors_2;
                
                
                Console.WriteLine("Sprawdzanie kopiowania");
                var vN = new ValueNode();
                var aN = new ArithmNode(ArithmOperation.Addition);

                var vNCopy = vN.Clone();

                Console.WriteLine($"ValueNode.Clone()\n\nOryginalny:{vN}\nNowy:{vNCopy}\n");

                aN.right = vN;
                var aNCopy = aN.Clone();
                
                Console.WriteLine($"ArithmNode.Clone()\n\nOryginalny:{aN}\nOryginalny.right:{aN.right}\nNowy:{aNCopy}\nNowy.right:{aNCopy.right}\n");
            }

#else
            Console.WriteLine("Etap 2. pominiety");
#endif
            #endregion
            
            #region ETAP_3

            Console.WriteLine("*********Etap 3.*********");
#if STAGE_3
            Console.WriteLine("Etap 3. rozpoczety");
            {
                int errors_3 = 0;

                Node[] nodes = null;

                try
                {
                    nodes = NodesFactory.CreateMultipleNodes(new[] { "0", "5.0", "-5", "6.0", "+", "-", "/", "*"});
                }
                catch (Exception)
                {
                    PrintError("Blad przy tworzeniu obiektow za pomoca fabryki");
                    goto Summary;
                }

                foreach (var node in nodes)
                {
                    if (node == null)
                    {
                        PrintError("Blad przy tworzeniu obiektu (jest \"null\")");
                        goto Summary;
                    }
                }

                try
                {
                    nodes = NodesFactory.CreateMultipleNodes(null);
                }
                catch (Exception)
                {
                    PrintError("Blad przy tworzeniu obiektow dla parametru o wartosci \"null\"");
                    goto Summary;
                }

                string[] ctrlStr = {"+", "/", "*", "-5", "6.0", "3", "50", "/", "*", null};
                foreach (var s in ctrlStr)
                {
                    try
                    {
                        var node = NodesFactory.CreateNode(s);
                    }
                    catch (Exception)
                    {
                        PrintError("Blad przy tworzeniu pojedynczego obiektu za pomoca fabryki");
                        goto Summary;
                    }
                }

                ctrlStr = ctrlStr[..7];
                ctrlStr[0] = "*";
                var head = NodesFactory.CreateTree(NodesFactory.CreateMultipleNodes(ctrlStr));
                var expectedResult = -125;
                if (Math.Abs(head.Evaluate() - expectedResult) > double.Epsilon)
                {
                    PrintError($"Blad w obliczeniach an drzewie - prawdopodobnie zle stworzone drzewo.\nOczekiwany wynik: {expectedResult},\notrzymano: {head.Evaluate()}.");
                }

                Summary:
                PrintStageSummary(errors_3, 3);
                errors += errors_3;
                
            }
#else
            Console.WriteLine("Etap 3. pominiety");
#endif
            #endregion
            
            #region ETAP_4
            Console.WriteLine("*********Etap 4.*********");
#if STAGE_4
            Console.WriteLine("Etap 4. rozpoczety");
            {
                var errors_4 = 0;

                var nodes = new ValueNode[]
                {
                    new ValueNode(2),
                    new ValueNode(43),
                    new ValueNode(24),
                    new ValueNode(),
                    new ValueNode(-21232),
                    new ValueNode(43.12),
                    new ValueNode(9214.12),
                    new ValueNode(-21341.212),
                    new ValueNode(1231)
                };

                var sortedNodes = ValueNode.Sort(nodes);
                for (int i = 0; i < sortedNodes.Length - 1; i++)
                {
                    if (sortedNodes[i].Evaluate() > sortedNodes[i + 1].Evaluate())
                    {
                        errors_4++;
                        PrintError("Wierzcholki nie sa posortowane rosnaco");
                    }
                }

                PrintStageSummary(errors_4, 4);
                errors += errors_4;
            }
#else
            Console.WriteLine("Etap 4. pominiety");
#endif
            #endregion
            
            #region PODSUMOWANIE
            Console.WriteLine("*********Wyniki:*********");
            if (errors == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Brak bledow!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Znaleziono {0} bledow!", errors);
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.WriteLine("Wcisnij dowolny przycisk, zeby zamknac okno");
            Console.ReadKey();
            #endregion
        }
        
        #region Pomocnicze
        private static void PrintError(String errorMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Blad: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(errorMessage);
        }

        private static void PrintStageSummary(int errors, int stageNumber)
        {
            if (errors > 0)
            {
                Console.Write($"Liczba bledow w etapie {stageNumber}. to: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("{0}", errors);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Brak bledow");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        static int TestTypeForFunction(Type type, String methodName, Type[] parametersTypes, Type returnType,
            String notExistsMessage, String wrongReturnTypeMessage, bool isAbsract = false)
        {
            int errors = 0;
            var methodInfo = type.GetMethod(methodName, parametersTypes);
            if (methodInfo is null)
            {
                errors++;
                PrintError(notExistsMessage);
            }
            else
            {
                if (methodInfo.ReturnType != returnType)
                {
                    errors++;
                    PrintError(wrongReturnTypeMessage);
                }

                if (isAbsract && !methodInfo.IsAbstract)
                {
                    errors++;
                    PrintError($"Metoda {methodName} powinna byc abstrakcyjna");
                }

                if (!isAbsract && methodInfo.IsAbstract)
                {
                    errors++;
                    PrintError($"Metoda {methodName} powinna byc zaimplementowana");
                }
            }

            return errors;
        }
        #endregion
    }
}