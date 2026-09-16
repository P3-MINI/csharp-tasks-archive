using System.Runtime.InteropServices;
using ImGuiNET;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Quack.Engine.Extra;

namespace Quack.Client;

public class QuackWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
    : ImGuiGameWindow(gameWindowSettings, nativeWindowSettings)
{
    private Overlay ScoreOverlay { get; set; } = null!;
    private Overlay HelpOverlay { get; set; } = null!;
    private FpsCounter FpsCounter { get; set; } = null!;
    private GameClient Client { get; set; } = null!;
    private Scoreboard Scoreboard { get; set; } = null!;
    private ConnectWindow ConnectWindow { get; set; } = null!;

    private DebugProc DebugProcCallback { get; } = OnDebugMessage;

    protected override void OnWindowLoad()
    {
        GL.DebugMessageCallback(DebugProcCallback, IntPtr.Zero);
        GL.Enable(EnableCap.DebugOutput);

#if DEBUG
        GL.Enable(EnableCap.DebugOutputSynchronous);
#endif

        ScoreOverlay = new Overlay(new Vector2(0, 10), () =>
        {
            if (Client.Scene?.Duck != null)
                ImGui.TextUnformatted($"Score: {Client.Scene.Duck.Scale * 100:0}");
        }, Anchor.TopCenter);
        HelpOverlay = new Overlay(new Vector2(10, 10), () =>
        {
            ImGui.TextUnformatted("WASD or arrows to swim");
            ImGui.TextUnformatted("Left Shift to swim faster");
            ImGui.TextUnformatted("Tab to view scoreboard");
            ImGui.TextUnformatted("Esc to exit");
        });
        FpsCounter = new FpsCounter();
        
        Client = new GameClient();
        Client.Scene = new Scene();
        ConnectWindow = new ConnectWindow(Client);
        Scoreboard = new Scoreboard();
        
        GL.ClearColor(0.4f, 0.7f, 0.9f, 1.0f);
        GL.Disable(EnableCap.CullFace);
        GL.Enable(EnableCap.DepthTest);
        GL.DepthFunc(DepthFunction.Lequal);
    }

    protected override void OnWindowUnload()
    {
        Client.Dispose();
        ResourcesManager.Instance.Dispose();
    }

    protected override void OnWindowResize(ResizeEventArgs e)
    {
        GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);
        Client.Scene?.UpdateViewport(ClientSize.X, ClientSize.Y);
    }

    private Input? _lastInput;
    protected override void OnUpdate(double time)
    {
        FpsCounter.Update(time);

        Client.Scene?.Update((float)time);
        if (Client.Scene != null) Scoreboard.Update(Client.Scene);

        var keyboard = KeyboardState.GetSnapshot();
        var mouse = MouseState.GetSnapshot();

        if (keyboard.IsKeyDown(Keys.Escape)) Close();

        if (Client.IsConnected)
        {
            var input = Input.Read(keyboard);
            if (input != _lastInput)
            {
                _ = Client.SendInput(input);
                _lastInput = input;
            }
        }
    }

    protected override void OnRender(double time)
    {
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        
        FpsCounter.Render();

        Client.Scene?.Render();
        
        RenderGui();

        Context.SwapBuffers();
    }
    
    protected override void RenderGui()
    {
        ScoreOverlay.Render();
        HelpOverlay.Render();
        ConnectWindow.Render();
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