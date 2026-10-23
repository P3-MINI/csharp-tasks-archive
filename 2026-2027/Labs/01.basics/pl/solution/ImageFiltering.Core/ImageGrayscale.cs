namespace ImageFiltering.Core;

public class ImageGrayscale : Image
{
    private readonly float[,] _data;

    public ImageGrayscale(int width, int height) : base(width, height)
    {
        _data = new float[width, height];
    }

    public override ColorRgb this[int x, int y]
    {
        get => new(_data[x, y], _data[x, y], _data[x, y]);
        set => _data[x, y] = value.Luminance;
    }

    public override Image CreateImage(int width, int height)
    {
        return new ImageGrayscale(width, height);
    }
}