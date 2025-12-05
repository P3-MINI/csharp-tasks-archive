using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using static _0_serialization.Program;

namespace _0_serialization
{
    [Serializable] // STUDENT_CODE
    public class Person
    {
        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        private Person() { } // STUDENT_CODE

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
        public override string ToString()
        {
            return $"Full name: {FirstName.PadRight(10)} {LastName.PadRight(10)} | {Country.PadRight(15)} | DOB: {DateOfBirth:yyyy-mm-dd}";
        }
    }

    public static class PersonListExtender
    {
        public static void CompareLists(this List<Person> source, List<Person> other)
        {

            if (other == null || source == null)
            {
                WriteColor("At least list is null");
                return;
            }
            if (source.Count != other.Count)
            {
                WriteColor("Number of elements mismatch between lists");
                return;
            }

            for (int i = 0; i < source.Count; i++)
            {
                if (other[i] == null && source[i] != null)
                {
                    WriteColor($"Person at index {i} was null");
                    return;
                }
                if (other[i].FirstName != source[i].FirstName)
                {
                    WriteColor($"Persons first name mismatch at index {i}");
                    return;
                }
                if (other[i].LastName != source[i].LastName)
                {
                    WriteColor($"Persons last name mismatch at index {i}");
                    return;
                }
                if (other[i].Country != source[i].Country)
                {
                    WriteColor($"Persons country mismatch at index {i}");
                    return;
                }
                if (other[i].DateOfBirth != source[i].DateOfBirth)
                {
                    WriteColor($"Persons date of birth mismatch at index {i}");
                    return;
                }
            }
            Console.WriteLine("Persons are the same");
        }
    }
}