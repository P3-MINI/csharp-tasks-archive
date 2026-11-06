using System.Collections;

namespace BreakingBad;

public class Container<T> : IEnumerable<T> where T : class {
    public int Capacity { get; }
    private readonly List<T> _items = [];

    public Container(int capacity) => Capacity = capacity;
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
        int last = _items.Count - 1;
        var item = _items[last];
        _items.RemoveAt(last);
        return item;
    }

    // TODO
    public T? Check() {
        return _items.Count == 0 ? null : _items[^1];
    }

    // TODO
    public IEnumerator<T> GetEnumerator() {
        for (int i = _items.Count - 1; i >= 0; i--) {
            yield return _items[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}