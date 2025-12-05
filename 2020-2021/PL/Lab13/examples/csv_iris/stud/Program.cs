//#define STAGE_1
//#define STAGE_2
//#define STAGE_3
//#define STAGE_4
//#define STAGE_5

using System;
using System.Collections.Generic;
using System.Linq;

namespace pl_lab11
{
    public class Program
    {

        private static void Main(string[] args)
        {
#if STAGE_1
            dynamic result;

            Console.WriteLine("### ETAP 1 ###");

            result = CsvFile.Read("non-existing-file.csv");
            Assert("Result should be null",
                () => result is null);

            result = CsvFile.Read("iris.csv");
            Assert("Result should not be null", () =>
                !(result is null));
            Assert("Result should be of type CsvFileInfo", () =>
                result is CsvFileInfo);
            Assert("FileName should be iris.csv", () =>
                result.FileName == "iris.csv");
            Assert("FullDirectoryPath should be not null",
                () => result.FullDirectoryPath != null);
            Assert("Headers should be not null", () =>
                result.Headers != null);
            Assert("Headers should be array of strings", () =>
                result.Headers is string[]);
            Assert("Headers should contains correct values", () =>
                (result.Headers as string[]).OrderBy(x => x).ToList().Zip(new[] { "petal.length", "petal.width", "sepal.length", "sepal.width", "variety" }, (a, b) => a == b).All(x => x));
            Assert("RowsNumber should be 150", () =>
                result.RowsNumber == 150);
            Assert("Data should not be null", () =>
                !(result.Data is null));
            Assert("Data should be a read only dictionary", () =>
                result.Data is Dictionary<string, string[]>);
            Assert("Data should not contains keys that starts/ends with \"", () =>
                !(result.Data as Dictionary<string, string[]>).Keys.Any(x => x.StartsWith("\"") || x.EndsWith("\"")));
            Assert("Data should contains all headers as keys", () =>
                new List<string>(new[] { "petal.length", "petal.width", "sepal.length", "sepal.width", "variety" }).Select(x => (result.Data as Dictionary<string, string[]>).ContainsKey(x)).All(x => x));
            Assert("Data should contains all values of length 150", () =>
                new List<string>(new[] { "petal.length", "petal.width", "sepal.length", "sepal.width", "variety" }).Select(x => (result.Data as Dictionary<string, string[]>)[x].Length).All(x => x == 150));
#endif

#if STAGE_2
            Console.WriteLine("### ETAP 2 ###");
#endif

#if STAGE_3
            Console.WriteLine("### ETAP 3 ###");

            result = DataFrame.FromCsv("iris.csv", data => new Iris
            {
                SepalLength = Convert.ToDouble(data["sepal.length"]),
                SepalWidth = Convert.ToDouble(data["sepal.width"]),
                PetalLength = Convert.ToDouble(data["petal.length"]),
                PetalWidth = Convert.ToDouble(data["petal.width"]),
                Species = (IrisSpecies)Enum.Parse(typeof(IrisSpecies), data["variety"])
            });

            AssertIrises(result);
#endif

#if STAGE_4

            Console.WriteLine("### ETAP 4 ###");

            result = DataFrame.FromCsv("iris.csv", data => new Iris
            {
                SepalLength = Convert.ToDouble(data["sepal.length"]),
                SepalWidth = Convert.ToDouble(data["sepal.width"]),
                PetalLength = Convert.ToDouble(data["petal.length"]),
                PetalWidth = Convert.ToDouble(data["petal.width"]),
                Species = (IrisSpecies)Enum.Parse(typeof(IrisSpecies), data["variety"])
            });

            Assert("Binary Serialization", () =>
            {
                result.ToBin("__student_iris.bin");
            });

            Assert("Binary Deserialization", () =>
            {
                result = DataFrame.FromBin<Iris>("__student_iris.bin");
                AssertIrises(result);
            });

#endif

#if STAGE_5

            Console.WriteLine("### ETAP 5 ###");

            result = DataFrame.FromCsv("iris.csv", data => new Iris
            {
                SepalLength = Convert.ToDouble(data["sepal.length"]),
                SepalWidth = Convert.ToDouble(data["sepal.width"]),
                PetalLength = Convert.ToDouble(data["petal.length"]),
                PetalWidth = Convert.ToDouble(data["petal.width"]),
                Species = (IrisSpecies)Enum.Parse(typeof(IrisSpecies), data["variety"])
            });

            Assert("Xml Serialization", () => { result.ToXml("__student_iris.xml"); return true; });

            Assert("Xml Deserialization 1", () =>
            {
                result = DataFrame.FromXml<Iris>("__student_iris.xml");
                AssertIrises(result);
            });

            Assert("Xml Deserialization 2", () =>
            {
                result = DataFrame.FromXml<Iris>("__iris.xml");
                AssertIrises(result);
            });
#endif
        }

        private static bool Assert(string message, Func<bool> condition, bool print = true)
        {
            try
            {
                bool result = condition();
                if (result)
                {
                    if (print)
                    {
                        Print(message, ConsoleColor.Green);
                    }
                }
                else
                {
                    Print(message, ConsoleColor.Red);
                }
                return result;
            }
            catch (Exception ex)
            {
                Print(ex?.Message, ConsoleColor.DarkMagenta);
                return false;
            }
        }

        private static bool Assert(string message, Action action)
        {
            try
            {
                action();
                Print(message, ConsoleColor.Green);
                return true;
            }
            catch (Exception ex)
            {
                Print(ex?.Message, ConsoleColor.DarkMagenta);
                return false;
            }
        }


#if STAGE_3
        private static void AssertIrises(dynamic df)
        {
            Assert("DataFrame should not be null", () => !(df is null));
            Assert("DataFrame should DataFrame of Iris", () => df is DataFrame<Iris>);
            int fails = 0;
            for (int i = 0; i < IrisDataSet.Count; i++)
            {
                if (!Assert($"Iris #{i} should not be null", () => !(df[i] is null), false)) { fails++; }
                if (!Assert($"Iris #{i} SepalLength should be correct", () => df[i].SepalLength == IrisDataSet[i].SepalLength, false)) { fails++; }
                if (!Assert($"Iris #{i} SepalWidth should be correct", () => df[i].SepalWidth == IrisDataSet[i].SepalWidth, false)) { fails++; }
                if (!Assert($"Iris #{i} PetalLength should be correct", () => df[i].PetalLength == IrisDataSet[i].PetalLength, false)) { fails++; }
                if (!Assert($"Iris #{i} PetalWidth should be correct", () => df[i].PetalWidth == IrisDataSet[i].PetalWidth, false)) { fails++; }
                if (!Assert($"Iris #{i} Species should be correct", () => df[i].Species == IrisDataSet[i].Species, false)) { fails++; }
            }
            if (fails == 0)
            {
                Print("Iris checking.", ConsoleColor.Green);
            }
            else
            {
                Print($"Iris checking. {fails} fails", ConsoleColor.Red);
            }
        }
#endif

        private static void Print(string message, ConsoleColor color)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            var prefix = color == ConsoleColor.Green
                ? "OK  "
                : "FAIL";
            Console.WriteLine($"{prefix} {message}");
            Console.ForegroundColor = oldColor;
        }

#if STAGE_2
        private static readonly List<Iris> IrisDataSet;

        static Program()
        {
            IrisDataSet = new List<Iris>
            {
                new Iris { SepalLength = 5.1, SepalWidth = 3.5, PetalLength = 1.4, PetalWidth = .2, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 4.9, SepalWidth = 3, PetalLength = 1.4, PetalWidth = .2, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 4.7, SepalWidth = 3.2, PetalLength = 1.3, PetalWidth = .2, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 4.6, SepalWidth = 3.1, PetalLength = 1.5, PetalWidth = .2, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 5, SepalWidth = 3.6, PetalLength = 1.4, PetalWidth = .2, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 5.4, SepalWidth = 3.9, PetalLength = 1.7, PetalWidth = .4, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 4.6, SepalWidth = 3.4, PetalLength = 1.4, PetalWidth = .3, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 5, SepalWidth = 3.4, PetalLength = 1.5, PetalWidth = .2, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 4.4, SepalWidth = 2.9, PetalLength = 1.4, PetalWidth = .2, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 4.9, SepalWidth = 3.1, PetalLength = 1.5, PetalWidth = .1, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 5.4, SepalWidth = 3.7, PetalLength = 1.5, PetalWidth = .2, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 4.8, SepalWidth = 3.4, PetalLength = 1.6, PetalWidth = .2, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 4.8, SepalWidth = 3, PetalLength = 1.4, PetalWidth = .1, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 4.3, SepalWidth = 3, PetalLength = 1.1, PetalWidth = .1, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 5.8, SepalWidth = 4, PetalLength = 1.2, PetalWidth = .2, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 5.7, SepalWidth = 4.4, PetalLength = 1.5, PetalWidth = .4, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 5.4, SepalWidth = 3.9, PetalLength = 1.3, PetalWidth = .4, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 5.1, SepalWidth = 3.5, PetalLength = 1.4, PetalWidth = .3, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 5.7, SepalWidth = 3.8, PetalLength = 1.7, PetalWidth = .3, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 5.1, SepalWidth = 3.8, PetalLength = 1.5, PetalWidth = .3, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 5.4, SepalWidth = 3.4, PetalLength = 1.7, PetalWidth = .2, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 5.1, SepalWidth = 3.7, PetalLength = 1.5, PetalWidth = .4, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 4.6, SepalWidth = 3.6, PetalLength = 1, PetalWidth = .2, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 5.1, SepalWidth = 3.3, PetalLength = 1.7, PetalWidth = .5, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 4.8, SepalWidth = 3.4, PetalLength = 1.9, PetalWidth = .2, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 5, SepalWidth = 3, PetalLength = 1.6, PetalWidth = .2, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 5, SepalWidth = 3.4, PetalLength = 1.6, PetalWidth = .4, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 5.2, SepalWidth = 3.5, PetalLength = 1.5, PetalWidth = .2, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 5.2, SepalWidth = 3.4, PetalLength = 1.4, PetalWidth = .2, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 4.7, SepalWidth = 3.2, PetalLength = 1.6, PetalWidth = .2, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 4.8, SepalWidth = 3.1, PetalLength = 1.6, PetalWidth = .2, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 5.4, SepalWidth = 3.4, PetalLength = 1.5, PetalWidth = .4, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 5.2, SepalWidth = 4.1, PetalLength = 1.5, PetalWidth = .1, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 5.5, SepalWidth = 4.2, PetalLength = 1.4, PetalWidth = .2, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 4.9, SepalWidth = 3.1, PetalLength = 1.5, PetalWidth = .2, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 5, SepalWidth = 3.2, PetalLength = 1.2, PetalWidth = .2, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 5.5, SepalWidth = 3.5, PetalLength = 1.3, PetalWidth = .2, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 4.9, SepalWidth = 3.6, PetalLength = 1.4, PetalWidth = .1, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 4.4, SepalWidth = 3, PetalLength = 1.3, PetalWidth = .2, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 5.1, SepalWidth = 3.4, PetalLength = 1.5, PetalWidth = .2, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 5, SepalWidth = 3.5, PetalLength = 1.3, PetalWidth = .3, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 4.5, SepalWidth = 2.3, PetalLength = 1.3, PetalWidth = .3, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 4.4, SepalWidth = 3.2, PetalLength = 1.3, PetalWidth = .2, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 5, SepalWidth = 3.5, PetalLength = 1.6, PetalWidth = .6, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 5.1, SepalWidth = 3.8, PetalLength = 1.9, PetalWidth = .4, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 4.8, SepalWidth = 3, PetalLength = 1.4, PetalWidth = .3, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 5.1, SepalWidth = 3.8, PetalLength = 1.6, PetalWidth = .2, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 4.6, SepalWidth = 3.2, PetalLength = 1.4, PetalWidth = .2, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 5.3, SepalWidth = 3.7, PetalLength = 1.5, PetalWidth = .2, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 5, SepalWidth = 3.3, PetalLength = 1.4, PetalWidth = .2, Species = IrisSpecies.Setosa },
                new Iris { SepalLength = 7, SepalWidth = 3.2, PetalLength = 4.7, PetalWidth = 1.4, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 6.4, SepalWidth = 3.2, PetalLength = 4.5, PetalWidth = 1.5, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 6.9, SepalWidth = 3.1, PetalLength = 4.9, PetalWidth = 1.5, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 5.5, SepalWidth = 2.3, PetalLength = 4, PetalWidth = 1.3, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 6.5, SepalWidth = 2.8, PetalLength = 4.6, PetalWidth = 1.5, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 5.7, SepalWidth = 2.8, PetalLength = 4.5, PetalWidth = 1.3, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 6.3, SepalWidth = 3.3, PetalLength = 4.7, PetalWidth = 1.6, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 4.9, SepalWidth = 2.4, PetalLength = 3.3, PetalWidth = 1, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 6.6, SepalWidth = 2.9, PetalLength = 4.6, PetalWidth = 1.3, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 5.2, SepalWidth = 2.7, PetalLength = 3.9, PetalWidth = 1.4, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 5, SepalWidth = 2, PetalLength = 3.5, PetalWidth = 1, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 5.9, SepalWidth = 3, PetalLength = 4.2, PetalWidth = 1.5, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 6, SepalWidth = 2.2, PetalLength = 4, PetalWidth = 1, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 6.1, SepalWidth = 2.9, PetalLength = 4.7, PetalWidth = 1.4, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 5.6, SepalWidth = 2.9, PetalLength = 3.6, PetalWidth = 1.3, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 6.7, SepalWidth = 3.1, PetalLength = 4.4, PetalWidth = 1.4, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 5.6, SepalWidth = 3, PetalLength = 4.5, PetalWidth = 1.5, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 5.8, SepalWidth = 2.7, PetalLength = 4.1, PetalWidth = 1, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 6.2, SepalWidth = 2.2, PetalLength = 4.5, PetalWidth = 1.5, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 5.6, SepalWidth = 2.5, PetalLength = 3.9, PetalWidth = 1.1, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 5.9, SepalWidth = 3.2, PetalLength = 4.8, PetalWidth = 1.8, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 6.1, SepalWidth = 2.8, PetalLength = 4, PetalWidth = 1.3, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 6.3, SepalWidth = 2.5, PetalLength = 4.9, PetalWidth = 1.5, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 6.1, SepalWidth = 2.8, PetalLength = 4.7, PetalWidth = 1.2, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 6.4, SepalWidth = 2.9, PetalLength = 4.3, PetalWidth = 1.3, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 6.6, SepalWidth = 3, PetalLength = 4.4, PetalWidth = 1.4, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 6.8, SepalWidth = 2.8, PetalLength = 4.8, PetalWidth = 1.4, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 6.7, SepalWidth = 3, PetalLength = 5, PetalWidth = 1.7, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 6, SepalWidth = 2.9, PetalLength = 4.5, PetalWidth = 1.5, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 5.7, SepalWidth = 2.6, PetalLength = 3.5, PetalWidth = 1, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 5.5, SepalWidth = 2.4, PetalLength = 3.8, PetalWidth = 1.1, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 5.5, SepalWidth = 2.4, PetalLength = 3.7, PetalWidth = 1, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 5.8, SepalWidth = 2.7, PetalLength = 3.9, PetalWidth = 1.2, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 6, SepalWidth = 2.7, PetalLength = 5.1, PetalWidth = 1.6, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 5.4, SepalWidth = 3, PetalLength = 4.5, PetalWidth = 1.5, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 6, SepalWidth = 3.4, PetalLength = 4.5, PetalWidth = 1.6, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 6.7, SepalWidth = 3.1, PetalLength = 4.7, PetalWidth = 1.5, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 6.3, SepalWidth = 2.3, PetalLength = 4.4, PetalWidth = 1.3, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 5.6, SepalWidth = 3, PetalLength = 4.1, PetalWidth = 1.3, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 5.5, SepalWidth = 2.5, PetalLength = 4, PetalWidth = 1.3, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 5.5, SepalWidth = 2.6, PetalLength = 4.4, PetalWidth = 1.2, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 6.1, SepalWidth = 3, PetalLength = 4.6, PetalWidth = 1.4, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 5.8, SepalWidth = 2.6, PetalLength = 4, PetalWidth = 1.2, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 5, SepalWidth = 2.3, PetalLength = 3.3, PetalWidth = 1, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 5.6, SepalWidth = 2.7, PetalLength = 4.2, PetalWidth = 1.3, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 5.7, SepalWidth = 3, PetalLength = 4.2, PetalWidth = 1.2, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 5.7, SepalWidth = 2.9, PetalLength = 4.2, PetalWidth = 1.3, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 6.2, SepalWidth = 2.9, PetalLength = 4.3, PetalWidth = 1.3, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 5.1, SepalWidth = 2.5, PetalLength = 3, PetalWidth = 1.1, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 5.7, SepalWidth = 2.8, PetalLength = 4.1, PetalWidth = 1.3, Species = IrisSpecies.Versicolor },
                new Iris { SepalLength = 6.3, SepalWidth = 3.3, PetalLength = 6, PetalWidth = 2.5, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 5.8, SepalWidth = 2.7, PetalLength = 5.1, PetalWidth = 1.9, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 7.1, SepalWidth = 3, PetalLength = 5.9, PetalWidth = 2.1, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 6.3, SepalWidth = 2.9, PetalLength = 5.6, PetalWidth = 1.8, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 6.5, SepalWidth = 3, PetalLength = 5.8, PetalWidth = 2.2, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 7.6, SepalWidth = 3, PetalLength = 6.6, PetalWidth = 2.1, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 4.9, SepalWidth = 2.5, PetalLength = 4.5, PetalWidth = 1.7, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 7.3, SepalWidth = 2.9, PetalLength = 6.3, PetalWidth = 1.8, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 6.7, SepalWidth = 2.5, PetalLength = 5.8, PetalWidth = 1.8, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 7.2, SepalWidth = 3.6, PetalLength = 6.1, PetalWidth = 2.5, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 6.5, SepalWidth = 3.2, PetalLength = 5.1, PetalWidth = 2, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 6.4, SepalWidth = 2.7, PetalLength = 5.3, PetalWidth = 1.9, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 6.8, SepalWidth = 3, PetalLength = 5.5, PetalWidth = 2.1, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 5.7, SepalWidth = 2.5, PetalLength = 5, PetalWidth = 2, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 5.8, SepalWidth = 2.8, PetalLength = 5.1, PetalWidth = 2.4, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 6.4, SepalWidth = 3.2, PetalLength = 5.3, PetalWidth = 2.3, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 6.5, SepalWidth = 3, PetalLength = 5.5, PetalWidth = 1.8, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 7.7, SepalWidth = 3.8, PetalLength = 6.7, PetalWidth = 2.2, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 7.7, SepalWidth = 2.6, PetalLength = 6.9, PetalWidth = 2.3, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 6, SepalWidth = 2.2, PetalLength = 5, PetalWidth = 1.5, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 6.9, SepalWidth = 3.2, PetalLength = 5.7, PetalWidth = 2.3, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 5.6, SepalWidth = 2.8, PetalLength = 4.9, PetalWidth = 2, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 7.7, SepalWidth = 2.8, PetalLength = 6.7, PetalWidth = 2, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 6.3, SepalWidth = 2.7, PetalLength = 4.9, PetalWidth = 1.8, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 6.7, SepalWidth = 3.3, PetalLength = 5.7, PetalWidth = 2.1, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 7.2, SepalWidth = 3.2, PetalLength = 6, PetalWidth = 1.8, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 6.2, SepalWidth = 2.8, PetalLength = 4.8, PetalWidth = 1.8, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 6.1, SepalWidth = 3, PetalLength = 4.9, PetalWidth = 1.8, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 6.4, SepalWidth = 2.8, PetalLength = 5.6, PetalWidth = 2.1, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 7.2, SepalWidth = 3, PetalLength = 5.8, PetalWidth = 1.6, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 7.4, SepalWidth = 2.8, PetalLength = 6.1, PetalWidth = 1.9, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 7.9, SepalWidth = 3.8, PetalLength = 6.4, PetalWidth = 2, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 6.4, SepalWidth = 2.8, PetalLength = 5.6, PetalWidth = 2.2, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 6.3, SepalWidth = 2.8, PetalLength = 5.1, PetalWidth = 1.5, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 6.1, SepalWidth = 2.6, PetalLength = 5.6, PetalWidth = 1.4, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 7.7, SepalWidth = 3, PetalLength = 6.1, PetalWidth = 2.3, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 6.3, SepalWidth = 3.4, PetalLength = 5.6, PetalWidth = 2.4, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 6.4, SepalWidth = 3.1, PetalLength = 5.5, PetalWidth = 1.8, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 6, SepalWidth = 3, PetalLength = 4.8, PetalWidth = 1.8, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 6.9, SepalWidth = 3.1, PetalLength = 5.4, PetalWidth = 2.1, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 6.7, SepalWidth = 3.1, PetalLength = 5.6, PetalWidth = 2.4, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 6.9, SepalWidth = 3.1, PetalLength = 5.1, PetalWidth = 2.3, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 5.8, SepalWidth = 2.7, PetalLength = 5.1, PetalWidth = 1.9, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 6.8, SepalWidth = 3.2, PetalLength = 5.9, PetalWidth = 2.3, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 6.7, SepalWidth = 3.3, PetalLength = 5.7, PetalWidth = 2.5, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 6.7, SepalWidth = 3, PetalLength = 5.2, PetalWidth = 2.3, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 6.3, SepalWidth = 2.5, PetalLength = 5, PetalWidth = 1.9, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 6.5, SepalWidth = 3, PetalLength = 5.2, PetalWidth = 2, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 6.2, SepalWidth = 3.4, PetalLength = 5.4, PetalWidth = 2.3, Species = IrisSpecies.Virginica },
                new Iris { SepalLength = 5.9, SepalWidth = 3, PetalLength = 5.1, PetalWidth = 1.8, Species = IrisSpecies.Virginica }
};
        }
#endif
    }

}