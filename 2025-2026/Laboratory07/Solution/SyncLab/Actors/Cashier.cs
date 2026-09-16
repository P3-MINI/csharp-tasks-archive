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
        await Task.Yield();
        try
        {
            foreach (var customer in _shop.CheckoutQueue.GetConsumingEnumerable(_shop.Token))
            {
                Console.WriteLine($"CASHIER {_id}: Serving Customer {customer.Id}");
                await Task.Delay(300, _shop.Token);
                customer.PaidSignal.Release();
            }
        }
        catch (OperationCanceledException)
        {
            // Graceful shutdown
        }
    }
}