namespace Utilities;

public static class ArrayUtils
{
    public static void SelectionSort(this int[] tab)
    {
        for (var i = 0; i < tab.Length - 1; ++i)
        {
            int jm = i;
            for (int j = i + 1; j < tab.Length; ++j)
                if (tab[j] < tab[jm]) jm = j;
            (tab[i], tab[jm]) = (tab[jm], tab[i]);
        }
    }
}