using System;
using System.Collections.Generic;

namespace Lab12
{

public class ObservableCollection : IObservableCollection
    {

    private List<object> _list = new List<object>();

    public ObservableCollection(string name)
        {
        this.Name = name;
        }

    public event EventHandler<CollectionCahngedEventArgs> CollectionCahnged;

    public string Name { get; private set; }

    public void Add(object value)
        {
        _list.Add(value);
        var changeNotifing = value as IChangeNotifing;
        if ( changeNotifing!=null)
            changeNotifing.NameChanged += OnValueChanged;
        OnCollectionChanged(CollectionOperation.Add, value);
        }

    public void Remove(object value)
        {
        _list.Remove(value);
        var changeNotifing = value as IChangeNotifing;
        if ( changeNotifing!=null)
            changeNotifing.NameChanged -= OnValueChanged;
        OnCollectionChanged(CollectionOperation.Remove, value);
        }

    private void OnCollectionChanged(CollectionOperation operation, object value)
        {
        var h = CollectionCahnged;
        if ( h!=null )
            h(this, new CollectionCahngedEventArgs(operation, value));
        }

    private void OnValueChanged(object sender, EventArgs e)
        {
        OnCollectionChanged(CollectionOperation.ValueChanged, sender);
        }

    }

}
