namespace Shapes;

public readonly struct Vector2
{
    public float X { get; }
    public float Y { get; }

    public Vector2(float x, float y)
    {
        X = x;
        Y = y;
    }

    public float Length => MathF.Sqrt(X * X + Y * Y);

    public static Vector2 operator +(Vector2 a, Vector2 b) => new(a.X + b.X, a.Y + b.Y);

    public static Vector2 operator -(Vector2 a, Vector2 b) => new(a.X - b.X, a.Y - b.Y);

    public static Vector2 operator *(Vector2 v, float scalar) => new(v.X * scalar, v.Y * scalar);

    public static Vector2 operator *(float scalar, Vector2 v) => new(v.X * scalar, v.Y * scalar);

    public override string ToString() => $"({X}, {Y})";
}
