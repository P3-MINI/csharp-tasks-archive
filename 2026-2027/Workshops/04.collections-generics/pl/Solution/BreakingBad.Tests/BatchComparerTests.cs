namespace BreakingBad.Tests;

#if STAGE3
[TestClass]
public class BatchComparerTests {
    private static readonly BatchComparer _cmp = new();

    [TestMethod]
    public void Compare_HigherPurity_SortsFirst() {
        var low = new Batch("Walter", 90.0, 500);
        var high = new Batch("Walter", 99.0, 500);

        Assert.IsLessThan(0, _cmp.Compare(high, low));
        Assert.IsGreaterThan(0, _cmp.Compare(low, high));
    }

    [TestMethod]
    public void Compare_SamePurity_CookAscending() {
        var jesse = new Batch("Jesse", 99.0, 500);
        var walter = new Batch("Walter", 99.0, 500);

        Assert.IsLessThan(0, _cmp.Compare(jesse, walter));
        Assert.IsGreaterThan(0, _cmp.Compare(walter, jesse));
    }

    [TestMethod]
    public void Compare_SamePurityAndCook_GramsDescending() {
        var small = new Batch("Walter", 99.0, 300);
        var big = new Batch("Walter", 99.0, 500);

        Assert.IsLessThan(0, _cmp.Compare(big, small));
        Assert.IsGreaterThan(0, _cmp.Compare(small, big));
    }

    [TestMethod]
    public void Compare_Purity_Beats_CookAndGrams() {
        var purerWalterSmall = new Batch("Walter", 99.0, 100);
        var lessPureJesseBig = new Batch("Jesse", 95.0, 500);

        Assert.IsLessThan(0, _cmp.Compare(purerWalterSmall, lessPureJesseBig));
        Assert.IsGreaterThan(0, _cmp.Compare(lessPureJesseBig, purerWalterSmall));
    }

    [TestMethod]
    public void Compare_Cook_Beats_Grams() {
        var jesseSmall = new Batch("Jesse", 99.0, 100);
        var walterBig = new Batch("Walter", 99.0, 500);

        Assert.IsLessThan(0, _cmp.Compare(jesseSmall, walterBig));
        Assert.IsGreaterThan(0, _cmp.Compare(walterBig, jesseSmall));
    }

    [TestMethod]
    public void Compare_NonNullSortsBeforeNull() {
        var b = new Batch("Walter", 99.0, 500);

        Assert.IsLessThan(0, _cmp.Compare(b, null));
        Assert.IsGreaterThan(0, _cmp.Compare(null, b));
        Assert.AreEqual(0, _cmp.Compare(null, null));
    }

    [TestMethod]
    public void Compare_WithSelf_IsZero() {
        var b = new Batch("Walter", 99.0, 500);
        Assert.AreEqual(0, _cmp.Compare(b, b));
    }

    [TestMethod]
    public void Compare_SortingProducesExpectedOrder() {
        var batches = new List<Batch>
        {
            new("Jesse",  85.0, 300),
            new("Walter", 99.0, 500),
            new("Jesse",  96.1, 200),
            new("Walter", 99.1, 400),
            new("Walter", 96.1, 500),
        };

        batches.Sort(_cmp);

        Assert.AreEqual("Walter", batches[0].Cook);
        Assert.AreEqual(99.1, batches[0].Purity, 1e-9);
        Assert.AreEqual(400, batches[0].Grams);

        Assert.AreEqual("Walter", batches[1].Cook);
        Assert.AreEqual(99.0, batches[1].Purity, 1e-9);
        Assert.AreEqual(500, batches[1].Grams);

        Assert.AreEqual("Jesse", batches[2].Cook);
        Assert.AreEqual(96.1, batches[2].Purity, 1e-9);
        Assert.AreEqual(200, batches[2].Grams);

        Assert.AreEqual("Walter", batches[3].Cook);
        Assert.AreEqual(96.1, batches[3].Purity, 1e-9);
        Assert.AreEqual(500, batches[3].Grams);

        Assert.AreEqual("Jesse", batches[4].Cook);
        Assert.AreEqual(85.0, batches[4].Purity, 1e-9);
        Assert.AreEqual(300, batches[4].Grams);
    }
}
#endif