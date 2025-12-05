using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lab_12_ENG_A
{
    public class Book
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public CoverType CoverType { get; set; }
        public Person Author { get; set; }
        public Details Details { get; set; }

        public override string ToString()
        {
            return $"Book '{Title}' by {Author}";
        }
    }

    public enum CoverType
    {
        Hardcover, EBook
    }

    public class Details
    {
        public DateTime PublicationDate { get; set; }
        public string PublicationCity { get; set; }
        public string PublisherName { get; set; }
    }

    public class Person
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }

        public override string ToString()
        {
            return $"{Name} {Surname}";
        }
    }

    public class Library
    {

    }
}
