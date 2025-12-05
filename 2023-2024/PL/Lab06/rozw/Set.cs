using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab06_PL
{
    class Set
    {
        private int[] _elements;

        public Set(params int[] elements)
        {
            this._elements = new int[elements.Length];
            int counter = 0;
            for (int i = 0; i < elements.Length; i++)
            {
                if(!ContainsElement(elements[i]))
                    _elements[counter++] = elements[i];
            }
            Array.Resize(ref _elements, counter);
        }

        public override string ToString()
        {
            if(_elements.Length == 0) return "{ }";
            StringBuilder stringBuilder = new StringBuilder("{" + _elements[0].ToString());
            for (int i = 1; i < _elements.Length; i++)
            {
                stringBuilder.Append(",");
                stringBuilder.Append(_elements[i].ToString());
            }
            return stringBuilder.ToString() + "}";
        }

        public int GetElementsArrayCapacity()
        { 
            return _elements.Length;
        }

        private bool ContainsElement(int element)
        {
            for (int i = 0; i < _elements.Length; i++)
            {
                if(_elements[i] == element)
                    return true;
            }
            return false;
        }

        public static Set operator+(Set lhs, Set rhs)
        {
            int[] elements = new int[lhs._elements.Length + rhs._elements.Length];
            for (int i = 0; i < lhs._elements.Length; i++)
            {
                elements[i] = lhs._elements[i];
            }
            int counter = lhs._elements.Length;
            for (int i = 0; i < rhs._elements.Length; i++)
            {
                if(!lhs.ContainsElement(rhs._elements[i]))
                {
                    elements[counter++] = rhs._elements[i];
                }
            }
            Array.Resize(ref elements, counter);
            return new Set(elements);
        }

        public static Set operator-(Set lhs, Set rhs)
        {
            int[] elements = new int[lhs._elements.Length];
            int counter = 0;
            for (int i = 0; i < lhs._elements.Length; i++)
            {
                if(!rhs.ContainsElement(lhs._elements[i]))
                    elements[counter++] = lhs._elements[i];
            }
            Array.Resize(ref elements, counter);
            return new Set(elements);
        }

        public static Set operator&(Set lhs, Set rhs)
        {
            Set subtract1 = lhs - rhs;
            Set subtract2 = rhs - lhs;
            return lhs + rhs - subtract1 - subtract2;
        }

        public static bool operator true(Set set)
        {
            return set._elements.Length != 0;
        }

        public static bool operator false(Set set)
        {
            return set._elements.Length == 0;
        }

        public static Set operator|(Set lhs, Set rhs)
        {
            return lhs + rhs;
        }

        public static bool operator ==(Set lhs, Set rhs)
        {
            if((lhs - rhs) || (rhs - lhs)) return false;
            return true;
        }

        public static bool operator!=(Set lhs, Set rhs)
        {
            return !(lhs == rhs);
        }
    }
}
