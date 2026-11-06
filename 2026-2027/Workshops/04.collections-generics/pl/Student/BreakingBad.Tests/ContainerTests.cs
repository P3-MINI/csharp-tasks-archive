namespace BreakingBad.Tests;

#if STAGE1
[TestClass]
public class ContainerTests {
    [TestMethod]
    public void Add_ReturnsTrue_UntilFull() {
        var c = new Container<string>(3);
        Assert.IsTrue(c.Add("a"));
        Assert.IsTrue(c.Add("b"));
        Assert.IsTrue(c.Add("c"));
        Assert.AreEqual(3, c.Count);
        Assert.IsTrue(c.IsFull);
        Assert.IsFalse(c.IsEmpty);
    }

    [TestMethod]
    public void Add_ReturnsFalse_WhenFull() {
        var c = new Container<string>(1);
        Assert.IsTrue(c.Add("a"));
        Assert.IsFalse(c.Add("b"));
        Assert.AreEqual(1, c.Count);
        Assert.AreEqual("a", c.Check());
    }

    [TestMethod]
    public void Take_ReturnsNull_WhenEmpty() {
        var c = new Container<string>(2);
        Assert.IsNull(c.Take());
    }

    [TestMethod]
    public void Take_PopsLifo() {
        var c = new Container<string>(3) { "a", "b", "c" };
        Assert.AreEqual("c", c.Take());
        Assert.AreEqual("b", c.Take());
        Assert.AreEqual("a", c.Take());
        Assert.IsNull(c.Take());
        Assert.AreEqual(0, c.Count);
        Assert.IsTrue(c.IsEmpty);
    }

    [TestMethod]
    public void Check_ReturnsTop_WithoutRemoving() {
        var c = new Container<string>(2) { "a", "b" };
        Assert.AreEqual("b", c.Check());
        Assert.AreEqual("b", c.Check());
        Assert.AreEqual(2, c.Count);
    }

    [TestMethod]
    public void Check_ReturnsNull_WhenEmpty() {
        var c = new Container<string>(2);
        Assert.IsNull(c.Check());
    }

    [TestMethod]
    public void Add_AfterTake_ReusesFreedCapacity() {
        var c = new Container<string>(2);
        Assert.IsTrue(c.Add("a"));
        Assert.IsTrue(c.Add("b"));
        Assert.IsFalse(c.Add("x"));

        Assert.AreEqual("b", c.Take());
        Assert.IsTrue(c.Add("c"));
        Assert.AreEqual(2, c.Count);

        var seen = new List<string>();
        foreach (var x in c) {
            seen.Add(x);
        }
        CollectionAssert.AreEqual(new[] { "c", "a" }, seen);
    }

    [TestMethod]
    public void Enumeration_IsTopFirst() {
        var c = new Container<string>(3) { "a", "b", "c" };
        var seen = new List<string>();
        foreach (var x in c) {
            seen.Add(x);
        }
        CollectionAssert.AreEqual(new[] { "c", "b", "a" }, seen);
        Assert.AreEqual(3, c.Count);
        Assert.AreEqual("c", c.Check());
    }

    [TestMethod]
    public void Enumeration_OnEmpty_YieldsNothing() {
        var c = new Container<string>(2);
        var seen = new List<string>();
        foreach (var x in c) {
            seen.Add(x);
        }
        Assert.IsEmpty(seen);
    }
}
#endif