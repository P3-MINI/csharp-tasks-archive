using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class Levels
    {
        public static List<Room> level_1_rooms { get; private set; } = new List<Room>() {
                new Room{ TopLeft = new(0, 0),  BottomRight = new(7, 4), Door = new(7, 2)},
                new Room{ TopLeft = new(3, 5),  BottomRight = new(7, 10), Door = new(7, 8) },
                new Room{ TopLeft = new(10, 2), BottomRight = new(14, 7), Door = new(10, 4)}
            };
        public static Dictionary<Point, string> level_1_items { get; private set; } = new Dictionary<Point, string>{
            [new Point(1, 1)] = "Rubber Duck",
            [new Point(1, 3)] = "Rubber Duck",
            [new Point(3, 4)] = "Rubber Duck",
            [new Point(2, 2)] = "Rubber Duck",
            [new Point(3, 3)] = "Cookie",
            [new Point(1, 1)] = "Cookie",
            [new Point(1, 3)] = "Gun",
            [new Point(4, 9)] = "Gold Duck",
            [new Point(4, 8)] = "Angry Duck",
            [new Point(12, 5)] = "Long-Hair Duck",
            [new Point(13, 4)] = "Blue-Hair Duck",
        };

    }
}
