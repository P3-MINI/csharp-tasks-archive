#if STAGE3
using System.Globalization;

namespace Polynomials.Tests;

[TestClass]
public class Stage3Tests
{
    private const float Delta = 1e-3f;

    [TestInitialize]
    public void SetUp()
    {
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;
    }

    [TestMethod]
    public void FindRoot_QuadraticWithRootInInterval_FindsExpectedRoot()
    {
        // f(x) = x^2 - 4 with root at 2 in [0, 3]
        var p = new Polynomial(new Term(1f, 2), new Term(-4f, 0));
        bool found = p.FindRoot(0f, 3f, out float root, 1e-4f);

        Assert.IsTrue(found);
        Assert.AreEqual(2.0f, root, Delta);
    }

    [TestMethod]
    public void FindRoot_LinearPolynomial_FindsExpectedRoot()
    {
        // f(x) = 2x - 6 with root at 3 in [0, 5]
        var p = new Polynomial(new Term(2f, 1), new Term(-6f, 0));
        bool found = p.FindRoot(0f, 5f, out float root, 1e-4f);

        Assert.IsTrue(found);
        Assert.AreEqual(3.0f, root, Delta);
    }

    [TestMethod]
    public void FindRoot_ReversedInterval_FindsExpectedRoot()
    {
        // f(x) = x^2 - 4 with root at 2 in [3, 0]
        var p = new Polynomial(new Term(1f, 2), new Term(-4f, 0));
        bool found = p.FindRoot(3f, 0f, out float root, 1e-4f);

        Assert.IsTrue(found);
        Assert.AreEqual(2.0f, root, Delta);
    }

    [TestMethod]
    public void FindRoot_SameSignAtEndpoints_ReturnsFalse()
    {
        // f(x) = x^2 - 4 in [3, 5], f(3) = 5, f(5) = 21 (both positive)
        var p = new Polynomial(new Term(1f, 2), new Term(-4f, 0));
        bool found = p.FindRoot(3f, 5f, out float root);

        Assert.IsFalse(found);
    }

    [TestMethod]
    public void FindRoot_CubicPolynomial_FindsExpectedRoot()
    {
        // f(x) = x^3 - 2 with root at cbrt(2) ~ 1.259921 in [1, 2]
        var p = new Polynomial(new Term(1f, 3), new Term(-2f, 0));
        bool found = p.FindRoot(1f, 2f, out float root, 1e-4f);

        Assert.IsTrue(found);
        float expected = MathF.Cbrt(2f);
        Assert.AreEqual(expected, root, Delta);
    }
}
#endif
