
#ifndef _WIELOMIAN_H_
#define _WIELOMIAN_H_

#include <iostream>
#include <initializer_list>

using namespace std;

template <class T> class Wielomian
    {
    //  W tej klasie wolno jedynie dodawac nowe skladowe
    //  Nie wolno modyfikowac ani usuwac istniejacych !!!

    //  Obliczanie wartosci wielomianu powinno (musi) miec zlozonosc liniowa wzgledem stopnia wielomianu
    //  Nie wolno korzystac z funkcji pow, dozwolone jest jedynie korzystanie z dodawania i mnozenia
    //  Oczywiscie najlepiej zastosowac schemat Hornera, ale nie jest to obowiazkowe

    private:

    int size;  // rozmiar tablicy wspolczynnikow (czyli stopien+1)
    T*  wsp;   // tablica wspolczynnikow

    public:
    
    // Zrozumienie szczegolow jego dzialania tego konstruktora nie jest niezbędne dla rozwiazania zadania
    Wielomian(initializer_list<T> args)
        {
        wsp = new T[size=args.size()];
        int i=0;
        for ( T e : args )
            wsp[i++] = e;
        }

    //  Konstruktor jednoparametrowy tworzy wielomian wskazanego stopnia ze wszystkimi wspolczynnikami rownymi 0
    //  Konstruktor dwuparametrowy tworzy wielomian wskazanego stopnia ze wspolczynnikami pobranymi ze wskazanej tablicy
    //  Oba zaimplementowac wspolnie jako funkcje z parametrem domyslnym

    ~Wielomian()
        {
        delete wsp;
        }
    
    T operator[](int n) const
        {
        return wsp[n];
        }

    friend ostream& operator<<(ostream& os, const Wielomian& w)
        {
        for ( int i=w.size-1 ; i>0 ; --i )
            os << w.wsp[i] << "*x^" << i << " + " ;
        return os << w.wsp[0];
        }

    };

#endif   // _WIELOMIAN_H_
