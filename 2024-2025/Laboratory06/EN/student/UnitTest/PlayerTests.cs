using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task;

namespace UnitTestLab6
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void TestCollect()
        {
            Player player = new Player();
            List<(string name, int quantity)> items = new List<(string, int)>()
            { ("Shovel", 1), ("Rubber Duck", 4), ("Sword", 2), ("Key", 5) };

            foreach (var item in items) {
                for (int i = 0; i < item.quantity; i++)
                    player.Collect(item.name);

                Assert.AreEqual(player.Quantity(item.name), item.quantity);
            }

            Assert.AreEqual(player.Quantity("Sandwich"), 0);
        }

    }
}
