using System.Net;
using System.Runtime.InteropServices;
using ImGuiNET;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Quack.Engine.Extra;

namespace Quack.Server;

public class QuackWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
    : ImGuiGameWindow(gameWindowSettings, nativeWindowSettings)
{
    private Overlay AddressesOverlay { get; set; } = null!;
    private Overlay HostnameOverlay { get; set; } = null!;
    private FpsCounter FpsCounter { get; set; } = null!;
    private GameServer Server { get; set; } = null!;
    private Scoreboard Scoreboard { get; set; } = null!;
    private List<string> Addresses { get; } = [];
    private string Hostname { get; } = Dns.GetHostName();

    private DebugProc DebugProcCallback { get; } = OnDebugMessage;

    protected override void OnWindowLoad()
    {
        GL.DebugMessageCallback(DebugProcCallback, IntPtr.Zero);
        GL.Enable(EnableCap.DebugOutput);

#if DEBUG
        GL.Enable(EnableCap.DebugOutputSynchronous);
#endif

        AddressesOverlay = new Overlay(new Vector2(0, 10), () =>
        {
            foreach (var address in Addresses)
            {
                ImGui.TextUnformatted(address);
            }
        }, Anchor.TopCenter);
        HostnameOverlay = new Overlay(new Vector2(10, 10), () =>
        {
            ImGui.TextUnformatted(Hostname);
        }, Anchor.TopLeft);
        Scoreboard = new Scoreboard();
        FpsCounter = new FpsCounter();

        Server = new GameServer();
        _ = Server.StartAsync();
        Addresses.AddRange(Server.GetServerIPs());
        
        GL.ClearColor(0.4f, 0.7f, 0.9f, 1.0f);
        GL.Disable(EnableCap.CullFace);
        GL.Enable(EnableCap.DepthTest);
        GL.DepthFunc(DepthFunction.Lequal);
    }

    protected override void OnWindowUnload()
    {
        Server.Dispose();
        ResourcesManager.Instance.Dispose();
    }

    protected override void OnWindowResize(ResizeEventArgs e)
    {
        GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);
        Server.Scene.UpdateViewport(ClientSize.X, ClientSize.Y);
    }

    private double _lastUpdateStateMs = 0.000;
    private const double UpdateStateTimeMs = 0.045;
    protected override void OnUpdate(double time)
    {
        FpsCounter.Update(time);

        Server.Update((float)time);
        Scoreboard.Update(Server.Scene);

        _lastUpdateStateMs += time;
        if (_lastUpdateStateMs > UpdateStateTimeMs)
        {
            _lastUpdateStateMs -= UpdateStateTimeMs;
            if (_lastUpdateStateMs > UpdateStateTimeMs) _lastUpdateStateMs = 0; 
            
            _ = Server.BroadcastUpdateStateAsync();
        }

        if (ImGui.GetIO().WantCaptureMouse) return;

        var keyboard = KeyboardState.GetSnapshot();
        var mouse = MouseState.GetSnapshot();

        Server.Scene.Camera.HandleInput((float)time, keyboard, mouse);

        if (keyboard.IsKeyDown(Keys.Escape)) Close();
    }

    protected override void OnRender(double time)
    {
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        
        FpsCounter.Render();
        Server.Scene.Render();
        RenderGui();

        Context.SwapBuffers();
    }
    
    protected override void RenderGui()
    {
        AddressesOverlay.Render();
        HostnameOverlay.Render();
        if (KeyboardState.IsKeyDown(Keys.Tab)) Scoreboard.Render();
        
        base.RenderGui();
    }

    private static void OnDebugMessage(
        DebugSource source,     // Source of the debugging message.
        DebugType type,         // Type of the debugging message.
        int id,                 // ID associated with the message.
        DebugSeverity severity, // Severity of the message.
        int length,             // Length of the string in pMessage.
        IntPtr pMessage,        // Pointer to message string.
        IntPtr pUserParam)      // The pointer you gave to OpenGL.
    {
        var message = Marshal.PtrToStringAnsi(pMessage, length);

        var log = $"[{severity} source={source} type={type} id={id}] {message}";

        Console.WriteLine(log);
    }
}