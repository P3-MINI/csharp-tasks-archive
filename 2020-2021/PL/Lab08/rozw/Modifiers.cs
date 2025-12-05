using System;
using System.Collections;

namespace Lab8
{
    /// <summary>
    /// Interfejs klas modyfikujących sekwencje
    /// </summary>
    public interface IModifier
    {
        /// <summary>
        /// Nazwa modyfikatora
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Metoda modyfikuje kolekcje
        /// </summary>
        /// <param name="sequence">Sekwencja do modyfikacji</param>
        /// <returns>Zmodyfikowana sekwencja</returns>
        IEnumerable Modify(IEnumerable sequence);
    }

    public class FirstN : IModifier
    {
        private int _n;

        public FirstN(int n)
        {
            _n = n;
        }

        public string Name
        {
            get { return "First " + _n + " numbers"; }
        }

        public IEnumerable Modify(IEnumerable sequence)
        {
            int i = 0;
            foreach (var v in sequence)
            {
                yield return v;
                if (++i >= _n)
                    yield break;
            }
        }
    }

    public class LinearTransform : IModifier
    {
        private int a;
        private int b;

        public LinearTransform(int a, int b)
        {
            this.a = a;
            this.b = b;
        }

        public string Name
        {
            get { return "Linear Transform"; }
        }

        public IEnumerable Modify(IEnumerable sequence)
        {
            foreach (int v in sequence)
            {
                yield return a*v+b;
            }
        }
    }

    public class Unique : IModifier
    {
        public string Name
        {
            get { return "Unique values"; }
        }

        public IEnumerable Modify(IEnumerable sequence)
        {
            int? last = null;
            foreach (int v in sequence)
            {
                if ( !(v==last) )
                    yield return v;
                last=v;
            }
        }
    }

    public class Prime : IModifier
    {
        public string Name
        {
            get { return "Prime numbers from sequence"; }
        }

        public IEnumerable Modify(IEnumerable sequence)
        {
            foreach (var i in sequence)
            {
                if (IsPrime((int)i))
                    yield return i;
            }
        }

        private bool IsPrime(int number)
        {
            if (number == 0)
                return false;

            if (number == 1)
                return false;

            if (number == 2)
                return true;

            for (int i = 2; i < Math.Sqrt(number) + 1; ++i)
                if (number % i == 0)
                    return false;

            return true;
        }
    }

    public class LocalMax : IModifier
    {

        public string Name
        {
            get { return "Locally max values"; }
        }

        public IEnumerable Modify(IEnumerable sequence)
        {
            IEnumerable unique = new Unique().Modify(sequence);
            IEnumerator enumerator = unique.GetEnumerator();
            int last, current, next;

            if (!enumerator.MoveNext())
                yield break;

            // tu wiemy ze seq ma co najmniej 1 element
            last = (int)enumerator.Current;
            if (!enumerator.MoveNext())
                {  // dokladnie 1 - zwracamy go
                yield return last;
                yield break;
                }

            // tu wiemy ze seq ma co najmniej 2 elementy
            current = (int)enumerator.Current;
            if ( last>current )
                yield return last;  // poczatkowy jest max lokalnym

            while (enumerator.MoveNext())
                {
                // tu wiemy ze seq ma co najmniej 3 elementy
                next = (int)enumerator.Current;
                if (current > last && current > next)
                    yield return current;
                last = current;
                current = next;
                }

            if ( current>last )
                yield return current;  // ostatni jest max lokalnym
        }
    }

    public class ComposedModifier : IModifier
    {
        private IModifier[] _modifiers;

        public ComposedModifier(IModifier [] modifiers)
        {
            _modifiers = (IModifier[])modifiers.Clone();
        }

        public string Name
        {
            get { return "Composed modifier"; }
        }

        public IEnumerable Modify(IEnumerable sequence)
        {
            foreach (var modifier in _modifiers)
                sequence = modifier.Modify(sequence);

            return sequence;
        }
    }

}
