// #define STAGE2

#if STAGE2
using ImageFiltering.Core;

namespace ImageFiltering.Tests;

[TestClass]
public class Stage2Tests
{
    private const float Delta = 1e-4f;

    private class IdentityFunctionalFilter : FilterFunctional;

    [TestMethod]
    public void FilterFunctional_BaseBehavior_PreservesImageUnchanged()
    {
        var filter = new IdentityFunctionalFilter();
        var image = new ImageRgb(2, 2);
        image[0, 0] = new ColorRgb(0.2f, 0.4f, 0.6f);

        var result = filter.Apply(image);

        Assert.IsInstanceOfType<ImageRgb>(result);
        Assert.AreEqual(2, result.Width);
        Assert.AreEqual(2, result.Height);
        Assert.AreEqual(0.2f, result[0, 0].Red, Delta);
        Assert.AreEqual(0.4f, result[0, 0].Green, Delta);
        Assert.AreEqual(0.6f, result[0, 0].Blue, Delta);
    }

    [TestMethod]
    public void FilterInvert_InvertsChannelsCorrectly()
    {
        var filter = new FilterInvert();
        var image = new ImageRgb(2, 2);
        image[0, 0] = ColorRgb.Black;
        image[1, 0] = ColorRgb.White;
        image[0, 1] = new ColorRgb(0.2f, 0.5f, 0.8f);

        var result = filter.Apply(image);

        // Black -> White
        Assert.AreEqual(1.0f, result[0, 0].Red, Delta);
        Assert.AreEqual(1.0f, result[0, 0].Green, Delta);
        Assert.AreEqual(1.0f, result[0, 0].Blue, Delta);

        // White -> Black
        Assert.AreEqual(0.0f, result[1, 0].Red, Delta);
        Assert.AreEqual(0.0f, result[1, 0].Green, Delta);
        Assert.AreEqual(0.0f, result[1, 0].Blue, Delta);

        // 0.2, 0.5, 0.8 -> 0.8, 0.5, 0.2
        Assert.AreEqual(0.8f, result[0, 1].Red, Delta);
        Assert.AreEqual(0.5f, result[0, 1].Green, Delta);
        Assert.AreEqual(0.2f, result[0, 1].Blue, Delta);
    }

    [TestMethod]
    public void FilterBrightnessContrast_CalculatesFormulaCorrectly()
    {
        // Neutral: brightness 0, contrast 1 -> no change
        var neutral = new FilterBrightnessContrast(0.0f, 1.0f);
        Assert.AreEqual(0.0f, neutral.Brightness, Delta);
        Assert.AreEqual(1.0f, neutral.Contrast, Delta);

        var image = new ImageRgb(1, 1);
        image[0, 0] = new ColorRgb(0.4f, 0.5f, 0.6f);

        var resNeutral = neutral.Apply(image);
        Assert.AreEqual(0.4f, resNeutral[0, 0].Red, Delta);
        Assert.AreEqual(0.5f, resNeutral[0, 0].Green, Delta);
        Assert.AreEqual(0.6f, resNeutral[0, 0].Blue, Delta);

        // Brightness shift: brightness = 0.2, contrast = 1.0
        var brighter = new FilterBrightnessContrast(0.2f, 1.0f);
        var resBrighter = brighter.Apply(image);
        Assert.AreEqual(0.6f, resBrighter[0, 0].Red, Delta);
        Assert.AreEqual(0.7f, resBrighter[0, 0].Green, Delta);
        Assert.AreEqual(0.8f, resBrighter[0, 0].Blue, Delta);

        // Contrast scaling: contrast = 2.0, brightness = 0.0:
        // formula: 2.0 * (c - 0.5) + 0.5 = 2c - 0.5
        // c = 0.5 -> 2*(0) + 0.5 = 0.5
        // c = 0.8 -> 2*(0.3) + 0.5 = 1.1 -> clamped to 1.0
        // c = 0.2 -> 2*(-0.3) + 0.5 = -0.1 -> clamped to 0.0
        var highContrast = new FilterBrightnessContrast(0.0f, 2.0f);
        image[0, 0] = new ColorRgb(0.2f, 0.5f, 0.8f);
        var resContrast = highContrast.Apply(image);

        Assert.AreEqual(0.0f, resContrast[0, 0].Red, Delta);
        Assert.AreEqual(0.5f, resContrast[0, 0].Green, Delta);
        Assert.AreEqual(1.0f, resContrast[0, 0].Blue, Delta);
    }

    [TestMethod]
    public void FilterGamma_AppliesExponentialScaling()
    {
        var defaultFilter = new FilterGamma();
        Assert.AreEqual(2.0f, defaultFilter.Gamma, Delta);

        var filter = new FilterGamma(2.0f);
        Assert.AreEqual(2.0f, filter.Gamma, Delta);

        var image = new ImageRgb(2, 1);
        image[0, 0] = ColorRgb.Black;
        image[1, 0] = new ColorRgb(0.25f, 0.04f, 0.81f);

        var result = filter.Apply(image);

        // Black stays black
        Assert.AreEqual(0.0f, result[0, 0].Red, Delta);

        // c^(1/2) = sqrt(c)
        // 0.25^(0.5) = 0.5, 0.04^(0.5) = 0.2, 0.81^(0.5) = 0.9
        Assert.AreEqual(0.5f, result[1, 0].Red, Delta);
        Assert.AreEqual(0.2f, result[1, 0].Green, Delta);
        Assert.AreEqual(0.9f, result[1, 0].Blue, Delta);
    }

    [TestMethod]
    public void FilterThreshold_BinarizesBasedOnLuminance()
    {
        var filter = new FilterThreshold(0.5f);
        Assert.AreEqual(0.5f, filter.Threshold, Delta);

        var image = new ImageRgb(3, 1);
        // Red luminance = 0.299 < 0.5 -> Black
        image[0, 0] = new ColorRgb(1.0f, 0.0f, 0.0f);

        // Green luminance = 0.587 >= 0.5 -> White
        image[1, 0] = new ColorRgb(0.0f, 1.0f, 0.0f);

        // Exact 0.5 luminance -> White
        image[2, 0] = new ColorRgb(0.5f, 0.5f, 0.5f);

        var result = filter.Apply(image);

        Assert.AreEqual(ColorRgb.Black.Red, result[0, 0].Red, Delta);
        Assert.AreEqual(ColorRgb.White.Red, result[1, 0].Red, Delta);
        Assert.AreEqual(ColorRgb.White.Red, result[2, 0].Red, Delta);
    }

    [TestMethod]
    public void Filters_PreserveGrayscaleImageTypeAndDimensions()
    {
        var filter = new FilterInvert();
        var gray = new ImageGrayscale(6, 8);
        gray[0, 0] = new ColorRgb(0.3f, 0.3f, 0.3f);

        var output = filter.Apply(gray);

        Assert.IsInstanceOfType<ImageGrayscale>(output);
        Assert.AreEqual(6, output.Width);
        Assert.AreEqual(8, output.Height);
        Assert.AreEqual(0.7f, output[0, 0].Red, Delta);
    }
}
#endif
