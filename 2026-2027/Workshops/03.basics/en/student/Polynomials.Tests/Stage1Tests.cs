#if STAGE1
using System.Globalization;

namespace Polynomials.Tests;

[TestClass]
public class Stage1Tests
{
    private const float Delta = 1e-4f;

    [TestInitialize]
    public void SetUp()
    {
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;
    }

    [TestMethod]
    public void Term_ConstructorAndProperties_SetsValuesCorrectly()
    {
        var t = new Term(3.5f, 2);
        Assert.AreEqual(3.5f, t.Coeff, Delta);
        Assert.AreEqual(2, t.Power);
        Assert.AreEqual(2, t.Degree);
    }

    [TestMethod]
    public void Term_Evaluate_CalculatesCorrectValue()
    {
        var t = new Term(3f, 2);
        Assert.AreEqual(12f, t.Evaluate(2f), Delta);
        Assert.AreEqual(27f, t.Evaluate(3f), Delta);
        Assert.AreEqual(0f, t.Evaluate(0f), Delta);
    }

    [TestMethod]
    public void Term_Evaluate_ZeroPower_ReturnsCoeff()
    {
        var t = new Term(5f, 0);
        Assert.AreEqual(5f, t.Evaluate(10f), Delta);
        Assert.AreEqual(5f, t.Evaluate(0f), Delta);
    }

    [TestMethod]
    public void Term_MultiplyByScalar_Right_ReturnsScaledTerm()
    {
        var t = new Term(3f, 2);
        var result = t * 4f;
        Assert.AreEqual(12f, result.Coeff, Delta);
        Assert.AreEqual(2, result.Power);
    }

    [TestMethod]
    public void Term_MultiplyByScalar_Left_ReturnsScaledTerm()
    {
        var t = new Term(2.5f, 3);
        var result = 2f * t;
        Assert.AreEqual(5f, result.Coeff, Delta);
        Assert.AreEqual(3, result.Power);
    }

    [TestMethod]
    public void Term_MultiplyByTerm_ReturnsCorrectProduct()
    {
        var a = new Term(2f, 3);
        var b = new Term(4f, 2);
        var result = a * b;
        Assert.AreEqual(8f, result.Coeff, Delta);
        Assert.AreEqual(5, result.Power);
    }

    [TestMethod]
    public void Term_ToString_ReturnsFormattedString()
    {
        var t = new Term(3f, 2);
        Assert.AreEqual("3x^2", t.ToString());

        var t2 = new Term(-1.5f, 4);
        Assert.AreEqual("-1.5x^4", t2.ToString());
    }

    [TestMethod]
    public void IPolynomial_Polymorphism_WorksForTerm()
    {
        IPolynomial poly = new Term(4f, 1);
        Assert.AreEqual(1, poly.Degree);
        Assert.AreEqual(8f, poly.Evaluate(2f), Delta);
    }
}
#endif
