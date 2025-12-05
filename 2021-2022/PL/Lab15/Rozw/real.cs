
struct Real : IComputable<Real>
    {
    private double val;
    private Real(double x) { val=x; }

    public static implicit operator Real(double x) { return new Real(x); }
    public static implicit operator double(Real x) { return x.val; }
    public Real Add(Real x) { return val+x.val; }
    public Real Sub(Real x) { return val-x.val; }
    public Real Mul(Real x) { return val*x.val; }
    public Real Div(Real x) { return val/x.val; }
    public override string ToString() { return val.ToString(); }
    }
