#define STAGE1
#define STAGE2
#define STAGE3
#define STAGE4

using System;
using System.Linq;

namespace Lab07
{
    class Program
    {
        private static int TestCounter;

        static void Test(object obj1, object obj2, string spacing = "", bool equals = true)
        {
            if (obj1.Equals(obj2) == equals)
                Console.WriteLine($"  {++TestCounter:00}. OK! {spacing}{obj1}{spacing} " + (equals ? "==" : "!=") + $" {spacing}{obj2}");
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  {++TestCounter:00}. Error! {spacing}{obj1}{spacing} == {spacing}{obj2}{spacing} is not {equals.ToString()}!");
            }
            Console.ResetColor();
        }

        static void TestMsg(string message, bool ok = true)
        {
            if (ok)
                Console.WriteLine($"  {++TestCounter:00}. OK! {message}");
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"  {++TestCounter:00}. Error! {message}");
                Console.ResetColor();
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine(" --------- Stage 1 (2 Pts) ---------");

#if STAGE1
            Console.WriteLine("Constructor and properties tests:");
            Matrix a = new Matrix(new double[,] { { 1, 2, 3, 4 }, {5, 6, 7, 8}, {9, 10, 11, 12} });
            Test(a.ToString(), "|   1.0    2.0    3.0    4.0 |\n|   5.0    6.0    7.0    8.0 |\n|   9.0   10.0   11.0   12.0 |", "\n");
            Test(a.M, 3);
            Test(a.N, 4);

            Matrix b = new Matrix(4, 3);
            Test(b.ToString(), "|   0.0    0.0    0.0 |\n|   0.0    0.0    0.0 |\n|   0.0    0.0    0.0 |\n|   0.0    0.0    0.0 |", "\n");
            Test(b.M, 4);
            Test(b.N, 3);
            
            Console.WriteLine("Indexers tests:");
            Test(a[.., ..].ToString(), "|   1.0    2.0    3.0    4.0 |\n|   5.0    6.0    7.0    8.0 |\n|   9.0   10.0   11.0   12.0 |", "\n");
            Test(a[1..^1, 1..^1].ToString(), "|   6.0    7.0 |", "\n");
            Test(a[1..^1, ..].ToString(), "|   5.0    6.0    7.0    8.0 |", "\n");
            Test(a[.., 1..^1].ToString(), "|   2.0    3.0 |\n|   6.0    7.0 |\n|  10.0   11.0 |", "\n");
            Test(a[^1, 1..^1].ToString(), "|  10.0   11.0 |", "\n");
            Test(a[.., ^1].ToString(), "|   4.0 |\n|   8.0 |\n|  12.0 |", "\n");
            a[1.., 1..^1] = b[1..3, 1..];
            Test(a.ToString(), "|   1.0    2.0    3.0    4.0 |\n|   5.0    0.0    0.0    8.0 |\n|   9.0    0.0    0.0   12.0 |", "\n");
            b[0, 0] = a[0, 0];
            b[0, 1] = a[1, 0];
            b[1, 0] = a[0, 1];
            b[^1, 2] = a[2, ^1];
            Test(b.ToString(), "|   1.0    5.0    0.0 |\n|   2.0    0.0    0.0 |\n|   0.0    0.0    0.0 |\n|   0.0    0.0   12.0 |", "\n");
            try
            {
                a[.., ..] = b;
            }
            catch (Exception)
            {
                TestMsg("Exception Has Been Thrown!");
            }
            try
            {
                a[-1, -1] = -1;
            }
            catch (Exception)
            {
                TestMsg("Exception Has Been Thrown!");
            }
            try
            {
                a[6, 6] = -1;
            }
            catch (Exception)
            {
                TestMsg("Exception Has Been Thrown!");
            }
#endif
            
            Console.WriteLine(" --------- Stage 2 (1 Pts) ---------");

#if STAGE2
            Console.WriteLine("Casting tests:");
            Matrix c = new Matrix(new double[,] { { 1, 2, 3, 4 }, {5, 6, 7, 8}, {9, 10, 11, 12} });
            Test(c.ToString(), "|   1.0    2.0    3.0    4.0 |\n|   5.0    6.0    7.0    8.0 |\n|   9.0   10.0   11.0   12.0 |", "\n");
            var cArray = (double[,])c;
            var cStr = string.Join(",", cArray.OfType<double>()
                .Select((value, index) => new {value, index})
                .GroupBy(x => x.index / cArray.GetLength(1))
                .Select(x => $"{{{string.Join(",", x.Select(y => y.value))}}}"));
            Test(cStr, "{1,2,3,4},{5,6,7,8},{9,10,11,12}");

            Console.WriteLine("Operators ==, !=:");
            Matrix d = new Matrix(new double[,] { { 1, 2, 3, 4 }, {5, 6, 7, 8}, {9, 10, 11, 12} });
            Matrix e = new Matrix(new double[,] { { 1, 2, 3, 4 }, {5, 6, 7, 8}, {9, 10, 11, 12}, {13, 14, 15, 16} });
            Matrix f = new Matrix(new double[,] { { 1, 2, 3, 4 }, {5, 6, 7, 8}, {9, 10, 11, 12}, {13, 14, 15, 16} });
            Matrix g = new Matrix(new double[,] { { 1, 5, 9, 13 }, {2, 6, 10, 14}, {3, 7, 11, 15}, {4, 8, 12, 16} });

            Test(d != e, true);
            Test(d == d, true);
            Test(e == f, true);
            Test(f != g, true);
            Test(g.Equals(null), false);
#endif

            Console.WriteLine(" --------- Stage 3 (1 Pts) ---------");

#if STAGE3
            Console.WriteLine("Operator +:");
            Matrix h = new Matrix(new double[,] { { 1, 2, 3, 4}, {3, 4, 5, 6}, {8, 7, 6, 5} });
            Matrix i = new Matrix(new double[,] { { -3, 2, -3, 4}, {3, 6, 3, 8}, {-3, 10, -3, 12} });
            Matrix j = new Matrix(new double[,] { { -3, 2, -3}, {3, 6, 3} });
            Test((h+i).ToString(), "|  -2.0    4.0    0.0    8.0 |\n|   6.0   10.0    8.0   14.0 |\n|   5.0   17.0    3.0   17.0 |", "\n");
            Test(h.ToString(), "|   1.0    2.0    3.0    4.0 |\n|   3.0    4.0    5.0    6.0 |\n|   8.0    7.0    6.0    5.0 |", "\n");
            Test(i.ToString(), "|  -3.0    2.0   -3.0    4.0 |\n|   3.0    6.0    3.0    8.0 |\n|  -3.0   10.0   -3.0   12.0 |", "\n");
            try
            {
                var k = i + j;
            }
            catch (Exception)
            {
                TestMsg("Exception Has Been Thrown!");
            }
            Console.WriteLine("Operator !:");
            Matrix l = new Matrix(new double[,] { { 1, 2, 3, 4}, {3, 4, 5, 6}, {8, 7, 6, 5} });
            var m = !l;
            Test(l.ToString(), "|   1.0    2.0    3.0    4.0 |\n|   3.0    4.0    5.0    6.0 |\n|   8.0    7.0    6.0    5.0 |", "\n");
            Test(m.ToString(), "|   1.0    3.0    8.0 |\n|   2.0    4.0    7.0 |\n|   3.0    5.0    6.0 |\n|   4.0    6.0    5.0 |", "\n");
            Console.WriteLine("Operator |:");
            Matrix n = new Matrix(new double[,] { { 1, 2, 3, 4}, {5, 6, 7, 8}, {9, 10, 11, 12} });
            Matrix o = new Matrix(new double[,] { { 1, 2, 3}, {3, 4, 5}, {8, 7, 6} });
            Matrix p = new Matrix(new double[,] { { 1, 2, 3, 4}, {3, 4, 5, 6} });
            var r = n | o;
            Test(r.ToString(), "|   1.0    2.0    3.0    4.0    1.0    2.0    3.0 |\n|   5.0    6.0    7.0    8.0    3.0    4.0    5.0 |\n|   9.0   10.0   11.0   12.0    8.0    7.0    6.0 |", "\n");
            try
            {
                var s = o | p;
            }
            catch (Exception)
            {
                TestMsg("Exception Has Been Thrown!");
            }      
#endif

            Console.WriteLine(" --------- Stage 4 (1 Pts) ---------");

#if STAGE4
            Console.WriteLine("Operators *, /:");
            Matrix t = new Matrix(new double[,] { { 1, 2, 3, 4}, {3, 4, 5, 6}, {8, 7, 6, 5} });
            Matrix u = new Matrix(new double[,] { { 2, 0, 1, 1 }, { 1, 0, -1, 2 }, { -2, 1, 3, 3 }, { -7, 3, 3, 3 } });
            var v = t * -2;
            Test(v.ToString(), "|  -2.0   -4.0   -6.0   -8.0 |\n|  -6.0   -8.0  -10.0  -12.0 |\n| -16.0  -14.0  -12.0  -10.0 |", "\n");
            var w = -2 * t;
            Test(w.ToString(), "|  -2.0   -4.0   -6.0   -8.0 |\n|  -6.0   -8.0  -10.0  -12.0 |\n| -16.0  -14.0  -12.0  -10.0 |", "\n");
            Test((w / 2).ToString(), "|  -1.0   -2.0   -3.0   -4.0 |\n|  -3.0   -4.0   -5.0   -6.0 |\n|  -8.0   -7.0   -6.0   -5.0 |", "\n");
            try
            {
                var x = u * t;
            }
            catch (Exception)
            {
                TestMsg("Exception Has Been Thrown!");
            }
            var y = t * u;
            Test(y.ToString(), "| -30.0   15.0   20.0   26.0 |\n| -42.0   23.0   32.0   44.0 |\n| -24.0   21.0   34.0   55.0 |", "\n");
            var z = u * u;
            Test(z.ToString(), "|  -5.0    4.0    8.0    8.0 |\n| -10.0    5.0    4.0    4.0 |\n| -30.0   12.0   15.0   18.0 |\n| -38.0   12.0    8.0   17.0 |", "\n");
            Test(u.ToString(), "|   2.0    0.0    1.0    1.0 |\n|   1.0    0.0   -1.0    2.0 |\n|  -2.0    1.0    3.0    3.0 |\n|  -7.0    3.0    3.0    3.0 |", "\n");
            try
            {
                var _ = u / 0;
            }
            catch (Exception)
            {
                TestMsg("Exception Has Been Thrown!");
            }
            
            Console.WriteLine("Solving system of equations:");
            Matrix A = new[,] { {0.02, 0.01, 0, 0}, {1, 2, 1, 0}, {0, 1, 2, 1}, {0, 0, 100, 200} };
            Matrix B = new[,] { { 0.02 }, { 1 }, { 4 }, { 800 } };
            Matrix C = new[,] { {0.0, 0, 0, 0}, {1, 2, 1, 0}, {0, 1, 2, 1}, {0, 0, 100, 200} };
            Matrix D = new[,] { { 0.04, 0.16 }, { 2, 2 }, { 8, 4 }, { 200, 100 } };
            Matrix E = new[,] { {0.02, 0.01, 0}, {1, 2, 1}, {0, 1, 2}, {0, 0, 100} };
            Test((A / B).ToString(), "|   1.0 |\n|   0.0 |\n|   0.0 |\n|   4.0 |", "\n");
            try
            {
                var _ = C / B;
            }
            catch (Exception)
            {
                TestMsg("Exception Has Been Thrown!");
            }
            try
            {
                var _ = B / A;
            }
            catch (Exception)
            {
                TestMsg("Exception Has Been Thrown!");
            }
            try
            {
                var _ = E / B;
            }
            catch (Exception)
            {
                TestMsg("Exception Has Been Thrown!");
            }
            Test((A / (B | D)).ToString(), "|   1.0    4.8   13.0 |\n|   0.0   -5.6  -10.0 |\n|   0.0    8.4    9.0 |\n|   4.0   -3.2   -4.0 |", "\n");
#endif
        }
    }
}