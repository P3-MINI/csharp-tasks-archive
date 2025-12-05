namespace Lab07;

public static class Extensions
{
    public static Server? GetByAddress(
        this ServerSystem system,
        string address)
    {
        return system.FirstOrDefault(x => x.Address == address);
    }

    public static void SetRedirectionTrafficPolicy(
        this ServerSystem system,
        double loadThreshold,
        Action<Server> redirectionPolicy)
    {
        foreach (var server in system)
        {
            server.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(Server.Load) &&
                    sender is Server s &&
                    s.Load > loadThreshold)
                {
                    redirectionPolicy(server);
                }
            };
        }
    }

    public static IEnumerable<Server> MaintenanceOrder(
        this ServerSystem system,
        Comparison<Server> priority)
    {
        var servers = system.ToList();
        servers.Sort(priority);
        return servers;
    }
}