using System;
using System.Collections;
using System.Collections.Generic;

namespace lab09_a
{
    public interface IPriorityQueue<T>
    {
        void Put(T item);
        T Get();
        int Count { get; }
        T Peek();
    }

    // Tutaj należy umieścić cały kod z laboratorium
}
