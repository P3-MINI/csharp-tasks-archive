using System.Runtime.InteropServices;
using ImGuiNET;
using OpenGL;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Duck;

public class Program(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
    : ImGuiGameWindow(gameWindowSettings, nativeWindowSettings)
{
    private Camera Camera { get; set; } = null!;
    private Water Water { get; set; } = null!;
    private Sky Sky { get; set; } = null!;
    private Overlay TimeOverlay { get; set; } = null!;
    private Overlay HelpOverlay { get; set; } = null!;
    private Shader Shader { get; set; } = null!;
    private Scene Scene { get; set; } = null!;

    private DebugProc DebugProcCallback { get; } = OnDebugMessage;

    public static void Main(string[] args)
    {
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        var gwSettings = GameWindowSettings.Default;
        var nwSettings = NativeWindowSettings.Default;
        nwSettings.NumberOfSamples = 16;

#if DEBUG
        nwSettings.Flags |= ContextFlags.Debug;
#endif

        using var program = new Program(gwSettings, nwSettings);
        program.Title = "Duck";
        program.Size = new Vector2i(1280, 800);
        program.Run();
    }

    protected override void OnLoad()
    {
        base.OnLoad();

#if DEBUG
        GL.Enable(EnableCap.DebugOutput);
        GL.Enable(EnableCap.DebugOutputSynchronous);
        GL.DebugMessageCallback(DebugProcCallback, IntPtr.Zero);
#endif

        Camera = new Camera(
            new OrbitalControl((Vector3.UnitY  + Vector3.UnitZ ) * 2, Vector3.UnitY * 2), 
            new PerspectiveProjection());

        Sky = new Sky();
        Water = new Water();
        TimeOverlay = new Overlay(new Vector2tk(-10, 10), () => ImGui.Text(Scene.Time.ToString("HH:mm:ss")), Anchor.TopRight);
        HelpOverlay = new Overlay(new Vector2tk(10, 10), () =>
        {
            ImGui.Text("Current objective: survive");
            ImGui.Text("Press Q to Quack");
            ImGui.Text("WASD to Move");
            ImGui.Text("Hold Shift to swim faster");
            ImGui.Text("Hold F to fast-forward time");
            ImGui.Text("Press F5 to quack-save");
            ImGui.Text("Press F9 to quack-load");
        });

        Scene = new Scene("Duck.Resources.ducks.csv");
        Camera.Control = new DuckCameraControl(Scene.Player);

        Shader = new Shader(("OpenGL.Resources.Shaders.shader.frag", ShaderType.FragmentShader),
            ("OpenGL.Resources.Shaders.shader.vert", ShaderType.VertexShader));

        GL.ClearColor(0.4f, 0.7f, 0.9f, 1.0f);
        GL.Disable(EnableCap.CullFace);
        GL.Enable(EnableCap.DepthTest);
        GL.DepthFunc(DepthFunction.Lequal);
    }

    protected override void OnUnload()
    {
        base.OnUnload();

        Water.Dispose();
        Sky.Dispose();
        Shader.Dispose();
        ResourcesManager.Instance.Dispose();
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);
        if (!IsLoaded) return;
        GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);
        Camera.Aspect = (float)ClientSize.X / ClientSize.Y;
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);
        float dt = (float)args.Time;
        
        var keyboard = KeyboardState.GetSnapshot();
        var mouse = MouseState.GetSnapshot();

        if (keyboard.IsKeyDown(Keys.F))
        {
            dt *= 60;
        }

        if (keyboard.IsKeyPressed(Keys.F5))
        {
            Scene.QuackSave();
        }

        if (keyboard.IsKeyPressed(Keys.F9))
        {
            if (Scene.QuackLoad() is { } scene)
            {
                Scene = scene;
                Camera.Control = new DuckCameraControl(Scene.Player);
            }
        }

        Scene.Update(dt, keyboard, mouse);
        Water.Update(Scene.Time);
        Sky.Update(Scene.Time);
        Camera.Update(dt);

        if (ImGui.GetIO().WantCaptureMouse) return;

        Camera.HandleInput(dt, keyboard, mouse);

        if (keyboard.IsKeyDown(Keys.Escape)) Close();
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        Sky.Render(Camera);
        Water.Render(Camera);
        TimeOverlay.Render();
        HelpOverlay.Render();
        RenderScene(Camera);
        RenderGui();

        Context.SwapBuffers();
    }

    void RenderScene(Camera camera)
    {
        Shader.Use();
        foreach (var duck in Scene.GetAllDucks())
        {
            duck.Texture.Bind();
            Shader.LoadInteger("tex", 0);
            Matrix4 modelMatrix = Matrix4.CreateScale(duck.Scale) *
                                  Matrix4.CreateRotationY(duck.Rotation) *
                                  Matrix4.CreateTranslation(duck.Position);
            Shader.LoadMatrix4("model", modelMatrix);
            Shader.LoadMatrix4("mvp", modelMatrix * camera.ProjectionViewMatrix);
            Shader.LoadFloat3("lightDir", Sky.SunDir);
            Shader.LoadFloat3("lightColor", Sky.Ambient);
            duck.Mesh.Bind();
            duck.Mesh.RenderIndexed();
            duck.NameBillboard.Render(Camera);
        }
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