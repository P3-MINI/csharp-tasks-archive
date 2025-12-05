using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace lab_13pl
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("------------------ Etap1 ------------------");
            // var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "Events");
            // if(Directory.Exists(rootPath)){
            //     Directory.Delete(rootPath, true);
            // }
            // var manager = new EventManager(rootPath);
            // var importedEvents = manager.Import(Path.Combine(Directory.GetCurrentDirectory(), "import.xml"));

            // var owners = importedEvents.Select(s => s.Owner).Distinct();
            // var expectedOwners = new [] {"Henryk Biegała", "Iwona Kiwała", "Janusz Bąk"};
            // VerifyCollectionsAreEqual(expectedOwners, owners);

            // var nonExistingFileName = "non-existing-file.xml";
            // try
            // {
            //     manager.Import(Path.Combine(Directory.GetCurrentDirectory(), nonExistingFileName));
            //     Console.WriteLine($"ERROR: Exception should be raised. {nonExistingFileName} doesn't exist");
            // }
            // catch (FileNotFoundException e)
            // {
            //     Console.WriteLine($"OK: Exception raised. File {e.FileName} doesn't exist");
            // }


            Console.WriteLine("\n------------------ Etap2 ------------------");
            // foreach (var importedEvent in importedEvents)
            // {
            //     manager.AddEvent(importedEvent);
            // }

            // var newOwner ="Grzegorz Brzęczyszczykiewicz";
            // var newData = new List<Event> {
            //     new Event{Id = Guid.NewGuid(), Name="DeviceEnabled", EventOccurredAt=DateTime.Parse("2020-01-09T13:01:00"), Owner=newOwner},
            //     new Event{Id = Guid.NewGuid(), Name="ProgramSet", EventOccurredAt=DateTime.Parse("2020-01-09T13:04:00"), Owner=newOwner, Details="ProgramId=P1"},
            //     new Event{Id = Guid.NewGuid(), Name="DeviceDisabled", EventOccurredAt=DateTime.Parse("2020-01-10T01:00:00"), Owner=newOwner}
            // };
            // foreach (var item in newData)
            // {
            //     manager.AddEvent(item);
            // }

            // var files =Directory.EnumerateFiles(rootPath, "*", SearchOption.AllDirectories);
            // Console.WriteLine("List of all created files:");
            // foreach (var file in files)
            // {
            //     Console.WriteLine(file.Replace(rootPath, ""));
            // }
            // var expectedNumberOfFiles = 6;
            // var actualNumberOfFiles = files.Count();
            // var filesAreValid = expectedNumberOfFiles == files.Count();
            // Console.WriteLine($"{( filesAreValid ? "OK": "ERROR" )}: Number of files is {actualNumberOfFiles}");


            Console.WriteLine("\n------------------ Etap 3 ------------------");
            // var storedData = manager.GetOwnerEvents(newOwner);
            // VerifyCollectionsAreEqual(newData, storedData);

            // var resultForInvalidOwner = manager.GetOwnerEvents("Jakub Nieistniejący");
            // Console.WriteLine($"{( resultForInvalidOwner.Count() == 0 ? "OK": "ERROR" )}: For non existing owner empty collection should be returned");

            // var isSuccessfullyRemoved = manager.RemoveOwnerEvents(newOwner);
            // Console.WriteLine($"{( isSuccessfullyRemoved ? "OK": "ERROR" )}: User exists - removal should succeed");
            // isSuccessfullyRemoved = manager.RemoveOwnerEvents(newOwner);
            // Console.WriteLine($"{( !isSuccessfullyRemoved ? "OK": "ERROR" )}: User doesn't exist - removal should return false");

            // var dataForRemovedUser = manager.GetOwnerEvents(newOwner);
            // Console.WriteLine($"{( dataForRemovedUser.Count() == 0 ? "OK": "ERROR" )}: After removing user get data result should be empty");


            Console.WriteLine("\n------------------ Etap 4 ------------------");
            // var exportPath = Path.Combine(Directory.GetCurrentDirectory(), "export.xml");
            // manager.Export(exportPath);
            // var importedExportedEvents = manager.Import(exportPath);
            // VerifyCollectionsAreEqual(importedEvents, importedExportedEvents);
        }

        public static void VerifyCollectionsAreEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual) where T : IEquatable<T>
        {
            var areCollectionsEqual = Enumerable.SequenceEqual(expected, actual);
            if(areCollectionsEqual){
                Console.WriteLine($"OK: Collections are equal");
            } else {
                Console.WriteLine($"ERROR: There is an issue - collections are different");
            }
        }
    }
}
