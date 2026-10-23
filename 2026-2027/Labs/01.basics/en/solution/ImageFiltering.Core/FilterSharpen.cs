namespace ImageFiltering.Core;

public class FilterSharpen() : FilterMatrix(SharpenKernel)
{
    private static readonly float[,] SharpenKernel =
    {
        {  0f, -1f,  0f },
        { -1f,  5f, -1f },
        {  0f, -1f,  0f }
    };
}