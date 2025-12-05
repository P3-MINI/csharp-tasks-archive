namespace Exchange;

public interface IClient
{
    void OnStartSession(StartSessionArgs args);
    void OnEndSession(EndSessionArgs args);
    bool IsReady();
    void AddStartingData(int[] volume, double[] price);
    void OnRecordUpdate(RecordUpdateArgs args);
    void OnOrderProcessed(OrderProcessedArgs args);
    void AddExchange(Exchange exchange);
}

public enum Side
{
    Buy = 0,
    Sell = 1,
}
public struct Operation
{
    public int Code;
    public Side Side;
    public int Volume;
}

public class Client : IClient
{
    private static int NextId = 0;
    public readonly int ClientID;

    private Dictionary<int, string> _codeToName;
    private int[] _volume;

    private double[]? _currentPrice;
    private int[]? _volumeOnExchange;

    private List<Order> _ordersToSend;
    private List<Order> _sentOrders = new List<Order>();
    private Exchange? _exchange;
    private readonly object _exchangeCond = new();

    private Random _random = new Random();

    public Client(Dictionary<int, string> codeToName, int[] startingVolume, List<Order> ordersToSend)
    {
        ClientID = NextId++;
        _codeToName = codeToName;
        _volume = startingVolume;
        _ordersToSend = ordersToSend;
    }
    public void OnStartSession(StartSessionArgs args)
    {
        Console.WriteLine("\n\n\n");
        Console.WriteLine($"ClientID: {ClientID}, Open positions:");
        foreach (var rec in _codeToName)
        {
            Console.WriteLine($"Id: {rec.Key}, Name: {rec.Value}, Shares: {_volume[rec.Key]}");
        }
    }

    public void OnEndSession(EndSessionArgs args)
    {
        Console.WriteLine($"\nClientID: {ClientID}, Closing Shop\n");
    }

    public bool IsReady()
    {
        return _exchange != null;
    }

    public void AddExchange(Exchange exchange)
    {
        lock (_exchangeCond)
        {
            _exchange = exchange;
            Monitor.Pulse(_exchangeCond);
        }
    }
    public void AddStartingData(int[] volume, double[] price)
    {
        _currentPrice = price;
        _volumeOnExchange = volume;
    }

    public void OnRecordUpdate(RecordUpdateArgs args)
    {
        _volumeOnExchange![args.SecurityCode] += args.VolumeChange;
    }

    public void OnOrderProcessed(OrderProcessedArgs args)
    {
        var val = _sentOrders.Find(x => x.OrderId == args.OrderId);

        if (val != null)
        {
            if (args.ExecutedSuccessfully)
            {
                _volume[val.OrderId] = val.Side switch
                {
                    Side.Buy => _volume[val.OrderId] + val.Volume,
                    Side.Sell => _volume[val.OrderId] - val.Volume,
                    _ => _volume[val.OrderId],
                };
            }
        }
    }

    public void Run()
    {
        lock (_exchangeCond)
        {
            while (!IsReady())
            {
                Monitor.Wait(_exchangeCond);
            }
        }

        foreach (var order in _ordersToSend)
        {
            _exchange?.Orders.Add(order);
            _sentOrders.Add(order);
            Console.WriteLine($"{Thread.CurrentThread.Name}: sent order");
            Thread.Sleep(_random.Next(10, 250));
        }
    }
}
