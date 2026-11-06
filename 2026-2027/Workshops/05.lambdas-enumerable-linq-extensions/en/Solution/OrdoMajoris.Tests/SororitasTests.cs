// #define STAGE3

namespace OrdoMajoris.Tests;

#if STAGE3
[TestClass]
public sealed class SororitasTests {
    [TestMethod]
    public void ThirdSmallest_ReturnsExpected_NameAndMembers() {
        var result = Sororitas.ThirdSmallestTerraOrder();

        Assert.AreEqual("Ebon Chalice", result.Item1);
        Assert.AreEqual(3100, result.Item2);
    }

    [TestMethod]
    public void ThirdSmallest_IsOrder_FromTerra() {
        var result = Sororitas.ThirdSmallestTerraOrder();
        var order = Database.Orders.Single(o => o.Name == result.Item1);

        Assert.AreEqual("Terra", order.HomeWorld);
    }

    [TestMethod]
    public void ThirdSmallest_HasExactlyTwoSmallerOrders_OnTerra() {
        var result = Sororitas.ThirdSmallestTerraOrder();
        var smaller = Database.Orders.Count(o => o.HomeWorld == "Terra" && o.Members < result.Item2);

        Assert.AreEqual(2, smaller);
    }

    [TestMethod]
    public void ThirdSmallest_IsNot_SmallestOrSecondSmallest() {
        var name = Sororitas.ThirdSmallestTerraOrder().Item1;

        Assert.AreNotEqual("Sacred Rose", name);    
        Assert.AreNotEqual("Argent Shroud", name);  
    }

    [TestMethod]
    public void ThirdSmallest_ReturnsSameValue_OnEachCall() {
        var first = Sororitas.ThirdSmallestTerraOrder();
        var second = Sororitas.ThirdSmallestTerraOrder();

        Assert.AreEqual(first, second);
    }

    [TestMethod]
    public void Weapons_Returns_NineItems() {
        var result = Sororitas.WeaponsByTotalKillCount();
        Assert.HasCount(9, result);
    }

    [TestMethod]
    public void Weapons_ReturnsExpected_WeaponsAndTotals() {
        var result = Sororitas.WeaponsByTotalKillCount();

        CollectionAssert.AreEqual(
            new[] { "Power Sword", "Eviscerator", "Storm Bolter", "Chainsword", "Boltgun", "Bolt Pistol", "Heavy Bolter", "Flamer", "Meltagun" },
            result.Select(r => r.Item1).ToList());

        CollectionAssert.AreEqual(
            new[] { 2385, 884, 736, 717, 503, 472, 418, 174, 149 },
            result.Select(r => r.Item2).ToList());
    }

    [TestMethod]
    public void Weapons_SortsDescending_ByTotalKillCount() {
        var result = Sororitas.WeaponsByTotalKillCount();
        var totals = result.Select(r => r.Item2).ToList();

        for (int i = 1; i < totals.Count; i++) {
            Assert.IsLessThanOrEqualTo(totals[i - 1], totals[i], $"{i} ({totals[i]}) <= {i - 1} ({totals[i - 1]})");
        }
    }

    [TestMethod]
    public void Weapons_GroupsByWeapon_NotBySisterId() {
        var weapons = Sororitas.WeaponsByTotalKillCount().Select(r => r.Item1).ToList();

        CollectionAssert.AllItemsAreUnique(weapons);
        CollectionAssert.AreEquivalent(
            new[] { "Power Sword", "Eviscerator", "Storm Bolter", "Chainsword", "Boltgun", "Bolt Pistol", "Heavy Bolter", "Flamer", "Meltagun" },
            weapons);
    }

    [TestMethod]
    public void Weapons_HasExpectedTotal_PerWeapon() {
        var result = Sororitas.WeaponsByTotalKillCount();

        Assert.AreEqual(2385, result.Single(r => r.Item1 == "Power Sword").Item2);
        Assert.AreEqual(884, result.Single(r => r.Item1 == "Eviscerator").Item2);
        Assert.AreEqual(736, result.Single(r => r.Item1 == "Storm Bolter").Item2);
        Assert.AreEqual(717, result.Single(r => r.Item1 == "Chainsword").Item2);
        Assert.AreEqual(503, result.Single(r => r.Item1 == "Boltgun").Item2);
        Assert.AreEqual(472, result.Single(r => r.Item1 == "Bolt Pistol").Item2);
        Assert.AreEqual(418, result.Single(r => r.Item1 == "Heavy Bolter").Item2);
        Assert.AreEqual(174, result.Single(r => r.Item1 == "Flamer").Item2);
        Assert.AreEqual(149, result.Single(r => r.Item1 == "Meltagun").Item2);
    }

    [TestMethod]
    public void Weapons_EqualsSum_OfKillCountsOfThatWeapon() {
        foreach (var (weapon, total) in Sororitas.WeaponsByTotalKillCount()) {
            var expected = Database.Sisters
                .Where(s => s.FavouriteWeapon == weapon)
                .Sum(s => s.KillCount);

            Assert.AreEqual(expected, total);
        }
    }

    [TestMethod]
    public void Weapons_Totals_EqualSumOfAllKillCounts() {
        var expected = Database.Sisters.Sum(s => s.KillCount);
        var actual = Sororitas.WeaponsByTotalKillCount().Sum(r => r.Item2);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Weapons_ReturnsNewList_OnEachCall() {
        var first = Sororitas.WeaponsByTotalKillCount();
        var second = Sororitas.WeaponsByTotalKillCount();

        Assert.AreNotSame(first, second);
        CollectionAssert.AreEqual(first, second);
    }
}
#endif
