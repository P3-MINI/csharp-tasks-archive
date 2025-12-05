using System;

namespace Lab02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Employee employee_1 = new Employee("Mike", 27, Position.ScrumMaster);
            Employee employee_2 = new Employee("Johnny", 34, Position.ScrumMaster);
            Employee employee_3 = new Employee("Adam", 47, Position.SoftwareEngineer);

            Person employee_4 = new Employee("Stephan", 34, Position.SoftwareEngineer);


            Company company = new Company("MyCompany");

            company.AddEmployee(employee_1);
            company.AddEmployee(employee_2);
            company.AddEmployee(employee_3);

            foreach(Employee employee in company.GetEmployees())
            {
                Console.WriteLine(employee);
            }

            employee_2.PromoteEmployee(Position.SoftwareEngineer);
        }
    }

    internal abstract class Person
    {
        public string name;
        public int age;

        protected Person(string name, int age)
        {
            this.name = name;
            this.age = age;
        }

        public void Display()
        {
            Console.WriteLine($"Name: {name}, Age: {age}");
        }
    }

    internal enum Position
    {
        ScrumMaster,
        SoftwareEngineer,
    }

    internal class Employee : Person
    {
        private static int sID;

        public int id;

        public Position position;

        public Employee(string name, int age, Position position)
            : base(name, age)
        {
            this.id = ++sID;
            this.position = position;
        }

        public void PromoteEmployee(Position newPosition)
        {
            position = newPosition;
        }
    }

    internal class Company
    {
        private string name;
        private List<Employee> Employees = new List<Employee>();

        public Company(string name)
        {
            this.name = name;
        }

        public void AddEmployee(Employee employee) => this.Employees.Add(employee);

        public List<Employee> GetEmployees() => Employees;
    }
}
