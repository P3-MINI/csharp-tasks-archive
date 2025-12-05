using System;
using System.Collections.Generic;
using System.Text;

namespace Lab5_EngA
{
    struct Person
    {
        public int weight;
        public string name;
        public string surname;

        public Person(string _surname, string _name, int _weight = 75)
        {
            surname = _surname;
            name = _name;

            weight = _weight;
        }

        public void Deconstruct(out string _name, out string _surname)
        {
            _name = name;
            _surname = surname;
        }

        public override string ToString()
        {
            return $"{surname} {name}";
        }
    }

    class Car
    {
        protected readonly int mass;
        protected readonly int torque;
        protected readonly int horsepower;
        protected readonly int maxCapacity;
        protected readonly int tankCapaacity;

        protected double milage = 0.0;

        protected Person[] passengers;

        public Car(int _mass, int _torque, int _horsepower, int _tankCapacity, int _maxCapacity, params Person[] _passengers)
        {
            mass = _mass;
            torque = _torque;
            horsepower = _horsepower;
            maxCapacity = _maxCapacity;
            tankCapaacity = _tankCapacity;

            passengers = new Person[_passengers.Length];
            Array.Copy(_passengers, passengers, _passengers.Length);
        }

        public void PrintInfo(bool printPassengers = true)
        {
            Console.WriteLine($"Milage: {milage}");

            Console.WriteLine($"Mass: {mass}");
            Console.WriteLine($"Torque: {torque}");
            Console.WriteLine($"Horsepower: {horsepower}");
            Console.WriteLine($"Max Capacity: {maxCapacity}");
            Console.WriteLine($"Tank Capacity: {tankCapaacity}");

            if (printPassengers)
            {
                Console.WriteLine($"Passengers:");

                foreach (Person person in passengers)
                    Console.WriteLine($"\t{person}");
            }
        }

        public void Deconstruct(out int _torque, out int _horsepower, out int _tankCapacity)
        {
            _torque = torque;
            _horsepower = horsepower;
            _tankCapacity = tankCapaacity;
        }

        public double CalculateRange()
        {
            int totalMass = mass;

            foreach (Person person in passengers)
                totalMass += person.weight;

            return tankCapaacity * (torque + horsepower) / (totalMass / 17.46);
        }

        public (bool finalDestination, double remainingDistance) Travel(double distance)
        {
            double maxRange = CalculateRange();

            if ((maxRange - milage) < distance)
            {
                var result = (false, distance - (maxRange - milage));
                milage += (maxRange - milage);
                return result;
            }

            milage += distance;
            return (true, 0.0);
        }

        public bool AddPassenger(Person passenger)
        {
            if (passengers.Length >= maxCapacity)
                return false;

            Person[] previousPassengers = passengers;

            passengers = new Person[previousPassengers.Length + 1];

            Array.Copy(previousPassengers, passengers, previousPassengers.Length);

            passengers[passengers.Length - 1] = passenger;

            return true;
        }
    }

    class Bus : Car
    {
        public Bus(int _mass, int _torque, int _horsepower, int _tankCapacity, int _maxCapacity, params Person[] _passengers)
            : base(_mass, _torque, _horsepower, _tankCapacity, _maxCapacity, _passengers) { }

        public bool AddPassengers(params Person[] _passengers)
        {
            foreach (Person passenger in _passengers)
            {
                if (AddPassenger(passenger) == false) // if(!AddPassenger(passenger))
                    return false;
            }

            return true;
        }
    }
}
