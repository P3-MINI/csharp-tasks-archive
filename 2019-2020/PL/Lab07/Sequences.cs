using System;
using System.Collections;

namespace lab7yield
{

interface ISequence : IEnumerable
    {
    string GetSignature();  // metoda zwraca nazwę ciągu wraz z parametrami (jeśli są wymagane dla danego rodzaju ciągu)
    }

class ArraySequence : ISequence
    {

    private int[] tab;

    public ArraySequence(int[] _tab)
        {
        tab = (int[])_tab.Clone();
        }

    public virtual string GetSignature()
        {
        return string.Format("ArraySequence");
        }

    public IEnumerator GetEnumerator()
        {
        foreach ( int i in tab )
            yield return i;
        }

    }


class ArithmeticSequence : ISequence
    {

    private int start;
    private int step;

    public ArithmeticSequence(int _start, int _step)
        {
        start = _start;
        step = _step;
        }

    public virtual string GetSignature()
        {
        return string.Format("ArithmeticSequence {{ Start = {0}, Step = {1} }}", start, step);
        }

    public IEnumerator GetEnumerator()
        {
        int i = 0;
        while (true)
            yield return start + i++*step;
        }

    }

class NaturalNumbers : ArithmeticSequence
    {

    public override string GetSignature()
        {
        return string.Format("NaturalNumbers");
        }

    public NaturalNumbers() : base(0,1) { }

    }

class RandomSequence : ISequence
    {
    private int seed;
    private int max;

    public RandomSequence(int _seed, int _max)
        {
        seed = _seed;
        max = _max;
        }

    public string GetSignature()
        {
        return string.Format("RandomSequence {{ Seed = {0}, Max = {1} }}", seed, max);
        }

    public IEnumerator GetEnumerator()
        {
        Random rnd = new Random(seed);
        while ( true )
            yield return rnd.Next(max);
        }

    }

class Tribonacci : ISequence
    {

    public string GetSignature()
        {
        return string.Format("Tribonacci");
        }

    public IEnumerator GetEnumerator()
        {
        int i = 0;
        int j = 0;
        int k = 1;
        int tmp;

        yield return i;
        yield return j;
        yield return k;

        while (true)
            {
            tmp = k;
            k = k + j + i;
            i = j;
            j = tmp;
            yield return k;
            }
        }

    }

}

