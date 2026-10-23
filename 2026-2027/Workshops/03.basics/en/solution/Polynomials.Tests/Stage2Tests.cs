#if STAGE2
using System.Globalization;

namespace Polynomials.Tests;

[TestClass]
public class Stage2Tests
{
    private const float Delta = 1e-4f;

    [TestInitialize]
    public void SetUp()
    {
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;
    }

    [TestMethod]
    public void Polynomial_ParameterlessConstructor_EmptyPolynomial()
    {
        var p = new Polynomial();
        Assert.AreEqual(0, p.Degree);
        Assert.AreEqual(0f, p.Evaluate(5f), Delta);
        Assert.AreEqual("0", p.ToString());
    }

    [TestMethod]
    public void Polynomial_ConstructorWithTerms_InitializesPolynomial()
    {
        var p = new Polynomial(new Term(2f, 2), new Term(3f, 1));
        Assert.AreEqual(2, p.Degree);
        Assert.AreEqual(14f, p.Evaluate(2f), Delta);
    }

    [TestMethod]
    public void Polynomial_Add_DifferentPowers_AppendsTerm()
    {
        var p = new Polynomial();
        p.Add(new Term(5f, 3));
        p.Add(new Term(2f, 1));
        Assert.AreEqual(3, p.Degree);
        Assert.AreEqual(44f, p.Evaluate(2f), Delta);
    }

    [TestMethod]
    public void Polynomial_Add_SamePower_MergesCoefficients()
    {
        var p = new Polynomial(new Term(2f, 2));
        p.Add(new Term(3f, 2));
        Assert.AreEqual(2, p.Degree);
        Assert.AreEqual(20f, p.Evaluate(2f), Delta);
        Assert.AreEqual("5x^2", p.ToString());
    }

    [TestMethod]
    public void Polynomial_Degree_HighestPowerAmongNonZeroTerms()
    {
        var p = new Polynomial(new Term(0f, 5), new Term(3f, 2), new Term(1f, 1));
        Assert.AreEqual(2, p.Degree);
    }

    [TestMethod]
    public void Polynomial_Degree_AllZeroTerms_ReturnsZero()
    {
        var p = new Polynomial(new Term(0f, 3), new Term(0f, 1));
        Assert.AreEqual(0, p.Degree);
    }

    [TestMethod]
    public void Polynomial_Addition_AddsTwoPolynomialsAndMerges()
    {
        var a = new Polynomial(new Term(2f, 2), new Term(3f, 1));
        var b = new Polynomial(new Term(4f, 2), new Term(5f, 0));
        var sum = a + b;
        Assert.AreEqual(2, sum.Degree);
        // at x = 2: (2+4)*4 + 3*2 + 5 = 24 + 6 + 5 = 35
        Assert.AreEqual(35f, sum.Evaluate(2f), Delta);
    }

    [TestMethod]
    public void Polynomial_ToString_JoinsTermsWithPlus()
    {
        var p = new Polynomial(new Term(3f, 2), new Term(2f, 1));
        Assert.AreEqual("3x^2 + 2x^1", p.ToString());
    }
}
#endif
