using Common;
using NuGet.Frameworks;
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
    public class MapTests
    {
        Random rand = new Random(2137);

        [TestMethod]
        public void TestInteract()
        {
            Map map = new(Levels.level_1_rooms, Levels.level_1_items);

            foreach (var item in Levels.level_1_items)
            {
                Assert.AreEqual(map.Interact(item.Key), item.Value);
            }
            Assert.AreEqual(map.Interact(new (0, 0)), string.Empty);
        }

        [TestMethod]
        public void TestConstructor()
        {
            List<Room> rooms = new List<Room>(Levels.level_1_rooms);
            Dictionary<Point, string> items = new Dictionary< Point, string> (Levels.level_1_items);
            Map map = new(rooms, items);
            rooms.Clear();

            foreach (var room in Levels.level_1_rooms) {
                var door = room.Door;
                Assert.AreEqual(map.Data[door.X, door.Y], Tile.Door);

                for(int i = 0; i < 5; i++)
                {
                    int x = rand.Next(room.TopLeft.X + 1, room.BottomRight.X - 1);
                    int y = rand.Next(room.TopLeft.Y + 1, room.BottomRight.Y - 1);

                    // random floor
                    Assert.AreEqual(map.Data[x, y], Tile.Floor);

                    // random wall or door
                    Tile tile = map.Data[x, room.TopLeft.Y];
                    Assert.IsTrue(new Point(x, room.TopLeft.Y) == door ? tile  == Tile.Door : tile == Tile.Wall);
                    tile = map.Data[x, room.BottomRight.Y];
                    Assert.IsTrue(new Point(x, room.BottomRight.Y) == door ? tile == Tile.Door : tile == Tile.Wall);
                    tile = map.Data[room.TopLeft.X, y];
                    Assert.IsTrue(new Point(room.TopLeft.X, y) == door ? tile == Tile.Door : tile == Tile.Wall);
                    tile = map.Data[room.BottomRight.X, y];
                    Assert.IsTrue(new Point(room.BottomRight.X, y) == door ? tile == Tile.Door : tile == Tile.Wall);
                }
            }

            Assert.AreEqual(Levels.level_1_rooms.Count, map.Rooms.Count);
        }

        [TestMethod]
        public void TestSearch()
        {
            Room room_1 = new Room() {TopLeft = new (0, 0), BottomRight = new (5, 5), Door = new (0, 3) };
            Room room_2 = new Room() { TopLeft = new(2, 5), BottomRight = new(10, 12), Door = new(2, 6) };
            var rooms = new List<Room>() { room_1, room_2 };
            var room_1_items = new Dictionary<Point, string>()
            {
                [new(1, 1)] = "Duck",
                [new(3, 2)] = "Chips",
                [new(3, 4)] = "Cola"
            };

            var items = new Dictionary<Point, string>(room_1_items);

            items[new(4, 6)] = "CAD/CAM Cola";

            Map map = new Map(rooms, items);

            var ItemsFromMapRooms_1 = map.Search(new(1, 1));
            Assert.AreEqual(room_1_items.Count, ItemsFromMapRooms_1.Count);
            foreach (var item in room_1_items)
            {
                ItemsFromMapRooms_1.Remove(item.Value);
            }
            Assert.AreEqual(ItemsFromMapRooms_1.Count, 0);
        }
    }
}
