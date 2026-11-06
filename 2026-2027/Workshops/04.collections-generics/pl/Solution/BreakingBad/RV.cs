namespace BreakingBad;

public class RV {
    private readonly Dictionary<string, Container<Chemical>> _chemicals = [];
    public IEnumerable<string> ChemicalNames => _chemicals.Keys;
    public int ChemicalContainerCapacity { get; }
    public RV(int chemCapacity = 10) => ChemicalContainerCapacity = chemCapacity;

    // TODO
    public bool Stock(string chemical, int grams) {
        if (!_chemicals.TryGetValue(chemical, out var c)) {
            c = new Container<Chemical>(ChemicalContainerCapacity);
            _chemicals[chemical] = c;
        }
        return c.Add(new Chemical(chemical, grams));
    }

    // TODO
    public int Amount(string chemical) {
        if (!_chemicals.TryGetValue(chemical, out var c)) {
            return 0;
        }
        int total = 0;
        foreach (var ch in c) {
            total += ch.Grams;
        }
        return total;
    }

    // TODO
    public bool Consume(string chemical, int grams) {
        if (!_chemicals.TryGetValue(chemical, out var c) || Amount(chemical) < grams) {
            return false;
        }

        int remaining = grams;
        while (remaining > 0) {
            var top = c.Take();
            if (top is null) {
                break;
            }
            if (top.Grams > remaining) {
                c.Add(new Chemical(top.Name, top.Grams - remaining));
                remaining = 0;
            } else {
                remaining -= top.Grams;
            }
        }

        if (c.IsEmpty) {
            _chemicals.Remove(chemical);
        }
        return true;
    }

    // TODO
    public IEnumerable<Batch> CookBatches(string cook, Recipe recipe, int count, int grams) {
        for (int attempt = 0; attempt < count; attempt++) {
            if (!HasAll(this, recipe)) {
                yield break;
            }

            foreach (var (chemical, need) in recipe.PerBatch) {
                Consume(chemical, need);
            }

            double p = Purity(cook, attempt);
            if (p < 66.0) {
                yield break;
            }

            var batch = new Batch(cook, p, grams);
            yield return batch;
        }
    }

    private static bool HasAll(RV rv, Recipe recipe) {
        foreach (var (chemical, need) in recipe.PerBatch) {
            if (rv.Amount(chemical) < need) {
                return false;
            }
        }
        return true;
    }

    private static double Purity(string cook, int attempt) {
        return cook switch {
            "Walter" => Math.Min(85.0 + 3.0 * attempt, 99.1),
            "Gale" => Math.Min(80.0 + 1.5 * attempt, 96.0),
            "Jesse" => Math.Min(68.0 + 2.0 * attempt, 96.2),
            "Todd" => Math.Min(50.0 + 0.5 * attempt, 76.0),
            _ => 0.0,
        };
    }
}