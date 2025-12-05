#define STAGE_1
#define STAGE_2
#define STAGE_3
#define STAGE_4

using System;
using System.Collections.Generic;
using System.Linq;

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

                Edge[] edges =
                {
                    new Edge(0, 0),
                    new Edge(0, 1),
                    new Edge(1, 3),
                    new Edge(3, 1),
                    new Edge(4, 5),
                    new Edge(-3, 6),
                    new Edge(7, -12316),
                    new Edge(2123019, 0b1011),
                };

                foreach (Edge e in edges)
                {
                    if (e.start > e.end)
                    {
                        errors_1++;
                        PrintError($"start({e.start}) > end({e.end})");
                    }

                    if (e.GetWeight() > 0)
                    {
                        errors_1++;
                        PrintError($"waga ({e.GetWeight()}) != 0");
                    }
                }

                var edge_01 = new Edge(0, 1, 5);
                edge_01 = new Edge(1, 2, 21312.21f);
                edge_01 = new Edge((2, 3, 10));
                edge_01 = new Edge(edge_01);

                edge_01.SetWeight(13);
                if (edge_01.GetWeight() != 13)
                {
                    errors_1++;
                    PrintError($"Blad przy zmianie wagi krawedzi");
                }

                edge_01.SetWeight(-3);
                if (edge_01.GetWeight() != 13)
                {
                    errors_1++;
                    PrintError($"Blad przy zmianie wagi krawedzi");
                }

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
                Type graph = typeof(Graph);

                if (!graph.IsAbstract)
                {
                    errors_2++;
                    PrintError("Klasa \"Graph\" powinna byc klasa abstakcyjna");
                }

                if (graph.GetConstructor(new Type[] { typeof(int) }) is null)
                {
                    errors_2++;
                    PrintError("Klasa \"Graph\" powinna mieć konstruktor przyjmujacy parametr typu \"int\"");
                }

                errors_2 += TestTypeForFunction(
                    graph,
                    "AddEdge",
                    new Type[] { typeof(Edge) },
                    typeof(void),
                    "Klasa \"Graph\" powinna miec metode \"AddEdge\" przyjmujaca parametr typu \"Edge\"",
                    "Metoda \"AddEdge\" powinna zwracac parametr typu \"void\"",
                    true);

                errors_2 += TestTypeForFunction(
                    graph,
                    "RemoveEdge",
                    new Type[] { typeof(Edge) },
                    typeof(void),
                    "Klasa \"Graph\" powinna miec metode \"RemoveEdge\" przyjmujaca parametr typu \"Edge\"",
                    "Metoda \"RemoveEdge\" powinna zwracac parametr typu \"void\"",
                    true);

                errors_2 += TestTypeForFunction(
                    graph,
                    "GetEdgesCount",
                    new Type[] { },
                    typeof(int),
                    "Klasa \"Graph\" powinna miec metode \"GetEdgesCount\" nieprzyjmujaca zadnego parametru",
                    "Metoda \"GetEdgesCount\" powinna zwracac parametr typu \"int\"",
                    true);

                errors_2 += TestTypeForFunction(
                    graph,
                    "GetVerticesCount",
                    new Type[] { },
                    typeof(int),
                    "Klasa \"Graph\" powinna miec metode \"GetVerticesCount\" nieprzyjmujaca zadnego parametru",
                    "Metoda \"GetVerticesCount\" powinna zwracac parametr typu \"int\"");

                errors_2 += TestTypeForFunction(
                    graph,
                    "GetEdge",
                    new Type[] { typeof(int), typeof(int) },
                    typeof(Edge),
                    "Klasa \"Graph\" powinna miec metode \"GetEdge\" przyjmujaca 2 parametry typu \"int\"",
                    "Metode \"GetEdge\" powinna zwracac parametr typu \"Edge\"",
                    true);

                PrintStageSummary(errors_2, 2);
                errors += errors_2;
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

                int verticesCount = 6;
                Graph graph = new MatrixGraph(verticesCount);

                if (graph.GetVerticesCount() != 6)
                {
                    errors_3++;
                    PrintError($"Graf powinien miec {verticesCount} wierzcholkow, a ma ich {graph.GetVerticesCount()}");
                }

                Edge e = new Edge(2, 3, 10f);

                var edges = new List<Edge>();
                edges.Add(e);
                edges.Add(new Edge(0, 1, 2));
                edges.Add(new Edge(4, 3, 10f));

                foreach (var f in edges)
                {
                    graph.AddEdge(f);
                }

                if (graph.GetEdgesCount() != 3)
                {
                    errors_3++;
                    PrintError($"Graf powinien miec {3} krawedzie, a ma ich {graph.GetEdgesCount()}");
                }

                if (graph.GetEdge(e.start, e.end).GetWeight() != e.GetWeight())
                {
                    errors_3++;
                    PrintError($"Krawedz {e.start}:{e.end} ma zla wage");
                }

                graph.AddEdge(new Edge(e.start, e.end, e.GetWeight() + 10));

                if (graph.GetEdge(e.start, e.end).GetWeight() != e.GetWeight() + 10)
                {
                    errors_3++;
                    PrintError($"Po aktualizacji wagi, krawedz {e.start}:{e.end} ma zla wage");
                }

                graph.RemoveEdge(graph.GetEdge(e.start, e.end));
                if (graph.GetEdge(e.start, e.end) != null)
                {
                    errors_3++;
                    PrintError($"Krawedz {e.start}:{e.end} nie zostala usunieta");
                }

                var graph_params = new MatrixGraph(20, new []{ new Edge(0, 1), new Edge(5, 3), new Edge(29, 5)});
                if (graph_params.GetEdgesCount() != 2)
                {
                    errors_3++;
                    PrintError("Zla liczba krawedzi w grafie");
                }


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
                var edges = new List<Edge>()
                {
                    new Edge(2, 3,12),
                    new Edge(2, 9,2),
                    new Edge(2, 4,0),
                    new Edge(2, 5,1231),
                    new Edge(2, 6,15f),
                    new Edge(0, 3,0x21b),
                    new Edge(1, 7,-5)
                };
                var graph = new MatrixGraph(10, edges.ToArray());

                var result = GraphProcessor.FindMinAndMaxDegree(graph);
                if (result.min.degree != 0)
                {
                    errors_4++;
                    PrintError($"Najmniejszyt stopien powinien byc rowny 0, a jest {result.min.degree}");
                }

                if (result.min.v != 8)
                {
                    errors_4++;
                    PrintError(
                        $"Wierzcholkiem o najmniejszym stopniu powinien byc wierzcholek 8, a jest {result.min.v}");
                }

                if (result.max.degree != 5)
                {
                    errors_4++;
                    PrintError($"Najmniejszyt stopien powinien byc rowny 0, a jest {result.max.degree}");
                }

                if (result.max.v != 2)
                {
                    errors_4++;
                    PrintError(
                        $"Wierzcholkiem o najmniejszym stopniu powinien byc wierzcholek 2, a jest {result.max.v}");
                }

                var sorted_edges = GraphProcessor.SortEdges(edges.ToArray());
                for (int i = 0; i < sorted_edges.Length - 1; i++)
                {
                    if (sorted_edges[i].GetWeight() > sorted_edges[i + 1].GetWeight())
                    {
                        errors_4++;
                        PrintError("Krawedzie nie sa posortowane rosnaco");
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