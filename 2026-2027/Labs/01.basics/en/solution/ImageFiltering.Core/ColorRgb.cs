namespace ImageFiltering.Core;

public readonly struct ColorRgb(float red, float green, float blue)
{
    public float Red { get; } = Math.Clamp(red, 0, 1);
    public float Green { get; } = Math.Clamp(green, 0, 1);
    public float Blue { get; } = Math.Clamp(blue, 0, 1);

    public static ColorRgb White => new ColorRgb(1, 1, 1);
    public static ColorRgb Black => new ColorRgb(0, 0, 0);

    public float Luminance => 0.299F * Red + 0.587F * Green + 0.114F * Blue;

    public static ColorRgb operator +(ColorRgb a, ColorRgb b) => new(a.Red + b.Red, a.Green + b.Green, a.Blue + b.Blue);
    public static ColorRgb operator -(ColorRgb a, ColorRgb b) => new(a.Red - b.Red, a.Green - b.Green, a.Blue - b.Blue);
    public static ColorRgb operator *(ColorRgb a, ColorRgb b) => new(a.Red * b.Red, a.Green * b.Green, a.Blue * b.Blue);
    public static ColorRgb operator *(ColorRgb a, float b) => new(a.Red * b, a.Green * b, a.Blue * b);
    public static ColorRgb operator *(float b, ColorRgb a) => a * b;

    public override string ToString() => $"({Red:F3}R, {Green:F3}G, {Blue:F3}B)";
}