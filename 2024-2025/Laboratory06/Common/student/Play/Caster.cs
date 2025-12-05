using Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using Task;

namespace Play
{
    internal static class Caster
    {
        static Caster()
        {
            Console.CursorVisible = false;
            Console.OutputEncoding = Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
        }
        internal struct Collision
        {
            public float distance;
            public float? transparentDistance;

            public Vector2 normal;
            public Vector2? transparentNormal;

            public Tile tile;
            public string item;
            public string enemy;
            public Point position;
        }
        private static Vector2 Normal(Vector2 offset)
        {
            Vector2 mid = new Vector2(
                MathF.Floor(offset.X) + 0.5f,
                MathF.Floor(offset.Y) + 0.5f);
            Vector2 outv = offset - mid;
            if (outv.X > MathF.Abs(outv.Y))
                return new(1, 0);
            else if (-outv.X > MathF.Abs(outv.Y))
                return new(-1, 0);
            else if (outv.Y > MathF.Abs(outv.X))
                return new(0, 1);
            else
                return new(0, -1);
        }
        private static Vector2 BoundsNormal(Vector2 offset, int width, int height)
        {
            if ((int)offset.X < 0)
                return new(1, 0);
            else if ((int)offset.Y < 0)
                return new(0, 1);
            else if ((int)offset.X >= width)
                return new(-1, 0);
            else
                return new(0, -1);
        }
        private static Collision Ray(Vector2 start, Vector2 dir, Map map)
        {
            Vector2 point = start;

            Collision c = new();
            do
            {
                point += dir * .01f;
                c.distance += .01f;
                c.position = new((int)point.X, (int)point.Y);
                if (!map.InBounds(c.position))
                {
                    c.normal = BoundsNormal(point, map.Data.GetLength(0), map.Data.GetLength(1));
                    c.tile = Tile.Wall;
                    return c;
                }

                c.tile = map.Data[c.position.X, c.position.Y];
                if (!c.transparentDistance.HasValue &&
                    (map.Items.ContainsKey(c.position) || (EnemyController.EC?.Enemies.ContainsKey(c.position)??false)))
                {
                    Vector2 mid = new(
                        MathF.Floor(point.X) + 0.5f,
                        MathF.Floor(point.Y) + 0.5f);
                    Vector2 outv = point - mid;
                    if (Math.Max(Math.Abs(outv.X), Math.Abs(outv.Y)) < 0.25f)
                    {
                        c.transparentNormal = Normal(point);
                        c.transparentDistance = c.distance;
                        _ = map.Items.TryGetValue(c.position, out c.item);
                        if (EnemyController.EC?.Enemies.TryGetValue(c.position, out _)??false)
                            c.enemy = "Moleman";
                    }
                }

            } while (c.tile == Tile.Floor);

            c.normal = Normal(point);
            return c;
        }
        const float FOV = 90;
        const float fogmin = 5;
        const float fogmax = 20;

        const int horSubpixels = 2;
        const int verSubpixels = 2;
        private static (char, byte) RenderPixel(byte tl, byte tr, byte bl, byte br)
        {
            byte bits =(byte)(
                (tl != 0 ? 1 : 0) |
                (tr != 0 ? 2 : 0) |
                (bl != 0 ? 4 : 0) |
                (br != 0 ? 8 : 0));
            byte[] colors = new byte[8];
            if (tl != 0)
                colors[Math.ILogB(tl)+1]++;
            if (tr != 0)
                colors[Math.ILogB(tr)+1]++;
            if (bl != 0)
                colors[Math.ILogB(bl)+1]++;
            if (br != 0)
                colors[Math.ILogB(br)+1]++;
            byte color = 0;
            byte colVotes = colors[0];
            for(byte i = 1; i<8; i++)
                if(colors[i] >colVotes)
                {
                    color = i;
                    colVotes = colors[i];
                }

            char character = '\0';
            if (bits == 0b1111)
                character = '█';
            else if (bits == 0b0000)
                character = ' ';
            else if (bits is 0b1010 or 0b0101)
                character = '|';
            else if (bits == 0b0011)
                character = '▀';
            else if (bits == 0b1100)
                character = '▄';
            else if (bits is 0b1001 or 0b0110)
                character = '░';
            else if (bits is 0b1000 or 0b0100)
                character = '_';
            else if (bits is 0b0010 or 0b0001)
                character = '¯';
            else
                character = '▓';
            return (character, color);
        }
        private static (char[],byte[]) RenderFrame(byte[,] buffer)
        {
            (int width, int height) = (buffer.GetLength(0)/horSubpixels, buffer.GetLength(1)/verSubpixels);
            var frame = new char[width * height];
            var colors = new byte[width * height];
            byte[,] components = new byte[horSubpixels,verSubpixels];
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                {
                    var tl = buffer[x * horSubpixels + 0, y * verSubpixels + 0];
                    var tr = buffer[x * horSubpixels + 1, y * verSubpixels + 0];
                    var bl = buffer[x * horSubpixels + 0, y * verSubpixels + 1];
                    var br = buffer[x * horSubpixels + 1, y * verSubpixels + 1];
                    (frame[x+y*width], colors[x+y*width]) = RenderPixel(tl, tr, bl, br);
                }
            return (frame, colors);
        }
        private static char[] lastBuffer = [];
        private static byte[] lastColors = [];
        public static void Print(Vector2 position, float angle, Map map)
        {
            (int width, int height) = (Console.WindowWidth, Console.WindowHeight);
            bool buffered = lastBuffer.Length == width * height;

            byte[,] subpixels = new byte[width * 2, height * 2];

            float fog = 0;
            for (int x = 0; x < width * 2; x++)
            {
                var dir = new Vector2(
                    MathF.Sin(angle - FOV * MathF.PI / 360 * (((float)x) / width - 1)),
                    MathF.Cos(angle - FOV * MathF.PI / 360 * (((float)x) / width - 1))
                    );
                var hit = Ray(position, dir, map);
                int upperSize = (int)(height * ((MathF.PI / 2 - MathF.Atan(hit.distance))));
                upperSize = Math.Clamp(upperSize, 0, height);
                int lowerSize = (int)(height * ((MathF.PI / 2 - MathF.Atan(hit.transparentDistance??hit.distance))));
                lowerSize = Math.Clamp(lowerSize, 0, height);

                float upperL = 1 - MathF.Max(Vector2.Dot(-dir, hit.normal), 0);
                float lowerL = 1 - MathF.Max(Vector2.Dot(-dir, hit.transparentNormal??hit.normal), 0);

                int y = 0;
                for (; y < height - upperSize; y++)
                {
                    subpixels[x, y] = 64;
                }
                for (; y < height + lowerSize; y++)
                {
                    fog += MathF.Min(y >= height?lowerL:upperL, 1);
                    if (fog > 1)
                    {
                        fog -= 1;
                    }
                    else
                    {
                        if (y >= height && hit.item != null)
                        {
                            subpixels[x, y] = 8;
                        }
                        else if (y >= height && hit.enemy != null)
                        {
                            subpixels[x, y] = 4;
                        }
                        else
                        {
                            subpixels[x, y] = hit.tile switch
                            {
                                Tile.Wall => 1,
                                Tile.Door => 2,
                                _ => 0
                            };

                            //if (subpixels[x, y] == 2)
                            //{
                            //    if (hit.normal.X > 0.5f)
                            //        subpixels[x, y] = 2;
                            //    else if (hit.normal.Y > 0.5f)
                            //        subpixels[x, y] = 4;
                            //    else if (hit.normal.X < -0.5f)
                            //        subpixels[x, y] = 8;
                            //    else
                            //        subpixels[x, y] = 16;
                            //}
                        }
                    }
                }
                for (; y < height * 2; y++)
                {
                    subpixels[x, y] = 32;
                }
            }
            (char[] currentBuffer, byte[] currentColors) = RenderFrame(subpixels);
            if(!buffered)
            {
                lastBuffer = new char[width * height];
                lastColors = new byte[width * height];
            }
            //if (buffered)
            {
                try
                {
                    int[] ys = Enumerable.Range(0, height).ToArray();
                    Random.Shared.Shuffle(ys);
                    int drawn = 0;
                    foreach (int y in ys)
                    {
                        if (drawn > 10000)
                            return;
                        int startx = width;
                        int balance = 0;
                        byte color = currentColors[y*width];
                        for (int x = 0; x < width; x++)
                        {
                            bool charMatch = lastBuffer[x + y * width] == currentBuffer[x + y * width];
                            bool colorMatch = lastColors[x + y * width] == currentColors[x + y * width];
                            if (!charMatch || !colorMatch || balance > 0)
                            {
                                if(startx < width && (currentColors[x + y * width] != color || x == width-1))
                                {
                                    Console.SetCursorPosition(startx, y);
                                    Console.ForegroundColor = color switch
                                    {
                                        1=> ConsoleColor.DarkYellow,
                                        2=> ConsoleColor.DarkBlue,
                                        3=> ConsoleColor.Red,
                                        4=> ConsoleColor.Green,
                                        5=> ConsoleColor.Magenta,
                                        6=> ConsoleColor.DarkGray,
                                        7=> ConsoleColor.Blue,
                                        _ => ConsoleColor.Gray,
                                    };
                                    Console.Write(currentBuffer[(startx + y * width)..(x + y * width)]);
                                    Array.Copy(currentBuffer, startx + y * width, lastBuffer, startx + y * width, x - startx+1);
                                    Array.Copy(currentColors, startx + y * width, lastColors, startx + y * width, x - startx+1);

                                    drawn += x-startx;
                                    balance = (!charMatch || !colorMatch) ? 1 : 0;
                                    startx = (!charMatch || !colorMatch) ? x : width;
                                    color = currentColors[x + y * width];
                                    continue;
                                }
                                startx = Math.Min(startx, x);
                                color = currentColors[x + y * width];
                                balance = 1;
                            }
                        }
                    }
                    return;
                }
                catch (Exception) { }
            }
        }
    }
}
