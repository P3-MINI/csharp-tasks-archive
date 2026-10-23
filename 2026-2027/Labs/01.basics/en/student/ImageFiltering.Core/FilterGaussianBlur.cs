namespace ImageFiltering.Core;

public class FilterGaussianBlur
{
    private static readonly float[,] GaussianBlurKernel =
    {
        { 1f, 2f, 1f },
        { 2f, 4f, 2f },
        { 1f, 2f, 1f }
    };
}
