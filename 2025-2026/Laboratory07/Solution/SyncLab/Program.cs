namespace SyncLab;

class Program
{
    static void Usage()
    {
        Console.WriteLine($"{AppDomain.CurrentDomain.FriendlyName} M N K");
        Console.WriteLine("\t20 <= M <= 100 - number of customers");
        Console.WriteLine("\t3 <= N <= 12, N % 3 = 0 - number of restock workers");
        Console.WriteLine("\t2 <= K <= 5 - number of cashiers");
        Environment.Exit(1);
    }
    
    static async Task Main(string[] args)
    {
        if (args.Length != 3)
        {
            Usage();
        }
        
        if (!int.TryParse(args[0], out var customers) || customers < 20 || customers > 100)
        {
            Usage();
        }
        
        if (!int.TryParse(args[1], out var workers) || workers < 3 || workers > 12 || workers % 3 != 0)
        {
            Usage();
        }
        
        if (!int.TryParse(args[2], out var cashiers) || cashiers < 2 || cashiers > 5)
        {
            Usage();
        }

        var cancellation = new CancellationTokenSource();

        Console.CancelKeyPress += (sender, e) =>
        {
            e.Cancel = true;
            Console.WriteLine("[SIGINT] Shutting down...");
            cancellation.Cancel();
        };
        
        using var shop = new Shop(customers, workers, cashiers, cancellation.Token);
        
        try
        {
            await shop.StartSimulationAsync();
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Simulation cancelled.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex}");
        }
    }
}