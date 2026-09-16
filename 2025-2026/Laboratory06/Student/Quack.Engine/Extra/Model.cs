using Assimp;
using OpenTK.Graphics.OpenGL4;
using Quack.Engine.Core;
using Mesh = Quack.Engine.Core.Mesh;
using PrimitiveType = OpenTK.Graphics.OpenGL4.PrimitiveType;

namespace Quack.Engine.Extra;

public class Model : IDisposable
{
    public string Path { get; }
    private Mesh Mesh { get; }
    
    private Model(string path, Mesh mesh)
    {
        Path = path;
        Mesh = mesh;
    }

    public void Dispose()
    {
        Mesh.Dispose();
        GC.SuppressFinalize(this);
    }
    
    public void Render()
    {
        Mesh.Bind();
        Mesh.RenderIndexed();
        Mesh.Unbind();
    }

    public static Model Load(string path)
    {
        using var stream = ResourcesUtils.GetResourceStream(path);
        using var context = new AssimpContext();
        
        var scene = context.ImportFileFromStream(
            stream, 
            PostProcessSteps.Triangulate | PostProcessSteps.GenerateNormals, 
            "obj"
        );

        if (scene == null || scene.SceneFlags.HasFlag(SceneFlags.Incomplete) || scene.RootNode == null)
        {
            throw new Exception($"Error importing model from resource: {path}");
        }

        if (scene.MeshCount == 0)
        {
            throw new Exception($"No meshes found in model: {path}");
        }

        var mesh = scene.Meshes[0];
        
        var vertices = new List<float>();

        for (int i = 0; i < mesh.VertexCount; i++)
        {
            var v = mesh.Vertices[i];
            var n = mesh.Normals[i];
            var t = mesh.HasTextureCoords(0) ? mesh.TextureCoordinateChannels[0][i] : new Vector3D(0, 0, 0);
            
            vertices.Add(v.X);
            vertices.Add(v.Y);
            vertices.Add(v.Z);
            
            vertices.Add(n.X);
            vertices.Add(n.Y);
            vertices.Add(n.Z);
            
            vertices.Add(t.X);
            vertices.Add(t.Y);
        }

        var indices = mesh.GetIndices();
        
        var vbo = new VertexBuffer(
            vertices.ToArray(), 
            vertices.Count * sizeof(float), 
            mesh.VertexCount, 
            BufferUsageHint.StaticDraw,
            new VertexBuffer.Attribute(0, 3), // Position (Index 0)
            new VertexBuffer.Attribute(1, 3), // Normal (Index 1)
            new VertexBuffer.Attribute(2, 2)  // TexCoord (Index 2)
        );

        var ibo = new IndexBuffer(
            indices, 
            indices.Length * sizeof(int), 
            DrawElementsType.UnsignedInt, 
            indices.Length
        );

        var glMesh = new Mesh(path, PrimitiveType.Triangles, ibo, vbo);

        return new Model(path, glMesh);
    }
}