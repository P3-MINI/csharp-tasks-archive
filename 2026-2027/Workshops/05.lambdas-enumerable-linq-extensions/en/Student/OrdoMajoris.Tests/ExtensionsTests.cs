// #define STAGE2

namespace OrdoMajoris.Tests;

#if STAGE2
[TestClass]
public sealed class ExtensionsTests {
    [TestMethod]
    public void Interleave_EqualLength() {
        var a = new[] { 1, 3, 5 };
        var b = new[] { 2, 4, 6 };
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4, 5, 6 }, a.Interleave(b).ToArray());
    }

    [TestMethod]
    public void Interleave_FirstLonger() {
        var a = new[] { 1, 3, 5, 7, 9 };
        var b = new[] { 2, 4 };
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 4, 5, 7, 9 }, a.Interleave(b).ToArray());
    }

    [TestMethod]
    public void Interleave_SecondLonger() {
        var a = new[] { "a", "b" };
        var b = new[] { "1", "2", "3", "4" };
        CollectionAssert.AreEqual(new[] { "a", "1", "b", "2", "3", "4" }, a.Interleave(b).ToArray());
    }

    [TestMethod]
    public void Interleave_OneEmpty() {
        var a = Array.Empty<int>();
        var b = new[] { 10, 20, 30 };
        CollectionAssert.AreEqual(new[] { 10, 20, 30 }, a.Interleave(b).ToArray());
        CollectionAssert.AreEqual(new[] { 10, 20, 30 }, b.Interleave(a).ToArray());
    }

    [TestMethod]
    public void Interleave_BothEmpty() {
        var a = Array.Empty<int>();
        var b = Array.Empty<int>();
        Assert.IsEmpty(a.Interleave(b).ToArray());
    }

    [TestMethod]
    public void Interleave_IsLazy_YieldsIncrementally() {
        static IEnumerable<int> FirstThreeThenThrow() {
            yield return 1;
            yield return 2;
            yield return 3;
            throw new InvalidOperationException("Should not be reached when taking 4 elements.");
        }

        var result = FirstThreeThenThrow().Interleave([10, 20, 30]).Take(4).ToArray();
        CollectionAssert.AreEqual(new[] { 1, 10, 2, 20 }, result);
    }

    [TestMethod]
    public void Median_OddLength() {
        Assert.AreEqual(3, new[] { 1, 2, 3, 4, 5 }.Median(x => x));
    }

    [TestMethod]
    public void Median_OddLength_Unsorted() {
        Assert.AreEqual(3, new[] { 5, 1, 4, 2, 3 }.Median(x => x));
    }

    [TestMethod]
    public void Median_EvenLength_Int_Truncates() {
        // (2 + 3) / 2 = 2 (int)
        Assert.AreEqual(2, new[] { 1, 2, 3, 4 }.Median(x => x));
    }

    [TestMethod]
    public void Median_EvenLength_Double() {
        // (2.0 + 3.0) / 2 = 2.5
        Assert.AreEqual(2.5, new[] { 1.0, 2.0, 3.0, 4.0 }.Median(x => x), 1e-9);
    }

    [TestMethod]
    public void Median_ReverseSorted() {
        Assert.AreEqual(3, new[] { 5, 4, 3, 2, 1 }.Median(x => x));
        Assert.AreEqual(2.5, new[] { 4.0, 3.0, 2.0, 1.0 }.Median(x => x), 1e-9);
    }

    [TestMethod]
    public void Median_TwoElements() {
        Assert.AreEqual(15, new[] { 10, 20 }.Median(x => x));
        Assert.AreEqual(1.5, new[] { 2.0, 1.0 }.Median(x => x), 1e-9);
    }

    [TestMethod]
    public void Median_Duplicates() {
        Assert.AreEqual(2, new[] { 5, 2, 10, 2, 2 }.Median(x => x));
        Assert.AreEqual(3, new[] { 9, 3, 1, 3 }.Median(x => x));
    }

    [TestMethod]
    public void Median_NegativeValues() {
        Assert.AreEqual(-3, new[] { -10, -1, -5, 0, -3 }.Median(x => x));
        Assert.AreEqual(-1.5, new[] { -4.0, 3.0, -2.0, -1.0 }.Median(x => x), 1e-9);
    }

    [TestMethod]
    public void Median_SingleElement() {
        Assert.AreEqual(42, new[] { 42 }.Median(x => x));
    }

    [TestMethod]
    public void Median_Empty_Throws() {
        Assert.Throws<InvalidOperationException>(() => Array.Empty<int>().Median(x => x));
    }

    [TestMethod]
    public void Median_DoesNotMutateSource() {
        var data = new[] { 3, 1, 4, 1, 5 };
        _ = data.Median(x => x);
        CollectionAssert.AreEqual(new[] { 3, 1, 4, 1, 5 }, data);
    }

    [TestMethod]
    public void Median_Decimal() {
        Assert.AreEqual(3.0m, new[] { 1.0m, 2.0m, 3.0m, 4.0m, 5.0m }.Median(x => x));
    }

    [TestMethod]
    public void Median_IgnoresSingleOutlier_OddLength() {
        Assert.AreEqual(3, new[] { 1, 2, 3, 4, 100 }.Median(x => x));
    }

    [TestMethod]
    public void Median_IgnoresSingleOutlier_EvenLength() {
        Assert.AreEqual(3.5, new[] { 1.0, 2.0, 3.0, 4.0, 5.0, 100.0 }.Median(x => x), 1e-9);
    }

    [TestMethod]
    public void Median_ChurchTestimonies_IsLow() {
        var median = Database.EcclesiarchyTestimonies.Median(t => t.HeresyPercent);

        Assert.IsLessThan(20.0, median, $"Church median {median}% should be low.");
    }

    [TestMethod]
    public void Median_GovernorTestimonies_IsHigh() {
        var median = Database.GovernorTestimonies.Median(t => t.HeresyPercent);

        Assert.IsGreaterThan(40.0, median, $"Governor median {median}% should be high.");
    }

    [TestMethod]
    public void Median_PopulationHeresy_IsLow() {
        var population = Database.PopulationHeresy;
        var median = population.Median(x => x);

        Assert.IsLessThan(10.0, median, $"Population median {median}% should be low.");
    }

    [TestMethod]
    public void Median_ChurchVsGovernor_GovernorIsHigher() {
        var churchMedian = Database.EcclesiarchyTestimonies.Median(t => t.HeresyPercent);
        var governorMedian = Database.GovernorTestimonies.Median(t => t.HeresyPercent);

        Assert.IsGreaterThan(churchMedian, governorMedian, $"Governor median ({governorMedian}%) should be higher than Church median ({churchMedian}%).");
    }

    [TestMethod]
    public void Median_CombinedTestimonies_IsBetweenChurchAndGovernor() {
        var churchMedian = Database.EcclesiarchyTestimonies.Median(t => t.HeresyPercent);
        var governorMedian = Database.GovernorTestimonies.Median(t => t.HeresyPercent);
        var combinedMedian = Database.EcclesiarchyTestimonies
            .Concat(Database.GovernorTestimonies)
            .Median(t => t.HeresyPercent);

        Assert.IsGreaterThan(churchMedian, combinedMedian, "Combined median should be above Church median.");
        Assert.IsLessThan(governorMedian, combinedMedian, "Combined median should be below Governor median.");
    }
}
#endif
