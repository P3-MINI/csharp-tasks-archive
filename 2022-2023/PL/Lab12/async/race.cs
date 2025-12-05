
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

class MoveEventArgs : EventArgs
    {
    public readonly int move;
    public MoveEventArgs (int m) => move=m;
    }

class Race
    {

    private int distance;
    private int place = 0;
    private Dictionary<string,int> position = new Dictionary<string,int>();
    private EventHandler run = null;

    public event EventHandler Run
        {
        add
            {
            run += value;
            var d = value.Target as Dog;  // Target property refers to the object on which the current delegate invokes the instance method
            position[d.Name] = 0;
            d.Move += this.Move;
            }
        remove
            {
            run -= value;
            }
        }

    public Race(int dist) => distance=dist;

    public void Move(object sender, MoveEventArgs e)
        {
        var d = sender as Dog;
        position[d.Name] += e.move;
        if ( position[d.Name]<distance )
            Console.WriteLine($" {d.Name} has run {position[d.Name]} meters");
        else
            {
            Console.WriteLine($" {d.Name} has come on {++place} place");
            d.Stop();
            }
        }

    public void Start()
        {
        Console.WriteLine("\n*** Race started ***\n");
        Task[] dogs = new Task[run.GetInvocationList().Length];
        int i = 0;
        foreach ( EventHandler dog in run.GetInvocationList() )
            dogs[i++] = Task.Run(()=>dog(this,EventArgs.Empty));
        Task.WaitAll(dogs);
        Console.WriteLine("\n*** Race completed ***\n");
        }

    }

class Dog
    {

    private static Random random = new Random();

    private bool isRunning;

    public readonly string Name;

    public event EventHandler<MoveEventArgs> Move;

    public Dog(string n) => Name=n;

    public void Run(object sender, EventArgs e)
        {
        isRunning = true;
        Console.WriteLine($" {Name} launched");
        while ( isRunning )
            {
            Thread.Sleep(random.Next(2000));
            Move(this,new MoveEventArgs(random.Next(1,9)));
            }
        }

    public void Stop() => isRunning = false;

    }

class Example
    {

    public static void Main()
        {
        Race race = new Race(20);
        Dog d1 = new Dog("Azor");
        Dog d2 = new Dog("Burek");
        Dog d3 = new Dog("Reksio");
        Dog d4 = new Dog("Szarik");
        race.Run += d1.Run;
        race.Run += d2.Run;
        race.Run += d3.Run;
        race.Run += d4.Run;
        race.Start();
        }

    }
