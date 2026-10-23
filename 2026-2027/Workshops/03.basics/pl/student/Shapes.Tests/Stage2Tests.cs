#if STAGE2
using System.Globalization;

namespace Shapes.Tests;

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
    public void Circle_ConstructorAndProperties_SetsValuesCorrectly()
    {
        var center = new Vector2(2.0f, 3.0f);
        var circle = new Circle(center, 5.0f);

        Assert.AreEqual(center.X, circle.Center.X, Delta);
        Assert.AreEqual(center.Y, circle.Center.Y, Delta);
        Assert.AreEqual(5.0f, circle.Radius, Delta);
    }

    [TestMethod]
    public void Circle_Area_CalculatesCorrectly()
    {
        var circle = new Circle(new Vector2(0f, 0f), 3.0f);
        float expectedArea = MathF.PI * 9.0f;
        Assert.AreEqual(expectedArea, circle.Area, Delta);
    }

    [TestMethod]
    public void Circle_Contains_InsidePoint_ReturnsTrue()
    {
        var circle = new Circle(new Vector2(1f, 1f), 5.0f);
        Assert.IsTrue(circle.Contains(new Vector2(2f, 2f)));
    }

    [TestMethod]
    public void Circle_Contains_BoundaryPoint_ReturnsTrue()
    {
        var circle = new Circle(new Vector2(0f, 0f), 5.0f);
        Assert.IsTrue(circle.Contains(new Vector2(3f, 4f)));
        Assert.IsTrue(circle.Contains(new Vector2(5f, 0f)));
    }

    [TestMethod]
    public void Circle_Contains_OutsidePoint_ReturnsFalse()
    {
        var circle = new Circle(new Vector2(0f, 0f), 2.0f);
        Assert.IsFalse(circle.Contains(new Vector2(3f, 3f)));
    }

    [TestMethod]
    public void Rectangle_ConstructorAndProperties_SetsValuesCorrectly()
    {
        var p1 = new Vector2(1f, 2f);
        var p2 = new Vector2(5f, 6f);
        var rect = new Rectangle(p1, p2);

        Assert.AreEqual(p1.X, rect.P1.X, Delta);
        Assert.AreEqual(p1.Y, rect.P1.Y, Delta);
        Assert.AreEqual(p2.X, rect.P2.X, Delta);
        Assert.AreEqual(p2.Y, rect.P2.Y, Delta);
    }

    [TestMethod]
    public void Rectangle_Area_CalculatesCorrectlyRegardlessOfCornerOrder()
    {
        var rect1 = new Rectangle(new Vector2(1f, 2f), new Vector2(5f, 6f));
        Assert.AreEqual(16.0f, rect1.Area, Delta);

        // Reversed corners
        var rect2 = new Rectangle(new Vector2(5f, 6f), new Vector2(1f, 2f));
        Assert.AreEqual(16.0f, rect2.Area, Delta);

        var rect3 = new Rectangle(new Vector2(1f, 6f), new Vector2(5f, 2f));
        Assert.AreEqual(16.0f, rect3.Area, Delta);
    }

    [TestMethod]
    public void Rectangle_Contains_InsideAndBoundaryPoints_ReturnsTrue()
    {
        var rect = new Rectangle(new Vector2(10f, 10f), new Vector2(2f, 4f));

        // Inside
        Assert.IsTrue(rect.Contains(new Vector2(5f, 6f)));
        // Corners
        Assert.IsTrue(rect.Contains(new Vector2(2f, 4f)));
        Assert.IsTrue(rect.Contains(new Vector2(10f, 10f)));
        // Edges
        Assert.IsTrue(rect.Contains(new Vector2(2f, 7f)));
        Assert.IsTrue(rect.Contains(new Vector2(6f, 10f)));
    }

    [TestMethod]
    public void Rectangle_Contains_OutsidePoint_ReturnsFalse()
    {
        var rect = new Rectangle(new Vector2(2f, 4f), new Vector2(10f, 10f));
        Assert.IsFalse(rect.Contains(new Vector2(1f, 5f)));
        Assert.IsFalse(rect.Contains(new Vector2(5f, 11f)));
    }

    [TestMethod]
    public void IShape_Polymorphism_WorksForCircleAndRectangle()
    {
        IShape shape1 = new Circle(new Vector2(0f, 0f), 2.0f);
        IShape shape2 = new Rectangle(new Vector2(0f, 0f), new Vector2(4f, 4f));

        Assert.AreEqual(MathF.PI * 4.0f, shape1.Area, Delta);
        Assert.AreEqual(16.0f, shape2.Area, Delta);
        Assert.IsTrue(shape1.Contains(new Vector2(1f, 1f)));
        Assert.IsTrue(shape2.Contains(new Vector2(2f, 2f)));
    }
}
#endif
