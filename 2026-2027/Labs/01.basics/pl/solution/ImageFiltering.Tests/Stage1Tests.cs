#define STAGE1

#if STAGE1
using ImageFiltering.Core;

namespace ImageFiltering.Tests;

[TestClass]
public class Stage1Tests
{
    private const float Delta = 1e-4f;

    [TestMethod]
    public void ColorRgb_Constructor_ClampsValuesToZeroOne()
    {
        var color = new ColorRgb(-0.5f, 1.5f, 0.4f);

        Assert.AreEqual(0.0f, color.Red, Delta);
        Assert.AreEqual(1.0f, color.Green, Delta);
        Assert.AreEqual(0.4f, color.Blue, Delta);
    }

    [TestMethod]
    public void ColorRgb_StaticProperties_WhiteAndBlack_HaveExpectedValues()
    {
        Assert.AreEqual(1.0f, ColorRgb.White.Red, Delta);
        Assert.AreEqual(1.0f, ColorRgb.White.Green, Delta);
        Assert.AreEqual(1.0f, ColorRgb.White.Blue, Delta);

        Assert.AreEqual(0.0f, ColorRgb.Black.Red, Delta);
        Assert.AreEqual(0.0f, ColorRgb.Black.Green, Delta);
        Assert.AreEqual(0.0f, ColorRgb.Black.Blue, Delta);
    }

    [TestMethod]
    public void ColorRgb_Luminance_CalculatesCorrectly()
    {
        var red = new ColorRgb(1.0f, 0.0f, 0.0f);
        var green = new ColorRgb(0.0f, 1.0f, 0.0f);
        var blue = new ColorRgb(0.0f, 0.0f, 1.0f);
        var mixed = new ColorRgb(0.5f, 0.3f, 0.2f);

        Assert.AreEqual(0.299f, red.Luminance, Delta);
        Assert.AreEqual(0.587f, green.Luminance, Delta);
        Assert.AreEqual(0.114f, blue.Luminance, Delta);
        Assert.AreEqual(0.299f * 0.5f + 0.587f * 0.3f + 0.114f * 0.2f, mixed.Luminance, Delta);
    }

    [TestMethod]
    public void ColorRgb_Operators_AddSubtractMultiply_WorkCorrectly()
    {
        var c1 = new ColorRgb(0.2f, 0.5f, 0.8f);
        var c2 = new ColorRgb(0.3f, 0.2f, 0.1f);

        var sum = c1 + c2;
        Assert.AreEqual(0.5f, sum.Red, Delta);
        Assert.AreEqual(0.7f, sum.Green, Delta);
        Assert.AreEqual(0.9f, sum.Blue, Delta);

        var diff = c1 - c2;
        Assert.AreEqual(0.0f, diff.Red, Delta); // 0.2 - 0.3 = -0.1 clamped to 0
        Assert.AreEqual(0.3f, diff.Green, Delta);
        Assert.AreEqual(0.7f, diff.Blue, Delta);

        var mult = c1 * c2;
        Assert.AreEqual(0.06f, mult.Red, Delta);
        Assert.AreEqual(0.10f, mult.Green, Delta);
        Assert.AreEqual(0.08f, mult.Blue, Delta);

        var scaled1 = c1 * 2.0f;
        Assert.AreEqual(0.4f, scaled1.Red, Delta);
        Assert.AreEqual(1.0f, scaled1.Green, Delta);
        Assert.AreEqual(1.0f, scaled1.Blue, Delta); // clamped to 1.0

        var scaled2 = 0.5f * c1;
        Assert.AreEqual(0.1f, scaled2.Red, Delta);
        Assert.AreEqual(0.25f, scaled2.Green, Delta);
        Assert.AreEqual(0.4f, scaled2.Blue, Delta);
    }

    [TestMethod]
    public void ColorRgb_ToString_FormatsCorrectly()
    {
        var color = new ColorRgb(0.1234f, 0.5f, 1.0f);
        string formatted = color.ToString();

        Assert.AreEqual("(0.123R, 0.500G, 1.000B)", formatted);
    }

    private class TestImage(int width, int height) : Image(width, height)
    {
        public override ColorRgb this[int x, int y]
        {
            get => ColorRgb.Black;
            set { }
        }

        public override Image CreateImage(int width, int height) => new TestImage(width, height);
    }

    [TestMethod]
    public void Image_Constructor_ThrowsOnNonPositiveDimensions()
    {
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => new ImageRgb(0, 5));
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => new ImageRgb(5, 0));
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => new TestImage(-1, 5));
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => new TestImage(5, -1));
    }

    [TestMethod]
    public void ImageRgb_ConstructorsAndIndexer_GetAndSetCorrectly()
    {
        var img = new ImageRgb(4, 3);
        Assert.AreEqual(4, img.Width);
        Assert.AreEqual(3, img.Height);

        var color = new ColorRgb(0.2f, 0.4f, 0.6f);
        img[2, 1] = color;

        Assert.AreEqual(0.2f, img[2, 1].Red, Delta);
        Assert.AreEqual(0.4f, img[2, 1].Green, Delta);
        Assert.AreEqual(0.6f, img[2, 1].Blue, Delta);

        var matching = img.CreateImage(2, 2);
        Assert.IsInstanceOfType<ImageRgb>(matching);
        Assert.AreEqual(2, matching.Width);
        Assert.AreEqual(2, matching.Height);
    }

    [TestMethod]
    public void ImageGrayscale_ConstructorsAndIndexer_ConvertsLuminanceCorrectly()
    {
        var img = new ImageGrayscale(3, 2);
        Assert.AreEqual(3, img.Width);
        Assert.AreEqual(2, img.Height);

        // Set pure red (luminance = 0.299)
        img[0, 0] = new ColorRgb(1.0f, 0.0f, 0.0f);
        var readColor = img[0, 0];

        Assert.AreEqual(0.299f, readColor.Red, Delta);
        Assert.AreEqual(0.299f, readColor.Green, Delta);
        Assert.AreEqual(0.299f, readColor.Blue, Delta);

        // Set pure white
        img[1, 0] = ColorRgb.White;
        Assert.AreEqual(1.0f, img[1, 0].Red, Delta);
        Assert.AreEqual(1.0f, img[1, 0].Green, Delta);
        Assert.AreEqual(1.0f, img[1, 0].Blue, Delta);

        // Set pure black
        img[2, 0] = ColorRgb.Black;
        Assert.AreEqual(0.0f, img[2, 0].Red, Delta);
        Assert.AreEqual(0.0f, img[2, 0].Green, Delta);
        Assert.AreEqual(0.0f, img[2, 0].Blue, Delta);

        var matching = img.CreateImage(4, 4);
        Assert.IsInstanceOfType<ImageGrayscale>(matching);
        Assert.AreEqual(4, matching.Width);
        Assert.AreEqual(4, matching.Height);
    }
}
#endif
