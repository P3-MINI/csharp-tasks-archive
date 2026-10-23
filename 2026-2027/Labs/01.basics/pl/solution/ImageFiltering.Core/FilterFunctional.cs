namespace ImageFiltering.Core;

public abstract class FilterFunctional : FilterBase
{
    public override Image Apply(Image image)
    {
        Image result = image.CreateImage(image.Width, image.Height);
        for (int y = 0; y < image.Height; y++)
        {
            for (int x = 0; x < image.Width; x++)
            {
                result[x, y] = Transform(image[x, y]);
            }
        }
        
        return result;
    }

    protected virtual ColorRgb Transform(ColorRgb color) => color;
}