using System;

namespace Task1_ObjectCreator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Part 1
            Author author = TypeCrafter.TypeCraft<Author>();
            Console.WriteLine(author);
            // Part 2
            Book book = TypeCrafter.TypeCraft<Book>();
            Console.WriteLine(book);
        }
    }
}
