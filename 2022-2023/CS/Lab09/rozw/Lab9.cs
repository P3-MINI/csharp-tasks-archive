using System;

namespace Lab09
{
    static class Matrix2DExtender
    {
        public static double Sum<T>(this Matrix2D<T> t)
        {
            double sum = 0;
            foreach (T val in t._array)
            {
                sum += Convert.ToDouble(val);
            }
            return sum;
        }
        public static void AddConst(this Matrix2D<double> t, double toAdd)
        {
            for (int i = 0; i < t._array.GetLength(0); i++)
                for (int k = 0; k < t._array.GetLength(1); k++)
                    t._array[i, k] += toAdd;
        }
        public static void AddConst(this Matrix2D<int> t, int toAdd)
        {
            for (int i = 0; i < t._array.GetLength(0); i++)
                for (int k = 0; k < t._array.GetLength(1); k++)
                    t._array[i, k] += toAdd;
        }
        public static void AddConst(this Matrix2D<uint> t, uint toAdd)
        {
            for (int i = 0; i < t._array.GetLength(0); i++)
                for (int k = 0; k < t._array.GetLength(1); k++)
                    t._array[i, k] += toAdd;
        }
        public static double Average<T>(this Matrix2D<T> t)
        {
            double sum = 0;
            foreach (T val in t._array)
            {
                sum += Convert.ToDouble(val);
            }
            return sum / t._array.Length;
        }
        public static T Max<T>(this Matrix2D<T> t) where T: struct, IComparable
        {
            T max = t._array[0,0];
            foreach (T val in t._array)
            {
                if (max.CompareTo(val) <= 0)
                    max = val;
            }
            return max;
        }
        public static T Min<T>(this Matrix2D<T> t) where T : struct, IComparable
        {
            T min = t._array[0, 0];
            foreach (T val in t._array)
            {
                if (min.CompareTo(val) >= 0)
                    min = val;
            }
            return min;
        }
        public static T[] Flatten<T>(this Matrix2D<T> t)
        {
            T[] array = new T[t._array.Length];
            int i = 0;
            foreach (T val in t._array)
            {
                array[i++] = val;
            }
            return array;
        }
    }
    class Matrix2D<T>
    {
        public T[,] _array;
        public Matrix2D(T[,] array)
        {
            _array = array;
        }
        public T[] this[Index index, Range range]
        {
            get
            {
                (int mOffset, int mLength) = range.GetOffsetAndLength(_array.GetLength(1));
                T[] arr = new T[mLength];
                for (int i = mOffset; i < mOffset + mLength; i++)
                    arr[i - mOffset] = _array[index.Value,i];
                return arr;
            }
            set
            {
                (int mOffset, int mLength) = range.GetOffsetAndLength(_array.GetLength(1));
                for (int i = mOffset; i < mOffset + mLength; i++)
                    _array[index.Value, i] = value[i-mOffset];
            }
        }
    }
}
