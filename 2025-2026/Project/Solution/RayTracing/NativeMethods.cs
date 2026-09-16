using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace RayTracing;

internal static partial class NativeMethods
{
    private const string LibName = "rt";

    [LibraryImport(LibName)]
    public static partial SceneSafeHandle CreateScene();

    [LibraryImport(LibName)]
    public static partial void DestroyScene(IntPtr scene);

    [LibraryImport(LibName)]
    public static partial MaterialSafeHandle CreateLambertian(double r, double g, double b);

    [LibraryImport(LibName)]
    public static partial MaterialSafeHandle CreateMetal(double r, double g, double b, double fuzz);

    [LibraryImport(LibName)]
    public static partial MaterialSafeHandle CreateDielectric(double refractionIndex);

    [LibraryImport(LibName)]
    public static partial void DestroyMaterial(IntPtr mat);

    [LibraryImport(LibName)]
    public static partial void AddSphere(SceneSafeHandle scene, double cx, double cy, double cz, double radius, MaterialSafeHandle mat);

    [LibraryImport(LibName)]
    public static partial void RenderScene(SceneSafeHandle scene, Camera cam, byte[] buffer, RenderCallback? callback);

    [LibraryImport(LibName, StringMarshalling = StringMarshalling.Utf8)]
    public static partial int SavePng(string filename, int width, int height, byte[] buffer);
}

public class SceneSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    public SceneSafeHandle() : base(true) { }

    protected override bool ReleaseHandle()
    {
        NativeMethods.DestroyScene(handle);
        return true;
    }
}

public class MaterialSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    public MaterialSafeHandle() : base(true) { }

    protected override bool ReleaseHandle()
    {
        NativeMethods.DestroyMaterial(handle);
        return true;
    }
}
