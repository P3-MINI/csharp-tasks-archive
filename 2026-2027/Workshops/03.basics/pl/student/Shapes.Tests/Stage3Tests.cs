#if STAGE3
using System.Globalization;

namespace Shapes.Tests;

[TestClass]
public class Stage3Tests
{
    private const float Delta = 1e-4f;

    [TestInitialize]
    public void SetUp()
    {
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;
    }

    [TestMethod]
    public void Scene_Empty_HasZeroAreaAndContainsNothing()
    {
        var scene = new Scene();
        Assert.AreEqual(0f, scene.Area, Delta);
        Assert.IsFalse(scene.Contains(new Vector2(0f, 0f)));
    }

    [TestMethod]
    public void Scene_Area_IsSumOfShapeAreas()
    {
        var scene = new Scene();
        var circle = new Circle(new Vector2(0f, 0f), 2f);
        var rect = new Rectangle(new Vector2(0f, 0f), new Vector2(3f, 4f));

        scene.Add(circle);
        scene.Add(rect);

        float expectedArea = (MathF.PI * 4f) + 12f;
        Assert.AreEqual(expectedArea, scene.Area, Delta);
    }

    [TestMethod]
    public void Scene_Contains_ReturnsTrueIfContainedInAnyShape()
    {
        var scene = new Scene();
        scene.Add(new Rectangle(new Vector2(0f, 0f), new Vector2(5f, 5f)));
        scene.Add(new Circle(new Vector2(10f, 10f), 2f));

        // Inside rectangle
        Assert.IsTrue(scene.Contains(new Vector2(2f, 3f)));
        // Inside circle
        Assert.IsTrue(scene.Contains(new Vector2(10f, 11f)));
        // Outside both
        Assert.IsFalse(scene.Contains(new Vector2(8f, 2f)));
    }

    [TestMethod]
    public void Scene_Render_ReturnsExpectedPixelBuffer()
    {
        var scene = new Scene();
        scene.Add(new Rectangle(new Vector2(1f, 1f), new Vector2(3f, 3f)));

        char[,] actual = scene.Render(x: 0, y: 0, width: 5, height: 5);

        char[,] expected = new char[5, 5]
        {
            { '.', '.', '.', '.', '.' },
            { '.', '#', '#', '#', '.' },
            { '.', '#', '#', '#', '.' },
            { '.', '#', '#', '#', '.' },
            { '.', '.', '.', '.', '.' }
        };

        Assert.AreEqual(5, actual.GetLength(0));
        Assert.AreEqual(5, actual.GetLength(1));

        for (int r = 0; r < 5; r++)
        {
            for (int c = 0; c < 5; c++)
            {
                Assert.AreEqual(expected[r, c], actual[r, c], $"Mismatch at row {r}, col {c}");
            }
        }
    }

    [TestMethod]
    public void Scene_IsIShape()
    {
        IShape scene = new Scene();
        Assert.IsNotNull(scene);
    }
}
#endif
