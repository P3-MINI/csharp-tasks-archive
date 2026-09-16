using System.Diagnostics;
using RayTracing;

namespace RayTracingDemo;

class Program
{
    static void Main(string[] args)
    {
        const int width = 800;
        const int height = 450;

        Windowing.Viewer.Show(width, height, "Ray Tracing in One Weekend", window =>
        {
            Render(width, height, window);
        });
    }

    static void Render(int width, int height, Windowing.IWindowUpdate window)
    {
        const int samples = 500;
        const int maxDepth = 10;

        var cam = Camera.CreateDefault();
        cam.AspectRatio = (double)width / height;
        cam.ImageWidth = width;
        cam.SamplesPerPixel = samples;
        cam.MaxDepth = maxDepth;
        cam.VFov = 20;
        cam.LookFrom = new Vec3(13, 2, 3);
        cam.LookAt = new Vec3(0, 0, 0);
        cam.Vup = new Vec3(0, 1, 0);
        cam.DefocusAngle = 0.6;
        cam.FocusDist = 10.0;

        using var scene = new Scene();

        var groundMaterial = new Lambertian(0.5, 0.5, 0.5);
        scene.AddSphere(0, -1000, 0, 1000, groundMaterial);

        var rnd = new Random(1337);
        for (int a = -11; a < 11; a++)
        {
            for (int b = -11; b < 11; b++)
            {
                var chooseMat = rnd.NextDouble();
                var center = new Vec3(a + 0.9 * rnd.NextDouble(), 0.2, b + 0.9 * rnd.NextDouble());

                if ((new Vec3(center.X - 4, center.Y - 0.2, center.Z - 0).Length()) > 0.9)
                {
                    Material sphereMaterial;
                    if (chooseMat < 0.8)
                    {
                        // diffuse
                        var albedo = new Vec3(rnd.NextDouble() * rnd.NextDouble(), rnd.NextDouble() * rnd.NextDouble(), rnd.NextDouble() * rnd.NextDouble());
                        sphereMaterial = new Lambertian(albedo.X, albedo.Y, albedo.Z);
                    }
                    else if (chooseMat < 0.95)
                    {
                        // metal
                        var albedo = new Vec3(0.5 * (1 + rnd.NextDouble()), 0.5 * (1 + rnd.NextDouble()), 0.5 * (1 + rnd.NextDouble()));
                        var fuzz = 0.5 * rnd.NextDouble();
                        sphereMaterial = new Metal(albedo.X, albedo.Y, albedo.Z, fuzz);
                    }
                    else
                    {
                        // glass
                        sphereMaterial = new Dielectric(1.5);
                    }
                    scene.AddSphere(center.X, center.Y, center.Z, 0.2, sphereMaterial);
                }
            }
        }

        var material1 = new Dielectric(1.5);
        scene.AddSphere(0, 1, 0, 1.0, material1);

        var material2 = new Lambertian(0.4, 0.2, 0.1);
        scene.AddSphere(-4, 1, 0, 1.0, material2);

        var material3 = new Metal(0.7, 0.6, 0.5, 0.0);
        scene.AddSphere(4, 1, 0, 1.0, material3);
        
        cam.LookAt = new Vec3(0, 1, 0);
        var dist = Math.Sqrt(Math.Pow(cam.LookFrom.X - cam.LookAt.X, 2) + Math.Pow(cam.LookFrom.Y - cam.LookAt.Y, 2) + Math.Pow(cam.LookFrom.Z - cam.LookAt.Z, 2));
        cam.FocusDist = dist;

        byte[] buffer = new byte[width * height * 4];

        window.UpdateStatus("Starting render...");
        var sw = Stopwatch.StartNew();

        scene.Render(cam, buffer, (samplesDone, data) =>
        {
            if (window.IsClosed) return;
            
            window.UpdateStatus($"Rendering sample {samplesDone}/{samples} ({sw.ElapsedMilliseconds}ms)");
            window.UpdateImage(data);
        });

        sw.Stop();
        if (window.IsClosed) return; // Exit if closed
        window.UpdateStatus($"Rendering complete in {sw.ElapsedMilliseconds}ms");

        Scene.SavePng("output.png", width, height, buffer);
        Console.WriteLine("Saved to output.png");
    }
}