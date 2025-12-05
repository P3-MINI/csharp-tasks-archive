#define STAGE1
#define STAGE2
#define STAGE3
#define STAGE4

using System;
using System.Globalization;

namespace Lab8
{
    class Program
    {
        private static int TestCounter = 0;

        static void Test(object obj1, object obj2, bool equals = true)
        {
            if (obj1.Equals(obj2) == equals)
                Console.WriteLine($"  {++TestCounter:00}. OK! \"{obj1}\" " + (equals ? "==" : "!=") + $" \"{obj2}\"");
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  {++TestCounter:00}. Error! \"{obj1}\" == \"{obj2}\" is not {equals}!");
            }
            Console.ResetColor();
        }

        static void TestMsg(string message, bool ok = true)
        {
            if (ok)
                Console.WriteLine($"  {++TestCounter:00}. OK! {message}");
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  {++TestCounter:00}. Error! {message}");
            }
            Console.ResetColor();
        }

        static void Main(string[] args)
        {
#if STAGE1

            Console.WriteLine("\nSTAGE 1 - Constructors, properties, Reciprocal()");

            Console.WriteLine("\nTest constructors");

            Fraction frac;
            frac = new Fraction(2);
            Test($"{frac}", "[2]");

            frac = new Fraction(1, 3);
            Test($"{frac}", "[1/3]");

            frac = new Fraction(2, 4);
            Test($"{frac}", "[1/2]");

            frac = new Fraction(-7, 14);
            Test($"{frac}", "-[1/2]");

            frac = new Fraction(-1, 3);
            Test($"{frac}", "-[1/3]");

            frac = new Fraction(4, 2);
            Test($"{frac}", "[2]");

            frac = new Fraction(10, 3);
            Test($"{frac}", "[3 1/3]");

            frac = new Fraction(-6, 3);
            Test($"{frac}", "-[2]");

            try
            {
                frac = new Fraction(5, -3);
                Test($"{frac}", "Should throw exception");
            }
            catch (ArgumentException)
            {
                TestMsg("Exception has been thrown");
            }

            Console.WriteLine("\nTest properties");

            frac = new Fraction(2, 4);
            Test($"l={frac.Numerator} m={frac.Denominator}", "l=1 m=2");

            frac = new Fraction(6, 3);
            Test($"l={frac.Numerator} m={frac.Denominator}", "l=2 m=1");

            frac = new Fraction(1, 6);
            frac.Numerator = 2;
            Test($"l={frac.Numerator} m={frac.Denominator}", "l=1 m=3");
            frac.Numerator = 0;
            Test($"l={frac.Numerator} m={frac.Denominator}", "l=0 m=1");

            frac = new Fraction(-4, 3);

            try
            {
                frac.Denominator = -6;
                Test($"l={frac.Numerator} m={frac.Denominator}", "Should throw Exception!!!");
            }
            catch (ArgumentException)
            {
                TestMsg("Exception has been thrown");
            }

            try
            {
                frac.Denominator = 0;
                Test($"l={frac.Numerator} m={frac.Denominator}", "Should throw Exception!!!");
            }
            catch (ArgumentException)
            {
                TestMsg("Exception has been thrown");
            }

            Console.WriteLine("\nTest Reciprocal()");

            frac = new Fraction(1, 2);
            Test($"{frac.Reciprocal()}", "[2]");

            frac = new Fraction(2, 3);
            Test($"{frac.Reciprocal()}", "[1 1/2]");

            frac = new Fraction(7, 14);
            Test($"{frac.Reciprocal()}", "[2]");

            try
            {
                frac = new Fraction(-2, 4);
                Test($"{frac.Reciprocal()}", "-[2]");
            }
            catch (ArgumentException)
            {
                TestMsg("Exception has been thrown", false);
            }

            try
            {
                frac = new Fraction(-1, 3);
                Test($"{frac.Reciprocal()}", "-[3]");
            }
            catch (ArgumentException)
            {
                TestMsg("Exception has been thrown", false);
            }

#endif
#if STAGE2

            Console.WriteLine("\nSTAGE 2 - Converters and arithmetic operators");

            Console.WriteLine("\nTest converters");

            frac = 5;
            Test($"{frac}", "[5]");

            frac = new Fraction(-6, 4);
            double d = (double)frac;
            Test($"{d.ToString(CultureInfo.InvariantCulture)}", "-1.5");

            frac = new Fraction(4, 2);
            long l = (long)frac;
            Test($"{l}", "2");

            frac = new Fraction(4, 3);
            l = (long)frac;
            Test($"{l}", "1");

            Console.WriteLine("\nTest arithmetic operators");

            Fraction u1, u2;

            u1 = new Fraction(1, 2);
            u2 = new Fraction(2, 3);

            frac = u1 + u2;
            Test($"{frac}", "[1 1/6]");

            frac = u2 - u1;
            Test($"{frac}", "[1/6]");

            frac = -frac;
            Test($"{frac}", "-[1/6]");

            u1 = new Fraction(1, 6);
            u2 = new Fraction(1, 5);
            frac = u1 * u2;
            Test($"{frac}", "[1/30]");

            frac = u1 / u2;
            Test($"{frac}", "[5/6]");

#endif
#if STAGE3

            Console.WriteLine("\nSTAGE 3 - Comparison operators");

            u1 = new Fraction(1, 2);
            u2 = new Fraction(1, 3);
            Test(u1 < u2, false);
            Test(u1 == u2, false);
            Test(u1 != u2, true);
            Test(u1 >= u2, true);

            u1 = new Fraction(4, 5);
            u2 = new Fraction(1, 2);
            Test(u1 > u2, true);
            Test(u1 == u2, false);
            Test(u1 != u2, true);
            Test(u1 <= u2, false);

            u1 = new Fraction(1, 2);
            u2 = new Fraction(2, 4);
            Test(u1 == u2, true);
            Test(u1 != u2, false);
            Test(u1.Equals(u2), true);
            Test(u1.GetHashCode(), 3);
            Test(u2.GetHashCode(), 3);

#endif
#if STAGE4

            Console.WriteLine("\nSTAGE 4 - Hard tests (operations on large numbers)");

            u1 = new Fraction(1, 30000000000);
            u2 = new Fraction(1, 2);
            u2 = u2 - u1;
            frac = u1 + u2;
            Test($"{frac}", "[1/2]");

            u1 = new Fraction(1234567890, 7777777773);
            u2 = u1;
            u1 = 42 * u1;
            frac = u1 / u2;
            Test($"{frac}", "[42]");

            u1 = new Fraction(1234567891011, 235690235690217);
            u2 = new Fraction(11223344556677, 176366841573);
            frac = u1 * u2;
            Test($"{frac}", "[1/3]");

            u1 = new Fraction(1234567891011, 235690235690217);
            u2 = new Fraction(176366841573, 11223344556677);
            frac = u2 / u1;
            Test($"{frac}", "[3]");

            u1 = new Fraction(5, 2222222222222222222);
            u2 = new Fraction(5, 4444444444444444444);
            Test(u1 > u2, true);
            Test(u1 == u2, false);
            Test(u2 <= u1, true);

            u1 = new Fraction(1, 9000000000000000000);
            u2 = new Fraction(1, 9000000000000000001);
            Test(u1 < u2, false);
            Test(u1 == u2, false);
            Test(u1 >= u2, true);

#endif
        }
    }
}
