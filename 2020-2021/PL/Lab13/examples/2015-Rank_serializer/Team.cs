using System;
using System.Collections.Generic;

namespace Lab13
{
    class Team
    {
        private static int maxId = 0;
        private string name;
        private int id;
        
        private List<Player> players;

        public string Name
        {
            get { return name; }
        }
        public List<Player> Players
        {
            get { return players; }
        }
        public double Score
        {
            get { return teamScore; }
        }
        
        private int GetIndex()
        {
            return ++maxId;
        }

        public Team(int teamMembersCount)
        {
            // E1 - zaimplementowac
        }
        private Team(string[] teamDetails)
        {
            // E5 - zaimplementowac
            // przy tworzeniu nowych obietow Player uzyc Player.FromText
        }

        public void AddPlayer(Player p)
        {
            // E1 - zaimplementowac
        }

        public override string ToString()
        {
            string ret = String.Format("Team: {0} has {1} players" + Environment.NewLine, name, players.Count);
            players.ForEach(p => ret += p.ToString() + Environment.NewLine);
            ret += "Team Score is " + Score + Environment.NewLine + Environment.NewLine; 
            return ret;
        }
        private double teamScore = 0;

        // ETAP 4
        public string ToText()
        {
            // E4 - zaimplementowac
            // format Id,Player.ToText(),Player.ToText()
            return null;
        }

        // ETAP 5
        static public Team FromText(string teamRecordData)
        {
            // E5 zaimplementowac
            // do tworzenia nowego obiektu Team uzyc prywatnego konstruktora
            return null;
        }
}
}
