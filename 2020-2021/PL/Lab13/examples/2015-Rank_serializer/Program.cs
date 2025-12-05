using System;
using System.IO;
using System.Linq;

namespace Lab13
{
    class Program
    {
        private const string SOAP_FILENAME = "rank.soap";
        private const string TEXT_FILENAME = "rank.txt";
        private const int TEAMS_COUNT = 3;
        private const int TEAM_MEMBERS_COUNT = 2;

        static void Main(string[] args)
        {
            Ranking rank = new Ranking(TEAMS_COUNT, TEAM_MEMBERS_COUNT);
            Console.WriteLine(rank.ToString());

            // Etap 1 - generowanie struktur Ranking
            TextReader tr = new StreamReader(new FileStream("example.str", FileMode.Open));
            string exampleContent = tr.ReadToEnd().Trim();
            if (rank.ToString().Trim() != exampleContent)
            {
                Console.WriteLine("Etap 1 - ERROR - values doesn't match, it should be:" + Environment.NewLine + exampleContent);
                return;
            }
            Console.WriteLine("Etap 1 - OK");

            // Etap 2 - serializacja do pamięci
            MemoryStream stream = RankSerializers.SerializeBinary(rank);
            Ranking r1 = RankSerializers.DeserializeBinary(stream);
            if (rank.IsTheSame(r1) == false)
            {
                Console.WriteLine("Etap 2 - ERROR");
                return;
            }
            Console.WriteLine("Etap 2 - OK");

            // Etap 3 - serializacja do pliku w formacie SOAP
            RankSerializers.SerializeSOAP(rank, SOAP_FILENAME);
            Ranking r2 = RankSerializers.DeserializeSOAP(SOAP_FILENAME);
            if (rank.IsTheSame(r2) == false)
            {
                Console.WriteLine("Etap 3 - ERROR");
                return;
            }
            Console.WriteLine("Etap 3 - OK");

            // Etap 4 - konwersja obiektów do napisu o formacie jak w treści zadania
            string exampleText = "1,0|sut|27|829,1|nwl|35|847;2,2|hlp|25|755,3|avy|29|731;3,4|uvy|16|964,5|nxr|26|291";
            if (rank.ToText() != exampleText)
            {
                Console.WriteLine("Etap 4 - ERROR - texts are not the same, should be:" + Environment.NewLine + exampleText);
                return;
            }
            Console.WriteLine("Etap 4 - OK");

            // Etap 5 - własna metoda serializacji do pliku tekstowego
            FileStream sw = new FileStream(TEXT_FILENAME, FileMode.Create);
            RankSerializers.SerializeOwn(rank, sw);
            Ranking r3 = RankSerializers.DeserializeOwn(sw);
            if (rank.IsTheSame(r3) == false)
            {
                Console.WriteLine("Etap 5 - ERROR - objects are not the same");
                return;
            }
            Team t = r3.AddNewTeam(TEAM_MEMBERS_COUNT);
            if (t.Name != "Team_4" || t.Players.Any(p => p.Id < TEAMS_COUNT * TEAM_MEMBERS_COUNT))
            {
                Console.WriteLine("Etap 5 - ERROR - check maxId parameters");
                return;
            }
            Console.WriteLine("Etap 5 - OK");

        }
    }
}
