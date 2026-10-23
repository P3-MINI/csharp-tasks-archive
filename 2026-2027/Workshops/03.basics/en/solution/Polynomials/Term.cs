namespace Polynomials;

public readonly struct Term : IPolynomial
{
    public float Coeff { get; }
    public int Power { get; }

    public Term(float coeff, int power)
    {
        Coeff = coeff;
        Power = power;
    }

    public int Degree => Power;

    public float Evaluate(float x) => Coeff * MathF.Pow(x, Power);

    public static Term operator *(Term term, float scalar) => new(term.Coeff * scalar, term.Power);

    public static Term operator *(float scalar, Term term) => new(term.Coeff * scalar, term.Power);

    public static Term operator *(Term a, Term b) => new(a.Coeff * b.Coeff, a.Power + b.Power);

    public override string ToString() => $"{Coeff}x^{Power}";
}
