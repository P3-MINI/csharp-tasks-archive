using System.Collections.Concurrent;

namespace PizzeriaSimulation;

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

		_barrier = new Barrier(chefsCount + deliverersCount + 1, (b) =>
		{
			Console.WriteLine("=== It's time to sum up the daily income! ===");

			foreach (var (pizza, income) in _dailyIncomeDictionary)
			{
				Console.WriteLine($"{pizza}: {income:F2}");
			}

			_dailyIncomeDictionary.Clear();
		});
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
		var cancellationToken = cancellationTokenSource.Token;

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

	private Task[] StartDeliverers(CancellationToken cancellationToken)
	{
		var deliverers = new Task[DeliverersCount];
		for (var i = 0; i < deliverers.Length; i++)
		{
			var delivererId = i + 1;
			deliverers[i] = Task.Run(async () =>
			{
				var random = new Random();
				var pizzaCounter = 0;

				try
				{
					while (!cancellationToken.IsCancellationRequested)
					{
						var pizza = await _queue.TryDequeueAsync(1000, cancellationToken);

						if (pizza != null)
						{
							await Task.Delay(random.Next(1000, 2000), cancellationToken);

							Console.WriteLine($"[Deliverer {delivererId}]: Delivered {pizza}");

							_dailyIncomeDictionary.AddOrUpdate(pizza.Name, pizza.Price, (key, old) => old + pizza.Price);

						}
						else
						{
							Console.WriteLine($"[Deliverer {delivererId}]: No pizzas available for delivery.");
						}

						pizzaCounter++;

						if (pizzaCounter == _dailyPizzaOrders)
						{
							Console.WriteLine($"[Deliverer {delivererId}]: Enough pizzas for today...");
							pizzaCounter = 0;
							_barrier.SignalAndWait(cancellationToken);
						}
					}
				}
				catch (Exception ex) when (ex is TaskCanceledException or OperationCanceledException)
				{
					Console.WriteLine($"[Deliverer {delivererId}]: Stopped working.");
				}
			}, cancellationToken);
		}

		return deliverers;
	}

	private Task[] StartChefs(CancellationToken cancellationToken)
	{
		var chefs = new Task[ChefsCount];
		for (var i = 0; i < chefs.Length; i++)
		{
			var chefId = i + 1;
			chefs[i] = Task.Run(async () =>
			{
				var random = new Random();
				var pizzaCounter = 0;

				try
				{
					while (!cancellationToken.IsCancellationRequested)
					{
						var pizza = GeneratePizzaOrder();

						await Task.Delay(random.Next(1000, 2000), cancellationToken);

						if (await _queue.TryEnqueueAsync(pizza, 1000, cancellationToken))
						{
							Console.WriteLine($"[Chef {chefId}]: Prepared {pizza}.");
						}
						else
						{
							Console.WriteLine($"[Chef {chefId}]: Couldn't place a pizza in the pizza queue.");
						}

						pizzaCounter++;

						if (pizzaCounter == _dailyPizzaOrders)
						{
							Console.WriteLine($"[Chef {chefId}]: Enough pizzas for today...");
							pizzaCounter = 0;
							_barrier.SignalAndWait(cancellationToken);
						}
					}
				}
				catch (Exception ex) when (ex is TaskCanceledException or OperationCanceledException)
				{
					Console.WriteLine($"[Chef {chefId}]: Stopped working.");
				}
			}, cancellationToken);
		}

		return chefs;
	}

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