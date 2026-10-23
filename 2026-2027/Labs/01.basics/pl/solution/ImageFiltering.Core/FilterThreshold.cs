namespace ImageFiltering.Core;

public class FilterThreshold(float threshold = 0.5f) : FilterFunctional
{
    public float Threshold { get; } = threshold;

    protected override ColorRgb Transform(ColorRgb color)
    {
        return color.Luminance >= Threshold ? ColorRgb.White : ColorRgb.Black;
    }
}