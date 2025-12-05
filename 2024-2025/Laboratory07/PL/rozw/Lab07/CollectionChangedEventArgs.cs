namespace Lab07;

public sealed class CollectionChangedEventArgs<T>(T element) : EventArgs
{
    public T Element { get; } = element;
}