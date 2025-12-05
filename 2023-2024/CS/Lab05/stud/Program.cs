//#define STAGE_1
//#define STAGE_2
//#define STAGE_3
using System;

namespace Lab05
{
    class Program
    {
        private static void PrintStage(int stage)
        {
            Console.WriteLine($"\n----------------------Stage {stage}-------------------------\n");
        }

        private static void MovePoint(Vector2 point)
        {
            point = Vector2.Add(point, new Vector2(1, 0));
        }

        public static void Main(string[] args)
        {
            #if STAGE_1
            PrintStage(1);
            Console.WriteLine("//// Creating objects of type Vector2");
            Vector2 pointS0 = new Vector2(1.0f, 0);
            Console.WriteLine(pointS0);
            MovePoint(pointS0);
            Console.WriteLine(pointS0); // Shouldn't change points position

            Console.WriteLine("//// Creating objects of type Circle and Triangle");
            Circle circle = new Circle(pointS0, 1.0f);
            Console.WriteLine("Position in Circle object: " + circle.GetRelativePosition());
            Console.WriteLine(circle);

            Vector2 pointC1 = new Vector2(1.0f, 0);
            Triangle triangle1 = new Triangle(pointC1, new Vector2(0,1), new Vector2(1,0), new Vector2(1,1));
            Console.WriteLine("Position in Triangle object: " + triangle1.GetRelativePosition());
            Console.WriteLine(triangle1);
            #endif
            #if STAGE_2
            PrintStage(2);
            Console.WriteLine("//// Creating tree of the objects");
            Vector2 pointC2 = new Vector2(3.0f, 0);
            Triangle triangle2 = new Triangle(pointC2, new Vector2(0,1), new Vector2(1,0), new Vector2(1,1));

            Vector2 pointS3 = new Vector2(4.0f, 0);
            Circle circle3 = new Circle(pointS3, 4.0f);

            circle.AddChild(triangle1);
            circle.AddChild(triangle2);
            triangle2.AddChild(circle3);

            Console.WriteLine(circle.GetTreeString());
            Console.WriteLine("Circle max number of children: " + circle.GetMaxNumberOfChildren());
            Console.WriteLine();

            Console.WriteLine("//// Extending child array capacity");
            Vector2 pointS4 = new Vector2(5.0f, 0);
            Circle circle4 = new Circle(pointS4, 5.0f);
            circle.AddChild(circle4);
            Console.WriteLine(circle.GetTreeString());
            Console.WriteLine("Circle max number of children: " + circle.GetMaxNumberOfChildren());

            Console.WriteLine("\n//// Moving tree");
            circle.Move(new Vector2(1, 0));
            Console.WriteLine(circle.GetTreeString());

            #endif
            #if STAGE_3
            PrintStage(3);
            Console.WriteLine("//// Calculating global coordinates");
            Console.WriteLine(circle.GetTreeString());

            Console.WriteLine("//// Creatng BoundingBox from pointS0 and pointS4");
            Console.WriteLine(pointS0);
            Console.WriteLine(pointS4);
            BoundingBox bb = new BoundingBox(pointS0, pointS4);
            Console.WriteLine(bb);
            Console.WriteLine();

            Console.WriteLine("//// Calculating BoundingBox for objects triangle1 i circle4");
            Console.WriteLine("Triangle1 - " + triangle1.GetFigureBoundingBox());
            Console.WriteLine("Circle4 - " + circle4.GetFigureBoundingBox());

            Console.WriteLine("//// Calculating BoundingBox for subtree");
            Console.WriteLine("Bounding box of the whole tree");
            Console.WriteLine(circle.CalculateBoundingBox());
            Console.WriteLine();
            Console.WriteLine("Bounding box for subtree with root in triangle2");
            Console.WriteLine(triangle2.CalculateBoundingBox());
            #endif
        }
    }

}

