using System;

namespace Lab05_pl
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintStage(1);

            /*Klasa Shape2D i numeracja 1.5p*/
            //Circle c1 = new Circle(5.0);
            ///*
            //Spodziewany wydruk:
            //Shape2D(1) created
            //Circle(1) with radius=5 created
            //*/
            //var shapePrint = c1.PrintShape2D();
            //Console.WriteLine(shapePrint);
            //Assert.AreEqual("Circle(r=5)", shapePrint);
            
            //var area = c1.CalculateArea();
            //Console.WriteLine($"Area of circle: {area:N4}");
            //Assert.AreEqual(78.5398, area);

            //// Odkomentuj również implementację metody
            //Finalizers();

            //Shape2D s1 = new Circle(5.0);
            //Console.WriteLine(s1.PrintShape2D());
            //Assert.AreEqual("Circle(r=5)", s1.PrintShape2D());

            ///* Finalizatory 0.5p*/
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
            ///*
            // Spodziewany wydruk:
            // Circle (2) destroyed
            // Shape2D (2) destroyed
            //*/

            //PrintStage(2);

            ///*Shape3D, Cylinder i Cone 2p*/
            ///*Cylinder*/
            //Circle circle3 = new Circle(10);
            //Cylinder cylinder3 = new Cylinder(circle3, 10);
            
            //var capacity3 = cylinder3.CalculateCapacity();
            //Console.WriteLine($"Cylinder capacity: {capacity3:N4}");
            //Assert.AreEqual(3141.5926, capacity3);
            
            //var ps3 = cylinder3.PrintShape3D();
            //Console.WriteLine(ps3);
            //Assert.AreEqual("Cylinder(h=10) with base: Circle(r=10)", ps3);

            ///*Cone*/
            //Circle circle4 = new Circle(5);
            //Cone cone4 = new Cone(circle4, 10);

            //var capacity4 = cone4.CalculateCapacity();
            //Assert.AreEqual(261.7994, capacity4);
            //Console.WriteLine($"Cone capacity: {capacity4:N4}");

            //var ps4 = cone4.PrintShape3D();
            //Console.WriteLine(ps4);
            //Assert.AreEqual("Cone(h=10) with base: Circle(r=5)", ps4);

            ///* Oddzielne numerowanie  0.5p*/
            //var numberOfShapes3D = Shape3D.NumberOfCreatedObjects;
            //var numberOfCones = Cone.NumberOfCreatedObjects;
            //var numberOfCylinders = Cylinder.NumberOfCreatedObjects;
            //Console.WriteLine($"NumberOfShapes3D: {numberOfShapes3D}");
            //Console.WriteLine($"NumberOfCones: {numberOfCones}");
            //Console.WriteLine($"NumberOfCylinders: {numberOfCylinders}");
            //Assert.AreEqual(2, numberOfShapes3D);
            //Assert.AreEqual(1, numberOfCones);
            //Assert.AreEqual(1, numberOfCylinders);

            ///* PrintShape3D  0.5p*/
            //string[] expectedPrints =
            //{
            //    "Shape3D with base Circle(r=10) and height: 10",
            //    "Cone(h=10) with base: Circle(r=5)"
            //};
            //Shape3D[] shapes = { cylinder3, cone4 };
            //for (int i = 0; i < shapes.Length; i++)
            //{
            //    var ps5 = shapes[i].PrintShape3D();
            //    var expectedPs = expectedPrints[i];
            //    Console.WriteLine(ps5);
            //    Assert.AreEqual(expectedPs, ps5);
            //}
        }

        //public static void Finalizers()
        //{
        //    Circle circle = new Circle(5);
        //    Console.WriteLine("End of Finalizers method");
        //}

        private static void PrintStage(int stage)
        {
            Console.WriteLine($"\n----------------------Stage {stage}-------------------------\n");
        }
    }

    /// <summary>
    /// Helper class to verify results
    /// </summary>
    static class Assert
    {
        public static void AreEqual(string expected, string actual) =>
            ThrowExpectedNotActualWithCondition(!string.Equals(expected, actual, StringComparison.OrdinalIgnoreCase), expected, actual);

        public static void AreEqual(double expected, double actual) => 
            ThrowExpectedNotActualWithCondition(Math.Abs(expected - actual) > 0.0001, expected.ToString("N4"), actual.ToString("N4"));

        private static void ThrowExpectedNotActualWithCondition(bool condition, string expected, string actual, string comparisonWord = "is different than")
        {
            if (condition)
            {
                ThrowExpectedNotEqualToActualException(expected, actual, comparisonWord);
            }
        }

        private static void ThrowExpectedNotEqualToActualException(string expected, string actual, string comparisonWord)
        {
            throw new ArgumentException($"Actual value: {actual} {comparisonWord} expected: {expected}");
        }
    }

}
