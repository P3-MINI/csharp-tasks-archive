using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;

namespace Lab_12
{
    public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public List<Prize> Prizes { get; set; }

        public override string ToString()
        {
            return $"{Name} {Surname}";
        }
    }

    public class Prize
    {
        public int Place { get; set; }
        public Contest Contest { get; set; }

        public override string ToString()
        {
            return $"{Place} at {Contest}";
        }
    }

    public class Contest
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public List<Person> Participants { get; set; }

        public override string ToString()
        {
            return $"{Name} took place on {Date}";
        }
    }

    public class Scoreboard
    {
        
    }
}
