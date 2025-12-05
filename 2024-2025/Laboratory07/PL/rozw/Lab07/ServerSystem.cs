using System.Collections;
using System.ComponentModel;

namespace Lab07;

public class ServerSystem : INotifyingCollection<Server>
{
    public event EventHandler<CollectionChangedEventArgs<Server>>? ElementAdded;
    public event EventHandler<CollectionChangedEventArgs<Server>>? ElementRemoved;
    public event EventHandler<ElementChangedEventArgs<Server>>? ElementPropertyChanged;

    public readonly Dictionary<string, Server> _servers = [];

    public bool Add(Server server)
    {
        if (_servers.TryAdd(server.Address, server))
        {
            server.PropertyChanged += OnServerPropertyChanged;
            OnServerAdded(server);
            return true;
        }

        return false;
    }

    public bool Remove(string address)
    {
        if (_servers.TryGetValue(address, out var server) &&
            _servers.Remove(address))
        {
            server.PropertyChanged -= OnServerPropertyChanged;
            OnServerRemoved(server);
            return true;
        }

        return false;
    }

    public IEnumerator<Server> GetEnumerator() => _servers.Values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _servers.Values.GetEnumerator();

    protected virtual void OnServerAdded(Server server)
    {
        ElementAdded?.Invoke(this, new CollectionChangedEventArgs<Server>(server));
    }

    protected virtual void OnServerRemoved(Server server)
    {
        ElementRemoved?.Invoke(this, new CollectionChangedEventArgs<Server>(server));
    }

    protected virtual void OnServerPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender is Server server)
        {
            ElementPropertyChanged?.Invoke(this, new ElementChangedEventArgs<Server>(server, e.PropertyName));
        }
    }
}