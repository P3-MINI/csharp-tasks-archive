using System.Globalization;

namespace Lab04.Models;

public sealed class Location
{
    public double X { get; }
    public double Y { get; }
    public string Name { get; }
    public CultureInfo CultureInfo { get; }

    public Location(double x, double y, string name, CultureInfo cultureInfo)
    {
        X = x;
        Y = y;
        Name = name;
        CultureInfo = cultureInfo;
    }

    public static (double x, double y) operator -(Location lhs, Location rhs)
    {
        return (lhs.X - rhs.X, lhs.Y - rhs.Y);
    }

    public override string ToString()
    {
        return string.Format(CultureInfo, "{0} at ({1,12:N4}; {2,12:N4})", Name, X, Y);
    }
}