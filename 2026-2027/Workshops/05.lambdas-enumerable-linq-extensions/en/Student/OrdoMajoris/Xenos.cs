namespace OrdoMajoris;

public enum Classification {
    Ignarus = 0, Minoris = 1, Majoris = 2, Extremis = 3
}

public record Trait(string Name, int BonusPercentage);

public record Specimen(string Name, int BaseThreat, Trait[] Traits) {
    public Classification Classification { get; set; } = Classification.Ignarus;
}

public class Xenos {
    public IEnumerable<Specimen> Specimens { get; init; } = [
        new("Tyranid Warrior",  90, [new("Adrenal Glands", 40), new("Toxin Sacs", 20), new("Brittle Chitin", -20)]),
        new("Tau Fire Warrior", 55, [new("Targeting Implant", 30), new("Hardened Skin", 20)]),
        new("Eldar Farseer",    75, [new("Psychic Gift", 60), new("Fragile Physique", -20)]),
        new("Ogryn",            40, [new("Thick Hide", 40), new("Slow Mind", -20)]),
        new("Nurgling",         20, [new("Plague Pox", 40)]),
        new("Genestealer",      85, [new("Rending Claws", 60), new("Hive Link", 30), new("Thin Carapace", -20)]),
        new("Kroot Shaper",     65, [new("Gene Graft", 30), new("Quill Weapon", 20)]),
    ];

    private const int threshold = 50;

    // TODO
    public List<(Specimen, double)> ProcessSpecimens(
        Func<Specimen, bool> filter,
        Action<Specimen> calculate,
        Func<Trait, double> transform,
        Func<double, Classification> classify) {

        var result = new List<(Specimen, double)>();
        /* complete with your code */
        return result;
    }

    // TODO
    public List<(Specimen, double)> ClassifyXenosThreat() {
        double excess = double.NaN;
        throw new NotImplementedException(); // <-- replace with your code
    }
}
