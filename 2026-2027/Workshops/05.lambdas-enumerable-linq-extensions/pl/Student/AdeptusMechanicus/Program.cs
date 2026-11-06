// #define STAGE1
// #define STAGE2
// #define STAGE3

using AdeptusMechanicus;
using System.Text;

#if !STAGE1 && !STAGE2 && !STAGE3
Console.WriteLine();
Console.WriteLine(" From the moment I understood the weakness of my flesh, it disgusted me. I craved the strength and certainty of steel.\r\n" +
    " I aspired to the purity of the Blessed Machine. Your kind cling to your flesh as if it will not decay and fail you.\r\n" +
    " One day the crude biomass you call a temple will wither, and you will beg my kind to save you.\r\n" +
    " But I am already saved, for the Machine is immortal... Even in death I serve the Omnissiah.\r\n");
Console.WriteLine(" [Uncomment the stages in Stage.props in Solution Items to start the work.]");
Console.WriteLine();
#endif

#if STAGE1
Console.WriteLine("=== Enginseer Prime Osric Vane — Machine Service Log ===");

var litany = new Litany();

Console.WriteLine();
Console.WriteLine("--- All components ---");
foreach (var component in litany.Components.OrderByDescending(x => x.Integrity)) {
    Console.WriteLine($" {component.Name,-18} integrity={component.Integrity}  damage={Math.Max(50 - component.Integrity, 0)}");
}

Console.WriteLine();
Console.WriteLine("--- All petitions ---");
foreach (var petition in litany.Petitions.OrderBy(x => x.Index)) {
    Console.WriteLine($" {petition.Text,-20}  potency={petition.Potency}");
}

Console.WriteLine();
Console.WriteLine("--- Litany of Restoration ---");

var restored = litany.LitanyOfRestoration();

foreach (var (component, index, total) in restored.OrderByDescending(x => x.Item1.Integrity)) {
    Console.WriteLine($" Blessed be the sacred {component.Name}!");
    for (int i = 0; i <= index; i++) {
        Console.WriteLine($" {i + 1}. {litany.Petitions[i].Text}!");
    }
    Console.WriteLine();
    Console.WriteLine($" Total prayer power: {total}");
    Console.WriteLine();
}

Console.WriteLine("Enginseer Vane recites the litany over the damaged components.");
Console.WriteLine();

var parts = new List<string>();
string encoded = Encoding.UTF8.GetString(Convert.FromBase64String("UHJhaXNlZCBiZSB0aGUgT21uaXNzaWFoIQ=="));
foreach (var ch in encoded) {
    parts.Add(Convert.ToString(ch, 2).PadLeft(8, '0'));
}
Console.WriteLine(string.Join(" ", parts));
#endif

#if STAGE2
Console.WriteLine();
Console.WriteLine("=== Archmagos Biologis Varnak Sollex — Sample Analysis ===");
Console.WriteLine();
Console.WriteLine("--- Invert: readings grouped by value ---");

var byReading = Database.Readings.Invert();

foreach (var (reading, cogitatorIds) in byReading.OrderBy(kv => kv.Key)) {
    var names = cogitatorIds.Select(id => Database.Cogitators.First(c => c.CogitatorId == id).Name);
    Console.WriteLine($"{reading,6:F1} => [{string.Join(", ", names)}]");
}

Console.WriteLine();
Console.WriteLine("--- IQR: anomaly detection ---");

var iqr = Database.Readings.IQR(kv => kv.Value);

var sorted = Database.Readings.Values.OrderBy(x => x).ToArray();
var lowerFence = sorted[sorted.Length / 4] - iqr * 3 / 2;
var upperFence = sorted[3 * sorted.Length / 4] + iqr * 3 / 2;

Console.WriteLine($" Sample size: {sorted.Length}");
Console.WriteLine($" IQR: {iqr:F1}");
Console.WriteLine($" Fences: ({lowerFence:F1}, {upperFence:F1})");
Console.WriteLine();

Console.WriteLine(" Anomalies:");
var anomalies = Database.Readings
    .Where(kv => kv.Value < lowerFence || kv.Value > upperFence)
    .OrderBy(kv => kv.Key).ToList();

if (anomalies.Count == 0) {
    Console.WriteLine("    (none)");
} else {
    foreach (var (id, reading) in anomalies) {
        var cogitator = Database.Cogitators.First(c => c.CogitatorId == id);
        Console.WriteLine($" {cogitator.Name} ({cogitator.Location}) => {reading:F1}");
    }
}

Console.WriteLine();
Console.WriteLine("Magos Sollex notes: two great anomalies detected in Sectors B and D.");
Console.WriteLine("Performing the Litany of Restoration required for Cogitator-Delta and Cogitator-Lambda.");
#endif

#if STAGE3
Console.WriteLine();
Console.WriteLine("=== Techpriest Voren Kast — Implant Order Reports ===");
Console.WriteLine();

Console.WriteLine("--- Top 3 oldest Magos techpriests ---");
var top3 = Implants.Top3OldestTechpriests();
for (int i = 0; i < top3.Count; i++) {
    Console.WriteLine($"  {i + 1}. {top3[i]}");
}

Console.WriteLine();
Console.WriteLine("--- Forge worlds by average cost (ascending order) ---");
var forgeWorlds = Implants.ForgeWorldsByAverageCost();
foreach (var (forgeWorld, average) in forgeWorlds) {
    Console.WriteLine($"  {forgeWorld,-7} {average,8:F2}");
}
#endif
