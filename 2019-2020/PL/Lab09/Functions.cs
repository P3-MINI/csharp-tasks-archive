
using System;
using System.Collections.Generic;

namespace Lab09
{

public static class Functions
    {

    public static Func<double,double> Constant(double c)
        {
        return x => c;
        }

    public static Func<double,double> Identity()
        {
        return x => x;
        }

    public static Func<double,double> Polynomial(params double[] coeff)
        {
        return x =>
            {
            if ( coeff.Length==0 )
                return 0;
            double y = coeff[0];
            for ( int i=1; i<coeff.Length ; ++i )
                y = y*x+coeff[i];
            return y;
            };
        }

    }

public static class Transformations
    {

    public static Func<double,double> Derivative(Func<double,double> f, double h=0.000001)
        {
        return x => (f(x+h)-f(x-h))/(2.0*h);
        }

    public static Func<double,double> Max(params Func<double,double>[] f)
        {
        return x =>
            {
            double t;
            double y=f[0](x);
            for ( int i=1 ; i<f.Length ; ++i )
                {
                t = f[i](x);
                if ( y<t ) y = t;
                }
            return y;
            };
        }

    public static Func<double,double> Min(params Func<double,double>[] f)
        {
        return x =>
            {
            double t;
            double y=f[0](x);
            for ( int i=1 ; i<f.Length ; ++i )
                {
                t = f[i](x);
                if ( y>t ) y = t;
                }
            return y;
            };
        }

    public static Func<double,double> Compose(params Func<double,double>[] f)
        {
        return x =>
            {
            double y = f[f.Length-1](x);
            for ( int i=f.Length-1 ; i>0 ; y=f[--i](y) ) ;
            return y;
            };
        }

    }


public static class NumericalMethods
    {

    public static double Bisection(Func<double,double> f, double a, double b, double eps=0.00001)
        {
        double x, y, ya, yb;
        ya = f(a);
        if ( Math.Abs(ya)<eps )
            return a;
        yb = f(b);
        if ( Math.Abs(yb)<eps )
            return b;
        if ( ya*yb>0.0 )
            return double.NaN;
        if ( ya>0.0 )
            {
            x=a;
            a=b;
            b=x;
            }
        while ( true )
            {
            x = 0.5*(a+b);
            y = f(x);
            if ( Math.Abs(y)<eps )
                break;
            if ( y>0.0 )
                b = x;
            else
                a = x;
            }
        return x;
        }

    public static double Integral(Func<double,double> f, double a, double b, int n=100)
        {
        double h = (b-a)/n;
        double y = 0.5*(f(a)+f(b));
        double x;
        int i;
        for ( i=1, x=a+h ; i<n ; ++i, x+=h )
            y+=f(x);
        return y*h;
        }

    }

public class Iterations
    {
    Func<double,double> f;
    double init;
    int n;

    public Iterations(Func<double,double> _f, double _init, int _n)
        {
        f = _f;
        init = _init;
        n = _n;
        }

    public IEnumerator<double> GetEnumerator()
        {
        double x = init;
        for ( int i=0 ; i<n ; ++i )
            {
            yield return x;
            x = f(x);
            }
        }

    }

}
