using System.Net.Mime;
using System.Runtime.InteropServices;
using OpenGL;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Duck;

public class Water : IDisposable
{
    private Mesh Mesh { get; }
    private Shader Shader { get; }
    private Texture Texture { get; }
    private DateTime Time { get; set; }
    private Vector3 Color { get; set; } = new Vector3(0, 0.35f, 1);
    private Vector3 Ambient { get; set; } = Vector3.One;

    public Water()
    {
        Vector4[] vertices =
        [
            new(0, 0, 0, 1),
            new(1, 0, 0, 0),
            new(0, 0, 1, 0),
            new(-1, 0, 0, 0),
            new(0, 0, -1, 0)
        ];
        byte[] indices = {
            0, 1, 2,
            0, 2, 3,
            0, 3, 4,
            0, 4, 1
        };
        IndexBuffer ibo = new IndexBuffer(indices, indices.Length * sizeof(byte), DrawElementsType.UnsignedByte,
            indices.Length);
        VertexBuffer vbo = new VertexBuffer(vertices, vertices.Length * Marshal.SizeOf<Vector4>(), vertices.Length,
            BufferUsageHint.StaticDraw, new VertexBuffer.Attribute(0, 4));
        Mesh = new Mesh(PrimitiveType.Triangles, ibo, vbo);
        Shader = new Shader(("OpenGL.Resources.Shaders.plane.frag", ShaderType.FragmentShader),
            ("OpenGL.Resources.Shaders.plane.vert", ShaderType.VertexShader));
        Texture = new Texture("OpenGL.Resources.Textures.water_still.png", true, 
            Texture.Options.Default
                .SetParameter(new Texture.EnumParameter(TextureParameterName.TextureMinFilter, TextureMinFilter.Linear))
                .SetParameter(new Texture.EnumParameter(TextureParameterName.TextureMagFilter, TextureMagFilter.Nearest)));
    }

    public void Update(DateTime dateTime)
    {
        Ambient = Sky.AmbientColor(dateTime);
        Time = DateTime.Now;
    }

    public unsafe void Render(Camera camera)
    {
        Shader.Use();
        Shader.LoadMatrix4("mvp", camera.ProjectionViewMatrix);
        var viewport = stackalloc int[4];
        GL.GetInteger(GetPName.Viewport, viewport);
        Shader.LoadFloat4("viewport", new Vector4(viewport[0], viewport[1], viewport[2], viewport[3]));
        Shader.LoadMatrix4("invViewProj", camera.ProjectionViewMatrix.Inverted());
        Shader.LoadInteger("sampler", 0);
        Shader.LoadFloat("time", (float)(8 * Time.TimeOfDay.TotalSeconds % 32.0));
        Shader.LoadFloat3("ambient", Ambient);
        Shader.LoadFloat3("color", Color);

        Texture.ActivateUnit();

        Mesh.Bind();
        Mesh.RenderIndexed();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        Mesh.Dispose();
        Shader.Dispose();
        Texture.Dispose();
    }
}