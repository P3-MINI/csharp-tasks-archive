
using System;

struct SPoint
    {
    public int x;
    public int y;

    public SPoint(int px, int py)
        {
        x = px;
        y = py;
        }

    public SPoint(int px)
        {
        x = px;
        y = 0;   // every structure constructor must initialize all fields
        }

//  public SPoint() {}   -  we mustn't define parameterless constructor for structures

    } // struct SPoint

class CPoint
    {
    public int x;
    public int y;

    public CPoint(int px, int py)
        {
        x = px;
        y = py;
        }

    public CPoint(int px)
        {
        x = px;
        // field y will be initialized by default value
        }

    public CPoint() {}  // correct - fields y will be initialized by default values

    } // class CPoint

class Test
    {

    static void Main()
        {

        SPoint s1;                    // object is created, fields are uninitialized!
        SPoint s2 = new SPoint();     // object is created, fields are initialized by default values
        SPoint s3 = new SPoint(1,2);  // object is created, fields are initialized by specified values
        SPoint s4 = new SPoint(3);
        SPoint s5 = s3;               // object is created, fields are initialized by values of s3 object fields

        CPoint c1;                    // object isn't created
        CPoint c2 = new CPoint();     // object is created, fields are initialized by default values
        CPoint c3 = new CPoint(1,2);  // object is created, fields are initialized by specified values
        CPoint c4 = new CPoint(3);
        CPoint c5 = c3;               // objects isn't created, reference c5 is associated with the same object as reference c3

//      Console.WriteLine(" Spoint s1 = ({0},{1})",s1.x,s1.y);  -  error - trying to read of uninitialized fields
        Console.WriteLine(" Spoint s2 = ({0},{1})",s2.x,s2.y);
        Console.WriteLine(" Spoint s3 = ({0},{1})",s3.x,s3.y);
        Console.WriteLine(" Spoint s4 = ({0},{1})",s4.x,s4.y);
        Console.WriteLine(" Spoint s5 = ({0},{1})",s5.x,s5.y);
        Console.WriteLine();

//      Console.WriteLine(" Cpoint c1 = ({0},{1})",c1.x,c1.y);  -  error - object doesn't exist
        Console.WriteLine(" Cpoint c2 = ({0},{1})",c2.x,c2.y);
        Console.WriteLine(" Cpoint c3 = ({0},{1})",c3.x,c3.y);
        Console.WriteLine(" Cpoint c4 = ({0},{1})",c4.x,c4.y);
        Console.WriteLine(" Spoint c5 = ({0},{1})",c5.x,c5.y);
        Console.WriteLine();

        s2 = s3;
        Console.WriteLine(" Spoint s2 = ({0},{1})",s2.x,s2.y);
        Console.WriteLine(" Spoint s3 = ({0},{1})",s3.x,s3.y);
        Console.WriteLine();

        s2.x = 4;
        Console.WriteLine(" Spoint s2 = ({0},{1})",s2.x,s2.y);
        Console.WriteLine(" Spoint s3 = ({0},{1})",s3.x,s3.y);
        Console.WriteLine(" Spoint s5 = ({0},{1})",s5.x,s5.y);
        Console.WriteLine();

        c2 = c3;
        Console.WriteLine(" Cpoint c2 = ({0},{1})",c2.x,c2.y);
        Console.WriteLine(" Cpoint c3 = ({0},{1})",c3.x,c3.y);
        Console.WriteLine();

        c2.x = 4;
        Console.WriteLine(" Cpoint c2 = ({0},{1})",c2.x,c2.y);
        Console.WriteLine(" Cpoint c3 = ({0},{1})",c3.x,c3.y);
        Console.WriteLine(" Spoint c5 = ({0},{1})",c5.x,c5.y);
        Console.WriteLine();

        c1 = new CPoint(5,6);
        s1.x = 5;              // new operator
        s1.y = 6;              // isn't necessary
        Console.WriteLine(" Spoint s1 = ({0},{1})",s1.x,s1.y);
        Console.WriteLine(" Cpoint c1 = ({0},{1})",c1.x,c1.y);
        Console.WriteLine();

        SPoint s6 = new SPoint(7,8);
        SPoint s7 = new SPoint(7,8);
        Console.WriteLine(" Spoint s6 = ({0},{1})",s6.x,s6.y);
        Console.WriteLine(" Spoint s7 = ({0},{1})",s7.x,s7.y);
        Console.WriteLine(" s6 == s7  :  {0}",s6.Equals(s7));
        Console.WriteLine();

        CPoint c6 = new CPoint(7,8);
        CPoint c7 = new CPoint(7,8);
        Console.WriteLine(" Cpoint c6 = ({0},{1})",c6.x,c6.y);
        Console.WriteLine(" Cpoint c7 = ({0},{1})",c7.x,c7.y);
        Console.WriteLine(" c6 == c7  :  {0}",c6.Equals(c7));
//      Console.WriteLine(" c6 == c7  :  {0}",c6==c7);
        Console.WriteLine();

        }

    }
