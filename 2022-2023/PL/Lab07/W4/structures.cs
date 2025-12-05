
#nullable disable

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
        y = 3;   // all fields must be initialized (in constructor or structure declaration)
    }

    public SPoint()
    {
        x = -1;   // all fields must be initialized
        y = -2;   // (in constructor or structure declaration)
    }

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

    public CPoint() { }  // correct - fields y will be initialized by default values

} // class CPoint

class Test
{

    static void Main()
    {

        SPoint s1;                    // object is created, fields are uninitialized!
        SPoint s2 = new SPoint();     // object is created, fields are initialized by values specified by explicitly defined parameterless constructor
        SPoint s3 = new SPoint(1, 2);  // object is created, fields are initialized by specified values
        SPoint s4 = new SPoint(3);
        SPoint s5 = s3;               // object is created, fields are initialized by values of s3 object fields
        SPoint s6 = default;          // object is created, fields are initialized by default values

        CPoint c1;                    // object isn't created, reference is unlnitialized
        CPoint c2 = new CPoint();     // object is created, fields are initialized by default values
        CPoint c3 = new CPoint(1, 2);  // object is created, fields are initialized by specified values
        CPoint c4 = new CPoint(3);
        CPoint c5 = c3;               // object isn't created, reference c5 is associated with the same object as reference c3
        CPoint c6 = default;          // object isn't created, reference is equal null (declaration is equivalent to CPoint c = null)

        //      Console.WriteLine(" Spoint s1 = ({0},{1})",s1.x,s1.y);  -  compilation error - trying to read of uninitialized fields
        Console.WriteLine(" Spoint s2 = ({0},{1})", s2.x, s2.y);
        Console.WriteLine(" Spoint s3 = ({0},{1})", s3.x, s3.y);
        Console.WriteLine(" Spoint s4 = ({0},{1})", s4.x, s4.y);
        Console.WriteLine(" Spoint s5 = ({0},{1})", s5.x, s5.y);
        Console.WriteLine(" Spoint s6 = ({0},{1})", s6.x, s6.y);
        Console.WriteLine();

        //      Console.WriteLine(" Cpoint c1 = ({0},{1})",c1.x,c1.y);  -  compilation error - trying to use of uninitialized reference
        Console.WriteLine(" Cpoint c2 = ({0},{1})", c2.x, c2.y);
        Console.WriteLine(" Cpoint c3 = ({0},{1})", c3.x, c3.y);
        Console.WriteLine(" Cpoint c4 = ({0},{1})", c4.x, c4.y);
        Console.WriteLine(" Cpoint c5 = ({0},{1})", c5.x, c5.y);
        //      Console.WriteLine(" Cpoint c6 = ({0},{1})",c6.x,c6.y);  -  runtime error - trying to use of null reference
        Console.WriteLine(" Cpoint c6 == null : {0}", c6 == null);
        Console.WriteLine();

        s2 = s3;
        Console.WriteLine(" Spoint s2 = ({0},{1})", s2.x, s2.y);
        Console.WriteLine(" Spoint s3 = ({0},{1})", s3.x, s3.y);
        Console.WriteLine();

        s2.x = 4;
        Console.WriteLine(" Spoint s2 = ({0},{1})", s2.x, s2.y);
        Console.WriteLine(" Spoint s3 = ({0},{1})", s3.x, s3.y);
        Console.WriteLine(" Spoint s5 = ({0},{1})", s5.x, s5.y);
        Console.WriteLine();

        c2 = c3;
        Console.WriteLine(" Cpoint c2 = ({0},{1})", c2.x, c2.y);
        Console.WriteLine(" Cpoint c3 = ({0},{1})", c3.x, c3.y);
        Console.WriteLine();

        c2.x = 4;
        Console.WriteLine(" Cpoint c2 = ({0},{1})", c2.x, c2.y);
        Console.WriteLine(" Cpoint c3 = ({0},{1})", c3.x, c3.y);
        Console.WriteLine(" Cpoint c5 = ({0},{1})", c5.x, c5.y);
        Console.WriteLine();

        s1.x = 5;              // new operator
        s1.y = 6;              // isn't necessary
        c1 = new CPoint(5, 6);
        Console.WriteLine(" Spoint s1 = ({0},{1})", s1.x, s1.y);
        Console.WriteLine(" Cpoint c1 = ({0},{1})", c1.x, c1.y);
        Console.WriteLine();

        SPoint s7 = new SPoint(7, 8);
        SPoint s8 = new SPoint(7, 8);
        Console.WriteLine(" Spoint s7 = ({0},{1})", s7.x, s7.y);
        Console.WriteLine(" Spoint s8 = ({0},{1})", s8.x, s8.y);
        Console.WriteLine(" s7 == s8  :  {0}", s7.Equals(s8));
        //      Console.WriteLine(" s7 == s8  :  {0}",s7==s8);   //  error - it is correct only if equality operator is explicitly defined
        Console.WriteLine();

        CPoint c7 = new CPoint(7, 8);
        CPoint c8 = new CPoint(7, 8);
        Console.WriteLine(" Cpoint c7 = ({0},{1})", c7.x, c7.y);
        Console.WriteLine(" Cpoint c8 = ({0},{1})", c8.x, c8.y);
        Console.WriteLine(" c7 == c8  :  {0}", c7.Equals(c8));
        Console.WriteLine(" c7 == c8  :  {0}", c7 == c8);
        Console.WriteLine();

    }

}
