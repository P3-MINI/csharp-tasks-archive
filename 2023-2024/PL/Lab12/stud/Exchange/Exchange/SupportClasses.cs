namespace Exchange;

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
