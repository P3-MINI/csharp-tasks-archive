#define STAGE_1
#define STAGE_2
#define STAGE_3
using System;

namespace Lab04
{
    class Program
    {
        static void Main(string[] args)
        {
#if STAGE_1
            PrintStage(1);

            Circle c1 = new Circle(5.0);

            var shapePrint = c1.PrintShape2D();
            Console.WriteLine(shapePrint);
            Assert.AreEqual("Circle(r=5)", shapePrint);

            var area = c1.CalculateArea();
            Console.WriteLine($"Area of circle: {area:N4}");
            Assert.AreEqual(78.5398, area);

            Shape2D s1 = new Circle(5.0);
            Console.WriteLine(s1.PrintShape2D());
            Assert.AreEqual("Circle(r=5)", s1.PrintShape2D());
#endif
#if STAGE_2
            PrintStage(2);

            Circle circle3 = new Circle(10);
            Cylinder cylinder3 = new Cylinder(circle3, 10);

            var volume3 = cylinder3.CalculateVolume();
            Console.WriteLine($"Cylinder volume: {volume3:N4}");
            Assert.AreEqual(3141.5926, volume3);

            var ps3 = cylinder3.PrintShape3D();
            Console.WriteLine(ps3);
            Assert.AreEqual("Cylinder(h=10)(Id: 0) with base: Circle(r=10) and height: 10", ps3);

            Circle circle4 = new Circle(5);
            Cone cone4 = new Cone(circle4, 10);

            var volume4 = cone4.CalculateVolume();
            Assert.AreEqual(261.7994, volume4);
            Console.WriteLine($"Cone volume: {volume4:N4}");

            var ps4 = cone4.PrintShape3D();
            Console.WriteLine(ps4);
            Assert.AreEqual("Cone(h=10)(Id: 100) with base: Circle(r=5)", ps4);

            var numberOfShapes3D = Shape3D.GetNumberOfCreatedObjects();
            Console.WriteLine($"NumberOfShapes3D: {numberOfShapes3D}");
            Assert.AreEqual(2, numberOfShapes3D);

            string[] expectedPrints =
            {
                "Cylinder(h=10)(Id: 0) with base: Circle(r=10) and height: 10",
                "Cone(h=10)(Id: 100) with base: Circle(r=5)"
            };
            Shape3D[] shapes = {cylinder3, cone4};
            for (int i = 0; i < shapes.Length; i++)
            {
                var ps5 = shapes[i].PrintShape3D();
                var expectedPs = expectedPrints[i];
                Console.WriteLine(ps5);
                Assert.AreEqual(expectedPs, ps5);
            }
#endif
#if STAGE_3
            PrintStage(3);
            var it = 0;
            foreach (var shape in shapes)
            {
                Console.WriteLine($"{it})");
                var copy = shape.MakeCopy();
                Console.WriteLine($"Id of original object: {shape.ObjId}. Id of a copy {copy.ObjId}");
                Console.WriteLine($"Type of original object {shape.GetType()}. Type of copy {copy.GetType()}");
                Assert.AreEqual(shape.ObjId+shapes.Length, copy.ObjId);
                Assert.AreEqual(shape.GetType().ToString(), copy.GetType().ToString());
                Console.WriteLine(shape.PrintShape3D());
                Console.WriteLine(copy.PrintShape3D());
                Console.WriteLine();
                it++;
            }
#endif
            Console.ReadKey();
        }

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
            ThrowExpectedNotActualWithCondition(!string.Equals(expected, actual, StringComparison.OrdinalIgnoreCase),
                expected, actual);

        public static void AreEqual(double expected, double actual) =>
            ThrowExpectedNotActualWithCondition(Math.Abs(expected - actual) > 0.0001, expected.ToString("N4"),
                actual.ToString("N4"));

        private static void ThrowExpectedNotActualWithCondition(bool condition, string expected, string actual,
            string comparisonWord = "is different than")
        {
            if (condition)
            {
                ThrowExpectedNotEqualToActualException(expected, actual, comparisonWord);
            }
        }

        private static void ThrowExpectedNotEqualToActualException(string expected, string actual,
            string comparisonWord)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Actual value: {actual} {comparisonWord} expected: {expected}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
