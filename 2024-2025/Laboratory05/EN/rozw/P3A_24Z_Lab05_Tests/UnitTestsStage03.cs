using P3A_24Z_Lab05;
using System.Collections.Generic;

namespace P3A_24Z_Lab05_Tests
{
    [TestClass]
    public class UnitTestsStage03
    {
        [TestMethod]
        public void TestMyListSignature()
        {
            var classType = typeof(MyList<>);
            Assert.IsTrue(classType.IsClass);
            Assert.IsTrue(classType.IsGenericType);

            var interfaces = classType.GetInterfaces();
            Assert.IsTrue(
                interfaces.Any(i => (i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
                );

            var properties = classType.GetProperties();
            Assert.AreEqual(2, properties.Length); // Count + Indexer

            var countProperty = properties.FirstOrDefault(p => p.Name == "Count");
            Assert.IsNotNull(countProperty);
            Assert.AreEqual(typeof(int), countProperty.PropertyType);
            Assert.AreEqual(true, countProperty.CanRead);
            Assert.AreEqual(false, countProperty.CanWrite);

        }

        [TestMethod]
        public void TestMyListAdd()
        {
            var list = new MyList<int>();

            list.Add(1);
            list.Add(2);
            list.Add(3);
            Assert.AreEqual(3, list.Count);
        }

        [TestMethod]
        public void TestMyListRemove()
        {
            var list = new MyList<int>();

            list.Add(1);
            list.Add(2);
            list.Add(3);
            Assert.AreEqual(3, list.Count);

            Assert.AreEqual(false, list.Remove(0));
            Assert.AreEqual(true, list.Remove(1));
            Assert.AreEqual(false, list.Remove(1));

            Assert.AreEqual(2, list.Count);
        }

    }
}