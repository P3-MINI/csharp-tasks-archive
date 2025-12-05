using System;
using System.Collections;

namespace Lab8
{

    class Program
    {
        private static void PrintIEnumerable(IEnumerable Sequence, int limit = int.MaxValue)
        {
            int index = 0;
            foreach (var i in Sequence)
            {
                Console.Write("{0}\t", i);
                if (++index == limit) break;
            }
            Console.WriteLine("\n");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("=== Etap 1 ===\n");

            //IEnumerable naturals = new Naturals();
            //Console.WriteLine("Natural numbers");
            //PrintIEnumerable(naturals, 10);

            //IEnumerable random = new RandomNumbers(665,1000);
            //Console.WriteLine("Random numbers");
            //PrintIEnumerable(random, 10);

            //IEnumerable tribonacci = new Tribonacci();
            //Console.WriteLine("Tribonacci numbers");
            //PrintIEnumerable(tribonacci, 10);

            //IEnumerable catalan = new Catalan();
            //Console.WriteLine("Catalan numbers");
            //PrintIEnumerable(catalan, 10);

            //int[] arr1 = {56, 6, -9, 1};
            //IEnumerable polynomial = new Polynomial(arr1);
            //Console.WriteLine("Polynomial values");
            //PrintIEnumerable(polynomial, 10);

            Console.WriteLine("=== Etap 2 ===\n");

            //IModifier first5 = new FirstN(5);
            //Console.WriteLine(first5.Name);
            //PrintIEnumerable(first5.Modify(random));

            //IModifier linear = new LinearTransform(10,5);
            //Console.WriteLine(linear.Name);
            //PrintIEnumerable(linear.Modify(naturals),10);

            //int[] arr2 = { 3,1,2,2,2,5,5,4,2,1,3,2,4,4,4 };
            //IModifier unique = new Unique();
            //Console.WriteLine(unique.Name);
            //PrintIEnumerable(unique.Modify(arr2));

            //IModifier prime = new Prime();
            //Console.WriteLine(prime.Name);
            //PrintIEnumerable(prime.Modify(naturals), 10);

            Console.WriteLine("=== Etap 3 ===\n");

            //IModifier localMax = new LocalMax();
            //Console.WriteLine(localMax.Name);
            //PrintIEnumerable(localMax.Modify(new int[0]));
            //PrintIEnumerable(localMax.Modify(new int[]{3}));
            //PrintIEnumerable(localMax.Modify(new int[]{4,1}));
            //PrintIEnumerable(localMax.Modify(new int[]{2,5}));
            //PrintIEnumerable(localMax.Modify(arr2));

            Console.WriteLine("=== Etap 4 ===\n");

            //IMerger add = new Add();
            //Console.WriteLine(add.Name);
            //PrintIEnumerable(add.Merge(naturals, prime.Modify(naturals)),10);

            Console.WriteLine("=== Etap 5 ===\n");

            //IModifier[] modifiers = {first5, linear, prime};
            //IModifier composed = new ComposedModifier(modifiers);
            //Console.WriteLine(composed.Name);
            //PrintIEnumerable(composed.Modify(naturals),10);

            //IModifier[] modifiers2 = {first5, prime, linear};
            //IModifier composed2 = new ComposedModifier(modifiers2);
            //Console.WriteLine(composed2.Name);
            //PrintIEnumerable(composed2.Modify(naturals),10);

            //MultiplicationTable table = new MultiplicationTable();
            //Console.WriteLine("Multplication Table\n");
            //foreach ( IEnumerable seq in table )
            //    PrintIEnumerable(seq);

        }

    }

}
