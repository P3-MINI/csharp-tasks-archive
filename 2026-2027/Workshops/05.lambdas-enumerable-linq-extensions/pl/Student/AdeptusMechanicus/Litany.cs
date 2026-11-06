namespace AdeptusMechanicus;

public record Component(string Name, int Integrity);
public record Petition(string Text, int Potency, int Index);

public class Litany {
    public IEnumerable<Component> Components { get; init; } = [
        new("Plasma Coil",      45),
        new("Warp Drive",       30),
        new("Gellar Field",     72),
        new("Void Shield",      60),
        new("Cogitator",        90),
        new("Voidsunder Lance", 75),
        new("Servo-Skull",      47)
    ];

    public List<Petition> Petitions { get; init; } = [
        new("Cleanse the oil",      0b000001, 0b0000),
        new("Guide the machine",    0b000100, 0b0001),
        new("Appease the spirit",   0b001100, 0b0010),
        new("Restore the function", 0b011000, 0b0011),
        new("Awaken the soul",      0b100100, 0b0100),
    ];

    private const int threshold = 50;

    // TODO
    public List<(Component, int, int)> ReciteLitany(
        Func<Component, bool> filter,
        Action<Component> calculate,
        Func<Petition, int> select,
        Func<int, int> summarize) {

        var result = new List<(Component, int, int)>();
        /* uzupełnij swoim kodem */
        return result;
    }


    // TODO
    public List<(Component, int, int)> LitanyOfRestoration() {
        int damage = 0;
        throw new NotImplementedException(); // <-- zastąp swoim kodem
    }
}
