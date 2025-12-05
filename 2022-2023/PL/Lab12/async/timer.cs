
using System;
using System.Threading;

public class TimeEventArgs : EventArgs
    {
    public readonly DateTime time;
    public TimeEventArgs(DateTime t) => time = t;
    }

class MyTimer
    {
    public event EventHandler<TimeEventArgs> OnSecondChanged = null;

    public void Run()
        {
        DateTime last = DateTime.Now;
        DateTime curr;
        while ( !Console.KeyAvailable )
            {
            Thread.Sleep(50);  //   50 miliseconds
            curr = DateTime.Now;
            if ( curr.Second!=last.Second )
                {
                // if ( OnSecondChanged!=null )                                         // this code
                //     OnSecondChanged.Invoke(this,new TimeEventArgs(curr));            // is incorrect!

                OnSecondChanged?.Invoke(this,new TimeEventArgs(curr));                  // correct - C# version 6 or newer

                // EventHandler<TimeEventArgs> localOnSecondChanged = OnSecondChanged;  //
                // if ( localOnSecondChanged!=null )                                    // correct - C# versions earlier then 6
                //     localOnSecondChanged.Invoke(this,new TimeEventArgs(curr));       //

                last = curr;
                }
            }
        }
    }

class FileSave
    {
    int count = 0;
    public void Write(object sender, TimeEventArgs e)
        {
        count = (count+1)%5;
        if ( count==0 )
            Console.WriteLine("\nFile save:      {0:00}:{1:00}:{2:00}\n",e.time.Hour,e.time.Minute,e.time.Second);
        }
    }

class Test
    {
    public static void Main()
        {
        MyTimer  t   = new MyTimer();
        FileSave fs  = new FileSave();
        t.OnSecondChanged += (object sender, TimeEventArgs e) =>
                                 Console.WriteLine("Current time:   {0:00}:{1:00}:{2:00}",e.time.Hour,e.time.Minute,e.time.Second);
        t.OnSecondChanged += fs.Write;
        t.Run();
        }
    }
