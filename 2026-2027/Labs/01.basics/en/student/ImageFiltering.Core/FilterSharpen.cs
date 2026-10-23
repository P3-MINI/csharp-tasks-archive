namespace ImageFiltering.Core;

public class FilterSharpen
{
    private static readonly float[,] SharpenKernel =
    {
        {  0f, -1f,  0f },
        { -1f,  5f, -1f },
        {  0f, -1f,  0f }
    };
}
