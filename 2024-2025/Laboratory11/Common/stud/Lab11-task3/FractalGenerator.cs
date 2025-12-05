using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Lab11_task3;

public class FractalGenerator
{
    public static Color GetColor(int iterations)
    {
        if (iterations == MandelbrotSet.MaxIterations)
            return Color.Black;

        byte c = (byte)(255 - (iterations * 255 / MandelbrotSet.MaxIterations));
        return Color.FromRgb(c, c, c);
    }

    public static void GenerateFractalSingleThread(Image<Rgba32> image)
    {
        int width = image.Width;
        int height = image.Height;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                double a = (x - width / 2.0) * 4.0 / width;
                double b = (y - height / 2.0) * 4.0 / height;
                int iterations = MandelbrotSet.Calculate(a, b);
                image[x, y] = GetColor(iterations);
            }
        }
    }

    public static void GenerateFractalMultiThread(Image<Rgba32> image)
    {
        // TODO: Create n threads, where n is equal to number of the processor cores
        //       Split equally job among all threads
        throw new NotImplementedException();
    }

    public static void GenerateFractalWithTasks(Image<Rgba32> image)
    {
        // TODO: Create n tasks, where n is equal to number of the processor cores
        //       Split equally job among all threads
        throw new NotImplementedException();
    }

    public static void GenerateFractalParallel(Image<Rgba32> image)
    {
        // TODO: Use Parallel class to calculate pixel values
        throw new NotImplementedException();
    }

}