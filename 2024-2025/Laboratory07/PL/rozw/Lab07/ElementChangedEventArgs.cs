namespace Lab07;

public sealed class ElementChangedEventArgs<T>(T element, string? propertyName) : EventArgs
{
    public T Element { get; } = element;
    public string? PropertyName { get; } = propertyName;
}