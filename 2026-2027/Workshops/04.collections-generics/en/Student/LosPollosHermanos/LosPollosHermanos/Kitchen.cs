namespace LosPollosHermanos;

public class Kitchen {
    public static readonly double SafeTemperature = 75.0;

    // maximum amount of each spice that can be stored in the kitchen
    private const double SpiceLimit = 2000.0;

    // the collection of unique equipment stored in the kitchen
    private readonly HashSet<string> _equipment;

    // the collection of pairs <unique spice name, amount in grams>
    private readonly Dictionary<string, double> _spices = [];

    // the line with prepared chicken
    private readonly Line<ChickenBatch> _ready;

    public Kitchen(int lineCapacity = 20) {
        _ready = new Line<ChickenBatch>(lineCapacity);
        _equipment = [];
    }

    public bool AddCookedBatch(ChickenBatch batch) => _ready.Add(batch);
    public ChickenBatch? ServeNext() => _ready.Take();

    public int StockEquipment(IEnumerable<string> newEquipment) {
        throw new NotImplementedException(); // <-- replace with your own implementation
    }

    public double StockSpices(IEnumerable<(string Spice, double Grams)> amounts) {
        throw new NotImplementedException(); // <-- replace with your own implementation
    }

    public bool CanCook(Recipe recipe) {
        throw new NotImplementedException(); // <-- replace with your own implementation
    }

    public IEnumerable<ChickenBatch> CookBatches(Recipe recipe, int id, int pieces, int count, int methEvery) {
        throw new NotImplementedException(); // <-- replace with your own implementation
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