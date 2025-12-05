using System.IO.Compression;
using System.Reflection;
using System.Text.Json;
using System.Xml.Serialization;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Duck;

public class Scene
{
    public Duck Player { get; set; } = new("") { Behaviour = new Duck.PlayerControlled() };
    public List<Duck> Ducks { get; set; } = [];
    public DateTime Time { get; set; } = DateTime.Now;

    public Scene()
    {
    }

    public Scene(string path)
    {
        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
        if (stream is null) throw new FileNotFoundException("Could not find scene file", path);
        using BinaryReader br = new BinaryReader(stream);
        
        ushort ducks = br.ReadUInt16();

        for (int i = 0; i < ducks; i++)
        {
            var name = br.ReadString();
            var position = new Vector3(br.ReadSingle(), 0, br.ReadSingle());
            var rotation = float.DegreesToRadians(br.ReadSingle());
            var scale = br.ReadSingle();
            Ducks.Add(new Duck(name, position, rotation, scale));
        }
    }

    public IEnumerable<Duck> GetAllDucks()
    {
        yield return Player;
        foreach (var duck in Ducks)
        {
            yield return duck;
        }
    }

    public void Update(float dt, KeyboardState keyboard, MouseState mouse)
    {
        Time += TimeSpan.FromMinutes(dt);
        Player.Update(dt, keyboard, mouse);
        foreach (var duck in Ducks)
        {
            duck.Update(dt, keyboard, mouse);
        }
    }

    public void QuackSave()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Scene));

        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string save = Path.Combine(path, "Duck", "quack.save");

        string? directory = Path.GetDirectoryName(save);
        if (directory is not null && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        using FileStream fs = File.OpenWrite(save);
        using GZipStream gs = new GZipStream(fs, CompressionLevel.SmallestSize);
        using StreamWriter sw = new StreamWriter(gs);

        serializer.Serialize(sw, this);
    }

    public Scene? QuackLoad()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Scene));

        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string save = Path.Combine(path, "Duck", "quack.save");

        if (File.Exists(save))
        {
            using FileStream fs = File.OpenRead(save);
            using GZipStream gs = new GZipStream(fs, CompressionMode.Decompress);
            using StreamReader sr = new StreamReader(gs);

            var deserialized = serializer.Deserialize(sr);
            if (deserialized is Scene scene)
            {
                return scene;
            }
        }
        
        return null;
    }
}