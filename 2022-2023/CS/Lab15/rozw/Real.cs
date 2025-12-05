using System;

struct Real : IComputable<Real>
{
    public double Value { get; private set; }

    public Real(double x) { Value = x; }

    public static implicit operator Real(double x) { return new Real(x); }

    public static implicit operator double(Real x) { return x.Value; }
    public Real Addition(Real x) { return Value + x.Value; }
    public Real Multiply(Real x) { return Value * x.Value; }
    public Real Subtraction(Real x) { return Value - x.Value; }
    public override string ToString() { return Value.ToString(); }
}
