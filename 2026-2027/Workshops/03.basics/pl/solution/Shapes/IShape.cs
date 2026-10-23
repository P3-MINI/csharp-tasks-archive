namespace Shapes;

public interface IShape
{
    float Area { get; }
    bool Contains(Vector2 point);
}
