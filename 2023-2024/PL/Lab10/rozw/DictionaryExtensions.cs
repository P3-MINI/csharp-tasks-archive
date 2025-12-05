namespace Lab10;

/// <summary>
/// Zaimplementuj metodę rozszerzającą interfejs IDictionary&lt;TKey, TValue&gt; o nazwie AddOrUpdate.
/// Ta metoda powinna dodawać nowe elementy do słownika jeżeli wartość pod wskazanym kluczem nie istnieje
/// lub aktualizować istniejącą wartość na podstawie klucza i poprzedniej wartości. <br/>
/// Metoda powinna przyjmować następujące parametry:<br/>
/// - TKey key: klucz, który ma zostać dodany lub zaktualizowany<br/>
/// - TValue addValue: wartość, którą należy dodać, jeśli klucz nie istnieje w słowniku.<br/>
/// - Func updateValueFactory: funkcja która na podstawie poprzedniej wartości i klucza
///   definiuje jak zaktualizować poprzednią wartość. Powinna przyjmować stary klucz TKey
///   i poprzednią wartość TValue i zwracać nową wartość TValue.<br/>
/// Metoda powinna zwrócić nowo dodaną wartość.<br/>
/// Następnie zaimplementuj metodę CountWords zliczającą liczbę wystąpień słów w sekwencji stringów
/// używając poprzednio zaimplementowanej metody. Metoda ta ma zwrócić IDictionary&lt;string, int&gt;,
/// gdzie kluczem są słowa, a wartością jest liczba wystąpień w sekwencji.
/// </summary>
public static class DictionaryExtensions
{
    public static TValue AddOrUpdate<TKey, TValue>(
        this IDictionary<TKey, TValue> dictionary,
        TKey key,
        TValue addValue,
        Func<TKey, TValue, TValue> updateValueFactory)
        where TKey : notnull
    {
        return dictionary.TryAdd(key, addValue) ? addValue : (dictionary[key] = updateValueFactory(key, dictionary[key]));
    }

    public static IDictionary<string, int> CountWords(IEnumerable<string> words)
    {
        var counts = new Dictionary<string, int>();
        foreach (var word in words)
        {
            counts.AddOrUpdate(word, 1, (_, count) => count + 1);
        }

        return counts;
    }
}