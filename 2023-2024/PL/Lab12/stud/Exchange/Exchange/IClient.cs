namespace Exchange;

public interface IClient
{
    public void OnStartSession(StartSessionArgs args);
    public void OnEndSession(EndSessionArgs args);

    bool IsReady();
    void AddStartingData(int[] volume, double[] price);
    void OnRecordUpdate(RecordUpdateArgs args);
    void OnOrderProcessed(OrderProcessedArgs args);
    void AddExchange(Exchange exchange);
}

public class Client : IClient
{
    //Można dodawać dodatkowe pola klasy

    private Dictionary<int, string> _codeToName;
    private int[] _volume;

    private double[]? _currentPrice;
    private int[]? _volumeOnExchange;

    private List<Order> _ordersToSend;
    private Exchange? _exchange;

    public Client(Dictionary<int, string> codeToName, int[] startingVolume, List<Order> ordersToSend)
    {

    }
    public void OnStartSession(StartSessionArgs args)
    {

    }

    public void OnEndSession(EndSessionArgs args)
    {

    }

    public bool IsReady()
    {
        return default;
    }

    public void AddExchange(Exchange exchange)
    {

    }

    public void AddStartingData(int[] volume, double[] price)
    {

    }

    public void OnRecordUpdate(RecordUpdateArgs args)
    {

    }

    public void OnOrderProcessed(OrderProcessedArgs args)
    {

    }

    public void Run()
    {

    }
}
