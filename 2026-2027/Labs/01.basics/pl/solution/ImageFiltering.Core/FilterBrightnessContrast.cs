namespace ImageFiltering.Core;

public class FilterBrightnessContrast(float brightness, float contrast) : FilterFunctional
{
    public float Brightness { get; } = brightness;
    public float Contrast { get; } = contrast;

    protected override ColorRgb Transform(ColorRgb color)
    {
        return new ColorRgb(Contrast * (color.Red - 0.5f) + 0.5f + Brightness, 
            Contrast * (color.Green - 0.5f) + 0.5f + Brightness, 
            Contrast * (color.Blue - 0.5f) + 0.5f + Brightness);
    }
}