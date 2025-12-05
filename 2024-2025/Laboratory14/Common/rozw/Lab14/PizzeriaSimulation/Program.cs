namespace PizzeriaSimulation;

class Program
{
	static async Task Main(string[] args)
	{
		var chefsCount = args.Length > 0
			? int.Parse(args[0])
			: Environment.ProcessorCount / 2;

		var deliverersCount = args.Length > 1
			? int.Parse(args[1])
			: Environment.ProcessorCount / 2;

		var pizzaQueueCapacity = args.Length > 2
			? int.Parse(args[2])
			: 10;

		using var pizzeria = new Pizzeria(chefsCount, deliverersCount, pizzaQueueCapacity);

		pizzeria.DisplayControls();

		await pizzeria.StartSimulationAsync();
	}
}