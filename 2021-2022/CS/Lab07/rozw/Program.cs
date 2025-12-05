#nullable enable

#define STAGE1_NULLABLE_TYPES
//#define STAGE2A_OPERATORS
#define STAGE2B_COMPARISONS
#define STAGE3_CONVERSIONS
#define STAGE4_PROPERTIES
#define STAGE5_INTERFACES_YIELD

using System;

namespace EN_Lab_07
{
    public static class Program
    {
        public static void Main()
        {
#if STAGE1_NULLABLE_TYPES
            Console.WriteLine("\nStage 1 - Nullable and non-nullable types");
            NullableTypesExample1();
            Console.WriteLine();
            NullableTypesExample2();
#endif

            Point2D p1 = new Point2D(2, 4);
            Point2D p2 = new Point2D(3, 1);

#if STAGE2A_OPERATORS
            double scalar = 5.0;
            Console.WriteLine("\nStage 2A - Operators");
            Console.WriteLine();
            BasicOperatorsTest(p1, p1, scalar);
            Console.WriteLine();
#endif
#if STAGE2B_COMPARISONS
            Console.WriteLine("\nStage 2B - Comparisons");
            EqualityOperatorTest(p1, p1);
            Console.WriteLine();
            EqualityOperatorTest(p1, p2);
#endif
#if STAGE3_CONVERSIONS
            Console.WriteLine("\nStage 3 - Conversions");
            Console.WriteLine();
            ConversionsTest();
#endif
#if STAGE4_PROPERTIES
            Console.WriteLine("\nStage 4 - Properties");
            Console.WriteLine();
            PropertiesTest(p1, p2);
#endif

            InterfacesYieldExample();
        }
#if STAGE1_NULLABLE_TYPES
        private static void fun_nn(string s)
        {
            Console.WriteLine($"Do something with string: {s}");
        }

        private static void fun_n(string? s)
        {
            Console.WriteLine($"Do something with string: {s}");
        }

        public static void NullableTypesExample1()
        {
            string snn1 = "non-empty non-nullable string";    // OK
            string snn2 = null;                               // warning - null assigned to non-nullable string variable

            string? sn1 = "non-empty nullable string";        // OK
            string? sn2 = null;                               // OK - null assigned to nullable string variable

            Console.WriteLine("----------");
            Console.WriteLine(snn1);                          // OK
            Console.WriteLine(snn2);                          // no warnings because WriteLine method can handle null value
            Console.WriteLine(sn1);                           // OK
            Console.WriteLine(sn2);                           // no warnings because WriteLine method can handle null value
            Console.WriteLine("----------");
            fun_nn(snn1);                                     // OK
            fun_nn(snn2);                                     // warning - parameter shouldn't be null
            fun_nn(sn1);                                      // OK
            fun_nn(sn2);                                      // warning - parameter shouldn't be null
            Console.WriteLine("----------");
            fun_n(snn1);                                      // OK
            fun_n(snn2);                                      // OK -- parameter can be null
            fun_n(sn1);                                       // OK
            fun_n(sn2);                                       // OK -- parameter can be null
            Console.WriteLine("----------");
            if (snn2 != null)
                fun_nn(snn2);                                 // no warning because compiler finds that method will be invoked only if parameter isn't null
            if (sn2 != null)
                fun_nn(sn2);                                  // no warning because compiler finds that method will be invoked only if parameter isn't null
            Console.WriteLine("----------");
            fun_nn(snn2!);                                    // no warning because of using ! null-forgiving operator
            fun_nn(sn2!);                                     // no warning because of using ! null-forgiving operator
            Console.WriteLine("----------");
        }

        private static string NullablePointToString(Point2D? p)
        {
            // p.HasValue ? p.ToString() : "NULL POINT";            // warning
            return p.HasValue ? p.Value.ToString() : "NULL POINT";
        }

        public static void NullableTypesExample2()
        {
            Point2D? p = new Point2D(1, 1);
            Point2D? np = null;
            Point2D? nnp = new Point2D(2, 4);

            Console.WriteLine($"p = {NullablePointToString(p)}");
            Console.WriteLine($"np = {NullablePointToString(np)}");
            Console.WriteLine($"nnp = {NullablePointToString(nnp)}");

            Point2D? newP = null;

            newP = np ?? p;
            Console.WriteLine($"newP = {NullablePointToString(newP)}   // newP = np ?? p");
            newP = nnp ?? p;
            Console.WriteLine($"newP = {NullablePointToString(newP)}   // newP = nnp ?? p");

            var npx = np?.X;
            var npy = np?.Y;

            var nnpx = nnp?.X;
            var nnpy = nnp?.Y;

            Console.WriteLine($"npx = {npx}, npy = {npy}");
            Console.WriteLine($"nnpx = {nnpx}, nnpy = {nnpy}");
        }
#endif
#if STAGE2A_OPERATORS
        public static void BasicOperatorsTest(Point2D p1, Point2D p2, double scalar)
        {
            Console.WriteLine($"Points P1 = {p1}, P2 = {p2}, SCALAR = {scalar}:");
            Console.WriteLine($"P1 + P2 equals {p1 + p2}");
            Console.WriteLine($"P1 - P2 equals {p1 - p2}");
            Console.WriteLine($"P1 * SCALAR equals {p1 * scalar}");
            Console.WriteLine($"SCALAR * P1 equals {scalar * p1}");
            Console.WriteLine($"P2 / SCALAR equals {p2 / scalar}");
            Console.WriteLine($"-P2 equals {-p2}");
        }
#endif
#if STAGE2B_COMPARISONS
        public static void EqualityOperatorTest(Point2D p1, Point2D p2)
        {
            Console.WriteLine($"Points P1 = {p1} and P2 = {p2}:");
            Console.WriteLine($"P1 == P2 equals {p1 == p2}");
            Console.WriteLine($"P1 != P2 equals {p1 != p2}");
            Console.WriteLine($"P1.Equals(P2) equals {p1.Equals(p2)}");
            Console.WriteLine($"P1.GetHashCode() == P2.GetHashCode() equals {p1.GetHashCode() == p2.GetHashCode()}");
        }
#endif
#if STAGE3_CONVERSIONS
        public static void ConversionsTest()
        {
            Point2D p = (1, 5);
            (var x, var y) = ((double, double))p;
            Console.WriteLine("Point2D can be created implicitly from tuple (x,y) = (1, 5).");
            Console.WriteLine("We can force explicit to convert Point2D to tuple by \"((double, double))p\".");

            Console.WriteLine($"p = {p}");
            Console.WriteLine($"(x,y) = ({x},{y})");

            Console.WriteLine("Implicit conversion of Point2D to Tuple will cause a compilation error.");
            //(var x, var y) = p;

            Console.WriteLine();

            Console.WriteLine("Double array (double[]) can be created implicitly from Point2D.");
            Console.WriteLine("We can also force explicit conversion from double array to Point2D.");
            Console.WriteLine("An explicit conversion will succeedeed only when array contains 2 items. In other case it will return Point2D(0, 0).");
            Console.WriteLine("The explicit conversions may cause an error, throw an exception or can lead to data loss.");

            double[] arr = p;
            double[] arr_ok = new double[] { 2, 4 };
            double[] arr_nok = new double[] { 1 };

            var p_ok = (Point2D)arr_ok!;
            var p_nok = (Point2D)arr_nok!;

            Console.WriteLine($"Array from {p} = [{string.Join(",", arr)}]");
            Console.WriteLine($"Point2D from array [{string.Join(",", arr_ok)}] = {p_ok}");
            Console.WriteLine($"Point2D from array [{string.Join(",", arr_nok)}] = {p_nok} - DATA LOSE");
        }
#endif
#if STAGE4_PROPERTIES
        public static void PropertiesTest(Point2D p1, Point2D p2)
        {
            Console.WriteLine($"Points P1 = {p1} and P2 = {p2}:");
            Console.WriteLine($"P1.X = {p1.X}, P1.Y = {p1.Y}");
            Console.WriteLine($"P2.X = {p2.X}, P1.Y = {p2.Y}");

            Console.WriteLine($"Distance between P1 and [0,0] equal {p1.Length}");
            Console.WriteLine($"Distance between P1 and P2 equal {(p1 - p2).Length:f3}");

            Console.WriteLine($"P1.X can be accessed with \"P1[0]\" -> {p1[0]}");
            Console.WriteLine($"P1.Y can be accessed with \"P1[1]\" -> {p1[1]}");

            Console.WriteLine($"P1.X can be set with \"P1[0] = 2\"");
            Console.WriteLine($"P1.Y can be set with \"P1[1] = 5\"");

            p1[0] = 2;
            p1[1] = 5;

            Console.WriteLine($"Point P1 = {p1}");
            Console.WriteLine("");
            Console.WriteLine("We can define static properties and use them like static fields.");
            Console.WriteLine($"Point Zero = {Point2D.Zero}");
            Console.WriteLine($"Point UnitX = {Point2D.UnitX}");
            Console.WriteLine($"Point UnitY = {Point2D.UnitY}");
        }
#endif
#if STAGE5_INTERFACES_YIELD
        public static void InterfacesYieldExample()
        {
            var vertices = new Point2D[]
            {
                new Point2D (1, 1),
                new Point2D (5, 1),
                new Point2D (1, 3)
            };

            Polygon p1 = new Polygon(vertices);
            IGeometryObject c1 = new Circle(new Point2D(0, 0), 1);

            IGeometryObject p2 = p1;        // implicit conversion
            Circle c2 = (Circle)c1;         // explicit conversion

            //p1.PrintGeometryObject();       // error, method is implemented in the interface
            p2.PrintGeometryObject();         // C# 8.0

            c1.PrintGeometryObject();         // C# 8.0
            //c2.PrintGeometryObject();       // error, method is implemented in the interface

            var pt1 = new Point2D(0, 0);
            var pt2 = new Point2D(1, 1);

            var d = IGeometryObject.DistanceBetweenPoints(pt1, pt2);            // C# 8.0
            Console.WriteLine($"Distance between {pt1} and {pt2} equals {d}.");

            // we can iterate through points of Polygon using foreach
            foreach (var p in p1)
            {
                Console.WriteLine(p);
            }

            // error, because only Polygon class implement IEnumerable interface
            //foreach (var p in p2)
            //{
            //    Console.WriteLine(p);
            //}
        }
    }
#endif
}
