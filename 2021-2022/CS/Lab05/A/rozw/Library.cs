using System;

namespace Lab5AEn
{
    public class Library

    {
        private readonly string _name;
        private readonly Bookshelf[] _shelves;

        public Library(string name, Bookshelf[] shelves)
        {
            _shelves = new Bookshelf[shelves.Length];
            for (var i = 0; i < shelves.Length; i++) _shelves[i] = shelves[i];

            _name = name;
        }

        public int GetItemsCount()
        {
            var count = 0;
            for (var i = 0; i < _shelves.Length; i++) count += _shelves[i].GetItemsCount();

            return count;
        }

        public Bookshelf GetBookshelf(int i)
        {
            if (i >= _shelves.Length)
                return null;
            return _shelves[i];
        }

        public void Print()
        {
            Console.WriteLine($"Library: {_name}");
            Console.WriteLine("Shelves:");

            var i = 0;
            foreach (var shelf in _shelves)
            {
                Console.WriteLine($"shelf {i++}:\n");
                shelf.Print();
            }
        }
    }
}