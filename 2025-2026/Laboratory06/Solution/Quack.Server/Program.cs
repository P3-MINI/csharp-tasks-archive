using System.Reflection;
using System.Runtime.InteropServices;
using Assimp;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Quack.Server;

public static class Program
{
    public static void Main(string[] args)
    {
        NativeLibrary.SetDllImportResolver(typeof(AssimpContext).Assembly, (libraryName, assembly, searchPath) =>
        {
            if (libraryName == "libdl.so")
            {
                return NativeLibrary.Load("libdl.so.2", assembly, searchPath);
            }
            return IntPtr.Zero;
        });

        var gwSettings = GameWindowSettings.Default;
        var nwSettings = NativeWindowSettings.Default;
        nwSettings.NumberOfSamples = 16;
        nwSettings.Vsync = VSyncMode.On;

#if DEBUG
        nwSettings.Flags |= ContextFlags.Debug;
#endif

        using var quack = new QuackWindow(gwSettings, nwSettings);
        quack.Title = "Quack III Arena";
        quack.ClientSize = new Vector2i(1280, 800);
        quack.Run();
    }
}