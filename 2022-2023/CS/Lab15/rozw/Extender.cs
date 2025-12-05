using System;

static class ComputableExtender
{
    public static T Negation<T>(this T x) where T : IComputable<T>
    {
        return default(T).Subtraction(x);
    }
}

static class PolynomialExtender
{ 
    public static T Horner<T>(this Polynomial<T> p, T x) where T : struct, IComputable<T>
    {
        T value;

        value = p[p.Degree];

        for (int i = p.Degree - 1; i >= 0; --i)
            value = value.Multiply(x).Addition(p[i]);

        return value;
    }
}
