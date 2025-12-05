using System;

namespace Lab7b
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("//--------------------Etap 1(2.0p)--------------------//");
            //UniqueSet set0 = new UniqueSet(new int[] { 5, 2, 6, 3, 1 });
            //UniqueSet set1 = new UniqueSet(new int[] { 1, 5, 6, 2, 3, 5, 2, 2, 2 });
            //UniqueSet set2 = new UniqueSet(new int[] { 2, 1, 5, 2, 2, 2, 2, 3 });
            //UniqueSet set3 = set1.Clone();
            //UniqueSet set4 = new UniqueSet();
            //(UniqueSet set5, UniqueSet set6) = set3;
            //(UniqueSet set7, UniqueSet set8) = set4;

            //set1[2] = 20;

            //Console.WriteLine($"set0 = {set0} Size = {set0.Size}");
            //Console.WriteLine($"set1 = {set1} Size = {set1.Size}");
            //Console.WriteLine($"set2 = {set2} Size = {set2.Size}");
            //Console.WriteLine($"set3 = {set3} Size = {set3.Size}");
            //Console.WriteLine($"set4 = {set4} Size = {set4.Size}");
            //Console.WriteLine($"Dekonstrukcja (set5, set6) = set3 : set5 = {set5} Size = {set5.Size}; set6 = {set6} Size = {set6.Size}");
            //Console.WriteLine($"Dekonstrukcja (set7, set8) = set4 : set7 = {set7} Size = {set7.Size}; set8 = {set8} Size = {set8.Size}");

            //try
            //{
            //    int k = set1[7];

            //    Console.WriteLine("Powinien być wyjątek");
            //}
            //catch (IndexOutOfRangeException e)
            //{
            //    Console.WriteLine($"Złapano wyjątek: {e.GetType()} OK");
            //}

            //try
            //{
            //    set1[7] = 10;
            //    Console.WriteLine("Powinien być wyjątek");
            //}
            //catch (IndexOutOfRangeException e)
            //{
            //    Console.WriteLine($"Złapano wyjątek: {e.GetType()} OK");
            //}

            Console.WriteLine("//--------------------Etap 2(1.0p)--------------------//");
            //UniqueSet set9 = set1.Clone();
            //UniqueSet set10 = 1;
            //Console.WriteLine($"set9 = {set9} Size = {set9.Size}");
            //Console.WriteLine($"set10 = {set10} Size = {set10.Size}");
            //Console.WriteLine($"set3 == set0 : {set3 == set0}");
            //Console.WriteLine($"set1 == set9 : {set1 == set9}");
            //Console.WriteLine($"set1 != set10 : {set1 != set10}");
            //Console.WriteLine($"set1 != set9 : {set1 != set9}");

            //int[] set1_tab = set1;
            //UniqueSet set11 = (UniqueSet)set1_tab;

            //Console.WriteLine($"Konwersja: UniqueSet set1-> int[] -> UnqueSet set11 = {set11} Size = {set11.Size} set1 == set15 : {set1 == set11}");

            Console.WriteLine("//--------------------Etap 3(2.0p)--------------------//");
            //UniqueSet set12 = new UniqueSet();
            //UniqueSet set13 = new UniqueSet();

            //for (int index = 0; index < 4; index++)
            //    set12 += 2;

            //Console.WriteLine($"set12 = {set12} Size = {set12.Size}");

            //for (int index = 0; index < 10; index++)
            //    set12 += index;

            //Console.WriteLine($"set12 = {set12} Size = {set12.Size}");
            //Console.WriteLine($"set13 = {set13} Size = {set13.Size}");

            //set13 += set12;
            //Console.WriteLine($"set13 = set12 + set13 = {set13} Size = {set13.Size}");

            //UniqueSet set14 = set13 + set12;
            //Console.WriteLine($"set14 = set12 + set13 = {set14} Size = {set14.Size}");

            //--set14;
            //Console.WriteLine($"--set14 = {set14} Size = {set14.Size}");

            //UniqueSet set15 = set14 ^ set12;
            //Console.WriteLine($"set15 = set14 ^ set13 = {set15} Size = {set15.Size}");

            //UniqueSet set16 = set5 ^ set6;
            //Console.WriteLine($"set16 = set5 ^ set6 = {set16} Size = {set16.Size}");
        }
    }
}
