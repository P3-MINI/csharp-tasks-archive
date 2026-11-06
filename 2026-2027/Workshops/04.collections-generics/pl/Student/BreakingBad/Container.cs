using System.Collections;

namespace BreakingBad;

public class Container<T> : IEnumerable<T> where T : class {
    // maksymalna liczba elementów w kontenerze
    public int Capacity { get; }
    // struktura, która tak naprawdę przechowuje elementy
    private readonly List<T> _items = [];

    public Container(int capacity) => Capacity = capacity;
    public int Count => _items.Count;
    public bool IsFull => _items.Count >= Capacity;
    public bool IsEmpty => _items.Count == 0;

    public bool Add(T item) {
        throw new NotImplementedException(); // <-- zastąp swoją implementacją
    }

    public T? Take() {
        throw new NotImplementedException(); // <-- zastąp swoją implementacją
    }

    public T? Check() {
        throw new NotImplementedException(); // <-- zastąp swoją implementacją
    }

    public IEnumerator<T> GetEnumerator() {
        throw new NotImplementedException(); // <-- zastąp swoją implementacją (podpowiedź: yield return)
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
} 