#define STAGE3

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
        var pipeline = new FilterPipeline(new FilterBoxBlur(), new FilterSharpen());
        var image = new ImageRgb(3, 3);
        image[1, 1] = ColorRgb.White;

        var result = pipeline.Apply(image);

        // Applying sequentially by hand
        var expected = new FilterSharpen().Apply(new FilterBoxBlur().Apply(image));

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
        var nestedInCtor = new FilterPipeline(new FilterBoxBlur());
        var pipe1 = new FilterPipeline(nestedInCtor);
        var pipe2 = new FilterPipeline(new FilterSharpen(), new FilterGaussianBlur());

        pipe1.Add(pipe2);

        // Mutating original sub-pipelines afterwards must not affect flattened pipe1
        nestedInCtor.Add(new FilterLaplacian());
        pipe2.Add(new FilterLaplacian());

        var image = new ImageRgb(3, 3);
        image[1, 1] = ColorRgb.White;

        var result = pipe1.Apply(image);

        // Expected sequential: BoxBlur -> Sharpen -> GaussianBlur
        var step1 = new FilterBoxBlur().Apply(image);
        var step2 = new FilterSharpen().Apply(step1);
        var expected = new FilterGaussianBlur().Apply(step2);

        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                Assert.AreEqual(expected[x, y].Red, result[x, y].Red, Delta);
            }
        }
    }

    [TestMethod]
    public void FilterBase_OperatorPlus_CombinesFiltersIntoPipeline()
    {
        FilterBase f1 = new FilterBoxBlur();
        FilterBase f2 = new FilterSharpen();

        FilterPipeline pipeline = f1 + f2;
        Assert.IsNotNull(pipeline);

        var image = new ImageRgb(3, 3);
        image[1, 1] = ColorRgb.White;

        var result = pipeline.Apply(image);
        var expected = f2.Apply(f1.Apply(image));

        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                Assert.AreEqual(expected[x, y].Red, result[x, y].Red, Delta);
            }
        }
    }

    [TestMethod]
    public void FilterBase_OperatorPlus_ChainingMultipleFilters()
    {
        FilterBase chained = new FilterBoxBlur() + new FilterSharpen() + new FilterLaplacian();

        var image = new ImageRgb(4, 4);
        image[2, 2] = ColorRgb.White;

        var result = chained.Apply(image);

        var step1 = new FilterBoxBlur().Apply(image);
        var step2 = new FilterSharpen().Apply(step1);
        var expected = new FilterLaplacian().Apply(step2);

        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                Assert.AreEqual(expected[x, y].Red, result[x, y].Red, Delta);
                Assert.AreEqual(expected[x, y].Green, result[x, y].Green, Delta);
                Assert.AreEqual(expected[x, y].Blue, result[x, y].Blue, Delta);
            }
        }
    }
}
#endif
