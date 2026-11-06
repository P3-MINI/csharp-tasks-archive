namespace LosPollosHermanos.Tests;

#if STAGE1
[TestClass]
public class LineTests {
    [TestMethod]
    public void Add_ReturnsTrue_UntilCapacity() {
        var line = new Line<string>(3);
        Assert.IsTrue(line.Add("a"));
        Assert.IsTrue(line.Add("b"));
        Assert.IsTrue(line.Add("c"));
        Assert.AreEqual(3, line.Count);
        Assert.IsTrue(line.IsFull);
        Assert.IsFalse(line.IsEmpty);
    }

    [TestMethod]
    public void Add_ReturnsFalse_WhenFull() {
        var line = new Line<string>(1);
        Assert.IsTrue(line.Add("a"));
        Assert.IsFalse(line.Add("b"));
        Assert.AreEqual(1, line.Count);
        Assert.AreEqual("a", line.Check());
    }

    [TestMethod]
    public void Take_ReturnsNull_WhenEmpty() {
        var line = new Line<string>(2);
        Assert.IsNull(line.Take());
    }

    [TestMethod]
    public void Take_PopsFifo() {
        var line = new Line<string>(3);
        line.Add("a");
        line.Add("b");
        line.Add("c");

        Assert.AreEqual("a", line.Take());
        Assert.AreEqual("b", line.Take());
        Assert.AreEqual("c", line.Take());
        Assert.IsNull(line.Take());
        Assert.AreEqual(0, line.Count);
        Assert.IsTrue(line.IsEmpty);
    }

    [TestMethod]
    public void Check_ReturnsFront_WithoutRemoving() {
        var line = new Line<string>(2);
        line.Add("a");
        line.Add("b");

        Assert.AreEqual("a", line.Check());
        Assert.AreEqual("a", line.Check());
        Assert.AreEqual(2, line.Count);
    }

    [TestMethod]
    public void Check_ReturnsNull_WhenEmpty() {
        var line = new Line<string>(2);
        Assert.IsNull(line.Check());
    }

    [TestMethod]
    public void Add_AfterTake_ReusesFreedCapacity() {
        var line = new Line<string>(2);
        Assert.IsTrue(line.Add("a"));
        Assert.IsTrue(line.Add("b"));
        Assert.IsFalse(line.Add("x"));

        Assert.AreEqual("a", line.Take());
        Assert.IsTrue(line.Add("c"));
        Assert.AreEqual(2, line.Count);

        var seen = new List<string>();
        foreach (var x in line) {
            seen.Add(x);
        }
        CollectionAssert.AreEqual(new[] { "b", "c" }, seen);
    }

    [TestMethod]
    public void Enumeration_IsFrontFirst() {
        var line = new Line<string>(3);
        line.Add("a");
        line.Add("b");
        line.Add("c");

        var seen = new List<string>();
        foreach (var x in line)
            seen.Add(x);

        CollectionAssert.AreEqual(new[] { "a", "b", "c" }, seen);
        Assert.AreEqual(3, line.Count);
        Assert.AreEqual("a", line.Check());
    }

    [TestMethod]
    public void Enumeration_OnEmpty_YieldsNothing() {
        var line = new Line<string>(2);
        var seen = new List<string>();
        foreach (var x in line)
            seen.Add(x);
        Assert.IsEmpty(seen);
    }
}
#endif