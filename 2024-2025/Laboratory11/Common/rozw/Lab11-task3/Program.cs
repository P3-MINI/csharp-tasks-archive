#define TASK3_MULTI_THREAD
#define TASK3_TASK
#define TASK4_PARALLEL

using System.Diagnostics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Lab11_task3;


internal class Program
{
    static void Main()
    {
        const int Width = 800;
        const int Height = 800;

        {
            using Image<Rgba32> image = new Image<Rgba32>(Width, Height);

            Console.WriteLine("Generating fractal with one thread...");

            Stopwatch stopwatch = Stopwatch.StartNew();
            FractalGenerator.GenerateFractalSingleThread(image);
            stopwatch.Stop();

            Console.WriteLine($"Single thread: {stopwatch.ElapsedMilliseconds} ms");
            image.Save("Fractal_SingleThread.png");

        }

#if TASK3_MULTI_THREAD
        {
            using Image<Rgba32> image = new Image<Rgba32>(Width, Height);

            Console.WriteLine("Generating fractal with multiple threads...");

            Stopwatch stopwatch = Stopwatch.StartNew();
            FractalGenerator.GenerateFractalMultiThread(image);
            stopwatch.Stop();

            Console.WriteLine($"Multi-thread: {stopwatch.ElapsedMilliseconds} ms");
            image.Save("Fractal_MultiThread.png");
        }
#endif

#if TASK3_TASK
        {
            using Image<Rgba32> image = new Image<Rgba32>(Width, Height);

            Console.WriteLine("Generating fractal with Tasks...");

            Stopwatch stopwatch = Stopwatch.StartNew();
            FractalGenerator.GenerateFractalWithTasks(image);
            stopwatch.Stop();

            Console.WriteLine($"Tasks: {stopwatch.ElapsedMilliseconds} ms");
            image.Save("Fractal_Tasks.png");
        }
#endif

#if TASK4_PARALLEL
        {
            using Image<Rgba32> image = new Image<Rgba32>(Width, Height);

            Console.WriteLine("Generating fractal with Parallel...");

            Stopwatch stopwatch = Stopwatch.StartNew();
            FractalGenerator.GenerateFractalParallel(image);
            stopwatch.Stop();

            Console.WriteLine($"Parallel: {stopwatch.ElapsedMilliseconds} ms");
            image.Save("Fractal_Parallel.png");
        }
#endif
    }
}
