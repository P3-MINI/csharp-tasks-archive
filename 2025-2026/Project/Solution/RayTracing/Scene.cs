namespace RayTracing;

public class Scene : IDisposable
{
    private readonly SceneSafeHandle _handle;
    private readonly List<Material> _materials = new();

    public Scene()
    {
        _handle = NativeMethods.CreateScene();
    }

    public void AddSphere(double cx, double cy, double cz, double radius, Material material)
    {
        _materials.Add(material); // Keep reference to prevent GC
        NativeMethods.AddSphere(_handle, cx, cy, cz, radius, material.Handle);
    }

    public void Render(Camera camera, byte[] buffer, SafeRenderCallback? onProgress = null)
    {
        RenderCallback? callback = null;
        if (onProgress != null)
        {
            int length = buffer.Length;
            callback = (samples, bufPtr) => 
            {
                unsafe 
                {
                    // Construct a span from the pointer provided by the native callback
                    var span = new ReadOnlySpan<byte>((void*)bufPtr, length);
                    onProgress(samples, span);
                }
            };
        }
        
        NativeMethods.RenderScene(_handle, camera, buffer, callback);
    }

    public static void SavePng(string filename, int width, int height, byte[] buffer)
    {
        NativeMethods.SavePng(filename, width, height, buffer);
    }

    public void Dispose()
    {
        _handle.Dispose();
        _materials.Clear();
    }
}
