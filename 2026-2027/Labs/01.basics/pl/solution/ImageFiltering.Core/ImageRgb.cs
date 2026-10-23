namespace ImageFiltering.Core;

public class ImageRgb : Image
{
    private readonly ColorRgb[,] _data;

    public ImageRgb(int width, int height) : base(width, height)
    {
        _data = new ColorRgb[width, height];
    }

    public override ColorRgb this[int x, int y]
    {
        get => _data[x, y];
        set => _data[x, y] = value;
    }

    public override Image CreateImage(int width, int height)
    {
        return new ImageRgb(width, height);
    }
}