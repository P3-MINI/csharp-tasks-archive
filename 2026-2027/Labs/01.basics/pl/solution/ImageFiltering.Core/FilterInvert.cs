namespace ImageFiltering.Core;

public class FilterInvert : FilterFunctional
{
    protected override ColorRgb Transform(ColorRgb color)
    {
        return new ColorRgb(1 - color.Red, 1 - color.Green, 1 - color.Blue);
    }
}