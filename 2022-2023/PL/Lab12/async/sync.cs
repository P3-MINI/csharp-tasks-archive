
using System;

class SyncExample
    {

    public static void Main()
        {
        int r1, r2, r3;
        DateTime start, stop;

        start = DateTime.Now;
        Console.WriteLine("Begin long computations series");
        r1 = FirstLongComputation();
        r2 = SecondLongComputation();
        r3 = ThirdLongComputation();
        Console.WriteLine("End long computations series");
        stop = DateTime.Now;

        Console.WriteLine($"Results:  {r1}, {r2}, {r3}");
        Console.WriteLine($"Total computation time:  {stop-start}");
        Console.WriteLine();
        }

    private static int FirstLongComputation()
        {
        Console.WriteLine("First long computation started");
        for ( int i=0 ; i<11 ; ++i )
            {
            delay();
            Console.WriteLine("*");
            }
        Console.WriteLine("First long computation completed");
        return 1;
        }

    private static int SecondLongComputation()
        {
        Console.WriteLine("Second long computation started");
        for ( int i=0 ; i<17 ; ++i )
            {
            delay();
            Console.WriteLine("#");
            }
        Console.WriteLine("Second long computation completed");
        return 2;
        }

    private static int ThirdLongComputation()
        {
        Console.WriteLine("Third long computation started");
        for ( int i=0 ; i<7 ; ++i )
            {
            delay();
            Console.WriteLine("@");
            }
        Console.WriteLine("Third long computation completed");
        return 3;
        }

    private static void delay() => System.Threading.Thread.Sleep(500);

    }