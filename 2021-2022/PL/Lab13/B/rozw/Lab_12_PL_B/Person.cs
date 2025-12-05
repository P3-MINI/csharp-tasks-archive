using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;

namespace Lab_12
{
    [Serializable]
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

    [Serializable]
    public class Prize
    {
        public int Place { get; set; }
        public Contest Contest { get; set; }

        public override string ToString()
        {
            return $"{Place} at {Contest}";
        }
    }

    [Serializable]
    public class Contest
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        [XmlIgnore] public List<Person> Participants { get; set; }

        public override string ToString()
        {
            return $"{Name} took place on {Date}";
        }
    }

    public class PathAlreadyExistsException : Exception
    {
        public PathAlreadyExistsException()
            : base("Couldn't Create Library. Directory Already Exists Exception.") { }
    }

    public class Scoreboard : IEnumerable
    {
        public List<Contest> Contests { get; set; } = new List<Contest>();
        public List<Person> Participants { get; set; } = new List<Person>();

        private string SourcePath { get; set; }

        public Scoreboard(string savePath = "")
        {
            if (savePath != string.Empty && Directory.Exists(savePath) == false)
            {
                Directory.CreateDirectory(savePath);
            }

            this.SourcePath = savePath;
        }

        public void Add(Contest contest)
        {
            string path = Path.Combine(this.SourcePath, $"{contest.Name}.xml");

            if (File.Exists(path) == true)
            {
                throw new PathAlreadyExistsException();
            }

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Contest));

                serializer.Serialize(stream, contest);
            }

            this.Contests.Add(contest);
        }

        public void Info()
        {
            foreach (Person person in this.Participants)
            {
                Console.WriteLine(person);

                foreach (Prize prize in person.Prizes)
                    Console.WriteLine($"\t{prize}");
            }

            Console.WriteLine();

            foreach (Contest contest in this.Contests)
                Console.WriteLine(contest);
        }

        public void Update(Contest contest)
        {
            string path = Path.Combine(this.SourcePath, $"{contest.Name}.xml");

            if (File.Exists(path) == false)
            {
                throw new FileNotFoundException();
            }

            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Contest));

                serializer.Serialize(stream, contest);
            }
        }

        public bool Delete(string contestName)
        {
            string filePath = Path.Combine(this.SourcePath, $"{contestName}.xml");

            if (File.Exists(filePath) == false)
            {
                throw new FileNotFoundException();
            }

            File.Delete(filePath); return true;
        }

        public IEnumerator GetEnumerator()
        {
            foreach (string fileinfo in Directory.EnumerateFiles(this.SourcePath))
            {
                using (StreamReader reader = new StreamReader(fileinfo))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Contest));

                    yield return serializer.Deserialize(reader) as Contest;
                }
            }
        }

        public static Scoreboard Create(string sourceFileContests)
        {
            if (File.Exists(sourceFileContests) == false)
            {
                throw new FileNotFoundException();
            }

            Scoreboard newScoreboard = new Scoreboard();

            using (FileStream stream = new FileStream(sourceFileContests, FileMode.Open))
            {
                BinaryFormatter serializer = new BinaryFormatter();
                newScoreboard.Contests = serializer.Deserialize(stream) as List<Contest>;
            }

            foreach (Contest contest in newScoreboard.Contests)
            {
                foreach (Person participant in contest.Participants)
                {
                    if (newScoreboard.Participants.Contains(participant) == false)
                    {
                        newScoreboard.Participants.Add(participant);
                    }
                }
            }

            return newScoreboard;
        }

        public void Save(string directoryPath)
        {
            using (FileStream writer = new FileStream(directoryPath, FileMode.Create))
            {
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(writer, this.Participants);
            }
        }
    }
}
