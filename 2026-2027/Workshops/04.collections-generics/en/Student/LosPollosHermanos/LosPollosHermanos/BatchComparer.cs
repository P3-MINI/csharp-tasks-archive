namespace LosPollosHermanos;

public class BatchComparer : Comparer<ChickenBatch> {
    public static readonly double SafeTemperature = Kitchen.SafeTemperature;

    public override int Compare(ChickenBatch? a, ChickenBatch? b) {
        throw new NotImplementedException(); // <-- replace with your own implementation
    }
}