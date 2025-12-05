#define STAGE01
#define STAGE02
#define STAGE03
#define STAGE04

using Lab04.Models;
using Lab04.Services;
using Lab04.Services.Validators;
using System.Globalization;

namespace Lab04;

internal class Program
{
    static void Main(string[] args)
    {
#if STAGE01
        Console.WriteLine("Stage01:");

        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

        var customers = new Customer[] {
            new("John", "Doe", "123123123", "john.doe@pw.edu.pl", [4.5, 3.1, 5.0]),
            new("Jane", "Smith", "456456456", "jane.smith@gmail.com", [ 2.0 ]),
            new("Bob", "Brown", "789789789", "bobby@personal.com", [])
        };

        Array.ForEach(customers, Console.WriteLine);
        Console.WriteLine();

        var names = new string[] { "John", "Alice", "Bob12", "Bob Charlie" };
        var phoneNumbers = new string[] { "1234567890", "1231231234", "12345678", "123a56789" };
        var emailAddresses = new string[] { "john.doe@pw.edu.pl", "jane.smith@gmail.com", "bobbypersonal.com" };

        var testValidators = (Validator validator, string[] values) =>
        {
            Array.ForEach(values, v => Console.WriteLine($"{v}: {validator.Validate(v)}"));
            Console.WriteLine();
        };

        testValidators(new NameValidator(), names);
        testValidators(new PhoneNumberValidator(), phoneNumbers);
        testValidators(new EmailAddressValidator(), emailAddresses);

        var content = File.ReadAllText("Data/customers.csv");
        var parser = new CsvParser(
            new NameValidator(),
            new PhoneNumberValidator(),
            new EmailAddressValidator()
        );

        customers = parser.ParseCustomers(content);
        Console.WriteLine();
        Array.ForEach(customers, Console.WriteLine);
        Console.WriteLine();

#endif // STAGE01

#if STAGE02
        Console.WriteLine("Stage02:");

        var package1 = new Package(3, 5.5);
        var package2 = new Package(4, 6.5);
        var package3 = new Package(5, 12.5);
        var package4 = new Package(5, 12.5);

        var package5 = package1 + package2;
        var (size, weight) = package5;
        Console.WriteLine($"size: {size}, weight: {weight}");

        Console.WriteLine($"{nameof(package3)} == {nameof(package4)}: {package3 == package4}");
        Console.WriteLine($"{nameof(package3)} == {nameof(package5)}: {package3 == package5}");

        var package6 = (Package)(10.5, 12.12); // explicit conversion

        var locations = new Location[]
        {
            new(-33.8688, 151.2093, "Sydney", CultureInfo.GetCultureInfo("en-AU")),
            new(39.9042, 116.4074, "Warsaw", CultureInfo.GetCultureInfo("pl-PL")),
            new(52.5200, 13.4050, "Berlin", CultureInfo.GetCultureInfo("de-DE")),
            new(19.4326, -99.1332, "Mexico City", CultureInfo.GetCultureInfo("es-MX"))
        };

        Array.ForEach(locations, Console.WriteLine);
        Console.WriteLine();

        Console.WriteLine($"locations[0] - locations[1] = {locations[0] - locations[1]}");
        Console.WriteLine();
#endif //STAGE02

#if STAGE03
        Console.WriteLine("Stage03:");

        Console.WriteLine("Priority:");
        foreach (var p in Enum.GetValues(typeof(Priority)))
        {
            Console.WriteLine($"{p} = {(int)p}");
        }
        Console.WriteLine();

        var package = new Package(3, 100)
        {
            Source = new Location(5, 4, "Location1", new("en-EN")),
            Destination = new Location(3, 2, "Location2", new("en-EN")),
            ShippedAt = DateTime.Now.AddDays(-2),
            DeliveredAt = DateTime.Now.AddDays(2),
            Priority = Priority.Standard | Priority.Fragile
        };

        Console.WriteLine($"{nameof(package.Cost)}: {package.Cost:N2}");
        Console.WriteLine($"{nameof(package.DeliverySpeed)}: {package.DeliverySpeed:N2}");
        Console.WriteLine();

#endif // STAGE03

#if STAGE04
        Console.WriteLine("Stage04:");

        var packageManager = new PackageManager();
        for (var i = 20; i > 0; i--, packageManager.CreatePackage()) ;

        packageManager.MakeReport();
        Console.WriteLine();
        
        var displayPackage = (Package p) => Console.WriteLine($"({p.Size:N2}, {p.Weight:N2})");

        Array.ForEach(packageManager[DateTime.Now.AddDays(-5), DateTime.Now.AddDays(5)], displayPackage);
        Console.WriteLine();
        Array.ForEach(packageManager[..^1], displayPackage);

#endif // STAGE04
        Console.ReadKey();
    }
}