using System;

namespace lab06
{
    public struct Point2D
    {
        public readonly double x;
        public readonly double y;

        public Point2D(double x = 0, double y = 0)
        {
            this.x = x;
            this.y = y;
            Console.WriteLine($"Point2D{this} has been created.");
        }

        public override string ToString()
        {
            return $"({x}, {y})";
        }

        public void Deconstruct(out double x, out double y)
        {
            x = this.x;
            y = this.y;
        }
    }

    public static class Geometry
    {
        public static double Distance(in Point2D p1, in Point2D p2)
        {
            double dx = p1.x - p2.x;
            double dy = p1.y - p2.y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public static double PolygonCircuit(params Point2D[] points)
        {
            if (points.Length < 3)
            {
                return -1.0;
            }

            double length = 0.0;
            for (int i = points.Length - 1, j = 0; j < points.Length; i = j, j++)
            {
                length += Distance(points[i], points[j]);
            }
            return length;
        }

        public static double ToOriginDistance((double x, double y) point, double radius)
        {
            return Math.Abs(Math.Sqrt(point.x * point.x + point.y * point.y) - radius);
        }
    }

    public abstract class Shape
    {
        public abstract double Circuit();
    }

    public abstract class Polygon : Shape
    {
        public readonly Point2D[] points;

        protected Polygon(params Point2D[] points)
        {
            this.points = points;
        }

        public override double Circuit()
        {
            return Geometry.PolygonCircuit(points);
        }
    }

    public class Rectangle : Polygon
    {
        public readonly double diagonal;
        public Rectangle(Point2D leftBottomPoint, double width, double height) : base(
            leftBottomPoint,
            new Point2D(leftBottomPoint.x, leftBottomPoint.y + height),
            new Point2D(leftBottomPoint.x + width, leftBottomPoint.y + height),
            new Point2D(leftBottomPoint.x + width, leftBottomPoint.y)
        )
        {
            diagonal = Geometry.Distance(points[0], points[2]);
        }

        public Rectangle(Point2D leftBottomPoint, double side) : this (leftBottomPoint, side, side) { }
    }

    public class Circle : Shape
    {
        public readonly Point2D center;
        public readonly double radius;

        public Circle(Point2D center, double radius)
        {
            this.center = center;
            this.radius = radius;
        }

        public override double Circuit()
        {
            return 2 * Math.PI * radius;
        }

        public void Deconstruct(out Point2D center, out double radius)
        {
            center = this.center;
            radius = this.radius;
        }
    }
}
