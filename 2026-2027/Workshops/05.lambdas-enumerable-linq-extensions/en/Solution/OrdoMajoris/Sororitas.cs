namespace OrdoMajoris;

public static class Sororitas {
    // TODO
    public static (string, int) ThirdSmallestTerraOrder() {
        // ---
        var result = (from order in Database.Orders
                      where order.HomeWorld == "Terra"
                      orderby order.Members
                      select (order.Name, order.Members)).Skip(2).First();
        return result;
        // ---
    }

    // TODO
    public static List<(string, int)> WeaponsByTotalKillCount() {
        // ---
        var result = (from sister in Database.Sisters
                      group sister by sister.FavouriteWeapon into g
                      let total = g.Sum(s => s.KillCount)
                      orderby total descending
                      select (g.Key, total)).ToList();
        return result;
        // ---
    }
}

