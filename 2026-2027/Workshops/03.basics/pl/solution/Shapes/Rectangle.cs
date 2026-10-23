namespace Shapes;

public class Rectangle : IShape
{
    public Vector2 P1 { get; }
    public Vector2 P2 { get; }

    public Rectangle(Vector2 p1, Vector2 p2)
    {
        P1 = p1;
        P2 = p2;
    }

    public float Area => MathF.Abs(P1.X - P2.X) * MathF.Abs(P1.Y - P2.Y);

    public bool Contains(Vector2 point)
    {
        float minX = MathF.Min(P1.X, P2.X);
        float maxX = MathF.Max(P1.X, P2.X);
        float minY = MathF.Min(P1.Y, P2.Y);
        float maxY = MathF.Max(P1.Y, P2.Y);

        return point.X >= minX && point.X <= maxX && point.Y >= minY && point.Y <= maxY;
    }
}
