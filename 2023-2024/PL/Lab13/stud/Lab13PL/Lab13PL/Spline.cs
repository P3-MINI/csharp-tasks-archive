using System.Numerics;

namespace Lab13PL;

public class Spline
{
    private const float Border = 5;
    public List<Vector2> Points { get; set; } = new()
    {
        new Vector2(RandomFloat(-Border, Border), RandomFloat(-Border, Border)),
        new Vector2(RandomFloat(-Border, Border), RandomFloat(-Border, Border)),
        new Vector2(RandomFloat(-Border, Border), RandomFloat(-Border, Border)),
        new Vector2(RandomFloat(-Border, Border), RandomFloat(-Border, Border))
    };

    public float T { get; set; }

    public void Update(float dt)
    {
        T += dt;
        while (T > 1)
        {
            Points.RemoveAt(0);
            Points.Add(new Vector2(RandomFloat(-Border, Border), RandomFloat(-Border, Border)));
            T -= 1;
        }
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