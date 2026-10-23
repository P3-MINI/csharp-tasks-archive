namespace Shapes;

internal class Program
{
    private static void Main()
    {
#if STAGE3
        var scene = new Scene();
        scene.Add(new Rectangle(new Vector2(2, 2), new Vector2(10, 8)));
        scene.Add(new Circle(new Vector2(25, 6), 5));

        Console.WriteLine($"Total Scene Area: {scene.Area:F2}");
        Console.WriteLine("Rendered Scene (35x12 from origin 0,0):");
        char[,] buffer = scene.Render(x: 0, y: 0, width: 35, height: 12);
        for (int row = 0; row < buffer.GetLength(0); row++)
        {
            for (int col = 0; col < buffer.GetLength(1); col++)
            {
                Console.Write(buffer[row, col]);
            }
            Console.WriteLine();
        }
#elif STAGE2
        var rect = new Rectangle(new Vector2(2, 2), new Vector2(10, 8));
        var circle = new Circle(new Vector2(25, 6), 5);
        Console.WriteLine($"Rectangle Area: {rect.Area:F2}");
        Console.WriteLine($"Circle Area: {circle.Area:F2}");
#elif STAGE1
        var v1 = new Vector2(1, 2);
        var v2 = new Vector2(3, 4);
        Console.WriteLine($"v1: {v1}, v2: {v2}, sum: {v1 + v2}");
#else
        Console.WriteLine("Shapes application starter. Configure active stage in Stage.csproj.");
#endif
    }
}
