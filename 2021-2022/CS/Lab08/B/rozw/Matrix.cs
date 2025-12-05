using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab08B
{
    class Matrix
    {
        // stage 1
        public int Rows { get; set; }
        public int Columns { get; set; }

        private double[][] values;
        public Matrix(int rows, int cols)
        {
            Rows = rows;
            Columns = cols;
            values = new double[Rows][];
            for (int i = 0; i < Rows; i++)
            {
                values[i] = new double[Columns];
            }
        }

        public Matrix(double[][] _values)
        {
            Rows = _values.Length;
            Columns = _values[0].Length;
            values = new double[Rows][];
            for (int i = 0; i < Rows; i++)
            {
                values[i] = new double[Columns];
            }

            if (_values == null)
                return;

            for (int i = 0; i < Rows; ++i)
                for (int j = 0; j < Columns; ++j)
                    values[i][j] = _values[i][j];
        }

        public bool IsSymmetric
        {
            get
            {
                if (Rows != Columns)
                    return false;

                for (int i = 0; i < Rows; i++)
                {
                    for (int j = i + 1; j < Columns; j++)
                    {
                        if (values[i][j] != values[j][i])
                            return false;
                    }
                }
                return true;
            }
        }

        public void Print()
        {
            for (int i = 0; i < Rows; ++i)
            {
                for (int j = 0; j < Columns; ++j)
                    Console.Write("{0, 4}   ", values[i][j]);
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        // stage 2
        public static explicit operator double[][] (Matrix m)
        {
            double[][] array = new double[m.Rows][];
            for (int i = 0; i < m.Rows; i++)
            {
                array[i] = new double[m.Columns];
            }
            for (int i = 0; i < m.Rows; ++i)
                for (int j = 0; j < m.Columns; ++j)
                    array[i][j] = m.values[i][j];
            return array;
        }

        public static implicit operator Matrix(double[][] array)
        {
            return new Matrix(array);
        }

        public static explicit operator double(Matrix m)
        {
            double sum = 0.0;
            for (int i = 0; i < m.Rows; ++i)
                for (int j = 0; j < m.Columns; ++j)
                    sum += m.values[i][j];
            return sum;
        }

        //// stage 3

        public static double[] operator -(Matrix m)
        {
            int smallerDimension = Math.Min(m.Rows, m.Columns);
            double[] array = new double[smallerDimension];
            for (int i = 0; i < smallerDimension; ++i)
                array[i] = m[i, i];
            return array;
        }

        public static Matrix operator +(Matrix m, double v)
        {
            double[][] array = new double[m.Rows][];
            for (int i = 0; i < m.Rows; i++)
            {
                array[i] = new double[m.Columns];
            }

            for (int i = 0; i < m.Rows; ++i)
                for (int j = 0; j < m.Columns; ++j)
                    array[i][j] = m[j, i] + v;

            return new Matrix(array);
        }

        public double this[int c, int r]
        {
            get
            {
                return values[r][c];
            }
            set
            {
                values[r][c] = value;
            }
        }

        //stage 4
        public static bool operator ==(Matrix m1, Matrix m2)
        {
            var o1 = (object)m1;
            var o2 = (object)m2;

            if (o1 == null || o2 == null)
                return o1 == null && o2 == null;

            if (m1.Rows != m2.Rows || m1.Columns != m2.Columns)
                return false;

            for (int i = 0; i < m1.Rows; ++i)
                for (int j = 0; j < m1.Columns; ++j)
                {
                    if (m1[j, i] != m2[j, i])
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
                for (int j = 0; j < Columns; ++j)
                    res ^= values[i][j].GetHashCode();

            return res;
        }
    }
}
