using System;

namespace Lab04_eng
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintLine();

            // Repetition from the last because students from c++ are joining
            // Intro to classes and objects
            // Create inherent classes and showed that inherent method is called

            // Teacher's TODO: Do an intro to classes

            //1. Intro to classes and implementation
            // Fast of simplified D2Shapes abstract and Circle

            // Show all classes and inheritance
            // In D2Shape why we can override ToString() method - it is base class?
            // By default every class inherit from Object

            // Show D3Shape classes - example where we can use Base type
            // Put attention to readonly fields
            // not accessed parameter-less constructor in base class

            // Presenting abstract class
            // private protected and internal modifiers on the object
            // showing also static field and static method
            // Here we are creating objects and showing that they are calling their implementation
            
            Circle c1 = new Circle(5);
            // Teacher's TODO: show what is an order of constructor calls
            //Constructor D2Shape (1) called
            //Constructor Circle (1) called

            Console.WriteLine(c1.PrintD2Shape());     // Circle(r=10)

            Console.WriteLine("\nProtected type or member can be accessed only by code in the same class, or in a class that is derived from that class");
            //Console.WriteLine(c1._objectNumber);                        // Compilation error -> 'D2Shape._objectNumber' is inaccessible due to its protection level

            //2. Polymorphism
            // Teacher's TODO: do an intro to Polymorphism
            // Creating objects but declare them as base class
            // object method is called not the base one
            // method require abstract of virtual keyword to be overriden

            D2Shape d2 = new Circle(5);
            // This works because we used override keyword. Method is called on object instance type
            Console.WriteLine(d2.PrintD2Shape());                          // Circle(r=10) not Shape(D2Shape)

            PrintLine();

            // Students TODO: Ask students to implement first class Rectangle
            // Uncomment code below and implement Rectangle which inherit from D2Shape to satisfy output
            // Show that _objectNumber for D2Shape is 3, while Rectangle _objectNumber is 1
            Rectangle r3 = new Rectangle(5, 10);
            // Constructor D2Shape (3) called
            // Constructor Rectangle (1) called
            // Call Rectangle constructor with two parameters: (5, 10)
            Console.WriteLine("Result should be the same:");
            Console.WriteLine(r3.PrintD2Shape());                           //Rectangle(a=5,b=10)
            Console.WriteLine(r3.CalculateArea());                           // 50
            D2Shape d3 = r3;
            Console.WriteLine(d3.PrintD2Shape());                           //Rectangle(a=5,b=10)

            PrintLine();
            
            // TODO: Hide method
            Circle c4 = new Circle(10);
            Cylinder cylinder4 = new Cylinder(c4, 10);
            Cone cone4 = new Cone(c4, 10);
            Console.WriteLine(cylinder4.PrintD3Shape());                        // Cylinder with height= 10 and base: Circle(r=10)
            Console.WriteLine(cone4.PrintD3Shape());                            // Cone with height= 10 and base: Circle(r=10)

            D3Shape d4Cylinder = cylinder4;
            D3Shape d4Cone = cone4;
            Console.WriteLine(d4Cylinder.PrintD3Shape());                       // Cylinder with height= 10 and base: Circle(r=10)
            // Why below code return result from D2Shape base class?
            Console.WriteLine(d4Cone.PrintD3Shape());                          // Circle(r=10) with height 10
            // TODO: Explain new keyword

            PrintLine();

            //Teacher's TODO: Show static field
            Console.WriteLine("\nStatic member is callable on a class");
            Console.WriteLine("Only one copy of a static member exists");
            Console.WriteLine($"Value from static field: {D2Shape.NumberOfCreatedObjects}");

            Console.WriteLine("\nStatic methods and properties cannot access non-static fields and events in their containing type");
            // Teacher's TODO: show static method
            Circle c5 = new Circle(5);
            D2Shape scaledC5 = D2Shape.ScaleD2Shape(c5, 2);
            Console.WriteLine(scaledC5.PrintD2Shape());

            //Teacher's TODO: Show static override with new keyword
            // Show with breakpoint that both counters are incremented separately
            Cone cone1 = new Cone(c5, 5);
            D3Shape cylinder1 = new Cylinder(c5, 5);
            // In derived classes we can hide `new` also static fields
            Console.WriteLine($"Number of created D3Shapes object={D3Shape.NumberOfCreatedObjects}");
            Console.WriteLine($"Number of created Cylinders object={Cylinder.NumberOfCreatedObjects}");

            PrintLine();

            // Teacher's TODO: Sealed
            // Add to D3Shape class seald keyword and show that there is error - this prevent to inherit other classes from it
            // Add sealed to any from override method and show that it can't be overriden

            PrintLine();

            // Finalizers
            // Teacher's TODO: do an intro to Finalizers
            // Run when object is removed by GC
            Finalizers();
            // Force to call GC to show Finalizers
            // Teacher's TODO: first run without uncommented code and as what has happens, then uncomment
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
            // Explain that it is not commonly use in C# 
            // Recommended is to use IDisposable pattern 

            PrintLine();

            //Students TODO: Cuboid
            // Students TODO: Implement Cuboid inheriting fom D3Shape
            // Go to D3Shapes file and add Cuboid inheriting from D2Shape
            Rectangle r6 = new Rectangle(5, 10);
            Cuboid cuboid6 = new Cuboid(r6, 10);
            Console.WriteLine(cuboid6.PrintD3Shape());                  // Cuboid(5, 10, 10)
            Console.WriteLine(cuboid6.CalculateCapacity());             // 500
            Console.WriteLine(cuboid6.CalculateArea());                 // 400
            D3Shape d6 = cuboid6;                                       // 
            Console.WriteLine(d6.PrintD3Shape());                       // Rectangle(a=5,b=10) with height 10
            r6.Scale(2);
            Console.WriteLine(cuboid6.PrintD3Shape());                  // Cuboid(10, 20, 10)
            Console.WriteLine(cuboid6.CalculateArea());                 // 1000

            Console.WriteLine("End of Main method");
            PrintLine();
        }

        public static void Finalizers()
        {
            Circle c1 = new Circle(5);
            D3Shape cone1 = new Cone(c1, 10);
            Console.WriteLine(cone1.PrintD3Shape());                          
            Console.WriteLine("End of Finalizers method");
            PrintLine();
        }

        // Example of static method, no need to have instance to call it
        private static void PrintLine()
        {
            Console.WriteLine("___________________________________________________________");
        }
    }
}
