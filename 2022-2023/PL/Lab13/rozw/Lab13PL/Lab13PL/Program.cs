using System.Runtime.InteropServices;
using ImGuiNET;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using ShaderType = OpenTK.Graphics.OpenGL4.ShaderType;
using System;
using System.Globalization;
using Lab13PL.ImGuiUtils;

namespace Lab13PL
{
    public class Program : GameWindow
    {
        public bool IsLoaded { get; private set; }

        private Shader shader, water;
        private ImGuiController controller;
        private Mesh rectangle;
        private Camera camera;
        private Texture texture;
        private Scene scene;
        private static DebugProc _debugProcCallback = OnDebugMessage;
        private static GCHandle _debugProcCallbackHandle;
        public static void Main(string[] args)
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            using var program = new Program(GameWindowSettings.Default, NativeWindowSettings.Default);
            program.Title = "Lab13PL";
            program.Size = new Vector2i(1280, 800);
            program.Run();
        }

        public Program(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings) { }

        protected override void OnLoad()
        {
            base.OnLoad();

            _debugProcCallbackHandle = GCHandle.Alloc(_debugProcCallback);
            GL.DebugMessageCallback(_debugProcCallback, IntPtr.Zero);
            GL.Enable(EnableCap.DebugOutput);

            shader = new Shader(("shader.vert", ShaderType.VertexShader), ("shader.frag", ShaderType.FragmentShader));
            water = new Shader(("water.vert", ShaderType.VertexShader), ("water.frag", ShaderType.FragmentShader));
            controller = new ImGuiController(ClientSize.X, ClientSize.Y);

            camera = new Camera(new FirstPersonControl(), new PerspectiveView());
            camera.Move(0, 2, -5);

            scene = new Scene();

            float[] vertices = {
            5f, 0f,  5f,
            5f, 0f, -5f,
            -5f, 0f, -5f,
            -5f, 0f,  5f
        };
            float[] texCoords = {
            0.0f, 0.0f,
            0.0f, 10.0f,
            10.0f, 10.0f,
            10.0f, 0.0f

        };
            int[] indices = {
            0, 1, 3,
            1, 2, 3
        };
            rectangle = new Mesh(PrimitiveType.Triangles, indices, (vertices, 0, 3), (texCoords, 1, 2));

            texture = new Texture("water.jpg");
            texture.ApplyOptions(Texture.Options.Default
                .SetParameter(TextureParameterName.TextureWrapR, TextureWrapMode.Repeat)
                .SetParameter(TextureParameterName.TextureWrapS, TextureWrapMode.Repeat)
                .SetParameter(TextureParameterName.TextureWrapT, TextureWrapMode.Repeat)
                .SetParameter(TextureParameterName.TextureMagFilter, TextureMagFilter.Nearest)
                .SetParameter(TextureParameterName.TextureMinFilter, TextureMinFilter.Nearest));

            GL.ClearColor(0.4f, 0.7f, 0.9f, 1.0f);
            GL.Disable(EnableCap.CullFace);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Lequal);

            IsLoaded = true;
        }

        protected override void OnUnload()
        {
            base.OnUnload();

            rectangle.Dispose();
            controller.Dispose();
            texture.Dispose();
            shader.Dispose();

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
            controller.WindowResized(ClientSize.X, ClientSize.Y);
            camera.Aspect = (float)Size.X / Size.Y;
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            controller.Update(this, (float)args.Time);

            if (ImGui.GetIO().WantCaptureMouse) return;

            var keyboard = KeyboardState.GetSnapshot();
            var mouse = MouseState.GetSnapshot();

            camera.HandleInput(keyboard, mouse, (float)args.Time);

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

            RenderScene(scene);

            RenderGui();

            Context.SwapBuffers();
        }

        public void RenderWater()
        {
            water.Use();
            texture.Bind();
            water.LoadInteger("sampler", 0);
            water.LoadMatrix4("mvp", camera.GetProjectionViewMatrix());
            rectangle.Render();
        }

        private void RenderScene(Scene scene)
        {
            foreach (var model in scene.Models)
            {
                if (model.Mesh == null || model.Texture == null) continue;
                shader.Use();
                model.Texture.Bind();
                shader.LoadInteger("tex", 0);
                Matrix4 modelMatrix = Matrix4.CreateScale(model.Scale) *
                                      Matrix4.CreateFromQuaternion(model.Rotation) *
                                      Matrix4.CreateTranslation(model.Position);
                shader.LoadMatrix4("model", modelMatrix);
                shader.LoadMatrix4("mvp", modelMatrix * camera.GetProjectionViewMatrix());
                model.Mesh.Render();
            }
        }

        private Exception lastException = null;
        private void RenderGui()
        {
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
                            duck.Position = new Vector3(10 * (float)random.NextDouble() - 5, 0, 10 * (float)random.NextDouble() - 5);
                            duck.Rotation = new Quaternion(0, 2 * (float)random.NextDouble() * MathF.PI, 0);
                            duck.Scale = new Vector3(0.005f + 0.001f * (float)random.NextDouble());
                            scene.Models.Add(duck);
                        }
                        catch (Exception e)
                        {
                            lastException = e;
                        }
                    }
                }
                if (ImGui.CollapsingHeader("Stage 2"))
                {
                    if (ImGui.Button("Try Load Non-Existing Model"))
                    {
                        try
                        {
                            Model cow = new Model("cow");
                        }
                        catch (Exception e)
                        {
                            lastException = e;
                        }
                    }
                }
                if (ImGui.CollapsingHeader("Stage 3"))
                {
                    if (ImGui.Button("Serialize"))
                    {
                        try
                        {
                            scene.Serialize("Scene");
                        }
                        catch (Exception e)
                        {
                            lastException = e;
                        }
                    }
                }
                if (ImGui.CollapsingHeader("Stage 4"))
                {
                    if (ImGui.Button("Clean Scene"))
                    {
                        scene.Clear();
                    }
                    if (ImGui.Button("Deserialize"))
                    {
                        try
                        {
                            scene.Deserialize("Scene");
                        }
                        catch (Exception e)
                        {
                            lastException = e;
                        }
                    }
                }
                if (lastException != null)
                {
                    ImGui.Text("Exception thrown");
                    ImGui.BulletText(lastException.ToString());
                }
                ImGui.End();
            }

            controller.Render();
        }

        protected override void OnTextInput(TextInputEventArgs e)
        {
            base.OnTextInput(e);

            controller.PressChar((char)e.Unicode);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            controller.MouseScroll(e.Offset);
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
            string message = Marshal.PtrToStringAnsi(pMessage, length);

            string log = $"[{severity} source={source} type={type} id={id}] {message}";

            Console.WriteLine(log);
        }
    }
}