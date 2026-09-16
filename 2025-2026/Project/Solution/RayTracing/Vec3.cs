using System.Runtime.InteropServices;

namespace RayTracing;

[StructLayout(LayoutKind.Sequential)]
public struct Vec3
{
    public double X;
    public double Y;
    public double Z;

    public Vec3(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public double Length() => Math.Sqrt(X * X + Y * Y + Z * Z);
}
