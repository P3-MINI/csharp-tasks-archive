using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Quack.Engine.Core;
using Quack.Engine.Extra;
using Quack.Messages;

namespace Quack.Client;

public class Scene : IDisposable
{
    private Camera Camera { get; set; } = new Camera(new EditorControl((Vector3.UnitY  + Vector3.UnitZ ) * 2, Vector3.UnitY * 2), new PerspectiveProjection());
    private Water Water { get; set; } = new Water();
    private Sky Sky { get; set; } = new Sky();
    private Shader Shader { get; set; } = new Shader(("Quack.Engine.Resources.Shaders.default.frag", ShaderType.FragmentShader),
        ("Quack.Engine.Resources.Shaders.default.vert", ShaderType.VertexShader));
    
    public Duck? Duck { get; set; }
    public Dictionary<int, Duck> Ducks { get; } = [];
    public Dictionary<int, Food> Foods { get; } = [];
    public DateTime GameTime { get; private set; } = DateTime.Now;

    public void InitState(WelcomeMessage welcome)
    {
        Ducks.Clear();
        foreach (var duckState in welcome.Ducks)
        {
            Ducks.Add(duckState.Id, new Duck(duckState));
        }
        
        Foods.Clear();
        foreach (var foodState in welcome.Food)
        {
            Foods.Add(foodState.Id, new Food(foodState));
        }

        Duck = Ducks[welcome.PlayerId];
        Camera.Control = new Duck.CameraControl(Duck);
        GameTime = welcome.GameTime;
    }
    
    public void UpdateState(UpdateStateMessage updateState)
    {
        foreach (var duckState in updateState.Ducks)
        {
            if (Ducks.TryGetValue(duckState.Id, out var duck))
            {
                duck.AddState(duckState);
            }
            else Ducks.Add(duckState.Id, new Duck(duckState));
        }
        
        foreach (var foodEvent in updateState.FoodEvents)
        {
            if (foodEvent.Type == FoodEventType.Spawn)
            {
                if (!Foods.ContainsKey(foodEvent.FoodId))
                {
                    Foods.Add(foodEvent.FoodId, new Food(new FoodState { Id = foodEvent.FoodId, X = foodEvent.X, Y = foodEvent.Y }));
                }
            }
            else if (foodEvent.Type == FoodEventType.Consumed)
            {
                Foods.Remove(foodEvent.FoodId);
            }
        }
        GameTime = updateState.GameTime;
    }
    
    public void RemoveDuck(int id)
    {
        Ducks.Remove(id);
    }

    public void Update(float dt)
    {
        Water.Update(GameTime);
        Sky.Update(GameTime);
        foreach (var duck in Ducks.Values)
        {
            duck.Update(dt);
        }
        foreach (var food in Foods.Values)
        {
            food.Update(dt);
        }

        Camera.Update(dt);
    }
    
    public void Render()
    {
        Sky.Render(Camera);
        Water.Render(Camera);
        
        Shader.Use();
        foreach (var duck in Ducks.Values)
        {
            if (Duck != null) duck.PlayerDuckScale = Duck.Scale;
            duck.Texture.Bind();
            Shader.LoadInteger("tex", 0);
            Matrix4 modelMatrix = duck.Transform;
            Shader.LoadMatrix4("model", modelMatrix);
            Shader.LoadMatrix4("mvp", modelMatrix * Camera.ProjectionViewMatrix);
            Shader.LoadFloat3("lightDir", Sky.SunDir);
            Shader.LoadFloat3("lightColor", Sky.Ambient);
            duck.Model.Render();
            if (Duck is not null && Duck.Id != duck.Id) duck.Billboard.Render(Camera);
        }
        
        foreach (var food in Foods.Values)
        {
            food.Texture.Bind();
            Shader.LoadInteger("tex", 0);
            Matrix4 modelMatrix = food.Transform;
            Shader.LoadMatrix4("model", modelMatrix);
            Shader.LoadMatrix4("mvp", modelMatrix * Camera.ProjectionViewMatrix);
            food.Model.Render();
        }
    }

    public void UpdateViewport(int width, int height)
    {
        Camera.Aspect = (float)width / height;
    }

    public void Dispose()
    {
        Water.Dispose();
        Sky.Dispose();
        Shader.Dispose();
        GC.SuppressFinalize(this);
    }
}