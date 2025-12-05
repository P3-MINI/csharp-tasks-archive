using System;

namespace Lab5BEn
{
    public class Package : Item
    {
        private readonly float _weight;

        public Package(string from, string to, DateTime postageDate, float weight) : base(from, to, postageDate)
        {
            _weight = weight;
        }

        public float GetWeight()
        {
            return _weight;
        }

        public override string ToString()
        {
            return base.ToString() + $"Type: package\nWeight: {_weight}";
        }

        public override DateTime CalculateArrivalTime()
        {
            return postageDate.AddDays(_weight > 20 ? 15 : 10);
        }
    }
}