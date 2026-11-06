// #define STAGE2

namespace AdeptusMechanicus.Tests;

#if STAGE2
[TestClass]
public sealed class ExtensionsTests {
    [TestMethod]
    public void Invert_UniqueValues() {
        var source = new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 };
        var inverted = source.Invert();
        CollectionAssert.AreEqual(new[] { "a" }, inverted[1]);
        CollectionAssert.AreEqual(new[] { "b" }, inverted[2]);
        CollectionAssert.AreEqual(new[] { "c" }, inverted[3]);
    }

    [TestMethod]
    public void Invert_DuplicateValues_GroupedInList() {
        var source = new Dictionary<string, int> { ["a"] = 1, ["b"] = 1, ["c"] = 2 };
        var inverted = source.Invert();
        CollectionAssert.AreEqual(new[] { "a", "b" }, inverted[1]);
        CollectionAssert.AreEqual(new[] { "c" }, inverted[2]);
    }

    [TestMethod]
    public void Invert_AllSameValue() {
        var source = new Dictionary<string, int> { ["a"] = 1, ["b"] = 1, ["c"] = 1 };
        var inverted = source.Invert();
        Assert.HasCount(1, inverted);
        CollectionAssert.AreEqual(new[] { "a", "b", "c" }, inverted[1]);
    }

    [TestMethod]
    public void Invert_PreservesKeyEncounterOrder() {
        var source = new Dictionary<string, int> { ["z"] = 1, ["a"] = 2, ["m"] = 1, ["b"] = 1 };
        var inverted = source.Invert();
        CollectionAssert.AreEqual(new[] { "z", "m", "b" }, inverted[1]);
        CollectionAssert.AreEqual(new[] { "a" }, inverted[2]);
    }

    [TestMethod]
    public void Invert_Empty_ReturnsEmpty() {
        var inverted = new Dictionary<string, int>().Invert();
        Assert.IsEmpty(inverted);
    }

    [TestMethod]
    public void Invert_DoesNotMutateSource() {
        var source = new Dictionary<string, int> { ["a"] = 1 };
        _ = source.Invert();
        Assert.HasCount(1, source);
        Assert.AreEqual(1, source["a"]);
    }

    [TestMethod]
    public void Invert_IntKeys_StringValues() {
        var source = new Dictionary<int, string> { [1] = "x", [2] = "y", [3] = "x" };
        var inverted = source.Invert();
        CollectionAssert.AreEqual(new[] { 1, 3 }, inverted["x"]);
        CollectionAssert.AreEqual(new[] { 2 }, inverted["y"]);
    }

    [TestMethod]
    public void Invert_CogitatorReadings_GroupsByAnomaly() {
        var readings = new Dictionary<int, double> {
            [1] = 20.1,
            [2] = 20.3,
            [3] = 20.1,
            [4] = 99.9,
            [5] = 20.2
        };
        var byReading = readings.Invert();
        CollectionAssert.AreEqual(new[] { 1, 3 }, byReading[20.1]);
        CollectionAssert.AreEqual(new[] { 4 }, byReading[99.9]);
    }

    [TestMethod]
    public void Invert_DatabaseReadings_PreservesEncounterOrder() {
        var byReading = Database.Readings.Invert();
        Assert.HasCount(8, byReading);
        CollectionAssert.AreEqual(new[] { 1, 3, 10, 15 }, byReading[20.1m]);
        CollectionAssert.AreEqual(new[] { 5, 8, 12, 16 }, byReading[20.2m]);
    }

    [TestMethod]
    public void IQR_FourElements_MinimumValidLength() {
        // [10, 20, 30, 40] -> n=4, n/4=1, 3n/4=3 => Q1=20, Q3=40 => IQR=20
        Assert.AreEqual(20, new[] { 10, 20, 30, 40 }.IQR(x => x));
    }

    [TestMethod]
    public void IQR_EightElements() {
        // [1..8] → n=8, n/4=2, 3n/4=6 => Q1=3, Q3=7 => IQR=4
        Assert.AreEqual(4, new[] { 1, 2, 3, 4, 5, 6, 7, 8 }.IQR(x => x));
    }

    [TestMethod]
    public void IQR_FiveElements() {
        // [1..5] → n=5, n/4=1, 3n/4=3 => Q1=2, Q3=4 => IQR=2
        Assert.AreEqual(2, new[] { 1, 2, 3, 4, 5 }.IQR(x => x));
    }

    [TestMethod]
    public void IQR_SixElements() {
        // [10, 20, 30, 40, 50, 60] -> n=6, n/4=1 (20), 3*n/4=4 (50) => IQR=30
        Assert.AreEqual(30, new[] { 10, 20, 30, 40, 50, 60 }.IQR(x => x));
    }

    [TestMethod]
    public void IQR_SevenElements() {
        // [10, 20, 30, 40, 50, 60, 70] -> n=7, n/4=1 (20), 3*n/4=5 (60) => IQR=40
        Assert.AreEqual(40, new[] { 10, 20, 30, 40, 50, 60, 70 }.IQR(x => x));
    }

    [TestMethod]
    public void IQR_Unsorted() {
        Assert.AreEqual(4, new[] { 8, 2, 6, 4, 1, 7, 3, 5 }.IQR(x => x));
    }

    [TestMethod]
    public void IQR_NegativeValues() {
        // sorted: [-40, -30, -20, -10, 0, 10] -> n=6, Q1=-30, Q3=0 => IQR=30
        Assert.AreEqual(30, new[] { -40, -10, -30, -20, 0, 10 }.IQR(x => x));
    }

    [TestMethod]
    public void IQR_ConstantData_IsZero() {
        Assert.AreEqual(0, new[] { 5, 5, 5, 5 }.IQR(x => x));
    }

    [TestMethod]
    public void IQR_Empty_Throws() {
        Assert.Throws<InvalidOperationException>(() => Array.Empty<int>().IQR(x => x));
    }

    [TestMethod]
    public void IQR_SingleElement_Throws() {
        Assert.Throws<InvalidOperationException>(() => new[] { 1 }.IQR(x => x));
    }

    [TestMethod]
    public void IQR_TwoElements_Throws() {
        Assert.Throws<InvalidOperationException>(() => new[] { 1, 2 }.IQR(x => x));
    }

    [TestMethod]
    public void IQR_ThreeElements_Throws() {
        Assert.Throws<InvalidOperationException>(() => new[] { 1, 2, 3 }.IQR(x => x));
    }

    [TestMethod]
    public void IQR_DoesNotMutate_Source() {
        var data = new[] { 3, 1, 4, 2 };
        _ = data.IQR(x => x);
        CollectionAssert.AreEqual(new[] { 3, 1, 4, 2 }, data);
    }

    [TestMethod]
    public void IQR_DoubleData() {
        Assert.AreEqual(4.0, new[] { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0 }.IQR(x => x), 1e-9);
    }

    [TestMethod]
    public void IQR_Eliminates_Anomaly() {
        var readings = new[] { 20.1, 20.3, 20.1, 99.9, 20.2 };
        var iqr = readings.IQR(x => x);
        Assert.IsLessThan(1.0, iqr);
    }

    [TestMethod]
    public void IQR_Stable_WithoutOutlier() {
        var clean = new[] { 20.1, 20.2, 20.3, 20.1, 20.2 };
        var iqr = clean.IQR(x => x);
        Assert.IsLessThan(1.0, iqr);
    }

    [TestMethod]
    public void IQR_DatabaseReadings_WithSelector() {
        var iqr = Database.Readings.IQR(kv => kv.Value);
        Assert.AreEqual(0.3m, iqr);
    }

    [TestMethod]
    public void IQR_ImplantOrders_ByCost() {
        var iqr = Database.ImplantOrders.IQR(o => o.Cost);
        Assert.AreEqual(410m, iqr);
    }
}
#endif
