namespace ImageFiltering.Core;

public class FilterBoxBlur() : FilterMatrix(BoxBlurKernel, 9.0f) 
{
    private static readonly float[,] BoxBlurKernel =
    {
        { 1f, 1f, 1f },
        { 1f, 1f, 1f },
        { 1f, 1f, 1f }
    };
}