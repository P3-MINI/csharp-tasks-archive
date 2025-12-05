using System.ComponentModel;

namespace Lab07;

public interface INotifyingCollection<T> : IEnumerable<T>
    where T : IAddressable, INotifyPropertyChanged
{
    event EventHandler<CollectionChangedEventArgs<T>>? ElementAdded;
    event EventHandler<CollectionChangedEventArgs<T>>? ElementRemoved;
    event EventHandler<ElementChangedEventArgs<T>>? ElementPropertyChanged;

    bool Add(T element);
    bool Remove(string address);
}
