using System.Text.Json.Serialization;
using OpenTK.Mathematics;

namespace Duck;

public class Spline(Vector2 position, float angle)
{
    public Spline() : this(Vector2.Zero, 0.0f) { }
    
    private const float ArcRadius = 4.20f;
    [JsonInclude]
    private List<Vector2> Points { get; set; } = GenerateInitialPoints(position, angle);

    private static List<Vector2> GenerateInitialPoints(Vector2 position, float angle)
    {
        List<Vector2> points =
        [
            position,
            position,
            position,
            position + new Vector2(-ArcRadius, 0) * Matrix2.CreateRotation(angle)
        ];

        return points;
    }

    public float T { get; set; }

    public void Update(float dt)
    {
        T += dt;
        while (T > 1)
        {
            Points.Add(GenerateNextPoint(Points));
            Points.RemoveAt(0);
            T -= 1;
        }
    }
    
    private static Vector2 GenerateNextPoint(List<Vector2> points)
    {
        Vector2 direction = Vector2.Normalize(points[3] - points[2]);
        
        float angle = RandomFloat(-MathF.PI / 2, MathF.PI / 2);
        float cos = MathF.Cos(angle);
        float sin = MathF.Sin(angle);

        Vector2 arcPoint = new Vector2(
            direction.X * cos - direction.Y * sin,
            direction.X * sin + direction.Y * cos
        );

        float bias = 0.999f;

        return (points[3] + arcPoint * ArcRadius) * bias;
    }

    public Vector2 GetPosition()
    {
        float t = T;
        float tt = t * t;
        float ttt = tt * t;
        return ((-1.0f * ttt + 3.0f * tt - 3.0f * t + 1.0f) * Points[0]
                + (3.0f * ttt - 6.0f * tt + 0.0f * t + 4.0f) * Points[1]
                + (-3.0f * ttt + 3.0f * tt + 3.0f * t + 1.0f) * Points[2]
                + (1.0f * ttt + 0.0f * tt + 0.0f * t + 0.0f) * Points[3]) * 0.16666667f;
    }

    public float GetRotation()
    {
        float t = T;
        float tt = t * t;
        Vector2 vector =
            ((-1.0f * tt + 2.0f * t - 1.0f) * Points[0]
             + (3.0f * tt - 4.0f * t + 0.0f) * Points[1]
             + (-3.0f * tt + 2.0f * t + 1.0f) * Points[2]
             + (1.0f * tt + 0.0f * t + 0.0f) * Points[3]) * 0.5f;
        vector = Vector2.Normalize(vector);
        return MathF.PI / 2 + MathF.Atan2(vector.X, vector.Y);
    }

    private static float RandomFloat(float from, float to)
    {
        return Random.Shared.NextSingle() * (to - from) + from;
    }
}