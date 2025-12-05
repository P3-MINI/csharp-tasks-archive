namespace Lab07;

public static class Extensions
{
    public static void Shutdown(this ServerSystem system)
    {
        foreach (var server in system)
        {
            server.Status = Status.Stopped;
            server.Load = 0;
        }
    }

    public static void AddLoadRule(
        this ServerSystem system,
        double loadThreshold,
        Action<Server> rule)
    {
        foreach (var server in system)
        {
            server.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(Server.Load) &&
                    sender is Server s &&
                    s.Load > loadThreshold)
                {
                    rule(server);
                }
            };
        }
    }

    public static void ClusterByStatus(
        this ServerSystem system,
        Action<IGrouping<Status, Server>> clusterOperation)
    {
        foreach(var cluster in system.GroupBy(server => server.Status))
        {
            clusterOperation(cluster);
        }
    }
}