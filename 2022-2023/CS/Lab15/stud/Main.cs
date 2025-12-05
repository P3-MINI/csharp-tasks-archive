// #define STAGE_1
// #define STAGE_2
// #define STAGE_3

using System;

class Lab15Retake
{
    static void Main()
    {
#if STAGE_1
        Real[] tab2 = { 2, 3, 4 };
        Real[] tab3 = { -2, -1, 0, 6 };
        Real[] tabh = { 0.5, -1.0, 0.75 };
#endif
#if STAGE_2
        Polynomial<Real> p1 = new Polynomial<Real>(1);
        Polynomial<Real> p2 = new Polynomial<Real>(tab2);
        Console.WriteLine(p1);            //   0*x^1 + 0
        Console.WriteLine(p2);            //   4*x^2 + 3*x^1 + 2
        Console.WriteLine();

        p1 = p2;
        p1[2] = 5;
        Console.WriteLine(p1);            //   5*x^2 + 3*x^1 + 2
        Console.WriteLine(p2);            //   5*x^2 + 3*x^1 + 2
        Console.WriteLine(p1[4]);         //   0
        p1 = p2.Clone();
        Console.WriteLine(p1);            //   7*x^4 + 0*x^3 + 5*x^2 + 3*x^1 + 2
        Console.WriteLine(p2);            //   5*x^2 + 3*x^1 + 2
        Console.WriteLine();

        Polynomial<Real> p3 = new Polynomial<Real>(tab3);
        p1 = p2 + p3;
        Console.WriteLine(p1);            //   6*x^3 + 5*x^2 + 2*x^1 + 0
        Console.WriteLine(p2);            //   5*x^2 + 3*x^1 + 2
        Console.WriteLine(p3);            //   6*x^3 + 0*x^2 + -1*x^1 + -2
        Console.WriteLine(-p3);           //  -6*x^3 + 0*x^2 + 1*x^1 + 2
        Console.WriteLine();

        foreach (Real r in p3)
            Console.WriteLine(r);   //  -2  -1  0  6
        Console.WriteLine();
#endif
#if STAGE_3
        foreach (Real r in p3)
            Console.WriteLine(r.Negation());   //  2  1  0  -6
        Console.WriteLine();

        Polynomial<Real> ph = new Polynomial<Real>(tabh);
        Console.WriteLine(ph.Horner(2.0)); //  1.5
        Console.WriteLine();
#endif
    }
}
