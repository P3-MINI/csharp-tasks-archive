namespace LosPollosHermanos.Tests;

#if STAGE3
[TestClass]
public class BatchComparerTests {
    private static ChickenBatch TestBatch(int pieces, double temp, bool meth) => new(0, pieces, temp, meth);
    private static readonly BatchComparer _cmp = new();

    [TestMethod]
    public void Compare_Safe_Beats_Unsafe() {
        var safe = TestBatch(8, 80.0, false);
        var unsafeB = TestBatch(8, 60.0, false);

        Assert.IsLessThan(0, _cmp.Compare(safe, unsafeB), "safe should sort first");
        Assert.IsGreaterThan(0, _cmp.Compare(unsafeB, safe));
    }

    [TestMethod]
    public void Compare_SafeBatches_Tie_EvenWithDifferentTemperatures() {
        var justSafe = TestBatch(8, 75.0, false);
        var alsoSafe = TestBatch(8, 200.0, false);
        Assert.AreEqual(0, _cmp.Compare(justSafe, alsoSafe),
            "both safe, same pieces, same meth");
    }

    [TestMethod]
    public void Compare_BarelyUnsafe_LosesTo_VeryHot() {
        var barelyUnsafe = TestBatch(8, 74.5, false);
        var veryHot = TestBatch(8, 200.0, false);
        Assert.IsLessThan(0, _cmp.Compare(veryHot, barelyUnsafe));
    }

    [TestMethod]
    public void Compare_ExactlyAtSafeTemperature_IsSafe() {
        var at75 = TestBatch(8, 75.0, false);
        var just74 = TestBatch(8, 74.999, false);
        Assert.IsLessThan(0, _cmp.Compare(at75, just74), "75.0 is the boundary");
    }

    [TestMethod]
    public void Compare_Meth_Beats_NoMeth_WhenSafetyMatches() {
        var withMeth = TestBatch(8, 80.0, true);
        var noMeth = TestBatch(8, 80.0, false);

        Assert.IsLessThan(0, _cmp.Compare(withMeth, noMeth), "meth sorts first on tie");
        Assert.IsGreaterThan(0, _cmp.Compare(noMeth, withMeth));
    }

    [TestMethod]
    public void Compare_UnsafeMeth_LosesTo_SafeNoMeth() {
        var unsafeMeth = TestBatch(8, 60.0, true);
        var safeNoMeth = TestBatch(8, 80.0, false);

        Assert.IsLessThan(0, _cmp.Compare(safeNoMeth, unsafeMeth));
        Assert.IsGreaterThan(0, _cmp.Compare(unsafeMeth, safeNoMeth));
    }

    [TestMethod]
    public void Compare_SmallerPieces_Beat_BiggerPieces_WhenSafetyAndMethMatch() {
        var small = TestBatch(4, 80.0, false);
        var big = TestBatch(10, 80.0, false);
        Assert.IsLessThan(0, _cmp.Compare(small, big), "smaller pieces sort first");
        Assert.IsGreaterThan(0, _cmp.Compare(big, small));
    }

    [TestMethod]
    public void Compare_Meth_Beats_Pieces() {
        var bigMeth = TestBatch(12, 80.0, true);
        var smallNoMeth = TestBatch(4, 80.0, false);
        Assert.IsLessThan(0, _cmp.Compare(bigMeth, smallNoMeth));
        Assert.IsGreaterThan(0, _cmp.Compare(smallNoMeth, bigMeth));
    }

    [TestMethod]
    public void Compare_WithSelf_IsZero() {
        var b = TestBatch(8, 80.0, false);
        Assert.AreEqual(0, _cmp.Compare(b, b));
    }

    [TestMethod]
    public void Compare_NonNullSortsBeforeNull() {
        var b = TestBatch(8, 80.0, false);
        Assert.IsLessThan(0, _cmp.Compare(b, null));
        Assert.IsGreaterThan(0, _cmp.Compare(null, b));
        Assert.AreEqual(0, _cmp.Compare(null, null));
    }

    [TestMethod]
    public void Compare_Sort_ProducesExpectedOrder() {
        var batches = new List<ChickenBatch>
        {
            TestBatch(6,  90.0,  false),
            TestBatch(10, 200.0, true),
            TestBatch(4,  60.0,  true),
            TestBatch(8,  74.5,  false),
            TestBatch(10, 80.0,  true),
            TestBatch(10, 80.0,  false),
        };

        batches.Sort(_cmp);

        Assert.AreEqual(10, batches[0].Pieces);
        Assert.IsTrue(batches[0].HasMeth);

        Assert.AreEqual(10, batches[1].Pieces);
        Assert.IsTrue(batches[1].HasMeth);

        Assert.AreEqual(6, batches[2].Pieces);
        Assert.IsFalse(batches[2].HasMeth);

        Assert.AreEqual(10, batches[3].Pieces);
        Assert.IsFalse(batches[3].HasMeth);

        Assert.AreEqual(4, batches[4].Pieces);
        Assert.AreEqual(60.0, batches[4].Temperature, 1e-9);
        Assert.IsTrue(batches[4].HasMeth);

        Assert.AreEqual(8, batches[5].Pieces);
        Assert.AreEqual(74.5, batches[5].Temperature, 1e-9);
        Assert.IsFalse(batches[5].HasMeth);
    }
}
#endif