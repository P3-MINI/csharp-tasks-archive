using System;

namespace Lab12
{

public class NotifingObject : IChangeNotifing
    {

    private string _name;

    public event EventHandler NameChanged;

    public string Name
        {
        get { return _name; }
        set
            {
            _name = value;
            OnChanged();
            }
        }

    public override string ToString()
        {
        return this.Name;
        }

    private void OnChanged()
        {
        var h = NameChanged;
        if ( h!=null )
            NameChanged(this, EventArgs.Empty);
        }

    }

}
