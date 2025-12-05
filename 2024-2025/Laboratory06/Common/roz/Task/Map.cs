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
            Items = new Dictionary<Point, string>(items);
            Rooms = new List<Room>(rooms);

            // begin task 1
            int width = rooms.Max((a) => a.BottomRight.X) + 1;
            int height = rooms.Max((a) => a.BottomRight.Y) + 1;
            Data = new Tile[width, height];

            foreach (var room in rooms)
            {
                for (int x = room.TopLeft.X; x <= room.BottomRight.X; x++)
                {
                    Data[x, room.TopLeft.Y] = Tile.Wall;
                    Data[x, room.BottomRight.Y] = Tile.Wall;
                }

                for (int y = room.TopLeft.Y + 1; y <= room.BottomRight.Y - 1; y++)
                {
                    Data[room.TopLeft.X, y] = Tile.Wall;
                    Data[room.BottomRight.X, y] = Tile.Wall;
                }
            }

            foreach (var room in rooms)
            {
                Data[room.Door.X, room.Door.Y] = Tile.Door;
            }
            // end task 1
        }

        public List<string> Search(Point position)
        {
            // begin task 2
            Room room = Rooms.Where(room =>
            {
                if(position.X < room.TopLeft.X) return false;
                if(position.X > room.BottomRight.X) return false;
                if(position.Y < room.TopLeft.Y) return false;
                if(position.Y > room.BottomRight.Y) return false;

                return true;
            }).FirstOrDefault();

            List<string> items = new List<string>();
            if(room.Equals(default(Room)))
                return items;

            for(int i = room.TopLeft.X; i < room.BottomRight.X; i++)
            {
                for (int j = room.TopLeft.Y; j < room.BottomRight.Y; j++)
                {
                    string item = Interact(new Point(i, j));
                    if (item != string.Empty)
                        items.Add(item);
                }
            }

            return items;
            // end task 2
        }

        public string Interact(Point position)
        {
            // begin task 3
            if(Items.ContainsKey(position))
                return Items[position];
            else
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
