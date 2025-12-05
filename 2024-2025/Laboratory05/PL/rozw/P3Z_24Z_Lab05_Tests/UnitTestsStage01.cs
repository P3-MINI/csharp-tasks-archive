using P3Z_24Z_Lab05;
using System.Collections;
using System.Reflection;

namespace P3Z_24Z_Lab05_Tests
{
    [TestClass]
    public class UnitTestsStage01
    {
        [TestMethod]
        public void TestIMyCollectionInterface()
        {
            var interfaceType = typeof(IMyCollection<>);
            Assert.IsTrue(interfaceType.IsInterface);
            Assert.IsTrue(interfaceType.IsGenericType);

            var interfaces = interfaceType.GetInterfaces();
            Assert.IsTrue(
                interfaces.Any(i => (i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
                );

            var properties = interfaceType.GetProperties();
            Assert.AreEqual(1, properties.Length);
            Assert.AreEqual("Count", properties[0].Name);
            Assert.IsTrue(properties[0].CanRead);
            Assert.IsFalse(properties[0].CanWrite);

            var methods = interfaceType.GetMethods();
            Assert.AreEqual(methods.Length, 1 + 1); // + 1 for Count property getter

            var addMethod = methods.FirstOrDefault(m => m.Name == "Add");
            Assert.IsNotNull(addMethod);
            Assert.AreEqual(typeof(void), addMethod.ReturnType);
            var addMethodParameters = addMethod.GetParameters();
            Assert.AreEqual(1, addMethodParameters.Length);
            Assert.IsTrue(addMethodParameters[0].ParameterType.IsGenericParameter);
        }
    }
}