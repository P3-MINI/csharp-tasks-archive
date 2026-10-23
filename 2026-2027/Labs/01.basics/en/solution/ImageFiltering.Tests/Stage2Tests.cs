#define STAGE2

#if STAGE2
using ImageFiltering.Core;

namespace ImageFiltering.Tests;

[TestClass]
public class Stage2Tests
{
    private const float Delta = 1e-4f;

    private class TestMatrixFilter(float[,] kernel, float divisor = 1.0f) : FilterMatrix(kernel, divisor);

    [TestMethod]
    public void FilterMatrix_ConstructorValidation_ThrowsOnInvalidKernelOrDivisor()
    {
        // Invalid dimensions (not 3x3)
        Assert.ThrowsExactly<ArgumentException>(() => new TestMatrixFilter(new float[3, 4], 1f));
        Assert.ThrowsExactly<ArgumentException>(() => new TestMatrixFilter(new float[4, 3], 1f));
        Assert.ThrowsExactly<ArgumentException>(() => new TestMatrixFilter(new float[2, 2], 1f));
        Assert.ThrowsExactly<ArgumentException>(() => new TestMatrixFilter(new float[1, 9], 1f));

        // Zero divisor
        Assert.ThrowsExactly<ArgumentException>(() => new TestMatrixFilter(new float[3, 3], 0f));
    }

    [TestMethod]
    public void FilterMatrix_AsymmetricKernel_IndexesRowAndColumnCorrectly()
    {
        // Weight 1.0 at row 0 (ky = -1), column 2 (kx = +1) -> samples (x + 1, y - 1)
        var kernel = new float[3, 3]
        {
            { 0, 0, 1 },
            { 0, 0, 0 },
            { 0, 0, 0 }
        };

        var filter = new TestMatrixFilter(kernel, 1.0f);
        var image = new ImageRgb(3, 3);
        image[2, 0] = ColorRgb.White;

        var result = filter.Apply(image);

        Assert.AreEqual(1.0f, result[1, 1].Red, Delta);
        Assert.AreEqual(0.0f, result[0, 2].Red, Delta);
    }

    [TestMethod]
    public void FilterMatrix_Constructor_ClonesKernelArray()
    {
        var kernel = new float[3, 3]
        {
            { 0, 0, 0 },
            { 0, 1, 0 },
            { 0, 0, 0 }
        };

        var filter = new TestMatrixFilter(kernel, 1.0f);

        // Mutate original array
        kernel[1, 1] = 999.0f;

        var image = new ImageRgb(3, 3);
        image[1, 1] = new ColorRgb(0.5f, 0.5f, 0.5f);

        var result = filter.Apply(image);

        // Result should still be 0.5f, not 999 * 0.5 clamped
        Assert.AreEqual(0.5f, result[1, 1].Red, Delta);
    }

    [TestMethod]
    public void FilterBoxBlur_PreservesUniformColor()
    {
        var filter = new FilterBoxBlur();
        var image = new ImageRgb(4, 4);
        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                image[x, y] = new ColorRgb(0.6f, 0.6f, 0.6f);
            }
        }

        var result = filter.Apply(image);

        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                Assert.AreEqual(0.6f, result[x, y].Red, Delta, $"Mismatch at ({x}, {y})");
                Assert.AreEqual(0.6f, result[x, y].Green, Delta, $"Mismatch at ({x}, {y})");
                Assert.AreEqual(0.6f, result[x, y].Blue, Delta, $"Mismatch at ({x}, {y})");
            }
        }
    }

    [TestMethod]
    public void FilterGaussianBlur_AppliesWeightedAverage()
    {
        var filter = new FilterGaussianBlur();
        var image = new ImageRgb(3, 3);

        // Single white pixel at center, all others black
        image[1, 1] = ColorRgb.White;

        var result = filter.Apply(image);

        // Center should have weight 4 / 16 = 0.25
        Assert.AreEqual(4f / 16f, result[1, 1].Red, Delta);

        // Orthogonal neighbors should have weight 2 / 16 = 0.125
        Assert.AreEqual(2f / 16f, result[1, 0].Red, Delta);
        Assert.AreEqual(2f / 16f, result[0, 1].Red, Delta);
        Assert.AreEqual(2f / 16f, result[2, 1].Red, Delta);
        Assert.AreEqual(2f / 16f, result[1, 2].Red, Delta);

        // Diagonal corners should have weight 1 / 16 = 0.0625
        Assert.AreEqual(1f / 16f, result[0, 0].Red, Delta);
        Assert.AreEqual(1f / 16f, result[2, 0].Red, Delta);
        Assert.AreEqual(1f / 16f, result[0, 2].Red, Delta);
        Assert.AreEqual(1f / 16f, result[2, 2].Red, Delta);
    }

    [TestMethod]
    public void FilterSharpen_SharpensContrast()
    {
        var filter = new FilterSharpen();
        var image = new ImageRgb(3, 3);

        // Center pixel is 0.5, neighbors are 0.2
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                image[x, y] = new ColorRgb(0.2f, 0.2f, 0.2f);
            }
        }
        image[1, 1] = new ColorRgb(0.5f, 0.5f, 0.5f);

        var result = filter.Apply(image);

        // Sharpen center: 5 * 0.5 - 4 * 0.2 = 2.5 - 0.8 = 1.7 (clamped to 1.0)
        Assert.AreEqual(1.0f, result[1, 1].Red, Delta);
    }

    [TestMethod]
    public void FilterLaplacian_DetectsEdgesAndBlanksFlatAreas()
    {
        var filter = new FilterLaplacian();
        var flatImage = new ImageRgb(3, 3);
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                flatImage[x, y] = new ColorRgb(0.5f, 0.5f, 0.5f);
            }
        }

        var result = filter.Apply(flatImage);

        // In a flat area: 4 * 0.5 - 4 * 0.5 = 0.0
        Assert.AreEqual(0.0f, result[1, 1].Red, Delta);
        Assert.AreEqual(0.0f, result[1, 1].Green, Delta);
        Assert.AreEqual(0.0f, result[1, 1].Blue, Delta);
    }

    [TestMethod]
    public void FilterMatrix_AppliesBoundaryClampingAtCornersAndEdges()
    {
        var filter = new FilterBoxBlur();
        var image = new ImageRgb(3, 3);

        // Single white pixel at corner (0, 0), all others black
        image[0, 0] = ColorRgb.White;

        var result = filter.Apply(image);

        // At corner (0, 0), 4 out of 9 kernel samples (-1,-1; 0,-1; -1,0; 0,0) clamp to (0, 0)
        Assert.AreEqual(4f / 9f, result[0, 0].Red, Delta);

        // At edge (1, 0), 2 out of 9 kernel samples (0,-1; 0,0) clamp to (0, 0)
        Assert.AreEqual(2f / 9f, result[1, 0].Red, Delta);

        // At opposite corner (2, 2), no kernel samples reach (0, 0)
        Assert.AreEqual(0f, result[2, 2].Red, Delta);
    }

    [TestMethod]
    public void Filters_PreserveImageTypeAndDimensions()
    {
        var filter = new FilterBoxBlur();
        var rgb32 = new ImageRgb32(5, 7);

        var output = filter.Apply(rgb32);

        Assert.IsInstanceOfType<ImageRgb32>(output);
        Assert.AreEqual(5, output.Width);
        Assert.AreEqual(7, output.Height);
    }
}
#endif
