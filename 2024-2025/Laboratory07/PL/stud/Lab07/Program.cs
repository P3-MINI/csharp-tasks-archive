//#define STAGE01
//#define STAGE02
//#define STAGE03

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

		Console.WriteLine("Admin has added a new rule (Rule75)!");

		//-----------------------------------------------------------
		// Add your code for here...



		//-----------------------------------------------------------

		Console.WriteLine($"{servers[0]} is getting overloaded...");
		servers[0].Load = 55;
		servers[0].Load = 70;
		servers[0].Load = 75;
		servers[0].Load = 80;

		if (servers[0].Status == Status.OverLoaded)
		{
			Console.WriteLine($"System reacted to {servers[0]} overloading correctly!");
		}
		else
		{
			Console.WriteLine($"System didn't react to {servers[0]} overloading :(");
		}

		servers[0].Load = 90;

		Console.WriteLine();

		var newServers = new List<Server>()
		{
			new("192.168.1.60", "Server05"),
			new("192.168.1.70", "Server06"),
			new("192.168.1.80", "Server07")
		};

		Console.WriteLine("Adding new servers to the system:");
		newServers.ForEach(server => system.Add(server));
		newServers[0].Status = Status.Failed;
		newServers[1].Status = Status.Failed;
		newServers[2].Status = Status.Stopped;

		Console.WriteLine();

		Console.WriteLine("Performing clustering...");

		//-----------------------------------------------------------
		// Add your code for here...



		//-----------------------------------------------------------

		Console.WriteLine("Shutting down the system...");
		system.Shutdown();

#endif // STAGE03
	}
}