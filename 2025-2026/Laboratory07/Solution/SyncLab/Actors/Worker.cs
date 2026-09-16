namespace SyncLab.Actors;

public class Worker : IActor
{
    private readonly int _id;
    private readonly int _teamId;
    private readonly int _memberId;
    private readonly Barrier _barrier;
    private readonly Shop _shop;

    public Worker(int id, int teamId, int memberId, Barrier barrier, Shop shop)
    {
        _id = id;
        _teamId = teamId;
        _memberId = memberId;
        _barrier = barrier;
        _shop = shop;
    }

    public async Task DoWork()
    {
        await Task.Yield();
        try
        {
            while (!_shop.IsTeamStopped(_teamId) && !_shop.Token.IsCancellationRequested)
            {
                // Synchronize start of phase
                _barrier.SignalAndWait(_shop.Token);

                if (_shop.IsTeamStopped(_teamId)) break;

                if (_memberId == 0)
                {
                    Console.WriteLine($"TEAM {_teamId}: Delivering stock");
                    await Task.Delay(400, _shop.Token);
                    _shop.IncrementStock();
                }

                // Synchronize end of phase (wait for leader to finish work)
                _barrier.SignalAndWait(_shop.Token);
            }
        }
        catch (OperationCanceledException)
        {
            // Graceful shutdown
        }
        catch (BarrierPostPhaseException)
        {
            // Barrier broken (likely shutdown)
        }
    }
}