namespace LosPollosHermanos;

public class Kitchen {
    public static readonly double SafeTemperature = 75.0;

    private const double SpiceLimit = 2000.0;
    private readonly HashSet<string> _equipment;
    private readonly Dictionary<string, double> _spices = [];
    private readonly Line<ChickenBatch> _ready;

    public Kitchen(int lineCapacity = 20) {
        _ready = new Line<ChickenBatch>(lineCapacity);
        _equipment = [];
    }

    public bool AddCookedBatch(ChickenBatch batch) => _ready.Add(batch);
    public ChickenBatch? ServeNext() => _ready.Take();

    // TODO
    public int StockEquipment(IEnumerable<string> newEquipment) {
        int before = _equipment.Count;
        _equipment.UnionWith(newEquipment);
        return _equipment.Count - before;
    }

    // TODO
    public double StockSpices(IEnumerable<(string Spice, double Grams)> amounts) {
        double added = 0.0;
        foreach (var (spice, grams) in amounts) {
            _spices.TryGetValue(spice, out var current);
            double room = SpiceLimit - current;        
            double toAdd = Math.Min(grams, room);     
            if (toAdd <= 0.0) {
                continue;
            }
            _spices[spice] = current + toAdd;
            added += toAdd;
        }
        return added;
    }

    // TODO
    public bool CanCook(Recipe recipe) {
        if (!_equipment.IsSupersetOf(recipe.Equipment)) {
            return false;
        }

        foreach (var (spice, need) in recipe.SpicesPerBatch) {
            if (!_spices.TryGetValue(spice, out var have) || have < need) {
                return false;
            }
        }

        return true;
    }

    // TODO
    public IEnumerable<ChickenBatch> CookBatches(Recipe recipe, int id, int pieces, int count, int methEvery) {
        for (int attempt = 0; attempt < count; attempt++) {
            if (!CanCook(recipe)) {
                yield break;
            }

            foreach (var (spice, need) in recipe.SpicesPerBatch) {
                _spices[spice] -= need;
            }

            bool hasMeth = attempt % methEvery == 0;
            var batch = new ChickenBatch(id + attempt, pieces, recipe.Temperature, hasMeth);

            if (!Accept(batch, attempt) || !_ready.Add(batch)) {
                yield break;
            }

            yield return batch;
        }
    }

    private static bool Accept(ChickenBatch batch, int attempt) {
        double effectiveTemp = batch.Temperature - attempt * 0.5;
        if (effectiveTemp >= SafeTemperature) {
            return true;
        }

        double deficit = SafeTemperature - effectiveTemp;
        return 1.0 / (1.0 + deficit * 0.1) > 0.5;
    }
}