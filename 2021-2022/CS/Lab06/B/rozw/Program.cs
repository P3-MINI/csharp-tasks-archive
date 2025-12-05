using System;

namespace Lab5_EngB
{
    class Program
    {
        static void Main(string[] args)
        {
            void localFunction_Test(bool currentResult, bool expectedResult)
            {
                if (currentResult == expectedResult)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("[OK]");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[ERROR]");
                }

                Console.ResetColor();
            };

            Console.WriteLine("--------------------- Stage_1 (1.5 Pts) ---------------------");

            {
                Person person1 = new Person("Jan", "Kowalski");
                Person person2 = new Person("Zbiwniew", "Nowak", 78);
                Person person3 = new Person("Anna", "Kowalczyk", 90);

                Console.WriteLine(person1);
                Console.WriteLine(person2);
                Console.WriteLine(person3);

                Person[] people = new Person[] { person1, person2, person3 };

                foreach (Person person in people)
                {
                    (string name, _) = person;

                    Console.WriteLine($"First Name: {name}");
                }
            }

            Console.WriteLine("--------------------- Stage_2 (1.0 Pts) ---------------------");

            {
                Plane plane1 = new Plane(1200, 400, 125, 45, new int[] { 2, 120 }, new Person("Tim", "Keten", 67), new Person() { name = "Broklyn", surname = "Fushs", weight = 80 });

                plane1.PrintInfo(false);

                (int torque, int horsepower, _) = plane1;

                Console.WriteLine(); Console.WriteLine($"Plane with torque: {torque} and horsepower: {horsepower}");
            }

            Console.WriteLine("--------------------- Stage_3 (2.0 Pts) ---------------------");

            {
                Plane plane = new Plane(17500, 4000, 1250, 550, new int[] { 2, 14 }, new Person("Mathew", "Json", 71), new Person() { name = "Yan", surname = "Chikaw", weight = 76 });

                Console.WriteLine($"Range = {plane.CalculateRange()}");
                localFunction_Test(plane.AddBusinessPassenger(new Person() { name = "Efmen", surname = "Ohara", weight = 91 }), true); ;
                Console.WriteLine($"Range = {plane.CalculateRange()}");
                localFunction_Test(plane.AddEconomyPassenger(new Person() { name = "Jarzy", surname = "Neuman", weight = 91 }), true); ;
                Console.WriteLine($"Range = {plane.CalculateRange()}");
                localFunction_Test(plane.AddBusinessPassenger(new Person() { name = "Jonas", surname = "Turwurden", weight = 91 }), true); ;
                Console.WriteLine($"Range = {plane.CalculateRange()}");
                localFunction_Test(plane.AddBusinessPassenger(new Person() { name = "Joanna", surname = "Eweren", weight = 91 }), false);
                Console.WriteLine($"Range = {plane.CalculateRange()}");

                plane.PrintInfo();

                void localFunction_Travel(double flightTime)
                {
                    (bool isFinalDestination, double remainingDistance) = plane.Travel(flightTime);
                    Console.WriteLine($"Is final destination: {isFinalDestination}. Remaining Time: {remainingDistance}");
                }

                Console.WriteLine();
                localFunction_Travel(1.5);
                localFunction_Travel(2.5);
                localFunction_Travel(1.85);
            }

            Console.WriteLine("--------------------- Stage_4 (0.5 Pts) ---------------------");

            {
                Plane plane = new Plane(170000, 900, 300, 350, new int[] { 50, 50 }, new Person("Przemyslaw", "Nowalijka", 90));

                (Person, Plane.Class)[] passengers = new (Person, Plane.Class)[]
                    {
                        (new Person("Eliasz", "Janas", 87), Plane.Class.Business),
                        (new Person("Amber", "Pink", 87), Plane.Class.Business),
                        (new Person("Jonasz", "Umber", 87), Plane.Class.Business),
                        (new Person("Bartosz", "Koziol", 87), Plane.Class.Business),
                        (new Person("Adrian", "Mroz", 87), Plane.Class.Business),
                        (new Person("Jakub", "Skrzypek", 87), Plane.Class.Business),
                        (new Person("Onas", "Epfint", 87), Plane.Class.Business),
                    };

                localFunction_Test(plane.AddPassengers(passengers), true);
                localFunction_Test(plane.AddPassengers(passengers), true);
                localFunction_Test(plane.AddPassengers(passengers), true);
                localFunction_Test(plane.AddPassengers(passengers), true);
                localFunction_Test(plane.AddPassengers(passengers), true);
                localFunction_Test(plane.AddPassengers(passengers), true);
                localFunction_Test(plane.AddPassengers(passengers), true);
                localFunction_Test(plane.AddPassengers(passengers), false);
                localFunction_Test(plane.AddPassengers(passengers), false);
                localFunction_Test(plane.AddPassengers(passengers), false);

                plane.PrintInfo(printPassengers: false);
            }
        }
    }
}
