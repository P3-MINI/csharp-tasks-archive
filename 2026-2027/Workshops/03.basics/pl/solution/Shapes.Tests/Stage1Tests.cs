#if STAGE1
using System.Globalization;

namespace Shapes.Tests;

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
    public void Vector2_ConstructorAndProperties_SetsCoordinatesCorrectly()
    {
        var v = new Vector2(3.5f, -2.1f);
        Assert.AreEqual(3.5f, v.X, Delta);
        Assert.AreEqual(-2.1f, v.Y, Delta);
    }

    [TestMethod]
    public void Vector2_Addition_ReturnsCorrectSum()
    {
        var a = new Vector2(1.0f, 2.0f);
        var b = new Vector2(3.0f, -4.0f);
        var result = a + b;
        Assert.AreEqual(4.0f, result.X, Delta);
        Assert.AreEqual(-2.0f, result.Y, Delta);
    }

    [TestMethod]
    public void Vector2_Subtraction_ReturnsCorrectDifference()
    {
        var a = new Vector2(5.0f, 3.0f);
        var b = new Vector2(2.0f, 7.0f);
        var result = a - b;
        Assert.AreEqual(3.0f, result.X, Delta);
        Assert.AreEqual(-4.0f, result.Y, Delta);
    }

    [TestMethod]
    public void Vector2_MultiplyByScalar_Right_ReturnsScaledVector()
    {
        var v = new Vector2(2.0f, -3.0f);
        var result = v * 2.5f;
        Assert.AreEqual(5.0f, result.X, Delta);
        Assert.AreEqual(-7.5f, result.Y, Delta);
    }

    [TestMethod]
    public void Vector2_MultiplyByScalar_Left_ReturnsScaledVector()
    {
        var v = new Vector2(-1.5f, 4.0f);
        var result = 3.0f * v;
        Assert.AreEqual(-4.5f, result.X, Delta);
        Assert.AreEqual(12.0f, result.Y, Delta);
    }

    [TestMethod]
    public void Vector2_Length_CalculatesEuclideanDistance()
    {
        var v = new Vector2(3.0f, 4.0f);
        Assert.AreEqual(5.0f, v.Length, Delta);

        var zero = new Vector2(0f, 0f);
        Assert.AreEqual(0f, zero.Length, Delta);
    }

    [TestMethod]
    public void Vector2_ToString_ReturnsFormattedString()
    {
        var v = new Vector2(1.5f, -2.5f);
        Assert.AreEqual("(1.5, -2.5)", v.ToString());
    }
}
#endif
