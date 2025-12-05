using System.Numerics;

namespace Lab15_Retake;

// Points: 2
// Create generic 'Polynomial' class that will implement + and - operators (use specific interfaces from above).
// Use 'Number' type to store the coefficients as a public readonly property.
// Implement readonly property 'Degree' that will return a degree of polynomial (do not store the value).
// Constructor that takes a degree and initializes coefficients with default values.
// Constructor that takes variable amount of 'Number' type values representing coefficients.
// Indexer that returns/assigns value at given index, get should return default value while set should throw argument exception if out of bounds.
public class Polynomial<T> :
    IAdditionOperators<Polynomial<T>, Polynomial<T>, Polynomial<T>>,
    ISubtractionOperators<Polynomial<T>, Polynomial<T>, Polynomial<T>>
    where T : INumber<T>
{
    public T[] Coefficients { get; }
    public int Degree => Coefficients.Length - 1;

    public Polynomial(int degree)
    {
        Coefficients = new T[degree + 1];
    }
    public Polynomial(params T[] coefficients) : this(coefficients.Length - 1)
    {
        for (int i = 0; i < coefficients.Length; i++)
            Coefficients[i] = coefficients[i];
    }
    public T this[int index]
    {
        get => index < Coefficients.Length ? Coefficients[index] : T.Zero;
        set
        {
            if (index < 0 || index >= Coefficients.Length)
                throw new ArgumentException($"Index should be more than 0 and less than {Coefficients.Length}");
            Coefficients[index] = value;
        }
    }
    public static Polynomial<T> operator +(Polynomial<T> left, Polynomial<T> right)
    {
        Polynomial<T> newPolynomial = new Polynomial<T>(Math.Max(left.Degree, right.Degree));
        for (int i = 0; i <= newPolynomial.Degree; i++)
            newPolynomial[i] = left[i] + right[i];
        return newPolynomial;
    }
    public static Polynomial<T> operator -(Polynomial<T> left, Polynomial<T> right)
    {
        Polynomial<T> newPolynomial = new Polynomial<T>(Math.Max(left.Degree, right.Degree));
        for (int i = 0; i <= newPolynomial.Degree; i++)
            newPolynomial[i] = left[i] - right[i];
        return newPolynomial;
    }
}

// Points: 1
// Create extension method 'Evaluate' for 'Polynomial' that calculates, using Horner method, value of given polynomial at given point.
public static class PolynomialExtension
{
    public static T Evaluate<T>(this Polynomial<T> polynomial, T x) where T : INumber<T>
    {
        T resultValue = polynomial[polynomial.Degree];
        for (int i = polynomial.Degree - 1; i >= 0; i--)
            resultValue = resultValue * x + polynomial[i];
        return resultValue;
    }
}
