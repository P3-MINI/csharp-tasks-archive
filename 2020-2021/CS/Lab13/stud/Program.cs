//#define STAGE1
//#define STAGE2
//#define STAGE3

using System;
using System.IO;

namespace Lab13
{

    public interface IEntity
    {
        int Id { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // delete old log file if exists
            if (File.Exists("Log.txt"))
                File.Delete("Log.txt");

            // remove old database files if exist
            if (Directory.Exists("Data"))
                Directory.Delete("Data", true);

            var db = new Database("Data");

#if STAGE1

            Console.WriteLine("\nSTAGE 1 - Add/Get tests (3p)\n");
            Console.WriteLine("Basic Add/Get tests");

            var user1 = new User { Id = 1, FirstName = "Bill", LastName = "Gates" };
            db.Add(user1);

            var product1 = new Product { Id = 1, Name = "Computer" };
            db.Add(product1);

            var order1 = new Order(user1, product1, 2) { Id = 1 };
            db.Add(order1);

            var user2 = new User { Id = 2, FirstName = "Steve", LastName = "Jobs" };
            db.Add(user2);

            var product2 = new Product { Id = 2, Name = "iPhone" };
            db.Add(product2);

            var order2 = new Order(user1, product2, 20) { Id = 2 };
            db.Add(order2);

            var user1Deserialized = db.Get<User>(1);
            var user2Deserialized = db.Get<User>(2);
            var product1Deserialized = db.Get<Product>(1);
            var product2Deserialized = db.Get<Product>(2);
            var order1Deserialized = db.Get<Order>(1);
            var order2Deserialized = db.Get<Order>(2);

            Console.WriteLine(user1Deserialized.ToString());
            Console.WriteLine(CompareUser(user1Deserialized, user1) ? "OK" : "FAIL");

            Console.WriteLine(user2Deserialized.ToString());
            Console.WriteLine(CompareUser(user2Deserialized, user2) ? "OK" : "FAIL");

            Console.WriteLine(product1Deserialized.ToString());
            Console.WriteLine(CompareProduct(product1Deserialized, product1) ? "OK" : "FAIL");

            Console.WriteLine(product2Deserialized.ToString());
            Console.WriteLine(CompareProduct(product2Deserialized, product2) ? "OK" : "FAIL");

            Console.WriteLine(order1Deserialized.ToString());
            Console.WriteLine(CompareOrder(order1Deserialized, order1) ? "OK" : "FAIL");

            Console.WriteLine(order2Deserialized.ToString());
            Console.WriteLine(CompareOrder(order2Deserialized, order2) ? "OK" : "FAIL");

            Console.WriteLine("Add existing object test");
            ThrowTest(() => db.Add(order1));

            Console.WriteLine("Assign next Id value if 0 test");
            var order3 = new Order(user1, product1, 30);
            db.Add(order3);
            var order3Deserialized = db.Get<Order>(3);
            Console.WriteLine(CompareOrder(order3Deserialized, order3) ? "OK" : "FAIL");

#endif
#if STAGE2

            Console.WriteLine("\nSTAGE 2 - Update test (1p)\n");

            Console.WriteLine("Basic update test");
            order1.Amount = 11;
            db.Update(order1);

            order1.Amount = 12;
            db.Update(order1);

            order1Deserialized = db.Get<Order>(1);
            Console.WriteLine(order1Deserialized.ToString());
            Console.WriteLine(CompareOrder(order1Deserialized, order1) ? "OK" : "FAIL");

            Console.WriteLine("Update object without Id test");
            ThrowTest(() => db.Update(new Order(user1, product1, 10)));

            Console.WriteLine("Update not persistent object test");
            ThrowTest(() => db.Update(new Order(user1, product1, 10) {Id = 50} ));

#endif
#if STAGE3

            Console.WriteLine("\nStage 3 - Delete test (1p)\n");

            Console.WriteLine("Basic delete");
            db.Delete<Order>(2);
            ThrowTest(() => db.Get<Order>(2));

            Console.WriteLine("Delete not existing object test");
            ThrowTest(() => db.Delete<Order>(50));

#endif
        }

        private static bool CompareUser(User deserialized, User original)
        {
            return deserialized.Id == original.Id &&
                deserialized.FirstName == original.FirstName &&
                deserialized.LastName == original.LastName;
        }

        private static bool CompareProduct(Product deserialized, Product original)
        {
            return deserialized.Id == original.Id &&
                deserialized.Name == original.Name;
        }

        private static bool CompareOrder(Order deserialized, Order original)
        {
            return deserialized.Id == original.Id &&
                deserialized.Amount == original.Amount &&
                deserialized.UserId == original.UserId &&
                deserialized.ProductId == original.ProductId;
        }

        private static void ThrowTest(Action action)
        {
            try
            {
                action();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("FAIL");
                Console.ResetColor();
            }
            catch (Exception)
            {
                Console.WriteLine("OK");
            }
        }
    }

}