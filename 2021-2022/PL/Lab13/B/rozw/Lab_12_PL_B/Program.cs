#define ETAP1
#define ETAP2
#define ETAP3
#define ETAP4

using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

namespace Lab_12
{
    class Program
    {
        static void Main(string[] args)
        {
            string scoreboardPath = Path.Combine(Directory.GetCurrentDirectory(), "Scoreboard");

            Prize prize1 = new Prize() { Place = 1 };
            Prize prize2 = new Prize() { Place = 2 };
            Prize prize3 = new Prize() { Place = 3 };
            Prize prize4 = new Prize() { Place = 1 };
            Prize prize5 = new Prize() { Place = 2 };
            Prize prize6 = new Prize() { Place = 3 };

            Person participant1 = new Person()
            {
                Name = "Adam",
                Surname = "Malysz",
                Birthday = DateTime.Parse("3.12.1977"),
                Prizes = new List<Prize> { prize1, prize3, prize4 },
            };

            Person participant2 = new Person()
            {
                Name = "Kamil",
                Surname = "Stoch",
                Birthday = DateTime.Parse("25.05.1987"),
                Prizes = new List<Prize> { prize2, prize1 },
            };

            Person participant3 = new Person()
            {
                Name = "Piotr",
                Surname = "Ahonen",
                Birthday = DateTime.Parse("16.01.1987"),
                Prizes = new List<Prize> { prize4, prize5, prize6 },
            };

            Person participant4 = new Person()
            {
                Name = "Janne",
                Surname = "Zyla",
                Birthday = DateTime.Parse("11.05.1977"),
                Prizes = new List<Prize> { prize5, prize2 },
            };

            Contest contest1 = new Contest()
            {
                Date = DateTime.Parse("15.12.2016"),
                Name = "Turniej 4 Skoczni",
                Participants = new List<Person>() { participant1, participant3, participant4 }
            };

            Contest contest2 = new Contest()
            {
                Date = DateTime.Parse("17.02.2017"),
                Name = "Sapporo Contest",
                Participants = new List<Person>() { participant2, participant1, participant3 }
            };

            Contest contest3 = new Contest()
            {
                Date = DateTime.Parse("28.01.2017"),
                Name = "Villingen Contest",
                Participants = new List<Person>() { participant1, participant2, participant3, participant4 }
            };

            prize1.Contest = contest1;
            prize2.Contest = contest2;
            prize3.Contest = contest3;
            prize4.Contest = contest3;
            prize5.Contest = contest1;
            prize6.Contest = contest2;


            Console.WriteLine("------------------ Etap1 ------------------");
#if ETAP1
            if (Directory.Exists(scoreboardPath) == true)
            {
                Directory.Delete(scoreboardPath, true);
            }

            Scoreboard scoreboard = new Scoreboard(scoreboardPath);

            scoreboard.Add(contest1);
            scoreboard.Add(contest2);
            scoreboard.Add(contest3);

            try
            {
                scoreboard.Add(contest3);
            }
            catch (PathAlreadyExistsException)
            {
                Console.WriteLine("Exception Expected: OK!");
            }

            scoreboard.Info();
#endif
            Console.WriteLine("------------------ Etap2 ------------------");
#if ETAP2
            {
                contest3.Date = DateTime.Parse("29.11.2019");

                scoreboard.Update(contest3);
                scoreboard.Info();

                Console.WriteLine();

                scoreboard.Delete(contest3.Name);

                foreach (Contest contest in scoreboard)
                    Console.WriteLine(contest);
            }
#endif
            Console.WriteLine("------------------ Etap3 ------------------");
#if ETAP3
            {
                if (Directory.Exists(scoreboardPath) == true)
                {
                    Directory.Delete(scoreboardPath, true);
                }

                Scoreboard newScoreboard = Scoreboard.Create("./Contests.bin");

                newScoreboard.Info();
            }
#endif

            Console.WriteLine("------------------ Etap4 ------------------");
#if ETAP4
            {
                string pathFilename = Path.Combine(Directory.GetCurrentDirectory(), "Participants.bin");

                scoreboard.Save(pathFilename);

                if (File.Exists(pathFilename) == true) Console.WriteLine("Binary File Found: OK!");
                else Console.WriteLine("Binary File NOT Found: Error!");
            }
#endif
        }
    }
}
