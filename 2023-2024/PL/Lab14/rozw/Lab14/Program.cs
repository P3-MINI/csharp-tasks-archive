namespace Lab14;

public class Program
{
    /// <summary>
    /// Zadanie 1 (0.5 pkt)
    /// Znajdź osoby (w kolejności alfabetycznej według nazwiska, a następnie imienia), które nie zrobiły jeszcze żadnych zakupów.
    /// Zwróć jedynie ID, imię i nazwisko, bez ich adresu.
    /// Wskazówka: klauzula join...into, metoda Count()
    /// </summary>
    public static IEnumerable<(uint ID, string FirstName, string LastName)> GetCustomersWithoutOrders(Database database)
    {
        return from customer in database.Customers
               join order in database.Orders on customer.CustomerId equals order.CustomerId into CustomerOrders
               where CustomerOrders.Count() == 0
               orderby customer.LastName, customer.FirstName
               select (customer.CustomerId, customer.FirstName, customer.LastName);
    }

    /// <summary>
    /// Zadanie 2 (0.5 pkt)
    /// Na podstawie daty rejestracji, wybierz te osoby, które zarejestrowały się co najmniej N dni temu i zwróć je w kolejności od najdawniejszej rejestracji. 
    /// Załóż, że daty rejestracji są przechowywane w czasie uniwersalnym (UTC).
    /// Wymaganie: nie liczyć kilka razy tego samego (można użyć klauzuli let)
    /// </summary>
    public static IEnumerable<(string FirstName, string LastName, int Days)> GetCustomersRegisteredAtLeastNDaysAgo(Database database, int daysAgo)
    {
        return from customer in database.Customers
               let Days = (int)(DateTime.UtcNow - customer.RegistrationDate).TotalDays
               where Days > daysAgo
               orderby Days descending
               select (customer.FirstName, customer.LastName, Days);
    }

    /// <summary>
    /// Zadanie 3 (1.0 pkt)
    /// Znajdź 10 klientów, którzy jako pierwsi założyli zamówienie.
    /// Zwróć informacje o ID klienta, dacie pierwszego zamówienia oraz liczbie dni która upłynęła pomiędzy rejestracją a pierwszym zamówieniem.
    /// </summary>
    public static IEnumerable<(uint ID, DateTime FirstOrderDate, double DaysFromRegistration)> GetCustomersFirstOrderInfo(Database database)
    {
        return (from order in database.Orders
                group order.Date by order.CustomerId into order2
                join customer in database.Customers on order2.Key equals customer.CustomerId
                let FirstOrderDate = order2.Min()
                orderby FirstOrderDate
                select (customer.CustomerId, FirstOrderDate, (FirstOrderDate - customer.RegistrationDate).TotalDays)).Take(10);
    }

    /// <summary>
    /// Zadanie 4 (1.0 pkt)
    /// Wyznaczyć 5 przedmiotów z największą liczbą łącznie zamówionych sztuk.
    /// Wynik zwrócić w kolejności zaczynając od tych z największą liczbą.
    /// </summary>
    public static IEnumerable<(string ProductName, long TotalCount)> GetMostOrderedProducts(Database database)
    {
        return (from productOrder in database.OrderProducts
                group productOrder.Quantity by productOrder.ProductId into productsQuantity
                join product in database.Products on productsQuantity.Key equals product.ProductId
                let TotalCount = productsQuantity.Sum(x => x)
                orderby TotalCount descending
                select (product.Name, TotalCount)).Take(5);
    }

    /// <summary>
    /// Zadanie 5 (2.0 pkt)
    /// Wyznaczyć dla każdej osoby całkowitą kwotę jaką wydała w sklepie, ilość zamówień oraz ile wydała średnio na zamówienie.
    /// Wynik zwrócić w kolejności alfabetycznej według nazwiska, a następnie imienia klienta.
    /// </summary>
    public static IEnumerable<(string FirstName, string LastName, decimal TotalExpenses, int OrderCount, decimal AvgExpenses)> GetCustomersExpenses(Database database)
    {
        return from order in database.Orders
               join orderProduct in database.OrderProducts on order.OrderId equals orderProduct.OrderId
               join product in database.Products on orderProduct.ProductId equals product.ProductId
               let productCost = orderProduct.Quantity * product.Price
               group productCost by order.CustomerId into customerOrders
               let totalExpenses = customerOrders.Sum()
               let orderCount = customerOrders.Count()
               let avgExpenses = totalExpenses / orderCount
               join customer in database.Customers on customerOrders.Key equals customer.CustomerId
               orderby customer.LastName, customer.FirstName
               select (customer.FirstName, customer.LastName, totalExpenses, orderCount, avgExpenses);
    }

    private static void Main()
    {
        Database database = Database.CreateDatabase();

        Console.WriteLine(" --==== Zadanie 1 (0.5 pkt) ====--");
        foreach (var record in GetCustomersWithoutOrders(database))
            Console.WriteLine("{0,2} {1,10} {2,10}",
                record.ID, record.FirstName, record.LastName);


        Console.WriteLine("\n --==== Zadanie 2 (0.5 pkt) ====--");
        foreach (var record in GetCustomersRegisteredAtLeastNDaysAgo(database, 500))
            Console.WriteLine("{0,10} {1,10} {2,6:0.0}",
                record.LastName, record.FirstName, record.Days);


        Console.WriteLine("\n --==== Zadanie 3 (1.0 pkt) ====--");
        foreach (var record in GetCustomersFirstOrderInfo(database))
            Console.WriteLine("{0,3} {1} {2,6:0.0}",
                record.ID, record.FirstOrderDate, record.DaysFromRegistration);


        Console.WriteLine("\n --==== Zadanie 4 (1.0 pkt) ====--");
        foreach (var record in GetMostOrderedProducts(database))
            Console.WriteLine("{0,15} {1,3}",
                record.ProductName, record.TotalCount);


        Console.WriteLine("\n --==== Zadanie 5 (2.0 pkt) ====--");
        foreach (var record in GetCustomersExpenses(database))
            Console.WriteLine("{0,10} {1,-10}: {2,2} {3,10:0.00} {4,10:0.00}",
                record.FirstName, record.LastName, record.OrderCount, record.TotalExpenses, record.AvgExpenses);
    }
}