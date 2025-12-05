using System;

interface IComputable<T>
{
    T Addition(T x);
    T Multiply(T x);
    T Subtraction(T x);
}
