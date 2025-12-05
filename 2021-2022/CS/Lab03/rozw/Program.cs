using System;

namespace Lab03_eng
{
    class TeacherProgram
    {
        static void Main(string[] args)
        {
            // Teacher's TODO: Do an intro to classes
            //0. Ask students why do we need inheritance?
            // DRY - don't repeat yourself, share some logic, fix in one place
            // Want to generalize

            //1. Intro to classes and implementation
            // Show all classes and inheritance
            // In D2Shape why we can override ToString() method - it is base class?
            // By default every class inherit from Object

            // Presenting abstract class 
            // private protected and internal modifiers on the object
            // showing also static field and static method
            // Here we are creating objects and showing that they are calling their implementation
            
            PrintBreak();

            Rectangle r1 = new Rectangle(10, 5);
            // Teacher's TODO: show what is an order of constructor calls
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

            // Teacher's TODO: Show static field
            Console.WriteLine("\nStatic member is callable on a class");
            Console.WriteLine("Only one copy of a static member exists");
            Console.WriteLine($"Value from static field: {D2Shape.NumberOfCreatedObjects}");

            Console.WriteLine("\nStatic methods and properties cannot access non-static fields and events in their containing type");
            // Teacher's TODO: show static method
            D2Shape scaledR1 = D2Shape.ScaleD2Shape(r1, 2);
            Console.WriteLine(scaledR1.PrintShape());
            
            PrintBreak();

            //2. Polymorphism
            // Teacher's TODO: do an intro to Polymorphism
            // Creating objects but declare them as base class
            // object method is called not the base one
            D2Shape r2 = new Rectangle(10, 5);
            D2Shape rt2 = new RectangularTriangle(10, 5);
            // This works because we used override keyword. Method is called on object instance type
            Console.WriteLine(r2.PrintShape());                          // Area of Rectangle (10,5) is = 50
            Console.WriteLine(rt2.PrintShape());                         // Area of RectangularTriangle (10,5) is = 25

            PrintBreak();

            //3. Finalizers
            // Teacher's TODO: do an intro to Finalizers
            // Run when object is removed by GC
            Finalizers();
            // Force to call GC to show Finalizers
            GC.Collect();
            GC.WaitForPendingFinalizers();

            PrintBreak();

            //4. Students work - Circle
            // Students TODO: Implement Circle inheriting fom D2Shape
            // Go to Classes file and add Circle inheriting from D2Shape
            Circle c1 = new Circle(5);
            Console.WriteLine(c1.PrintShape());                         // Circle area (5) is =78,53981633974483
            Console.WriteLine(c1.CalculateCircuit());                   // 31,41592653589793

            D2Shape c2 = D2Shape.ScaleD2Shape(c1, 5);
            Console.WriteLine(c2.PrintShape());                         // Circle area (25) is =1963,4954084936207
            //Console.WriteLine(c2.CalculateCircuit());                   // Not accessible for c2 because it is type D2Shape

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
