using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab08B
{
    class Program
    {
        static void Main(string[] args)
        {
            // stage 1
            Console.WriteLine("--Stage 1---");

            double[][] arr1 = new double[3][] { new double[] { 1, 2, 3, 4 }, new double[] { 5, 6, 7, 8 }, new double[] { 9, 10, 11, 12 } };
            double[][] arr11 = new double[3][] { new double[] { 1, 2, 3 }, new double[] { 2, 5, 8 }, new double[] { 3, 8, 9 } };
            double[][] arr12 = new double[3][] { new double[] { 1, 2, 3 }, new double[] { 2, 5, 8 }, new double[] { 3, 7, 9 } };
            Matrix m0 = new Matrix(3, 4);
            Matrix m1 = new Matrix(arr1);
            Matrix m11 = new Matrix(arr11);
            Matrix m12 = new Matrix(arr12);
            Matrix m13 = new Matrix(3, 3);


            Console.WriteLine(m0.Rows);
            Console.WriteLine(m0.Columns);
            Console.WriteLine(m1.Rows);
            Console.WriteLine(m1.Columns);
            Console.WriteLine(m11.Rows);
            Console.WriteLine(m11.Columns);
            m0.Print();
            m1.Print();
            m11.Print();
            Console.WriteLine(m0.IsSymmetric);
            Console.WriteLine(m1.IsSymmetric);
            Console.WriteLine(m11.IsSymmetric);
            Console.WriteLine(m12.IsSymmetric);
            Console.WriteLine(m13.IsSymmetric);

            // stage 2
            Console.WriteLine("--Stage 2---");

            double[][] arr2 = new double[3][] { new double[] { 11, 12, 13, 14 }, new double[] { 15, 16, 17, 18 }, new double[] { 19, 110, 111, 112 } };
            Matrix m2 = arr2;
            m2.Print();
            double[][] arr3 = (double[][])m2;
            for (int i = 0; i < arr3.GetLength(0); ++i)
            {
                for (int j = 0; j < arr3[0].GetLength(0); ++j)
                    Console.Write("{0, 4}   ", arr3[i][j]);
                Console.WriteLine();
            }
            Console.WriteLine();

            double sum_mat2 = (double)m2;
            Console.WriteLine(sum_mat2);

            // stage 3
            Console.WriteLine("--Stage 3---");

            Console.WriteLine(m2[3, 2]);
            Console.WriteLine(m1[0, 1]);
            m1[0, 1] = 997;
            Console.WriteLine(m1[0, 1]);
            m1.Print();

            double[] diagonal = -m1;
            for (int j = 0; j < diagonal.GetLength(0); ++j)
                Console.Write("{0, 4}   ", diagonal[j]);
            Console.WriteLine();

            Matrix m5 = m1 + 100000;
            m5.Print();

            // stage 4
            Console.WriteLine("--Stage 4---");
            Matrix m1Clone = new Matrix(arr1);
            Matrix m1Clone2 = new Matrix(arr1);
            Console.WriteLine(m1Clone == m1Clone2);
            Console.WriteLine(m1Clone != m1Clone2);
            Console.WriteLine(m1 == m0);
            Console.WriteLine(m1 != m0);
            Console.WriteLine(m1Clone.Equals(m1Clone2));
        }
    }
}
