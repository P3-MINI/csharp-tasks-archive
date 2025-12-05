using System;

namespace lab06
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("\n== STAGE 1 ==\n");

            Point2D origin = new Point2D();
            Point2D p1 = new Point2D(1, 2);
            Point2D p2 = new Point2D(1);
            Point2D p3 = new Point2D(x: 1, y: -1);
            Point2D p4 = new Point2D(y: 1);
            Point2D p5 = new Point2D(y: 1, x: 1);
            Point2D p6 = new Point2D(x: 1);

            Console.WriteLine("\n== STAGE 2 ==\n");

            Point2D target = new Point2D(y: 4, x: 3);

            Console.WriteLine("Distance between {0} and {1} is {2:n2}", origin, target, Geometry.Distance(origin, target));
            Console.WriteLine("Distance between {0} and {0} is {2:n2}", origin, origin, Geometry.Distance(origin, origin));
            Console.WriteLine("Circuit of polygon created from p1, p2, p3 is {0:n2}", Geometry.PolygonCircuit(p1, p2, p3));
            Console.WriteLine("Circuit of polygon created from p4, p5, p6 is {0:n2}", Geometry.PolygonCircuit(new Point2D[] { p4, p5, p6 }));


            Console.WriteLine("\n== STAGE 3 ==\n");

            var c1 = new Circle(p2, 5.0);
            var r1 = new Rectangle(p3, 2, 4);
            var r2 = new Rectangle(p4, 2);

            object[] objects = new object[]
            {
                string.Empty, c1, r1, r2, 42, p1, 
            };

            Console.WriteLine("\n== STAGE 4 ==\n");
            {
                for (int i = 0; i < objects.Length; i++)
                {
                    Console.WriteLine($"\nDescribing object {i + 1}:");

                    Shape shape = objects[i] as Shape;

                    if (shape is null)
                    {
                        Console.WriteLine("Object is not a shape.");
                        continue;
                    }

                    Console.WriteLine("Object is a shape");
                    Console.WriteLine("Shape has circuit of {0:n2}", shape.Circuit());

                    if (shape is Polygon polygon)
                    {
                        Console.WriteLine("Shape is a Polygon");
                        Console.WriteLine("List of points in polygon:");
                        for (int j = 0; j < polygon.points.Length; j++)
                        {
                            Console.WriteLine(polygon.points[j]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Shape is not a Polygon");
                    }

                    if (shape is Circle c)
                    {   
                        Console.WriteLine($"Shape is a Circle that has center at {c.center} and radius of {c.radius}");
                    }

                    if (shape is Rectangle r)
                    {
                        Console.WriteLine($"Shape is a Rectangle and has diagonal of {r.diagonal:n2}");
                    }
                }
            }

            Console.WriteLine("\n== STAGE 5 ==\n");
            {
                double x, y, r; 

                ((x, y), r) = c1;

                Console.WriteLine("Distance of the closest point on the circle to origin point is {0:n2}", Geometry.ToOriginDistance((x, y), r));
            }

            Console.WriteLine("\n== END ==\n");
        }

    }
}
