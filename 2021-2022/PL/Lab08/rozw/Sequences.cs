using System;
using System.Collections;

namespace Lab8
{

    public class Naturals : IEnumerable
    {
        private int start;

        public Naturals(int n=0)
        {
            start=n;
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = start; ; ++i)
                yield return i;
        }
    }

    public class RandomNumbers : IEnumerable
    {
        private int seed;
        private int max;

        public RandomNumbers(int s,int m)
        {
            seed=s;
            max=m;
        }

        public IEnumerator GetEnumerator()
        {
            Random r=new Random(seed);;
            for (int i = 0; ; ++i)
                yield return r.Next(max);
        }
    }

    public class Tribonacci : IEnumerable
    {
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

    public class Polynomial : IEnumerable
    {
        private int[] _a;

        public Polynomial(int[] a)
        {
            _a = (int[])a.Clone();
        }

        public IEnumerator GetEnumerator()
        {
            int value;
            for (int i = 0; ; ++i)
            {
                value = _a[_a.Length - 1];
                for (int j = _a.Length - 2; j >= 0; --j)
                    value = value * i + _a[j];
                yield return value;
            }
        }

    }

    public class Catalan : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            int c = 1;
            yield return c;
            for (int n = 0; ; ++n)
            {
                c = c * 2 * (2 * n + 1) / (n + 2);
                yield return c;
            }
        }
    }

    public class MultiplicationTable : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
        Naturals seq1 = new Naturals(1);
        Naturals seq2 = new Naturals(1);
        FirstN first = new FirstN(5);
        LinearTransform mul;
        foreach (int v in first.Modify(seq1))
            {
            mul = new LinearTransform(v,0);
            yield return mul.Modify(first.Modify(seq2));
            }
        }
    }

}
