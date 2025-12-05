using Common;
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
    public class AnalysisTests
    {
        [TestMethod]
        public void TestAnalysis()
        {
            Dictionary<Point, string> items =
            new()
            {
                [new(0, 0)] = "1",
                [new(0, 5)] = "2",
                [new(15, 0)] = "3",
                [new(15, 1)] = "3",
                [new(15, 5)] = "4",
                [new(15, 4)] = "4",
                [new(14, 5)] = "4",
                [new(14, 4)] = "4",
            };
            Room[] rooms = new Room[10];
            for(int i = 0; i<rooms.Length; i++)
            {
                rooms[i].BottomRight = new Point(15+i, 5+i*2);
            }
            Map[] maps = rooms.Select(r => new Map([r], items)).ToArray();

            var result = Analysis.CountItemsOnQuarter(maps);

            Assert.AreEqual(result[Analysis.Quarter.TopLeft],17);
            Assert.AreEqual(result[Analysis.Quarter.TopRight], 50);
            Assert.AreEqual(result[Analysis.Quarter.BottomLeft], 3);
            Assert.AreEqual(result[Analysis.Quarter.BottomRight], 10);
        }
    }
}
