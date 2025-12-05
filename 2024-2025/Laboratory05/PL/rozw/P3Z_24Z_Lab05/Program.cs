#define STAGE_02
#define STAGE_03

namespace P3Z_24Z_Lab05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n  -=== STAGE 01 ===-\n");

            Console.WriteLine(" No code to test - only unit tests");

#if STAGE_02
            Console.WriteLine("\n  -=== STAGE 02 ===-\n");

            var myCircularBuf1 = new MyCircularBuffer<int>(10);

            Console.WriteLine($"MyCircularBuffer should be empty: {(myCircularBuf1.IsEmpty == true ? "OK" : "EROOR")}");
            Console.WriteLine($"MyCircularBuffer should not be full: {(myCircularBuf1.IsFull == false ? "OK" : "EROOR")}");

            Console.WriteLine("\nAdding values: 1,1,2,3,5,8");
            myCircularBuf1.Add(1);
            myCircularBuf1.Add(1);
            myCircularBuf1.Add(2);
            myCircularBuf1.Add(3);
            myCircularBuf1.Add(5);
            myCircularBuf1.Add(8);

            Console.WriteLine($"Enumerated values: [{string.Join(", ", myCircularBuf1.Take(1000))}]");
            Console.WriteLine($"Items: [{string.Join(", ", myCircularBuf1.GetItems())}]");

            Console.WriteLine("\nAdding values: 13,21,34,55");
            myCircularBuf1.Add(13);
            myCircularBuf1.Add(21);
            myCircularBuf1.Add(34);
            myCircularBuf1.Add(55);

            Console.WriteLine($"Items: [{string.Join(", ", myCircularBuf1.GetItems())}]");
            Console.WriteLine($"Enumerated values: [{string.Join(", ", myCircularBuf1.Take(21))}]");

            Console.WriteLine($"MyCircularBuffer should not be empty: {(myCircularBuf1.IsEmpty == false ? "OK" : "EROOR")}");
            Console.WriteLine($"MyCircularBuffer should be full: {(myCircularBuf1.IsFull == true ? "OK" : "EROOR")}");

            Console.WriteLine("\nAdding values: 89,144,233");
            myCircularBuf1.Add(89);
            myCircularBuf1.Add(144);
            myCircularBuf1.Add(233);

            Console.WriteLine($"Items: [{string.Join(", ", myCircularBuf1.GetItems())}]");
            Console.WriteLine($"Enumerated values: [{string.Join(", ", myCircularBuf1.Take(21))}]");
#endif
#if STAGE_03
            Console.WriteLine("\n  -=== STAGE 03 ===-\n");
            var sortedList = new MySortedLinkedList<int>();

            try
            {
                Console.WriteLine("PopFront on empty list!");
                var popEmpty = sortedList.PopFront();
                Console.WriteLine($"PopFront should throw an exception: ERROR");
            }
            catch (IndexOutOfRangeException) { Console.WriteLine($"PopFront should throw an exception: OK"); }
            catch (Exception) { Console.WriteLine($"PopFront should throw the IndexOutOfRangeException exception: ERROR"); }

            Console.WriteLine("\nAdding values: 5,3,8");
            sortedList.Add(5);
            sortedList.Add(3);
            sortedList.Add(8);

            Console.WriteLine($"Count: {sortedList.Count}");
            Console.WriteLine($"Contains '3': {(sortedList.Contains(3) ? "OK" : "ERROR")}");

            Console.WriteLine("\nEnumerating values:");
            foreach (var value in sortedList)
            {
                Console.WriteLine($"{value}");
            }

            var popValue = sortedList.PopFront();
            Console.WriteLine($"\nPopFront value {popValue}: {(popValue == 3 ? "OK" : "ERROR")}");

            Console.WriteLine($"Count after PopFront: {sortedList.Count}");

            Console.WriteLine($"Enumerated values: [{string.Join(", ", sortedList)}]");

            Console.WriteLine("\nAdding values: 84,1,55,8");
            sortedList.Add(84);
            sortedList.Add(1);
            sortedList.Add(55);
            sortedList.Add(8);
            Console.WriteLine($"Count: {sortedList.Count}");
            Console.WriteLine($"Enumerated values: [{string.Join(", ", sortedList)}]");
#endif
        }
    }
}
