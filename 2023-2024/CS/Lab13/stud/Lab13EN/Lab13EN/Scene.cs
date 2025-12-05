using System.Runtime.Serialization;

namespace Lab13EN;

public class Scene : IDisposable
{
    public List<Model> Models { get; } = new();

    // TODO: Stage 3 (1pt)
    // Create a directory at the specified path.
    // If it already exists, remove all files (and only files!) in that directory.
    // Iterate through all models in the Models list and serialize them to a file named $"{model.Name}({i}).xml" using the DataContractSerializer.
    // Do not serialize the Mesh and Texture properties!
    // Add appropriate implementation details to the Model class.
    public void Serialize(string path)
    {

    }

    // TODO: Stage 4 (1pt)
    // Deserialize all files with the .xml extension at the specified path using the DataContractSerializer.
    // Remember that the Mesh and Texture properties were not serialized.
    // After deserialization, on the loaded objects, reload the texture and mesh using the OnDeserialized attribute.
    // Add all deserialized models to the Models list.
    // Add appropriate implementation details to the Model class.
    public void Deserialize(string path)
    {

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