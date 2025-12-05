using System;

namespace cs8
{

class Program
    {

    static void Main()
        {
        int[] tab = new int[] { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };
        Console.WriteLine("---------------");
        Console.WriteLine($"tab[0]   =  {tab[0]}");
        Console.WriteLine($"tab[1]   =  {tab[1]}");
        Console.WriteLine("...");
        Console.WriteLine($"tab[8]   =  {tab[8]}");
        Console.WriteLine($"tab[9]   =  {tab[9]}");
        Console.WriteLine("---------------");
        Console.WriteLine($"tab[^0]  =  IndexOutOfRangeException");
        Console.WriteLine($"tab[^1]  =  {tab[^1]}");
        Console.WriteLine($"tab[^2]  =  {tab[^2]}");
        Console.WriteLine("...");
        Console.WriteLine($"tab[^8]  =  {tab[^8]}");
        Console.WriteLine($"tab[^9]  =  {tab[^9]}");
        Console.WriteLine($"tab[^10] =  {tab[^10]}");
        Console.WriteLine($"tab[^11]  =  IndexOutOfRangeException");
        Console.WriteLine("---------------");
        Console.WriteLine($"In general:  tab[^i]==tab[tab.Length-i]");
        Console.WriteLine("---------------");

        // Multi dimensional array
        int[,] tab2 = new int[,] { {0,1,2}, {10,11,12}, {20,21,22} };
//      Console.WriteLine($"tab2[^1,^1]  =  {tab2[^1,^1]}");   // compilation error

        // Array of arrays
        int[][] tab11 = new int[][] { new int[]{0,1,2}, new int[]{10,11,12}, new int[]{20,21,22} };
        Console.WriteLine($"tab11[^1][^1]  =  {tab11[^1][^1]}");

        Console.WriteLine("---------------");
        int[] tr25 = tab[2..5];
        for ( int i=0 ; i<tr25.Length ; ++i )
            Console.WriteLine($"tr25[{i}]   =  {tr25[i]}");

        Console.WriteLine("---------------");
        int[] trb4 = tab[..4];
        for ( int i=0 ; i<trb4.Length ; ++i )
            Console.WriteLine($"trb4[{i}]   =  {trb4[i]}");

        Console.WriteLine("---------------");
        int[] tr7e = tab[7..];
        for ( int i=0 ; i<tr7e.Length ; ++i )
            Console.WriteLine($"tr7e[{i}]   =  {tr7e[i]}");

        Console.WriteLine("---------------");
        int[] trbe = tab[..];
        for ( int i=0 ; i<trbe.Length ; ++i )
            Console.WriteLine($"trbe[{i}]   =  {trbe[i]}");

        Console.WriteLine("---------------");
        }

    }

}
