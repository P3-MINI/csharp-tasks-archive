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
        // TODO: Stage 1
        // Load the embedded file given as a path and parse it.
        // Add parsed ducks to the Ducks list.
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
        // TODO: Stage 2
        // Implement quack-save functionality
    }

    public Scene? QuackLoad()
    {
        // TODO: Stage 3
        // Implement quack-load functionality
        return null;
    }
}