using System;

namespace Lab05_pl
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintStage(1);

            ///*Klasa Shape2D i numeracja 1.5p*/
            //Rectangle r1 = new Rectangle(5.0, 10.0);
            ///*
            //Spodziewany wydruk:
            //Shape2D (1) created
            //Rectangle (1) with x=5, y=10 created
            //*/
            //var shapePrint = r1.PrintShape2D();
            //Console.WriteLine(shapePrint);
            //Assert.AreEqual("Rectangle(x=5,y=10)", shapePrint);

            //var area = r1.CalculateArea();
            //Console.WriteLine($"Area of rectangle: {area:N4}");
            //Assert.AreEqual(50, area);

            //// Odkomentuj również implementację metody
            //Finalizers();

            //Shape2D s1 = new Rectangle(5.0, 10.0);
            //Console.WriteLine(s1.PrintShape2D());
            //Assert.AreEqual("Rectangle(x=5,y=10)", s1.PrintShape2D());

            ///* Finalizatory 0.5p*/
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
            ///*
            //Spodziewany wydruk:
            //Rectangle (2) destroyed
            //Shape2D (2) destroyed
            //*/

            PrintStage(2);

            ///*Shape3D, Cuboid i Pyramid 2p*/
            ///*Cuboid*/
            //Rectangle rectangle3 = new Rectangle(5.0, 10.0);
            //Cuboid cuboid3 = new Cuboid(rectangle3, 10);
            //var capacity3 = cuboid3.CalculateCapacity();
            //Console.WriteLine($"Cuboid capacity: {capacity3:N4}");
            //Assert.AreEqual(500.0000, capacity3);

            //var ps3 = cuboid3.PrintShape3D();
            //Console.WriteLine(ps3);
            //Assert.AreEqual("Cuboid(h=10) with base: Rectangle(x=5,y=10)", ps3);

            ///*Pyramid*/

            //Rectangle rectangle4 = new Rectangle(4.0, 8.0);
            //Pyramid pyramid4 = new Pyramid(rectangle4, 5.0);

            //var capacity4 = pyramid4.CalculateCapacity();
            //Assert.AreEqual(53.3333, capacity4);
            //Console.WriteLine($"Pyramid capacity: {capacity4:N4}");

            //var ps4 = pyramid4.PrintShape3D();
            //Console.WriteLine(ps4);
            //Assert.AreEqual("Pyramid(h=5) with base: Rectangle(x=4,y=8)", ps4);

            ///* Oddzielne numerowanie  0.5p*/
            //var numberOfShapes3D = Shape3D.NumberOfCreatedObjects;
            //var numberOfCuboids = Cuboid.NumberOfCreatedObjects;
            //var numberOfPyramids = Pyramid.NumberOfCreatedObjects;
            //Console.WriteLine($"NumberOfShapes3D: {numberOfShapes3D}");
            //Console.WriteLine($"NumberOfCuboids: {numberOfCuboids}");
            //Console.WriteLine($"NumberOfPyramids: {numberOfPyramids}");
            //Assert.AreEqual(2, numberOfShapes3D);
            //Assert.AreEqual(1, numberOfCuboids);
            //Assert.AreEqual(1, numberOfPyramids);

            ///* PrintShape3D  0.5p*/
            //string[] expectedPrints =
            //{
            //    "Cuboid(h=10) with base: Rectangle(x=5,y=10)",
            //    "Shape3D with base Rectangle(x=4,y=8) and height: 5"
            //};
            //Shape3D[] shapes = { cuboid3, pyramid4 };
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
        //    Rectangle rectangle = new Rectangle(5.0, 10.0);
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
