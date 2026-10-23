namespace ImageFiltering.Core;

public class FilterLaplacian
{
    private static readonly float[,] LaplacianKernel =
    {
        { 0f,  1f, 0f },
        { 1f, -4f, 1f },
        { 0f,  1f, 0f }
    };
}
