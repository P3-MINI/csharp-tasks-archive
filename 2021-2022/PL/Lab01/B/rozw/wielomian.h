
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
    
    // Zrozumienie szczegolow jego dzialania tego konstruktora nie jest niezbêdne dla rozwiazania zadania
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
    
    Wielomian(int n, const T tab[] = nullptr)
        {
        wsp = new T[size=n+1];
        if ( tab==nullptr )
            for ( int i=0 ; i<size ; ++i )
                wsp[i] = 0;
        else
            for ( int i=0 ; i<size ; ++i )
                wsp[i] = tab[i];
        }

    Wielomian(const Wielomian& w)
        {
        wsp = new T[size=w.size];
        for ( int i=0 ; i<size ; ++i )
            wsp[i] = w.wsp[i];
        }

    ~Wielomian()
        {
        delete wsp;
        }

    Wielomian& operator=(const Wielomian& w)
        {
        if ( this!=&w )
            {
            delete wsp;
            wsp = new T[size=w.size];
            for ( int i=0 ; i<size ; ++i )
                wsp[i] = w.wsp[i];
            }
        return *this;
        }

    T& operator[](int n)
        {
        return wsp[n];
        }
    
    T operator[](int n) const
        {
        return wsp[n];
        }
    
    friend Wielomian operator+(const Wielomian& w1, const Wielomian& w2)
        {
        int s1, s2;
        T *wsp1, *wsp2;
        if ( w1.size>=w2.size )
            {
            s1 = w1.size;
            s2 = w2.size;
            wsp1 = w1.wsp;
            wsp2 = w2.wsp;
            }
        else
            {
            s1 = w2.size;
            s2 = w1.size;
            wsp1 = w2.wsp;
            wsp2 = w1.wsp;
            }
        Wielomian w(s1-1,wsp1);
        for ( int i=0 ; i<s2 ; ++i )
                w[i] += wsp2[i];
        return w;
        }

    friend Wielomian operator*(const Wielomian& w1, const Wielomian& w2)
        {
        Wielomian w(w1.size+w2.size-2);
        for ( int i=0 ; i<w1.size ; ++i )
            for ( int j=0 ; j<w2.size ; ++j )
                w[i+j] += w1[i]*w2[j];
        return w;
        }

    T operator()(T x) const
        {
        T y = wsp[size-1];
        for ( int i=size-2 ; i>=0 ; --i )
            y = y*x+wsp[i];
        return y;    
        }

    friend ostream& operator<<(ostream& os, const Wielomian& w)
        {
        for ( int i=w.size-1 ; i>0 ; --i )
            os << w.wsp[i] << "*x^" << i << " + " ;
        return os << w.wsp[0];
        }

    };

#endif   // _WIELOMIAN_H_
