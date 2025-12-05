#define STAGE1
#define STAGE2
#define STAGE3
using System;

namespace Lab10
{
    class Program
    {
        static void Main(string[] args)
        {
#if STAGE1
            Console.WriteLine("STAGE 1");
            NDimMatrix<int> nDimMatrix1D = new NDimMatrix<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            NDimMatrix<double> nDimMatrix2D = new NDimMatrix<double>(new double[,] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 }, { 13, 14, 15, 16 } });
            NDimMatrix<uint> nDimMatrix3D = new NDimMatrix<uint>(new uint[,,] { { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } }, { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } }, { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } } });
            Console.WriteLine("Matrices created!");
            Console.WriteLine("");
#endif


#if STAGE2
            Console.WriteLine("STAGE 2");
            double max = nDimMatrix2D.Max();

            Console.WriteLine(max);
            Console.WriteLine("==");
            Console.WriteLine("16");
            Console.WriteLine("");

            max = nDimMatrix3D.Max();

            Console.WriteLine(max);
            Console.WriteLine("==");
            Console.WriteLine("9");
            Console.WriteLine("");

            uint min = nDimMatrix3D.Min();

            Console.WriteLine(min);
            Console.WriteLine("==");
            Console.WriteLine("1");
            Console.WriteLine("");

            uint[] flat = nDimMatrix3D.Flatten();

            Console.WriteLine("Flatten array:");
            string flat_string = "";
            for (int i = 0; i < flat.Length; i++)
                flat_string += flat[i].ToString() + ",";
            flat_string = flat_string.Remove(flat_string.Length - 1);
            Console.WriteLine(flat_string);
#endif
#if STAGE3
            Console.WriteLine("STAGE 3");
            Range range = 2..5;
            Console.WriteLine("[" + nDimMatrix1D[range]._array.GetValue(0) + "," + nDimMatrix1D[range]._array.GetValue(1) + "," + nDimMatrix1D[range]._array.GetValue(2) + "]");
            Console.WriteLine("==");
            Console.WriteLine("[3,4,5]");
            Console.WriteLine("");

            Range[] indices = { 1..^1, 1..^0 };

            Console.WriteLine("[" + nDimMatrix2D[indices]._array.GetValue(new int[] { 0, 0 }) + "," + nDimMatrix2D[indices]._array.GetValue(new int[] { 0, 1 }) + "," + nDimMatrix2D[indices]._array.GetValue(new int[] { 0, 2 }) + "]");
            Console.WriteLine("[" + nDimMatrix2D[indices]._array.GetValue(new int[] { 1, 0 }) + "," + nDimMatrix2D[indices]._array.GetValue(new int[] { 1, 1 }) + "," + nDimMatrix2D[indices]._array.GetValue(new int[] { 1, 2 }) + "]");
            Console.WriteLine("==");
            Console.WriteLine("[6,7,8]");
            Console.WriteLine("[10,11,12]");
            Console.WriteLine("");

            nDimMatrix2D[indices] = new NDimMatrix<double>(new double[,] { { 10, 11, 12 }, { 6, 7, 8 } });

            Console.WriteLine("[" + nDimMatrix2D[indices]._array.GetValue(new int[] { 0, 0 }) + "," + nDimMatrix2D[indices]._array.GetValue(new int[] { 0, 1 }) + "," + nDimMatrix2D[indices]._array.GetValue(new int[] { 0, 2 }) + "]");
            Console.WriteLine("[" + nDimMatrix2D[indices]._array.GetValue(new int[] { 1, 0 }) + "," + nDimMatrix2D[indices]._array.GetValue(new int[] { 1, 1 }) + "," + nDimMatrix2D[indices]._array.GetValue(new int[] { 1, 2 }) + "]");
            Console.WriteLine("==");
            Console.WriteLine("[10,11,12]");
            Console.WriteLine("[6,7,8]");
            Console.WriteLine("");

            Range[] indices3d = { ^2..^1, 1..3, 1..3 };

            Console.WriteLine("[" + nDimMatrix3D[indices3d]._array.GetValue(new int[] { 0, 0, 0 }) + "," + nDimMatrix3D[indices3d]._array.GetValue(new int[] { 0, 0, 1 }) + "]");
            Console.WriteLine("[" + nDimMatrix3D[indices3d]._array.GetValue(new int[] { 0, 1, 0 }) + "," + nDimMatrix3D[indices3d]._array.GetValue(new int[] { 0, 1, 1 }) + "]");

            Console.WriteLine("==");

            Console.WriteLine("[5,6]");
            Console.WriteLine("[8,9]");
            Console.WriteLine("");
#endif
        }
    }
}
