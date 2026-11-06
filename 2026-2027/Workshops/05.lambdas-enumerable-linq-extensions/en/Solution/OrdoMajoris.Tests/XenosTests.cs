// #define STAGE1

namespace OrdoMajoris.Tests;

#if STAGE1
[TestClass]
public sealed class XenosTests {
    private static Xenos MakeXenos(Specimen[]? specimens = null) => specimens is null ? new() : new() { Specimens = specimens };

    [TestMethod]
    public void ProcessSpecimens_AssignsClassification_ToAllSpecimens() {
        var xenos = MakeXenos([
            new Specimen("A", 30, []),
            new Specimen("B", 70, []),
            new Specimen("C", 90, []),
        ]);

        xenos.ProcessSpecimens(
            filter: _ => true,
            calculate: _ => { },
            transform: _ => 0,
            classify: _ => Classification.Majoris);

        foreach (var s in xenos.Specimens) {
            Assert.AreEqual(Classification.Majoris, s.Classification);
        }
    }

    [TestMethod]
    public void ProcessSpecimens_ResultContainsOnly_FilteredSpecimens() {
        var xenos = MakeXenos();

        var result = xenos.ProcessSpecimens(
            filter: s => s.BaseThreat > 80,
            calculate: _ => { },
            transform: t => 0,
            classify: _ => Classification.Majoris);

        Assert.HasCount(2, result);
        CollectionAssert.AreEquivalent(
            new[] { "Tyranid Warrior", "Genestealer" },
            result.Select(r => r.Item1.Name).ToArray());
    }

    [TestMethod]
    public void ProcessSpecimens_ReturnsEmpty_WhenNothingPasses() {
        var xenos = MakeXenos();

        var result = xenos.ProcessSpecimens(
            filter: _ => false,
            calculate: _ => { },
            transform: t => 0,
            classify: _ => Classification.Minoris);

        Assert.IsEmpty(result);
    }

    [TestMethod]
    public void ProcessSpecimens_Calculate_CalledOncePerFilteredSpecimen() {
        var xenos = MakeXenos();
        int calls = 0;

        xenos.ProcessSpecimens(
            filter: s => s.BaseThreat > 50,
            calculate: _ => calls++,
            transform: t => 0,
            classify: _ => Classification.Majoris);

        Assert.AreEqual(5, calls);
    }

    [TestMethod]
    public void ProcessSpecimens_Calculate_NotCalledWhenNothingPasses() {
        var xenos = MakeXenos();
        int calls = 0;

        xenos.ProcessSpecimens(
            filter: _ => false,
            calculate: _ => calls++,
            transform: t => 0,
            classify: _ => Classification.Minoris);

        Assert.AreEqual(0, calls);
    }

    [TestMethod]
    public void ProcessSpecimens_Transform_SummedOverTraits() {
        var xenos = MakeXenos();

        var result = xenos.ProcessSpecimens(
            filter: s => s.Name == "Tyranid Warrior",
            calculate: _ => { },
            transform: t => t.BonusPercentage,
            classify: _ => Classification.Majoris);

        // Adrenal 40 + Toxin 20 + Brittle -20 = 40
        Assert.AreEqual(40.0, result.Single().Item2, 1e-9);
    }

    [TestMethod]
    public void ProcessSpecimens_Classify_ReceivesTransformTotal() {
        var xenos = MakeXenos();
        double seen = double.NaN;

        xenos.ProcessSpecimens(
            filter: s => s.Name == "Tau Fire Warrior",
            calculate: _ => { },
            transform: t => t.BonusPercentage,
            classify: total => { seen = total; return Classification.Minoris; });

        // Targeting 30 + Hardened 20 = 50
        Assert.AreEqual(50.0, seen, 1e-9);
    }

    [TestMethod]
    public void ProcessSpecimens_ClassificationMatches_ClassifyResult() {
        var xenos = MakeXenos();

        xenos.ProcessSpecimens(
            filter: _ => true,
            calculate: _ => { },
            transform: t => t.BonusPercentage,
            classify: total => total switch {
                >= 60 => Classification.Extremis,
                >= 45 => Classification.Majoris,
                >= 30 => Classification.Minoris,
                _ => Classification.Ignarus,
            });

        Assert.AreEqual(Classification.Minoris, xenos.Specimens.First(s => s.Name == "Tyranid Warrior").Classification); // 40
        Assert.AreEqual(Classification.Majoris, xenos.Specimens.First(s => s.Name == "Tau Fire Warrior").Classification); // 50
        Assert.AreEqual(Classification.Minoris, xenos.Specimens.First(s => s.Name == "Eldar Farseer").Classification); // 40
        Assert.AreEqual(Classification.Ignarus, xenos.Specimens.First(s => s.Name == "Ogryn").Classification); // 20
        Assert.AreEqual(Classification.Minoris, xenos.Specimens.First(s => s.Name == "Nurgling").Classification); // 40
        Assert.AreEqual(Classification.Extremis, xenos.Specimens.First(s => s.Name == "Genestealer").Classification); // 70
        Assert.AreEqual(Classification.Majoris, xenos.Specimens.First(s => s.Name == "Kroot Shaper").Classification); // 50
    }

    [TestMethod]
    public void ProcessSpecimens_Calculate_RunsBeforeTransform() {
        var xenos = MakeXenos();
        int current = 0;
        var seen = new List<int>();

        xenos.ProcessSpecimens(
            filter: s => s.Name == "Genestealer",
            calculate: s => current = s.BaseThreat,
            transform: t => { seen.Add(current); return 0; },
            classify: _ => Classification.Majoris);

        CollectionAssert.AreEqual(new[] { 85, 85, 85 }, seen);
    }

    [TestMethod]
    public void ProcessSpecimens_ResultContainsTotal_ForEachSpecimen() {
        var xenos = MakeXenos();

        var result = xenos.ProcessSpecimens(
            filter: _ => true,
            calculate: _ => { },
            transform: t => t.BonusPercentage,
            classify: _ => Classification.Minoris);

        // Tyranid 40, Tau 50, Eldar 40, Ogryn 20, Nurgling 40, Genestealer 70, Kroot 50
        var expected = new[] { 40.0, 50.0, 40.0, 20.0, 40.0, 70.0, 50.0 };
        var actual = result.Select(r => r.Item2).ToList();

        Assert.HasCount(expected.Length, actual);
        for (int i = 0; i < expected.Length; i++) {
            Assert.AreEqual(expected[i], actual[i], 1e-9);
        }
    }

    [TestMethod]
    public void ClassifyXenosThreat_ReturnsOnlySpecimens_AboveThreshold() {
        var xenos = MakeXenos();
        var result = xenos.ClassifyXenosThreat();

        CollectionAssert.AreEquivalent(
            new[] { "Tau Fire Warrior", "Eldar Farseer", "Genestealer", "Kroot Shaper", "Tyranid Warrior" },
            result.Select(r => r.Item1.Name).ToArray());
    }

    [TestMethod]
    public void ClassifyXenosThreat_ReturnsExpected_SpecimensAndTotals() {
        var xenos = MakeXenos();

        var result = xenos.ClassifyXenosThreat();

        CollectionAssert.AreEqual(
            new[] { "Tyranid Warrior", "Tau Fire Warrior", "Eldar Farseer", "Genestealer", "Kroot Shaper" },
            result.Select(r => r.Item1.Name).ToList());

        // excess * percentages: Tyranid 40 * 40%, Tau 5 * 50%, Eldar 25 * 40%, Genestealer 35 * 70%, Kroot 15 * 50%
        var expected = new[] { 16.0, 2.5, 10.0, 24.5, 7.5 };
        var actual = result.Select(r => r.Item2).ToList();

        for (int i = 0; i < expected.Length; i++) {
            Assert.AreEqual(expected[i], actual[i], 1e-9);
        }
    }

    [TestMethod]
    public void ClassifyXenosThreat_AssignsExpected_ClassificationsForDefaultData() {
        var xenos = MakeXenos();

        xenos.ClassifyXenosThreat();

        Classification Of(string name) => xenos.Specimens.First(s => s.Name == name).Classification;

        Assert.AreEqual(Classification.Extremis, Of("Tyranid Warrior"));   // 3 * (16 + 40) / 50 = 3.36 -> clamped to 3
        Assert.AreEqual(Classification.Minoris, Of("Tau Fire Warrior"));   // 3 * (2.5 + 5) / 50 = 0.45 -> clamped to 1
        Assert.AreEqual(Classification.Majoris, Of("Eldar Farseer"));      // 3 * (10 + 25) / 50 = 2.1 -> 2
        Assert.AreEqual(Classification.Extremis, Of("Genestealer"));       // 3 * (24.5 + 35) / 50 = 3.57 -> clamped to 3
        Assert.AreEqual(Classification.Minoris, Of("Kroot Shaper"));       // 3 * (7.5 + 15) / 50 = 1.35 -> 1
        Assert.AreEqual(Classification.Ignarus, Of("Ogryn"));              // filtered out
        Assert.AreEqual(Classification.Ignarus, Of("Nurgling"));           // filtered out
    }

    [TestMethod]
    public void ClassifyXenosThreat_ComputesExcess_ScaledByTraits() {
        var xenos = MakeXenos([
            new Specimen("T", 90, [new("A", 40), new("B", 20), new("C", -20)]),
        ]);

        var result = xenos.ClassifyXenosThreat();

        // excess = 40, sum = 40 * 0.4 + 40 * 0.2 + 40 * (-0.2) = 16 + 8 - 8 = 16
        Assert.AreEqual(16.0, result.Single().Item2, 1e-9);
    }

    [TestMethod]
    public void ClassifyXenosThreat_Classification_UsesExcessAndTotal() {
        var xenos = MakeXenos([
            new Specimen("T", 90, [new("A", 40), new("B", 20), new("C", -20)]),
        ]);

        var result = xenos.ClassifyXenosThreat();

        // clamp(3 * (16 + 40) / 50, 1, 3) = clamp(3.36, 1, 3) = 3 -> Extremis
        Assert.AreEqual(Classification.Extremis, result.Single().Item1.Classification);
    }

    [TestMethod]
    public void ClassifyXenosThreat_NoneAboveThreshold_ReturnsEmpty() {
        var xenos = MakeXenos([
            new Specimen("A", 5, []),
            new Specimen("B", 10, []),
        ]);

        var result = xenos.ClassifyXenosThreat();

        Assert.IsEmpty(result);
    }

    [TestMethod]
    public void ClassifyXenosThreat_EmptySpecimens_ReturnsEmpty() {
        var xenos = MakeXenos(Array.Empty<Specimen>());

        var result = xenos.ClassifyXenosThreat();

        Assert.IsEmpty(result);
    }

    [TestMethod]
    public void ClassifyXenosThreat_BoundaryAtThreshold_IsExcluded() {
        var xenos = MakeXenos([new Specimen("At 50", 50, [])]);

        var result = xenos.ClassifyXenosThreat();

        Assert.IsEmpty(result);
        Assert.AreEqual(Classification.Ignarus, xenos.Specimens.Single().Classification);
    }

    [TestMethod]
    public void ClassifyXenosThreat_BoundaryAboveThreshold_IsIncluded() {
        var xenos = MakeXenos([new Specimen("At 51", 51, [])]);

        var result = xenos.ClassifyXenosThreat();

        Assert.HasCount(1, result);
        // excess = 1, total = 0 -> clamp(3 * (0 + 1) / 50, 1, 3) = clamp(0.06, 1, 3) = 1 -> Minoris
        Assert.AreEqual(Classification.Minoris, xenos.Specimens.Single().Classification);
    }

    [TestMethod]
    public void ClassifyXenosThreat_HighThreat_ClampsToExtremis() {
        var xenos = MakeXenos([
            new Specimen("Extremis", 100, [ new("Big", 200) ]),
        ]);

        var result = xenos.ClassifyXenosThreat();

        // total = 50 * 2 = 100, excess = 50 -> 3 * 150 / 50 = 9 -> clamp to 3 -> Extremis
        Assert.AreEqual(Classification.Extremis, result.Single().Item1.Classification);
    }

    [TestMethod]
    public void ClassifyXenosThreat_NegativeNetThreat_ClampsToMinoris() {
        var xenos = MakeXenos([
            new Specimen("Weakened", 60, [new("Debuff", -200)]),
        ]);

        var result = xenos.ClassifyXenosThreat();

        // excess = 10, total = -20 -> 3 * (-20 + 10) / 50 = -0.6 -> clamped to 1 (Minoris)
        Assert.AreEqual(-20.0, result.Single().Item2, 1e-9);
        Assert.AreEqual(Classification.Minoris, result.Single().Item1.Classification);
    }

    [TestMethod]
    public void ClassifyXenosThreat_FractionalClass_IsTruncatedNotRounded() {
        var xenos = MakeXenos([
            new Specimen("Fractional", 80, [new("Boost", 50)]),
        ]);

        var result = xenos.ClassifyXenosThreat();

        // excess = 30, total = 15 -> 3 * (15 + 30) / 50 = 2.7 -> 2 (rounding would give 3)
        Assert.AreEqual(Classification.Majoris, result.Single().Item1.Classification);
    }

    [TestMethod]
    public void ClassifyXenosThreat_ExcessIsRecalculated_ForEachSpecimen() {
        var xenos = MakeXenos([
            new Specimen("Near", 60, [new("Full", 100)]),
            new Specimen("Far",  90, [new("Full", 100)]),
        ]);

        var result = xenos.ClassifyXenosThreat();

        // the same trait, but excess 10 and excess 40
        Assert.AreEqual(10.0, result[0].Item2, 1e-9);
        Assert.AreEqual(40.0, result[1].Item2, 1e-9);
        Assert.AreEqual(Classification.Minoris, result[0].Item1.Classification);   // 3 * (10 + 10) / 50 = 1.2 -> 1
        Assert.AreEqual(Classification.Extremis, result[1].Item1.Classification);  // 3 * (40 + 40) / 50 = 4.8 -> clamped to 3
    }

    [TestMethod]
    public void ClassifyXenosThreat_SetsClassification_OnlyOnFilteredSpecimens() {
        var xenos = MakeXenos([
            new Specimen("Low",   20, []),
            new Specimen("High", 80, [ new("Boost", 50) ]),
        ]);

        xenos.ClassifyXenosThreat();

        Assert.AreEqual(Classification.Ignarus, xenos.Specimens.First(s => s.Name == "Low").Classification);
        Assert.AreEqual(Classification.Majoris, xenos.Specimens.First(s => s.Name == "High").Classification);
    }
}
#endif
