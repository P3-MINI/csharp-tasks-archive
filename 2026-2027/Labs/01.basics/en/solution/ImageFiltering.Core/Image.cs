namespace ImageFiltering.Core;

public abstract class Image
{
    public int Width { get; }
    public int Height { get; }
    
    protected Image(int width, int height)
    {
        if (width <= 0 || height <= 0)
        {
            throw new ArgumentOutOfRangeException(width <= 0 ? nameof(width) : nameof(height), "Image dimensions must be positive.");
        }

        Width = width;
        Height = height;
    }
    
    public abstract ColorRgb this[int x, int y] { get; set; }
    
    public abstract Image CreateImage(int width, int height);

    public ColorRgb SampleClamped(int x, int y)
    {
        x = Math.Clamp(x, 0, Width - 1);
        y = Math.Clamp(y, 0, Height - 1);
        return this[x, y];
    }
}