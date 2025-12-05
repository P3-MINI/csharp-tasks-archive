using System.Xml.Serialization;
using ImGuiNET;
using OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Duck;

public class Duck
{
    public string Name { get; set; }

    [XmlIgnore] public Mesh Mesh { get; private set; }
    [XmlIgnore] public Texture Texture { get; private set; }
    [XmlIgnore] public Billboard NameBillboard { get; private set; }

    public DuckBehaviour Behaviour { get; set; }
    public Vector3 Position { get; set; }
    public float Rotation { get; set; }
    public float Scale { get; set; }

    public Duck() : this("")
    {
        
    }

    public Duck(string name, Vector3 position = default, float rotation = 0.0f, float scale = 1.0f)
    {
        Name = name;
        Position = position;
        Rotation = rotation;
        Scale = scale;
        Behaviour = new RandomlyMoving(position.Xz, rotation);
        Mesh = ResourcesManager.Instance.GetMesh("Duck.Resources.duck.mesh");
        Texture = ResourcesManager.Instance.GetTexture("Duck.Resources.duck.texture");
        NameBillboard = new Billboard(Vector3.Zero, () => ImGui.Text(Name));
    }

    public void Update(float dt, KeyboardState keyboard, MouseState mouse)
    {
        Behaviour.Update(this, dt, keyboard, mouse);
        NameBillboard.Position = Position + 1.5f * Scale * Vector3.UnitY;
    }

    [XmlInclude(typeof(RandomlyMoving))]
    [XmlInclude(typeof(PlayerControlled))]
    public abstract class DuckBehaviour
    {
        public abstract void Update(Duck duck, float dt, KeyboardState keyboard, MouseState mouse);
    }

    public class RandomlyMoving(Vector2 initialPosition, float initialRotation) : DuckBehaviour
    {
        public RandomlyMoving() : this(Vector2.Zero, 0) { }

        public Spline Spline { get; set; } = new (initialPosition, initialRotation);

        public override void Update(Duck duck, float dt, KeyboardState keyboard, MouseState mouse)
        {
            Spline.Update(dt);
            var position = Spline.GetPosition();
            duck.Position = new Vector3(position.X, 0, position.Y);
            duck.Rotation = Spline.GetRotation();
        }
    }
    
    public class PlayerControlled : DuckBehaviour
    {
        public float Speed { get; set; }
        public float TopSpeed { get; set; } = 4.0f;
        public float Acceleration { get; set; } = 8.0f;
        public float RotationSpeed { get; set; }
        public float TopRotationSpeed { get; set; } = MathF.PI / 2;
        public float RotationAcceleration { get; set; } = MathF.PI * 2;
        public float DampingFactor { get; set; } = 4.0f;
        private System.Timers.Timer QuackTimer { get; set; } = new(TimeSpan.FromSeconds(2));
        public override void Update(Duck duck, float dt, KeyboardState keyboard, MouseState mouse)
        {
            if (keyboard.IsKeyDown(Keys.W))
            {
                Speed += Acceleration * dt;
            }
            else if (keyboard.IsKeyDown(Keys.S))
            {
                Speed -= Acceleration * dt;
            }
            else
            {
                Speed *= Math.Clamp(1 - DampingFactor * dt, 0.0f, 1.0f);
            }
            if (keyboard.IsKeyDown(Keys.A))
            {
                RotationSpeed += RotationAcceleration * dt;
            }
            else if (keyboard.IsKeyDown(Keys.D))
            {
                RotationSpeed -= RotationAcceleration * dt;
            }
            else
            {
                RotationSpeed *= Math.Clamp(1 - DampingFactor * dt, 0.0f, 1.0f);
            }

            if (keyboard.IsKeyDown(Keys.Q))
            {
                duck.Name = "Quack";
                QuackTimer.AutoReset = false;
                QuackTimer.Elapsed += (_, _) => duck.Name = "";
                QuackTimer.Stop();
                QuackTimer.Start();
            }

            Speed = keyboard.IsKeyDown(Keys.LeftShift) ? 
                Math.Clamp(Speed, -TopSpeed, 2 * TopSpeed) : 
                Math.Clamp(Speed, -TopSpeed, TopSpeed);
            RotationSpeed = Math.Clamp(RotationSpeed, -TopRotationSpeed, TopRotationSpeed);
            
            var front = new Vector3(-1, 0, 0) * Matrix3.CreateRotationY(duck.Rotation);
            duck.Position += front * Speed * duck.Scale * dt;
            duck.Rotation += RotationSpeed * dt;
        }
    }
}

public class DuckCameraControl : Camera.IControl {
    private Duck Duck { get; }
    public DuckCameraControl(Duck duck)
    {
        Duck = duck;
        Setup();
    }

    private void Setup()
    {
        Up = Vector3.UnitY;
        Forward = new Vector3(-1, 0, 0) * Matrix3.CreateRotationY(Duck.Rotation);
        Target = Duck.Position + Vector3.UnitY * 4;
        Position = Target - 10 * Forward + Vector3.UnitY;
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