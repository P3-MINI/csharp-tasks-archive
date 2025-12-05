using System;

namespace Lab7a
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("//--------------------Etap 1(2.0p)--------------------//");
            //UniqueVector vec1 = new UniqueVector(new int[] { 1, 1, 2, 1, 6, 4, 8, 2, 2 });
            //UniqueVector vec2 = new UniqueVector(new int[] { 2, 1, 5, 2, 2, 2, 2, 3 });
            //UniqueVector vec3 = vec1.Clone();
            //UniqueVector vec4 = new UniqueVector();
            //(UniqueVector vec5, UniqueVector vec6) = vec3;
            //(UniqueVector vec7, UniqueVector vec8) = vec4;

            //vec1[2] = 20;

            //Console.WriteLine($"vec1 = {vec1} Count = {vec1.Count}");
            //Console.WriteLine($"vec2 = {vec2} Count = {vec2.Count}");
            //Console.WriteLine($"vec3 = {vec3} Count = {vec3.Count}");
            //Console.WriteLine($"vec4 = {vec4} Count = {vec4.Count}");
            //Console.WriteLine($"Dekonstrukcja (vec5, vec6) = vec3 : vec5 = {vec5} Count = {vec5.Count}; vec6 = {vec6} Count = {vec6.Count}");
            //Console.WriteLine($"Dekonstrukcja (vec7, vec8) = vec4 : vec7 = {vec7} Count = {vec7.Count}; vec8 = {vec8} Count = {vec8.Count}");

            //try
            //{
            //    int k = vec1[7];

            //    Console.WriteLine("Powinien być wyjątek");
            //}
            //catch(IndexOutOfRangeException e)
            //{
            //    Console.WriteLine($"Złapano wyjątek: {e.GetType()} OK");
            //}

            // try
            //{
            //    vec1[7] = 10;
            //    Console.WriteLine("Powinien być wyjątek");
            //}
            //catch(IndexOutOfRangeException e)
            //{
            //    Console.WriteLine($"Złapano wyjątek: {e.GetType()} OK");
            //}

            Console.WriteLine("//--------------------Etap 2(1.5p)--------------------//");
            //UniqueVector vec9 = vec1.Clone();
            //UniqueVector vec10 = 1;
            //Console.WriteLine($"vec9 = {vec9} Count = {vec9.Count}");
            //Console.WriteLine($"vec10 = {vec10} Count = {vec10.Count}");
            //Console.WriteLine($"vec1 == vec3 : {vec1 == vec3}");
            //Console.WriteLine($"vec1 == vec9 : {vec1 == vec9}");
            //Console.WriteLine($"vec1 != vec10 : {vec1 != vec10}");
            //Console.WriteLine($"vec1 != vec9 : {vec1 != vec9}");

            //int[] vec1_tab = vec1;
            //UniqueVector vec11 = (UniqueVector)vec1_tab;

            //Console.WriteLine($"Konwersja: UniqueVector vec1-> int[] -> UnqueVector vec11 = {vec11} Count = {vec11.Count} vec1 == vec15 : {vec1 == vec11}");

            Console.WriteLine("//--------------------Etap 3(1.5p)--------------------//");
            //UniqueVector vec12 = new UniqueVector();
            //UniqueVector vec13 = new UniqueVector();

            //for (int index = 0; index < 4; index++)
            //    vec12 += 2;

            //Console.WriteLine($"vec12 = {vec12} Count = {vec12.Count}");

            //for (int index = 0; index < 10; index++)
            //    vec12 += index;

            //Console.WriteLine($"vec12 = {vec12} Count = {vec12.Count}");
            //Console.WriteLine($"vec13 = {vec13} Count = {vec13.Count}");

            //vec13 += vec12;
            //Console.WriteLine($"vec13 = vec12 + vec13 = {vec13} Count = {vec13.Count}");

            //UniqueVector vec14 = vec13 + vec12;
            //Console.WriteLine($"vec14 = vec12 + vec13 = {vec14} Count = {vec14.Count}");

            //++vec14;
            //Console.WriteLine($"++vec14 = {vec14} Count = {vec14.Count}");

            //UniqueVector vec15 = vec14 * vec13;
            //Console.WriteLine($"vec15 = vec14 * vec13 = {vec15} Count = {vec15.Count}");

            //UniqueVector vec16 = vec5 * vec6;
            //Console.WriteLine($"vec16 = vec5 * vec6 = {vec16} Count = {vec16.Count}");
        }
    }
}
