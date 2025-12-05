
// UWAGA: Nale¿y utworzyæ projekt typu:
// C++, Windows, Console  -> Empty project

#include <iostream>
#include "wielomian.h"

using namespace std;

inline __int64 rdtsc() { __asm RDTSC }

template <class T> T testp(int n, T x)
    {
    double y=x;
    for ( int i=1 ; i<n ; ++i )
        y *= x;
    return y;
    }

template <class T> T test(const Wielomian<T>& w, int n)
    {
    const int k=100;
    int cp, cf;
    __int64 t1, t2;
    double y[k];

    t1 = rdtsc();
    for ( int i=0 ; i<k ; ++i )
        y[i] = testp(n,1.1+0.001*i);
    t2 = rdtsc();
    cp = (int)(t2-t1);

    t1 = rdtsc();
    for ( int i=0 ; i<k ; ++i )
        y[i] = w(1.1+0.001*i);
    t2 = rdtsc();
    cf = (int)(t2-t1);
    printf("Czas: %8d - %s", cf, cf<3*cp?"OK":"za wolno");
    return y[k-1];
    }

template <class T> void wypisz(const Wielomian<T>& w)
    {
    cout << w << endl ;
    }

int main()
    {
    int tab2[] = { 2, 3, 4 };
    int tab3[] = { -2, -1, 0, 5 };
    double tabd[] = { 0.5, -1.0, 0.75 };

    cout << endl << "ETAP I" << endl << endl ;

    Wielomian<int> w1(1);             //   odkomentowanie tego bloku 1p
    const Wielomian<int> w2(2,tab2);  //
    Wielomian<double> wd(2,tabd);     //
    Wielomian<int> w3=w2;             //
    wypisz(w1);                       //   0*x^1 + 0
    wypisz(w2);                       //   4*x^2 + 3*x^1 + 2
    wypisz(w3);                       //   4*x^2 + 3*x^1 + 2
    cout << endl;                     //
    tab2[1] = 555;                    //
    wypisz(w2);                       //   4*x^2 + 3*x^1 + 2
    wypisz(w3);                       //   4*x^2 + 3*x^1 + 2

    cout << endl << "ETAP II" << endl << endl ;

    int a;                            //   odkomentowanie tego bloku 1p
    w1 = w2;                          //
    a = w2[0];                        //
    w1[2] = a+3;                      //
    w3[0] = 1;                        //
    w3 = w3;                          //
    wypisz(w1);                       //   5*x^2 + 3*x^1 + 2
    wypisz(w2);                       //   4*x^2 + 3*x^1 + 2
    wypisz(w3);                       //   4*x^2 + 3*x^1 + 1
    cout << endl;                     //
    Wielomian<int> w4(3,tab3);        //
    Wielomian<int> w5(1);             //
    w5 = w2+w4;                       //
    wypisz(w5);                       //   5*x^3 + 4*x^2 + 2*x^1 + 0
    cout << endl;                     //
    Wielomian<int> w6(6);             //
    w6 = w4+w2;                       //
    wypisz(w6);                       //   5*x^3 + 4*x^2 + 2*x^1 + 0

    cout << endl << "ETAP III" << endl << endl ;

    w1 = w2*w4;                       //   odkomentowanie tego bloku 0.5p
    wypisz(w1);                       //   20*x^5 + 15*x^4 + 6*x^3 + -11*x^2 + -8*x^1 + -
    wypisz(w2);                       //   4*x^2 + 3*x^1 + 2
    wypisz(w4);                       //   5*x^3 + 0*x^2 + -1*x^1 + -2

    Wielomian<double> w10 = { 1.00, 1.01, 1.02, 1.03, 1.04, 1.05, 1.06, 1.07, 1.08, 1.09, 1.10 };
    Wielomian<double> w50 = { 1.00, 1.01, 1.02, 1.03, 1.04, 1.05, 1.06, 1.07, 1.08, 1.09, 1.10,
                                    1.11, 1.12, 1.13, 1.14, 1.15, 1.16, 1.17, 1.18, 1.19, 1.20,
                                    1.21, 1.22, 1.23, 1.24, 1.25, 1.26, 1.27, 1.28, 1.29, 1.30,
                                    1.31, 1.32, 1.33, 1.34, 1.35, 1.36, 1.37, 1.38, 1.39, 1.40,
                                    1.41, 1.42, 1.43, 1.44, 1.45, 1.46, 1.47, 1.48, 1.49, 1.50 };

    cout << endl << "ETAP IV" << endl << endl ;

    double y;                                                                //   odkomentowanie tego bloku 0.5p
    y = wd(2.0);                                                             //
    cout << y << endl ;                                                      //   1.5
    cout << endl << "Testy wydajnosci dla wielomianow stopnia 10" << endl ;  //
    test(w10,10);                                                            //
    cout << endl << "Testy wydajnosci dla wielomianow stopnia 50" << endl ;  //
    test(w50,50);                                                            //
    cout<<endl;                                                              //

    cout << endl << "KONIEC" << endl << endl ;

    // tu nie wolno dopisywac zadnego getchar(), system("pause")
    // ani nic co zatrzyma wykonanie funkcji main

    return 0;
    }

