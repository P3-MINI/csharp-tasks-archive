using P3A_24Z_Lab05;

namespace P3A_24Z_Lab05_Tests
{
    [TestClass]
    public class UnitTestsStage04
    {
        [TestMethod]
        public void TestMyBinaryTreeSignature()
        {
            var classType = typeof(MyBinaryTree<,>);
            Assert.IsTrue(classType.IsClass);
            Assert.IsTrue(classType.IsGenericType);


        }
    }
}