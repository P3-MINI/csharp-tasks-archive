using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace lab13_a
{
    public class Student
    {
        public int Id { get; set; }
        [JsonPropertyName("First name")]
        public string FirstName { get; set; }
        [JsonPropertyName("Last name")]
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public StudentType StudentType { get; set; }
        public Address Address { get; set; }
    }

    public enum StudentType
    {
        FullTime,
        External
    }

    public class Address
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string StreetNo { get; set; }
    }

    public class StudentsDb
    {
        private readonly string _rootPath;

        public StudentsDb(string rootPath)
        {
            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }
            _rootPath = rootPath;
        }

        public IEnumerable<Student> ImportFromCsv(string filePath)
        {
            if(!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File {filePath} doesn't exist", Path.GetFileName(filePath));
            }

            var items = new List<Student>();
            using var sr = new StreamReader(filePath);
            var isHeader = true;
            while(!sr.EndOfStream)
            {
                var line = sr.ReadLine().Split(";");
                if(isHeader)
                {
                    isHeader = false;
                    continue;
                }

                var item = new Student
                {
                    Id = int.Parse(line[0]),
                    FirstName = line[1],
                    LastName = line[2],
                    BirthDate = DateTime.ParseExact(line[3], "dd.MM.yyyy", null),
                    StudentType = (StudentType)Enum.Parse(typeof(StudentType), line[4]),
                    Address = new Address
                    {
                        City = line[5],
                        Street = line[6],
                        StreetNo = line[7]
                    }
                };
                items.Add(item);
            }
            return items;
        }

        public void Add(Student item)
        {
            var filePath = CreateStudentFullPath(item.FirstName, item.LastName);
            if(File.Exists(filePath))
            {
                throw new InvalidOperationException($"Item with provided name, already exists");
            }

            using var sw = new StreamWriter(filePath);
            sw.Write(JsonSerializer.Serialize(item));
        }

        public Student Get(string firstName, string lastName)
        {
            var filePath = CreateStudentFullPath(firstName, lastName);
            if(!File.Exists(filePath))
            {
                return null;
            }
            using var sw = new StreamReader(filePath);
            return JsonSerializer.Deserialize<Student>(sw.ReadToEnd());
        }

        private string CreateStudentFullPath(string firstName, string lastName)
        {
            var fileName = $"{firstName}-{lastName}.json";
            return Path.Combine(_rootPath, fileName);
        }

        public void AddOrUpdate(Student item)
        {
            var filePath = CreateStudentFullPath(item.FirstName, item.LastName);

            using var sw = new StreamWriter(filePath, false);
            sw.Write(JsonSerializer.Serialize(item));
        }

        public void Delete(string firstName, string lastName)
        {
            var filePath = CreateStudentFullPath(firstName, lastName);
            if(File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            else
            {
                throw new InvalidOperationException($"Item with name {firstName}-{lastName} doesn't exist");
            }
        }

        public IEnumerable<Student> List()
        {
            var files = Directory.EnumerateFiles(_rootPath, "*.json", SearchOption.AllDirectories);

            var items = new List<Student>();
            foreach (var file in files)
            {
                using var sw = new StreamReader(file);
                items.Add(JsonSerializer.Deserialize<Student>(sw.ReadToEnd()));
            }
            return items;
        }
    }
}