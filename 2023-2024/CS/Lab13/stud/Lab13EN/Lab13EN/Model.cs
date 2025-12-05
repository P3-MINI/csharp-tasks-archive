using System.Numerics;
using System.Runtime.Serialization;
using System.Text;

namespace Lab13EN;

public class Model : IDisposable
{
    public string Name { get; set; }
    public string MeshPath { get; set; }
    public string TexturePath { get; set; }

    public Vector3 Position { get; set; } = Vector3.Zero;
    public Quaternion Rotation { get; set; } = Quaternion.Identity;
    public Vector3 Scale { get; set; } = Vector3.One;
    public Spline Spline { get; set; } = new();

    public Mesh? Mesh { get; private set; }
    public Texture? Texture { get; private set; }

    public Model()
    {
        Name = "";
        MeshPath = "";
        TexturePath = "";
    }
    
    public Model(string name)
    {
        Name = name;
        MeshPath = $"Resources/{name}.mesh";
        TexturePath = $"Resources/{name}.tex";
        LoadMesh();
        LoadTexture();
        Update(0);
    }

    public void Update(float dt)
    {
        Spline.Update(dt);
        var position = Spline.GetPosition();
        var rotation = Spline.GetRotation();
        Position = new Vector3(position.X, 0, position.Y);
        Rotation = Quaternion.CreateFromYawPitchRoll(rotation, 0, 0);
    }

    // TODO: Stage 1a (1pt)
    // Load the model mesh file located at MeshPath.
    // The model mesh consists of a list of vertices and a list of faces.
    // Each vertex has 8 attributes:
    // 
    // - 3 floating-point numbers specifying the vertex position
    // - 3 floating-point numbers specifying the vertex normal vector
    // - 2 floating-point numbers specifying the texture coordinates of the vertex
    // 
    // Faces are triangles with 3 indices corresponding to the vertices in the vertex list.
    // 
    // The loaded files will be in a fictional binary .mesh format:
    // - The file is binary with ASCII encoding and contains entries for vertices and triangles.
    // - Each entry starts with an 8-bit ASCII character: 'v' for vertices and 't' for triangles.
    // - Vertices entries are followed by position (X, Y, Z), normal (X, Y, Z), and texture coordinates (X, Y), 
    //   all eight numbers encoded as a single-precision floating-point number.
    // - Triangles entries are followed by three vertex indices (X, Y, Z), encoded as a 32-bit unsigned integers.
    // 
    // After reading the entire file, create a new model mesh (remove a line that creates a cube mesh):
    // Mesh = new Mesh(positions, normals, textures, triangles);
    public void LoadMesh()
    {
        // var positions = new List<(float x, float y, float z)>();
        // var normals = new List<(float x, float y, float z)>();
        // var textures = new List<(float u, float v)>();
        // var triangles = new List<(uint a, uint b, uint c)>();
        // Fill lists using the file at path MeshPath
        // Mesh = new Mesh(positions, normals, textures, triangles);

        Mesh = Mesh.CreateCube(100);
    }

    // TODO: Stage 1b (1pt)
    // Load the model texture file located at TexturePath.
    // 
    // The loaded files will be in a fictional .tex format:
    // - The file is text-based and contains the width and height of the texture followed by RGB color data for each pixel.
    // - The first line of the file contains width and height values separated with 'x' character.
    //   - Example: 512x128
    // - Following the header, each subsequent line represents RGB color values for a pixel. 
    //   There should be exactly width*height subsequent lines.
    // - RGB values are space-separated and expressed as floating-point numbers between 0.0 and 1.0.
    // - To convert a floating-point value in the range [0.0, 1.0] to a byte value in the range [0, 255], 
    //   you can multiply the floating-point value by 255 and cast the result to a byte.
    // 
    // After reading the file, create a new model texture:
    // Texture = new Texture(data, width, height);
    // 
    // An example file can be found in Resources/duck.tex.
    public void LoadTexture()
    {
        var width = 1;
        var height = 1;

        var data = new byte[] {255, 127, 0};

        Texture = new Texture(data, width, height);
    }

    public void Dispose()
    {
        Mesh?.Dispose();
        Texture?.Dispose();
        GC.SuppressFinalize(this);
    }

    //TODO: Stage 2 (1pt)
    // Create a class TextureParseException inheriting from Exception.
    // This class should have two constructors:
    // - Accepting a filename (string filename), line number (int line), where the problem occurred, and a message (string message).
    // - Additionally accepting the exception that caused this exception (Exception innerException).
    // 
    // - The constructors should call the appropriate constructors from the base Exception class.
    // - The exception message should be formatted as follows:
    //   $"Exception while parsing "{filename}"({line}): {message}"
    //     
    // In the LoadTexture method, throw appropriate exceptions for the following parsing problems:
    // - Empty file, unexpected EOF
    // - Missing texture data, unexpected EOF
    // - Excessive texture data
    // - Incorrect number of dimensions
    // - Failed to parse color component
    // - Dimensions must be greater than 0
    // - Incorrect number of color components
    // - Color component out of range
    // - Failed to parse dimensions
}