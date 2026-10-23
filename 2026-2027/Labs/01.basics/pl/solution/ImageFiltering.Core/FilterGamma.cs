namespace ImageFiltering.Core;

public class FilterGamma : FilterFunctional
{
    private readonly float _invGamma;

    public float Gamma { get; }

    public FilterGamma(float gamma = 2.0f)
    {
        if (gamma <= 0.0f)
        {
            throw new ArgumentOutOfRangeException(nameof(gamma), "Gamma must be greater than zero.");
        }

        Gamma = gamma;
        _invGamma = 1.0f / gamma;
    }

    protected override ColorRgb Transform(ColorRgb color)
    {
        return new ColorRgb(
            MathF.Pow(color.Red, _invGamma), 
            MathF.Pow(color.Green, _invGamma), 
            MathF.Pow(color.Blue, _invGamma));
    }
}