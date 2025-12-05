using System;

namespace Lab13
{
    class Player
    {
        private static int NAME_LENGTH = 3;
        private static Random rand = new Random(0);
        private static int maxId = -1;

        private int id;
        private string nickname;
        private int age;
        private double score;
        
        public int Id
        {
            get { return id; }
        }
        public string Name
        {
            get { return nickname; }
        }
        public int Age
        {
            get { return age; }
        }
        public double Score
        {
            get { return score; }
        }

        private static string GenerateName()
        {
            string name = "";
            for (int i = 0; i < NAME_LENGTH; ++i)
                name += Char.ConvertFromUtf32(rand.Next('z' - 'a') + 'a');
            return name;
        }

        private static int GenerateAge()
        {
            return rand.Next(20) + 16;
        }

        public Player()
        {
            id = ++maxId;
            this.nickname = GenerateName();
            this.age = GenerateAge();
            this.score = rand.Next() % 1000;
        }
        private Player(int id) {
            // E5 - zaimplementowac
        }
        public override string ToString()
        {
            return "player: " + id + " name: " + nickname + " age: " + age + " score: " + score;
        }

        // ETAP 4
        public string ToText()
        {
            // E4 - zaimplementowac
            // format powinien być: id|nickname|age|score
            return null;
        }

        // ETAP 5
        static public Player FromText(string playerDetailsString)
        {
            // E5 - zaimplementowac
            // przy tworzeniu nowego obiektu Player użyc prywatnej wersji konstruktora
            return null;
        }
    }
}
