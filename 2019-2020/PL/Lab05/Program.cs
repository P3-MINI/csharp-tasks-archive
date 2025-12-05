#define STAGE_1
#define STAGE_2
#define STAGE_4

using System;

namespace POL_Lab05
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("********************* Etap_1 (0.5 Pkt) *********************");
#if STAGE_1
            {
                Point[] points_1 = new Point[]
                {
                    new Point(0,0),
                    new Point(1,1),
                    new Point(3,4),
                    new Point(7,1),
                    new Point(3,-8),
                    new Point(-7,2),
                    new Point(-5,1),
                    new Point(-1,-5),
                };

                foreach (Point point in points_1)
                    Console.WriteLine($"Point {point} With Length = {point.Length(),2:0.00}");
            }
#endif

            Console.WriteLine(); Console.WriteLine("********************* Etap_2/3 (3.0 Pkt) *********************");
#if STAGE_2
            {
                Figure[] figures = new Figure[]
                {
                    new Circle(new Point(0,0), 5.0),
                    new Circle(new Point(1,5), 16.0),
                    new Circle(new Point(-7,3), 9.0),
                    new Circle(new Point(9,-2), 1.0),
                    new Rectangle (( new Point(5, 7), new Point(9, 9))),
                    new Rectangle (( new Point(-1, 1), new Point(1, -1))),
                    new Rectangle (( new Point(-9, 8), new Point(-6, 6))),
                };

                foreach (Figure figure in figures)
                    Console.WriteLine($"{figure.Description()} - Area = {figure.Area():0.00}");

                Console.WriteLine(); Program.Stage23_1(figures);
                Console.WriteLine(); Program.Stage23_2(figures);
                Console.WriteLine(); Program.Stage23_3(figures);
            }
#endif

            Console.WriteLine(); Console.WriteLine("********************* Etap_4 (1.5 Pkt) *********************");
#if STAGE_4
            {
                Point[] points_1 = new Point[]
                {
                    new Point(0,0),
                    new Point(1,1),
                    new Point(3,4),
                    new Point(7,1),
                    new Point(3,-8),
                    new Point(-7,2),
                    new Point(-5,1),
                    new Point(-1,-5),
                };

                EuclidianSpace euclidianSpace = new EuclidianSpace(points_1,
                    new Circle(new Point(1, 5), 16.0),
                    new Circle(new Point(-7, 3), 9.0),
                    new Circle(new Point(9, -2), 1.0),
                    new Rectangle(( new Point(5, 7), new Point(9, 9))),
                    new Rectangle(( new Point(-1, 1), new Point(1, -1))),
                    new Rectangle(( new Point(-9, 8), new Point(-6, 6))));

                Program.Stage4_3(euclidianSpace); Console.WriteLine();
                Program.Stage4_4(euclidianSpace); Console.WriteLine();
            }
#endif
        }

#if STAGE_2
        private static void Stage23_1(Figure[] figures)
        {
            foreach (Figure figure in figures)
                if (figure is Rectangle)
                {
                    Point center = (figure as Rectangle).ComputeCenter();

                    Console.WriteLine($"Rectangle With Center {center} Found");
                }
        }

        private static void Stage23_2(Figure[] figures)
        {
            foreach (Figure figure in figures)
                if (figure is Circle)
                {
                    double maxPoint = (figure as Circle).MaxPoint();

                    Console.WriteLine($"Circle With Max Point {maxPoint:0.00} Found");
                }
        }

        private static void Stage23_3(Figure[] figures)
        {
            foreach (Figure figure in figures)
                switch (figure)
                {
                    case Rectangle rectangle:
                    {
                        var (point1, point2) = rectangle;

                        Console.WriteLine($"Deconstruction: Rectangle Points: {point1} {point2}");

                    }
                    break;
                    case Circle circle:
                    {
                        (Point center, double radius) = circle;

                        Console.WriteLine($"Deconstruction: Circle Center: {center} Radius: {radius}");
                    }
                    break;
                }
        }
#endif

#if STAGE_4
        private static void Stage4_3(EuclidianSpace euclidianSpace)
        {
            Circle newCircle = euclidianSpace.CreateCircle();

            Console.WriteLine($"Circle From Radius As Sum: {newCircle.Description()}");
        }

        private static void Stage4_4(EuclidianSpace euclidianSpace)
        {
            (Rectangle rectangleA, Rectangle rectangleB) = euclidianSpace.GetTheFurthestRectangles(out double maxDistance);

            Console.WriteLine($"Rectangles Found At Distance = {maxDistance}");
            Console.WriteLine($"Rectangle_1: {rectangleA.Description()}");
            Console.WriteLine($"Rectangle_2: {rectangleB.Description()}");
        }
#endif
    }
}
