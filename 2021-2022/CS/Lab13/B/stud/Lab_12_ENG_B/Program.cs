// #define STAGE1
// #define STAGE2
// #define STAGE3
// #define STAGE4

using System;
using System.IO;
using System.Collections.Generic;

namespace Lab_12_ENG_B
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
                Birthday = DateTime.ParseExact("03.12.1977", "dd.MM.yyyy", null),
                Prizes = new List<Prize> { prize1, prize3, prize4 },
            };

            Person participant2 = new Person()
            {
                Name = "Kamil",
                Surname = "Stoch",
                Birthday = DateTime.ParseExact("25.05.1987", "dd.MM.yyyy", null),
                Prizes = new List<Prize> { prize2, prize1 },
            };

            Person participant3 = new Person()
            {
                Name = "Piotr",
                Surname = "Ahonen",
                Birthday = DateTime.ParseExact("16.01.1987", "dd.MM.yyyy", null),
                Prizes = new List<Prize> { prize4, prize5, prize6 },
            };

            Person participant4 = new Person()
            {
                Name = "Janne",
                Surname = "Zyla",
                Birthday = DateTime.ParseExact("11.05.1977", "dd.MM.yyyy", null),
                Prizes = new List<Prize> { prize5, prize2 },
            };

            Contest contest1 = new Contest()
            {
                Date = DateTime.ParseExact("15.12.2016", "dd.MM.yyyy", null),
                Name = "Turniej 4 Skoczni",
                Participants = new List<Person>() { participant1, participant3, participant4 }
            };

            Contest contest2 = new Contest()
            {
                Date = DateTime.ParseExact("17.02.2017", "dd.MM.yyyy", null),
                Name = "Sapporo Contest",
                Participants = new List<Person>() { participant2, participant1, participant3 }
            };

            Contest contest3 = new Contest()
            {
                Date = DateTime.ParseExact("28.01.2017", "dd.MM.yyyy", null),
                Name = "Villingen Contest",
                Participants = new List<Person>() { participant1, participant2, participant3, participant4 }
            };

            prize1.Contest = contest1;
            prize2.Contest = contest2;
            prize3.Contest = contest3;
            prize4.Contest = contest3;
            prize5.Contest = contest1;
            prize6.Contest = contest2;


            Console.WriteLine("------------------ Stage1 ------------------");
#if STAGE1
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

            scoreboard.Info(false);
#endif
            Console.WriteLine("------------------ Stage2 ------------------");
#if STAGE2
            {
                contest3.Date = DateTime.ParseExact("29.11.2019", "dd.MM.yyyy", null);

                scoreboard.Update(contest3);
                scoreboard.Info(false);

                Console.WriteLine();

                scoreboard.Delete(contest3.Name);

                foreach (Contest contest in scoreboard)
                    Console.WriteLine(contest);
            }
#endif
            Console.WriteLine("------------------ Stage3 ------------------");
#if STAGE3
            {
                if (Directory.Exists(scoreboardPath) == true)
                {
                    Directory.Delete(scoreboardPath, true);
                }

                Scoreboard newScoreboard = Scoreboard.Create("./Contests.bin");

                newScoreboard.Info();
            }
#endif

            Console.WriteLine("------------------ Stage4 ------------------");
#if STAGE4
            {
                string pathFilename = Path.Combine(Directory.GetCurrentDirectory(), "Participants.xml");

                scoreboard.Save(pathFilename);

                if (File.Exists(pathFilename) == true) Console.WriteLine("Binary File Found: OK!");
                else Console.WriteLine("XML File NOT Found: Error!");
            }
#endif
        }
    }
}
