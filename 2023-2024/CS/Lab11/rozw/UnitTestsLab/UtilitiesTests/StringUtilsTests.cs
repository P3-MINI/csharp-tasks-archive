using Utilities;

namespace UtilitiesTests;

[TestClass]
public class StringUtilsTests
{
    [TestMethod]
    public void Reverse_HelloString_ProducesReversedString()
    {
        // Arrange:
        string hello = "Hello";
        string expected = "olleH";
        // Act:
        string result = StringUtils.ReverseString(hello);
        // Act and Assert:
        Assert.AreEqual(expected, result);
    }
    
    [TestMethod]
    public void Reverse_OneLetterString_ProducesSameString()
    {
        // Arrange:
        string letter = "A";
        string expected = "A";
        // Act:
        string result = StringUtils.ReverseString(letter);
        // Act and Assert:
        Assert.AreEqual(expected, result);
    }
    
    [TestMethod]
    public void Reverse_EmptyString_ProducesEmptyString()
    {
        // Arrange:
        string empty = "";
        string expected = "";
        // Act:
        string result = StringUtils.ReverseString(empty);
        // Act and Assert:
        Assert.AreEqual(expected, result);
    }
}