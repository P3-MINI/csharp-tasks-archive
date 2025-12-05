using System;
using System.Collections;
using System.Collections.Generic;

namespace lab09_b
{
    public interface IPriorityQueue<T>
    {
        void Put(T item);
        T Get();
        int Count { get; }
        T Peek { get; }
    }
    
    // Tutaj należy umieścić cały kod z laboratorium
}
