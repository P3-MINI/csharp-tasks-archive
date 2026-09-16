using System.Runtime.InteropServices;

namespace RayTracing;

[StructLayout(LayoutKind.Sequential)]
public struct Camera
{
    public double AspectRatio;
    public int ImageWidth;
    public int SamplesPerPixel;
    public int MaxDepth;

    public double VFov;
    public Vec3 LookFrom;
    public Vec3 LookAt;
    public Vec3 Vup;

    public double DefocusAngle;
    public double FocusDist;

    public static Camera CreateDefault()
    {
        return new Camera
        {
            AspectRatio = 1.0,
            ImageWidth = 100,
            SamplesPerPixel = 10,
            MaxDepth = 10,
            VFov = 90,
            LookFrom = new Vec3(0, 0, 0),
            LookAt = new Vec3(0, 0, -1),
            Vup = new Vec3(0, 1, 0),
            DefocusAngle = 0,
            FocusDist = 10
        };
    }
}
