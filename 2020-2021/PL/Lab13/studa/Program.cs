using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace lab13_b
{
    //TODO: Odkomentować w etapie 1
    // public static class PrintExtensions
    // {
    //     public static void PrintItem(this Book item)
    //     {
    //         Console.WriteLine($"{item.Id}: {item.Title}, {item.Author}, pages: {item.PagesNumber}, type:{item.BookType}, publish details: {item?.PublicationDetails?.Publisher}, {item?.PublicationDetails?.PublicationDate:yyyy-MM-dd}, {item?.PublicationDetails?.PublicationPlace}");
    //     }

    //     public static void PrintItems(this IEnumerable<Book> items)
    //     {
    //         foreach (var item in items)
    //         {
    //             item.PrintItem();
    //         }
    //     }
    // }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("------------------ Etap1 ------------------");
            // var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "Library");
            // if(Directory.Exists(rootPath)){
            //     Directory.Delete(rootPath, true);
            // }
            // var db = new LibraryDb(rootPath);
            // var importedItems = db.ImportFromCsv(Path.Combine(Directory.GetCurrentDirectory(), "import.csv"));

            // Console.WriteLine("Imported items:");
            // importedItems.PrintItems();

            // var nonExistingFileName = "non-existing-file.csv";
            // try
            // {
            //     db.ImportFromCsv(Path.Combine(Directory.GetCurrentDirectory(), nonExistingFileName));
            //     Console.WriteLine($"\nERROR: Exception should be raised. {nonExistingFileName} doesn't exist");
            // }
            // catch (FileNotFoundException e)
            // {
            //     Console.WriteLine($"\nOK: FileNotFoundException raised. File {e.FileName} doesn't exist");
            // }

            Console.WriteLine("\n------------------ Etap2 ------------------");
            // foreach (var importedItem in importedItems)
            // {
            //     db.Add(importedItem);
            // }

            // var files =Directory.EnumerateFiles(rootPath, "*", SearchOption.AllDirectories);
            // Console.WriteLine("List of all created files:");
            // foreach (var file in files)
            // {
            //     Console.WriteLine(file.Replace(rootPath, ""));
            // }
            // var actualNumberOfFiles = files.Count();
            // var filesAreValid = importedItems.Count() == files.Count();
            // Console.WriteLine($"{( filesAreValid ? "OK": "ERROR" )}: Number of files is {actualNumberOfFiles}");

            // Console.WriteLine($"\nRead single item");
            // var nonExistingItem = db.Get("non-existing-book");
            // Console.WriteLine($"{( nonExistingItem == null ? "OK": "ERROR" )}: geting object for non existing item should return null");
            
            // Console.WriteLine($"\nGetting existing item:");
            // var lastItem = importedItems.Last();
            // var readItem = db.Get(lastItem.Title);
            // readItem?.PrintItem();

            Console.WriteLine("\n------------------ Etap 3 ------------------");

            // var newItem = new Book
            // {
            //     Id = 6,
            //     Title = "The Pragmatic Programmer",
            //     Author = "Andrew Hunt",
            //     BookType = BookType.Ebook,
            //     PagesNumber = 458,
            //     PublicationDetails = new PublicationDetails
            //     {
            //         Publisher = "Addison-Wesley Professional",
            //         PublicationDate = new DateTime(1999, 10, 20),
            //         PublicationPlace = "Boston"
            //     }
            // };
            // db.AddOrUpdate(newItem);
            // newItem.BookType = BookType.Hardcover;
            // db.AddOrUpdate(newItem);
            // var newItemCopy = db.Get(newItem.Title);
            // newItemCopy.PrintItem();

            // db.Delete(newItem.Title);

            // try
            // {
            //     db.Delete(newItem.Title);
            //     Console.WriteLine($"ERROR: Exception should be raised. {newItem.Title} doesn't exist");
            // }
            // catch (InvalidOperationException)
            // {
            //     Console.WriteLine($"OK: InvalidOperationException raised when delete operates on non-existing object");
            // }

            // files =Directory.EnumerateFiles(rootPath, "*", SearchOption.AllDirectories);
            // Console.WriteLine("\nList of all files:");
            // foreach (var file in files)
            // {
            //     Console.WriteLine(file.Replace(rootPath, ""));
            // }
            // actualNumberOfFiles = files.Count();
            // filesAreValid = importedItems.Count() == files.Count();
            // Console.WriteLine($"{( filesAreValid ? "OK": "ERROR" )}: Number of files is {actualNumberOfFiles}");

            // var allStoredItems = db.List();
            // Console.WriteLine($"\nList whole db:");
            // allStoredItems.PrintItems();

            Console.WriteLine("\n------------------ Etap 4 ------------------");
            // db.AddOrUpdate(newItem);
            // var serializedContent = File.ReadAllText(Path.Combine(rootPath, $"{newItem.Title}.xml"));
            // Console.WriteLine(serializedContent);
            // Console.WriteLine($"{( serializedContent.Contains("BookType=\"Hardcover\"")  ? "OK": "ERROR" )}: Enum BookType value should be serialized as attribute");
            // Console.WriteLine($"{( serializedContent.Contains("PrintLength")  ? "OK": "ERROR" )}: PagesNumber field name should be serialized as: PrintLength");
            // Console.WriteLine($"{( serializedContent.Contains("Identifier")  ? "OK": "ERROR" )}: Id field name should be serialized as: Identifier");
        }
    }
}
