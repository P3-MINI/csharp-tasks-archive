using System.Xml.Serialization;
using System.IO;
using System;
using System.Collections.Generic;

namespace lab13_b
{
    public class Book
    {
        [XmlElement("Identifier")]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        [XmlElement("PrintLength")]
        public int PagesNumber { get; set; }
        [XmlAttribute("BookType")]
        public BookType BookType { get; set; }
        public PublicationDetails PublicationDetails { get; set; }
    }

    public class PublicationDetails
    {
        public DateTime PublicationDate { get; set; }
        public string PublicationPlace { get; set; }
        public string Publisher { get; set; }
    }

    public enum BookType
    {
        Hardcover,
        Ebook
    }

    public class LibraryDb
    {
        private string _rootPath;

        public LibraryDb(string rootPath)
        {
            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }
            _rootPath = rootPath;
        }

        public IEnumerable<Book> ImportFromCsv(string filePath)
        {
            if(!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File {filePath} doesn't exist", Path.GetFileName(filePath));
            }

            var items = new List<Book>();
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

                var item = new Book
                {
                    Id = int.Parse(line[0]),
                    Title = line[1],
                    Author = line[2],
                    PagesNumber = int.Parse(line[3]),
                    BookType = (BookType)Enum.Parse(typeof(BookType), line[4]),
                    PublicationDetails = new PublicationDetails
                    {
                        PublicationDate = DateTime.ParseExact(line[5], "dd.MM.yyyy", null),
                        PublicationPlace = line[6],
                        Publisher = line[7]
                    }
                };
                items.Add(item);
            }
            return items;
        }

        public void Add(Book item)
        {
            var filePath = GetBookFullPath(item.Title);
            if(File.Exists(filePath))
            {
                throw new InvalidOperationException("Item with provided title already exists");
            }
            XmlSerializer serializer = new XmlSerializer(typeof(Book));
            using var sw = new StreamWriter(filePath);
            serializer.Serialize(sw, item);
        }

        public Book Get(string title)
        {
            var filePath = GetBookFullPath(title);
            if(!File.Exists(filePath))
            {
                return null;
            }

            XmlSerializer serializer = new XmlSerializer(typeof(Book));
            using var sr = new StreamReader(filePath);
            return (Book)serializer.Deserialize(sr);
        }

        public void AddOrUpdate(Book item)
        {
            var filePath = GetBookFullPath(item.Title);
            XmlSerializer serializer = new XmlSerializer(typeof(Book));
            using var sw = new StreamWriter(filePath);
            serializer.Serialize(sw, item);
        }

        public void Delete(string title)
        {
            var filePath = GetBookFullPath(title);
            if(File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            else
            {
                throw new InvalidOperationException($"Item with provided title doesn't exist");
            }
        }

        public IEnumerable<Book> List()
        {
            var files = Directory.EnumerateFiles(_rootPath, "*.xml", SearchOption.AllDirectories);
            var books = new List<Book>();
            XmlSerializer serializer = new XmlSerializer(typeof(Book));

            foreach (var file in files)
            {
                using var sr = new StreamReader(file);
                var book = (Book)serializer.Deserialize(sr);
                books.Add(book);
            }

            return books;
        }

        private string GetBookFullPath(string title)
        {
            var fileName = $"{title}.xml";
            return Path.Combine(_rootPath, fileName);
        }
    }
}