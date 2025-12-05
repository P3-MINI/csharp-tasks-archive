
using System;

class CPoint
    {
    public int x;
    public int y;

    public CPoint(int px=10, int py=20)
        {
        x = px;
        y = py;
        }
    }

struct SPoint
    {
    public int x;
    public int y;

    public SPoint(int px=10, int py=20)
        {
        x = px;
        y = py;
        }
    }

class Test
    {

    static void funC(CPoint cp)
        {
        cp.x = 30;
        }

    static void funCr(ref CPoint cp)
        {
        cp.x = 30;
        }

    static void funS(SPoint sp)
        {
        sp.x = 30;
        }

    static void funSr(ref SPoint sp)
        {
        sp.x = 30;
        }

    static void swapC(CPoint cp1, CPoint cp2)
        { 
        CPoint p;
        p = cp1;
        cp1 = cp2;
        cp2 = p;
        }

    static void swapCr(ref CPoint cp1, ref CPoint cp2)
        {
        CPoint p;
        p = cp1;
        cp1 = cp2;
        cp2 = p;
        }

    static void Main()
        {
        CPoint c1 = new CPoint(1,2);
        CPoint c2 = new CPoint(1,2);
        SPoint s1 = new SPoint(3,4);
        SPoint s2 = new SPoint(3,4);

        funC(c1);
        funCr(ref c2);
        funS(s1);
        funSr(ref s2);

        Console.WriteLine("  {0} ,  {1} ,  {2} ,  {3}", c1.x, c2.x, s1.x, s2.x);
                                                     //   30,   30,    3,   30
        CPoint c3 = new CPoint(3,3);
        CPoint c4 = new CPoint(4,4);
        CPoint c5 = new CPoint(5,5);
        CPoint c6 = new CPoint(6,6);

        swapC(c3, c4);
        swapCr(ref c5, ref c6);

        Console.WriteLine("  {0} ,  {1} ,  {2} ,  {3}", c3.x, c4.x, c5.x, c6.x);
                                                     //    3,    4,    6,    5
        SPoint s7 = new SPoint();   // automatically generated constructor is used
        CPoint c7 = new CPoint();   // constructor with 2 optional parameters (omitted in this invocation) is used

        Console.WriteLine("  {0} ,  {1} ,  {2} ,  {3}", s7.x, s7.y, c7.x, c7.y);
                                                     //    0,    0,   10,   20
        }

    }
