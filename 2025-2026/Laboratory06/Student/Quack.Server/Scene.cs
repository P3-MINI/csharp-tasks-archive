using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Quack.Engine.Core;
using Quack.Engine.Extra;

namespace Quack.Server;

public class Scene : IDisposable
{
    public Dictionary<int, Duck> Ducks { get; } = new();
    public Dictionary<int, Food> Food { get; } = new();

    public Camera Camera { get; set; } =
        new Camera(new EditorControl((Vector3.UnitY + Vector3.UnitZ) * 2, Vector3.UnitY * 2),
            new PerspectiveProjection());

    public Sky Sky { get; } = new Sky();
    public Water Water { get; } = new Water();
    public DateTime GameTime { get; private set; }

    private Shader Shader { get; set; } = new Shader(
        ("Quack.Engine.Resources.Shaders.default.frag", ShaderType.FragmentShader),
        ("Quack.Engine.Resources.Shaders.default.vert", ShaderType.VertexShader));

    public void Update(float dt)
    {
        GameTime += TimeSpan.FromMinutes(5 * dt);
        Sky.Update(GameTime);
        Water.Update(GameTime);
        Camera.Update(dt);
        
        foreach (var duck in Ducks.Values)
        {
            duck.Update(dt);
        }

        foreach (var food in Food.Values)
        {
            food.Update(dt);
        }
    }

    public void Render()
    {
        Sky.Render(Camera);
        Water.Render(Camera);

        Shader.Use();
        foreach (var duck in Ducks.Values)
        {
            duck.Texture.Bind();
            Shader.LoadInteger("tex", 0);
            Matrix4 modelMatrix = duck.Transform;
            Shader.LoadMatrix4("model", modelMatrix);
            Shader.LoadMatrix4("mvp", modelMatrix * Camera.ProjectionViewMatrix);
            Shader.LoadFloat3("lightDir", Sky.SunDir);
            Shader.LoadFloat3("lightColor", Sky.Ambient);
            duck.Model.Render();
            duck.Billboard.Render(Camera);
        }
        
        foreach (var food in Food.Values)
        {
            food.Texture.Bind();
            Shader.LoadInteger("tex", 0);
            Matrix4 modelMatrix = food.Transform;
            Shader.LoadMatrix4("model", modelMatrix);
            Shader.LoadMatrix4("mvp", modelMatrix * Camera.ProjectionViewMatrix);
            Shader.LoadFloat3("lightDir", Sky.SunDir);
            Shader.LoadFloat3("lightColor", Sky.Ambient);
            food.Model.Render();
        }
    }

    public void UpdateViewport(int width, int height)
    {
        Camera.Aspect = (float)width / height;
    }

    public void Dispose()
    {
        Sky.Dispose();
        Water.Dispose();
        Shader.Dispose();
        GC.SuppressFinalize(this);
    }
}