using OpenTK.Mathematics;
using Quack.Engine.Core;
using Quack.Engine.Extra;
using Quack.Messages;

namespace Quack.Client;

public class Food
{
    public Model Model { get; }
    public Texture Texture { get; }

    public FoodState State { get; }
    public Vector3 Position => new (State.X, 0, State.Y);
    public float Rotation { get; set; }
    public float Scale => 1.0f;

    public Matrix4 Transform => Matrix4.CreateScale(Scale) *
                                Matrix4.CreateRotationY(Rotation) *
                                Matrix4.CreateTranslation(Position);

    public Food(FoodState state)
    {
        State = state;
        Model = ResourcesManager.Instance.GetModel("Quack.Engine.Resources.Models.bread.obj");
        Texture = ResourcesManager.Instance.GetTexture("Quack.Engine.Resources.Textures.bread.png");
        Rotation = Random.Shared.NextSingle(-float.Pi, float.Pi);
    }

    public void Update(float dt)
    {
        Rotation += 1.0f * dt;
        if (Rotation > float.Pi) Rotation -= 2 * float.Pi;
    }
}