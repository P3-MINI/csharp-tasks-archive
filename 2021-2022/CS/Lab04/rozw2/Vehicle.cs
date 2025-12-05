using System;

namespace Vehicles
{
    public abstract class Vehicle
    {
        public static int NumberOfCreatedVehicles;
        protected readonly int vehicleID;
        protected string name;

        protected Vehicle()
        {
            vehicleID = ++NumberOfCreatedVehicles;
        }

        public override string ToString()
        {
            return $"Vehicle (ID: {vehicleID}) {{ {name} }}";
        }

        public abstract void Travel(double distance);

        public virtual void Beep()
        {
            Console.WriteLine($"Vehicle {name} honks!");
        }

    }

    public class Car : Vehicle
    {
        public Car(string name)
        {
            this.name = name;
            Console.WriteLine(this + " has been created.");
        }

        public override void Travel(double distance)
        {
            Console.WriteLine($"Car {name} traveled {distance} km.");
        }

        public override string ToString()
        {
            return $"Car ({vehicleID}) {{ {name} }}";
        }

        public new void Beep()
        {
            Console.WriteLine($"Car {name} beeps!");
        }
    }

    public class Bus : Vehicle
    {
        public static readonly uint passengerLimit = 48;
        protected uint passengerCount;

        public Bus(string name)
        {
            this.name = name;
            Console.WriteLine(this + " has been created.");
        }

        public override void Travel(double distance)
        {
            Console.WriteLine($"Bus {name} traveled {distance} km with {passengerCount} passengers.");
        }

        public bool SetPassengerCount(uint passengerCount)
        {
            if (passengerCount > passengerLimit)
            {
                return false;
            }

            this.passengerCount = passengerCount;
            return true;
        }

        public uint PassengerCount()
        {
            return passengerCount;
        }

        public override string ToString()
        {
            return $"Bus ({vehicleID}) {{ {name} }} [ {passengerCount}/{passengerLimit} ]";
        }

        public override void Beep()
        {
            Console.WriteLine($"Bus {name} beeps!");
        }
    }

    public class Truck : Vehicle
    {
        public static readonly double capacity = 2500.0;
        protected double load;
        
        public Truck(string name)
        {
            this.name = name;
            Console.WriteLine( this + " has been created.");
        }

        public override void Travel(double distance)
        {
            Console.WriteLine($"Truck {name} traveled {distance} km with load of {load} kg.");
        }

        public bool SetLoad(double load)
        {
            if (load > capacity)
                return false;

            this.load = load;
            return true;
        }

        public double Load()
        {
            return load;
        }

        public override string ToString()
        {
            return $"Truck ({vehicleID}) {{ {name} }} [ {load} of {capacity} kg ]";
        }
        public override void Beep()
        {
            Console.WriteLine($"Truck {name} beeps with loud trumpet!");
        }
    }
}
