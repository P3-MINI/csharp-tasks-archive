namespace Task
{
    public class Player
    {
        private Dictionary<string, int> Equipment { get; set; } = new Dictionary<string, int>();

        public int Quantity(string item) => Equipment.ContainsKey(item) ? Equipment[item] : 0;
        
        public void Collect(string item)
        {
            //begin task 3
            if(!_equipment.TryAdd(item, 1))
                _equipment[item]++;
            //end task 3
        }
    }
}
