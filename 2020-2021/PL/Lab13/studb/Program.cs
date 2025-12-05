using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace lab13_a
{
    // TODO: ODKOMENTOWĄC w Etap1
    // public static class PrintExtensions
    // {
    //     public static void PrintItem(this Student item)
    //     {
    //         Console.WriteLine($"{item.Id}: {item.FirstName} {item.LastName}, birth: {item.BirthDate:yyyy-MM-dd}, type:{item.StudentType}, address: {item?.Address?.City}, {item?.Address?.Street} {item?.Address?.StreetNo}");
    //     }

    //     public static void PrintItems(this IEnumerable<Student> items)
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
            // var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "Students");
            // if(Directory.Exists(rootPath)){
            //     Directory.Delete(rootPath, true);
            // }
            // var db = new StudentsDb(rootPath);
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
            // var nonExistingItem = db.Get("non-existing","person");
            // Console.WriteLine($"{( nonExistingItem == null ? "OK": "ERROR" )}: geting object for non existing item should return null");
            
            // Console.WriteLine($"\nGetting existing item:");
            // var lastItem = importedItems.Last();
            // var readItem = db.Get(lastItem.FirstName, lastItem.LastName);
            // readItem?.PrintItem();

            Console.WriteLine("\n------------------ Etap 3 ------------------");

            // var newItem = new Student
            // {
            //     Id = 6,
            //     FirstName = "Grzegorz",
            //     LastName = "Brzęczyszczykiewicz",
            //     BirthDate = new DateTime(1990, 01, 01),
            //     StudentType = StudentType.FullTime,
            //     Address = new Address
            //     {
            //         City = "Chrząszczyżewoszyce",
            //         Street = "Łękołody",
            //         StreetNo = "1"
            //     }
            // };
            // db.AddOrUpdate(newItem);
            // newItem.StudentType = StudentType.External;
            // db.AddOrUpdate(newItem);
            // var newItemCopy = db.Get(newItem.FirstName, newItem.LastName);
            // newItemCopy.PrintItem();

            // db.Delete(newItem.FirstName, newItem.LastName);
            // try
            // {
            //     db.Delete(newItem.FirstName, newItem.LastName);
            //     Console.WriteLine($"ERROR: Exception should be raised. {newItem.FirstName}-{newItem.LastName} doesn't exist");
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
            // var serializedContent = File.ReadAllText(Path.Combine(rootPath, $"{newItem.FirstName}-{newItem.LastName}.json"));
            // Console.WriteLine(serializedContent);
            // Console.WriteLine($"{( serializedContent.Contains("External")  ? "OK": "ERROR" )}: Enum StudentType value should be serialized using string value");
            // Console.WriteLine($"{( serializedContent.Contains("First name")  ? "OK": "ERROR" )}: FirstName field name should be serialized as: First name");
            // Console.WriteLine($"{( serializedContent.Contains("Last name")  ? "OK": "ERROR" )}: LastName field name should be serialized as: Last name");
        }
    }
}
