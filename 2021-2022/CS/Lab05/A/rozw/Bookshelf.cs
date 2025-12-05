using System;

namespace Lab5AEn
{
    public class Bookshelf
    {
        private readonly Item[] _items;
        private int _numOfItems;

        public Bookshelf(int size)
        {
            _items = new Item[size];
        }

        public bool AddItem(Item item)
        {
            if (_numOfItems >= _items.Length)
                return false;
            _items[_numOfItems++] = item;
            return true;
        }

        public int GetItemsCount()
        {
            return _numOfItems;
        }

        public void Print()
        {
            Console.WriteLine($"Bookshelf with {_numOfItems} items\n\nItems: ");
            for (var i = 0; i < _numOfItems; i++) Console.WriteLine(_items[i] + "\n");
        }
    }
}