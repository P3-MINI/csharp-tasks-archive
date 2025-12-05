using System.IO.Compression;
using System.Reflection;
using System.Text.Json;
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
        var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
        if (stream is null) throw new FileNotFoundException("Could not find scene file", path);
        using StreamReader streamReader = new StreamReader(stream);
        while (streamReader.ReadLine() is { } line)
        {
            var tokens = line.Split([";", ","], StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var name = tokens[0];
            var position = new Vector3(float.Parse(tokens[1]), 0, float.Parse(tokens[2]));
            var rotation = float.DegreesToRadians(float.Parse(tokens[3]));
            var scale = float.Parse(tokens[4]);
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

    private JsonSerializerOptions GetJsonSerializerOptions()
    {
        JsonSerializerOptions options = new JsonSerializerOptions();
        options.Converters.Add(new Vector3Converter());
        options.Converters.Add(new Vector2Converter());
        options.WriteIndented = true;
        return options;
    }

    public void QuackSave()
    {
        JsonSerializerOptions options = GetJsonSerializerOptions();
        string json = JsonSerializer.Serialize(this, options);
        
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
        sw.Write(json);
    }

    public Scene? QuackLoad()
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string save = Path.Combine(path, "Duck", "quack.save");

        if (File.Exists(save))
        {
            using FileStream fs = File.OpenRead(save);
            using GZipStream gs = new GZipStream(fs, CompressionMode.Decompress);
            using StreamReader sr = new StreamReader(gs);
            
            string json = sr.ReadToEnd();
            JsonSerializerOptions options = GetJsonSerializerOptions();
            return JsonSerializer.Deserialize<Scene>(json, options);
        }
        
        return null;
    }
}