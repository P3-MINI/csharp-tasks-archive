using System;
using System.Text;

class Polynomial<T> where T : struct, IComputable<T>
{
    public T[] Coefficients { get; private set; }

    public Polynomial(int n)
    {
        Coefficients = new T[n + 1];
    }

    public Polynomial(T[] coefficients)
    {
        Coefficients = new T[coefficients.Length];

        for (int i = 0; i < coefficients.Length; ++i)
            Coefficients[i] = coefficients[i];
    }

    public int Degree
    {
        get
        {
            return Coefficients.Length - 1;
        }
    }

    public static Polynomial<T> operator +(Polynomial<T> polynomial1, Polynomial<T> polynomial2)
    {
        Polynomial<T> newPolynomial = new Polynomial<T>(Math.Max(polynomial1.Degree, polynomial2.Degree));

        for (int i = 0; i <= newPolynomial.Degree; ++i)
            newPolynomial[i] = polynomial1[i].Addition(polynomial2[i]);

        return newPolynomial;
    }

    public static Polynomial<T> operator -(Polynomial<T> p1)
    {
        Polynomial<T> newPolynomial = new Polynomial<T>(p1.Degree);

        for (int i = 0; i <= newPolynomial.Degree; ++i)
            newPolynomial[i] = p1[i].Negation();

        return newPolynomial;
    }

    public T this[int index]
    {
        get
        {
            return index < Coefficients.Length ? Coefficients[index] : default(T);
        }
        set
        {
            Coefficients[index] = value;
        }
    }

    public System.Collections.Generic.IEnumerator<T> GetEnumerator()
    {
        foreach (T x in Coefficients)
            yield return x;
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();

        for (int i = Degree; i > 0; --i)
            stringBuilder.Append($"{Coefficients[i]}*x^{i} + ");

        stringBuilder.Append(Coefficients[0]);

        return stringBuilder.ToString();
    }

    public Polynomial<T> Clone()
    {
        return new Polynomial<T>(Coefficients);
    }
}
