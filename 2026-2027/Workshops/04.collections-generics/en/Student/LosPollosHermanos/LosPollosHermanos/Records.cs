namespace LosPollosHermanos;

public record ChickenBatch(int Id, int Pieces, double Temperature, bool HasMeth);
public record Recipe(string Name, double Temperature, IReadOnlyDictionary<string, double> SpicesPerBatch, IReadOnlyList<string> Equipment);