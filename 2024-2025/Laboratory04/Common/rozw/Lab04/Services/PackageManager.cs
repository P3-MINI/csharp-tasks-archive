using Lab04.Data;
using Lab04.Models;
using System.Globalization;

namespace Lab04.Services;
public class PackageManager
{
    public List<Package> Packages { get; set; } = [];
    private Random Random { get; set; } = new(12345);

    public Package CreatePackage()
    {
        var size = 90 * Random.NextDouble() + 10;
        var weight = 90 * Random.NextDouble() + 10;
        var now = DateTime.Now;
        var shippedAt = now.AddDays(Random.Next(-10, -1));
        DateTime? deliveredAt = Random.NextDouble() < 0.25
            ? null
            : now.AddDays(Random.Next(1, 10));

        var package = new Package(size, weight)
        {
            ShippedAt = shippedAt,
            DeliveredAt = deliveredAt,
            Sender = Repository.DrawCustomer(Random),
            Recipient = Repository.DrawCustomer(Random),
            Source = Repository.DrawLocation(Random),
            Destination = Repository.DrawLocation(Random)
        };

        Packages.Add(package);
        return package;
    }

    public void MakeReport()
    {
        foreach (var package in Packages)
        {
            var sourceName = package.Source?.Name ?? "unknown";
            var destinationName = package.Destination?.Name ?? "unknown";
            var shippedAtFormatted = package.ShippedAt?.ToString("D", package.Source?.CultureInfo ?? CultureInfo.InvariantCulture);

            if (package.DeliveredAt != null)
            {
                var deliveredAtFormatted = package.DeliveredAt.Value.ToString("D", package.Destination?.CultureInfo ?? CultureInfo.InvariantCulture);
                Console.WriteLine($"{sourceName} (at {shippedAtFormatted}) => {destinationName} (at {deliveredAtFormatted}).");
            }
            else
            {
                Console.WriteLine($"{sourceName} (at {shippedAtFormatted}) => {destinationName} (not delivered yet).");
            }
        }
    }

    public Package[] this[Range range]
    {
        get
        {
            var (start, length) = range.GetOffsetAndLength(Packages.Count);
            var result = new Package[length];

            for (var i = start; i < start + length; i++)
            {
                result[i] = Packages[i];
            }

            return result;
        }
    }

    public Package[] this[DateTime from, DateTime to]
    {
        get
        {
            var result = new List<Package>();

            foreach (var p in Packages)
            {
                if(p.ShippedAt is not null &&
                    p.DeliveredAt is not null &&
                    from <= p.ShippedAt &&
                    p.DeliveredAt <= to)
                {
                    result.Add(p);
                }
            }

            return [.. result];
        }
    }
}