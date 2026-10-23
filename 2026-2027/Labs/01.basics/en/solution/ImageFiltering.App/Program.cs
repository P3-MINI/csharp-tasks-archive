#define STAGE1

using ImageFiltering.Core;
using SixLabors.ImageSharp.PixelFormats;

namespace ImageFiltering.App;

public class Program
{
    public static void Main(string[] args)
    {
#if STAGE1
        string inputPath = ResolvePath("Lena_512.png");
        string outputPath = Path.Combine(AppContext.BaseDirectory, "LenaFiltered.png");
        Image image = ReadImage(inputPath, (w, h) => new ImageRgb32(w, h));

        // var pipeline = new FilterSharpen() +
        //                new FilterLaplacian() +
        //                new FilterBoxBlur() +
        //                new FilterGaussianBlur();
        // image = pipeline.Apply(image);

        WriteImage(outputPath, image);
#else
        Console.WriteLine("This is sandbox environment for you to test your code.");
#endif
    }

    private static string ResolvePath(string path)
    {
        if (File.Exists(path)) return path;
        string appDirFile = Path.Combine(AppContext.BaseDirectory, path);
        if (File.Exists(appDirFile)) return appDirFile;
        return path;
    }
#if STAGE1
    public static Image ReadImage(string path, Func<int, int, Image> createImage)
    {
        using var image = SixLabors.ImageSharp.Image.Load<Rgb24>(path);
        var result = createImage(image.Width, image.Height);
        image.ProcessPixelRows(accessor =>
        {
            for (int y = 0; y < accessor.Height; y++)
            {
                Span<Rgb24> row = accessor.GetRowSpan(y);
                for (int x = 0; x < accessor.Width; x++)
                {
                    ref readonly Rgb24 pixel = ref row[x];
                    var color = new ColorRgb(
                        pixel.R / 255.0f,
                        pixel.G / 255.0f,
                        pixel.B / 255.0f
                    );
                    result[x, y] = color;
                }
            }
        });
        return result;
    }
    
    public static void WriteImage(string path, Image image)
    {
        using var sharpImage = new SixLabors.ImageSharp.Image<Rgb24>(image.Width, image.Height);

        sharpImage.ProcessPixelRows(accessor =>
        {
            for (int y = 0; y < accessor.Height; y++)
            {
                Span<Rgb24> row = accessor.GetRowSpan(y);
                for (int x = 0; x < accessor.Width; x++)
                {
                    ColorRgb color = image[x, y];
                    byte r = (byte)Math.Clamp((int)MathF.Round(color.Red * 255f), 0, 255);
                    byte g = (byte)Math.Clamp((int)MathF.Round(color.Green * 255f), 0, 255);
                    byte b = (byte)Math.Clamp((int)MathF.Round(color.Blue * 255f), 0, 255);
                    row[x] = new Rgb24(r, g, b);
                }
            }
        });
        
        SixLabors.ImageSharp.ImageExtensions.SaveAsPng(sharpImage, path);
    }
#endif
}