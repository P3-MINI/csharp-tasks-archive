using System;

namespace Lab04_eng
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintLine();

            
            Circle c1 = new Circle(5);
            //Constructor D2Shape (1) called
            //Constructor Circle (1) called

            Console.WriteLine(c1.PrintD2Shape());     // Circle(r=10)

            Console.WriteLine("\nProtected type or member can be accessed only by code in the same class, or in a class that is derived from that class");
            //Console.WriteLine(c1._objectNumber);                        // Compilation error -> 'D2Shape._objectNumber' is inaccessible due to its protection level

            //2. Polymorphism
            // method require abstract of virtual keyword to be overriden

            D2Shape d2 = new Circle(5);
            // This works because we used override keyword. Method is called on object instance type
            Console.WriteLine(d2.PrintD2Shape());                          // Circle(r=10) not Shape(D2Shape)

            PrintLine();

            // TODO: Uncomment code below and implement Rectangle which inherit from D2Shape to satisfy output
            //Rectangle r3 = new Rectangle(5, 10);
            //// Expected output:
            //// Constructor D2Shape (3) called
            //// Constructor Rectangle (1) called
            //Console.WriteLine("Result should be the same:");
            //Console.WriteLine(r3.PrintD2Shape());                           //Rectangle(a=5,b=10)
            //Console.WriteLine(r3.CalculateArea());                           // 50
            //D2Shape d3 = r3;
            //Console.WriteLine(d3.PrintD2Shape());                           //Rectangle(a=5,b=10)

            PrintLine();
            
            // Hide method
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

            PrintLine();

            Console.WriteLine("\nStatic member is callable on a class");
            Console.WriteLine("Only one copy of a static member exists");
            Console.WriteLine($"Value from static field: {D2Shape.NumberOfCreatedObjects}");

            Console.WriteLine("\nStatic methods and properties cannot access non-static fields and events in their containing type");
            
            // static method
            Circle c5 = new Circle(5);
            D2Shape scaledC5 = D2Shape.ScaleD2Shape(c5, 2);
            Console.WriteLine(scaledC5.PrintD2Shape());

            // static field with new keyword
            Cone cone1 = new Cone(c5, 5);
            D3Shape cylinder1 = new Cylinder(c5, 5);
            // In derived classes we can hide `new` also static fields
            Console.WriteLine($"Number of created D3Shapes object={D3Shape.NumberOfCreatedObjects}");
            Console.WriteLine($"Number of created Cylinders object={Cylinder.NumberOfCreatedObjects}");

            PrintLine();

            // Finalizers
            Finalizers();
            // Force to call GC to show Finalizers
            // GC.Collect();
            // GC.WaitForPendingFinalizers();
            // Recommendation to use IDisposable pattern 

            PrintLine();

            // TODO: Uncomment below lines
            // Go to D3Shapes file and add Cuboid inheriting from D2Shape
            //Rectangle r6 = new Rectangle(5, 10);
            //Cuboid cuboid6 = new Cuboid(r6, 10);
            //Console.WriteLine(cuboid6.PrintD3Shape());                  // Cuboid(5, 10, 10)
            //Console.WriteLine(cuboid6.CalculateCapacity());             // 500
            //Console.WriteLine(cuboid6.CalculateArea());                 // 400
            //D3Shape d6 = cuboid6;                                       // 
            //Console.WriteLine(d6.PrintD3Shape());                       // Rectangle(a=5,b=10) with height 10
            //r6.Scale(2);
            //Console.WriteLine(cuboid6.PrintD3Shape());                  // Cuboid(10, 20, 10)
            //Console.WriteLine(cuboid6.CalculateArea());                 // 1000

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
