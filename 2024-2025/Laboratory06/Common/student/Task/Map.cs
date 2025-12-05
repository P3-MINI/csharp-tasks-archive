using Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    public class Map
    {
        public Tile[,] Data { get; private set; }
        public List<Room> Rooms { get; private set; }
        public Dictionary<Point, string> Items { get; private set; }

        public int Width => Data.GetLength(0);
        public int Height => Data.GetLength(1);

        public Map(IList<Room> rooms, IDictionary<Point, string> items)
        {
            // begin task 1
            Data = new Tile[0,0];
            Rooms = [];
            Items = [];
            // end task 1
        }

        public List<string> Search(Point position)
        {
            // begin task 2
            return [];
            // end task 2
        }

        public string Interact(Point position)
        {
            // begin task 3
            return string.Empty;
            // end task 3
        }

        public bool InBounds(Point point)
        {
            return point.X>=0 && point.Y>=0 &&
                point.X < Data.GetLength(0) &&
                point.Y < Data.GetLength(1);
        }
    }
}
