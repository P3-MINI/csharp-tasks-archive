namespace AdeptusMechanicus;

public static class Implants {
    // TODO
    public static List<(string, int)> Top3OldestTechpriests() {
        // ---
        var result = (from priest in Database.Techpriests
                      where priest.Rank == "Magos"
                      orderby priest.Age descending
                      select (priest.Name, priest.Age)).Take(3).ToList();
        return result;
        // ---
    }

    // TODO
    public static List<(string, decimal)> ForgeWorldsByAverageCost() {
        // ---
        var result = (from order in Database.ImplantOrders
                      group order by order.ForgeWorld into g
                      let avg = g.Average(o => o.Cost)
                      orderby avg
                      select (g.Key, avg)).ToList();
        return result;
        // ---
    }
}
