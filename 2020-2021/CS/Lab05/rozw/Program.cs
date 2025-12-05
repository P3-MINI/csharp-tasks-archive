using System;

namespace Vehicles
{
    class Program
    {
        static void Main(string[] args)
        {
            // Soulution can not contains any compilation errors or warnings!

            // STAGE 1: 3.0 points
            Console.WriteLine("\nSTAGE 1");

            Console.WriteLine("\nBasic test");

            Car car1 = new Car("Car1");
            car1.Travel(13.5);

            Bus bus1 = new Bus("Bus1");

            uint passengers1 = 100;
            if (bus1.SetPassengerCount(passengers1))
                Console.WriteLine($"[Error] Bus can not carry more then {Bus.passengerLimit} passengers.");
            else
                Console.WriteLine($"[OK] Bus can not carry more then {Bus.passengerLimit} passengers.");

            uint passengers2 = 23;
            if (bus1.SetPassengerCount(passengers2))
                Console.WriteLine($"[OK] Bus can carry {bus1.PassengerCount()} passengers.");
            else
                Console.WriteLine($"[Error] Bus should be able to carry {passengers2} passengers.");

            bus1.Travel(120.4);

            Truck truck1 = new Truck("Truck1");
            double load1 = 2682.3;
            if (truck1.SetLoad(load1))
                Console.WriteLine($"[Error] Truck can not transport more then {Truck.capacity} kg.");
            else
                Console.WriteLine($"[OK] Truck can not transport more then {Truck.capacity} kg.");

            double load2 = 1682.3;
            if (truck1.SetLoad(load2))
                Console.WriteLine($"[OK] Truck can transport {truck1.Load()} kg.");
            else
                Console.WriteLine($"[Error] Truck should be able to transport {load2} kg.");

            truck1.Travel(203.8);

            Vehicle[] fleet = { 
                car1,
                new Car("Car2"),
                bus1,
                truck1
            };

            Console.WriteLine("\nFleet:");
            for (int i = 0; i < fleet.Length; i++)
            {
                Console.WriteLine(fleet[i]);
            }

            bus1.SetPassengerCount(32);

            Console.WriteLine("\nFleet travel:");
            for (int i = 0; i < fleet.Length; i++)
            {
                fleet[i].Travel(342.4);
            }

            // STAGE 2: 1.0 points
            Console.WriteLine("\nSTAGE 2");

            Console.WriteLine("\nBeeps: ");
            car1.Beep();
            bus1.Beep();
            truck1.Beep();
            Console.WriteLine("\nVehicle beeps: ");
            Vehicle vehicle = car1;
            vehicle.Beep();
            vehicle = bus1;
            vehicle.Beep();
            vehicle = truck1;
            vehicle.Beep();

            // STAGE 3: 1.0 points
            Console.WriteLine("\nSTAGE 3");

            Console.WriteLine("\nNew fleet order:");
            string[,] orders = {
                { "car", "New car 1" },
                { "truck", "New truck 1" },
                { "bus", "New bus 1" },
                { "motorcycle", "This should not be created" },
                { "bus", "New bus 2" }
            };

            // Implement order creation here
            for (int i = 0; i < orders.GetLength(0); i++)
            {
                Vehicle newVehicle = Factory.Manufacture(orders[i, 0], orders[i, 1]);
                if (newVehicle != null)
                {
                    Console.WriteLine($"New {newVehicle} has beed added to fleet.");
                }
                else 
                {
                    Console.WriteLine($"Factory could not manufactured a {orders[i, 0]}.");
                }
            }
        }
    }
}
