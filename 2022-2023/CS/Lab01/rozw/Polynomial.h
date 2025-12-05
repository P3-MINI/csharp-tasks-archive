
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

    Polynomial(int degree, const Type coefficients[] = nullptr)
    {
        this->m_Coefficients = new Type[this->m_Size = degree + 1];

        if (coefficients == nullptr)
        {
            for (int i = 0; i < this->m_Size; i++)
                this->m_Coefficients[i] = 0;
        }
        else
        {
            for (int i = 0; i < this->m_Size; i++)
                this->m_Coefficients[i] = coefficients[i];
        }
    }

    Polynomial(const Polynomial& polynomial)
    {
        this->m_Coefficients = new Type[this->m_Size = polynomial.m_Size];

        for (int i = 0; i < this->m_Size; i++)
            this->m_Coefficients[i] = polynomial.m_Coefficients[i];
    }

    ~Polynomial()
    {
        delete this->m_Coefficients;
    }

    Polynomial& operator=(const Polynomial& polynomial)
    {
        if (this != &polynomial)
        {
            delete this->m_Coefficients;

            this->m_Coefficients = new Type[this->m_Size = polynomial.m_Size];

            for (int i = 0; i < this->m_Size; i++)
                this->m_Coefficients[i] = polynomial.m_Coefficients[i];
        }

        return *this;
    }

    Type& operator[](int index)
    {
        return this->m_Coefficients[index];
    }

    Type operator[](int index) const
    {
        return this->m_Coefficients[index];
    }

    friend Polynomial operator+(const Polynomial& polynomial_1, const Polynomial& polynomial_2)
    {
        int s1, s2;
        Type* coefficients1;
        Type* coefficients2;

        if (polynomial_1.m_Size >= polynomial_2.m_Size)
        {
            s1 = polynomial_1.m_Size;
            s2 = polynomial_2.m_Size;
            coefficients1 = polynomial_1.m_Coefficients;
            coefficients2 = polynomial_2.m_Coefficients;
        }
        else
        {
            s1 = polynomial_2.m_Size;
            s2 = polynomial_1.m_Size;
            coefficients1 = polynomial_2.m_Coefficients;
            coefficients2 = polynomial_1.m_Coefficients;
        }

        Polynomial polynomial(s1 - 1, coefficients1);

        for (int i = 0; i < s2; i++)
            polynomial[i] += coefficients2[i];

        return polynomial;
    }

    friend Polynomial operator*(const Polynomial& polynomial_1, const Polynomial& polynomial_2)
    {
        Polynomial polynomial(polynomial_1.m_Size + polynomial_2.m_Size - 2);

        for (int i = 0; i < polynomial_1.m_Size; i++)
            for (int j = 0; j < polynomial_2.m_Size; j++)
            {
                polynomial[i + j] += polynomial_1[i] * polynomial_2[j];
            }

        return polynomial;
    }

    Type operator()(Type x) const
    {
        Type y = this->m_Coefficients[this->m_Size - 1];

        for (int i = this->m_Size - 2; i >= 0; i--)
            y = y * x + this->m_Coefficients[i];

        return y;
    }

    friend std::ostream& operator<<(std::ostream& os, const Polynomial& polynomial)
    {
        for (int i = polynomial.m_Size - 1; i > 0; i--)
            os << polynomial.m_Coefficients[i] << "*x^" << i << " + ";

        return os << polynomial.m_Coefficients[0];
    }
};

#endif   // _WIELOMIAN_H_
