using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab08A
{
    class Matrix
    {
        // stage 1
        public int Rows { get; set; }
        public int Cols { get; set; }

        private double[,] values;
        public Matrix(int rows, int cols, double[,] _values = null)
        {
            Rows = rows;
            Cols = cols;
            values = new double[Rows, Cols];

            if (_values == null)
                return;

            for (int i = 0; i < Rows; ++i)
                for (int j = 0; j < Cols; ++j)
                    values[i, j] = _values[i, j];
        }

        public double Trace
        {
            get
            {
                int smallerDimension = Math.Min(Rows, Cols);
                double sum = 0.0;
                for (int i = 0; i < smallerDimension; ++i)
                    sum += values[i, i];
                return sum;
            }
        }

        public void Print()
        {
            for (int i = 0; i < Rows; ++i)
            {
                for (int j = 0; j < Cols; ++j)
                    Console.Write("{0, 4}   ", values[i, j]);
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        // stage 2
        public static explicit operator double[,] (Matrix m)
        {
            double[,] array = new double[m.Rows, m.Cols];
            for (int i = 0; i < m.Rows; ++i)
                for (int j = 0; j < m.Cols; ++j)
                    array[i, j] = m.values[i, j];
            return array;
        }

        public static implicit operator Matrix(double[,] array)
        {
            return new Matrix(array.GetLength(0), array.GetLength(1), array);
        }

        //// stage 3

        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            int maxRows = Math.Max(m1.Rows, m2.Rows);
            int maxCols = Math.Max(m1.Cols, m2.Cols);
            double[,] array = new double[maxRows, maxCols];
            for (int i = 0; i < maxRows; ++i)
                for (int j = 0; j < maxCols; ++j)
                    array[i, j] = m1.values[i % m1.Rows, j % m1.Cols] + m2.values[i % m2.Rows, j % m2.Cols];

            return new Matrix(maxRows, maxCols, array);
        }

        public double this[int r, int c]
        {
            get
            {
                return values[r, c];
            }
            set
            {
                values[r, c] = value;
            }
        }

        //stage 4
        public static bool operator ==(Matrix m1, Matrix m2)
        {
            var o1 = (object)m1;
            var o2 = (object)m2;

            if (o1 == null || o2 == null)
                return o1 == null && o2 == null;

            if (m1.Rows != m2.Rows || m1.Cols != m2.Cols)
                return false;

            for (int i = 0; i < m1.Rows; ++i)
                for (int j = 0; j < m1.Cols; ++j)
                {
                    if (m1[i, j] != m2[i, j])
                        return false;
                }
            return true;
        }

        public static bool operator !=(Matrix m1, Matrix m2)
        {
            return !(m1 == m2);
        }

        public override bool Equals(object obj)
        {
            return this == (Matrix)obj;
        }

        public override int GetHashCode()
        {
            int res = 0;

            for (int i = 0; i < Rows; ++i)
                for (int j = 0; j < Cols; ++j)
                    res ^= values[i, j].GetHashCode();

            return res;
        }
    }
}
