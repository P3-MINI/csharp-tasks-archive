using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

class MoveEventArgs : EventArgs
{
    public readonly int Movement;
    public MoveEventArgs(int m)
    {
        Movement = m;
    }
}

interface IParticipant
{
    event EventHandler<MoveEventArgs> Moved;
    string Name { get; }
    void Run(object sender, EventArgs eventArgs);
    void OnFinishLineCrossed();
}

class Dog : IParticipant
{  
    public string Name { get; private set; }
    public event EventHandler<MoveEventArgs> Moved;
    private static readonly Random random = new Random();
    private bool isRunning = true;

    public Dog(string name)
    {
        Name = name;
    }

    public void Run(object sender, EventArgs e)
    {
        Console.WriteLine($" {Name} launched");
        while (isRunning)
        {
            Thread.Sleep(random.Next(2000));
            Moved(this, new MoveEventArgs(random.Next(1, 9)));
        }
    }

    public void OnFinishLineCrossed()
    {
        isRunning = false;
    }
}

class Race
{
    private readonly int targetDistance;
    private int ranking;
    private readonly Dictionary<IParticipant,int> distances = new Dictionary<IParticipant,int>();

    private readonly HashSet<object> participants = new HashSet<object>();
    private readonly List<Task> participantTasks = new List<Task>();

    private EventHandler raceStarted = null;
    public event EventHandler RaceStarted
    {
        add
        {
            if (value.Target is IParticipant)
            {
                if (!participants.Contains(value.Target))
                {
                    participants.Add(value.Target);
                    raceStarted += value;
                }
            }
        }
        remove
        {
            if (participants.Contains(value.Target))
            {
                participants.Remove(value.Target);
                raceStarted -= value;
            } 
        }
    }

    public Race(int distance) 
    {
        targetDistance = distance;
    }

    private void OnParticipantMove(object sender, MoveEventArgs e)
    {
        var participant = sender as IParticipant;
        distances[participant] += e.Movement;
        int totalParticipantDistance = distances[participant];
        if (totalParticipantDistance < targetDistance)
        {
            Console.WriteLine("{0} has run {1} meters and traveled {2} meters so far",
                participant.Name, e.Movement, totalParticipantDistance);
        }
        else
        {
            Console.WriteLine($"{participant.Name} has finished the race! Ranking: {++ranking}");
            participant.OnFinishLineCrossed();
            participant.Moved -= OnParticipantMove;
        }
    }

    public void Start()
    {
        Console.WriteLine("Poof!");
        foreach (EventHandler eh in raceStarted.GetInvocationList())
        {
            var participant = eh.Target as IParticipant;
            Console.WriteLine($"Participant {participant.Name} hears the shoot");
            distances[participant] = 0;
            // subscribe to movement of participant so that we can update our ranking
            // we're doing it here so that we handle move events only during race
            participant.Moved += this.OnParticipantMove;
            participantTasks.Add(
                Task.Run(() => eh.Invoke(this, EventArgs.Empty))
            );

        }

        Task.WaitAll(participantTasks.ToArray());
    }
}


class Example
{

    public static void Main()
    {
        var race = new Race(20);

        Dog d1 = new Dog("Azor");
        Dog d2 = new Dog("Burek");
        Dog d3 = new Dog("Reksio");
        Dog d4 = new Dog("Szarik");

        var dogs = new [] { d1, d2, d3, d4 };

        foreach (var dog in dogs)
        {
            race.RaceStarted += dog.Run;
        }

        Console.WriteLine("Ready?");  Thread.Sleep(1000);
        Console.WriteLine("Set!");  Thread.Sleep(1000);

        race.Start();
        Console.WriteLine("*** Race finished ***");
    }
}

