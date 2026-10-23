namespace ImageFiltering.Core;

public class ImageRgb32 : Image
{
    private readonly uint[,] _data;

    public ImageRgb32(int width, int height) : base(width, height)
    {
        _data = new uint[width, height];
    }
    
    public override ColorRgb this[int x, int y]
    {
        get
        {
            return new ColorRgb(((_data[x, y] >> 24) & 0xFF) / 255f,
                ((_data[x, y] >> 16) & 0xFF) / 255f,
                ((_data[x, y] >> 8) & 0xFF) / 255f);
        }
        set
        {
            uint red = (uint)MathF.Round(value.Red * 255f);
            uint green = (uint)MathF.Round(value.Green * 255f);
            uint blue = (uint)MathF.Round(value.Blue * 255f);
            _data[x, y] = (red << 24) | (green << 16) | (blue << 8) | 0xFF;
        }
    }

    public override Image CreateImage(int width, int height)
    {
        return new ImageRgb32(width, height);
    }
}