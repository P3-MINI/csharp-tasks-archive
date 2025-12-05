using System.Collections.Concurrent;

namespace Exchange;

public class Exchange
{
    private Dictionary<int, string> _securityCodeToName;
    private int[] _volume;
    private double[] _price;

    public delegate void StartSession(StartSessionArgs args);

    private event StartSession? StartSessionHandler;

    public delegate void EndSession(EndSessionArgs args);

    private event EndSession? EndSessionHandler;

    //Stage 3
    public delegate void RecordUpdate(RecordUpdateArgs args);

    private event RecordUpdate? RecordUpdateHandler;

    public delegate void OrderProcessed(OrderProcessedArgs args);

    private event OrderProcessed? OrderProcessedHandler;

    //End of Stage 3

    public Exchange(Dictionary<int, string> securityCodeToName, int[] startingFreeVolume, double[] startingPrice)
    {
        _securityCodeToName = securityCodeToName;
        _volume = startingFreeVolume;
        _price = startingPrice;
    }

    public void AddClient(IClient client)
    {
        client.AddStartingData(_volume, _price);

        //Stage 2
        StartSessionHandler += client.OnStartSession;
        EndSessionHandler += client.OnEndSession;
        //Stage 3
        RecordUpdateHandler += client.OnRecordUpdate;
        OrderProcessedHandler += client.OnOrderProcessed;
        client.AddExchange(this);
        //End of Stage 3
    }

    public void Start(int sessionTimeToLiveInSeconds)
    {
        Console.WriteLine("Starting session");
        var startTime = DateTime.Now;
        var startArgs = new StartSessionArgs()
        {
            StartTime = startTime,
            TimeToEndInSeconds = sessionTimeToLiveInSeconds
        };

        Orders = new BlockingCollection<Order>();
        StartSessionHandler?.Invoke(startArgs);

        Task.Delay(TimeSpan.FromSeconds(sessionTimeToLiveInSeconds))
            .ContinueWith(_ => Orders.CompleteAdding());

        foreach (var order in Orders.GetConsumingEnumerable())
        {
            Console.WriteLine($"{Thread.CurrentThread.Name}: Processing order");
            double priceChange = 0.0;
            int volumeChange = 0;
            switch (order.Side)
            {
                case Side.Buy:
                {
                    if (_volume[order.SecurityCode] >= order.Volume)
                    {
                        _volume[order.SecurityCode] -= order.Volume;
                        volumeChange = -order.Volume;
                        priceChange = 0.1 * order.Volume;
                        OrderProcessedHandler?.Invoke(new OrderProcessedArgs
                            { OrderId = order.OrderId, ExecutedSuccessfully = true });
                    }
                    else
                    {
                        OrderProcessedHandler?.Invoke(new OrderProcessedArgs
                            { OrderId = order.OrderId, ExecutedSuccessfully = false });
                        continue;
                    }
                }
                    break;
                case Side.Sell:
                {
                    _volume[order.SecurityCode] = +order.Volume;
                    volumeChange = order.Volume;
                    priceChange = -0.1 * order.Volume;
                    OrderProcessedHandler?.Invoke(new OrderProcessedArgs
                        { OrderId = order.OrderId, ExecutedSuccessfully = true });
                }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var updateArgs = new RecordUpdateArgs()
            {
                SecurityCode = order.SecurityCode,
                VolumeChange = volumeChange,
                PriceChange = priceChange,
            };
            RecordUpdateHandler?.Invoke(updateArgs);
        }

        EndSessionHandler?.Invoke(new EndSessionArgs { CurrentTime = DateTime.Now });
        CleanUp();
    }

    private void CleanUp()
    {
        Console.WriteLine("\n\n\n");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Session End");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Final volume and price:");
        foreach (var rec in _securityCodeToName)
        {
            Console.WriteLine($"Name: {rec.Value}, Volume: {_volume[rec.Key]}, Price: {_price[rec.Key]}");
        }
    }

    //Stage 3
    public BlockingCollection<Order> Orders = new BlockingCollection<Order>();
}

public class EndSessionArgs
{
    public DateTime CurrentTime;
}

public class StartSessionArgs
{
    public DateTime StartTime;
    public int TimeToEndInSeconds;
}

//Stage 3
public class RecordUpdateArgs
{
    public int SecurityCode;
    public int VolumeChange;
    public double PriceChange;
}

public class OrderProcessedArgs
{
    public int OrderId;
    public bool ExecutedSuccessfully;
}
