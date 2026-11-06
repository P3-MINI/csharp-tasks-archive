namespace BreakingBad;

// TODO
public class BatchComparer : Comparer<Batch> {
    public override int Compare(Batch? a, Batch? b) {
        if (ReferenceEquals(a, b)) {
            return 0;
        }
        if (a is null) {
            return 1;
        }
        if (b is null) {
            return -1;
        }

        int c = b.Purity.CompareTo(a.Purity);
        if (c != 0) {
            return c;
        }

        c = a.Cook.CompareTo(b.Cook);
        if (c != 0) {
            return c;
        }

        return b.Grams.CompareTo(a.Grams);
    }
}