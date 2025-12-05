
using System;
using System.Threading.Tasks;

public class Matrix
    {

    private double[,] tab;
    private int size;

    public Matrix(int n)
        {
        tab = new double[n,n];
        size = n;
        }

    public static Matrix Random(int n)
        {
        Matrix m = new Matrix(n);
        Random r = new Random();
        for ( int i=0 ; i<n ; ++i )
            for ( int j=0 ; j<n ; ++j )
                m.tab[i,j] = r.NextDouble();
        return m;
        }

    public Matrix Mul(Matrix a)
        {
        if ( size!=a.size ) throw new ArgumentException("Incorrect martix size");
        Matrix m = new Matrix(size);
        for ( int i=0 ; i<size ; ++i )
            for ( int j=0 ; j<size ; ++j )
                for ( int k=0 ; k<size ; ++k )
                    m.tab[i,j] += tab[i,k]*a.tab[k,j];
        return m;
        }

    public Matrix MulParallel(Matrix a)
        {
        if ( size!=a.size ) throw new ArgumentException("Incorrect martix size");
        Matrix m = new Matrix(size);
        Parallel.For( 0, size, i=>
            {
            for ( int j=0 ; j<size ; ++j )
                for ( int k=0 ; k<size ; ++k )
                    m.tab[i,j] += tab[i,k]*a.tab[k,j];
            });
        return m;
        }

    public bool IsEqual(Matrix a)
        {
        if ( size!=a.size ) return false;
        for ( int i=0 ; i<size ; ++i )
            for ( int j=0 ; j<size ; ++j )
                if ( tab[i,j]!=a.tab[i,j] )
                    return false;
        return true;
        }

    public bool IsEqualParallel(Matrix a)
        {
        if ( size!=a.size ) return false;
        var res = Parallel.For( 0, size, (i,ls)=>
            {
            for ( int j=0 ; j<size ; ++j )
                if ( tab[i,j]!=a.tab[i,j] )
                    ls.Stop();
            });
        return res.IsCompleted;
        }

    }

class Test
    {

    const int N = 800;

    public static void Main()
        {
        DateTime t1, t2, t3, t4, t5;

        Matrix m1 = Matrix.Random(N);
        Matrix m2 = Matrix.Random(N);

        t1 = DateTime.Now;
        Matrix r1 = m1.Mul(m2);
        t2 = DateTime.Now;
        Matrix r2 = m1.MulParallel(m2);

        t3 = DateTime.Now;
        bool e1 = r1.IsEqual(r2);
        t4 = DateTime.Now;
        bool e2 = r1.IsEqualParallel(r2);

        t5 = DateTime.Now;
        Console.WriteLine($" Sequential matrix multiplication: {t2-t1}");
        Console.WriteLine($" Parallel matrix multiplication:   {t3-t2}");

        Console.WriteLine($" Sequential matrix comparison: {e1}, {t4-t3}");
        Console.WriteLine($" Parallel matrix comparison:   {e2}, {t5-t4}");

        Console.WriteLine();
        }

    }