using System;
using System.Text;

namespace Lab05
{
    /*
        Stage_1 (1 Point) create struct 'Point' representing point in R^2 space (it should contain two doubles)
            * Implement public constructor with 2 parameters of correct type.
            * Implement public method 'Length' which calculates distance from an origin (O,0) to the point  
            * Implement public method 'ToString' inherited from 'Object' class, which returns '[X,Y]'.
    */

    public struct Point
    {
        public double X;
        public double Y;

        public Point(double _x, double _y)
        {
            this.X = _x;
            this.Y = _y;
        }

        public double Length()
        {
            return Math.Sqrt(this.X * this.X + this.Y * this.Y);
        }

        public override string ToString()
        {
            return $"[{this.X},{this.Y}]";
        }
    }

    /*
        Stage_2 (1 point): Create abstract class 'Figure' representing geometric figure in R^2 space.
            * It should contain array of points accessible from derived classes (but not from outside).
            * Implement public constructor which takes variable amount of points.
                * Initialize array of points and copy received points.
            * Implement public method 'ComputeCenter' which calculates the center of the figure.
            * Implement public virtual method 'Description' which returns a following description 'Points: [x1,y1] [x2,y2] ...'.
                * Use ToString method from class Point.
                * U can use ordinary operations on 'String' class, but for those type of operations use of 'StringBuilder' class is highly recommended.  
            * Create public abstract method 'Area' which will calculate area of the figure.  
    */
    public abstract class Figure
    {
        protected Point[] Points;

        protected Figure(params Point[] points)
        {
            this.Points = new Point[points.Length];

            for (int i = 0; i < this.Points.Length; i++)
                this.Points[i] = points[i];
        }

        public Point ComputeCenter()
        {
            Point resultPoint = new Point(0, 0);

            /* Studenci tutaj używają pętli for, ponieważ foreach jeszcze nie było. */
            foreach (Point point in this.Points)
            {
                resultPoint.X += point.X;
                resultPoint.Y += point.Y;
            }

            resultPoint.X /= this.Points.Length;
            resultPoint.Y /= this.Points.Length;

            return resultPoint;
        }

        public abstract double Area();

        public virtual string Description()
        {
            /* Jeśli studenci mieliby pytania można powiedzieć, żeby przeczytali dokumentację lub opowiedzieć podstawy na tablicy. */

            /* Akceptowalne mogą być użycia klasy String, ponieważ StringBuilder jeszcze nie było. */
            StringBuilder stringBuilder = new StringBuilder("Points:");

            /* Studenci tutaj używają pętli for, ponieważ foreach jeszcze nie było. */
            foreach (Point point in this.Points)
                stringBuilder.Append($" {point}");

            return stringBuilder.ToString();
        }
    }

    /*
        Stage_3 (3 points) Create classes 'Circle' and 'Rectangle', which are derived classes of 'Figure'.
            * Class 'Circle' (1.5 point):
                * Should contain one field of type double representing radius of a circle.
                * Implement public constructor which takes 2 parameters, representing the center of a circle and its radius.
                * Implement two methods 'GetCenter' and 'GetRadius' that return corresponding values.
                * Implement public method 'MaxPoint', which returns distance to the point on a circle which is furthers from the origin (0,0).
                    * (Distance from origin to circle center + radius)
                * Implement public method 'Area' inherited from the base class.
                * Implement public method 'Description' which describes circle in a following way: "Circle with center at: 'center' and radius: 'radius'".
            * Class 'Rectangle' (1.5 point):
                * Shouldn't contain any fields.
                * Implement constructor which takes two points (NW, SE), that defines rectangle.
                * Implement two methods 'GetPoints' that return the copy of points array.  
                * Implement public method 'Area' inherited from the base class.
                * Implement public method 'Description' which describes circle in a following way: "Rectangle with" appending points calling the base class.
    */
    public class Circle : Figure
    {
        private double _radius;

        public Circle(Point center, double radius) : base(center)
        {
            this._radius = radius;
        }

        public Point GetCenter()
        {
            return Points[0];
        }
        
        public double GetRadius()
        {
            return _radius;
        }
        
        public double MaxPoint()
        {
            Point center = base.Points[0];

            return (center.Length() + this._radius);
        }

        public override double Area()
        {
            return (Math.PI * this._radius * this._radius);
        }

        public override string Description()
        {
            Point center = base.Points[0];

            return $"Circle With Center At: [{center.X},{center.Y}] And Radius: {this._radius}";
        }
    }

    public class Rectangle : Figure
    {
        public Rectangle(Point NW, Point SE) : base(NW, SE) { }

        public Point[] GetPoints()
        {
            return new[] {Points[0], Points[1]};
        }

        public override double Area()
        {
            Point point1 = base.Points[0];
            Point point2 = base.Points[1];

            Point point3 = new Point(point1.X, point2.Y);
            Point point4 = new Point(point2.X, point1.Y);

            Point aSide = new Point(point1.X - point4.X, point1.Y - point4.Y);
            Point bSide = new Point(point2.X - point4.X, point2.Y - point4.Y);

            return (aSide.Length() * bSide.Length());
        }

        public override string Description()
        {
            return ("Rectangle With " + base.Description());
        }
    }
}
