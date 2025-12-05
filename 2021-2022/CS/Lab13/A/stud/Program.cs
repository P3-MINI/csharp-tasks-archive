// #define STAGE1
// #define STAGE2
// #define STAGE3
// #define STAGE4

using System;
using System.IO;
using System.Linq;

namespace Lab_12_ENG_A
{
    class Program
    {
        static void Main(string[] args)
        {
            string libraryPath = Path.Combine(Directory.GetCurrentDirectory(), "Library");

            Console.WriteLine("------------------ Stage1 ------------------");
#if STAGE1
            {
                if (Directory.Exists(libraryPath) == true)
                {
                    Directory.Delete(libraryPath, true);
                }

                Library newLibrary = Library.Create(libraryPath, "./Books.csv");

                newLibrary.Info();
            }
#endif
            Console.WriteLine("------------------ Stage2 ------------------");
#if STAGE2
            if (Directory.Exists(libraryPath) == true)
            {
                Directory.Delete(libraryPath, true);
            }

            Library currentLibrary = Library.Create(libraryPath, "./Books.csv");

            Book existingBook = currentLibrary.Books.First();

            Person author = new Person()
            {
                ID = Guid.NewGuid(),
                Name = "Joanne",
                Surname = "Rowling",
                Birthday = DateTime.Parse("31.07.1965"),
            };

            Book newBook1 = new Book()
            {
                Author = author,
                ID = Guid.NewGuid(),
                Title = "Harry Potter i Kamień Filozoficzny",
                CoverType = CoverType.Hardcover,
                Details = new Details()
                {
                    PublicationCity = "London",
                    PublisherName = "Bloomsbury Publishing",
                    PublicationDate = DateTime.Parse("26.06.1997"),
                }
            };

            Book newBook2 = new Book()
            {
                Author = author,
                ID = Guid.NewGuid(),
                Title = "Harry Potter i Komnata Tajemnic",
                CoverType = CoverType.Hardcover,
                Details = new Details()
                {
                    PublicationCity = "London",
                    PublisherName = "Bloomsbury Publishing",
                    PublicationDate = DateTime.Parse("2.07.1998"),
                }
            };

            currentLibrary.Add(newBook1);
            currentLibrary.Add(newBook2);

            try
            {
                currentLibrary.Add(existingBook);
            }
            catch (PathAlreadyExistsException)
            {
                Console.WriteLine("PathAlreadyExistsException Expected: OK!");
            }

            currentLibrary.Info();
#endif

            Console.WriteLine("------------------ Stage3 ------------------");
#if STAGE3
            {
                Book book1 = currentLibrary.Get("Harry Potter i Komnata Tajemnic");
                Book book2 = currentLibrary.Get("Małżeństwo - przemyśl to");
                Book book3 = currentLibrary.Get("Not Existing Book");

                if (book1 != null) Console.WriteLine("Book #1 Found: OK!"); else Console.WriteLine("Book #1 NOT Found: ERROR!");
                if (book2 != null) Console.WriteLine("Book #2 Found: OK!"); else Console.WriteLine("Book #2 NOT Found: ERROR!");
                if (book3 == null) Console.WriteLine("Book #3 NOT Found: OK!"); else Console.WriteLine("Book #3 Found: ERROR!");

                Console.WriteLine(book1);
                Console.WriteLine(book2);

                try
                {
                    currentLibrary.Delete(Guid.NewGuid());
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("FileNotFoundException Expected: OK!");
                }


                if (currentLibrary.Delete(book1.ID))
                    currentLibrary.Info();
            }
#endif

            Console.WriteLine("------------------ Stage4 ------------------");
#if STAGE4
            {
                string pathFilename = Path.Combine(Directory.GetCurrentDirectory(), "Books.xml");

                currentLibrary.Save(pathFilename);

                if (File.Exists(pathFilename) == true) Console.WriteLine("Binary File Found: OK!");
                else Console.WriteLine("Binary File NOT Found: Error!");
            }
#endif
        }
    }
}
