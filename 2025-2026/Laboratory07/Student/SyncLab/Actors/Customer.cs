namespace SyncLab.Actors;

public class Customer : IActor
{
    private readonly int _id;
    private readonly Shop _shop;

    public Customer(int id, Shop shop)
    {
        _id = id;
        _shop = shop;
    }

    public async Task DoWork()
    {
        // TODO: Implement Customer logic
        // 1. Enter shop (wait for capacity)
        // 2. Simulate shopping (wait 200ms)
        // 3. Wait for stock if necessary
        // 4. Pick up item
        // 5. Join checkout queue
        // 6. Wait for cashier
        // 7. Leave
        
        await Task.CompletedTask;
    }
}
