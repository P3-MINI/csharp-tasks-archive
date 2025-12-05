using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab08A
{
    class Program
    {
        static void Main(string[] args)
        {
            // stage 1
            Console.WriteLine("--Stage 1---");

            double[,] arr1 = new double[3, 4] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 } };
            Matrix m0 = new Matrix(3, 4);
            Matrix m1 = new Matrix(3, 4, arr1);

            Console.WriteLine(m0.Rows);
            Console.WriteLine(m0.Cols);
            Console.WriteLine(m1.Rows);
            Console.WriteLine(m1.Cols);
            m0.Print();
            m1.Print();
            Console.WriteLine(m1.Trace);

            // stage 2
            //Console.WriteLine("--Stage 2---");

            //double[,] arr2 = new double[3, 4] { { 11, 22, 33, 44 }, { 55, 66, 77, 88 }, { 99, 110, 111, 112 } };
            //Matrix m2 = arr2;
            //m2.Print();
            //double[,] arr3 = (double[,])m2;
            //for (int i = 0; i < arr3.GetLength(0); ++i)
            //{
            //    for (int j = 0; j < arr3.GetLength(1); ++j)
            //        Console.Write("{0, 4}   ", arr3[i, j]);
            //    Console.WriteLine();
            //}
            //Console.WriteLine();

            //// stage 3
            //Console.WriteLine("--Stage 3---");

            //Console.WriteLine(m2[2, 3]);
            //Console.WriteLine(m1[0, 1]);
            //m1[0, 1] = 997;
            //Console.WriteLine(m1[0, 1]);
            //m1.Print();

            //Matrix m3 = m1 + m2;
            //m1.Print();
            //m2.Print();
            //m3.Print();

            //Matrix m4 = new Matrix(1, 2, new double[1, 2] { { 1000, 100000 } });
            //Matrix m5 = m1 + m4;
            //m5.Print();

            //// stage 4
            //Console.WriteLine("--Stage 4---");
            //Matrix m1Clone = new Matrix(3, 4, arr1);
            //Matrix m1Clone2 = new Matrix(3, 4, arr1);
            //Console.WriteLine(m1Clone == m1Clone2);
            //Console.WriteLine(m1Clone != m1Clone2);
            //Console.WriteLine(m1 == m0);
            //Console.WriteLine(m1 != m0);
            //Console.WriteLine(m1Clone.Equals(m1Clone2));
        }
    }
}
