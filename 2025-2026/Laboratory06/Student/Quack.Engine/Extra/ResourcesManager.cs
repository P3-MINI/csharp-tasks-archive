using System.Collections.Concurrent;
using Quack.Engine.Core;

namespace Quack.Engine.Extra;

public class ResourcesManager : IDisposable
{
    public static ResourcesManager Instance { get; } = new();

    private ConcurrentDictionary<string, Texture> Textures { get; } = new();
    private ConcurrentDictionary<string, Model> Models { get; } = new();

    private ResourcesManager()
    {
        
    }

    public Texture GetTexture(string path)
    {
        return Textures.GetOrAdd(path, p => new Texture(p));
    }

    public Model GetModel(string path)
    {
        return Models.GetOrAdd(path, Model.Load);
    }

    public void Dispose()
    {
        foreach (var texture in Textures.Values)
        {
            texture.Dispose();
        }
        Textures.Clear();

        foreach (var model in Models.Values)
        {
            model.Dispose();
        }
        Models.Clear();

        GC.SuppressFinalize(this);
    }
}
