using P3Z_24Z_Lab05;
using System.Collections;
using System.Reflection;

namespace P3Z_24Z_Lab05_Tests
{
	[TestClass]
	public class UnitTestsStage03
	{
		[TestMethod]
		public void TestMySortedLinkedListClassSignature()
		{
			var classType = typeof(MySortedLinkedList<>);
			Assert.IsTrue(classType.IsGenericType);

			var constructors = classType.GetConstructors();
			Assert.AreEqual(1, constructors.Length);
			Assert.IsTrue(constructors[0].IsPublic);
			var constructorParameters = constructors[0].GetParameters();
			Assert.AreEqual(0, constructorParameters.Length);

			var interfaces = classType.GetInterfaces();
			var classInterface = interfaces.FirstOrDefault(i => i.Name == typeof(IEnumerable<>).Name);
			Assert.IsNotNull(classInterface);
		}


		[TestMethod]
		public void TestMySortedLinkedListCountAdd()
		{
			var classType = typeof(MySortedLinkedList<>);
			var countProperty = classType.GetProperty("Count");
			Assert.IsNotNull(countProperty);
			var countSetMethod = countProperty.GetSetMethod(true);
			Assert.IsNotNull(countSetMethod);
			Assert.IsTrue(countSetMethod.IsPrivate);

			var sortedList = new MySortedLinkedList<int>();
			Assert.AreEqual(0, sortedList.Count);

			sortedList.Add(5);
			sortedList.Add(3);
			sortedList.Add(8);
			Assert.AreEqual(3, sortedList.Count);

			sortedList.PopFront();
			Assert.AreEqual(2, sortedList.Count);
		}

		[TestMethod]
		public void TestMySortedLinkedListContains()
		{
			var sortedList = new MySortedLinkedList<int>();

			Assert.IsFalse(sortedList.Contains(5));

			sortedList.Add(5);
			sortedList.Add(3);
			sortedList.Add(8);
			Assert.IsTrue(sortedList.Contains(5));
			Assert.IsFalse(sortedList.Contains(7));

		}

		[TestMethod]
		public void TestMySortedLinkedListPopFront()
		{
			var sortedList = new MySortedLinkedList<int>();

			Assert.ThrowsException<IndexOutOfRangeException>(() => sortedList.PopFront());

			sortedList.Add(5);
			sortedList.Add(3);
			sortedList.Add(8);

			Assert.AreEqual(3, sortedList.PopFront());
			Assert.AreEqual(5, sortedList.PopFront());
			Assert.AreEqual(8, sortedList.PopFront());
			Assert.ThrowsException<IndexOutOfRangeException>(() => sortedList.PopFront());
		}

		[TestMethod]
		public void TestMySortedLinkedListEnumeration()
		{
			var sortedList = new MySortedLinkedList<int>();

			sortedList.Add(5);
			sortedList.Add(3);
			sortedList.Add(8);

			CollectionAssert.AreEqual(new int[] { 3, 5, 8 }, sortedList.ToArray());

			sortedList.PopFront();

			CollectionAssert.AreEqual(new int[] { 5, 8 }, sortedList.ToArray());

			sortedList.Add(84);
			sortedList.Add(1);
			sortedList.Add(55);
			sortedList.Add(8);

			CollectionAssert.AreEqual(new int[] { 1, 5, 8, 8, 55, 84 }, sortedList.ToArray());
		}
	}
}