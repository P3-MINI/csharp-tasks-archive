using System;

namespace Lab5BEn
{
    public class PostBox
    {
        private readonly Item[] _items;
        private readonly string _location;

        public PostBox(string location, Item[] items)
        {
            _location = location;
            _items = items;
        }

        public int GetItemsCount()
        {
            var count = 0;
            for (var i = 0; i < _items.Length; i++)
                if (_items[i] != null)
                    count++;

            return count;
        }

        public Item GetItem(int i)
        {
            if (i >= _items.Length)
                return null;
            return _items[i];
        }

        public void Print()
        {
            Console.WriteLine($"Postbox: {_location}");
            Console.WriteLine("Items:");

            var i = 0;
            foreach (var item in _items) Console.WriteLine($"Item {i++}:\n{item}\n");
        }
    }
}