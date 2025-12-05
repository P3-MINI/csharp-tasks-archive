using Lab04.Models;
using System.Globalization;

namespace Lab04.Data;
public static class Repository
{
    public static Location DrawLocation(Random random)
    {
        var index = random.Next(0, _locations.Length);
        return _locations[index];
    }

    public static Customer DrawCustomer(Random random)
    {
        var index = random.Next(0, _customers.Length);
        return _customers[index];
    }

    private static Location[] _locations = [
        new(34.0522, -118.2437, "Los Angeles", CultureInfo.GetCultureInfo("en-US")),
        new(48.8566, 2.3522, "Paris", CultureInfo.GetCultureInfo("fr-FR")),
        new(51.5074, -0.1278, "London", CultureInfo.GetCultureInfo("en-GB")),
        new(40.7128, -74.0060, "New York", CultureInfo.GetCultureInfo("en-US")),
        new(-33.8688, 151.2093, "Sydney", CultureInfo.GetCultureInfo("en-AU")),
        new(39.9042, 116.4074, "Warsaw", CultureInfo.GetCultureInfo("pl-PL")),
        new(52.5200, 13.4050, "Berlin", CultureInfo.GetCultureInfo("de-DE")),
        new(19.4326, -99.1332, "Mexico City", CultureInfo.GetCultureInfo("es-MX"))
    ];

    private static Customer[] _customers = [
        new("John", "Doe", "123456789", "john.doe@example.com", [4.2, 3.9, 4.5]),
        new("Jane", "Smith", "234567901", "jane.smith@example.com", [5.0, 4.8, 4.9]),
        new("Carlos", "Garcia", "345679012", "carlos.garcia@example.com", [3.8, 4.0, 4.1]),
        new("Sakura", "Tanaka", "456890123", "sakura.tanaka@example.com", [4.5, 4.7, 4.6]),
        new("Anna", "Petrov", "567891234", "anna.petrov@example.com", [4.1, 4.2, 4.0]),
        new("Michael", "Brown", "678012345", "michael.brown@example.com", [4.9, 4.8, 4.7]),
        new("Elena", "Rodriguez", "790123456", "elena.rodriguez@example.com", [3.7, 3.9, 4.0]),
        new("Ahmed", "Hassan", "890134567", "ahmed.hassan@example.com", [4.3, 4.1, 4.2]),
        new("Emily", "Johnson", "901345678", "emily.johnson@example.com", [4.6, 4.7, 4.8]),
        new("Chen", "Wei", "012345689", "chen.wei@example.com", [4.0, 4.1, 3.9])
    ];
}