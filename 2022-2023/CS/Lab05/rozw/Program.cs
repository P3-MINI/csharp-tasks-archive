#define STAGE_1
#define STAGE_2

using System;

namespace POL_Lab05
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("********************* Stage_1 (1 Point) *********************");
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

            Console.WriteLine(); Console.WriteLine("********************* Stage_2/3 (4 Points) *********************");
#if STAGE_2
            {
                Figure[] figures = new Figure[]
                {
                    new Circle(new Point(0,0), 5.0),
                    new Circle(new Point(1,5), 16.0),
                    new Circle(new Point(-7,3), 9.0),
                    new Circle(new Point(9,-2), 1.0),
                    new Rectangle ( new Point(5, 7), new Point(9, 9)),
                    new Rectangle ( new Point(-1, 1), new Point(1, -1)),
                    new Rectangle ( new Point(-9, 8), new Point(-6, 6)),
                };

                foreach (Figure figure in figures)
                    Console.WriteLine($"{figure.Description()} - Area = {figure.Area():0.00}");

                Console.WriteLine(); Program.Stage23_1(figures);
                Console.WriteLine(); Program.Stage23_2(figures);
                Console.WriteLine(); Program.Stage23_3(figures);
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
                        var points = rectangle.GetPoints();
                        Console.WriteLine($"Deconstruction: Rectangle Points: {points[0]} {points[1]}");

                    }
                    break;
                    case Circle circle:
                    {
                        Console.WriteLine($"Deconstruction: Circle Center: {circle.GetCenter()} Radius: {circle.GetRadius()}");
                    }
                    break;
                }
        }
#endif
    }
}
