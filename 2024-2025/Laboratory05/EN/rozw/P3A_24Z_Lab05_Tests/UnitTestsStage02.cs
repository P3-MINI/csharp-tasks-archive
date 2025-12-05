using Newtonsoft.Json.Linq;
using P3A_24Z_Lab05;
using System.Collections.Generic;

namespace P3A_24Z_Lab05_Tests
{
    [TestClass]
    public class UnitTestsStage02
    {
        [TestMethod]
        public void TestIMyMapSignature()
        {
            var interfaceType = typeof(IMyMap<,>);
            Assert.IsTrue(interfaceType.IsInterface);
            Assert.IsTrue(interfaceType.IsGenericType);

            var interfaces = interfaceType.GetInterfaces();
            Assert.AreEqual(0, interfaces.Length);

            var properties = interfaceType.GetProperties();
            Assert.AreEqual(1, properties.Length);
            Assert.AreEqual("Count", properties[0].Name);
            Assert.IsTrue(properties[0].CanRead);
            Assert.IsFalse(properties[0].CanWrite);

            var methods = interfaceType.GetMethods();
            Assert.AreEqual(methods.Length, 2 + 1); // + 1 for Count property getter

            var addMethod = methods.FirstOrDefault(m => m.Name == "Add");
            Assert.IsNotNull(addMethod);
            Assert.AreEqual(typeof(bool), addMethod.ReturnType);
            var addMethodParameters = addMethod.GetParameters();
            Assert.AreEqual(2, addMethodParameters.Length);
            Assert.IsTrue(addMethodParameters[0].ParameterType.IsGenericParameter);
            Assert.IsTrue(addMethodParameters[1].ParameterType.IsGenericParameter);

            var findMethod = methods.FirstOrDefault(m => m.Name == "Find");
            Assert.IsNotNull(findMethod);
            //Assert.AreEqual(typeof(MyPair<,>), findMethod.ReturnType);
            Assert.AreEqual(typeof(MyPair<,>).Name, findMethod.ReturnType.Name);
            
            var findMethodParameters = findMethod.GetParameters();
            Assert.AreEqual(1, findMethodParameters.Length);
        }
    }
}