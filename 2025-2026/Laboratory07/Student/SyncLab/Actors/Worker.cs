namespace SyncLab.Actors;

public class Worker : IActor
{
    private readonly int _id;
    private readonly Shop _shop;
    
    // Additional fields for team coordination might be needed

    public Worker(int id, Shop shop)
    {
        _id = id;
        _shop = shop;
    }

    public async Task DoWork()
    {
        // TODO: Implement Worker logic
        // 1. Synchronize with team (Barrier)
        // 2. Leader delivers stock (wait 400ms, increment stock)
        // 3. Synchronize again
        // 4. Repeat until goal reached
        
        await Task.CompletedTask;
    }
}
