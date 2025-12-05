using P3A_24Z_Lab05;
using System.Collections.Generic;
using System.Data;

namespace P3A_24Z_Lab05_Tests
{
    [TestClass]
    public class UnitTestsStage01
    {
        [TestMethod]
        public void TestMyPairSignature()
        {
            var classType = typeof(MyPair<,>);
            Assert.IsTrue(classType.IsClass);
            Assert.IsTrue(classType.IsGenericType);

            var constructors = classType.GetConstructors();
            Assert.AreEqual(1, constructors.Length);
            Assert.IsTrue(constructors[0].IsPublic);
            var constructorParameters = constructors[0].GetParameters();
            Assert.AreEqual(2, constructorParameters.Length);

            var genericArguments = classType.GetGenericArguments();
            Assert.AreEqual(2, genericArguments.Length);
            var keyTypeConstraints = genericArguments[0].GetGenericParameterConstraints();
            Assert.IsTrue(keyTypeConstraints.Length >= 1);
            Assert.IsTrue(
                keyTypeConstraints.Any(c => c.GetGenericTypeDefinition() == typeof(IComparable<>) &&
                                            c.GetGenericArguments()[0] == genericArguments[0])
                );
        }

        [TestMethod]
        public void TestMyPairCompare()
        {
            var pair1 = new MyPair<string, double>("abc", 123);
            var pair2 = new MyPair<string, double>("xyz", 123);
            var pair3 = new MyPair<string, double>("abc", 456);

            Assert.AreEqual(0, pair1.CompareTo(pair1));
            Assert.IsTrue(pair1.CompareTo(pair2) < 0);
            Assert.IsTrue(pair2.CompareTo(pair1) > 0);
            Assert.AreEqual(0, pair1.CompareTo(pair3));
            Assert.AreEqual(0, pair3.CompareTo(pair1));
        }
    }
}