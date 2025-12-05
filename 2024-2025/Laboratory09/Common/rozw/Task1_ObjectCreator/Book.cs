using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Task1_ObjectCreator
{
    public class Author
    {
        public Author() { }
        public Author(string name, string surname, DateTime yearOfBirth)
        {
            Name = name;
            Surname = surname;
            BirthDate = yearOfBirth;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }

        public override string? ToString()
        {
            return $"Author {Name} {Surname} (Age {DateTime.Now.Year - BirthDate.Year})";
        }
    }
    
    public class Book
    {
        public string Title { get; set; }
        public Author Author { get; set; }

        public override string? ToString()
        {
            return $"Book '{Title} - written by {Author}";
        }
    }
}
