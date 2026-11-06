namespace LosPollosHermanos;

public class BatchComparer : Comparer<ChickenBatch> {
    public static readonly double SafeTemperature = Kitchen.SafeTemperature;

    public override int Compare(ChickenBatch? a, ChickenBatch? b) {
        if (ReferenceEquals(a, b)) {
            return 0;
        }
        if (a is null) {
            return 1;
        }
        if (b is null) {
            return -1;
        }

        bool aSafe = a.Temperature >= SafeTemperature;
        bool bSafe = b.Temperature >= SafeTemperature;

        int c = bSafe.CompareTo(aSafe);
        if (c != 0) {
            return c;
        }

        c = b.HasMeth.CompareTo(a.HasMeth);
        if (c != 0) {
            return c;
        }

        return a.Pieces.CompareTo(b.Pieces);
    }
}