// #define STAGE3

namespace AdeptusMechanicus.Tests;

#if STAGE3
[TestClass]
public sealed class ImplantsTests {
    [TestMethod]
    public void Oldest_Returns_ThreeItems() {
        var result = Implants.Top3OldestTechpriests();
        Assert.HasCount(3, result);
    }

    [TestMethod]
    public void Oldest_ReturnsExpected_NamesAndAges() {
        var result = Implants.Top3OldestTechpriests();

        CollectionAssert.AreEqual(
            new[] { "Draven Koll", "Thalrix Dorn", "Sarn Vex" },
            result.Select(r => r.Item1).ToList());

        CollectionAssert.AreEqual(
            new[] { 205, 187, 164 },
            result.Select(r => r.Item2).ToList());
    }

    [TestMethod]
    public void Oldest_SortsDescending_ByAge() {
        var result = Implants.Top3OldestTechpriests();
        var ages = result.Select(r => r.Item2).ToList();

        for (int i = 1; i < ages.Count; i++) {
            Assert.IsGreaterThanOrEqualTo(ages[i], ages[i - 1], $"{i - 1} ({ages[i - 1]}) >= {i} ({ages[i]})");
        }
    }

    [TestMethod]
    public void Oldest_Contains_OnlyMagosTechpriests() {
        var allowed = new[] { "Halix Vorn", "Thalrix Dorn", "Draven Koll", "Sarn Vex", "Marn Thule II" };
        var result = Implants.Top3OldestTechpriests();

        foreach (var item in result)
            CollectionAssert.Contains(allowed, item.Item1);
    }

    [TestMethod]
    public void Oldest_DoesNotInclude_OlderTechpriestsOfOtherRanks() {
        var names = Implants.Top3OldestTechpriests().Select(r => r.Item1).ToList();

        CollectionAssert.DoesNotContain(names, "Varnak Sollex");  // Archmagos, 310
        CollectionAssert.DoesNotContain(names, "Torv Kael");      // Archmagos, 276
    }

    [TestMethod]
    public void Oldest_DoesNotInclude_YoungerMagos() {
        var names = Implants.Top3OldestTechpriests().Select(r => r.Item1).ToList();

        CollectionAssert.DoesNotContain(names, "Marn Thule II");  // 153
        CollectionAssert.DoesNotContain(names, "Halix Vorn");     // 142
    }

    [TestMethod]
    public void Oldest_ReturnsNewList_OnEachCall() {
        var first = Implants.Top3OldestTechpriests();
        var second = Implants.Top3OldestTechpriests();

        Assert.AreNotSame(first, second);
        CollectionAssert.AreEqual(first, second);
    }

    [TestMethod]
    public void AverageCost_Returns_FourItems() {
        var result = Implants.ForgeWorldsByAverageCost();

        Assert.HasCount(4, result);
    }

    [TestMethod]
    public void AverageCost_ReturnsExpected_ForgeWorldsAndAverages() {
        var result = Implants.ForgeWorldsByAverageCost();

        CollectionAssert.AreEqual(
            new[] { "Lucius", "Ryza", "Graia", "Mars" },
            result.Select(r => r.Item1).ToList());

        CollectionAssert.AreEqual(
            new[] { 800m, 875m, 1012.5m, 6500m / 6 },
            result.Select(r => r.Item2).ToList());
    }

    [TestMethod]
    public void AverageCost_SortsAscending_ByAverage() {
        var result = Implants.ForgeWorldsByAverageCost();
        var costs = result.Select(r => r.Item2).ToList();

        for (int i = 1; i < costs.Count; i++) {
            Assert.IsLessThanOrEqualTo(costs[i], costs[i - 1], $"{i - 1} ({costs[i - 1]}) <= {i} ({costs[i]})");
        }
    }

    [TestMethod]
    public void AverageCost_GroupsByForgeWorld() {
        var result = Implants.ForgeWorldsByAverageCost();
        var worlds = result.Select(r => r.Item1).ToList();

        CollectionAssert.AllItemsAreUnique(worlds);
        CollectionAssert.AreEquivalent(
            new[] { "Mars", "Ryza", "Lucius", "Graia" },
            worlds);
    }

    [TestMethod]
    public void AverageCost_Has_ExpectedAverage_PerForgeWorld() {
        var result = Implants.ForgeWorldsByAverageCost();

        Assert.AreEqual(800m, result.Single(r => r.Item1 == "Lucius").Item2);
        Assert.AreEqual(875m, result.Single(r => r.Item1 == "Ryza").Item2);
        Assert.AreEqual(1012.5m, result.Single(r => r.Item1 == "Graia").Item2);
        Assert.AreEqual(6500m / 6, result.Single(r => r.Item1 == "Mars").Item2);
    }

    [TestMethod]
    public void AverageCost_EqualsAverage_OfOrdersOfThatForgeWorld() {
        foreach (var (world, average) in Implants.ForgeWorldsByAverageCost()) {
            var expected = Database.ImplantOrders
                .Where(o => o.ForgeWorld == world)
                .Average(o => o.Cost);

            Assert.AreEqual(expected, average);
        }
    }

    [TestMethod]
    public void AverageCost_IsOrderedByAverage_NotByTotal() {
        var result = Implants.ForgeWorldsByAverageCost();

        Assert.AreEqual("Graia", result[2].Item1);
        Assert.AreEqual("Mars", result[3].Item1);
    }

    [TestMethod]
    public void AverageCost_ReturnsNewList_OnEachCall() {
        var first = Implants.ForgeWorldsByAverageCost();
        var second = Implants.ForgeWorldsByAverageCost();

        Assert.AreNotSame(first, second);
        CollectionAssert.AreEqual(first, second);
    }
}
#endif
