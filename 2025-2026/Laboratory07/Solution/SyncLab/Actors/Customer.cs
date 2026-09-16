namespace SyncLab.Actors;

public class Customer : IActor
{
    public int Id { get; }
    private readonly Shop _shop;
    public SemaphoreSlim PaidSignal { get; } = new SemaphoreSlim(0);

    public Customer(int id, Shop shop)
    {
        Id = id;
        _shop = shop;
    }

    public async Task DoWork()
    {
        try
        {
            await _shop.ShopSemaphore.WaitAsync(_shop.Token);
            Console.WriteLine($"CUSTOMER {Id}: Entered the shop");
            
            try
            {
                await Task.Delay(200, _shop.Token);
                
                // Wait for stock
                await _shop.StockSemaphore.WaitAsync(_shop.Token);
                Console.WriteLine($"CUSTOMER {Id}: Picked up Lidlomix");
                
                // Go to checkout
                _shop.CheckoutQueue.Add(this, _shop.Token);
                
                // Wait for cashier to finish serving
                await PaidSignal.WaitAsync(_shop.Token);
                Console.WriteLine($"CUSTOMER {Id}: Paid and leaving");
            }
            finally
            {
                _shop.ShopSemaphore.Release();
            }
        }
        catch (OperationCanceledException)
        {
            // Graceful exit
        }
        catch (InvalidOperationException)
        {
            // Shop closed while queuing
        }
        finally
        {
            PaidSignal.Dispose();
        }
    }
}