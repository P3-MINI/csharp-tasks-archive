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



		//-----------------------------------------------------------
#endif // STAGE03
	}
}