using P3Z_24Z_Lab05;
using System.Collections;
using System.Reflection;

namespace P3Z_24Z_Lab05_Tests
{
    [TestClass]
    public class UnitTestsStage02
    {
        public static void TestEnumeratorValues<T>(IEnumerable<T> enumerable, T[] values, bool shouldHaveNextValue = false)
        {
            var enumerator = enumerable.GetEnumerator();
            bool hasNext = enumerator.MoveNext();
            foreach (T value in values)
            {
                Assert.IsTrue(hasNext);
                Assert.AreEqual(value, enumerator.Current);
                hasNext = enumerator.MoveNext();
            }

            Assert.AreEqual(shouldHaveNextValue, hasNext);
        }

        [TestMethod]
        public void TestMyCircularBufferClassSignature()
        {
            var classType = typeof(MyCircularBuffer<>);
            Assert.IsTrue(classType.IsGenericType);

            var constructors = classType.GetConstructors();
            Assert.AreEqual(1, constructors.Length);
            Assert.IsTrue(constructors[0].IsPublic);
            var constructorParameters = constructors[0].GetParameters();
            Assert.AreEqual(1, constructorParameters.Length);
            Assert.IsTrue(constructorParameters[0].ParameterType == typeof(int));

            var interfaces = classType.GetInterfaces();
            var classInterface = interfaces.FirstOrDefault(i => i.Name == typeof(IMyCollection<>).Name);
            Assert.IsNotNull(classInterface);
        }

        [TestMethod]
        public void TestCircularBufferCount()
        {
            var sortedList = new MyCircularBuffer<int>(5);
            Assert.AreEqual(0, sortedList.Count);
            sortedList.Add(1);
            Assert.AreEqual(1, sortedList.Count);
            sortedList.Add(2);
            Assert.AreEqual(2, sortedList.Count);
            sortedList.Add(2);
            Assert.AreEqual(3, sortedList.Count);
            sortedList.Add(3);
            Assert.AreEqual(4, sortedList.Count);
            sortedList.Add(3);
            Assert.AreEqual(5, sortedList.Count);
            sortedList.Add(3);
            Assert.AreEqual(5, sortedList.Count);
        }

        [TestMethod]
        public void TestCircularBufferGetItems()
        {
            var classType = typeof(MyCircularBuffer<>);

            var methods = classType.GetMethods();
            var getItemsMethod = methods.FirstOrDefault(m => m.Name == "GetItems");
            Assert.IsNotNull(getItemsMethod);
            Assert.AreEqual(0, getItemsMethod.GetParameters().Length);
            Assert.IsTrue(getItemsMethod.ReturnType.IsGenericType);
            Assert.AreEqual(typeof(IEnumerable<>), getItemsMethod.ReturnType.GetGenericTypeDefinition());

            // Test values
            var circularBuffer = new MyCircularBuffer<int>(4);
            circularBuffer.Add(1);
            circularBuffer.Add(2);
            circularBuffer.Add(3);
            circularBuffer.Add(4);
            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4 }, circularBuffer.GetItems().ToArray());
            circularBuffer.Add(5);
            CollectionAssert.AreEqual(new int[] { 2, 3, 4, 5 }, circularBuffer.GetItems().ToArray());
            circularBuffer.Add(10);
            CollectionAssert.AreEqual(new int[] { 3, 4, 5, 10 }, circularBuffer.GetItems().ToArray());
        }

        [TestMethod]
        public void TestCircularBufferEnumerator()
        {
            var circularBuffer = new MyCircularBuffer<int>(4);
            circularBuffer.Add(1);
            circularBuffer.Add(2);
            circularBuffer.Add(3);
            TestEnumeratorValues(circularBuffer, [1, 2, 3]);
            circularBuffer.Add(5);
            TestEnumeratorValues(circularBuffer, [1, 2, 3, 5, 1, 2, 3, 5, 1, 2, 3, 5], true); // If full, should repeat values

            circularBuffer.Add(13);
            TestEnumeratorValues(circularBuffer, [2, 3, 5, 13, 2, 3, 5, 13], true); // Same
        }

        [TestMethod]
        public void TestCircularBufferIsFull()
        {
            var circularBuffer = new MyCircularBuffer<int>(4);
            Assert.IsFalse(circularBuffer.IsFull);
            circularBuffer.Add(1);
            Assert.IsFalse(circularBuffer.IsFull);
            circularBuffer.Add(2);
            Assert.IsFalse(circularBuffer.IsFull);
            circularBuffer.Add(3);
            Assert.IsFalse(circularBuffer.IsFull);
            circularBuffer.Add(4);
            Assert.IsTrue(circularBuffer.IsFull);
            circularBuffer.Add(5);
            Assert.IsTrue(circularBuffer.IsFull);
        }

        [TestMethod]
        public void TestCircularBufferIsEmpty()
        {
            var circularBuffer = new MyCircularBuffer<int>(5);
            Assert.IsTrue(circularBuffer.IsEmpty);
            circularBuffer.Add(1);
            Assert.IsFalse(circularBuffer.IsEmpty);
            circularBuffer.Add(2);
            Assert.IsFalse(circularBuffer.IsEmpty);
            circularBuffer.Add(3);
            Assert.IsFalse(circularBuffer.IsEmpty);
            circularBuffer.Add(4);
        }

    }
}