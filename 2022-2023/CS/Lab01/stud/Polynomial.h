
#ifndef _WIELOMIAN_H_
#define _WIELOMIAN_H_

#include <iostream>
#include <initializer_list>

template <class Type> class Polynomial
{
    // You can only add new members here.
    // Don't modify or delete any existing ones!

    // Calculations of value of the polynomial (at given x) should be linear in respect to its degree.
    // You cannot use power function. Only additions and multiplications are allowed.
    // Horner method is strongly recommended, yet not required.

private:

    int m_Size;             // Size of the array of coefficients (aka Degree).
    Type* m_Coefficients;   // Array of coefficients of given polynomial.

public:

    Polynomial(std::initializer_list<Type> arguments)
    {
        this->m_Coefficients = new Type[this->m_Size = arguments.size()];

        int index = 0;
        
        for (Type argument : arguments)
            this->m_Coefficients[index++] = argument;
    }

    // One-Parameter constructor should create polynomial of given degree with all coefficients equal to zero.
    // Two-Parameter constructor should create polynomial of given degree with coefficients given by the array.
    // You should implement both of them using single constructor (default parameters).

    ~Polynomial()
    {
        delete this->m_Coefficients;
    }

    Type operator[](int index) const
    {
        return this->m_Coefficients[index];
    }

    friend std::ostream& operator<<(std::ostream& os, const Polynomial& polynomial)
    {
        for (int i = polynomial.m_Size - 1; i > 0; i--)
            os << polynomial.m_Coefficients[i] << "*x^" << i << " + ";

        return os << polynomial.m_Coefficients[0];
    }
};

#endif   // _WIELOMIAN_H_
