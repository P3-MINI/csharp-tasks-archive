using System;
using System.Collections.Generic;

namespace Lab13
{
    class Ranking : IDisposable
    {
        public class TeamComparer : IComparer<Team>
        {
            public int Compare(Team x, Team y)
            {
                if (x.Score > y.Score) return -1;
                if (x.Score == y.Score) return 0;
                return 1;
            }
        }
        private SortedSet<Team> teams= new SortedSet<Team>(new TeamComparer());

        public Ranking(int teamCount, int teamMembersCount)
        {
            // E1 - zaimplementowac
        }

        private Ranking(string[] teamRecordsData)
        {
            // E5 - zaimplementowac
            // do tworzenia obiektów druzyn należy wykorzystać statyczną metodę Team.FromText(string)
        }

        public Team AddNewTeam(int teamMembersCount)
        {
            Team nt = new Team(teamMembersCount);
            teams.Add(nt);
            return nt;
        }

        public override string ToString()
        {
            string ret = "Ranking:" + Environment.NewLine + Environment.NewLine;
            foreach (Team t in teams)
            {
                ret += t.ToString();
            }
            return ret;
        }

        public bool IsTheSame(Ranking other)
        {
            return other == null ? false : this.ToString().Equals(other.ToString());
        }

        public void Dispose() { }
        
        // ETAP 4
        public string ToText()
        {
            // E4 - zaimplementowac
            // format powinien być następujący: 
            // Team.ToText();Team.ToText();...
            return null;
        }

        // ETAP 5
        public static Ranking FromText(string data)
        {
            // E5 - zaimplementowac
            // metoda powinna stworzyć nowy obiekt Ranking na podstawie otrzymanego napisu,
            // do stworzenia nowego obiektu Ranking nalezy uzyc prywatnego konstruktora
            return null;
        }
    }
}
