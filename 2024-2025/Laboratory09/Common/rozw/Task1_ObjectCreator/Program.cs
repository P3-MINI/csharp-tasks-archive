using System;

namespace Task1_ObjectCreator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            InvokeCraft<Author>();
            InvokeCraft<Book>();
        }

        private static void InvokeCraft<T>()
        {
            try
            {
                T craftedObject = TypeCrafter.TypeCraft<T>();
                Console.WriteLine(craftedObject);
            }
            catch (ParseException ex)
            {
                Console.WriteLine($"Wrong format for parsing. Attempting to craft object once again.");
                InvokeCraft<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Received unexpected exception with message: {ex.Message} Cannot create object of type {typeof(T)}.");
            }
        }
    }
}
