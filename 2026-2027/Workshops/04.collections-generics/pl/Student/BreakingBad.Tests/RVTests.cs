namespace BreakingBad.Tests;

#if STAGE2
[TestClass]
public class RVTests {
    private static bool Contains(RV rv, string name) {
        foreach (var n in rv.ChemicalNames) {
            if (n == name) return true;
        }
        return false;
    }

    private static Recipe Single(string chemical, int need) =>
        new("test", new Dictionary<string, int> { [chemical] = need });

    [TestMethod]
    public void Stock_CreatesContainer_OnFirstUse() {
        var rv = new RV();
        Assert.IsFalse(rv.ChemicalNames.GetEnumerator().MoveNext(), "empty RV");
        Assert.IsTrue(rv.Stock("methylamine", 100));
        Assert.IsTrue(Contains(rv, "methylamine"));
    }

    [TestMethod]
    public void Stock_PushesShipments() {
        var rv = new RV();
        rv.Stock("methylamine", 100);
        rv.Stock("methylamine", 200);
        Assert.AreEqual(300, rv.Amount("methylamine"));
    }

    [TestMethod]
    public void Stock_ReturnsFalse_WhenContainerFull() {
        var rv = new RV(1);
        Assert.IsTrue(rv.Stock("lithium", 10));
        Assert.IsFalse(rv.Stock("lithium", 20));
        Assert.AreEqual(10, rv.Amount("lithium"));
    }

    [TestMethod]
    public void Amount_SumsAllShipments() {
        var rv = new RV();
        rv.Stock("methylamine", 500);
        rv.Stock("methylamine", 200);
        rv.Stock("methylamine", 50);
        Assert.AreEqual(750, rv.Amount("methylamine"));
    }

    [TestMethod]
    public void Amount_ReturnsZero_ForUnknownChemical() {
        var rv = new RV();
        Assert.AreEqual(0, rv.Amount("moronic acid"));
    }

    [TestMethod]
    public void Consume_ReturnsFalse_ForUnknownChemical() {
        var rv = new RV();
        Assert.IsFalse(rv.Consume("moronic acid", 10));
    }

    [TestMethod]
    public void Consume_ReturnsFalse_WhenShort() {
        var rv = new RV();
        rv.Stock("methylamine", 100);
        Assert.IsFalse(rv.Consume("methylamine", 200));
    }

    [TestMethod]
    public void Consume_Short_DoesNotMutateState() {
        var rv = new RV();
        rv.Stock("methylamine", 100);
        rv.Stock("methylamine", 50);
        int before = rv.Amount("methylamine");

        Assert.IsFalse(rv.Consume("methylamine", 500));
        Assert.AreEqual(before, rv.Amount("methylamine"));
    }

    [TestMethod]
    public void Consume_PopsNewestShipmentFirst() {
        var rv = new RV(chemCapacity: 2);
        rv.Stock("methylamine", 100);
        rv.Stock("methylamine", 200);

        Assert.IsTrue(rv.Consume("methylamine", 150));
        Assert.AreEqual(150, rv.Amount("methylamine"));
        Assert.IsFalse(rv.Stock("methylamine", 10));

        Assert.IsTrue(rv.Consume("methylamine", 50));
        Assert.AreEqual(100, rv.Amount("methylamine"));
        Assert.IsTrue(rv.Stock("methylamine", 10));
    }

    [TestMethod]
    public void Consume_Partial_KeepsContainer() {
        var rv = new RV();
        rv.Stock("methylamine", 500);

        Assert.IsTrue(rv.Consume("methylamine", 200));
        Assert.AreEqual(300, rv.Amount("methylamine"));
        Assert.IsTrue(Contains(rv, "methylamine"));
    }

    [TestMethod]
    public void Consume_Exact_RemovesContainer() {
        var rv = new RV();
        rv.Stock("methylamine", 300);
        rv.Stock("methylamine", 200);

        Assert.IsTrue(rv.Consume("methylamine", 500));
        Assert.AreEqual(0, rv.Amount("methylamine"));
        Assert.IsFalse(Contains(rv, "methylamine"));
    }

    [TestMethod]
    public void Consume_Exact_ThenRestock_StartsFresh() {
        var rv = new RV(chemCapacity: 2);
        rv.Stock("lithium", 100);
        rv.Stock("lithium", 100);
        Assert.IsTrue(rv.Consume("lithium", 200));
        Assert.IsFalse(Contains(rv, "lithium"));

        Assert.IsTrue(rv.Stock("lithium", 50));
        Assert.IsTrue(rv.Stock("lithium", 50));
        Assert.AreEqual(100, rv.Amount("lithium"));
    }

    [TestMethod]
    public void Consume_AcrossMultipleShipments() {
        var rv = new RV();
        rv.Stock("methylamine", 100);
        rv.Stock("methylamine", 100);
        rv.Stock("methylamine", 100);

        Assert.IsTrue(rv.Consume("methylamine", 250));
        Assert.AreEqual(50, rv.Amount("methylamine"));
        Assert.IsTrue(Contains(rv, "methylamine"));
    }

    [TestMethod]
    public void CookBatches_ProducesRequestedCount() {
        var rv = new RV();
        rv.Stock("methylamine", 1000);
        var produced = new List<Batch>();

        foreach (var b in rv.CookBatches("Walter", Single("methylamine", 100), 5, 500)) {
            produced.Add(b);
        }

        Assert.HasCount(5, produced);
    }

    [TestMethod]
    public void CookBatches_StopsWhenChemicalsRunShort() {
        var rv = new RV();
        rv.Stock("methylamine", 150);
        var produced = new List<Batch>();

        foreach (var b in rv.CookBatches("Walter", Single("methylamine", 100), 5, 500)) {
            produced.Add(b);
        }

        Assert.HasCount(1, produced);
        Assert.AreEqual(50, rv.Amount("methylamine"));
    }

    [TestMethod]
    public void CookBatches_StopsOnQCFailure() {
        var rv = new RV();
        rv.Stock("methylamine", 1000);
        var produced = new List<Batch>();

        foreach (var b in rv.CookBatches("Todd", Single("methylamine", 100), 5, 500)) {
            produced.Add(b);
        }

        Assert.IsEmpty(produced);
        Assert.AreEqual(900, rv.Amount("methylamine"));
    }

    [TestMethod]
    public void CookBatches_Walter_StartsAt85_GrowsBy3() {
        var rv = new RV();
        rv.Stock("methylamine", 10_000);
        var produced = new List<Batch>();

        foreach (var b in rv.CookBatches("Walter", Single("methylamine", 1), 4, 100)) {
            produced.Add(b);
        }

        Assert.HasCount(4, produced);
        Assert.AreEqual(85.0, produced[0].Purity, 1e-9);
        Assert.AreEqual(88.0, produced[1].Purity, 1e-9);
        Assert.AreEqual(91.0, produced[2].Purity, 1e-9);
        Assert.AreEqual(94.0, produced[3].Purity, 1e-9);
    }

    [TestMethod]
    public void CookBatches_Walter_CapsAt99_1() {
        var rv = new RV();
        rv.Stock("methylamine", 10_000);
        var produced = new List<Batch>();

        foreach (var b in rv.CookBatches("Walter", Single("methylamine", 1), 20, 100)) {
            produced.Add(b);
        }

        Assert.AreEqual(99.1, produced[^1].Purity, 1e-9);
    }

    [TestMethod]
    public void CookBatches_Todd_StopsImmediately_Below66() {
        var rv = new RV();
        rv.Stock("methylamine", 10_000);
        var produced = new List<Batch>();

        foreach (var b in rv.CookBatches("Todd", Single("methylamine", 1), 30, 100)) {
            produced.Add(b);
        }

        Assert.IsEmpty(produced);
    }

    [TestMethod]
    public void CookBatches_UnknownCook_ReturnsZeroPurity_Stops() {
        var rv = new RV();
        rv.Stock("methylamine", 1000);
        var produced = new List<Batch>();

        foreach (var b in rv.CookBatches("Badger", Single("methylamine", 100), 5, 500)) {
            produced.Add(b);
        }

        Assert.IsEmpty(produced);
        Assert.AreEqual(900, rv.Amount("methylamine"));
    }

    [TestMethod]
    public void CookBatches_MultiChemicalRecipe_DoesNotConsumeWhenAnyIngredientShort() {
        var rv = new RV();
        rv.Stock("methylamine", 100);
        rv.Stock("lithium", 20);
        var recipe = new Recipe("blue", new Dictionary<string, int> {
            ["methylamine"] = 100,
            ["lithium"] = 50,
        });

        var produced = new List<Batch>();
        foreach (var b in rv.CookBatches("Walter", recipe, 3, 500)) {
            produced.Add(b);
        }

        Assert.IsEmpty(produced);
        Assert.AreEqual(100, rv.Amount("methylamine"));
        Assert.AreEqual(20, rv.Amount("lithium"));
    }

    [TestMethod]
    public void CookBatches_YieldsLazily_AndSetsCookAndGrams() {
        var rv = new RV();
        rv.Stock("methylamine", 1000);

        var stream = rv.CookBatches("Jesse", Single("methylamine", 100), 5, 250);
        Assert.AreEqual(1000, rv.Amount("methylamine"));

        var taken = new List<Batch>();
        foreach (var b in stream) {
            taken.Add(b);
            if (taken.Count == 2) {
                break;
            }
        }

        Assert.HasCount(2, taken);
        Assert.AreEqual(800, rv.Amount("methylamine"));
        Assert.AreEqual("Jesse", taken[0].Cook);
        Assert.AreEqual(250, taken[0].Grams);
        Assert.AreEqual(68.0, taken[0].Purity, 1e-9);
        Assert.AreEqual(70.0, taken[1].Purity, 1e-9);
    }
}
#endif