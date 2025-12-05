using System;

namespace Lab7a
{
    public class UniqueVector
    {
        private int[] tab;

        public int Count { get { return tab.Length; } }

        public int this[int index]
        {
            get
            {
                if (index < Count && index >= 0)
                    return tab[index];
                else
                    throw new IndexOutOfRangeException();
            }

            set
            {
                if (index < Count && index >= 0)
                {
                    tab[index] = value;
                    return;
                }

                throw new IndexOutOfRangeException();
            }
        }

        public UniqueVector(int[] values = null)
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

        public void Deconstruct(out UniqueVector v1, out UniqueVector v2)
        {
            int split_index = (int)Math.Ceiling(Count / 2.0f);
            v1 = new UniqueVector(tab[..split_index]);
            v2 = new UniqueVector(tab[split_index..]);
        }

        public UniqueVector Clone()
        {
            UniqueVector result = new UniqueVector();
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
            return this == (UniqueVector)obj;
        }

        public override int GetHashCode()
        {
            return tab.Length;
        }

        public static bool operator ==(UniqueVector v1, UniqueVector v2)
        {
            if (v1.Count != v2.Count) return false;

            for (int index = 0; index < v1.Count; index++)
                if (v1.tab[index] != v2.tab[index])
                    return false;

            return true;
        }

        public static bool operator !=(UniqueVector v1, UniqueVector v2)
        {
            return !(v1 == v2);
        }

        public static UniqueVector operator +(UniqueVector v1, UniqueVector v2)
        {
            int[] temp = new int[v1.Count + v2.Count];

            for (int index = 0; index < v1.Count; index++)
                temp[index] = v1.tab[index];

            for (int index = 0; index < v2.Count; index++)
                temp[index + v1.Count] = v2.tab[index];

            return new UniqueVector(temp);
        }

        public static UniqueVector operator ++(UniqueVector vector)
        {
            for (int index = 0; index < vector.Count; index++)
                vector.tab[index]++;
            return vector;
        }

        public static UniqueVector operator *(UniqueVector v1, UniqueVector v2)
        {
            int count = 0;
            int[] inter_values = new int[v1.Count];

            for (int index = 0; index < v1.Count; index++)
            {
                int value = v1.tab[index];

                int index2 = 0;
                while (index2 < v2.Count && value != v2.tab[index2]) index2++;

                if (index2 != v2.Count)
                    inter_values[count++] = v1.tab[index];
            }

            UniqueVector result_vector = new UniqueVector();

            result_vector.tab = inter_values[..count];

            return result_vector;
        }

        public static explicit operator UniqueVector(int[] tab)
        {
            return new UniqueVector(tab);
        }

        public static implicit operator int[](UniqueVector vector)
        {
            return (int[])vector.tab.Clone();
        }

        public static implicit operator UniqueVector(int value)
        {
            return new UniqueVector(new int[] { value });
        }
    }
}
