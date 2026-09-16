using ImGuiNET;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Quack.Engine.Core;
using Quack.Engine.Extra;
using Quack.Messages;
using Vector4 = System.Numerics.Vector4;

namespace Quack.Client;

public class Duck
{
    public Model Model { get; }
    public Texture Texture { get; }
    public Billboard Billboard { get; }
    public int Id { get; }

    public Queue<DuckState> StatesQueue { get; } = [];

    private DuckState PrevState { get; set; }
    private DuckState NextState { get; set; }
    
    private float _currentDuration = 0.045f;
    public float TransitionTime { get; private set; } = -0.045f;

    public Vector3 Position => new (
        MathUtils.Lerp(PrevState.X, NextState.X, GetAlpha()), 
        0, 
        MathUtils.Lerp(PrevState.Y, NextState.Y, GetAlpha())
    );
    
    public float Rotation => MathUtils.LerpAngle(PrevState.Rotation, NextState.Rotation, GetAlpha());
    public float Scale => MathUtils.Lerp(PrevState.Scale, NextState.Scale, GetAlpha());
    public string Name => NextState.Name;
    public float PlayerDuckScale { get; set; } = 1.0f;

    public Matrix4 Transform => Matrix4.CreateScale(Scale) *
                                Matrix4.CreateRotationY(Rotation) *
                                Matrix4.CreateTranslation(Position);

    private float GetAlpha() => Math.Clamp(TransitionTime / _currentDuration, 0f, 1f);

    public Duck(DuckState state)
    {
        PrevState = state;
        NextState = state;
        Id = state.Id;
        
        Model = ResourcesManager.Instance.GetModel("Quack.Engine.Resources.Models.duck.obj");
        Texture = ResourcesManager.Instance.GetTexture("Quack.Engine.Resources.Textures.duck.png");

        Billboard = new Billboard(Vector3.Zero, DrawBillboard);
    }

    public void AddState(DuckState state) 
    {
        StatesQueue.Enqueue(state);
    }

    public void Update(float dt)
    {
        float playbackSpeed = 1.0f;

        if (StatesQueue.Count > 10) playbackSpeed = 1.5f;      // Buffer huge: catch up
        else if (StatesQueue.Count > 4) playbackSpeed = 1.25f; // Buffer healthy: speed up
        else if (StatesQueue.Count > 1) playbackSpeed = 1.05f; // Buffer steady: slight speed up
        if (StatesQueue.Count == 0) playbackSpeed = 0.9f;      // Buffer empty: slow down

        TransitionTime += dt * playbackSpeed;

        if (TransitionTime >= _currentDuration)
        {
            if (StatesQueue.TryDequeue(out var nextState))
            {
                PrevState = NextState;
                NextState = nextState;
                TransitionTime -= _currentDuration;
                
                long deltaTicks = NextState.Timestamp - PrevState.Timestamp;
                if (deltaTicks > 0 && PrevState.Timestamp > 0) 
                {
                    _currentDuration = deltaTicks / 10_000_000.0f;
                }
                else 
                {
                    _currentDuration = 0.045f;
                }

                if (_currentDuration is < 0.001f or > 1.0f) _currentDuration = 0.045f;

                while (StatesQueue.Count > 15)
                {
                    PrevState = NextState;
                    NextState = StatesQueue.Dequeue();
                    TransitionTime = 0; 
                }
            }
            else
            {
                TransitionTime = _currentDuration;
            }
        }

        Billboard.Position = Position + 1.5f * Scale * Vector3.UnitY;
    }
    
    public class CameraControl : Camera.IControl {
        private Duck Duck { get; }
        public CameraControl(Duck duck)
        {
            Duck = duck;
            Setup();
        }

        private void Setup()
        {
            Up = Vector3.UnitY;
            Forward = new Vector3(-1, 0, 0) * Matrix3.CreateRotationY(Duck.Rotation);
            Target = Duck.Position + Vector3.UnitY * 4 * Duck.Scale;
            Position = Target - 10 * Forward * Duck.Scale + Vector3.UnitY * Duck.Scale;
            Forward = (Target - Position).Normalized();
            Right = Vector3.Cross(Forward, Up).Normalized();
            Up = Vector3.Cross(Right, Forward).Normalized();
            ViewMatrix = Matrix4.LookAt(Position, Position + Forward, Up);
        }

        public void Update(Camera camera, float dt)
        {
            Setup();
        }

        public void HandleInput(Camera camera, float dt, KeyboardState keyboard, MouseState mouse) { }

        public Vector3 Target { get; private set; }
        public Vector3 Position { get; private set; }
        public Vector3 Forward { get; private set; }
        public Vector3 Right { get; private set; }
        public Vector3 Up { get; private set; }
        public Matrix4 ViewMatrix { get; private set; }
    }
    
    void DrawBillboard()
    {
        if (PlayerDuckScale * PlayerDuckScale > 1.4f * Scale * Scale)
        {
            ImGui.TextColored(new Vector4(0.125f, 0.6f, 0.125f, 1.0f), NextState.Name);
        }
        else if (Scale * Scale > 1.4f * PlayerDuckScale * PlayerDuckScale)
        {
            ImGui.TextColored(new Vector4(0.6f, 0.125f, 0.125f, 1.0f), NextState.Name);
        }
        else
        {
            ImGui.Text(NextState.Name);
        }
    }
}
