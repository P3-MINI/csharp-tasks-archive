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

        int width = image.Width;
        int height = image.Height;
        int threadCount = Environment.ProcessorCount;
        Thread[] threads = new Thread[threadCount];
        int rowsPerThread = height / threadCount;

        for (int t = 0; t < threadCount; t++)
        {
            int startRow = t * rowsPerThread;
            int endRow = (t == threadCount - 1) ? height : startRow + rowsPerThread;

            threads[t] = new Thread(() =>
            {   
                for (int y = startRow; y < endRow; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        double a = (x - width / 2.0) * 4.0 / width;
                        double b = (y - height / 2.0) * 4.0 / height;
                        int iterations = MandelbrotSet.Calculate(a, b);
                        image[x, y] = GetColor(iterations);
                    }
                }
            });
            threads[t].Start();
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }

    }

    public static void GenerateFractalWithTasks(Image<Rgba32> image)
    {
        // TODO: Create n tasks, where n is equal to number of the processor cores
        //       Split equally job among all threads

        int width = image.Width;
        int height = image.Height;
        int taskCount = Environment.ProcessorCount;
        int rowsPerTask = height / taskCount;

        Task[] tasks = new Task[taskCount];
        for (int t = 0; t < taskCount; t++)
        {
            int startRow = t * rowsPerTask;
            int endRow = (t == taskCount - 1) ? height : startRow + rowsPerTask;

            tasks[t] = Task.Run(() =>
            {
                for (int y = startRow; y < endRow; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        double a = (x - width / 2.0) * 4.0 / width;
                        double b = (y - height / 2.0) * 4.0 / height;
                        int iterations = MandelbrotSet.Calculate(a, b);
                        image[x, y] = GetColor(iterations);
                    }
                }
            });
        }

        Task.WaitAll(tasks);
    }

    public static void GenerateFractalParallel(Image<Rgba32> image)
    {
        // TODO: Use Parallel class to calculate pixel values

        int width = image.Width;
        int height = image.Height;

        Parallel.For(0, height, y =>
        {
            for (int x = 0; x < width; x++)
            {
                double a = (x - width / 2.0) * 4.0 / width;
                double b = (y - height / 2.0) * 4.0 / height;
                int iterations = MandelbrotSet.Calculate(a, b);
                image[x, y] = GetColor(iterations);
            }
        });
    }

}