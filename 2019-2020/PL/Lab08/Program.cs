#define STAGE1
#define STAGE2
#define STAGE3
#define STAGE4
using System;
using System.Linq;
using System.Text;

namespace PL_Lab_08
{
    class Program
    {
        static int testNum = 1;
        static string groupName = null;
        static bool groupResult;
        static int groupStartTop;
        public static void Main()
        {
#if STAGE1
            PairTest();
#endif
#if STAGE2
            LinkedListTest();
#endif
#if STAGE3
            BinaryTreeTest();
#endif
#if STAGE4
            IMapExtenderTest();
#endif
        }
#if STAGE1
        private static void PairTest()
        {
            BeginGroup("Pair");
            var pair1 = new Pair<int, string>(1, "Value");
            var pair2 = new Pair<int, string>(42, "Second value");
            var pair3 = new Pair<int, string>(42, "Different value");
            Check(pair1.Key == 1, "Key", "Key returns wrong value.");
            Check(pair1.Value == "Value", "Value", "Value returns wrong value.");
            Check(pair2.Key == 42, "Key", "Key returns wrong value.");
            Check(pair2.Value == "Second value", "Value", "Value returns wrong value.");
            Check(pair3.Key == 42, "Key", "Key returns wrong value.");
            Check(pair3.Value == "Different value", "Value", "Value returns wrong value.");
            Check(pair1.CompareTo(pair2) < 0, "CompareTo", "CompareTo behaves wrong.");
            Check(pair2.CompareTo(pair1) > 0, "CompareTo", "CompareTo behaves wrong.");
            Check(pair3.CompareTo(pair2) == 0, "CompareTo", "CompareTo behaves wrong.");
            Check(pair2.CompareTo(pair3) == 0, "CompareTo", "CompareTo behaves wrong.");
            EndGroup();
        }
#endif
#if STAGE2
        private static void LinkedListTest()
        {
            BeginGroup("LinkedList");
            var list = new SortedLinkedList<int, string>();
            IMapTest(list);
            Pair<int, string> p;
            p = list.PopFront();
            Check(!(p is null), "PopFront", "PopFront should return pair with the smallest key.");
            Check(!(p is null) && p.Value == "-32", "PopFront", "PopFront returned wrong pair.");
            Check(list.Count == 4, "Count", "Count should decrease after PopFront.");
            p = list.PopFront();
            Check(!(p is null), "PopFront", "PopFront should return pair with the smallest key.");
            Check(!(p is null) && p.Value == "-1", "PopFront", "PopFront returned wrong pair.");
            Check(list.Count == 3, "Count", "Count should decrease after PopFront.");
            p = list.PopFront();
            Check(!(p is null), "PopFront", "PopFront should return pair with the smallest key.");
            Check(!(p is null) && p.Value == "3", "PopFront", "PopFront returned wrong pair.");
            Check(list.Count == 2, "Count", "Count should decrease after PopFront.");
            p = list.PopFront();
            Check(!(p is null), "PopFront", "PopFront should return pair with the smallest key.");
            Check(!(p is null) && p.Value == "4", "PopFront", "PopFront returned wrong pair.");
            Check(list.Count == 1, "Count", "Count should decrease after PopFront.");
            p = list.PopFront();
            Check(!(p is null), "PopFront", "PopFront should return pair with the smallest key.");
            Check(!(p is null) && p.Value == "23", "PopFront", "PopFront returned wrong pair.");
            Check(list.Count == 0, "Count", "Count should decrease after PopFront.");
            p = list.PopFront();
            Check(p is null, "PopFront", "PopFront should return null if there is no elements in list.");
            Check(list.Count == 0, "Count", "Count should remain 0 after PopFront on empty list.");
            EndGroup();
        }
#endif
#if STAGE3
        private static void BinaryTreeTest()
        {
            BeginGroup("BinaryTree");
            var tree = new BinaryTree<int, string>();
            IMapTest(tree);
            EndGroup();
        }
#endif
#if STAGE2
        private static void IMapTest(IMap<int, string> map)
        {
            Check(map.Count == 0, "Count", "Count of an empty map should return 0.");
            Check(map.Add(3, "3"), "Add", "Adding new key should return true.");
            Check(map.Count == 1, "Count", "Count return wrong value.");
            Check(map.Add(-1, "-1"), "Add", "Adding new key should return true.");
            Check(map.Count == 2, "Count", "Count return wrong value.");
            Check(map.Add(4, "4"), "Add", "Adding new key should return true.");
            Check(map.Count == 3, "Count", "Count return wrong value.");
            Check(map.Add(23, "23"), "Add", "Adding new key should return true.");
            Check(map.Count == 4, "Count", "Count return wrong value.");
            Check(!map.Add(-1, "-1"), "Add", "Adding already existing key should return false.");
            Check(map.Count == 4, "Count", "Count shouldn't change if adding failed.");
            Check(map.Add(-32, "-32"),  "Add", "Adding new key should return true.");
            Check(map.Count == 5, "Count", "Count return wrong value.");
            Check(!map.Add(23, "23"), "Add", "Adding already existing key should return false.");
            Check(map.Count == 5, "Count", "Count shouldn't change if adding failed.");

            Pair<int, string> p;
            p = map.Find(23);
            Check(!(p is null), "Find", "Find should return added pair.");
            Check(!(p is null) && p.Value == "23", "Find", "Find return pair with wrong value.");
            p = map.Find(4);
            Check(!(p is null), "Find", "Find should return added pair.");
            Check(!(p is null) && p.Value == "4", "Find", "Find return pair with wrong value.");
            p = map.Find(7);
            Check(p is null, "Find", "Find shouldn't return non-existing pair.");
        }
#endif
#if STAGE4
        private static void IMapExtenderTest()
        {
            IMap<int, int> map;
            BeginGroup("IMapExtender[SortedLinkedList]");
            map = new SortedLinkedList<int, int>();
            foreach (var k in Enumerable.Range(-5, 11))
                map.Add(k, k);
            Check(map.ContainsAll(Enumerable.Range(-3, 7)), "ContainsAll", "ContainsAll should return true");
            Check(!map.ContainsAll(Enumerable.Range(-5, 12)), "ContainsAll", "ContainsAll should return false");
            Check(map.SumForKeys(Enumerable.Range(0, 5)) == 10, "SumForKey", "Sum should equals 10.");
            Check(map.SumForKeys(new int[] { -11, -2, 3, 4 }) == 5, "SumForKey", "Sum should equals 5.");
            EndGroup();

#if STAGE3
            BeginGroup("IMapExtender[BinaryTree]");
            map = new BinaryTree<int, int>();
            foreach (var k in Enumerable.Range(-5, 11))
                map.Add(k, k);
            Check(map.ContainsAll(Enumerable.Range(-3, 7)), "ContainsAll", "ContainsAll should return true");
            Check(!map.ContainsAll(Enumerable.Range(-5, 12)), "ContainsAll", "ContainsAll should return false");
            Check(map.SumForKeys(Enumerable.Range(0, 5)) == 10, "SumForKey", "Sum should equals 10.");
            Check(map.SumForKeys(new int[] { -11, -2, 3, 4 }) == 5, "SumForKey", "Sum should equals 5.");
            EndGroup();
#endif
        }
#endif
        private static void BeginGroup(string groupName_)
        {
            if (!(groupName is null))
                throw new InvalidOperationException("Cannot begin group if previous one doesn't ended");
            groupResult = true;
            groupName = groupName_;
            groupStartTop = Console.CursorTop;
            Console.WriteLine($"Group ({groupName}) {{");
        }
        private static void EndGroup()
        {
            var h = Console.CursorTop;
            var color = Console.ForegroundColor;
            Console.CursorTop = groupStartTop;
            Console.ForegroundColor = groupResult ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine($"Group ({groupName}) {{");
            Console.CursorTop = h;
            Console.WriteLine("}\n");
            Console.ForegroundColor = color;
            groupName = null;
        }
        private static void Check(bool val, string testName, string hint = null)
        {
            groupResult &= val;
            var color = Console.ForegroundColor;
            Console.ForegroundColor = val ? ConsoleColor.Green : ConsoleColor.Red;
            string head = val ? "OK" : "BAD";
            var sb = new StringBuilder($"[{testNum,2}]{head,-3} <{testName}>");
            if (!val && !(hint is null))
                sb.Append($" - {hint}");
            Console.WriteLine(sb.ToString());
            Console.ForegroundColor = color;
            testNum += 1;
        }
    }
}
