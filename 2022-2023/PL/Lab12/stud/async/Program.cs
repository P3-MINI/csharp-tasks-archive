using System;

namespace Lab12
{

public enum CollectionOperation
    {
    Add,
    Remove,
    ValueChanged
    }

public interface IObservableCollection
    {
    event EventHandler<CollectionCahngedEventArgs> CollectionCahnged;
    string Name { get; }
    void Add(object value);
    void Remove(object value);
    }

public interface IChangeNotifing
    {
    event EventHandler NameChanged;
    }

public class CollectionCahngedEventArgs : EventArgs
    {

    public CollectionCahngedEventArgs(CollectionOperation operation, object value)
        {
        this.Operation = operation;
        this.Value = value;
        }

    public CollectionOperation Operation { get; private set; }
    public object Value { get; private set; }

    }

public class Program
    {

    public static void Main()
        {
        // ETAP 1
        Console.WriteLine("\nETAP 1\n");

        //var collection = new ObservableCollection("[collection 1]");
        //var simpleWatcher = new SimpleWatcher();

        //collection.Add("[First item]");

        //simpleWatcher.Watch(collection);

        //collection.Add("[Second item]");
        //collection.Remove("[First item]");

        // ETAP 2
        Console.WriteLine("\nETAP 2\n");

        //var smartWatcher = new SmartWatcher();
        //smartWatcher.Watch(collection);
        //collection.Add("[Third item]");
        //Console.WriteLine("-------------------------------");

        //simpleWatcher.StopWatching(collection);
        //collection.Remove("[Third item]");

        // ETAP 3
        Console.WriteLine("\nETAP 3\n");

        //var object1 = new NotifingObject();
        //var object2 = new NotifingObject();
        //object1.Name = "[o1]";
        //object2.Name = "[o2]";

        //collection.Add(object1);
        //collection.Add(object2);

        //Console.WriteLine("-------------------------------");

        //object1.Name = "[new o1]";
        //object2.Name = "[new o2]";

        //Console.WriteLine("-------------------------------");

        //collection.Remove(object2);

        //Console.WriteLine("-------------------------------");

        //object1.Name = "[even newer o1]";
        //object2.Name = "[even newer o2]";

        Console.WriteLine();
        }

    }

}
