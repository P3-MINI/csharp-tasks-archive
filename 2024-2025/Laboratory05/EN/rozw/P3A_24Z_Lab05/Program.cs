#define STAGE01
#define STAGE02
#define STAGE03
#define STAGE04

namespace P3A_24Z_Lab05
{
    internal class Program
    {
        static void Main(string[] args)
        {
#if STAGE01
            Console.WriteLine("\n  -=== STAGE 01 ===-\n");
            var pair = new MyPair<int, string>(1, "Apple");

            Console.WriteLine($"Key: {pair.Key}, Value: {pair.Value}");

            pair.Value = "Banana";
            Console.WriteLine($"Updated Value: {pair.Value}");
#endif
            Console.WriteLine("\n  -=== STAGE 02 ===-\n");

            Console.WriteLine(" No code to test - only unit tests");

#if STAGE03
            Console.WriteLine("\n  -=== STAGE 02 ===-\n");

            var myList = new MyList<double>();

            Console.WriteLine("\nAdding values: 1,2,3.14");
            myList.Add(1);
            myList.Add(2);
            myList.Add(3.14);

            Console.WriteLine($"Count: {myList.Count}");

            for (int i=0; i<myList.Count; i++)
            {
                Console.WriteLine(myList[i]);
            }

            try
            {
                myList[myList.Count] = 0;
                Console.WriteLine($"Indexer should throw an exception: ERROR");
            }
            catch (ArgumentOutOfRangeException) { Console.WriteLine($"Indexer should throw an exception: OK"); }
            catch { Console.WriteLine($"Indexer should throw the ArgumentOutOfRangeException exception: ERROR"); }

            try
            {
                myList[-1] = 0;
                Console.WriteLine($"Indexer should throw an exception: ERROR");
            }
            catch (ArgumentOutOfRangeException) { Console.WriteLine($"Indexer should throw an exception: OK"); }
            catch { Console.WriteLine($"Indexer should throw the ArgumentOutOfRangeException exception: ERROR"); }

            Console.WriteLine("\nAdding values: 13,21,2.71,36.5,sqrt(2)");
            myList.Add(13);
            myList.Add(21);
            myList.Add(2.71);
            myList.Add(36.5);
            myList.Add(Math.Sqrt(2));

            Console.WriteLine($"Count: {myList.Count}");

            Console.WriteLine($"Values: [{string.Join(", ", myList)}]");

            Console.WriteLine($"\nIndexOf(13) = {myList.IndexOf(13)}");
            Console.WriteLine($"IndexOf(-1) = {myList.IndexOf(-123)}");
            Console.WriteLine($"Contains(2.71) = {myList.Contains(2.71)}");
            Console.WriteLine($"Contains(10) = {myList.Contains(10)}");

            Console.WriteLine($"\nRemoving 2");
            myList.Remove(2);
            Console.WriteLine($"Count: {myList.Count}");
            Console.WriteLine($"Values: [{string.Join(", ", myList)}]");

            Console.WriteLine($"\nRemoving at 2");
            myList.RemoveAt(2);
            Console.WriteLine($"Count: {myList.Count}");
            Console.WriteLine($"Values: [{string.Join(", ", myList)}]");

            Console.WriteLine($"\nClearing list");
            myList.Clear();
            Console.WriteLine($"Count: {myList.Count}");
            Console.WriteLine($"IndexOf(13) = {myList.IndexOf(13)}");
            Console.WriteLine($"Contains(21) = {myList.Contains(21)}");
#endif
#if STAGE04
            Console.WriteLine("\n  -=== STAGE 04 ===-\n");
            var binaryTree = new MyBinaryTree<string, int>();

            binaryTree.Add("banana", 10);
            binaryTree.Add("apple", 5);
            binaryTree.Add("cherry", 15);
            binaryTree.Add("cherry", 123);

            Console.WriteLine($"Count: {binaryTree.Count}");

            var foundNode = binaryTree.Find("apple");
            if (foundNode != null)
            {
                Console.WriteLine($"\nFound: {foundNode.Key} - {foundNode.Value}");
            }

            foundNode = binaryTree.Find("cherry");
            if (foundNode != null)
            {
                Console.WriteLine($"\nFound: {foundNode.Key} - {foundNode.Value}");
            }

            var notFoundNode = binaryTree.Find("grape");
            if (notFoundNode == null)
            {
                Console.WriteLine("\nKey 'grape' not found.");
            }
#endif
        }
    }
}
