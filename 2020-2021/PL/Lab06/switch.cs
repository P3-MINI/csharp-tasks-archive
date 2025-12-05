
using System;

namespace PatternMatching
{

class Point2
    {
    public Point2(double _x=0, double _y=0) { x=_x; y=_y; }
    public double x;
    public double y;
    }

class Test
    {

    public static void Main()
        {
        object obj = null;
        int n;
        string buf;

        Console.Write("  Podaj liczbe calkowita z zakresu 1-7:  ");
        buf = Console.ReadLine();
        int.TryParse(buf, out n);

        //Exchange for if-else statement
        switch ( n )
            {
            case 1:
                obj = 1L;
                break;
            case 2:
                obj = (short)2;
                break;
            case 3:
            case 4:
                obj = n;
                break;
            case 5:
                obj = new Point2(5,5);
                break;
            case 6:
                obj = new Point2(1000,2000);
                break;
            case 7:
                obj = 7.0;
                break;
            }

        //Pattern matching
        switch ( obj )
            {
            case long l:
                Console.WriteLine("long");
                break;
            case short s:
                Console.WriteLine("short");
                break;
            case 3:
                Console.WriteLine("3");
                break;
            case int i:
                Console.WriteLine("int");
                break;
            case Point2 p when p.x<100:
                Console.WriteLine($"Point2({p.x},{p.y}, x<100)");
                break;
            case Point2 p when p.x<10:
                Console.WriteLine($"Point2({p.x},{p.y}, x<10)");
                break;
            case Point2 p when p.y<10:
                Console.WriteLine($"Point2({p.x},{p.y}, y<10)");
                break;
            case Point2 p:
                Console.WriteLine($"Point2({p.x},{p.y})");
                break;
            case null:
                Console.WriteLine("null");
                break;
            default:
                Console.WriteLine("default");
                break;
            case var p:
                Console.WriteLine("var");
                break;
            }

        double x = 10.0;
        double y =  5.5;

        bool b = false;
        //Additional condition -> when
        switch ( b )
            {
            case var v when x>0:
                Console.WriteLine("x>10");
                break;
            case var v when y>0:
                Console.WriteLine("y>10");
                break;
            case var v when x<20 && y<10:
                Console.WriteLine("x<20 && y<10");
                break;
            }

        }  // method Main

    }  // class Test

}  // namespace PatternMatching