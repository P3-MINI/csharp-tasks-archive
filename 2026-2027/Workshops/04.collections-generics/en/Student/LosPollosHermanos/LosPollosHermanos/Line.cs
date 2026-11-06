using System.Collections;

namespace LosPollosHermanos;

public class Line<T> : IEnumerable<T> where T : class {
    // maximum number of items stored on the line
    public int Capacity { get; }

    // the actual data structure for storing the items
    private readonly List<T> _items = [];

    public Line(int capacity) => Capacity = capacity;
    public int Count => _items.Count;
    public bool IsFull => _items.Count >= Capacity;
    public bool IsEmpty => _items.Count == 0;

    public bool Add(T item) {
        throw new NotImplementedException(); // <-- replace with your own implementation
    }

    public T? Take() {
        throw new NotImplementedException(); // <-- replace with your own implementation
    }

    public T? Check() {
        throw new NotImplementedException(); // <-- replace with your own implementation
    }

    public IEnumerator<T> GetEnumerator() {
        throw new NotImplementedException(); // <-- replace with your own implementation
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}