using Utilities;

namespace UtilitiesTests;

[TestClass]
public class ArrayUtilsTests
{
    [TestMethod]
    public void SelectionSort_AlreadySorted_Unchanged()
    {
        // Arrange:
        int[] array = { 1, 2, 3, 4, 5 };
        int[] expected = { 1, 2, 3, 4, 5 };
        // Act:
        array.SelectionSort();
        // Assert:
        CollectionAssert.AreEqual(expected, array);
    }

    [TestMethod]
    public void SelectionSort_Unsorted_ShouldBeSorted()
    {
        // Arrange:
        int[] array = { 4, 2, 5, 1, 3 };
        int[] expected = { 1, 2, 3, 4, 5 };
        // Act:
        array.SelectionSort();
        // Assert:
        CollectionAssert.AreEqual(expected, array);
    }

    [TestMethod]
    public void SelectionSort_EmptyArray_Unchanged()
    {
        // Arrange:
        int[] array = Array.Empty<int>();
        int[] expected = Array.Empty<int>();
        // Act:
        array.SelectionSort();
        // Assert:
        CollectionAssert.AreEqual(expected, array);
    }
}