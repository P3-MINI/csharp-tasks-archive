// #define STAGE3

#if STAGE3
using ImageFiltering.Core;

namespace ImageFiltering.Tests;

[TestClass]
public class Stage3Tests
{
    private const float Delta = 1e-4f;

    [TestMethod]
    public void FilterPipeline_Constructor_WithParams_AppliesFiltersInOrder()
    {
        // Brightness(+0.2) followed by Invert is non-commutative:
        // (c + 0.2) then (1.0 - result) => 1.0 - (c + 0.2)
        var pipeline = new FilterPipeline(new FilterBrightnessContrast(0.2f, 1.0f), new FilterInvert());
        var image = new ImageRgb(2, 2);
        image[0, 0] = new ColorRgb(0.1f, 0.2f, 0.3f);

        var result = pipeline.Apply(image);

        // Expected: Red = 1.0 - 0.3 = 0.7, Green = 1.0 - 0.4 = 0.6, Blue = 1.0 - 0.5 = 0.5
        Assert.AreEqual(0.7f, result[0, 0].Red, Delta);
        Assert.AreEqual(0.6f, result[0, 0].Green, Delta);
        Assert.AreEqual(0.5f, result[0, 0].Blue, Delta);
    }

    [TestMethod]
    public void FilterPipeline_Empty_ReturnsImageUnmodified()
    {
        var pipeline = new FilterPipeline();
        var image = new ImageRgb(2, 2);
        image[0, 0] = new ColorRgb(0.1f, 0.2f, 0.3f);

        var result = pipeline.Apply(image);

        Assert.AreSame(image, result);
    }

    [TestMethod]
    public void FilterPipeline_Add_FlattensNestedPipelines()
    {
        var nestedInCtor = new FilterPipeline(new FilterGamma(2.0f));
        var pipe1 = new FilterPipeline(nestedInCtor);
        var pipe2 = new FilterPipeline(new FilterBrightnessContrast(0.1f, 1.0f), new FilterInvert());

        pipe1.Add(pipe2);

        // Mutating original sub-pipelines afterwards must not affect flattened pipe1
        nestedInCtor.Add(new FilterInvert());
        pipe2.Add(new FilterInvert());

        var image = new ImageRgb(2, 2);
        image[0, 0] = new ColorRgb(0.25f, 0.25f, 0.25f);

        var result = pipe1.Apply(image);

        // Expected sequential: Gamma -> Brightness -> Invert
        var step1 = new FilterGamma(2.0f).Apply(image);
        var step2 = new FilterBrightnessContrast(0.1f, 1.0f).Apply(step1);
        var expected = new FilterInvert().Apply(step2);

        Assert.AreEqual(expected[0, 0].Red, result[0, 0].Red, Delta);
        Assert.AreEqual(expected[0, 0].Green, result[0, 0].Green, Delta);
        Assert.AreEqual(expected[0, 0].Blue, result[0, 0].Blue, Delta);
    }

    [TestMethod]
    public void FilterBase_OperatorPlus_CombinesFiltersIntoPipeline()
    {
        FilterBase f1 = new FilterInvert();
        FilterBase f2 = new FilterThreshold(0.5f);

        FilterPipeline pipeline = f1 + f2;
        Assert.IsNotNull(pipeline);

        var image = new ImageRgb(2, 2);
        image[0, 0] = new ColorRgb(0.2f, 0.2f, 0.2f); // inverted -> 0.8 -> threshold -> 1.0 White

        var result = pipeline.Apply(image);
        var expected = f2.Apply(f1.Apply(image));

        Assert.AreEqual(expected[0, 0].Red, result[0, 0].Red, Delta);
        Assert.AreEqual(expected[0, 0].Green, result[0, 0].Green, Delta);
        Assert.AreEqual(expected[0, 0].Blue, result[0, 0].Blue, Delta);
    }

    [TestMethod]
    public void FilterBase_OperatorPlus_ChainingMultipleFilters()
    {
        FilterBase chained = new FilterGamma(2.0f) + new FilterBrightnessContrast(0.1f, 1.2f) + new FilterInvert();

        var image = new ImageRgb(3, 3);
        image[1, 1] = new ColorRgb(0.36f, 0.49f, 0.64f);

        var result = chained.Apply(image);

        var step1 = new FilterGamma(2.0f).Apply(image);
        var step2 = new FilterBrightnessContrast(0.1f, 1.2f).Apply(step1);
        var expected = new FilterInvert().Apply(step2);

        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                Assert.AreEqual(expected[x, y].Red, result[x, y].Red, Delta);
                Assert.AreEqual(expected[x, y].Green, result[x, y].Green, Delta);
                Assert.AreEqual(expected[x, y].Blue, result[x, y].Blue, Delta);
            }
        }
    }
}
#endif
