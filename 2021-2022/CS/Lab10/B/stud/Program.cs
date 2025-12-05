using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace LAB_2021_CS_10B
{
    public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
            
        public int Age { get; set; }

        public override string ToString()
        {
            return $"{Name} {Surname}, Age: {Age}";
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            //Stage 1 - 1 point
            {
                Console.WriteLine("STAGE 1");
                List<Person> persons = new List<Person>()
                {
                    new Person(){ Name = "XYZ", Surname = "ABCDEFG", Age = 25},
                    new Person(){ Name = "Havelock", Surname = "Vetinari", Age = 64},
                    new Person(){ Name = "Abc", Surname = "Cfg", Age = 3},
                    new Person(){ Name = "Elvis", Surname = "Presley", Age = 3004},
                    new Person(){ Name = "Frank", Surname = "Herbert", Age = 10},
                    new Person(){ Name = "Vlad", Surname = "Dracula", Age = 5900},
                    new Person(){ Name = "Default", Surname = "Default", Age = 17},
                    new Person(){ Name = "Marco", Surname = "Polo", Age = 38},
                    new Person(){ Name = "Sam", Surname = "Vimes", Age = 45},
                    new Person(){ Name = "Frodo", Surname = "Baggins", Age = 34},
                };
                Console.WriteLine("List:");
                PrintList(persons);
                //Use lambda function here
                persons.Sort(...);
                Console.WriteLine("Sorted list:");
                PrintList(persons);
                
                int accumulatedAge = 0;
                persons.ForEach(...);
                Console.WriteLine("Accumulated age: " accumulatedAge);
                Console.WriteLine("\n\n");
            }

            //Stage 2 - 1 point
            {
                Console.WriteLine("STAGE 2");
                Console.WriteLine("Constant function");
                var funC = BaseFunctions.ConstantFunction(5);
                for (int i = 13; i < 27; i++)
                {
                    Console.WriteLine((i, funC(i)));
                }
                Console.WriteLine("\n\n");
            
                Console.WriteLine("Quadratic function");
                var funQ = BaseFunctions.ModulusFunction(2, -6);
                for (int i = 0; i <= 10; i++)
                {
                    Console.WriteLine((i, funQ(i)));
                }
                Console.WriteLine("\n\n");
            
                Console.WriteLine("Polynomial function");
                var funP = BaseFunctions.PolynomialFunction(10, 6, 8, -3, -14, 1);
                for (int i = 0; i <= 10; i++)
                {
                    Console.WriteLine((i, funP(i)));
                }
                Console.WriteLine("\n\n");
            }
            
            //Stage 3 - 1 point
            {
                Console.WriteLine("STAGE 3");
                Console.WriteLine("Choose function");
                var f = BaseFunctions.ConstantFunction(-3);
                var g = BaseFunctions.ModulusFunction(1, -2);
                var funM = FunctionsManipulator.ChooseFunction(f, g);
                for (double i = -5; i <= 0; i+=0.5)
                {
                    Console.WriteLine((i, funM(i)));
                }
                Console.WriteLine("\n\n");
            
                Console.WriteLine("Multiply functions");
                f = BaseFunctions.ConstantFunction(10);
                g = BaseFunctions.ModulusFunction(2, -4);
                var funD = FunctionsManipulator.MultiplyFunctions(f, g);
                for (int i = -5; i <= 5; i++)
                {
                    Console.WriteLine((i, funD(i)));
                }
                Console.WriteLine("\n\n");
                
                Console.WriteLine("Combine functions");
                f = BaseFunctions.ModulusFunction(1, 0);
                g = BaseFunctions.ModulusFunction(0, -4);
                var funC = FunctionsManipulator.CombineFunctions(f, g);
                for (int i = -5; i <= 5; i++)
                {
                    Console.WriteLine((i, funC(i)));
                }
                Console.WriteLine("\n\n");
            }
            
            //Stage 4 - 1.5 points
            {
                List<Person> persons = new List<Person>()
                {
                    new Person(){ Name = "XYZ", Surname = "ABCDEFG", Age = 25},
                    new Person(){ Name = "Havelock", Surname = "Vetinari", Age = 64},
                    new Person(){ Name = "Abc", Surname = "Cfg", Age = 3},
                    new Person(){ Name = "Elvis", Surname = "Presley", Age = 3004},
                    new Person(){ Name = "Frank", Surname = "Herbert", Age = 10},
                    new Person(){ Name = "Vlad", Surname = "Dracula", Age = 5900},
                    new Person(){ Name = "Default", Surname = "Default", Age = 17},
                    new Person(){ Name = "Marco", Surname = "Polo", Age = 38},
                    new Person(){ Name = "Sam", Surname = "Vimes", Age = 45},
                    new Person(){ Name = "Frodo", Surname = "Baggins", Age = 34},
                };
                
                Console.WriteLine("STAGE 4");
                Random random = new Random(0);
                Console.WriteLine("Invoke action extension");
                PrintList(persons);
                
                //Action to fill
                persons.PerformAction(...);
                Console.WriteLine("\n\n");
                
                Console.WriteLine("Accumulate extension");
                PrintList(persons);
            
                //Transform function to fill
                var transformedDoubles = persons.Accumulate(...);
                Console.WriteLine(transformedDoubles);
            }
        }

        private static void PrintList<T>(List<T> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();
        }
    }
}