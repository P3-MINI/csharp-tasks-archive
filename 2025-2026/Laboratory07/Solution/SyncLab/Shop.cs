using System.Collections.Concurrent;
using SyncLab.Actors;

namespace SyncLab;

public class Shop : IDisposable
{
    public int CustomersCount { get; }
    public int WorkersCount { get; }
    public int CashiersCount { get; }
    public CancellationToken Token { get; }

    public SemaphoreSlim ShopSemaphore { get; }
    public SemaphoreSlim StockSemaphore { get; }
    public BlockingCollection<Customer> CheckoutQueue { get; }
    public Barrier[] WorkerBarriers { get; }
    private readonly bool[] _teamStopFlags;
    
    private int _deliveredCount;

    public Shop(int customers, int workers, int cashiers, CancellationToken token = default)
    {
        CustomersCount = customers;
        WorkersCount = workers;
        CashiersCount = cashiers;
        Token = token;
        
        ShopSemaphore = new SemaphoreSlim(8);
        StockSemaphore = new SemaphoreSlim(0);
        CheckoutQueue = new BlockingCollection<Customer>(5);
        
        int teams = workers / 3;
        _teamStopFlags = new bool[teams];
        WorkerBarriers = new Barrier[teams];
        for(int i = 0; i < teams; i++)
        {
            int teamId = i;
            WorkerBarriers[i] = new Barrier(3, (b) => 
            {
                if (IsStockGoalReached())
                {
                    _teamStopFlags[teamId] = true;
                }
            });
        }
    }

    public bool IsTeamStopped(int teamId) => _teamStopFlags[teamId];

    public void IncrementStock()
    {
        Interlocked.Increment(ref _deliveredCount);
        StockSemaphore.Release();
    }
    
    public bool IsStockGoalReached()
    {
        return _deliveredCount >= CustomersCount;
    }

    public async Task StartSimulationAsync()
    {
        var workerTasks = new List<Task>();
        var cashierTasks = new List<Task>();
        var customerTasks = new List<Task>();

        // Create Workers
        int teams = WorkersCount / 3;
        for (int i = 0; i < teams; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                var worker = new Worker(i * 3 + j + 1, i + 1, j, WorkerBarriers[i], this);
                workerTasks.Add(worker.DoWork());
            }
        }

        // Create Cashiers
        for (int i = 0; i < CashiersCount; i++)
        {
            var cashier = new Cashier(i + 1, this);
            cashierTasks.Add(cashier.DoWork());
        }

        // Create Customers
        for (int i = 0; i < CustomersCount; i++)
        {
            var customer = new Customer(i + 1, this);
            customerTasks.Add(customer.DoWork());
        }
        
        // Wait for Customers
        try 
        {
            await Task.WhenAll(customerTasks);
        }
        catch (OperationCanceledException) { }
        catch (Exception ex) { Console.WriteLine($"Customer Error: {ex}"); }

        // Once customers are done, stop Cashiers
        CheckoutQueue.CompleteAdding();
        
        try
        {
            await Task.WhenAll(cashierTasks);
        }
        catch (OperationCanceledException) { }
        catch (Exception ex) { Console.WriteLine($"Cashier Error: {ex}"); }
        
        // Workers should stop when goal reached
        try 
        {
            await Task.WhenAll(workerTasks);
        }
        catch (OperationCanceledException) { }
        catch (Exception ex) { Console.WriteLine($"Worker Error: {ex}"); }
    }

    public void Dispose()
    {
        ShopSemaphore.Dispose();
        StockSemaphore.Dispose();
        CheckoutQueue.Dispose();
        foreach (var b in WorkerBarriers) b.Dispose();
    }
}