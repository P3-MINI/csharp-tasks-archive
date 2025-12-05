#define STAGE1
#define STAGE2
#define STAGE3

using System;

namespace Vector3D
{
    class Program
    {
        private static int TestCounter = 0;
        static void Test(object obj1, object obj2, bool equals = true)
        {
            if (obj1.Equals(obj2) == equals)
                Console.WriteLine($"  {++TestCounter:00}. OK! \"{obj1.ToString()}\" " + (equals ? "==" : "!=") + $" \"{obj2.ToString()}\"");
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  {++TestCounter:00}. Error! \"{obj1.ToString()}\" == \"{obj2.ToString()}\" is not {equals.ToString()}!");
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
            Console.WriteLine(" --- Stage 1 (1p) ---");
            //check constructor and properties
            Console.WriteLine("Constructor and properties tests");
#if STAGE1
            Vector3D A = new Vector3D(1, 2, 3);
            Test($"x = {A.X}, y = {A.Y}, z = {A.Z}", $"x = {1.0}, y = {2.0}, z = {3.0}");
            Vector3D A1 = new Vector3D(7, 0, 0);
            Test($"x = {A1.X}, y = {A1.Y}, z = {A1.Z}", $"x = {7.0}, y = {0.0}, z = {0.0}");
#endif
            //additional tests
            Console.WriteLine("Additional tests");
#if STAGE1
            try
            {
                Vector3D[] va = new Vector3D[1];
                va[0].X = 1;
                TestMsg("It works!");
            }
            catch (Exception)
            {
                TestMsg("This should works!", false);
            }
#endif
            //test add operator
            Console.WriteLine("Addition tests");
#if STAGE1
            Vector3D B = new Vector3D(3, 2, 1);
            Vector3D Sum = A + B;
            Test($"x = {Sum.X}, y = {Sum.Y}, z = {Sum.Z}", $"x = {4.0}, y = {4.0}, z = {4.0}");
            Test(new Vector3D(0, 0, 0) + new Vector3D(0, 0, 0), new Vector3D(0, 0, 0));
            Test(new Vector3D(1, 1, 1) + new Vector3D(-1, -1, -1), new Vector3D(0, 0, 0));
            Test(new Vector3D(5, 5, 5) + new Vector3D(-1, 2, 4), new Vector3D(4, 7, 9));
#endif
            //test Equals, == , !=
            Console.WriteLine("Equals tests");
#if STAGE1
            Test((A + B), (Sum));
            Test(A, Sum, false);
            Test(B != Sum, true);
            Test((A + B), Sum);
            Test(B, Sum, false);
#endif
            //test ToString()
            Console.WriteLine("ToString() tests");
#if STAGE1
            //Console.WriteLine("  " + A.ToString() + " [Student]");
            //Console.WriteLine("  Vector3D(X = 1, Y = 2, Z = 3) [Example]");
            Test(A.ToString(), "Vector3D(X = 1, Y = 2, Z = 3)");
            Test(B.ToString(), "Vector3D(X = 3, Y = 2, Z = 1)");
            Test(new Vector3D(1.23, 3.14, 2.72).ToString(), "Vector3D(X = 1.23, Y = 3.14, Z = 2.72)");
#endif
            Console.WriteLine();
            Console.WriteLine(" --- Stage 2 (2p) ---");
            //test scalar multiplication
            Console.WriteLine("Scalar multiplication tests");
#if STAGE2
            Test((A + 3 * B), (new Vector3D(10, 8, 6)));
            Vector3D C = (A + 3 * B) / 2;
            Test($"x = {C.X}, y = {C.Y}, z = {C.Z}", $"x = {5.0}, y = {4.0}, z = {3.0}");
            C *= 3;
            Test($"x = {C.X}, y = {C.Y}, z = {C.Z}", $"x = {15.0}, y = {12.0}, z = {9.0}");
#endif
            //test subtraction
            Console.WriteLine("Subtraction tests");
#if STAGE2
            Test((A), (Sum - B));
            Test(3 * A - 2 * A, A);
            Test(A + B, new Vector3D(4, 4, 4));
#endif
            //test minus vector
            Console.WriteLine("Minus operator tests");
#if STAGE2
            Test((-A), (new Vector3D(-1, -2, -3)));
            Test((-A), (new Vector3D(1, 2, 3)), false);
            Test(Vector3D.Zero, (-Vector3D.Zero));
#endif

            //Test static class objects
            Console.WriteLine("Static class object tests");
#if STAGE2
            Test(Vector3D.Zero, new Vector3D(0, 0, 0));
            Test(Vector3D.One, new Vector3D(1, 1, 1));
            Test(Vector3D.UnitX, new Vector3D(1, 0, 0));
            Test(Vector3D.UnitY, new Vector3D(0, 1, 0));
            Test(Vector3D.UnitZ, new Vector3D(0, 0, 1));
#endif
            //Test Length/Length2
            Console.WriteLine("Length and Length2 tests");
#if STAGE2
            Test(A.Length, Math.Sqrt(14));
            Test(A.Length2, 14.0);
            Test((-A).Length, Math.Sqrt(14));
            Test((-A).Length2, 14.0);
            Test(Vector3D.Zero.Length, 0.0);
            Test(Vector3D.UnitX.Length, 1.0);
            Test(Vector3D.One.Length, Math.Sqrt(3));
#endif
            //Test Dot
            Console.WriteLine("Dot product tests");
#if STAGE2
            Test(Vector3D.Dot(A, B), 10.0);
            Test(A.Dot(B), 10.0);
            Test(B.Dot(A), 10.0);
            Test(Sum.Dot(Sum), Sum.Length2);
#endif
            Console.WriteLine();
            Console.WriteLine(" --- Stage 3 (2p) ---");
            //Test Normalize/Normalized
#if STAGE3
            Console.WriteLine("Normalize/Normalized tests");
            Vector3D A2 = A.Normalized();
            Test(A2.Length, 1.0);
            Test(A.Length, Math.Sqrt(14));
            Test(Vector3D.Zero.Normalized().Length, 0.0);
            A.Normalize();
            Test(A.Length, 1.0);
            A *= Math.Sqrt(14);
            Test($"x = {A.X}, y = {A.Y}, z = {A.Z}", $"x = {1.0}, y = {2.0}, z = {3.0}");
#endif
            //test indexer
            Console.WriteLine("Indexer tests");
#if STAGE3
            try
            {
                A[1] = 0;
                Test($"A[0] = {A[0]}, A[1] = {A[1]}, A[2] = {A[2]}", $"A[0] = {1.0}, A[1] = {0.0}, A[2] = {3.0}");
                A[0] = 1;
                A[2] = 3;
                A[1] = 2;
            }
            catch (Exception)
            {
                TestMsg("Exception has been thrown", false);
            }
            try
            {
                Test(A[-1], double.NaN);
                TestMsg("Should throw IndexOutOfRangeException!!!", false);
            }
            catch (IndexOutOfRangeException)
            {
                TestMsg("Exception has been thrown");
            }
            try
            {
                Test(A[3], double.NaN);
                TestMsg("Should throw IndexOutOfRangeException!!!", false);
            }
            catch (IndexOutOfRangeException)
            {
                TestMsg("Exception has been thrown");
            }
            try
            {
                A[3] = 0;
                TestMsg("Should throw IndexOutOfRangeException!!!", false);
            }
            catch (IndexOutOfRangeException)
            {
                TestMsg("Exception has been thrown");
            }
            try
            {
                Test(A[1337], double.NaN);
                TestMsg("Should throw IndexOutOfRangeException!!!", false);
            }
            catch (IndexOutOfRangeException)
            {
                TestMsg("Exception has been thrown");
            }
            try
            {
                A[-1] = 0;
                TestMsg("Should throw IndexOutOfRangeException!!!", false);
            }
            catch (IndexOutOfRangeException)
            {
                TestMsg("Exception has been thrown");
            }

            Test($"B[0] = {B[0]}, B[1] = {B[1]}, B[2] = {B[2]}", $"B[0] = {3.0}, B[1] = {2.0}, B[2] = {1.0}");
            Test(A2[0] == 0.2672612419124244 && A2[1] == 0.53452248382484879 && A2[2] == 0.80178372573727319, true);
#endif
            //test deconstructor
            Console.WriteLine("Deconstructor tests");
#if STAGE3
            var (x, y, z) = A;
            Test($"x = {x}, y = {y}, z = {z}", $"x = {1.0}, y = {2.0}, z = {3.0}");
            (x, y, z) = Sum;
            Test($"x = {x}, y = {y}, z = {z}", $"x = {4.0}, y = {4.0}, z = {4.0}");
#endif
            //test converters
            Console.WriteLine($"Converters tests");
#if STAGE3
            A = (1.3, 2.4, 1.1);
            Test(A, new Vector3D(1.3, 2.4, 1.1));

            A = (1, 2, 3);
            Test(A, new Vector3D(1, 2, 3));

            (x, y, z) = ((double, double, double))Sum;
            Test($"x = {x}, y = {y}, z = {z}", $"x = {4.0}, y = {4.0}, z = {4.0}");
#endif

            //test factory methods MakeUnit
            Console.WriteLine("MakeUnit tests");
#if STAGE3
            Vector3D Unit = Vector3D.MakeUnit(1, -33, 7);
            Test(Unit.Length, 1.0);
            Test(Unit.AlmostEquals(new Vector3D(0.029630442547298, -0.977804604060834, 0.207413097831086)), true);
#endif
            //test AlmostEquals 
            Console.WriteLine("AlmostEquals tests");
#if STAGE3
            Vector3D AlmostUnit = new Vector3D(0.0296304, -0.977804, 0.20741309);
            Test(!Unit.AlmostEquals(AlmostUnit), true);
            Test(Unit.AlmostEquals(AlmostUnit, 1e-6), true);
            Test(Vector3D.MakeUnit(1, 2, 3) == A.Normalized(), true);
#endif
            //test AlmostZero
            Console.WriteLine("AlmostZero tests");
#if STAGE3
            Vector3D AlmostZero = Vector3D.One - new Vector3D(0.99999999, 0.99999999, 0.99999999);
            Test(!Vector3D.Zero.Equals(AlmostZero), true);
            Test(AlmostZero.AlmostZero(), true);
            Test(Vector3D.Zero.AlmostZero(), true);
            Test(new Vector3D(1e-8, -1e-8, -1e-9).AlmostZero(), true);
#endif
            //test factory methods LERP
            Console.WriteLine("LERP tests");
#if STAGE3
            Vector3D mid = Vector3D.LERP(A, Sum);
            Test(Vector3D.LERP(A, B), new Vector3D(2, 2, 2));
            Test(Vector3D.LERP(A, Sum), new Vector3D(2.5, 3, 3.5));
            Test(Vector3D.LERP(B, C), new Vector3D(9, 7, 5));
            Test(Vector3D.LERP(new Vector3D(-2, -1, 0), new Vector3D(-5, 3, 0)), new Vector3D(-3.5, 1, 0));
            Test(Vector3D.LERP(new Vector3D(-2, -1, 0), new Vector3D(-5, 3, 0)), new Vector3D(-3.5, 1, 0));
            Test(Vector3D.LERP(new Vector3D(-2, -1, 0), new Vector3D(-5, 3, 0), 0.25), new Vector3D(-4.25, 2, 0));
            Test(Vector3D.LERP(new Vector3D(-2, -1, 0), new Vector3D(-5, 3, 0), 0.75), new Vector3D(-2.75, 0, 0));
            Test(Vector3D.LERP(new Vector3D(-2, -1, 0), new Vector3D(-5, 3, 0), 0.9).AlmostEquals(new Vector3D(-2.3, -0.6, 0)), true);
#endif
            //test IsNaN
            Console.WriteLine("IsNaN tests");
#if STAGE3
            Test(A.IsNaN(), false);
            Vector3D N = new Vector3D(1, 0, 2) / 0;
            Test(Vector3D.IsNaN(N), true);
            Test(new Vector3D(1, 3, 2).IsNaN(), false);
            Test(new Vector3D(1, Math.Sqrt(-1), 2).IsNaN(), true);
#endif

            Console.WriteLine("\nTest finished!\n");
        }

    }
}