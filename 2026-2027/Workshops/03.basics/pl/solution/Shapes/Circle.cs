namespace Shapes;

public class Circle : IShape
{
    public Vector2 Center { get; }
    public float Radius { get; }

    public Circle(Vector2 center, float radius)
    {
        Center = center;
        Radius = radius;
    }

    public float Area => MathF.PI * Radius * Radius;

    public bool Contains(Vector2 point)
    {
        Vector2 diff = point - Center;
        return diff.Length <= Radius;
    }
}
