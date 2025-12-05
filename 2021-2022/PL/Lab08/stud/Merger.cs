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

}
