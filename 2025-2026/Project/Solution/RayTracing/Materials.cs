namespace RayTracing;

public abstract class Material : IDisposable
{
    internal readonly MaterialSafeHandle Handle;

    protected Material(MaterialSafeHandle handle)
    {
        Handle = handle;
    }

    public void Dispose()
    {
        Handle.Dispose();
    }
}

public class Lambertian : Material
{
    public Lambertian(double r, double g, double b) 
        : base(NativeMethods.CreateLambertian(r, g, b)) { }
}

public class Metal : Material
{
    public Metal(double r, double g, double b, double fuzz) 
        : base(NativeMethods.CreateMetal(r, g, b, fuzz)) { }
}

public class Dielectric : Material
{
    public Dielectric(double refractionIndex) 
        : base(NativeMethods.CreateDielectric(refractionIndex)) { }
}