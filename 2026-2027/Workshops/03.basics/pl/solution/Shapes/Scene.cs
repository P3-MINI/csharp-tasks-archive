namespace Shapes;

public class Scene : IShape
{
    private readonly List<IShape> _shapes = new();

    public void Add(IShape shape)
    {
        _shapes.Add(shape);
    }

    public float Area
    {
        get
        {
            float total = 0;
            foreach (var shape in _shapes)
            {
                total += shape.Area;
            }
            return total;
        }
    }

    public bool Contains(Vector2 point)
    {
        foreach (var shape in _shapes)
        {
            if (shape.Contains(point))
            {
                return true;
            }
        }
        return false;
    }

    public char[,] Render(int x, int y, int width, int height)
    {
        char[,] buffer = new char[height, width];
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                var pt = new Vector2(x + col, y + row);
                buffer[row, col] = Contains(pt) ? '#' : '.';
            }
        }
        return buffer;
    }
}
