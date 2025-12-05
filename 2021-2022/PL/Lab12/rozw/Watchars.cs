using System;

namespace Lab12
{

public class SimpleWatcher
    {

    public void Watch(IObservableCollection obj)
        {
        obj.CollectionCahnged += OnChanged;
        }

    public void StopWatching(IObservableCollection obj)
        {
        obj.CollectionCahnged -= OnChanged;
        }

    private void OnChanged(object sender, CollectionCahngedEventArgs e)
        {
        Console.WriteLine("collection changed");
        }

    }

public class SmartWatcher
    {

    public void Watch(IObservableCollection obj)
        {
        obj.CollectionCahnged += OnChanged;
        }

    public void StopWatching(IObservableCollection obj)
        {
        obj.CollectionCahnged -= OnChanged;
        }

    private void OnChanged(object sender, CollectionCahngedEventArgs e)
        {
        Console.WriteLine("collection: {0} changed, value: {1} was {2}", ((IObservableCollection)sender).Name, e.Value.ToString(),
                          e.Operation==CollectionOperation.Add ? "added" : e.Operation==CollectionOperation.Remove ? "removed" : "changed");
        }

    }

}
