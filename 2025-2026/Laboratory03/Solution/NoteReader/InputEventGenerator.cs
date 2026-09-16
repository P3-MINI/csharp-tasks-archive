using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class InputEventGenerator
{
    public event EventHandler<ConsoleKeyInfo>? Input;
    public InputEventGenerator(CancellationToken cancellationToken)
    {
        thread = new Thread(() =>
        {
            while (!cancellationToken.IsCancellationRequested)
                Input?.Invoke(this, Console.ReadKey(true));
        });
        thread.Start();
    }
    private Thread thread;
}

