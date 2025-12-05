#define STAGE01
#define STAGE02
#define STAGE03
using System;

namespace Lab5AEn
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Item[] items =
            {
                new Book("Terry Pratchett", "Small gods", new DateTime(1998, 1, 1), 10),
                new Book("J.R.R. Tolkien", "The Hobbit, or There and Back Again", new DateTime(2003, 5, 17), 13, true),
                new Magazine("NG Media", "National Geographic", new DateTime(2017, 10, 1), 5),
                new Magazine("IEEE Computer Society", "Computing Edge", new DateTime(2020, 7, 5), 2),
                new Book("Unknown", "Poetic Edda", new DateTime(1950, 4, 16), 15, true)
            };

            //Stage 1 - 3 points
#if STAGE01
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Stage one\n");
                Console.ForegroundColor = ConsoleColor.White;
                var it = 0;
                foreach (var item in items)
                {
                    Console.WriteLine(it++);
                    Console.WriteLine("id: " + item.GetId());
                    Console.WriteLine("name: " + item.GetName());
                    Console.WriteLine("author: " + item.GetAuthor());
                    Console.WriteLine("print date: " + item.GetPrintDate().Date.ToString("dd-MM-yyyy"));
                    Console.WriteLine("fee for 60 days: " + item.CalculateFee(60));
                    Console.WriteLine();
                    Console.WriteLine(item);
                    Console.Write("\n\n");
                }
            }
#endif

            //Stage 2 - 1 point
#if STAGE02
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Stage two\n");
                Console.ForegroundColor = ConsoleColor.White;

                var shelf = new Bookshelf(3);

                Console.WriteLine(shelf.AddItem(items[0]));
                Console.WriteLine(shelf.GetItemsCount());

                Console.WriteLine(shelf.AddItem(items[1]));
                Console.WriteLine(shelf.GetItemsCount());

                Console.WriteLine(shelf.AddItem(items[2]));
                Console.WriteLine(shelf.GetItemsCount());

                Console.WriteLine();

                Console.WriteLine(shelf.AddItem(items[3]));
                Console.WriteLine(shelf.GetItemsCount());

                Console.WriteLine();

                shelf.Print();
                Console.Write("\n\n");
            }
#endif

            //Stage 3 - 1 point
#if STAGE03
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Stage four\n");
                Console.ForegroundColor = ConsoleColor.White;
                var one = new Bookshelf(2);
                one.AddItem(items[2]);
                one.AddItem(items[3]);
                var two = new Bookshelf(3);
                two.AddItem(items[0]);
                two.AddItem(items[1]);
                Bookshelf[] shelves = {one, two};
                var library = new Library("L-space", shelves);
                Console.WriteLine($"Items in library: {library.GetItemsCount()}\n");
                library.Print();
                Console.WriteLine("Bookshelf number 0\n");
                library.GetBookshelf(0).Print();
                Console.Write("\n\n");
            }
#endif
        }
    }
}