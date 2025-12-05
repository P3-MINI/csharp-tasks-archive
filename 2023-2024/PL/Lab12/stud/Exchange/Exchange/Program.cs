// #define STAGE_1
// #define STAGE_2
// #define STAGE_3

namespace Exchange;

class Program
{
    static void Main(string[] args)
    {
#if STAGE_1
        Console.WriteLine("Etap 1 - Parallel 1 pkt");
        
        List<string> lines = new List<string>();
        PopulateList(lines);
        
        // Etap 1 - wybierz odpowiednią kolekcję na dane
        // var messages = ...
        
        // Możesz dodawać zmienne pomocnicze    

        // Etap 1 - wykorzystaj klasę Parallel    
        // Parallel...
            
        foreach (var message in messages)
        {
            Console.WriteLine(message);
        }
        Console.WriteLine($"Total workers count: {messages.Count}");
        Console.WriteLine($"First read line: {messages.First()}");
        Console.WriteLine($"Last read line: {messages.Last()}");

        // Etap 1
        var primesCount = 0;
        
        // Możesz dodawać zmienne pomocnicze
        
        // Parallel...
        Console.WriteLine($"Total primes count: {primesCount}");
#endif
        var codeToName = new Dictionary<int, string>()
        {
            {0, "ALR"},
            {1, "ALE"},
            {2, "ACP"},
            {3, "CDR"},
            {4, "CPS"},
            {5, "DNP"},
            {6, "JSW"},
            {7, "KTY"},
            {8, "KGH"},
            {9, "KRU"},
            {10, "LPP"},
            {11, "MBK"},
            {12, "OPL"},
            {13, "PEO"},
            {14, "PCO"},
            {15, "PGE"},
            {16, "PKN"},
            {17, "PKO"},
            {18, "PZU"},
            {19, "SPL"},
        };
        var startingFreeVolume = new int[]
        {
            (int) 2e6, (int) 28e6, (int) 590e4, (int) 4.5e6, (int) 1.3e6, (int) 4.7e6, (int) 3.7e6, (int) 740e4,
            (int) 11e6, (int) 1.5e6, (int) 12e6, (int) 1.3e6, (int) 1.3e6, (int) 16.8e6, (int) 10e6, (int) 2e6,
            (int) 11.7e6, (int) 9e6, (int) 17e6, (int) 3.5e6
        };
        var startingPrice = new double[]
        {
            75.28, 33.43, 75.85, 114.25, 12.79, 459.9, 45.05, 739, 123.15, 477, 16530, 541.6, 8.25, 146.55, 26.12,
            8.94, 63.24, 49.34, 48.56, 504
        };

        var exchange = new Exchange(codeToName, startingFreeVolume, startingPrice);
        var clientA = new Client(codeToName,
            new[] { 10, 2000, 12, 231, 43, 0, 0, 0, 0, 10, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new List<Order>()
            {
                new Order()
                {
                    Side = Side.Buy,
                    Volume = 1000,
                    OrderId = 0,
                    SecurityCode = 3
                },
                new Order()
                {
                    Side = Side.Sell,
                    Volume = 20,
                    OrderId = 1,
                    SecurityCode = 1
                },new Order()
                {
                    Side = Side.Buy,
                    Volume = 100000,
                    OrderId = 2,
                    SecurityCode = 10
                },new Order()
                {
                    Side = Side.Sell,
                    Volume = 2000,
                    OrderId = 3,
                    SecurityCode = 11
                },new Order()
                {
                    Side = Side.Sell,
                    Volume = 5000,
                    OrderId = 4,
                    SecurityCode = 13
                },
            });
        var clientB = new Client(codeToName,
            new[] { 0, 0, 0, 0, 0, 0, 2000000, 0, 0, 1233123, 0, 0, 0, 0, 0, 0, 0, 0, 0, 122222 }, new List<Order>()
            {
                new Order()
                {
                    Side = Side.Sell,
                    Volume = 1000,
                    OrderId = 5,
                    SecurityCode = 3
                },
                new Order()
                {
                    Side = Side.Buy,
                    Volume = 10000,
                    OrderId = 6,
                    SecurityCode = 2
                },new Order()
                {
                    Side = Side.Buy,
                    Volume = 8000,
                    OrderId = 7,
                    SecurityCode = 5
                },new Order()
                {
                    Side = Side.Buy,
                    Volume = 200,
                    OrderId = 8,
                    SecurityCode = 3
                },new Order()
                {
                    Side = Side.Buy,
                    Volume = 1233000,
                    OrderId = 9,
                    SecurityCode = 6
                },
            });

        exchange.AddClient(clientA);
        exchange.AddClient(clientB);
#if STAGE_2
#if !STAGE_3
        exchange.Start(1);
#endif
#endif
#if STAGE_3
        //Uzupełnij
        // BackgroundWorker workerClientA = ...
        // workerClientA.RunWorkerAsync();
        //Uzupełnij
        // BackgroundWorker workerClientB = ...
        workerClientB.RunWorkerAsync();
        Thread.CurrentThread.Name = "Exchange";
        exchange.Start(10);
#endif
    }

    private static void PopulateList(List<string> lines)
    {
        lines.AddRange(File.ReadAllLines("./text.txt"));
    }
}
