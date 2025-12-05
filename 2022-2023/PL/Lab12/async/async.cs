
using System;
using System.Threading.Tasks;   // new

class AsyncExample
    {

    public static void Main()
        {
        Task<int> r1, r2, r3;   // change
        DateTime start, stop;

        start = DateTime.Now;
        Console.WriteLine("Begin long computations series");
        r1 = FirstLongComputationAsync();    // change
        r2 = SecondLongComputationAsync();   // change
        r3 = ThirdLongComputationAsync();    // change
        Task.WaitAll(r1,r2,r3);              // new
        Console.WriteLine("End long computations series");
        stop=DateTime.Now;

        Console.WriteLine($"Results:  {r1.Result}, {r2.Result}, {r3.Result}");   // change
        Console.WriteLine($"Total computation time:  {stop-start}");
        Console.WriteLine();
        }

    private static async Task<int> FirstLongComputationAsync()    // new function
        {
        Console.WriteLine("Begin first asynchronous call");
        int r = await Task<int>.Run(()=>FirstLongComputation());
        Console.WriteLine("End first asynchronous call");
        return r;
        }

    private static async Task<int> SecondLongComputationAsync()   // new function
        {
        Console.WriteLine("Begin second asynchronous call");
        int r = await Task<int>.Run(()=>SecondLongComputation());
        Console.WriteLine("End second asynchronous call");
        return r;
        }

    private static async Task<int> ThirdLongComputationAsync()    // new function
        {
        Console.WriteLine("Begin third asynchronous call");
        int r = await Task<int>.Run(()=>ThirdLongComputation());
        Console.WriteLine("End third asynchronous call");
        return r;
        }

    private static int FirstLongComputation()    // unchanged !!!
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

    private static int SecondLongComputation()    // unchanged !!!
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

    private static int ThirdLongComputation()    // unchanged !!!
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