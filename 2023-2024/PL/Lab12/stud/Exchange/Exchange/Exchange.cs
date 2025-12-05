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

    }

    public void AddClient(IClient client)
    {

    }

    public void Start(int sessionTimeToLiveInSeconds)
    {

    }

    private void CleanUp()
    {

    }
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

}

public class OrderProcessedArgs
{

}
