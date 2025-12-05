using System;

namespace Lab5AEn
{
    public class Magazine : Item
    {
        private readonly int _editionNumber;

        public Magazine(string author, string name, DateTime printDate, int editionNumber) : base(author, name,
            printDate)
        {
            _editionNumber = editionNumber;
        }

        public override decimal CalculateFee(int days)
        {
            return days / 2 * 13;
        }

        public override string ToString()
        {
            return base.ToString() +
                   $"Magazine {name} by {author}, printed {printDate:dd-MM-yyyy)} edition: {_editionNumber}";
        }
    }
}