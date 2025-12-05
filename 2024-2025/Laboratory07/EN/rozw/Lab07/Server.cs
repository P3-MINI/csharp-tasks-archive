using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Lab07;

public class Server
    : IAddressable, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private string _name;
    private Status _status;
    private double _load;

    public string Address { get; init; }

    public string Name
    {
        get => _name;
        set
        {
            if (_name != value)
            {
                _name = value;
                OnPropertyChanged();
            }
        }
    }

    public Status Status
    {
        get => _status;
        set
        {
            if (_status != value)
            {
                _status = value;
                OnPropertyChanged();
            }
        }
    }

    public double Load
    {
        get => _load;
        set
        {
            if (_load != value)
            {
                _load = value;
                OnPropertyChanged();
            }
        }
    }

    public Server(string address, string name, Status status = Status.Stopped, double load = 0.0)
    {
        Address = address;
        _name = name;
        _status = status;
        _load = load;
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public override string ToString()
    {
        return $"{Name} [{Address}]";
    }
}