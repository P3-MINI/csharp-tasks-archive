
using System;

class Polynomial<T> where T: struct, IComputable<T>
    {
    private T[] coef;

    public Polynomial(int n)
        {
        coef = new T[n+1];
        }

    public Polynomial(T[] tab)
        {
        coef = new T[tab.Length];
        for ( int i=0 ; i<tab.Length ; ++i )
            coef[i]=tab[i];
        }

    public int Degree
        {
        get
            {
            return coef.Length-1;
            }
        }

    public static Polynomial<T> operator +(Polynomial<T> p1, Polynomial<T> p2)
        {
        Polynomial<T> p = new Polynomial<T>(Math.Max(p1.Degree,p2.Degree));
        for ( int i=0 ; i<=p.Degree ; ++i )
            p[i]=p1[i].Add(p2[i]);
        return p;
        }

    public static Polynomial<T> operator *(Polynomial<T> p1, Polynomial<T> p2)
        {
        Polynomial<T> p = new Polynomial<T>(p1.Degree+p2.Degree);
        for ( int i=0 ; i<=p1.Degree ; ++i )
            for ( int j=0 ; j<=p2.Degree ; ++j )
                p[i+j]=p[i+j].Add(p1[i].Mul(p2[j]));
        return p;
        }

    public static Polynomial<T> operator -(Polynomial<T> p1)
        {
        Polynomial<T> p = new Polynomial<T>(p1.Degree);
        for ( int i=0 ; i<=p.Degree ; ++i )
            p[i]=p1[i].Neg();
        return p;
        }

    public T this[int i]
        {
        get
            {
            return i<=Degree?coef[i]:default(T);
            }
        set
            {
            if ( i>Degree )
                {
                T[] c = new T[i+1];
                for ( int j=0 ; j<=Degree ; ++j )
                    c[j]=coef[j];
                coef = c;
                }
            coef[i] = value;
            }
        }

    public System.Collections.Generic.IEnumerator<T> GetEnumerator()
        {
        foreach ( T x in coef )
            yield return x;
        }

    public override string ToString()
        {
        string s="";
        for ( int i=Degree ; i>0 ; --i )
            s+=coef[i]+"*x^"+i+" + ";
        return s+coef[0];
        }

    public Polynomial<T> Clone()
        {
        return new Polynomial<T>(coef);
        }

    }
