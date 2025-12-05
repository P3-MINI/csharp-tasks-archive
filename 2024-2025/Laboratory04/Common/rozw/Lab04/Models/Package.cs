using System;

namespace Lab04.Models;

public sealed class Package
{
    public readonly double Size;
    public readonly double Weight;
    public double Volume => Math.Pow(Size, 3);
    public Customer? Sender { get; set; }
    public Customer? Recipient { get; set; }
    public Location? Source { get; set; }
    public Location? Destination { get; set; }
    public DateTime? ShippedAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public Priority Priority { get; set; } = Priority.Standard;
    
    public decimal? Cost
    {
        get
        {
            if (Source is null || Destination is null ||
                ShippedAt is null || DeliveredAt is null)
                return null;

            var v = Destination - Source;

            var m = 50.0;
            if ((Priority & (Priority.Standard | Priority.Fragile)) == (Priority.Standard | Priority.Fragile))
                m = 100.0;
            if ((Priority & (Priority.Express | Priority.Fragile)) == (Priority.Express | Priority.Fragile))
                m = 200.0;

            return (decimal)(m * Math.Sqrt(v.x * v.x + v.y * v.y));
        }
    }

    public double? DeliverySpeed
    {
        get
        {
            if (Source is null || Destination is null ||
                ShippedAt is null || DeliveredAt is null)
                return null;

            var v = Destination - Source;
            var hours = (DeliveredAt - ShippedAt)?.TotalHours;

            return Math.Sqrt(v.x * v.x + v.y * v.y) / hours;
        }
    }

    public Package(double size, double weight)
    {
        Size = size;
        Weight = weight;
    }

    public void Deconstruct(out double size, out double weight)
    {
        weight = Weight;
        size = Size;
    }

    public static Package operator +(Package lhs, Package rhs)
    {
        var size = Math.Pow(lhs.Volume + rhs.Volume, 1.0 / 3);
        var weight = lhs.Weight + rhs.Weight;
        return new Package(size, weight);
    }

    public static bool operator ==(Package lhs, Package rhs)
    {
        return lhs.Weight == rhs.Weight && lhs.Volume == rhs.Volume;
    }

    public static bool operator !=(Package lhs, Package rhs)
    {
        return !(lhs == rhs);
    }

    public override bool Equals(object? obj)
    {
        return obj is Package other && this == other;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Size, Weight);
    }

    public static explicit operator Package((double size, double weight) value)
    {
        return new Package(value.size, value.weight);
    }
}