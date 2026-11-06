// #define STAGE1

namespace AdeptusMechanicus.Tests;

#if STAGE1
[TestClass]
public sealed class LitanyTests {
    private static Litany MakeLitany(Component[]? components = null, List<Petition>? petitions = null) {
        var defaults = new Litany();
        return new Litany {
            Components = components ?? defaults.Components,
            Petitions = petitions ?? defaults.Petitions
        };
    }

    [TestMethod]
    public void ReciteLitany_NoComponentsIncluded_ReturnsEmpty() {
        var litany = MakeLitany();

        var result = litany.ReciteLitany(
            filter: _ => false,
            calculate: _ => { },
            select: p => p.Index,
            summarize: i => i);

        Assert.HasCount(0, result);
    }

    [TestMethod]
    public void ReciteLitany_Calculate_CalledOnce_PerIncludedComponent() {
        var litany = MakeLitany();
        int calls = 0;

        litany.ReciteLitany(
            filter: _ => true,
            calculate: _ => calls++,
            select: p => p.Index,
            summarize: i => i);

        Assert.AreEqual(7, calls);
    }

    [TestMethod]
    public void ReciteLitany_Calculate_NotCalled_ForSkippedComponents() {
        var litany = MakeLitany();
        int calls = 0;

        litany.ReciteLitany(
            filter: _ => false,
            calculate: _ => calls++,
            select: p => p.Index,
            summarize: i => i);

        Assert.AreEqual(0, calls);
    }

    [TestMethod]
    public void ReciteLitany_Calculate_ReceivesComponents_InOriginalOrder() {
        var litany = MakeLitany();
        var names = new List<string>();

        litany.ReciteLitany(
            filter: _ => true,
            calculate: c => names.Add(c.Name),
            select: p => p.Index,
            summarize: i => i);

        CollectionAssert.AreEqual(
            new[] { "Plasma Coil", "Warp Drive", "Gellar Field", "Void Shield", "Cogitator", "Voidsunder Lance", "Servo-Skull" },
            names);
    }

    [TestMethod]
    public void ReciteLitany_Select_CalledForEveryPetition_OfEveryIncludedComponent() {
        var litany = MakeLitany();
        int calls = 0;

        litany.ReciteLitany(
            filter: _ => true,
            calculate: _ => { },
            select: p => { calls++; return p.Index; },
            summarize: i => i);

        Assert.AreEqual(7 * 5, calls);
    }

    [TestMethod]
    public void ReciteLitany_Select_SeesStateSetByCalculate() {
        var litany = MakeLitany();
        int current = 0;
        var seen = new List<int>();

        litany.ReciteLitany(
            filter: c => c.Name == "Warp Drive",
            calculate: c => current = c.Integrity,
            select: p => { seen.Add(current); return p.Index; },
            summarize: i => i);

        CollectionAssert.AreEqual(new[] { 30, 30, 30, 30, 30 }, seen);
    }

    [TestMethod]
    public void ReciteLitany_Summarize_CalledOnce_PerIncludedComponent() {
        var litany = MakeLitany();
        int calls = 0;

        litany.ReciteLitany(
            filter: _ => true,
            calculate: _ => { },
            select: p => p.Index,
            summarize: i => { calls++; return i; });

        Assert.AreEqual(7, calls);
    }

    [TestMethod]
    public void ReciteLitany_Summarize_Receives_MaximumOfSelectedValues() {
        var litany = MakeLitany();
        var received = new List<int>();

        litany.ReciteLitany(
            filter: _ => true,
            calculate: _ => { },
            select: p => p.Index,
            summarize: i => { received.Add(i); return i; });

        CollectionAssert.AreEqual(new[] { 4, 4, 4, 4, 4, 4, 4 }, received);
    }

    [TestMethod]
    public void ReciteLitany_Result_ContainsComponent_MaximumAndSummary() {
        var litany = MakeLitany();

        var result = litany.ReciteLitany(
            filter: c => c.Name == "Warp Drive",
            calculate: _ => { },
            select: p => p.Potency,
            summarize: i => i * 2);

        Assert.HasCount(1, result);
        Assert.AreEqual((new Component("Warp Drive", 30), 36, 72), result[0]);
    }

    [TestMethod]
    public void ReciteLitany_Filter_IncludesOnlyMatchingComponents() {
        var litany = MakeLitany();

        var result = litany.ReciteLitany(
            filter: c => c.Integrity >= 75,
            calculate: _ => { },
            select: p => p.Index,
            summarize: i => i);

        CollectionAssert.AreEqual(
            new[] { "Cogitator", "Voidsunder Lance" },
            result.Select(r => r.Item1.Name).ToList());
    }

    [TestMethod]
    public void ReciteLitany_Result_PreservesComponentOrder() {
        var litany = MakeLitany();

        var result = litany.ReciteLitany(
            filter: _ => true,
            calculate: _ => { },
            select: p => p.Index,
            summarize: i => i);

        CollectionAssert.AreEqual(
            litany.Components.ToList(),
            result.Select(r => r.Item1).ToList());
    }

    [TestMethod]
    public void ReciteLitany_CustomComponentsAndPetitions_AreUsed() {
        var litany = MakeLitany(
            components: [new Component("Test Component", 10)],
            petitions: [new Petition("Custom Petition", 4, 0)]);

        var result = litany.ReciteLitany(
            filter: _ => true,
            calculate: _ => { },
            select: p => p.Potency,
            summarize: i => i + 1);

        Assert.HasCount(1, result);
        Assert.AreEqual((new Component("Test Component", 10), 4, 5), result[0]);
    }

    [TestMethod]
    public void LitanyOfRestoration_Returns_ThreeItems() {
        var litany = MakeLitany();

        var result = litany.LitanyOfRestoration();

        Assert.HasCount(3, result);
    }

    [TestMethod]
    public void LitanyOfRestoration_OnlyDamagedComponents_Included() {
        var litany = MakeLitany();

        var result = litany.LitanyOfRestoration();

        CollectionAssert.AreEqual(
            new[] { "Plasma Coil", "Warp Drive", "Servo-Skull" },
            result.Select(r => r.Item1.Name).ToList());
    }

    [TestMethod]
    public void LitanyOfRestoration_ReturnsExpected_IndicesAndValues() {
        var litany = MakeLitany();

        var result = litany.LitanyOfRestoration();

        CollectionAssert.AreEqual(
            new[] { 1, 2, 0 },
            result.Select(r => r.Item2).ToList());

        CollectionAssert.AreEqual(
            new[] { 25, 340, 3 },
            result.Select(r => r.Item3).ToList());
    }

    [TestMethod]
    public void LitanyOfRestoration_Selects_HighestRequiredPetition() {
        var litany = MakeLitany();

        var result = litany.LitanyOfRestoration();
        var warpDrive = result.Single(r => r.Item1.Name == "Warp Drive");

        // damage 20: potency 12 (index 2) is required, potency 24 (index 3) is not
        Assert.AreEqual(2, warpDrive.Item2);
    }

    [TestMethod]
    public void LitanyOfRestoration_ComponentAtThreshold_IsNotIncluded() {
        var litany = MakeLitany(components: [new Component("Borderline", 50)]);

        var result = litany.LitanyOfRestoration();

        Assert.HasCount(0, result);
    }

    [TestMethod]
    public void LitanyOfRestoration_PetitionWithPotencyEqualToDamage_IsRequired() {
        var litany = MakeLitany(components: [new Component("Edge", 38)]);

        var result = litany.LitanyOfRestoration();

        // damage 12: potencies 1, 4, 12 are required -> index 2, summary 12 * (1 + 4 + 12)
        Assert.HasCount(1, result);
        Assert.AreEqual(2, result[0].Item2);
        Assert.AreEqual(204, result[0].Item3);
    }

    [TestMethod]
    public void LitanyOfRestoration_MinimalDamage_UsesOnlyFirstPetition() {
        var litany = MakeLitany(components: [new Component("Barely Damaged", 49)]);

        var result = litany.LitanyOfRestoration();

        // damage 1: only potency 1 (index 0) is required, summary 1 * 1
        Assert.HasCount(1, result);
        Assert.AreEqual(0, result[0].Item2);
        Assert.AreEqual(1, result[0].Item3);
    }

    [TestMethod]
    public void LitanyOfRestoration_NoRequiredPetition_ReturnsMinusOneAndZero() {
        var litany = MakeLitany(
            components: [new Component("Barely Damaged", 49)],
            petitions: [new Petition("Costly", 5, 0), new Petition("Costlier", 9, 1)]);

        var result = litany.LitanyOfRestoration();

        // damage 1: every petition has potency of at least 5
        Assert.HasCount(1, result);
        Assert.AreEqual(-1, result[0].Item2);
        Assert.AreEqual(0, result[0].Item3);
    }

    [TestMethod]
    public void LitanyOfRestoration_CompletelyDamagedComponent_UsesAllPetitions() {
        var litany = MakeLitany(components: [new Component("Wreck", 0)]);

        var result = litany.LitanyOfRestoration();

        // damage 50: every petition is required -> index 4, summary 50 * (1 + 4 + 12 + 24 + 36)
        Assert.HasCount(1, result);
        Assert.AreEqual(4, result[0].Item2);
        Assert.AreEqual(3850, result[0].Item3);
    }

    [TestMethod]
    public void LitanyOfRestoration_Summary_EqualsDamageTimesCumulativePotency() {
        var litany = MakeLitany();

        foreach (var (component, index, summary) in litany.LitanyOfRestoration()) {
            var damage = 50 - component.Integrity;
            var expected = damage * litany.Petitions.Take(index + 1).Sum(p => p.Potency);

            Assert.AreEqual(expected, summary, component.Name);
        }
    }

    [TestMethod]
    public void LitanyOfRestoration_NoDamagedComponents_ReturnsEmpty() {
        var litany = MakeLitany(components:
        [
            new Component("Healthy A", 82),
            new Component("Healthy B", 95),
        ]);

        var result = litany.LitanyOfRestoration();

        Assert.HasCount(0, result);
    }

    [TestMethod]
    public void LitanyOfRestoration_ReturnsNewList_OnEachCall() {
        var litany = MakeLitany();

        var first = litany.LitanyOfRestoration();
        var second = litany.LitanyOfRestoration();

        Assert.AreNotSame(first, second);
        CollectionAssert.AreEqual(first, second);
    }
}
#endif
