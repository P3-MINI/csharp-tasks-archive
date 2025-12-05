//#define STAGE1
//#define STAGE2
//#define STAGE3

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace Lab09_EN
{
    interface IBuffer<T>
    {
        void Put(T value);

        T Get();

        uint Size { get; }

        uint Count { get; }

        bool Empty { get; }

        bool Full { get; }

        void Reset();
    }

    // TODO: Implement CircularBuffer extender here

    class Program
    {
        static void PrintInfo<T>(IBuffer<T> buffer)
        {
            Console.WriteLine($"\tCount: {buffer.Count}/{buffer.Size}");
            Console.WriteLine($"\tIs empty: {buffer.Empty}");
            Console.WriteLine($"\tIs full: {buffer.Full}");

            if (buffer is IEnumerable)
            {
                Console.Write("\tValues: ");
                foreach (var value in buffer as IEnumerable)
                {
                    Console.Write($"{value}, ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void TestPut<T>(IBuffer<T> buffer, T value, bool expectedException = false)
        {
            try
            {
                buffer.Put(value);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"OK. Puts {value}");
            }
            catch (Exception e)
            {
                if (expectedException)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"OK. Exception has been thrown! {e.Message}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error. Exception has been thrown! {e.Message}");
                }
            }
            Console.ResetColor();
        }

        static void TestGet<T>(IBuffer<T> buffer, T expectedValue, bool expectedException = false)
        {
            try
            {
                T value = buffer.Get();
                if (expectedValue.Equals(value))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"OK. Gets {value}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error. Gets {value}, but expected {expectedValue}");
                }
            }
            catch (Exception e)
            {
                if (expectedException)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"OK. Exception has been thrown! {e.Message}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error. Exception has been thrown! {e.Message}");
                }
            }
            Console.ResetColor();
        }

        static bool TestValues<T>(IBuffer<T> buffer, IEnumerable<T> values)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            IEnumerable enumerator = buffer as IEnumerable;
            if (enumerator == null)
            {
                Console.WriteLine("Error. Buffer does not interface IEnumerator");
                Console.ResetColor();
                return false;
            }

            var enenumerator2 = values.GetEnumerator();

            foreach (T value in enumerator)
            {
                if (enenumerator2.MoveNext())
                {
                    T value2 = enenumerator2.Current;
                    if (!value.Equals(value2))
                    {
                        Console.WriteLine($"Error. Wrong value in buffer {value}, should be {value2}");
                        Console.ResetColor();
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine($"Error. Extra value in buffer {value}");
                    Console.ResetColor();
                    return false;
                }
            }

            if (enenumerator2.MoveNext())
            {
                Console.WriteLine($"Error. Missing value in buffer {enenumerator2.Current}");
                Console.ResetColor();
                return false;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("OK. Buffer has correct values");
            Console.ResetColor();
            return true;
        }

        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentCulture.Clone() as CultureInfo;
            Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";

            Console.WriteLine(" === SATGE 1 ===");

#if STAGE1
            CircularBuffer<int> cb1 = new CircularBuffer<int>(5);
            PrintInfo(cb1);

            TestPut(cb1, 1);
            TestGet(cb1, 1);
            TestGet(cb1, 0, true);

            TestPut(cb1, 2);
            TestPut(cb1, 3);
            TestPut(cb1, 4);
            TestPut(cb1, 5);
            TestGet(cb1, 2);
            TestPut(cb1, 6);
            TestGet(cb1, 3);
            TestPut(cb1, 3);
            TestPut(cb1, 4);

            Console.WriteLine("Buffer reset");
            cb1.Reset();
            PrintInfo(cb1);
            TestGet(cb1, 0, true);

#endif
            Console.WriteLine();
            Console.WriteLine(" === SATGE 2 ===");

#if STAGE2
            TestPut(cb1, 2);
            TestPut(cb1, 3);
            TestPut(cb1, 4);
            TestPut(cb1, 5);
            TestPut(cb1, 6);
            TestPut(cb1, 3, true);
            TestPut(cb1, 4, true);
            TestValues(cb1, new int[] { 2, 3, 4, 5, 6 });

            TestGet(cb1, 2);
            TestPut(cb1, 100);
            TestValues(cb1, new int[] { 3, 4, 5, 6, 100 });

            Console.WriteLine();
            Console.WriteLine("Values in buffer lower than 5:");
            foreach (var value in cb1.FilterLowerThan(5))
            {
                Console.Write($"{value}, ");
            }
            Console.WriteLine("\nShould be: \n3, 4");

#endif
            Console.WriteLine();
            Console.WriteLine(" === SATGE 3 ===");

#if STAGE3
            Console.WriteLine("CircularBuffer1");
            PrintInfo(cb1);

            Console.WriteLine("Cloning...");
            var cb2 = cb1.Clone();

            Console.WriteLine("CircularBuffer1");
            PrintInfo(cb1);
            TestValues(cb1, new int[] { 3, 4, 5, 6, 100 });

            Console.WriteLine("CircularBuffer2");
            PrintInfo(cb1);
            TestValues(cb2, new int[] { 3, 4, 5, 6, 100 });

            TestGet(cb1, 3);
            TestGet(cb1, 4);
            TestPut(cb2, 0, true);
            TestPut(cb1, 0, true);
            TestGet(cb2, 3);

            TestPut(cb1, 6);
            TestGet(cb1, 5);
            TestPut(cb1, 7);

            PrintInfo(cb1);
            PrintInfo(cb2);

            TestValues(cb1, new int[] { 6, 100, 0, 6, 7 });
            TestValues(cb2, new int[] { 4, 5, 6, 100 });


            Console.WriteLine(" === Additional Tests with doubles ===");
            CircularBuffer<double> cb3 = new CircularBuffer<double>(10);
            PrintInfo(cb3);

            cb3.Put(Math.PI);
            cb3.Put(0.0);
            cb3.Put(Math.E);

            PrintInfo(cb3);

            Random random = new Random(0);
            for (int i = 0; i < 13; i++)
            {
                TestPut(cb3, 5.0 + random.Next(20) * 0.25, i > 6);
            }

            PrintInfo(cb3);
            TestValues(cb3, new double[] { Math.PI, 0, Math.E, 8.5, 9, 8.75, 7.75, 6, 7.75, 9.5 });

            TestGet(cb3, Math.PI);
            TestValues(cb3, new double[] { 0, Math.E, 8.5, 9, 8.75, 7.75, 6, 7.75, 9.5 });

            Console.WriteLine();
            Console.WriteLine("Values in buffer lower than 8.0:");
            foreach (var value in cb3.FilterLowerThan(8.0))
            {
                Console.Write($"{value}, ");
            }
            Console.WriteLine("\nShould be: \n6, 7.75, 7, 6.25, 6.25, 7.25");
#endif
        }
    }
}
