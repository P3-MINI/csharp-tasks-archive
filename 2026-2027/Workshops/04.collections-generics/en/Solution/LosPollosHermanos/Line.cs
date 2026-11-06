using System.Collections;

namespace LosPollosHermanos;

public class Line<T> : IEnumerable<T> where T : class {
    public int Capacity { get; }
    private readonly List<T> _items = [];

    public Line(int capacity) => Capacity = capacity;
    public int Count => _items.Count;
    public bool IsFull => _items.Count >= Capacity;
    public bool IsEmpty => _items.Count == 0;

    // TODO
    public bool Add(T item) {
        if (_items.Count >= Capacity) {
            return false;
        }
        _items.Add(item);
        return true;
    }

    // TODO
    public T? Take() {
        if (_items.Count == 0) {
            return null;
        }
        var item = _items[0];
        _items.RemoveAt(0);
        return item;
    }

    // TODO
    public T? Check() {
        return _items.Count == 0 ? null : _items[0];
    }

    // TODO
    public IEnumerator<T> GetEnumerator() {
        for (int i = 0; i < _items.Count; i++) {
            yield return _items[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}