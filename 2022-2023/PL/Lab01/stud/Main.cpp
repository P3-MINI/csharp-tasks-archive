
// Note: You should create following project:
// C++, Windows, Console -> Empty Project.

#include "Polynomial.h"

#include <chrono>

template <class Type> Type TestP(int n, Type x)
{
    double y = x;

    for (int i = 1; i < n; i++)
        y *= x;

    return y;
}

template <class Type> Type Test(const Polynomial<Type>& w, int n)
{
    const int k = 100;
    int cp, cf;
    double y[k];
    {
        const auto t1 = std::chrono::high_resolution_clock::now();
        for (int i = 0; i < k; ++i)
            y[i] = TestP(n, 1.1 + 0.001 * i);
        const auto t2 = std::chrono::high_resolution_clock::now();
        cp = std::chrono::duration_cast<std::chrono::nanoseconds>(t2 - t1).count();
    }
    {
        const auto t1 = std::chrono::high_resolution_clock::now();
        for (int i = 0; i < k; ++i)
            y[i] = w(1.1 + 0.001 * i);
        const auto t2 = std::chrono::high_resolution_clock::now();
        cf = std::chrono::duration_cast<std::chrono::nanoseconds>(t2 - t1).count();
    }
    printf("Time: %8dns - %s", cf, cf < 3 * cp ? "OK." : "Too Slow.");
    return y[k - 1];
}

template <class Type> void Print(const Polynomial<Type>& polynomial)
{
    std::cout << polynomial << std::endl;
}

int main(int argc, char** argv)
{
    int tab1[] = { 2, 3, 4 };
    int tab2[] = { -2, -1, 0, 5 };
    double tab3[] = { 0.5, -1.0, 0.75 };

    std::cout << std::endl << "Stage I" << std::endl << std::endl;

    //Polynomial<int> w1(1);              //
    //const Polynomial<int> w2(2, tab1);  //
    //Polynomial<double> wd(2, tab3);     //
    //Polynomial<int> w3 = w2;            //
    //Print(w1);                          //   0*x^1 + 0
    //Print(w2);                          //   4*x^2 + 3*x^1 + 2
    //Print(w3);                          //   4*x^2 + 3*x^1 + 2
    //std::cout << std::endl;             //
    //tab1[1] = 555;                      //
    //Print(w2);                          //   4*x^2 + 3*x^1 + 2
    //Print(w3);                          //   4*x^2 + 3*x^1 + 2

    std::cout << std::endl << "Stage II" << std::endl << std::endl;

    //int a;                          //
    //w1 = w2;                        //
    //a = w2[0];                      //
    //w1[2] = a + 3;                  //
    //w3[0] = 1;                      //
    //w3 = w3;                        //
    //Print(w1);                      //   5*x^2 + 3*x^1 + 2
    //Print(w2);                      //   4*x^2 + 3*x^1 + 2
    //Print(w3);                      //   4*x^2 + 3*x^1 + 1
    //std::cout << std::endl;         //
    //Polynomial<int> w4(3, tab2);    //
    //Polynomial<int> w5(1);          //
    //w5 = w2 + w4;                   //
    //Print(w5);                      //   5*x^3 + 4*x^2 + 2*x^1 + 0
    //std::cout << std::endl;         //
    //Polynomial<int> w6(6);          //
    //w6 = w4 + w2;                   //
    //Print(w6);                      //   5*x^3 + 4*x^2 + 2*x^1 + 0

    std::cout << std::endl << "Stage III" << std::endl << std::endl;

    //w1 = w2 * w4;       //
    //Print(w1);          //   20*x^5 + 15*x^4 + 6*x^3 + -11*x^2 + -8*x^1 + -4
    //Print(w2);          //   4*x^2 + 3*x^1 + 2
    //Print(w4);          //   5*x^3 + 0*x^2 + -1*x^1 + -2

    //Polynomial<double> w10 = { 1.00, 1.01, 1.02, 1.03, 1.04, 1.05, 1.06, 1.07, 1.08, 1.09, 1.10 };
    //Polynomial<double> w50 = { 1.00, 1.01, 1.02, 1.03, 1.04, 1.05, 1.06, 1.07, 1.08, 1.09, 1.10,
    //                                 1.11, 1.12, 1.13, 1.14, 1.15, 1.16, 1.17, 1.18, 1.19, 1.20,
    //                                 1.21, 1.22, 1.23, 1.24, 1.25, 1.26, 1.27, 1.28, 1.29, 1.30,
    //                                 1.31, 1.32, 1.33, 1.34, 1.35, 1.36, 1.37, 1.38, 1.39, 1.40,
    //                                 1.41, 1.42, 1.43, 1.44, 1.45, 1.46, 1.47, 1.48, 1.49, 1.50 };

    std::cout << std::endl << "Stage IV" << std::endl << std::endl;

    //double y;                                                                                   //
    //y = wd(2.0);                                                                                //   1.5
    //std::cout << y << std::endl;                                                                //
    //std::cout << std::endl << "Performance tests for 10th degree polymonial:" << std::endl;     //
    //Test(w10, 10);                                                                              //   OK.
    //std::cout << std::endl << "Performance tests for 50th degree polymonial:" << std::endl;     //
    //Test(w50, 50);                                                                              //   OK.
    //std::cout << std::endl;                                                                     //

    std::cout << std::endl << "The End" << std::endl << std::endl;

    // You cannot add any line of code here!
    // No getchar() and/or system("pause") etc.

    return 0;
}

