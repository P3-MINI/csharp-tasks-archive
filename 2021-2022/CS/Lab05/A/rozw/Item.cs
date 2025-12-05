using System;

namespace Lab5AEn
{
    public abstract class Item
    {
        private static int _nextId;

        private readonly int _id;
        protected readonly string author;
        protected readonly string name;
        protected readonly DateTime printDate;

        protected Item(string author, string name, DateTime printDate)
        {
            _id = _nextId++;
            this.author = author;
            this.name = name;
            this.printDate = printDate;
        }

        public override string ToString()
        {
            return $"Item with id: {_id}\n";
        }

        public abstract decimal CalculateFee(int days);

        public int GetId()
        {
            return _id;
        }

        public string GetAuthor()
        {
            return author;
        }

        public string GetName()
        {
            return name;
        }

        public DateTime GetPrintDate()
        {
            return printDate;
        }
    }
}