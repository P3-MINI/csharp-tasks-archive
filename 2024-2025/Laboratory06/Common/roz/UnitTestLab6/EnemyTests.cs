using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task;

namespace UnitTestLab6
{
    [TestClass]
    public class EnemyTests
    {
        [TestMethod]
        public void TestMove()
        {
            Point[] moves = [new(0, 1), new(3, 2), new(4, 6), new(7, 4), new(10, 6)];
            Enemy enemy = new Enemy(moves);

            foreach (Point p in moves) {
                Assert.AreEqual(enemy.Position, p);
                enemy.MoveNext();
            }
            foreach (Point p in moves)
            {
                Assert.AreEqual(enemy.Position, p);
                enemy.MoveNext();
            }

            Assert.AreEqual(enemy.Position, moves[0]);
        }
    }
}
