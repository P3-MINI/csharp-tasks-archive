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

        private static void MovePoint(Vector3 point)
        {
            Vector3.Add(point, new Vector3(1, 0, 0));
        }

        public static void Main(string[] args)
        {
            #if STAGE_1
            PrintStage(1);
            Console.WriteLine("//// Tworzenie obiektów typu Vector3");
            Vector3 pointS0 = new Vector3(0.1f, 0, 1.0f);
            Console.WriteLine(pointS0);
            MovePoint(pointS0);
            Console.WriteLine(pointS0); // Nie powinno zmienić pozycji punktu

            Console.WriteLine("//// Tworzenie obiektów typu Sphere i Cylinder");
            Sphere sphere = new Sphere(pointS0, 1.0f);
            Console.WriteLine("Position in Sphere object: " + sphere.GetPosition());
            Console.WriteLine(sphere);

            Vector3 pointC1 = new Vector3(0.2f, 0, 2.0f);
            Cylinder cylinder1 = new Cylinder(pointC1, 2.0f, 4.0f);
            Console.WriteLine("Position in Cylinder object: " + cylinder1.GetPosition());
            Console.WriteLine(cylinder1);

            Console.WriteLine("//// Tworzenie drzewa obiektów");
            Vector3 pointC2 = new Vector3(0.3f, 0, 3.0f);
            Cylinder cylinder2 = new Cylinder(pointC2, 4.0f, 8.0f);

            Vector3 pointS3 = new Vector3(0.4f, 0, 4.0f);
            Sphere sphere3 = new Sphere(pointS3, 4.0f);

            sphere.AddChild(cylinder1);
            sphere.AddChild(cylinder2);
            cylinder2.AddChild(sphere3);

            Console.WriteLine(sphere.GetTreeString());
            Console.WriteLine("Sphere max number of children: " + sphere.GetMaxNumberOfChildren());
            Console.WriteLine();

            Console.WriteLine("//// Rozszerzanie tablicy dzieci");
            Vector3 pointS4 = new Vector3(0.5f, 0, 5.0f);
            Sphere sphere4 = new Sphere(pointS4, 5.0f);
            sphere.AddChild(sphere4);
            Console.WriteLine(sphere.GetTreeString());
            Console.WriteLine("Sphere max number of children: " + sphere.GetMaxNumberOfChildren());
            #endif
            #if STAGE_2
            PrintStage(2);
            Console.WriteLine("//// Liczenie współrzędnych w kartezjańskim układzie współrzędnych");
            Console.WriteLine(sphere.GetTreeString());

            Console.WriteLine("//// Tworzenie bounding boxa z pointS0 i pointS4");
            Console.WriteLine(pointS0);
            Console.WriteLine(pointS4);
            BoundingBox bb = new BoundingBox(pointS0, pointS4);
            Console.WriteLine(bb);
            Console.WriteLine();

            Console.WriteLine("//// Liczenie bounding boxa dla obiektów cylinder1 i sphere4");
            Console.WriteLine("Cylinder1 - " + cylinder1.GetFigureBoundingBox());
            Console.WriteLine("Sphere4 - " + sphere4.GetFigureBoundingBox());
            #endif
            #if STAGE_3
            PrintStage(3);
            Console.WriteLine("//// Liczenie bounding boxa dla danego poddrzewa");
            Console.WriteLine("Bounding box całego drzewa");
            Console.WriteLine(sphere.CalculateBoundingBox());
            Console.WriteLine();
            Console.WriteLine("Bounding box dla poddrzewa zaczynającego się w cylinder2");
            Console.WriteLine(cylinder2.CalculateBoundingBox());
            #endif
        }
    }

}

