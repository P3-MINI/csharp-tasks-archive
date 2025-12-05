#define STAGE1
#define STAGE2
#define STAGE3
using System;

namespace Lab09
{
    class Program
    {
        static void Main(string[] args)
        {
#if STAGE1
            Console.WriteLine("STAGE 1");
            Matrix2D<double> matrix2D = new Matrix2D<double>(new double[,] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 }, { 13, 14, 15, 16 } });
            Matrix2D<int> matrix2Dint = new Matrix2D<int>(new int[,] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 }, { 13, 14, 15, 16 } });
            Matrix2D<int> matrix2Dint2 = new Matrix2D<int>(new int[,] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 }, { 13, 14, 15, 16 } });
            Matrix2D<uint> matrix2Duint = new Matrix2D<uint>(new uint[,] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 }, { 13, 14, 15, 16 } });
            Console.WriteLine("Matrices created!");
            Console.WriteLine("");
#endif

#if STAGE2
            Console.WriteLine("STAGE 2");

            Console.WriteLine("[" + matrix2Dint[1, 1..^0].GetValue(new int[] { 0 }) + "," + matrix2Dint[1, 1..^0].GetValue(new int[] { 1 }) + "," + matrix2Dint[1, 1..^0].GetValue(new int[] { 2 }) + "]");
            Console.WriteLine("[" + matrix2Dint[2, 1..^0].GetValue(new int[] { 0 }) + "," + matrix2Dint[2, 1..^0].GetValue(new int[] { 1 }) + "," + matrix2Dint[2, 1..^0].GetValue(new int[] { 2 }) + "]");
            Console.WriteLine("==");
            Console.WriteLine("[6,7,8]");
            Console.WriteLine("[10,11,12]");
            Console.WriteLine("");

            matrix2Dint[1, 1..^0] = new int[] { 10, 11, 12 };
            matrix2Dint[2, 1..^0] = new int[] { 6, 7, 8 };
            Console.WriteLine("[" + matrix2Dint[1, 1..^0].GetValue(new int[] { 0 }) + "," + matrix2Dint[1, 1..^0].GetValue(new int[] { 1 }) + "," + matrix2Dint[1, 1..^0].GetValue(new int[] { 2 }) + "]");
            Console.WriteLine("[" + matrix2Dint[2, 1..^0].GetValue(new int[] { 0 }) + "," + matrix2Dint[2, 1..^0].GetValue(new int[] { 1 }) + "," + matrix2Dint[2, 1..^0].GetValue(new int[] { 2 }) + "]");
            Console.WriteLine("==");
            Console.WriteLine("[10,11,12]");
            Console.WriteLine("[6,7,8]");
            Console.WriteLine("");

#endif

#if STAGE3
            Console.WriteLine("STAGE 3");
            double sum = matrix2Dint.Sum();
            int max = matrix2Dint.Max();
            int min = matrix2Dint.Min();
            double avg = matrix2Dint.Average();
            int[] flat = matrix2Dint.Flatten();

            Console.WriteLine(sum);
            Console.WriteLine("==");
            Console.WriteLine("136");
            Console.WriteLine("");

            Console.WriteLine(max);
            Console.WriteLine("==");
            Console.WriteLine("16");
            Console.WriteLine("");

            Console.WriteLine(min);
            Console.WriteLine("==");
            Console.WriteLine("1");
            Console.WriteLine("");

            Console.WriteLine(avg);
            Console.WriteLine("==");
            Console.WriteLine("8.5");
            Console.WriteLine("");

            matrix2D.AddConst(0.5);
            Console.WriteLine(matrix2D.Average());
            Console.WriteLine("==");
            Console.WriteLine("9");
            Console.WriteLine("");

            matrix2Duint.AddConst(2);
            Console.WriteLine(matrix2Duint.Average());
            Console.WriteLine("==");
            Console.WriteLine("10.5");
            Console.WriteLine("");

            matrix2Dint2.AddConst(-5);
            Console.WriteLine(matrix2Dint2.Average());
            Console.WriteLine("==");
            Console.WriteLine("3.5");
            Console.WriteLine("");

            Console.WriteLine(max);
            Console.WriteLine("Flatten array:");
            string flat_string = "";
            for (int i = 0; i < flat.Length; i++)
                flat_string += flat[i].ToString() + ",";
            flat_string = flat_string.Remove(flat_string.Length - 1);
            Console.WriteLine(flat_string);
            Console.WriteLine("");
#endif
        }
    }
}
