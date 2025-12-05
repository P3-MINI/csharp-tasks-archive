namespace Task
{
    public class Player
    {
        private Dictionary<string, int> Equipment { get; set; } =  new Dictionary<string, int>();

        public int Quantity(string item) => Equipment.ContainsKey(item) ? Equipment[item] : 0;
        
        public void Collect(string item)
        {
            //begin task 3
            //end task 3
        }
    }
}
