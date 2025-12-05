using System.Runtime.Serialization;

namespace Lab13EN;

public class Scene : IDisposable
{
    public List<Model> Models { get; } = new();

    // TODO: Stage 3 (1pt)
    public void Serialize(string path)
    {
        Directory.CreateDirectory(path);
        foreach (var filename in Directory.EnumerateFiles(path))
        {
            File.Delete(filename);
        }

        var i = 0;
        foreach (var model in Models)
        {
            var filename = $"{model.Name}({i++}).xml";
            var filepath = Path.Combine(path, filename);

            var stream = new FileStream(filepath, FileMode.Create, FileAccess.Write);
            var serializer = new DataContractSerializer(typeof(Model));
            serializer.WriteObject(stream, model);
        }
    }

    // TODO: Stage 4 (1pt)
    public void Deserialize(string path)
    {
        var serializer = new DataContractSerializer(typeof(Model));
        foreach (var filename in Directory.EnumerateFiles(path))
        {
            if (Path.GetExtension(filename) is not ".xml") continue;
            var stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            var serialized = serializer.ReadObject(stream);
            if (serialized is Model model)
            {
                Models.Add(model);
            }
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