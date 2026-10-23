namespace ImageFiltering.Core;

public class FilterBoxBlur
{
    private static readonly float[,] BoxBlurKernel =
    {
        { 1f, 1f, 1f },
        { 1f, 1f, 1f },
        { 1f, 1f, 1f }
    };
}
