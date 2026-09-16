using System.Numerics;
using Box2D;
using Box2D.Math;
using Box2D.Types;
using Box2D.Types.Bodies;
using Box2D.Types.Shapes;
using Box2D.Id;
using Box2D.Collision;

namespace Quack.Server;

public class PhysicsSystem : IDisposable
{
    private World _world;
    public World World => _world;

    private readonly Dictionary<BodyId, EntityData> _entityMap = [];
    private readonly List<IDisposable> _staticShapes = []; // Keep static shapes alive
    private Body? _arenaBody;

    public event EventHandler<FoodConsumedEventArgs>? FoodConsumed; // duckId, foodId
    public event EventHandler<DuckEatenEventArgs>? DuckEaten; // predatorId, preyId

    public PhysicsSystem()
    {
        var worldDef = WorldDef.Default();
        worldDef.Gravity = Vector2.Zero; // Top-down game
        _world = new World(ref worldDef);

        CreateArenaBoundaries(200, 200);
    }

    private void CreateArenaBoundaries(float width, float height)
    {
        var bodyDef = BodyDef.Default();
        bodyDef.Type = BodyType.StaticBody;
        bodyDef.Position = Vector2.Zero;

        _arenaBody = new Body(_world.Id, ref bodyDef);

        float halfW = width / 2.0f;
        float halfH = height / 2.0f;
        float thickness = 10.0f;

        var shapeDef = ShapeDef.Default();
        shapeDef.Material = SurfaceMaterial.Default();
        shapeDef.Material.Friction = 0.5f;
        shapeDef.Material.Restitution = 0.1f;

        // 4 Walls (Thick Polygons)
        var rot0 = new Rotation { Cos = 1.0f, Sin = 0.0f };
        var top = Polygon.MakeOffsetBox(halfW + thickness, thickness, new Vector2(0, halfH + thickness), rot0);
        var bottom = Polygon.MakeOffsetBox(halfW + thickness, thickness, new Vector2(0, -halfH - thickness), rot0);
        var left = Polygon.MakeOffsetBox(thickness, halfH, new Vector2(-halfW - thickness, 0), rot0);
        var right = Polygon.MakeOffsetBox(thickness, halfH, new Vector2(halfW + thickness, 0), rot0);

        _staticShapes.Add(new Shape(_arenaBody.Id, ref shapeDef, ref top));
        _staticShapes.Add(new Shape(_arenaBody.Id, ref shapeDef, ref bottom));
        _staticShapes.Add(new Shape(_arenaBody.Id, ref shapeDef, ref left));
        _staticShapes.Add(new Shape(_arenaBody.Id, ref shapeDef, ref right));
    }

    /// <summary>
    /// Creates a dynamic physics body for a Duck, modeled as a capsule.
    /// </summary>
    public IPhysicsBody CreateDuckBody(int id, float x, float y, float rotation, float scale)
    {
        // Define Body
        var bodyDef = BodyDef.Default();
        bodyDef.Type = BodyType.DynamicBody;
        bodyDef.Position = new Vector2(x, y);
        bodyDef.Rotation = new Rotation { Cos = MathF.Cos(rotation), Sin = MathF.Sin(rotation) };
        bodyDef.FixedRotation = false;
        bodyDef.LinearDamping = 10.0f; // Stop sliding
        bodyDef.AngularDamping = 10.0f; // Stop spinning

        var body = new Body(_world.Id, ref bodyDef);

        // Define Capsule Shape
        var shapeDef = ShapeDef.Default();
        shapeDef.Density = 1.0f;
        shapeDef.Material = SurfaceMaterial.Default();
        shapeDef.Material.Friction = 0.3f;
        shapeDef.Material.Restitution = 0.5f;
        shapeDef.EnableContactEvents = true;
        shapeDef.EnableHitEvents = true;
        shapeDef.EnableSensorEvents = true; // Enable for Duck too

        var initialCapsule = CreateCapsuleGeometry(scale);
        var shape = new Shape(body.Id, ref shapeDef, ref initialCapsule);

        var physicsBody = new Box2DPhysicsBody(body, shape);
        physicsBody.SetScaleInternal(scale); // Initialize tracking
        _entityMap[body.Id] = new EntityData { Type = EntityType.Duck, Id = id, PhysicsBody = physicsBody };
        return physicsBody;
    }

    /// <summary>
    /// Creates a static physics body for Food, modeled as a sensor circle.
    /// </summary>
    public IPhysicsBody CreateFoodBody(int id, float x, float y)
    {
        var bodyDef = BodyDef.Default();
        bodyDef.Type = BodyType.StaticBody;
        bodyDef.Position = new Vector2(x, y);

        var body = new Body(_world.Id, ref bodyDef);

        var shapeDef = ShapeDef.Default();
        shapeDef.IsSensor = true; // Collectible
        shapeDef.EnableSensorEvents = true;

        var circle = new Circle { Radius = 0.5f, Center = Vector2.Zero };
        var shape = new Shape(body.Id, ref shapeDef, ref circle);

        var physicsBody = new Box2DPhysicsBody(body, shape);
        _entityMap[body.Id] = new EntityData { Type = EntityType.Food, Id = id, PhysicsBody = physicsBody };
        return physicsBody;
    }

    public void DestroyBody(IPhysicsBody physicsBody)
    {
        if (physicsBody is Box2DPhysicsBody b2Body)
        {
            _entityMap.Remove(b2Body.BodyId);
            b2Body.Dispose();
        }
    }

    public static Capsule CreateCapsuleGeometry(float scale)
    {
        float baseRadius = 0.25f;
        float baseHalfHeight = 0.3f;

        return new Capsule
        {
            Center1 = new Vector2(-baseHalfHeight * scale, 0),
            Center2 = new Vector2(baseHalfHeight * scale, 0),
            Radius = baseRadius * scale
        };
    }

    /// <summary>
    /// Steps the physics simulation and processes collision events.
    /// </summary>
    public void Step(float dt)
    {
        _world.Step(dt);
        ProcessCollisions();
    }

    private unsafe void ProcessCollisions()
    {
        var events = _world.GetContactEvents();

        for (int i = 0; i < events.HitCount; i++)
        {
            var hit = events.HitEvents[i];
            // Hit events are for solids colliding (Duck vs Duck)
            HandleSolidCollision(hit.ShapeIdA, hit.ShapeIdB);
        }

        // Sensor events (overlap) are usually handled via SensorEvents or by checking Overlaps if IsSensor used.
        // Box2D 3.x separates sensor events.
        var sensorEvents = _world.GetSensorEvents();
        for (int i = 0; i < sensorEvents.BeginCount; i++)
        {
            var begin = sensorEvents.BeginEvents[i];
            HandleSensorCollision(begin.SensorShapeId, begin.VisitorShapeId);
        }
    }

    private void HandleSolidCollision(ShapeId shapeA, ShapeId shapeB)
    {
         var bodyIdA = new Shape(shapeA).GetBody();
         var bodyIdB = new Shape(shapeB).GetBody();

         if (_entityMap.TryGetValue(bodyIdA, out var dataA) && _entityMap.TryGetValue(bodyIdB, out var dataB))
         {
             if (dataA.Type == EntityType.Duck && dataB.Type == EntityType.Duck)
             {
                 // Predation Logic
                 if (dataA.PhysicsBody is Box2DPhysicsBody duckA && dataB.PhysicsBody is Box2DPhysicsBody duckB)
                 {
                     float scaleA = duckA.GetScale();
                     float scaleB = duckB.GetScale();
                     if (scaleA * scaleA > scaleB * scaleB * 1.4f)
                        DuckEaten?.Invoke(this, new DuckEatenEventArgs(dataA.Id, dataB.Id));
                     else if (scaleB * scaleB > scaleA * scaleA * 1.4f)
                        DuckEaten?.Invoke(this, new DuckEatenEventArgs(dataB.Id, dataA.Id));
                 }
             }
         }
    }

    private void HandleSensorCollision(ShapeId sensorShape, ShapeId visitorShape)
    {
        var sensorBodyId = new Shape(sensorShape).GetBody();
        var visitorBodyId = new Shape(visitorShape).GetBody();

        if (_entityMap.TryGetValue(sensorBodyId, out var sensorData) && 
            _entityMap.TryGetValue(visitorBodyId, out var visitorData))
        {
            if (sensorData.Type == EntityType.Food && visitorData.Type == EntityType.Duck)
            {
                FoodConsumed?.Invoke(this, new FoodConsumedEventArgs(visitorData.Id, sensorData.Id));
            }
            else if (visitorData.Type == EntityType.Food && sensorData.Type == EntityType.Duck)
            {
                FoodConsumed?.Invoke(this, new FoodConsumedEventArgs(sensorData.Id, visitorData.Id));
            }
        }
    }

    public void Dispose()
    {
        _staticShapes.Clear(); // Shapes are destroyed when World is disposed, just clear ref
        _world.Dispose();
    }

    private enum EntityType
    {
        Duck,
        Food
    }

    private struct EntityData
    {
        public EntityType Type;
        public int Id;
        public IPhysicsBody PhysicsBody;
    }

    public class FoodConsumedEventArgs : EventArgs
    {
        public int DuckId { get; }
        public int FoodId { get; }

        public FoodConsumedEventArgs(int duckId, int foodId)
        {
            DuckId = duckId;
            FoodId = foodId;
        }
    }

    public class DuckEatenEventArgs : EventArgs
    {
        public int PredatorId { get; }
        public int PreyId { get; }

        public DuckEatenEventArgs(int predatorId, int preyId)
        {
            PredatorId = predatorId;
            PreyId = preyId;
        }
    }
}

public interface IPhysicsBody : IDisposable
{
    Vector2 Position { get; }
    float Rotation { get; }
    void SetVelocity(Vector2 velocity);
    void SetRotation(float radians);
    void SetScale(float scale);
    void ApplyImpulse(Vector2 impulse);
    void ApplyForce(Vector2 force);
    void ApplyTorque(float torque);
}

public class Box2DPhysicsBody : IPhysicsBody
{
    private readonly Body _body;
    private readonly Shape _shape; // Shape is managed by the PhysicsSystem as it's owned by the Body
    private float _currentScale = 1.0f;

    public BodyId BodyId => _body.Id;

    public Box2DPhysicsBody(Body body, Shape shape)
    {
        _body = body;
        _shape = shape;
    }

    public void Dispose()
    {
        _body.Dispose();
    }

    public Vector2 Position => _body.GetPosition();

    public float Rotation
    {
        get
        {
            var rot = _body.GetRotation();
            return MathF.Atan2(rot.Sin, rot.Cos);
        }
    }

    public void SetVelocity(Vector2 velocity)
    {
        _body.SetLinearVelocity(velocity);
    }

    public void SetRotation(float radians)
    {
        var rot = new Rotation { Cos = MathF.Cos(radians), Sin = MathF.Sin(radians) };
        _body.SetTransform(Position, rot);
    }

    public void SetScaleInternal(float scale)
    {
        _currentScale = scale;
    }

    public void SetScale(float scale)
    {
        if (Math.Abs(_currentScale - scale) < 0.001f) return;
        _currentScale = scale;
        var capsule = PhysicsSystem.CreateCapsuleGeometry(scale);
        _shape.SetCapsule(ref capsule);
        _body.ApplyMassFromShapes();
    }

    public float GetScale()
    {
        return _currentScale;
    }

    public void ApplyImpulse(Vector2 impulse)
    {
        _body.ApplyLinearImpulseToCenter(impulse, true);
    }

    public void ApplyForce(Vector2 force)
    {
        _body.ApplyForceToCenter(force, true);
    }

    public void ApplyTorque(float torque)
    {
        _body.ApplyTorque(torque, true);
    }
}