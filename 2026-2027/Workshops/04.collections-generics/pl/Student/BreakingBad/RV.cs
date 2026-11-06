namespace BreakingBad;

public class RV {
    // słownik przechowujący związki chemiczne w relacji <nazwa związku chemicznego, kontener ze związkiem>
    private readonly Dictionary<string, Container<Chemical>> _chemicals = [];

    // nazwy związków chemicznych dostępnych w kamperze
    public IEnumerable<string> ChemicalNames => _chemicals.Keys;

    // maksymalny rozmiar kontenerów używanych w kamperze (przydatny przy tworzeniu nowych kontenerów)
    public int ChemicalContainerCapacity { get; }

    public RV(int chemCapacity = 10) => ChemicalContainerCapacity = chemCapacity;

    public bool Stock(string chemical, int grams) {
        throw new NotImplementedException(); // <-- zastąp swoją implementacją
    }

    public int Amount(string chemical) {
        throw new NotImplementedException(); // <-- zastąp swoją implementacją
    }

    public bool Consume(string chemical, int grams) {
        throw new NotImplementedException(); // <-- zastąp swoją implementacją
    }

    public IEnumerable<Batch> CookBatches(string cook, Recipe recipe, int count, int grams) {
        throw new NotImplementedException(); // <-- zastąp swoją implementacją (podpowiedź: yield return / yield break)
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