using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
namespace ConsolePainter;
internal abstract class CoolWindow
{
    public bool bold;
    public (int x, int y) start, end;

    public CoolWindow((int x, int y) start, (int x, int y) end)
    {
        this.start = start;
        this.end = end;
    }

    public void DrawBorder()
    {
        Guard.Begin();

        char h = bold ? '═' : '─';
        char v = bold ? '║' : '│';
        char lu = bold ? '╔' : '┌';
        char rd = bold ? '╝' : '┘';
        char ru = bold ? '╗' : '┐';
        char ld = bold ? '╚' : '└';
        Console.SetCursorPosition(start.x + 1, start.y);
        Console.Write(new string(h, end.x - start.x - 1));
        Console.SetCursorPosition(start.x + 1, end.y);
        Console.Write(new string(h, end.x - start.x - 1));

        for (int y = start.y +1; y < end.y; y++)
        {
            Console.SetCursorPosition(start.x, y);
            Console.Write(v);
            Console.SetCursorPosition(end.x, y);
            Console.Write(v);
        }
        Console.SetCursorPosition(start.x, start.y);
        Console.Write(lu);
        Console.SetCursorPosition(start.x, end.y);
        Console.Write(ld);
        Console.SetCursorPosition(end.x, start.y);
        Console.Write(ru);
        Console.SetCursorPosition(end.x, end.y);
        Console.Write(rd);
        Guard.End();
    }
    protected abstract void DrawContent();
    protected (int x, int y) StartContent { get { return (start.x + 1, start.y + 1); } }
    protected (int x, int y) EndContent { get { return (end.x - 1, end.y - 1); } }
}