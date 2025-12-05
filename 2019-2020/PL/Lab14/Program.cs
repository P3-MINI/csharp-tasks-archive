using System;
using System.Linq;

namespace Lab14
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Database database = Database.GetInstance();

            // 1. Wypisać osoby (w kolejności alfabetycznej według nazwiska, a następnie imienia), które nie mają karty stałego klienta (0.5 pkt)
            // Wskazówka: klauzula join...into, metoda Count()
            Console.WriteLine("******Zadanie 1 (0.5 pkt)******");

            var result1 = from customer in database.Customers
                          join card in database.LoyaltyCards on customer.CustomerId equals card.CustomerId into CustomerCard
                          where CustomerCard.Count() == 0
                          orderby customer.LastName, customer.FirstName
                          select new { customer.FirstName, customer.LastName };

            foreach (var element in result1)
                Console.WriteLine("{0} {1}", element.LastName, element.FirstName);

            Console.WriteLine();
            Console.WriteLine("Powinno być:");
            Console.WriteLine("Bąk Magdalena");
            Console.WriteLine("Bąk Marek");
            Console.WriteLine("Wiśniewski Artur");

            // 2. Na podstawie numeru PESEL wybrać osoby, które rocznikowo mają więcej niż 29 lat. Należy wyświetlić te osoby od najstarszej do najmłodszej (0.5 pkt)
            // Wymaganie: nie liczyć kilka razy tego samego (można uzyć klauzuli let)
            // Wskazówka: Numer PESEL od 2000 roku różni się tym, że do miesiąca dodawana jest wartość 20
            Console.WriteLine();
            Console.WriteLine("******Zadanie 2 (0.5 pkt)******");

            var result2 = from customer in database.Customers
                          let Age = (int.Parse(customer.PESEL.Substring(2, 2)) > 20 ? 20 : 120) - int.Parse(customer.PESEL.Substring(0, 2))
                          where Age > 29
                          orderby Age descending
                          select new { customer.FirstName, customer.LastName, Age };

            foreach (var element in result2)
                Console.WriteLine("{0} {1} {2}", element.LastName, element.FirstName, element.Age);

            Console.WriteLine();
            Console.WriteLine("Powinno być:");
            Console.WriteLine("Kowalski Jan 117");
            Console.WriteLine("Marciniak Jerzy 35");
            Console.WriteLine("Antoniewski Michał 32");

            // 3. Wyznaczyć 3 osoby, które korzystają z usług sklepu najdłużej (1.0 pkt)
            Console.WriteLine();
            Console.WriteLine("******Zadanie 3 (1.0 pkt)******");

            var result3 = from order in database.Orders
                          group order.Date by order.CustomerId
                          into order2
                          join customer in database.Customers on order2.Key equals customer.CustomerId
                          let Date = order2.Min()
                          orderby Date
                          select new { customer.CustomerId, customer.FirstName, customer.LastName, Date };

            foreach (var element in result3.Take(3))
                Console.WriteLine("{0} {1} {2}", element.LastName, element.FirstName, element.Date.ToShortDateString());

            Console.WriteLine();
            Console.WriteLine("Powinno być:");
            Console.WriteLine("Nowak Andrzej 02.01.2017");
            Console.WriteLine("Antoniewski Michał 22.03.2017");
            Console.WriteLine("Kowalska Aneta 12.03.2018");

            //4. Wyznaczyć 5 przedmiotów z największą liczbą łącznie zamówionych sztuk (1.0 pkt)
            Console.WriteLine();
            Console.WriteLine("******Zadanie 4 (1.0 pkt)******");
            var result = from productOrder in database.OrderProducts
                         group productOrder.Quantity by productOrder.ProductId
                         into productsQuantity
                         join product in database.Products on productsQuantity.Key equals product.ProductId
                         let TotalCount = productsQuantity.Sum(x => x)
                         orderby TotalCount descending
                         select new { product.Name, TotalCount };

            foreach (var element in result.Take(5))
                Console.WriteLine("{0} {1}", element.Name, element.TotalCount);

            Console.WriteLine();
            Console.WriteLine("Powinno być:");
            Console.WriteLine("Krzesło gaminigowe 21");
            Console.WriteLine("Marker 11");
            Console.WriteLine("Płyta CD 10");
            Console.WriteLine("Długopis 10");
            Console.WriteLine("Monitor 5");

            // 5. Wyznaczyć dla każdej osoby całkowitą kwotę jaką wydała w sklepie (należy wziąć pod uwagę zniżkę przysługującą osobom z kartą stałego klienta),
            //    wypisać klientów w kolejności alfabetycznej według nazwiska, a następnie imienia (2.0 pkt)
            Console.WriteLine();
            Console.WriteLine("******Zadanie 5 (2.0 pkt)******");

            var result5 = from order in database.Orders
                          join orderProduct in database.OrderProducts on order.OrderId equals orderProduct.OrderId
                          join product in database.Products on orderProduct.ProductId equals product.ProductId
                          let productCost = orderProduct.Quantity * product.Price
                          group productCost by order.CustomerId
                          into customerOrders
                          select new { customerId = customerOrders.Key, total = customerOrders.Sum() }
                          into customerExpenses
                          let card = database.LoyaltyCards.FirstOrDefault(card => card.CustomerId == customerExpenses.customerId)
                          let priceMultiplier = 1.0 - (card?.Discount ?? 0)
                          join customer in database.Customers on customerExpenses.customerId equals customer.CustomerId
                          orderby customer.LastName, customer.FirstName
                          select new { customer.LastName, customer.FirstName, TotalExpenses = customerExpenses.total * priceMultiplier };

            foreach (var element in result5)
                Console.WriteLine("{0} {1} {2}", element.LastName, element.FirstName, element.TotalExpenses);


            //var result5 = from productOrder in database.OrderProducts
            //              join product in database.Products on productOrder.ProductId equals product.ProductId
            //              let ProductPrize = productOrder.Quantity * product.Price
            //              group ProductPrize by productOrder.OrderId
            //              into OrderProducts
            //              join order in database.Orders on OrderProducts.Key equals order.OrderId
            //              let TotalPrize = OrderProducts.Sum()
            //              group TotalPrize by order.CustomerId
            //              into CustomerOrders
            //              join customer in database.Customers on CustomerOrders.Key equals customer.CustomerId
            //              join card in database.LoyaltyCards on CustomerOrders.Key equals card.CustomerId into CustomerCards
            //              let CustomerCard = CustomerCards.FirstOrDefault()
            //              let discount = CustomerCard != null ? CustomerCard.Discount : 0
            //              let TotalAmount = (1 - discount) * CustomerOrders.Sum()
            //              orderby customer.LastName, customer.FirstName
            //              select new { customer.FirstName, customer.LastName, TotalAmount };

            //foreach (var element in result5)
            //    Console.WriteLine("{0} {1} {2}", element.LastName, element.FirstName, element.TotalAmount);

            Console.WriteLine();
            Console.WriteLine("Powinno być:");
            Console.WriteLine("Antoniewski Michał 1053");
            Console.WriteLine("Bąk Magdalena 2912");
            Console.WriteLine("Bąk Marek 3000");
            Console.WriteLine("Czerwińska Justyna 2593,5");
            Console.WriteLine("Kowalska Aneta 212,5");
            Console.WriteLine("Kowalski Jan 3654");
            Console.WriteLine("Marciniak Jerzy 13600");
            Console.WriteLine("Nowak Alina 1400");
            Console.WriteLine("Nowak Andrzej 2550");
            Console.WriteLine("Wiśniewski Artur 2025");

            Console.WriteLine();
        }
    }
}
