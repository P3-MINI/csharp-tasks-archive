#define STAGE01
#define STAGE02
#define STAGE03

using System.ComponentModel;

namespace Lab07;

public class Program
{

    static void Main(string[] args)
    {
#if STAGE01
        Console.WriteLine("STAGE01:\n");

        var firstServer = new Server("192.168.1.10", "Server00", Status.Running, 25);
        Console.WriteLine(firstServer);

        //-----------------------------------------------------------
        // Add your code for here...
        firstServer.PropertyChanged += (sender, e) =>
        {
            if (sender is not Server server) return;

            var value = e.PropertyName switch
            {
                nameof(Server.Load) => $"{server.Load}",
                nameof(Server.Status) => $"{server.Status}",
                nameof(Server.Name) => $"{server.Name}",
                _ => "Unknown"
            };

            Console.WriteLine($"[{server.Address}]: {e.PropertyName} => {value}");
        };
        //-----------------------------------------------------------

        firstServer.Status = Status.Stopped;
        firstServer.Load = 0.0;
        Console.WriteLine();

#endif // STAGE01
#if STAGE02
        Console.WriteLine("STAGE02:\n");

        var random = new Random(2137);
        var system = new ServerSystem();
        var servers = new List<Server> {
            new("192.168.1.10", "Server00"),
            new("192.168.1.20", "Server01"),
            new("192.168.1.30", "Server02"),
            new("192.168.1.40", "Server03")
        };

        //-----------------------------------------------------------
        // Add your code for here...
        system.ElementAdded += (_, e) =>
        {
            Console.WriteLine($"Added {e.Element}");
            e.Element.Status = Status.Running;
            e.Element.Load = random.Next(40, 50);
        };

        system.ElementRemoved += (_, e) => Console.WriteLine($"Removed {e.Element}");

        system.ElementPropertyChanged += (_, e) =>
        {
            var value = e.PropertyName switch
            {
                nameof(Server.Load) => $"{e.Element.Load}",
                nameof(Server.Status) => $"{e.Element.Status}",
                nameof(Server.Name) => $"{e.Element.Name}",
                _ => "Unknown"
            };

            Console.WriteLine($"[{e.Element.Address}]: {e.PropertyName} => {value}");
        };

        servers.ForEach(server => system.Add(server));

        //-----------------------------------------------------------

        Console.WriteLine();

        var server03 = servers[3];
        system.Remove(server03.Address);
        servers.Remove(server03);

        server03.Load = 90;
        server03.Status = Status.Failed;

        if (!system.Remove(server03.Address))
        {
            Console.WriteLine($"Couldn't remove server {server03} (it has already been removed).");
        }

        Console.WriteLine();

#endif // STAGE02
#if STAGE03
        Console.WriteLine("STAGE03:\n");

        var address = servers[0].Address;
        var server00 = system.GetByAddress("192.168.1.10");
        if (server00 is null)
        {
            Console.WriteLine($"Server with address {address} should be found :(\n");
        }
        else
        {
            Console.WriteLine($"{server00} retrieved successfully!\n");
        }

        Console.WriteLine("Admin has added new traffic redirection policy.");

        //-----------------------------------------------------------
        // Add your code for here...
        var threshold = 50.0;
        system.SetRedirectionTrafficPolicy(threshold, server =>
        {
            var chosen = system.FirstOrDefault(s => s.Load < threshold && s.Status == Status.Running);
            if(chosen is null)
            {
                server.Status = Status.Failed;
                Console.WriteLine($"{server}: Internal Server Error!");
                return;
            }

            var toRedirect = server.Load - threshold;
            server.Load = threshold;

            Console.WriteLine($"Redirecting load ({toRedirect}) to... {chosen}");
            chosen.Load += toRedirect;
        });
        //-----------------------------------------------------------
        
        Console.WriteLine($"{servers[0]} is getting overloaded...");
        servers[0].Load = 99;
        Console.WriteLine();

        Console.WriteLine("Adding new servers to the system:");
        system.Add(new("192.168.1.60", "Server05"));
        system.Add(new("192.168.1.70", "Server06"));
        system.Add(new("192.168.1.80", "Server07"));

        Console.WriteLine();

        Console.WriteLine("Getting the maintenance order...");

        //-----------------------------------------------------------
        // Add your code for here...
        var ordered = system.MaintenanceOrder((s, t) =>
        {
            if (s.Status == Status.Failed && t.Status != Status.Failed) return -1;
            if (s.Status != Status.Failed && t.Status == Status.Failed) return 1;

            return t.Load.CompareTo(s.Load);
        });

        foreach (var s in ordered)
        {
            Console.WriteLine($"{s} -> ({s.Status}, {s.Load})");
        }
        //-----------------------------------------------------------
        
#endif // STAGE03
    }
}