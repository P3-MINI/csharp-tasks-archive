namespace SyncLab.Actors;

public class Cashier : IActor
{
    private readonly int _id;
    private readonly Shop _shop;

    public Cashier(int id, Shop shop)
    {
        _id = id;
        _shop = shop;
    }

    public async Task DoWork()
    {
        // TODO: Implement Cashier logic
        // 1. Take customer from queue
        // 2. Simulate processing (wait 300ms)
        // 3. Signal customer to leave
        
        await Task.CompletedTask;
    }
}
