using System.Text.Json;

namespace Lab13PL;

public class Scene : IDisposable
{
    public List<Model> Models { get; } = new();

    // TODO: Stage 3 (1.5pt)
    public void Serialize(string path)
    {
        Directory.CreateDirectory(path);
        foreach (var filename in Directory.EnumerateFiles(path))
        {
            File.Delete(filename);
        }

        foreach (var model in Models)
        {
            int i = 0;
            string filename = $"{model.Name}({i}).json";
            string filepath = Path.Combine(path, filename);
            while (File.Exists(filepath))
            {
                i++;
                filename = $"{model.Name}({i}).json";
                filepath = Path.Combine(path, filename);
            }

            var options = new JsonSerializerOptions
            {
                IncludeFields = true
            };
            File.WriteAllText(filepath, JsonSerializer.Serialize(model, options));
        }
    }

    // TODO: Stage 4 (1pt)
    public void Deserialize(string path)
    {
        foreach (var filename in Directory.EnumerateFiles(path))
        {
            if (Path.GetExtension(filename) != ".json") continue;
            var options = new JsonSerializerOptions
            {
                IncludeFields = true
            };
            string jsonString = File.ReadAllText(filename);
            Models.Add(JsonSerializer.Deserialize<Model>(jsonString, options)!);
        }
    }

    public void Clear()
    {
        foreach (var model in Models)
        {
            model.Dispose();
        }
        Models.Clear();
    }

    public void Dispose()
    {
        Clear();
        GC.SuppressFinalize(this);
    }
}