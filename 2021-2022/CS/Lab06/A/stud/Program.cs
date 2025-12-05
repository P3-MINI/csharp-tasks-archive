using System;

namespace Lab5_EngA
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

            // {
            //     Person person1 = new Person("Jan", "Kowalski");
            //     Person person2 = new Person("Zbiwniew", "Nowak", 78);
            //     Person person3 = new Person("Anna", "Kowalczyk", 90);

            //     Console.WriteLine(person1);
            //     Console.WriteLine(person2);
            //     Console.WriteLine(person3);

            //     Person[] people = new Person[] { person1, person2, person3 };

            //     foreach (Person person in people)
            //     {
            //         (string name, _) = person;

            //         Console.WriteLine($"First Name: {name}");
            //     }
            // }

            Console.WriteLine("--------------------- Stage_2 (1.0 Pts) ---------------------");

            // {
            //     Car car1 = new Car(1200, 400, 125, 45, 5, new Person("Mathew", "Json", 67), new Person() { name = "Przemyslaw", surname = "Nowalijka", weight = 80 });

            //     car1.PrintInfo(false);

            //     (int torque, int horsepower, _) = car1;

            //     Console.WriteLine(); Console.WriteLine($"Car with torque: {torque} and horsepower: {horsepower}");
            // }

            Console.WriteLine("--------------------- Stage_3 (1.5 Pts) ---------------------");

            // {
            //     Car car = new Car(1200, 400, 125, 45, 5, new Person("Tim", "Mroz", 71), new Person() { name = "Yan", surname = "Umber", weight = 76 });

            //     Console.WriteLine($"Range = {car.CalculateRange()}");
            //     localFunction_Test(car.AddPassenger(new Person() { name = "Broklyn", surname = "Chikaw", weight = 91 }), true);
            //     Console.WriteLine($"Range = {car.CalculateRange()}");
            //     localFunction_Test(car.AddPassenger(new Person() { name = "Tim", surname = "Skrzypek", weight = 91 }), true);
            //     Console.WriteLine($"Range = {car.CalculateRange()}");
            //     localFunction_Test(car.AddPassenger(new Person() { name = "Jakub", surname = "Epfint", weight = 91 }), true);
            //     Console.WriteLine($"Range = {car.CalculateRange()}");
            //     localFunction_Test(car.AddPassenger(new Person() { name = "Adrian", surname = "Umber", weight = 91 }), false);
            //     Console.WriteLine($"Range = {car.CalculateRange()}");

            //     car.PrintInfo();

            //     void localFunction_Travel(double distance)
            //     {
            //         (bool isFinalDestination, double remainingDistance) = car.Travel(distance);
            //         Console.WriteLine($"Is final destination: {isFinalDestination}. Remaining Distance: {remainingDistance}");
            //     }

            //     localFunction_Travel(60);
            //     localFunction_Travel(180);
            //     localFunction_Travel(87);
            // }

            Console.WriteLine("--------------------- Stage_4 (1.0 Pts) ---------------------");

            // {
            //     Bus bus = new Bus(170000, 900, 300, 350, 19, new Person("Tim", "Keten", 90));

            //     localFunction_Test(bus.AddPassengers(new Person("Broklyn", "Koziol"), new Person("Yan", "Chikaw", 45), new Person("Mathew", "Json", 86)), true);
            //     localFunction_Test(bus.AddPassengers(new Person("Eliasz", "Mroz"), new Person("Jakub", "Epfint", 45), new Person("Onas", "Pink", 86)), true);
            //     localFunction_Test(bus.AddPassengers(new Person("Amber", "Janas"), new Person("Bartosz", "Skrzypek", 45), new Person("Jakub", "Umber", 86)), true);
            //     localFunction_Test(bus.AddPassengers(new Person("Adrian", "Janas"), new Person("Bartosz", "Skrzypek", 45), new Person("Jakub", "Umber", 86)), true);
            //     localFunction_Test(bus.AddPassengers(new Person("Amber", "Janas"), new Person("Bartosz", "Skrzypek", 45), new Person("Jakub", "Umber", 86)), true);
            //     localFunction_Test(bus.AddPassengers(new Person("Eliasz", "Mroz"), new Person("Jakub", "Epfint", 45), new Person("Onas", "Pink", 86)), true);
            //     localFunction_Test(bus.AddPassengers(new Person("Broklyn", "Koziol"), new Person("Yan", "Chikaw", 45), new Person("Mathew", "Json", 86)), false);

            //     bus.PrintInfo(printPassengers: false);
            // }
        }
    }
}
