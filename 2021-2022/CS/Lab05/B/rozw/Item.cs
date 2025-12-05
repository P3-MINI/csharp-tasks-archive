using System;

namespace Lab5BEn
{
    public abstract class Item
    {
        private static int _nextId;

        private readonly int _id;
        private readonly string _recipient;
        private readonly string _sender;
        protected readonly DateTime postageDate;

        protected Item(string sender, string recipient, DateTime postageDate)
        {
            _sender = sender;
            _recipient = recipient;
            this.postageDate = postageDate;
            _id = _nextId++;
        }

        public override string ToString()
        {
            return $"Item id: {_id}\nFrom: {_sender}\nTo: {_recipient}\nPostage date: {postageDate}\n";
        }

        public abstract DateTime CalculateArrivalTime();

        public int GetId()
        {
            return _id;
        }

        public string GetSender()
        {
            return _sender;
        }

        public string GetRecipient()
        {
            return _recipient;
        }

        public DateTime GetPostageDate()
        {
            return postageDate;
        }
    }
}