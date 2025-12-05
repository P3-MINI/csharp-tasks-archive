
namespace Lab09
{
using System;

public class Lab09
    {

    static void Main()
        {
        double y = 0.0;

        Console.WriteLine("\nEtap 1 (1.0 pkt)");

        // Przypisz do zmiennej y wartość funkcji będącej wynikiem wywołania funkcji Constant z parametrem 5 (czyli funkcji zwracającej zawsze 5) dla parametru równego 3
        y = Functions.Constant(5)(3);
        Console.WriteLine($"Funkcja o stałej wartosci 5 w punkcie 3:  {y}");

        // Przypisz do zmiennej y wartość funkcji będącej wynikiem wywołania funkcji Identity dla dla parametru równego -2
        y = Functions.Identity()(-2);
        Console.WriteLine($"Funkcja identycznosciowa w punkcie -2:  {y}");

        // Przypisz do zmiennej y wartość funkcji będącej wynikiem wywołania funkcji Polynomial z parametrami 3,0,-1 (czyli funkcji zwracającej wielomian 3x^2-1) dla parametru równego 2
        y = Functions.Polynomial(3,0,-1)(2);
        Console.WriteLine($"Wielomian 3x^2-1 w punkcie 2:  {y}");

        // Przypisz do zmiennej y wartość funkcji będącej wynikiem wywołania funkcji Polynomial z parametrem 2 (czyli funkcji zwracającej wielomian 2 (stopnia 0)) dla parametru równego 5
        y = Functions.Polynomial(2)(5);
        Console.WriteLine($"Wielomian 2 (stopnia 0) w punkcie 5:  {y}");

        // Przypisz do zmiennej y wartość funkcji będącej wynikiem wywołania funkcji Polynomial bez parametów (czyli funkcji zwracającej wielomian 0 (stopnia 0)) dla parametru równego -1
        y = Functions.Polynomial()(-1);
        Console.WriteLine($"Wielomian 0 (stopnia 0) w punkcie -1:  {y}");

        Console.WriteLine("\nEtap 2 (2.0 pkt.)");

        // Przypisz do zmiennej y wartość funkcji będącej wynikiem wywołania funkcji Derivative z parametrem będącym wielomianem -3x^2+7x (użyj metody Polynomial!) dla x = 2
        y = Transformations.Derivative(Functions.Polynomial(-3,7,0))(2);
        Console.WriteLine($"Pochodna wielomianu -3x^2+7x w punkcie 2:  {y:0.000}");

        // Przypisz do zmiennej y wartość funkcji będącej wynikiem wywołania funkcji Derivative z będącym funkcją sinus (użyj standardowej funkcji) dla x = 2*Pi/3
        y = Transformations.Derivative(Math.Sin)(2*Math.PI/3);
        Console.WriteLine($"Pochodna funkcji sin w punkcie 2Pi/3:  {y:0.000}");

        // Zdefiniuj funkcję (delegację) relu okresloną zależnością relu(x) = max(x,0) korzystając z metod Identity, Constant i transformacji Max
        Func<double,double> relu = Transformations.Max(Functions.Identity(),Functions.Constant(0));

        // Przypisz do zmiennej y wartość funkcji relu dla x =-3
        y = relu(-3);
        Console.WriteLine($"Funkcja relu w punkcie -3:  {y}");

        // Przypisz do zmiennej y wartość funkcji relu dla x = 0.5
        y = relu(0.5);
        Console.WriteLine($"Funkcja relu w punkcie 0.5:  {y}");

        // Przypisz do zmiennej y wartość funkcji będącej wynikiem zastosowania transformacji Max dla funkcji sin, cos i funkcji o stałej wartości 0.5 w punkcie pi/2
        y = Transformations.Max(Math.Sin,Math.Cos,x=>0.5)(Math.PI/2);
        Console.WriteLine($"Funkcja max(sin,cos,0.5) w punkcie Pi/2:  {y}");

        // Przypisz do zmiennej y wartość funkcji będącej wynikiem zastosowania transformacji Min dla funkcji identycznościowej w punkcie 2 (minimum z jednej funkcji!)
        y = Transformations.Min(x=>x)(2);
        Console.WriteLine($"Funkcja min(x) w punkcie 2:  {y}");

        // Zdefiniuj funkcję (delegację) sat okresloną zależnością
        //    sat(x) =  1,  dla x>=1
        //    sat(x) =  x,  dla -1<x<1
        //    sat(x) = -1,  dla x<=-1
        // Wykorzystaj transformacje Max i Min oraz funkcje Identity i Constant (nie wolno jawnie używać notacji wyrażeń lambda ani metod anonimowych)
        Func<double,double> sat = Transformations.Min(Transformations.Max(Functions.Identity(),Functions.Constant(-1)),Functions.Constant(1));

        // Przypisz do zmiennej y wartość funkcji sat dla x = -3
        y = sat(-3);
        Console.WriteLine($"Funkcja sat w punkcie -3:  {y}");

        // Przypisz do zmiennej y wartość funkcji sat dla x = 0.5
        y = sat(0.5);
        Console.WriteLine($"Funkcja sat w punkcie 0.5:  {y}");

        // Przypisz do zmiennej y wartość funkcji sat dla x = 5
        y = sat(5);
        Console.WriteLine($"Funkcja sat w punkcie 5:  {y}");

        // Zdefiniuj funkcję (delegację) acos okresloną zależnością:  acos(x) = 10*cos((pi/180)*x+pi/2)
        // Wykorzystaj transformacje Compose oraz funkcję Polynomial i standardową metodę Math.Cos (nie wolno jawnie używać notacji wyrażeń lambda ani metod anonimowych)
        Func<double,double> acos = Transformations.Compose(Functions.Polynomial(10,0),Math.Cos,Functions.Polynomial(Math.PI/180,Math.PI/2));

        // Przypisz do zmiennej y wartość funkcji acos dla x = 90.0
        y = acos(90.0);
        Console.WriteLine($"Funkcja 10*cos((pi/180)*x+pi/2) w punkcie 90:  {y}");

        // Zdefiniuj funkcję (delegację) sin2 okresloną zależnością:  sin2(x) = sin^2(x)
        // Wykorzystaj transformacje Compose oraz funkcję Polynomial i standardową metodę Math.Sin (nie wolno jawnie używać notacji wyrażeń lambda ani metod anonimowych)
        Func<double,double> sin2 = Transformations.Compose(Functions.Polynomial(1,0,0),Math.Sin);

        // Przypisz do zmiennej y wartość funkcji sin2 dla x = pi/3
        y = sin2(Math.PI/3);
        Console.WriteLine($"Funkcja sin^2(x) w punkcie pi/3:  {y}");

        double z = 0.0;
        Console.WriteLine("\nEtap 3 (1.5 pkt.)");

        // Przypisz do zmiennej z miejsce zerowe funkcji x^2-1 na przedziale <0,2>
        // Wykorzystaj metodę Bisection (funkcję możesz zdefiniować w dowolny sposób)
        z = NumericalMethods.Bisection(x=>x*x-1,0,2);
        Console.WriteLine($"Miejsce zerowe funkcji x^2-1 na przedziale <0,2>:  {z}");

        // Przypisz do zmiennej z miejsce zerowe funkcji x*sin(x^3)-1 na przedziale <1,10>
        // Wykorzystaj metodę Bisection (funkcję możesz zdefiniować w dowolny sposób)
        z = NumericalMethods.Bisection(x=>x*Math.Sin(x*x*x)-1,1,10);
        Console.WriteLine($"Miejsce zerowe funkcji x*sin(x^3)-1 na przedziale <1,10>:  {z}");

        // Przypisz do zmiennej z miejsce zerowe funkcji x*sin(x^3)-1 na przedziale <5,7> i wyjaśnij wyniki
        // Wykorzystaj metodę Bisection (funkcję możesz zdefiniować w dowolny sposób)
        z = NumericalMethods.Bisection(x=>x*Math.Sin(x*x*x)-1,5,7);
        Console.WriteLine($"Miejsce zerowe funkcji x*sin(x^3)-1 na przedziale <5,7>:  {z}");

        // Przypisz do zmiennej y całkę oznaczoną funkcji sin^2(x)+cos^2(x) na przedziale <2,5>
        // Wykorzystaj metodę Integral (funkcję możesz zdefiniować w dowolny sposób)
        y = NumericalMethods.Integral(x=>{var s=Math.Sin(x); var c=Math.Cos(x); return s*s+c*c;},2,5);
        Console.WriteLine($"Calka funkcji sin^2(x)+cos^2(x) na przedziale <2,5>:  {y}");

        // Przypisz do zmiennej y całkę oznaczoną funkcji x*sin(x^3)-1 na przedziale <1,10> z wykorzystaniem 10 podprzedziałów
        // Wykorzystaj metodę Integral (funkcję możesz zdefiniować w dowolny sposób)
        y = NumericalMethods.Integral(x=>x*Math.Sin(x*x*x)-1,1,10,10);
        Console.WriteLine($"Calka funkcji x*sin(x^3)-1 na przedziale <1,10> z wykorzystaniem 10 podprzedzialow:  {y}");

        // Przypisz do zmiennej y całkę oznaczoną funkcji x*sin(x^3)-1 na przedziale <1,10> z wykorzystaniem 1000 podprzedziałów i wyjaśnij wyniki
        // Wykorzystaj metodę Integral (funkcję możesz zdefiniować w dowolny sposób)
        y = NumericalMethods.Integral(x=>x*Math.Sin(x*x*x)-1,1,10,1000);
        Console.WriteLine($"Calka funkcji x*sin(x^3)-1 na przedziale <1,10> z wykorzystaniem 1000 podprzedzialow:  {y}");

        Console.WriteLine("\nEtap 4 (0.5 pkt.)");

        // Wykorzystaj odpowiednio zainicjowany obiekt klasy Iterations do wypisania 10 pierwszych liczb naturalnych: 0, 1, 2, ..., 9
        Iterations naturals = new Iterations(x=>x+1,0,10);
        Console.Write("Liczby naturalne:");
        foreach ( double d in naturals )
            Console.Write($" {d}");
        Console.WriteLine();

        // Wykorzystaj odpowiednio zainicjowany obiekt klasy Iterations do wypisania 11 pierwszych potęg liczby 2: 1, 2, 4, ..., 1024
        Iterations powersof2 = new Iterations(x=>2*x,1,11);
        Console.Write("Potegi liczby 2:");
        foreach ( double d in powersof2 )
            Console.Write($" {d}");
        Console.WriteLine();

        Console.WriteLine("\nKoniec\n");
        }

    }

}
