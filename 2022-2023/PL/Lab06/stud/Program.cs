// #define STAGE1
// #define STAGE2
// #define STAGE3
// #define STAGE4

using System;

namespace Lab06
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
            Console.WriteLine(" --- Stage 1 (2 Pts) ---");

#if STAGE1
            Console.WriteLine("Constructor and Properties Tests:");
            Vector3D A = new Vector3D(1, 2, 3);
            Test($"x = {A.X}, y = {A.Y}, z = {A.Z}", $"x = {1.0}, y = {2.0}, z = {3.0}");
            Vector3D B = new Vector3D(7, 0, 0);
            Test($"x = {B.X}, y = {B.Y}, z = {B.Z}", $"x = {7.0}, y = {0.0}, z = {0.0}");

            try
            {
                Vector3D[] vectors = new Vector3D[1];
                vectors[0].X = 1;
                TestMsg("It Works!");
            }
            catch (Exception)
            {
                TestMsg("This Should Work!", false);
            }

            Console.WriteLine("Equals Tests:");

            Test(A, B, false);
            Test(A == B, false);
            Test(A != B, true);
            Vector3D C = B;
            Test(C, B, true);
            Test(C == B, true);
            Test(C != B, false);

            Console.WriteLine("ToString() Tests:");

            Test(A.ToString(), "Vector3D(X = 1, Y = 2, Z = 3)");
            Test(B.ToString(), "Vector3D(X = 7, Y = 0, Z = 0)");
            Test(new Vector3D(1.23, 3.14, 2.72).ToString(), $"Vector3D(X = {1.23}, Y = {3.14}, Z = {2.72})");
#endif
            Console.WriteLine();
            Console.WriteLine(" --- Stage 2 (1 Pts) ---");
#if STAGE2
            Console.WriteLine("Static Class Object Tests:");
            Test(Vector3D.Zero, new Vector3D(0, 0, 0));
            Test(Vector3D.One, new Vector3D(1, 1, 1));
            Test(Vector3D.UnitX, new Vector3D(1, 0, 0));
            Test(Vector3D.UnitY, new Vector3D(0, 1, 0));
            Test(Vector3D.UnitZ, new Vector3D(0, 0, 1));

            Console.WriteLine("Length Tests:");

            Test(A.Length, Math.Sqrt(14));
            Test(Vector3D.Zero.Length, 0.0);
            Test(Vector3D.UnitX.Length, 1.0);
            Test(Vector3D.One.Length, Math.Sqrt(3));

            Console.WriteLine("Dot Product Tests:");

            Test(Vector3D.Dot(A, B), 7.0);
#endif
            Console.WriteLine();
            Console.WriteLine(" --- Stage 3 (1 Pts) ---");
#if STAGE3
            Console.WriteLine("Indexer Tests:");
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
                TestMsg("Exception Has Been Thrown!");
            }
            try
            {
                Test(A[-1], double.NaN);
                TestMsg("Should Throw IndexOutOfRangeException!", false);
            }
            catch (IndexOutOfRangeException)
            {
                TestMsg("Exception Has Been Thrown!");
            }
            try
            {
                Test(A[3], double.NaN);
                TestMsg("Should Throw IndexOutOfRangeException!", false);
            }
            catch (IndexOutOfRangeException)
            {
                TestMsg("Exception Has Been Thrown!");
            }
            try
            {
                A[3] = 0;
                TestMsg("Should Throw IndexOutOfRangeException!", false);
            }
            catch (IndexOutOfRangeException)
            {
                TestMsg("Exception Has Been Thrown!");
            }
            try
            {
                Test(A[1337], double.NaN);
                TestMsg("Should Throw IndexOutOfRangeException!", false);
            }
            catch (IndexOutOfRangeException)
            {
                TestMsg("Exception Has Been Thrown!");
            }
            try
            {
                A[-1] = 0;
                TestMsg("Should Throw IndexOutOfRangeException!", false);
            }
            catch (IndexOutOfRangeException)
            {
                TestMsg("Exception Has Been Thrown!");
            }

            B[1] = 2.0; B[2] = 3.0;
            Test($"B[0] = {B[0]}, B[1] = {B[1]}, B[2] = {B[2]}", $"B[0] = {7.0}, B[1] = {2.0}, B[2] = {3.0}");

            Console.WriteLine("Deconstructor Tests:");

            var (x, y, z) = A;
            Test($"X = {x}, Y = {y}, Z = {z}", $"X = {1.0}, Y = {2.0}, Z = {3.0}");
            (x, y, z) = B;
            Test($"X = {x}, Y = {y}, Z = {z}", $"X = {7.0}, Y = {2.0}, Z = {3.0}");

            Console.WriteLine($"Converters Tests:");

            A = (1.3, 2.4, 1.1);
            Test(A, new Vector3D(1.3, 2.4, 1.1));

            A = (1, 2, 3);
            Test(A, new Vector3D(1, 2, 3));
#endif
            Console.WriteLine();
            Console.WriteLine(" --- Stage 4 (1 Pts) ---");
#if STAGE4
            Console.WriteLine("Normalize Tests:");
            Vector3D A2 = A.Normalized();
            Test(A2.Length, 1.0);
            Test(A.Length, Math.Sqrt(14));
            Test(Vector3D.Zero.Normalized().Length, 0.0);
            Test($"x = {A.X}, y = {A.Y}, z = {A.Z}", $"x = {1.0}, y = {2.0}, z = {3.0}");
            Test(A2[0] == 0.2672612419124244 && A2[1] == 0.53452248382484879 && A2[2] == 0.80178372573727319, true);

            Console.WriteLine("MakeUnit Tests:");

            Vector3D Unit = Vector3D.MakeUnit(1, -33, 7);
            Test(Unit.Length, 1.0);
            Test(Unit.AlmostEquals(new Vector3D(0.029630442547298, -0.977804604060834, 0.207413097831086)), true);

            Console.WriteLine("AlmostEquals Tests:");

            Vector3D AlmostUnit = new Vector3D(0.0296304, -0.977804, 0.20741309);
            Test(Unit.AlmostEquals(AlmostUnit), false);
            Test(Unit.AlmostEquals(AlmostUnit, 1e-6), true);
            Test(Vector3D.MakeUnit(1, 2, 3) == A.Normalized(), true);

            Console.WriteLine("AlmostZero Tests:");

            Vector3D AlmostZero = new Vector3D(0.00000001, 0.00000001, 0.00000001);
            Test(!Vector3D.Zero.Equals(AlmostZero), true);
            Test(Vector3D.Zero.AlmostEquals(AlmostZero), true);

            Console.WriteLine("IsNaN Tests:");

            Test(A.IsNaN(), false);
            Test(new Vector3D(1, 3, 2).IsNaN(), false);
            Test(new Vector3D(1, Math.Sqrt(-1), 2).IsNaN(), true);
#endif
            Console.WriteLine("\nTest Finished!\n");
        }

    }
}
