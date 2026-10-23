namespace ImageFiltering.Core;

public class FilterGaussianBlur() : FilterMatrix(GaussianBlurKernel, 16.0f)
{
    private static readonly float[,] GaussianBlurKernel =
    {
        { 1f, 2f, 1f },
        { 2f, 4f, 2f },
        { 1f, 2f, 1f }
    };
}