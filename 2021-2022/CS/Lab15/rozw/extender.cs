
static class Extender
{

public static T Neg<T>(this T x) where T: IComputable<T>
    {
    return default(T).Sub(x);
    }

public static T Horner<T>(this Polynomial<T> p, T x) where T: struct, IComputable<T>
    {
        T y;
        y=p[p.Degree];
        for ( int i=p.Degree-1 ; i>=0 ; --i )
        y=y.Mul(x).Add(p[i]);
        return y;    
    }

}

