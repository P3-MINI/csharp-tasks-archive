using System;

namespace Lab5BEn
{
    public class Mail : Item
    {
        private readonly bool _registered;

        public Mail(string sender, string recipient, DateTime postageDate, bool registered = false) : base(sender,
            recipient, postageDate)
        {
            _registered = registered;
        }

        public override DateTime CalculateArrivalTime()
        {
            return postageDate.AddDays(_registered ? 2 : 5);
        }

        public override string ToString()
        {
            return base.ToString() + "Type: " + (_registered ? "registered" : "standard") + " mail";
        }

        public bool IsRegistered()
        {
            return _registered;
        }
    }
}