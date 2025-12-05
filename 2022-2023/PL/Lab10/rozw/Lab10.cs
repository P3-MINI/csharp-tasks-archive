using System;

namespace Lab10
{
    static class MatrixExtender
    {
        public static T Max<T>(this NDimMatrix<T> t) where T: struct, IComparable
        {
            int[] first_index = new int[t._array.Rank];
            T max = (T) t._array.GetValue(first_index);
            foreach (T val in t._array)
            {
                if (max.CompareTo(val) <= 0)
                    max = val;
            }
            return max;
        }
        public static T Min<T>(this NDimMatrix<T> t) where T : struct, IComparable
        {
            int[] first_index = new int[t._array.Rank];
            T min = (T) t._array.GetValue(first_index);
            foreach (T val in t._array)
            {
                if (min.CompareTo(val) >= 0)
                    min = val;
            }
            return min;
        }
        public static T[] Flatten<T>(this NDimMatrix<T> t)
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
    class NDimMatrix<T>
    {
        public Array _array;
        public NDimMatrix(Array array)
        {
            _array = array;
        }
        public NDimMatrix<T> this[params Range[] ranges]
        {
            get
            {
                int[] dims = new int[ranges.Length];
                int i = 0;
                foreach (Range range in ranges)
                {
                    (int mOffset, int mLength) = range.GetOffsetAndLength(_array.GetLength(i));
                    dims[i] = mLength;
                    i++;
                }
                Array newArray = Array.CreateInstance(typeof(T), dims);
                CreateNewArrayWithRanges(_array, newArray, new int[] { }, new int[] { }, ranges);
                return new NDimMatrix<T>(newArray);
            }
            set
            {
                ModifyCurrentArrayWithRanges(value._array, new int[] { }, new int[] { }, ranges);
            }
        }
        public static void CreateNewArrayWithRanges(Array _array, Array a, int[] original_array_indices, int[] new_array_indices, params Range[] ranges)
        {
            (int mOffset, int mLength) = ranges[0].GetOffsetAndLength(_array.GetLength(0));
            if (ranges.GetLength(0) == 1)
            {
                for (int i = mOffset; i < mOffset + mLength; i++)
                {
                    a.SetValue(_array.GetValue(CreateIndices(original_array_indices, i)), CreateIndices(new_array_indices, i - mOffset));
                }
            }
            else
                for (int i = mOffset; i < mOffset + mLength; i++)
                {
                    CreateNewArrayWithRanges(_array, a, CreateIndices(original_array_indices, i), CreateIndices(new_array_indices, i - mOffset), ranges[1..]);
                }
        }
        public void ModifyCurrentArrayWithRanges(Array a, int[] original_array_indices, int[] new_array_indices, params Range[] ranges)
        {
            (int mOffset, int mLength) = ranges[0].GetOffsetAndLength(_array.GetLength(0));
            if (ranges.GetLength(0) == 1)
            {
                for (int i = mOffset; i < mOffset + mLength; i++)
                {
                    _array.SetValue(a.GetValue(CreateIndices(new_array_indices, i - mOffset)), CreateIndices(original_array_indices, i));
                }
            }
            else
                for (int i = mOffset; i < mOffset + mLength; i++)
                {
                    ModifyCurrentArrayWithRanges(a, CreateIndices(original_array_indices, i), CreateIndices(new_array_indices, i - mOffset), ranges[1..]);
                }
        }
        public static int[] CreateIndices(int[] old_array, int new_index)
        {
            int[] new_array = new int[old_array.Length + 1];
            old_array.CopyTo(new_array, 0);
            new_array[new_array.Length - 1] = new_index;
            return new_array;
        }
    }
}
