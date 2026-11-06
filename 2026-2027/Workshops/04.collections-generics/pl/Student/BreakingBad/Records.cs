namespace BreakingBad;
public record Batch(string Cook, double Purity, int Grams);
public record Chemical(string Name, int Grams);
public record Recipe(string Name, IReadOnlyDictionary<string, int> PerBatch);