using System;
using System.Collections;

namespace EN_Lab_07
{
    public interface IGeometryObject
    {
        string Name { get; }

        IEnumerable GetPoints();

        double CalculateCircuit() // C# 8.0
        {
            double circuit = 0.0;
            bool isFirst = true;
            Point2D first, prev;
            first = prev = new Point2D();  // needed by compiler
            foreach (Point2D point in GetPoints())
            {
                if ( isFirst )
                {
                    first = prev = point;
                    isFirst = false;
                }
                else
                {
                    circuit += IGeometryObject.DistanceBetweenPoints(prev, point);
                    prev = point;
                }
            }
            circuit += IGeometryObject.DistanceBetweenPoints(prev, first);

            return circuit;
        }

        void PrintGeometryObject() // C# 8.0
        {
            Console.WriteLine($"This is {Name}, its circuit equals {CalculateCircuit()} and it contains below points:");
            foreach (var point in GetPoints())
            {
                Console.WriteLine(point);
            }
        }

        static double DistanceBetweenPoints(Point2D p1, Point2D p2) // C# 8.0
        {
            var dx = p1.X - p2.X;
            var dy = p1.Y - p2.Y;

            return Math.Sqrt(dx * dx + dy * dy);
        }
    }

    public class Polygon : IGeometryObject, IEnumerable
    {
        public string Name { get { return "Polygon"; } }

        private readonly Point2D[] vertices;

        public Polygon(Point2D[] vertices)
        {
            this.vertices = vertices;
        }

        public IEnumerable GetPoints()
        {
            return vertices;
        }

        public IEnumerator GetEnumerator()
        {
            return GetPoints().GetEnumerator();
        }
    }

    public class Circle : IGeometryObject
    {
        public string Name { get { return "Circle"; } }

        private readonly Point2D center;
        private readonly double radius;

        public Circle(Point2D center, double radius)
        {
            this.center = center;
            this.radius = radius;
        }

        public double CalculateCircuit()
        {
            return 2 * Math.PI * radius;
        }

        public IEnumerable GetPoints()
        {
            int numberOfPoints = 100;  // arbitrary number of points
            var step = 2 * Math.PI / numberOfPoints;
            var alfa = 0.0;
            while (numberOfPoints > 0)
            {
                var x = center.X + radius * Math.Cos(alfa);
                var y = center.Y + radius * Math.Sin(alfa);

                yield return new Point2D(x, y);
                --numberOfPoints;
                alfa += step;
            }
            yield break;
        }

    }
}
