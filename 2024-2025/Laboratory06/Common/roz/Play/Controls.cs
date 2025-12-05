using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Task;

namespace Play
{
    internal static class Controls
    {
        internal static (Vector2, float) ProcessMovement(Vector2 position, float angle, Map map)
        {
            Vector2 newPosition = position;
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Escape)
                    Environment.Exit(0);
                if (key.Key == ConsoleKey.A)
                    angle += MathF.PI / 8;
                if (key.Key == ConsoleKey.D)
                    angle -= MathF.PI / 8;
                if (key.Key == ConsoleKey.W)
                    newPosition += new Vector2(MathF.Sin(angle), MathF.Cos(angle));
                if (key.Key == ConsoleKey.S)
                    newPosition -= new Vector2(MathF.Sin(angle), MathF.Cos(angle));
                while (Console.KeyAvailable)
                    _ = Console.ReadKey(true);
            }
            if (newPosition != position)
            {
                Point p = new((int)newPosition.X, (int)newPosition.Y);
                if (map.InBounds(p) && map.Data[p.X, p.Y] != Tile.Wall)
                    position = newPosition;
                else
                    Console.Beep();
            }
            return (position, angle);
        }
    }
}
