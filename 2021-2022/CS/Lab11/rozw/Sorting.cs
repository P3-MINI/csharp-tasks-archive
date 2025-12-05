
public static class ArrayExtension
    {

    public static void InsertionSort(this int[] tab)
        {
        int i, j;
        int v;
        for ( i=1 ; i<tab.Length ; ++i )
            {
            v = tab[i];
            for ( j=i-1 ; j>=0 && v<tab[j] ; --j )
                tab[j+1] = tab[j];
            tab[j+1] = v;
            }
        }

    public static void SelectionSort(this int[] tab)
        {
        int i, j, jm;
        int v;
        for ( i=0 ; i<tab.Length-1 ; ++i )
            {
            jm=i;
            for ( j=i+1 ; j<tab.Length ; ++j )
                if ( tab[j]<tab[jm] ) jm=j;
            v = tab[i];
            tab[i] = tab[jm];
            tab[jm] = v;
            }
        }

    }