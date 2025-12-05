#define STEP1
#define STEP2 // Odkomentować
#define STEP3 // Odkomentować

using System;

namespace Lab9A
{
    class Program
    {
        static void Main(string[] args)
        {
#if STEP1
            Console.WriteLine("\n#Etap 1 (3.0p)\n");

            MyDictionary<int, double> dictionary1 = new MyDictionary<int, double>();
            for(int index = 0; index < 6; ++index)
            {
                dictionary1.Add(index, (double)index);
            }

            Console.WriteLine("Test #1 Add: {0}", dictionary1);

            dictionary1.Add(5, 3.0);
            dictionary1.Add(5, 2.0);
            dictionary1.Add(5, 1.5);
            dictionary1.Add(1, -2.3);
            dictionary1.Add(2, 4.5);
            dictionary1.Add(3, 2.12);
            Console.WriteLine("Test #2 Add: {0}", dictionary1);

            Console.WriteLine("Test #3 Contains: {0}", dictionary1.Contains(2));
            Console.WriteLine("Test #4 Contains: {0}", dictionary1.Contains(1));
            Console.WriteLine("Test #5 Contains: {0}", dictionary1.Contains(10));
            Console.WriteLine("Test #6 Contains: {0}", dictionary1);

            double value;
            bool result;

            result = dictionary1.TryGetValue(2, out value);
            Console.WriteLine("Test #7 TryGetValue: {0} {1}", result, value);

            result = dictionary1.TryGetValue(-1, out value);
            Console.WriteLine("Test #8 TryGetValue: {0} {1}", result, value);
            Console.WriteLine("Test #9 TryGetValue: {0}", dictionary1);

            Console.WriteLine("Test #10 Count: {0}", dictionary1.Count);
            Console.WriteLine("Test #11 Remove: {0}", dictionary1.Remove(2));
            Console.WriteLine("Test #12 Remove: {0}", dictionary1);
            Console.WriteLine("Test #13 Count: {0}", dictionary1.Count);
            Console.WriteLine("Test #14 Remove: {0}", dictionary1.Remove(2));
            Console.WriteLine("Test #15 Count: {0}", dictionary1.Count);
#endif
#if STEP2
            Console.WriteLine("\n#Etap 2 (1.0p)\n");
            MyDictionary<char, string> dictionary2 = new MyDictionary<char, string>();

            dictionary2.Add('e', "EEEE");
            dictionary2.Add('a', "YYYY");
            dictionary2.Add('b', "BBBB");
            dictionary2.Add('j', "JJJJ");
            dictionary2.Add('a', "AAAA");

            Console.WriteLine("Test #16 IEnumerable: {0}", dictionary2);
            foreach (var element in dictionary2)
            {
                Console.WriteLine($"{element.Item1}={element.Item2}");
            }
#endif
#if STEP3
            Console.WriteLine("\n#Etap 3 (1.0p)\n");
            Console.WriteLine("Test #17 Keys (dictionary1): {0}", dictionary1);
            foreach (var element in dictionary1.GetKeys())
            {
                Console.Write($"{element} ");
            }
            Console.WriteLine();
            Console.WriteLine("Test #18 MaxValue (dictionary1): {0}", dictionary1.MaxValue());

            Console.WriteLine();
            Console.WriteLine("Test #19 Keys (dictionary2): {0}", dictionary2);
            foreach (var element in dictionary2.GetKeys())
            {
                Console.Write($"{element} ");
            }
            Console.WriteLine();
            Console.WriteLine("Test #20 MaxValue (dictionary2): {0}", dictionary2.MaxValue());
#endif
        }
    }
}
