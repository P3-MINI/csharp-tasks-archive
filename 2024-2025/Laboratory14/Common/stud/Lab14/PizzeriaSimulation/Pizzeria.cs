using System.Collections.Concurrent;

namespace PizzeriaSimulation;

/// <summary>
/// This record will be used in our simulation to make it more attractive.
/// </summary>
public sealed record PizzaOrder(string Name, int Size, string Toppings, decimal Price)
{
	public override string ToString()
	{
		return $"{Name} of size {Size} with {Toppings} (${Price:F2})";
	}
}

public sealed class Pizzeria : IDisposable
{
	public int ChefsCount { get; }
	public int DeliverersCount { get; }
	public int PizzaQueueCapacity { get; }

	public Pizzeria(int chefsCount, int deliverersCount, int pizzaQueueCapacity)
	{
		ChefsCount = chefsCount;
		DeliverersCount = deliverersCount;
		PizzaQueueCapacity = pizzaQueueCapacity;

		_queue = new ParallelQueue<PizzaOrder>(maxSize: pizzaQueueCapacity);

		// modify and use this barrier in STAGE04
		_barrier = new Barrier(participantCount: default);
	}

	public void DisplayControls()
	{
		Console.Title = "Pizzeria Simulation";
		Console.WriteLine($"=> Number of chefs: {ChefsCount}");
		Console.WriteLine($"=> Number of deliverers: {DeliverersCount}");
		Console.WriteLine($"=> Pizza queue capacity: {PizzaQueueCapacity}");
		Console.WriteLine();
		Console.WriteLine("Control:");
		Console.WriteLine("  p - Pause pizza preparation");
		Console.WriteLine("  r - Resume pizza preparation");
		Console.WriteLine("  k - Pause delivery");
		Console.WriteLine("  l - Resume delivery");
		Console.WriteLine("  b - Sum up daily income");
		Console.WriteLine("  q - Exit");
		Console.Out.Flush();
	}

	public void Dispose()
	{
		_queue?.Dispose();
		_barrier?.Dispose();
	}

	public async Task StartSimulationAsync()
	{
		var cancellationTokenSource = new CancellationTokenSource();

		// use the token below in STAGE04
		var cancellationToken = cancellationTokenSource.Token;

		// implement the methods below in STAGE03 and STAGE04
		var chefs = StartChefs(cancellationToken);
		var drivers = StartDeliverers(cancellationToken);

		var task = Task.Run(() =>
		{
			while (!cancellationToken.IsCancellationRequested)
			{
				var input = Console.ReadKey(intercept: true).Key;

				switch (input)
				{
					case ConsoleKey.P:
						_queue.PauseEnqueue();
						Console.WriteLine("=> Pizza preparation has been paused...");
						break;
					case ConsoleKey.R:
						_queue.ResumeEnqueue();
						Console.WriteLine("=> Pizza preparation has been resumed.");
						break;
					case ConsoleKey.K:
						_queue.PauseDequeue();
						Console.WriteLine("=> Deliveries have been paused...");
						break;
					case ConsoleKey.L:
						_queue.ResumeDequeue();
						Console.WriteLine("Deliveries have been resumed...");
						break;
					case ConsoleKey.B:
						if (_barrier.ParticipantsRemaining == 1)
						{
							Console.WriteLine("Waiting for chefs and deliverers to finish processing today's orders...");
							_barrier.SignalAndWait(cancellationToken);
						}
						break;
					case ConsoleKey.Q:
						Console.WriteLine("Closing the pizzeria...");
						cancellationTokenSource.Cancel();
						break;
				}
			}
		});

		await task;
		await Task.WhenAll(chefs);
		await Task.WhenAll(drivers);

		Console.WriteLine("The pizzeria is closed for the day.");
	}

	/// <summary>
	/// Implement this method in STAGE03
	/// Feel free to change the signature of the method (if need be).
	/// </summary>
	/// <param name="cancellationToken">Token used to stop the simulation</param>
	/// <returns>A collection of tasks responsible for delivering pizzas.</returns>
	private Task[] StartDeliverers(CancellationToken cancellationToken)
	{
		return [];
	}

	/// <summary>
	/// Implement this method in STAGE03
	/// Feel free to change the signature of the method (if need be).
	/// </summary>
	/// <param name="cancellationToken">Token used to stop the simulation</param>
	/// <returns>A collection of tasks responsible for preparing pizzas.</returns>
	private Task[] StartChefs(CancellationToken cancellationToken)
	{
		return [];
	}

	/// <summary>
	/// Use this method to generate a random pizza in a thread-safe way.
	/// </summary>
	/// <returns>A random pizza :)</returns>
	private static PizzaOrder GeneratePizzaOrder()
	{
		var pizza = new PizzaOrder(
			_pizzaNames[Random.Shared.Next(_pizzaNames.Length)],
			Random.Shared.Next(8, 16),
			_pizzaToppings[Random.Shared.Next(_pizzaToppings.Length)],
			(decimal)(Random.Shared.NextDouble() * 10) + 20
		);

		return pizza;
	}

	private Barrier _barrier;
	private ParallelQueue<PizzaOrder> _queue;
	private static ConcurrentDictionary<string, decimal> _dailyIncomeDictionary = [];

	private const int _dailyPizzaOrders = 5;
	private static readonly string[] _pizzaNames = new[] { "Margherita", "Pepperoni", "Hawaiian", "Veggie", "BBQ Chicken" };
	private static readonly string[] _pizzaToppings = new[] { "Cheese", "Olives", "Mushrooms", "Onions", "Bacon", "Spinach" };
}