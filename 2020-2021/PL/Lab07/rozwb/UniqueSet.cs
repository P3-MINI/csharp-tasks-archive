using System;

namespace Lab7b
{
    public class UniqueSet
    {
        private int[] tab;

        public int Size { get { return tab.Length; } }

        public int this[int index]
        {
            get
            {
                if (index < Size && index >= 0)
                    return tab[index];
                else
                    throw new IndexOutOfRangeException();
            }

            set
            {
                if (index < Size && index >= 0)
                {
                    tab[index] = value;
                    return;
                }

                throw new IndexOutOfRangeException();
            }
        }

        public UniqueSet(int[] values = null)
        {
            if (values != null)
            {
                int free_index = 0;
                int[] temp = new int[values.Length];

                for (int index = 0; index < values.Length; index++)
                {
                    int value = values[index];

                    int index2 = 0;
                    while (index2 < free_index && value != temp[index2]) index2++;

                    if (index2 == free_index)
                        temp[free_index++] = value;
                }

                tab = temp[..free_index];
            }
            else
                tab = new int[0];
        }

        public void Deconstruct(out UniqueSet v1, out UniqueSet v2)
        {
            int count_even = 0;
            int count_odd = 0;
            int[] tab_even = new int[Size];
            int[] tab_odd = new int[Size];

            for (int index = 0; index < Size; index++)
            {
                int value = tab[index];

                if (value % 2 == 0)
                    tab_even[count_even++] = value;
                else
                    tab_odd[count_odd++] = value;
            }

            v1 = new UniqueSet(tab_even[..count_even]);
            v2 = new UniqueSet(tab_odd[..count_odd]);
        }

        public UniqueSet Clone()
        {
            UniqueSet result = new UniqueSet();
            result.tab = (int[])this.tab.Clone();
            return result;
        }

        public override string ToString()
        {
            string result = "[";
            result += string.Join(';', tab);
            result += "]";

            return result;
        }

        public override bool Equals(object obj)
        {
            return this == (UniqueSet)obj;
        }

        public override int GetHashCode()
        {
            return tab.Length;  
        }

        public static bool operator ==(UniqueSet s1, UniqueSet s2)
        {
            if (s1.Size != s2.Size) return false;

            for (int index = 0; index < s1.Size; index++)
                if (!s1.Contains(s2.tab[index]))
                    return false;

            return true;
        }

        public static bool operator !=(UniqueSet s1, UniqueSet s2)
        {
            return !(s1 == s2);
        }

        public static UniqueSet operator +(UniqueSet v1, UniqueSet v2)
        {
            int[] temp = new int[v1.Size + v2.Size];

            for (int index = 0; index < v1.Size; index++)
                temp[index] = v1.tab[index];

            for (int index = 0; index < v2.Size; index++)
                temp[index + v1.Size] = v2.tab[index];

            return new UniqueSet(temp);
        }

        public static UniqueSet operator --(UniqueSet s)
        {
            for (int index = 0; index < s.Size; index++)
                s.tab[index]--;
            return s;
        }

        public static UniqueSet operator ^(UniqueSet s1, UniqueSet s2)
        {
            int count = 0;
            int[] diff_values = new int[s1.Size + s2.Size];

            for (int index = 0; index < s1.Size; index++)
                if (!s2.Contains(s1.tab[index]))
                    diff_values[count++] = s1.tab[index];

            for (int index = 0; index < s2.Size; index++)
                if (!s1.Contains(s2.tab[index]))
                    diff_values[count++] = s2.tab[index];

            return new UniqueSet(diff_values[..count]);
        }

        public static explicit operator UniqueSet(int[] tab)
        {
            return new UniqueSet(tab);
        }

        public static implicit operator int[](UniqueSet vector)
        {
            return vector.tab[..vector.Size];
        }

        public static implicit operator UniqueSet(int value)
        {
            return new UniqueSet(new int[] { value });
        }

        private bool Contains(int value)
        {
            for (int index = 0; index < Size; index++)
                if (tab[index] == value)
                    return true;

            return false;
        }
    }
}
