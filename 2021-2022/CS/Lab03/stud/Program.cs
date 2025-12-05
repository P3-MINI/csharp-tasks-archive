using System;

namespace Lab03_eng
{
    class StudProgram
    {
        static void Main(string[] args)
        {
            PrintBreak();

            Rectangle r1 = new Rectangle(10, 5);
            //Constructor D2Shape (1) called
            //Constructor Rectangle (1) called
            //Call Rectangle constructor with two parameters: (10, 5)

            RectangularTriangle rt1 = new RectangularTriangle(10, 5);
            //Constructor D2Shape (2) called
            //Constructor RectangularTriangle (2) called

            Console.WriteLine(r1.PrintShape());     // Area of Rectangle (10,5) is = 50
            Console.WriteLine(rt1.PrintShape());    // Area of RectangularTriangle (10,5) is = 25

            Console.WriteLine("\nProtected type or member can be accessed only by code in the same class, or in a class that is derived from that class");
            //Console.WriteLine(r1._objectNumber);                        // Compilation error -> 'D2Shape._objectNumber' is inaccessible due to its protection level
            Console.WriteLine(r1.ExampleOfPublicField);
            Console.WriteLine(r1.ExampleOfInternalField);

            Console.WriteLine("\nStatic member is callable on a class");
            Console.WriteLine("Only one copy of a static member exists");
            Console.WriteLine($"Value from static field: {D2Shape.NumberOfCreatedObjects}");

            Console.WriteLine("\nStatic methods and properties cannot access non-static fields and events in their containing type");
            D2Shape scaledR1 = D2Shape.ScaleD2Shape(r1, 2);
            Console.WriteLine(scaledR1.PrintShape());
            
            PrintBreak();

            //2. Polymorphism
            // Creating objects but declare them as base class
            // object method is called not the base one
            D2Shape r2 = new Rectangle(10, 5);
            D2Shape rt2 = new RectangularTriangle(10, 5);
            // This works because we used override keyword. Method is called on object instance type
            Console.WriteLine(r2.PrintShape());                          // Area of Rectangle (10,5) is = 50
            Console.WriteLine(rt2.PrintShape());                         // Area of RectangularTriangle (10,5) is = 25

            PrintBreak();

            //3. Finalizers
            // Run when object is removed by GC
            Finalizers();
            // Force to call GC to show Finalizers
            GC.Collect();
            GC.WaitForPendingFinalizers();

            PrintBreak();
            
            //4. Students work - Circle
            // Go to Classes file and add Circle inheriting from D2Shape
            // Uncomment code below
            //Circle c1 = new Circle(5);
            //Console.WriteLine(c1.PrintShape());                         // Circle area (5) is =78,53981633974483
            //Console.WriteLine(c1.CalculateCircuit());                   // 31,41592653589793

            //D2Shape c2 = D2Shape.ScaleD2Shape(c1, 5);
            //Console.WriteLine(c2.PrintShape());                         // Circle area (25) is =1963,4954084936207
            //Console.WriteLine(c2.CalculateCircuit());                   // Not accessible for c2 -> why?

            Console.WriteLine("End of Main method");
            PrintBreak();
        }

        public static void Finalizers()
        {
            D2Shape r3 = new Rectangle(10, 5);
            Console.WriteLine(r3.PrintShape());                          // Area of Rectangle (10,5) is = 50
            Console.WriteLine("End of Finalizers method");
            PrintBreak();
        }

        // Example of static method, no need to have instance to call it
        private static void PrintBreak()
        {
            Console.WriteLine("___________________________________________________________");
        }
    }
}
