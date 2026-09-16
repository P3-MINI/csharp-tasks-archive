using SyncLab.Actors;

namespace SyncLab;

public class Shop : IDisposable
{
    public int CustomersCount { get; }
    public int WorkersCount { get; }
    public int CashiersCount { get; }
    public CancellationToken Token { get; }

    // TODO: Define necessary synchronization primitives (Semaphores, Barriers, Collections, etc.)

    public Shop(int customers, int workers, int cashiers, CancellationToken token = default)
    {
        CustomersCount = customers;
        WorkersCount = workers;
        CashiersCount = cashiers;
        Token = token;
        
        // TODO: Initialize primitives
    }

    public async Task StartSimulationAsync()
    {
        // TODO: 1. Create and start Customer tasks
        // TODO: 2. Create and start Worker tasks (in teams)
        // TODO: 3. Create and start Cashier tasks
        // TODO: 4. Wait for completion and handle shutdown gracefully

        await Task.CompletedTask;
    }

    public void Dispose()
    {
        // TODO: Dispose of synchronization primitives
    }
}
