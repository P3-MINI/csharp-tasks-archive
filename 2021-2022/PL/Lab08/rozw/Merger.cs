using System.Collections;

namespace Lab8
{
    /// <summary>
    /// Interfejs łączenia dwóch sekwencji w jedną według jakiejś reguły
    /// </summary>
    public interface IMerger
    {
        /// <summary>
        /// Nazwa metody łączenia sekwencji
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Metoda łącząca sekwencji
        /// </summary>
        /// <param name="IEnumerable1">Pierwsza sekwencji</param>
        /// <param name="IEnumerable2">Druga sekwencji</param>
        /// <returns>Sekwencja będąca wynikiem połączenia pierwszej i drugiej sekwencji</returns>
        IEnumerable Merge(IEnumerable sequence1, IEnumerable sequence2);
    }

    public class Add : IMerger
    {

        public string Name
        {
            get { return "Added values"; }
        }

        public IEnumerable Merge(IEnumerable sequence1, IEnumerable sequence2)
        {
            IEnumerator enum1 = sequence1.GetEnumerator();
            IEnumerator enum2 = sequence2.GetEnumerator();
            while (enum1.MoveNext() && enum2.MoveNext())
                yield return (int)enum1.Current + (int)enum2.Current;
        }
    }

}
