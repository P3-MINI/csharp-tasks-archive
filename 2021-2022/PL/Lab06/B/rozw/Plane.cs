using System;
using System.Collections.Generic;
using System.Text;

namespace Lab5_EngB
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


    class Plane
    {
        public enum Class
        {
            Business,
            Economy,
        }

        Person[] crew;
        Person[][] passengers;

        private readonly int[] maxCapacity;

        private readonly int mass;
        private readonly int torque;
        private readonly int horsepower;
        private readonly int tankCapaacity;

        protected double maxSpeed = 700.0;
        protected double flightHours = 0.0;

        public Plane(int _mass, int _torque, int _horsepower, int _tankCapacity, int[] _maxCapacity, params Person[] _crew)
        {
            mass = _mass;
            torque = _torque;
            horsepower = _horsepower;
            tankCapaacity = _tankCapacity;

            crew = new Person[_crew.Length];
            Array.Copy(_crew, crew, _crew.Length);

            passengers = new Person[2][];
            passengers[(int)Class.Business] = new Person[0];
            passengers[(int)Class.Economy] = new Person[0];

            maxCapacity = new int[2];
            maxCapacity[(int)Class.Business] = _maxCapacity[(int)Class.Business];
            maxCapacity[(int)Class.Economy] = _maxCapacity[(int)Class.Economy];
        }

        public void PrintInfo(bool printPassengers = true)
        {
            Console.WriteLine($"Flight Hours: {flightHours}");

            Console.WriteLine($"Mass: {mass}");
            Console.WriteLine($"Torque: {torque}");
            Console.WriteLine($"Horsepower: {horsepower}");
            Console.WriteLine($"Max Capacity Business: {maxCapacity[0]}");
            Console.WriteLine($"Max Capacity Economy: {maxCapacity[1]}");
            Console.WriteLine($"Tank Capacity: {tankCapaacity}");

            if (printPassengers)
            {
                Console.WriteLine($"Business Passengers:");
                foreach (Person person in passengers[(int)Class.Business])
                    Console.WriteLine($"\t{person}");

                Console.WriteLine($"Economy Passengers:");
                foreach (Person person in passengers[(int)Class.Economy])
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

            foreach (Person person in passengers[(int)Class.Business])
                totalMass += 2 * person.weight;

            foreach (Person person in passengers[(int)Class.Economy])
                totalMass += person.weight;

            return tankCapaacity * (torque + horsepower) / (totalMass / 17.46);
        }

        public (bool finalDestination, double remainingHours) Travel(double flightTime)
        {
            double maxRange = CalculateRange();

            double remainingTime = maxRange / maxSpeed;

            if ((remainingTime - flightHours) < flightTime)
            {
                var result = (false, flightTime - (remainingTime - flightHours));
                flightHours += (remainingTime - flightHours);
                return result;
            }

            flightHours += flightTime;
            return (true, 0.0);
        }

        private bool AddPassenger(Class _class, Person passenger)
        {
            int currentClass = (int)_class;

            if (passengers[currentClass].Length >= maxCapacity[currentClass])
                return false;

            Person[] previousPassengers = new Person[passengers[currentClass].Length];

            Array.Copy(passengers[currentClass], previousPassengers, passengers[currentClass].Length);

            passengers[currentClass] = new Person[previousPassengers.Length + 1];

            Array.Copy(previousPassengers, passengers[currentClass], previousPassengers.Length);

            passengers[currentClass][passengers[currentClass].Length - 1] = passenger;

            return true;
        }

        public bool AddBusinessPassenger(Person passenger)
        {
            return AddPassenger(Class.Business, passenger);
        }

        public bool AddEconomyPassenger(Person passenger)
        {
            return AddPassenger(Class.Economy, passenger);
        }

        public bool AddPassengers((Person passenger, Class _class)[] passengerClass)
        {
            foreach (var pc in passengerClass)
            {
                if (AddPassenger(pc._class, pc.passenger) == false) // if(!AddPassenger(pc._class, pc.passenger))
                    return false;
            }

            return true;
        }
    }
}
