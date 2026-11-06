// #define STAGE1
// #define STAGE2
// #define STAGE3

using OrdoMajoris;

#if !STAGE1 && !STAGE2 && !STAGE3
Console.WriteLine();
Console.WriteLine(" Innocentiae Nihil Probat: Innocence Proves Nothing.");
Console.WriteLine();
Console.WriteLine(" We are His Sword, We are His Wrath. Suffering is our prayer and faith our armor. We shall not submit!");
Console.WriteLine();
Console.WriteLine(" [Uncomment the stages in Stage.props in Solution Items to start the work.]");
Console.WriteLine();
#endif

#if STAGE1
var xenos = new Xenos();

Console.WriteLine("=== Inquisitor Katarina Vex of Ordo Xenos — Specimen Threat Report ===");
Console.WriteLine();
Console.WriteLine("--- All specimens ---");

var all = xenos.ProcessSpecimens(_ => true, _ => { }, t => t.BonusPercentage, _ => 0);

foreach (var (specimen, total) in all.OrderByDescending(x => x.Item1.BaseThreat)) {
    Console.WriteLine($" {specimen.Name,-18} base={specimen.BaseThreat,2:F0}  bonus={total,2:F0} ");
}

Console.WriteLine();
Console.WriteLine("--- Xenos Threat Classification ---");
var classified = xenos.ClassifyXenosThreat();
foreach (var (specimen, total) in classified.OrderByDescending(x => x.Item2)) {
    Console.WriteLine($" {specimen.Name,-18} total={total + specimen.BaseThreat - 50,4:F1}  {specimen.Classification}");
}

Console.WriteLine();
Console.WriteLine("--- Summary ---");
Console.WriteLine($" Total specimens {xenos.Specimens.Count()}");
Console.WriteLine($" Non-Ignarus     {xenos.Specimens.Count(s => s.Classification != Classification.Ignarus)}");

var byClass = xenos.Specimens.GroupBy(s => s.Classification).OrderBy(g => g.Key);

foreach (var group in byClass) {
    Console.WriteLine($" {group.Key,-15} {group.Count()}");
}
#endif

#if STAGE2
Console.WriteLine();
Console.WriteLine("=== High Inquisitor Lucian Draven of Ordo Hereticus - Investigation of Subsector Drusus ===");
Console.WriteLine();
Console.WriteLine("--- Interleave: cross-referencing testimonies ---");

var ecclesiarchy = Database.EcclesiarchyTestimonies;
var governor = Database.GovernorTestimonies;

var merged = ecclesiarchy.Select(t => t.Statement)
    .Interleave(governor.Select(t => t.Statement))
    .ToArray();

for (int i = 0; i < merged.Length; i += 2) {
    var pair = i / 2 + 1;
    var fromEcclesiarchy = merged[i];
    var fromGovernor = i + 1 < merged.Length ? merged[i + 1] : "(none)";

    var churchWitness = ecclesiarchy[pair - 1].Witness;
    var governorWitness = pair - 1 < governor.Count ? governor[pair - 1].Witness : "(none)";

    Console.WriteLine($"  [{pair}] Ecclesiarchy ({churchWitness}):");
    Console.WriteLine($"      \"{fromEcclesiarchy}\"");
    Console.WriteLine($"      Governor ({governorWitness}):");
    Console.WriteLine($"      \"{fromGovernor}\"");
    if (fromEcclesiarchy != fromGovernor)
        Console.WriteLine("      CONTRADICTION DETECTED");
    Console.WriteLine();
}

Console.WriteLine("--- Median: typical heresy level in the population ---");

var population = Database.PopulationHeresy;
var populationMedian = population.Median(x => x);

Console.WriteLine($" Population size:      {population.Count}");
Console.WriteLine($" Median heresy:        {populationMedian:F2}%");
Console.WriteLine($" Max heresy:           {population.Max():F1}%");
Console.WriteLine();

if (populationMedian >= 50.0) {
    Console.WriteLine(" VERDICT: Heresy is widespread. Exterminatus is authorized.");
} else {
    Console.WriteLine(" VERDICT: Heresy is marginal. No Exterminatus. Continue investigation.");
}

Console.WriteLine();
Console.WriteLine("--- Median heresy by source ---");

var ecclesiarchyMedian = ecclesiarchy.Median(t => t.HeresyPercent);
var governorMedian = governor.Median(t => t.HeresyPercent);
var combinedMedian = ecclesiarchy.Select(t => t.HeresyPercent)
    .Concat(governor.Select(t => t.HeresyPercent))
    .Median(x => x);

Console.WriteLine($" Ecclesiarchy median:  {ecclesiarchyMedian:F2}%");
Console.WriteLine($" Governor median:      {governorMedian:F2}%");
Console.WriteLine($" Combined median:      {combinedMedian:F2}%");
Console.WriteLine();

Console.WriteLine("--- Inquisitor's Verdict ---");

if (governorMedian > 50.0 && populationMedian < 50.0) {
    Console.WriteLine(" The Governor is a heretic, but the population is loyal.");
    Console.WriteLine(" => Do NOT order Exterminatus :(");
    Console.WriteLine(" => Interrogate Magistrate Vorn and Advisor Kael.");
    Console.WriteLine(" => Purge the Governor and his inner circle only.");
} else if (populationMedian >= 50.0) {
    Console.WriteLine(" The population is widely heretical.");
    Console.WriteLine(" => Exterminatus is authorized.");
} else {
    Console.WriteLine(" Insufficient evidence. Continue investigation.");
}
#endif

#if STAGE3
Console.WriteLine();
Console.WriteLine("=== Canoness Superior Amalia Verrin of the Order of Our Martyred Lady - Order Archives ===");
Console.WriteLine();
Console.WriteLine("--- Third smallest Order from Terra ---");

var (orderName, orderMembers) = Sororitas.ThirdSmallestTerraOrder();
Console.WriteLine($" {orderName,-15}  members={orderMembers}");

Console.WriteLine();
Console.WriteLine("--- Total kill count by favourite weapon ---");

var weapons = Sororitas.WeaponsByTotalKillCount();

foreach (var (weapon, kills) in weapons) {
    Console.WriteLine($" {weapon,-12} kills={kills}");
}

Console.WriteLine();
Console.WriteLine("--- Summary ---");
Console.WriteLine($" Total orders:   {Database.Orders.Count}");
Console.WriteLine($" Total sisters:  {Database.Sisters.Count}");
Console.WriteLine($" Total kills:    {Database.Sisters.Sum(s => s.KillCount)}");
Console.WriteLine($" Weapons in use: {weapons.Count}");

var (topWeapon, topKills) = weapons.First();
Console.WriteLine($" Deadliest weapon: {topWeapon} ({topKills} kills)");
#endif
