using System.Numerics;
using System.Runtime.Serialization;
using System.Text;

namespace Lab13EN;

[DataContract]
public class Model : IDisposable
{
    [DataMember]
    public string Name { get; set; }
    [DataMember]
    public string MeshPath { get; set; }
    [DataMember]
    public string TexturePath { get; set; }

    [DataMember]
    public Vector3 Position { get; set; } = Vector3.Zero;
    [DataMember]
    public Quaternion Rotation { get; set; } = Quaternion.Identity;
    [DataMember]
    public Vector3 Scale { get; set; } = Vector3.One;
    [DataMember]
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
        LoadTexture();
        LoadMesh();
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
        using var stream = new FileStream(MeshPath, FileMode.Open, FileAccess.Read);
        using var reader = new BinaryReader(stream, Encoding.ASCII); // Might be UTF-8 as well

        var positions = new List<(float x, float y, float z)>();
        var normals = new List<(float x, float y, float z)>();
        var textures = new List<(float u, float v)>();
        var triangles = new List<(uint a, uint b, uint c)>();

        while (stream.Position < stream.Length)
        {
            var type = reader.ReadChar();
            switch (type)
            {
                case 'v':
                    positions.Add((reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle()));
                    normals.Add((reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle()));
                    textures.Add((reader.ReadSingle(), reader.ReadSingle()));
                    break;
                case 't':
                    triangles.Add((reader.ReadUInt32(), reader.ReadUInt32(), reader.ReadUInt32()));
                    break;
            }
        }

        Mesh = new Mesh(positions, normals, textures, triangles);
    }

    // TODO: Stage 1b (1pt)
    public void LoadTexture()
    {
        using var reader = new StreamReader(TexturePath);
        int lineNum = 0;

        var line = reader.ReadLine(); lineNum++;
        if (line is null) throw new TextureParseException(TexturePath, lineNum, "Unexpected EOF");
        var tokens = line.Split('x', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        if (tokens.Length is not 2)
        {
            throw new TextureParseException(TexturePath, lineNum, "Incorrect number of dimensions");
        }

        int width, height;
        try
        {
            width = int.Parse(tokens[0]);
            height = int.Parse(tokens[1]);
        }
        catch (Exception e)
        {
            throw new TextureParseException(TexturePath, lineNum, "Failed to parse dimensions", e);
        }
        if (width <= 0 || height <= 0)
        {
            throw new TextureParseException(TexturePath, lineNum, "Dimensions must be greater than 0");
        }

        var data = new byte[3 * width * height];
        for (int i = 0; i < width * height; i++)
        {
            line = reader.ReadLine(); lineNum++;
            if (line is null) throw new TextureParseException(TexturePath, lineNum, "Unexpected EOF");
            tokens = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length is not 3)
            {
                throw new TextureParseException(TexturePath, lineNum, "Incorrect number of color components");
            }

            float red, green, blue;
            try
            {
                red = float.Parse(tokens[0]);
                green = float.Parse(tokens[1]);
                blue = float.Parse(tokens[2]);
            }
            catch (Exception e)
            {
                throw new TextureParseException(TexturePath, lineNum, "Failed to parse color component", e);
            }

            if (red is < 0 or > 1 || green is < 0 or > 1 || blue is < 0 or > 1)
            {
                throw new TextureParseException(TexturePath, lineNum, "Color component out of range");
            }

            data[3 * i + 0] = (byte) (red * 255);
            data[3 * i + 1] = (byte) (green * 255);
            data[3 * i + 2] = (byte) (blue * 255);
        }

        if (!reader.EndOfStream)
        {
            throw new Exception("Excessive texture data");
        }

        Texture = new Texture(data, width, height);
    }
    
    [OnDeserialized]
    public void OnDeserialized(StreamingContext context)
    {
        LoadMesh();
        LoadTexture();
    }

    public void Dispose()
    {
        Mesh?.Dispose();
        Texture?.Dispose();
        GC.SuppressFinalize(this);
    }

    //TODO: Stage 2 (1pt)
    public class TextureParseException : Exception
    {
        public TextureParseException(string filename, int line, string message)
            : base($"Exception while parsing \"{filename}\"({line}): {message}")
        {
        }
        public TextureParseException(string filename, int line, string message, Exception inner)
            : base($"Exception while parsing \"{filename}\"({line}): {message}", inner)
        {
        }
    }
}