using System.Diagnostics;
using System.Runtime.InteropServices;
using ImGuiNET;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using ShaderType = OpenTK.Graphics.OpenGL4.ShaderType;
using System.Globalization;
using Lab13PL.ImGuiUtils;
using Vector2 = System.Numerics.Vector2;

namespace Lab13PL;

public class Program : GameWindow
{
    public bool IsLoaded { get; private set; }
    private DebugProc DebugProcCallback { get; } = OnDebugMessage;

    private Shader? _shader, _water;
    private ImGuiController? _controller;
    private Mesh? _rectangle;
    private Camera? _camera;
    private Texture? _texture;
    private Scene? _scene;
    private Stopwatch sw = new Stopwatch();

    public static void Main()
    {
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        using var program = new Program(GameWindowSettings.Default, NativeWindowSettings.Default);
        program.Title = "Lab13PL";
        program.Size = new Vector2i(1280, 800);
        program.Run();
    }

    public Program(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(
        gameWindowSettings, nativeWindowSettings)
    {
    }

    protected override void OnLoad()
    {
        base.OnLoad();

        GL.DebugMessageCallback(DebugProcCallback, IntPtr.Zero);
        GL.Enable(EnableCap.DebugOutput);

#if DEBUG
        GL.Enable(EnableCap.DebugOutputSynchronous);
#endif

        _shader = new Shader(("shader.vert", ShaderType.VertexShader), ("shader.frag", ShaderType.FragmentShader));
        _water = new Shader(("water.vert", ShaderType.VertexShader), ("water.frag", ShaderType.FragmentShader));
        _controller = new ImGuiController(ClientSize.X, ClientSize.Y);
        _controller.SetupClipboard(this);

        _camera = new Camera(new OrbitingControl(), new PerspectiveView());
        _camera.Move(0, 2, -8);

        _scene = new Scene();

        float[] vertices =
        {
            5f, 0f, 5f,
            5f, 0f, -5f,
            -5f, 0f, -5f,
            -5f, 0f, 5f
        };
        float[] texCoords =
        {
            0.0f, 0.0f,
            0.0f, 10.0f,
            10.0f, 10.0f,
            10.0f, 0.0f
        };
        uint[] indices =
        {
            0, 1, 3,
            1, 2, 3
        };
        _rectangle = new Mesh(PrimitiveType.Triangles, indices, (vertices, 0, 3), (texCoords, 1, 2));

        _texture = new Texture("water_still.png");
        _texture.Bind();
        _texture.ApplyOptions(Texture.Options.Default
            .SetParameter(TextureParameterName.TextureWrapR, TextureWrapMode.Repeat)
            .SetParameter(TextureParameterName.TextureWrapS, TextureWrapMode.Repeat)
            .SetParameter(TextureParameterName.TextureWrapT, TextureWrapMode.Repeat)
            .SetParameter(TextureParameterName.TextureMagFilter, TextureMagFilter.Nearest)
            .SetParameter(TextureParameterName.TextureMinFilter, TextureMinFilter.Nearest));

        GL.ClearColor(0.4f, 0.7f, 0.9f, 1.0f);
        GL.Disable(EnableCap.CullFace);
        GL.Enable(EnableCap.DepthTest);
        GL.DepthFunc(DepthFunction.Lequal);
        
        sw.Start();

        IsLoaded = true;
    }

    protected override void OnUnload()
    {
        base.OnUnload();

        _rectangle?.Dispose();
        _controller?.Dispose();
        _texture?.Dispose();
        _shader?.Dispose();

        IsLoaded = false;
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        if (!IsLoaded)
        {
            return;
        }

        base.OnResize(e);
        GL.Viewport(0, 0, Size.X, Size.Y);
        _controller!.WindowResized(ClientSize.X, ClientSize.Y);
        _camera!.Aspect = (float)Size.X / Size.Y;
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);

        _controller!.Update(this, (float)args.Time);

        foreach (var model in _scene!.Models)
        {
            model.Update((float)args.Time);
        }

        if (ImGui.GetIO().WantCaptureMouse) return;

        var keyboard = KeyboardState.GetSnapshot();
        var mouse = MouseState.GetSnapshot();

        _camera!.HandleInput(keyboard, mouse, (float)args.Time);

        if (keyboard.IsKeyDown(Keys.Escape)) Close();
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);

        GL.Disable(EnableCap.CullFace);
        GL.Enable(EnableCap.DepthTest);
        GL.DepthFunc(DepthFunction.Lequal);

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        RenderWater();

        RenderScene(_scene!);

        RenderGui();

        Context.SwapBuffers();
    }

    public void RenderWater()
    {
        _water!.Use();
        _texture!.Bind();
        _water.LoadInteger("sampler", 0);
        _water.LoadFloat("time", (float)(8 * sw.Elapsed.TotalSeconds % 32.0));
        _water.LoadFloat3("color", new Vector3(0, 0.25f, 1));
        _water.LoadMatrix4("mvp", _camera!.GetProjectionViewMatrix());
        _rectangle!.Render();
    }

    private void RenderScene(Scene scene)
    {
        foreach (var model in scene.Models)
        {
            if (model.Mesh == null || model.Texture == null) continue;
            _shader!.Use();
            model.Texture.Bind();
            _shader.LoadInteger("tex", 0);
            Matrix4 modelMatrix = Matrix4.CreateScale(FromNumerics(model.Scale)) *
                                  Matrix4.CreateFromQuaternion(FromNumerics(model.Rotation)) *
                                  Matrix4.CreateTranslation(FromNumerics(model.Position));
            _shader.LoadMatrix4("model", modelMatrix);
            _shader.LoadMatrix4("mvp", modelMatrix * _camera!.GetProjectionViewMatrix());
            model.Mesh.Render();
        }
    }

    private Exception? _lastException;

    private void RenderGui()
    {
        ImGui.SetNextWindowSize(new Vector2(720, 480), ImGuiCond.Once);
        if (ImGui.Begin("Lab13", ImGuiWindowFlags.HorizontalScrollbar))
        {
            if (ImGui.CollapsingHeader("Stage 1"))
            {
                if (ImGui.Button("Spawn Duck"))
                {
                    try
                    {
                        Random random = new Random();
                        Model duck = new Model("duck");
                        duck.Scale = new System.Numerics.Vector3(0.005f + 0.001f * (float)random.NextDouble());
                        _scene!.Models.Add(duck);
                    }
                    catch (Exception e)
                    {
                        _lastException = e;
                    }
                }
            }

            if (ImGui.CollapsingHeader("Stage 2"))
            {
                if (ImGui.Button("bat.mesh - Incorrect number of vertex attributes"))
                {
                    try
                    {
                        Model unused = new Model("bat");
                    }
                    catch (Exception e)
                    {
                        _lastException = e;
                    }
                }
                if (ImGui.Button("bear.mesh - Incorrect format of vertex attribute"))
                {
                    try
                    {
                        Model unused = new Model("bear");
                    }
                    catch (Exception e)
                    {
                        _lastException = e;
                    }
                }
                if (ImGui.Button("cat.mesh - Incorrect number of triangle indices"))
                {
                    try
                    {
                        Model unused = new Model("cat");
                    }
                    catch (Exception e)
                    {
                        _lastException = e;
                    }
                }
                if (ImGui.Button("cow.mesh - Incorrect format of triangle index"))
                {
                    try
                    {
                        Model unused = new Model("cow");
                    }
                    catch (Exception e)
                    {
                        _lastException = e;
                    }
                }
                if (ImGui.Button("dog.mesh - Incorrect token"))
                {
                    try
                    {
                        Model unused = new Model("dog");
                    }
                    catch (Exception e)
                    {
                        _lastException = e;
                    }
                }
            }

            if (ImGui.CollapsingHeader("Stage 3"))
            {
                if (ImGui.Button("Serialize"))
                {
                    try
                    {
                        _scene!.Serialize("Scene");
                    }
                    catch (Exception e)
                    {
                        _lastException = e;
                    }
                }
            }

            if (ImGui.CollapsingHeader("Stage 4"))
            {
                if (ImGui.Button("Clean Scene"))
                {
                    _scene!.Clear();
                }

                if (ImGui.Button("Deserialize"))
                {
                    try
                    {
                        _scene!.Deserialize("Scene");
                    }
                    catch (Exception e)
                    {
                        _lastException = e;
                    }
                }
            }

            if (_lastException != null)
            {
                string exception = _lastException.ToString();
                int width = exception.Split('\n').MaxBy(s => s.Length)!.Length * 8;
                ImGui.Text("Exception was thrown");
                ImGui.BeginChild("text", new Vector2(-1), false, ImGuiWindowFlags.HorizontalScrollbar);
                ImGui.InputTextMultiline("", ref exception, 0, new Vector2(width, -1),
                    ImGuiInputTextFlags.ReadOnly);
                ImGui.EndChild();
            }

            ImGui.End();
        }

        _controller!.Render();
    }

    protected override void OnTextInput(TextInputEventArgs e)
    {
        base.OnTextInput(e);

        _controller!.PressChar((char)e.Unicode);
    }

    protected override void OnMouseWheel(MouseWheelEventArgs e)
    {
        base.OnMouseWheel(e);

        _controller!.MouseScroll(e.Offset);
    }

    private static void OnDebugMessage(
        DebugSource source, // Source of the debugging message.
        DebugType type, // Type of the debugging message.
        int id, // ID associated with the message.
        DebugSeverity severity, // Severity of the message.
        int length, // Length of the string in pMessage.
        IntPtr pMessage, // Pointer to message string.
        IntPtr pUserParam) // The pointer you gave to OpenGL.
    {
        var message = Marshal.PtrToStringAnsi(pMessage, length);

        var log = $"[{severity} source={source} type={type} id={id}] {message}";

        Console.WriteLine(log);
    }

    private static Vector3 FromNumerics(System.Numerics.Vector3 vector)
    {
        return new Vector3(vector.X, vector.Y, vector.Z);
    }

    private static Quaternion FromNumerics(System.Numerics.Quaternion quaternion)
    {
        return new Quaternion(quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);
    }
}