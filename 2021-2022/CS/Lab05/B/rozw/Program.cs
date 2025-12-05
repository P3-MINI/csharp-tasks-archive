#define STAGE01
#define STAGE02
#define STAGE03
#define STAGE04
using System;

namespace Lab5BEn
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Item[] items =
            {
                new Mail("Carrot", "Vimes", DateTime.Now.AddDays(-1)),
                new Mail("Frodo", "Gandalf", DateTime.Now.AddDays(-4), true),
                new Package("Paul", "Shaddam IV Corrino", DateTime.MaxValue.AddDays(-1000), 5),
                new Package("Ford Prefect", "Zaphod Beeblebrox", DateTime.Now, 40),
                new Mail("Zeus", "Odin", new DateTime(30, 4, 16), true)
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
                    Console.WriteLine("sender: " + item.GetSender());
                    Console.WriteLine("recipient: " + item.GetRecipient());
                    Console.WriteLine("postage date: " + item.GetPostageDate().Date.ToString("dd-MM-yyyy"));
                    Console.WriteLine("arrival time: " + item.CalculateArrivalTime());
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

                var postBox = new PostBox("Ankh-Morpork", items);

                Console.WriteLine(postBox.GetItem(0));
                Console.WriteLine(postBox.GetItem(1));
                Console.WriteLine(postBox.GetItem(2));
                Console.WriteLine(postBox.GetItemsCount());
                Console.WriteLine();

                Console.WriteLine(postBox.GetItem(6));
                Console.WriteLine();

                postBox.Print();
                Console.Write("\n\n");
            }
#endif

            //Stage 3 - 1 point
#if STAGE03
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Stage three\n");
                Console.ForegroundColor = ConsoleColor.White;
                var postOffice = new PostOffice(3);
                Console.WriteLine(postOffice.AddPostBox(new PostBox("The Restaurant at the End of the Universe",
                    new[] {items[2]})));
                Console.WriteLine(postOffice.GetPostBoxesCount());
                Console.WriteLine(postOffice.AddPostBox(new PostBox("Gondolin", new[] {items[1], items[3]})));
                Console.WriteLine(postOffice.GetPostBoxesCount());
                Console.WriteLine(postOffice.AddPostBox(new PostBox("Kaladan", new[] {items[4]})));
                Console.WriteLine(postOffice.GetPostBoxesCount());
                Console.WriteLine();
                Console.WriteLine(postOffice.AddPostBox(new PostBox("Eberron", null)));
                Console.WriteLine(postOffice.GetPostBoxesCount());
                Console.WriteLine();
                postOffice.Print();
                Console.Write("\n\n");
            }
#endif
        }
    }
}