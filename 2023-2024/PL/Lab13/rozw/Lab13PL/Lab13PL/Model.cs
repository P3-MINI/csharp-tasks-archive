using System.Numerics;
using System.Text.Json.Serialization;

namespace Lab13PL;

[Serializable]
public class Model : IDisposable, IJsonOnDeserialized
{
    public string Name { get; set; }
    public string MeshPath { get; set; }
    public string TexturePath { get; set; }

    public Vector3 Position { get; set; } = Vector3.Zero;
    public Quaternion Rotation { get; set; } = Quaternion.Identity;
    public Vector3 Scale { get; set; } = Vector3.One;
    public Spline Spline { get; set; } = new();

    [JsonIgnore] public Mesh? Mesh { get; private set; }
    [JsonIgnore] public Texture? Texture { get; private set; }

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
    public void LoadMesh()
    {
        using var streamReader = new StreamReader(MeshPath);

        var positions = new List<(float x, float y, float z)>();
        var normals = new List<(float x, float y, float z)>();
        var textures = new List<(float u, float v)>();
        var triangles = new List<(uint a, uint b, uint c)>();

        int lineNumber = 0;
        while (streamReader.ReadLine() is { } line)
        {
            lineNumber++;
            if (line.StartsWith("v"))
            {
                var tokens = line.Split(" ", StringSplitOptions.TrimEntries);
                if (tokens.Length != 9)
                {
                    throw new MeshParseException(MeshPath, lineNumber, "Incorrect number of vertex attributes");
                }

                try
                {
                    positions.Add((float.Parse(tokens[1]), float.Parse(tokens[2]), float.Parse(tokens[3])));
                    normals.Add((float.Parse(tokens[4]), float.Parse(tokens[5]), float.Parse(tokens[6])));
                    textures.Add((float.Parse(tokens[7]), float.Parse(tokens[8])));
                }
                catch (Exception e)
                {
                    throw new MeshParseException(MeshPath, lineNumber, "Failed to parse vertex attribute", e);
                }
            }
            else if (line.StartsWith("t"))
            {
                var tokens = line.Split(" ", StringSplitOptions.TrimEntries);
                if (tokens.Length != 4)
                {
                    throw new MeshParseException(MeshPath, lineNumber, "Incorrect number of triangle indices");
                }

                try
                {
                    triangles.Add((uint.Parse(tokens[1]), uint.Parse(tokens[2]), uint.Parse(tokens[3])));
                }
                catch (Exception e)
                {
                    throw new MeshParseException(MeshPath, lineNumber, "Failed to parse triangle's index", e);
                }
            }
            else
            {
                throw new MeshParseException(MeshPath, lineNumber, "Unrecognized token");
            }
        }

        Mesh = new Mesh(positions, normals, textures, triangles);
    }

    // TODO: Stage 1b (0.5pt)
    public void LoadTexture()
    {
        using var fileStream = new FileStream(TexturePath, FileMode.Open);
        using var binaryReader = new BinaryReader(fileStream);

        var width = binaryReader.ReadInt32();
        var height = binaryReader.ReadInt32();

        var data = binaryReader.ReadBytes(3 * width * height);

        Texture = new Texture(data, width, height);
    }

    public void Dispose()
    {
        Mesh?.Dispose();
        Texture?.Dispose();
        GC.SuppressFinalize(this);
    }

    public void OnDeserialized()
    {
        LoadMesh();
        LoadTexture();
    }

    //TODO: Stage 2 (1pt)
    public class MeshParseException : Exception
    {
        public MeshParseException(string filename, int line, string message)
            : base($"Exception while parsing \"{filename}\"({line}): {message}")
        {
        }
        public MeshParseException(string filename, int line, string message, Exception inner)
            : base($"Exception while parsing \"{filename}\"({line}): {message}", inner)
        {
        }
    }
}