using System;

namespace Lab5AEn
{
    public class Book : Item
    {
        private readonly decimal _feePerDay;
        private readonly bool _hardcover;

        public Book(string author, string name, DateTime printDate, decimal feePerDay, bool hardcover = false) :
            base(author, name, printDate)
        {
            _feePerDay = feePerDay;
            _hardcover = hardcover;
        }

        public override decimal CalculateFee(int days)
        {
            if (days < 30)
                return days * _feePerDay;
            return 30 * _feePerDay + (days - 30) * _feePerDay * 2;
        }

        public bool HasHardcover()
        {
            return _hardcover;
        }

        public override string ToString()
        {
            return base.ToString() + $"Book {name} by {author}, printed {printDate:dd-MM-yyyy} " +
                   (_hardcover ? "hardcover" : "softcover");
        }
    }
}