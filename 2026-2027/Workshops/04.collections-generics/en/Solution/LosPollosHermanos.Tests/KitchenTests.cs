namespace LosPollosHermanos.Tests;

#if STAGE2
[TestClass]
public class KitchenTests {
    private static Recipe TestRecipe(Dictionary<string, double> spices, string[] equipment) => new("test", 80.0, spices, equipment);
    private static Recipe SimpleRecipe() => TestRecipe(new() { ["salt"] = 10.0 }, ["fryer"]);
    private static Kitchen StockedKitchen(int lineCapacity = 20) {
        var k = new Kitchen(lineCapacity);
        k.StockSpices([("salt", 100.0)]);
        k.StockEquipment(["fryer"]);
        return k;
    }

    [TestMethod]
    public void StockSpices_Returns_AddedGrams() {
        var k = new Kitchen();
        Assert.AreEqual(300.0, k.StockSpices([("salt", 100.0), ("pepper", 150.0), ("paprika", 50.0)]), 1e-9);
        Assert.AreEqual(50.0, k.StockSpices([("salt", 50.0)]), 1e-9);
        Assert.AreEqual(0.0, k.StockSpices([("salt", 0.0)]), 1e-9);
    }

    [TestMethod]
    public void StockSpices_ClampsTo_SpiceLimit() {
        var k = new Kitchen();
        Assert.AreEqual(2000.0, k.StockSpices([("salt", 2500.0)]), 1e-9);
        Assert.AreEqual(0.0, k.StockSpices([("salt", 500.0)]), 1e-9);
        Assert.AreEqual(1500.0, k.StockSpices([("pepper", 1500.0)]), 1e-9);
        Assert.AreEqual(500.0, k.StockSpices([("pepper", 800.0)]), 1e-9);
    }

    [TestMethod]
    public void StockSpices_Accumulates_AcrossCalls() {
        var k = new Kitchen();
        k.StockSpices([("salt", 100.0)]);
        k.StockSpices([("pepper", 50.0)]);

        k.StockEquipment(["fryer"]);
        var recipe = TestRecipe(new() { ["salt"] = 100.0, ["pepper"] = 50.0 }, ["fryer"]);
        Assert.IsTrue(k.CanCook(recipe));
    }

    [TestMethod]
    public void StockEquipment_Returns_CountOfNewEntries() {
        var k = new Kitchen();
        Assert.AreEqual(3, k.StockEquipment(["breader", "fryer", "packer"]));
        Assert.AreEqual(1, k.StockEquipment(["fryer", "seasoner"]));
    }

    [TestMethod]
    public void StockEquipment_Ignores_Duplicates() {
        var k = new Kitchen();
        Assert.AreEqual(1, k.StockEquipment(["fryer", "fryer", "fryer"]));
    }

    [TestMethod]
    public void CanCook_False_WhenSpiceMissing() {
        var k = new Kitchen();
        k.StockSpices([("salt", 100.0), ("pepper", 100.0)]);
        k.StockEquipment(["fryer"]);

        var recipe = TestRecipe(new() { ["salt"] = 50.0, ["pepper"] = 50.0, ["paprika"] = 50.0 }, ["fryer"]);
        Assert.IsFalse(k.CanCook(recipe));
    }

    [TestMethod]
    public void CanCook_False_WhenSpiceInsufficient() {
        var k = new Kitchen();
        k.StockSpices([("salt", 30.0)]);
        k.StockEquipment(["fryer"]);

        var recipe = TestRecipe(new() { ["salt"] = 100.0 }, ["fryer"]);
        Assert.IsFalse(k.CanCook(recipe));
    }

    [TestMethod]
    public void CanCook_False_WhenEquipmentMissing() {
        var k = new Kitchen();
        k.StockSpices([("salt", 100.0)]);
        k.StockEquipment(["fryer"]);

        var recipe = TestRecipe(new() { ["salt"] = 50.0 }, ["fryer", "packer"]);
        Assert.IsFalse(k.CanCook(recipe));
    }

    [TestMethod]
    public void CanCook_True_WhenAllCovered() {
        var k = new Kitchen();
        k.StockSpices([("salt", 100.0), ("pepper", 100.0)]);
        k.StockEquipment(["breader", "fryer", "packer"]);

        var recipe = TestRecipe(new() { ["salt"] = 50.0, ["pepper"] = 50.0 }, ["breader", "fryer", "packer"]);
        Assert.IsTrue(k.CanCook(recipe));
    }

    [TestMethod]
    public void CanCook_IgnoresDuplicateEquipment() {
        var k = new Kitchen();
        k.StockSpices([("salt", 100.0)]);
        k.StockEquipment(["fryer"]);

        var recipe = TestRecipe(
            new() { ["salt"] = 50.0 },
            ["fryer", "fryer", "fryer"]);
        Assert.IsTrue(k.CanCook(recipe));
    }

    [TestMethod]
    public void ServeNext_ReturnsNull_WhenEmpty() {
        var k = new Kitchen();
        Assert.IsNull(k.ServeNext());
    }

    [TestMethod]
    public void ServeNext_PopsFifo() {
        var k = new Kitchen();
        k.AddCookedBatch(new ChickenBatch(1, 8, 80.0, false));
        k.AddCookedBatch(new ChickenBatch(2, 8, 80.0, false));
        k.AddCookedBatch(new ChickenBatch(3, 8, 80.0, false));

        Assert.AreEqual(1, k.ServeNext()!.Id);
        Assert.AreEqual(2, k.ServeNext()!.Id);
        Assert.AreEqual(3, k.ServeNext()!.Id);
        Assert.IsNull(k.ServeNext());
    }

    [TestMethod]
    public void CookBatches_Produces_RequestedCount() {
        var k = StockedKitchen();
        var produced = new List<ChickenBatch>();

        foreach (var b in k.CookBatches(SimpleRecipe(), id: 100, pieces: 8, count: 5, methEvery: 3)) {
            produced.Add(b);
        }

        Assert.HasCount(5, produced);
    }

    [TestMethod]
    public void CookBatches_ReturnsNothing_WhenCannotCook() {
        var k = new Kitchen();
        var produced = new List<ChickenBatch>();

        foreach (var b in k.CookBatches(SimpleRecipe(), id: 100, pieces: 8, count: 5, methEvery: 3)) {
            produced.Add(b);
        }

        Assert.IsEmpty(produced);
    }

    [TestMethod]
    public void CookBatches_IdsAreSequential_FromIdParameter() {
        var k = StockedKitchen();
        var produced = new List<ChickenBatch>();

        foreach (var b in k.CookBatches(SimpleRecipe(), id: 100, pieces: 8, count: 4, methEvery: 3)) {
            produced.Add(b);
        }

        Assert.AreEqual(100, produced[0].Id);
        Assert.AreEqual(101, produced[1].Id);
        Assert.AreEqual(102, produced[2].Id);
        Assert.AreEqual(103, produced[3].Id);
    }

    [TestMethod]
    public void CookBatches_StampsHasMeth_EveryNthAttempt() {
        var k = StockedKitchen();
        var produced = new List<ChickenBatch>();

        foreach (var b in k.CookBatches(SimpleRecipe(), id: 100, pieces: 8, count: 6, methEvery: 3)) {
            produced.Add(b);
        }

        Assert.IsTrue(produced[0].HasMeth);
        Assert.IsFalse(produced[1].HasMeth);
        Assert.IsFalse(produced[2].HasMeth);
        Assert.IsTrue(produced[3].HasMeth);
        Assert.IsFalse(produced[4].HasMeth);
        Assert.IsFalse(produced[5].HasMeth);
    }

    [TestMethod]
    public void CookBatches_Uses_RecipeTemperature() {
        var k = StockedKitchen();
        var recipe = TestRecipe(new() { ["salt"] = 10.0 }, ["fryer"]);
        var produced = new List<ChickenBatch>();

        foreach (var b in k.CookBatches(recipe, id: 100, pieces: 8, count: 2, methEvery: 3)) {
            produced.Add(b);
        }

        Assert.AreEqual(80.0, produced[0].Temperature, 1e-9);
        Assert.AreEqual(80.0, produced[1].Temperature, 1e-9);
    }

    [TestMethod]
    public void CookBatches_Stops_WhenReadyLineIsFull() {
        var k = StockedKitchen(lineCapacity: 2);
        var produced = new List<ChickenBatch>();

        foreach (var b in k.CookBatches(SimpleRecipe(), id: 100, pieces: 8, count: 10, methEvery: 3)) {
            produced.Add(b);
        }

        Assert.HasCount(2, produced);
    }

    [TestMethod]
    public void CookBatches_Consumes_Spices() {
        var k = new Kitchen();
        k.StockSpices([("salt", 100.0)]);
        k.StockEquipment(["fryer"]);

        var recipe = TestRecipe(new() { ["salt"] = 20.0 }, ["fryer"]);
        foreach (var _ in k.CookBatches(recipe, id: 100, pieces: 8, count: 3, methEvery: 3)) { }

        Assert.IsTrue(k.CanCook(TestRecipe(new() { ["salt"] = 40.0 }, ["fryer"])));
        Assert.IsFalse(k.CanCook(TestRecipe(new() { ["salt"] = 40.1 }, ["fryer"])));
    }

    [TestMethod]
    public void CookBatches_Stops_WhenSpicesRunOut() {
        var k = new Kitchen();
        k.StockSpices([("salt", 50.0)]);
        k.StockEquipment(["fryer"]);

        var recipe = TestRecipe(new() { ["salt"] = 20.0 }, ["fryer"]);
        var produced = new List<ChickenBatch>();

        foreach (var b in k.CookBatches(recipe, id: 100, pieces: 8, count: 10, methEvery: 3)) {
            produced.Add(b);
        }

        Assert.HasCount(2, produced);
    }

    [TestMethod]
    public void CookBatches_Stops_WhenAcceptReturnsFalse() {
        var k = new Kitchen();
        k.StockSpices([("salt", 100.0)]);
        k.StockEquipment(["fryer"]);

        var lukewarm = new Recipe("lukewarm", 66.0, new Dictionary<string, double> { ["salt"] = 10.0 }, ["fryer"]);
        var produced = new List<ChickenBatch>();

        foreach (var b in k.CookBatches(lukewarm, id: 100, pieces: 8, count: 5, methEvery: 3)) {
            produced.Add(b);
        }

        Assert.HasCount(2, produced);
        Assert.AreEqual(100, produced[0].Id);
        Assert.AreEqual(101, produced[1].Id);
        Assert.AreEqual(100, k.ServeNext()!.Id);
        Assert.AreEqual(101, k.ServeNext()!.Id);
        Assert.IsNull(k.ServeNext());
        Assert.IsTrue(k.CanCook(TestRecipe(new() { ["salt"] = 70.0 }, ["fryer"])));
        Assert.IsFalse(k.CanCook(TestRecipe(new() { ["salt"] = 70.1 }, ["fryer"])));
    }

    [TestMethod]
    public void CookBatches_YieldsLazily_SetsPieces_AndEnqueuesToReady() {
        var k = new Kitchen();
        k.StockSpices([("salt", 100.0)]);
        k.StockEquipment(["fryer"]);

        var recipe = TestRecipe(new() { ["salt"] = 20.0 }, ["fryer"]);
        var stream = k.CookBatches(recipe, id: 50, pieces: 12, count: 4, methEvery: 2);

        Assert.IsNull(k.ServeNext());
        Assert.IsTrue(k.CanCook(TestRecipe(new() { ["salt"] = 100.0 }, ["fryer"])));

        ChickenBatch? first = null;
        foreach (var b in stream) {
            first = b;
            break;
        }

        Assert.IsNotNull(first);
        Assert.AreEqual(50, first.Id);
        Assert.AreEqual(12, first.Pieces);
        Assert.IsTrue(first.HasMeth);
        Assert.AreEqual(50, k.ServeNext()!.Id);
        Assert.IsNull(k.ServeNext());
        Assert.IsTrue(k.CanCook(TestRecipe(new() { ["salt"] = 80.0 }, ["fryer"])));
        Assert.IsFalse(k.CanCook(TestRecipe(new() { ["salt"] = 80.1 }, ["fryer"])));
    }
}
#endif