
using System;

struct Point
{
   public double x;
   public double y;
   public Point(double px, double py) { x=px; y=py; }
    
   public override string ToString()
   {
       return string.Format($"({x},{y})");
   }
}

abstract class Figure
{
   public abstract Figure Clone();
   public abstract void Scale(double s);

   public static Figure MakeScaledFigure(Figure f, double s)
   {
       Figure nf = f.Clone();
       nf.Scale(s);
       return nf;
   }

}  // Figure

class Triangle : Figure
{
   public readonly Point[] v;

   public Triangle(Point a, Point b, Point c)
   {
       v = new Point[3];
       v[0] = a;
       v[1] = b;
       v[2] = c;
   }
  
   public override Figure Clone()
   {
       return new Triangle(v[0],v[1],v[2]);
   }

   public override void Scale(double s)
   {
       for ( int i=0 ; i<3 ; ++i )
       {
           v[i].x *= s;
           v[i].y *= s;
       }
   }

   public override string ToString()
   {
       return string.Format("Triangle ( {0} , {1} , {2} )",v[0],v[1],v[2]);
   }
}  // Triangle

class Circle : Figure
{
   public Point centre;
   public double radius;

   public Circle(Point c, double r)
   {
       centre = c;
       radius = r;
   }
  
   public override Figure Clone()
   {
       return new Circle(centre,radius);
   }

   public override void Scale(double s)
   {
       centre.x *= s;
       centre.y *= s;
       radius *= s;
   }

   public override string ToString()
   {
       return string.Format("Circle ( {0} , {1} )", centre, radius);
   }
}  // Circle

class Example
{
   static void Main()
   {
       Triangle t = new Triangle( new Point(1,2), new Point(1,6), new Point(4,2) );
       Circle c = new Circle( new Point(4,5), 3 );

       Figure ft = Figure.MakeScaledFigure(t,2.5);
       Figure fc = Figure.MakeScaledFigure(c,3);

       Console.WriteLine(t);
       Console.WriteLine(c);
       Console.WriteLine();
       Console.WriteLine(ft);
       Console.WriteLine(fc);
       Console.WriteLine();

       //  v - readonly field
       t.v[0].x = 10;
       t.v[1] = new Point(20,30);
       //  t.v = new Point[4];        - error
   }
}
