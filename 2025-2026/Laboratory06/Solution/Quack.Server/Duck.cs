using System.Diagnostics;
using ImGuiNET;
using OpenTK.Mathematics;
using Quack.Engine.Core;
using Quack.Engine.Extra;
using Quack.Messages;

namespace Quack.Server;

public class Duck
{
    public Model Model { get; }
    public Texture Texture { get; }
    public Billboard Billboard { get; }

    public DuckState State { get; }
    public Vector3 Position => new (State.X, 0, State.Y);
    public float Rotation => State.Rotation;
    public float Scale => State.Scale;

    public float Food { get; set; }
    private Input _playerInput;
    private readonly IPhysicsBody _body;

    public Matrix4 Transform => Matrix4.CreateScale(Scale) *
                                Matrix4.CreateRotationY(Rotation) *
                                Matrix4.CreateTranslation(Position);
    
    const float MoveForce = 50.0f;
    const float TurnTorque = 2.5f;

    public Duck(DuckState state, IPhysicsBody body)
    {
        State = state;
        _body = body;
        Model = ResourcesManager.Instance.GetModel("Quack.Engine.Resources.Models.duck.obj");
        Texture = ResourcesManager.Instance.GetTexture("Quack.Engine.Resources.Textures.duck.png");
        Billboard = new Billboard(Vector3.Zero, () => ImGui.Text(state.Name));
    }
    
    public IPhysicsBody PhysicsBody => _body;

    
    public void ReadInput(InputMessage message)
    {
        _playerInput.Up = message.Up;
        _playerInput.Down = message.Down;
        _playerInput.Right = message.Right;
        _playerInput.Left = message.Left;
        _playerInput.Sprint = message.Sprint;
    }

    public void Update(float dt)
    {
        var front3 = new Vector3(-1, 0, 0) * Matrix3.CreateRotationY(_body.Rotation);
        var frontDir = new System.Numerics.Vector2(front3.X, front3.Z);

        float move = MoveForce * Scale * MathF.Sqrt(Scale);
        float torque = TurnTorque * Scale * Scale * Scale;
        if (_playerInput.Sprint)
        {
            move *= MathF.Sqrt(Scale + 1.0f);
            State.Scale -= 0.1f / Scale * dt;
            State.Scale = Math.Max(State.Scale, 1.0f);
            _body.SetScale(State.Scale);
        }
        
        // Input -> Physics
        if (_playerInput.Up)
        {
            _body.ApplyForce(frontDir * move);
        }
        else if (_playerInput.Down)
        {
            _body.ApplyForce(-frontDir * move);
        }

        if (_playerInput.Left)
        {
            _body.ApplyTorque(torque);
        }
        else if (_playerInput.Right)
        {
            _body.ApplyTorque(-torque);
        }

        // Growth
        if (Food > 0)
        {
            float growth = Math.Max(0.1f * dt * Food, 0.1f * dt);
            growth = Math.Min(growth, Food);
            Food -= growth;
            State.Scale += growth * 0.1f / MathF.Sqrt(Scale);
            _body.SetScale(State.Scale);
        }

        // Physics -> State Sync
        var physPos = _body.Position;
        State.X = physPos.X;
        State.Y = physPos.Y;
        State.Rotation = _body.Rotation;
        State.Timestamp = (long)(Stopwatch.GetTimestamp() * (10_000_000.0 / Stopwatch.Frequency));

        Billboard.Position = Position + 1.5f * Scale * Vector3.UnitY;
    }

    private struct Input
    {
        public bool Up, Down, Right, Left, Sprint;
    }
}