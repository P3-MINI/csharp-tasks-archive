using System;

namespace Lab05_pl
{
    class Program
    {
        static void Main(string[] args)
        {
            // Compilation. e.g.Warning CS0108 shouldn't give you any warnings.
            // To ensure that 
            // TODO: Go to Project properties -> Build -> Treat warnings as errors -> and set to All
            
            PrintStage(1, 1.0);
            // 1pkt

            // In file Shapes2D there is abstract class Shape2D
            // Implement class `Circle` that inherit from Shape2D
            // All fields in Circle need to be private
            // Uncomment code below

            Circle c1 = new Circle(5.0);
            // Expected print out:
            // Constructor Shape2D (1)
            // Constructor Circle (1)
            var shapePrint = c1.PrintShape2D();
            Console.WriteLine(shapePrint);
            Assert.AreEqual("Circle(r=5)", shapePrint);
            var area = c1.CalculateArea();
            Console.WriteLine($"Area of circle: {area:N4}");
            Assert.AreEqual(78.5398, area);
            
            c1.Scale(2);
            var scaledArea = c1.CalculateArea();
            Console.WriteLine($"Area of scaled circle: {scaledArea:N4}");
            Assert.AreEqual(314.1593, scaledArea);
           
            Shape2D s1 = new Circle(5.0);
            Console.WriteLine(s1.PrintShape2D());
            Assert.AreEqual("Circle(r=5)", s1.PrintShape2D());

            PrintStage(2, 0.5);
            //0.5pkt

            //TODO: Implement Shape2D.ScaleShape2D method
            // Uncomment code below

            Circle c2 = new Circle(10.0);
            Console.WriteLine(c2.PrintShape2D());
            Shape2D s2 = Shape2D.ScaleShape2D(c2, 2);
            Console.WriteLine(s2.PrintShape2D());
            Assert.AreNotEqual(c2, s2);

            PrintStage(3, 1.0);
            //1.0pkt

            //TODO: Go to Shape3D class and finish
            // implementation of base class for `Cylinder` -> Shape3D
            // Next finish implementation of `Cylinder
            // Uncomment code below and verify if it is correct

            Circle circle3 = new Circle(10);
            Cylinder cylinder3 = new Cylinder(circle3, 10);
            var capacity3 = cylinder3.CalculateCapacity();
            Console.WriteLine($"Cylinder capacity: {capacity3:N4}");
            Assert.AreEqual(3141.5926, capacity3);

            var area3 = cylinder3.CalculateArea();
            Console.WriteLine($"Cylinder area: {area3:N4}");
            Assert.AreEqual(1256.6371, area3);

            var ps3 = cylinder3.PrintShape3D();
            Console.WriteLine(ps3);
            Assert.AreEqual("Cylinder(h=10) and base: Circle(r=10)", ps3);

            PrintStage(4, 1.0);
            //1pkt

            //TODO: Go to Shape3D file and implement `Cone` class
            // `Cone` need to inherit from Shape3D
            // Capacity = (Pi*r^2*h)/3
            // Area=Pi*r^2+Pi*r*sqrt(r^2+h^2)
            // Hint: to implement CalculateArea
            // you can add additional methods to `Circle`
            Circle circle4 = new Circle(5);
            Cone cone4 = new Cone(circle4, 10);
            
            var capacity4 = cone4.CalculateCapacity();
            Assert.AreEqual(261.7994, capacity4);
            Console.WriteLine($"Cone capacity: {capacity4:N4}");

            var area4 = cone4.CalculateArea();
            Console.WriteLine($"Cone area: {area4:N4}");
            Assert.AreEqual(254.1602, area4);

            var ps4 = cone4.PrintShape3D();
            Console.WriteLine(ps4);
            Assert.AreEqual("Cone(h=10) and base: Circle(r=5)", ps4);

            PrintStage(5, 0.5);
            //0.5pkt

            string[] expectedPrints =
            {
                "Cylinder(h=10) and base: Circle(r=10)",
                "Shape3D with base Circle(r=5) and height: 10"
            };
            Shape3D[] shapes = {cylinder3, cone4};
            for (int i = 0; i < shapes.Length; i++)
            {
                var ps5 = shapes[i].PrintShape3D();
                var expectedPs = expectedPrints[i];
                Console.WriteLine(ps5);
                Assert.AreEqual(expectedPs, ps5);
            }
            PrintStage(6, 0.5);
            //pkt 0.5
            //TODO: This stage verifies that each object has properly 
            // implemented numbering of Object creation
            // Verification of properly provided numbering for objects

            var numberOfShapes3D = Shape3D.NumberOfCreatedObjects;
            var numberOfCones = Cone.NumberOfCreatedObjects;
            var numberOfCylinders = Cylinder.NumberOfCreatedObjects;
            Console.WriteLine($"NumberOfShapes3D: {numberOfShapes3D}");
            Console.WriteLine($"NumberOfCones: {numberOfCones}");
            Console.WriteLine($"NumberOfCylinders: {numberOfCylinders}");
            Assert.AreEqual(2, numberOfShapes3D);
            Assert.AreEqual(1, numberOfCones);
            Assert.AreEqual(1, numberOfCylinders);

            PrintStage(7, 0.5);
            // 0.5pkt
            //TODO: Implement finalizers in `Shape3D`, `Cylinder` and `Cone`
            /*
                Constructor Shape2D (7)
                Constructor Circle (7)
                Constructor Shape3D (3)
                Constructor Cone (2)
                Constructor Shape3D (4)
                Constructor Cylinder (2)
                Cone(h=10) and base: Circle(r=5)
                Cylinder(h=10) and base: Circle(r=5)
                End of Finalizers method
                Finalizer Cylinder (2)
                Finalizer Shape3D (4) called
                Finalizer Cone (2)
                Finalizer Shape3D (3) called
                Finalizer Circle (7)
                Finalizer Shape2D (7)
            */
            //TODO: Uncomment and verify print out with the one above
            Finalizers();
            // Force to call GC to show Finalizers
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public static void Finalizers()
        {
            Circle circle = new Circle(5);
            Cone cone = new Cone(circle, 10);
            Cylinder cylinder = new Cylinder(circle, 10);
            Console.WriteLine(cone.PrintShape3D());
            Console.WriteLine(cylinder.PrintShape3D());
            Console.WriteLine("End of Finalizers method");
        }

        private static void PrintStage(int stage, double points)
        {
            Console.WriteLine($"----------------------Stage {stage} ({points:N1} points)----------------------");
        }
    }

    /// <summary>
    /// Helper class to verify results
    /// </summary>
    static class Assert
    {
        public static void AreNotEqual(Shape2D expected, Shape2D actual) =>
            ThrowExpectedNotActualWithCondition(expected.Equals(actual), expected.PrintShape2D(),
                actual.PrintShape2D());

        public static void AreEqual(Shape2D expected, Shape2D actual) =>
            ThrowExpectedNotActualWithCondition(!expected.Equals(actual), expected.PrintShape2D(),
                actual.PrintShape2D());

        public static void AreEqual(string expected, string actual) =>
            ThrowExpectedNotActualWithCondition(!string.Equals(expected, actual, StringComparison.OrdinalIgnoreCase), expected, actual);

        public static void AreEqual(double expected, double actual) => 
            ThrowExpectedNotActualWithCondition(Math.Abs(expected - actual) > 0.0001, expected.ToString("N4"), actual.ToString("N4"));

        private static void ThrowExpectedNotActualWithCondition(bool condition, string expected, string actual)
        {
            if (condition)
            {
                ThrowExpectedNotEqualToActualException(expected, actual);
            }
        }

        private static void ThrowExpectedNotEqualToActualException(string expected, string actual)
        {
            throw new ArgumentException($"Actual value: {actual} is different than expected: {expected}");
        }
    }

}
