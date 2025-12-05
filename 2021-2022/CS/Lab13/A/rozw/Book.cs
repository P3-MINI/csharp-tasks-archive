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
        [JsonPropertyName("GUID")] public Guid ID { get; set; }
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
        [JsonPropertyName("GUID")] public Guid ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }

        public override string ToString()
        {
            return $"{Name} {Surname}";
        }
    }


    public class PathAlreadyExistsException : Exception
    {
        public PathAlreadyExistsException()
            : base("Couldn't Create Library. Directory Already Exists Exception.") { }
    }

    public class Library
    {
        public List<Book> Books { get; set; } = new List<Book>();
        public List<Person> Authors { get; set; } = new List<Person>();

        private string SourcePath { get; set; }

        public Library(string libraryPath)
        {
            if (Directory.Exists(libraryPath) == false)
            {
                Directory.CreateDirectory(libraryPath);
            }

            this.SourcePath = libraryPath;
        }

        public void Info()
        {
            foreach (Book book in this.Books)
                Console.WriteLine(book);
        }

        public static Library Create(string libraryPath, string sourceFile)
        {
            if (File.Exists(sourceFile) == false)
            {
                throw new FileNotFoundException($"Cannot Create Library From {sourceFile} File");
            }

            Library newLibrary = new Library(libraryPath);

            using (StreamReader reader = new StreamReader(sourceFile))
            {
                _ = reader.ReadLine(); /* Discard First Row (Header Row) Of CSV File*/

                while (reader.EndOfStream == false)
                {
                    string[] currentLine = reader.ReadLine().Split(";");

                    Person newAuthor = new Person()
                    {
                        ID = new Guid(currentLine[3]),
                        Name = currentLine[4],
                        Surname = currentLine[5],
                        Birthday = DateTime.Parse(currentLine[6]),
                    };

                    Book newBook = new Book()
                    {
                        ID = new Guid(currentLine[0]),
                        Title = currentLine[1],
                        CoverType = (CoverType)Enum.Parse(typeof(CoverType), currentLine[2]),
                        Author = newAuthor,
                        Details = new Details()
                        {
                            PublicationDate = DateTime.Parse(currentLine[7]),
                            PublicationCity = currentLine[8],
                            PublisherName = currentLine[9],
                        },
                    };

                    /* Stage_1 */
                    {
                        //newLibrary.Authors.Add(newAuthor);
                        //newLibrary.Books.Add(newBook);
                    }
                    /* Stage_2 */
                    {
                        newLibrary.Add(newAuthor);
                        newLibrary.Add(newBook);
                    }
                }
            }

            return newLibrary;
        }

        public void Add(Person newAuthor)
        {
            this.Add(newAuthor, $"{newAuthor.ID}.json");
            this.Authors.Add(newAuthor);
        }

        public void Add(Book newBook)
        {
            this.Add(newBook, $"{newBook.ID}.json");
            this.Books.Add(newBook);
        }

        private void Add(object _object, string filename)
        {
            string path = Path.Combine(this.SourcePath, filename);

            if (File.Exists(path) == true)
            {
                throw new PathAlreadyExistsException();
            }

            using (StreamWriter stream = new StreamWriter(path))
            {
                string json = JsonSerializer.Serialize(_object);

                stream.Write(json);
            }
        }

        public Book Get(string title)
        {
            string directoryPath = this.SourcePath;

            foreach (string filename in Directory.EnumerateFiles(directoryPath))
            {
                using (StreamReader reader = new StreamReader(filename))
                {
                    string json = reader.ReadToEnd();

                    /* Doesn't throw exception as xml serializer.
                       Just interprets as many fields as possible and creates Book */
                    Book book = JsonSerializer.Deserialize(json, typeof(Book)) as Book;

                    if (book.Title == title) return book;
                }
            }

            return null;
        }

        public bool Delete(Guid guid)
        {
            string filePath = Path.Combine(this.SourcePath, $"{guid}.json");

            if (File.Exists(filePath) == false)
            {
                throw new FileNotFoundException();
            }

            File.Delete(filePath); return true;
        }

        public void Save(string directoryPath)
        {
            using (FileStream writer = new FileStream(directoryPath, FileMode.Create))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Book>));
                serializer.Serialize(writer, this.Books);
            }
        }
    }
}
