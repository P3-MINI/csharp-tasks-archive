using System.Collections.Concurrent;
using System.Reflection;
using System.Runtime.InteropServices;
using OpenGL;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Duck;

public record struct Vertex(Vector3 Position, Vector3 Normal, Vector2 TexCoord);
public record struct Triangle(Vector3i Indices);

public class ResourcesManager : IDisposable
{
    public static ResourcesManager Instance { get; } = new ();

    private ResourcesManager() {}

    private ConcurrentDictionary<string, Texture> Textures { get; } = new();
    private ConcurrentDictionary<string, Mesh> Meshes { get; } = new();

    public Mesh GetMesh(string path)
    {
        return Meshes.GetOrAdd(path, _ =>
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream(path);
            if (stream == null) throw new FileNotFoundException($"Failed to load resource: {path}");
            using var streamReader = new StreamReader(stream);

            var vertices = new List<Vertex>();
            var triangles = new List<Triangle>();

            while (streamReader.ReadLine() is { } line)
            {
                var tokens = line.Split(" ", StringSplitOptions.TrimEntries);

                if (line.StartsWith('v'))
                {
                    vertices.Add(ParseVertex(tokens));
                }
                if (line.StartsWith('t'))
                {
                    triangles.Add(ParseTriangle(tokens));
                }
            }
            
            return ConstructMesh(vertices.ToArray(), triangles.ToArray());
        });
    }

    private static Vertex ParseVertex(string[] tokens)
    {
        return new Vertex(new Vector3(float.Parse(tokens[1]), float.Parse(tokens[2]), float.Parse(tokens[3])),
            new Vector3(float.Parse(tokens[4]), float.Parse(tokens[5]), float.Parse(tokens[6])),
            new Vector2(float.Parse(tokens[7]), float.Parse(tokens[8])));
    }
    
    private static Triangle ParseTriangle(string[] tokens)
    {
        return new Triangle(new Vector3i(int.Parse(tokens[1]), int.Parse(tokens[2]), int.Parse(tokens[3])));
    }

    private Mesh ConstructMesh(Vertex[] vertices, Triangle[] triangles)
    {
        VertexBuffer vertexBuffer = new VertexBuffer(
            vertices, vertices.Length * Marshal.SizeOf<Vertex>(), vertices.Length, 
            BufferUsageHint.StaticDraw, 
            new VertexBuffer.Attribute(0, 3),
            new VertexBuffer.Attribute(1, 3),
            new VertexBuffer.Attribute(2, 2));
        IndexBuffer indexBuffer =
            new IndexBuffer(triangles, triangles.Length * Marshal.SizeOf<Triangle>(), 
                DrawElementsType.UnsignedInt, triangles.Length * 3);
        return new Mesh(PrimitiveType.Triangles, indexBuffer, vertexBuffer);
    }

    public Texture GetTexture(string path)
    {
        return Textures.GetOrAdd(path, _ =>
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream(path);
            if (stream == null) throw new FileNotFoundException($"Failed to load resource: {path}");
            using var binaryReader = new BinaryReader(stream);

            var width = binaryReader.ReadInt32();
            var height = binaryReader.ReadInt32();

            var data = binaryReader.ReadBytes(3 * width * height);

            return new Texture(data, width, height);
        });
    }

    public void Dispose()
    {
        foreach (var mesh in Meshes)
        {
            mesh.Value.Dispose();
        }

        foreach (var texture in Textures)
        {
            texture.Value.Dispose();
        }
        
        GC.SuppressFinalize(this);
    }
}
