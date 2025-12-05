
using System;
using System.Collections.Generic;

namespace PZ12
{

    class GameMaster
    {
        public const int MAXVAL = 6;
        public const int MINVAL = 1;

        private List<int> moves = new List<int>();
        private List<int> players = new List<int>();
        private Random r = new Random(665);

        //event wywoływanie na początku każdej rundy - informuje pierwszego gracza o ruchu
        public event EventHandler<EventArgs> StartGame;

        //event wywoływany na koniec rundy - wysyła do wszystkich graczy wylosowaną wygrywającą liczbę
        public event EventHandler<MoveEventArgs> SendResult;

        public void RaiseStartGame()
        {
            StartGame += EndRound;
            StartGame(this,EventArgs.Empty);
        }

        public void AddPlayer(Player player)
        {
            StartGame += player.MakeMove;
            SendResult += player.GetResult;
            player.MoveMade += GetMove;
            player.GameOver += RemovePlayer;
            players.Add(player.PlayerId);
        }

        private void RemovePlayer(object sender, EventArgs e)
        {
            Player player= sender as Player;
            StartGame -= player.MakeMove;
            SendResult -= player.GetResult;
            player.MoveMade -= GetMove;
            player.GameOver -= RemovePlayer;
            players.Remove(player.PlayerId);
            Console.WriteLine("Gracz {0} przegral", player.PlayerId);
        }

        //ruch otrzymany od gracza
        private void GetMove(object sender, MoveEventArgs e)
        {
            Console.WriteLine("Gracz {0} wykonuje ruch: {1}", (sender as Player).PlayerId, e.move);
            if ( e.move < MINVAL || e.move > MAXVAL )
                throw new InvalidMoveException("Liczba spoza zakresu");
            if ( moves.Contains(e.move) )
                    throw new InvalidMoveException("Liczba juz wybrana przez innego gracza");
            moves.Add(e.move);
        }

        private void EndRound(object sender, EventArgs e)
        {
            int numb = r.Next(MINVAL, MAXVAL);
            Console.WriteLine("Wylosowana liczba {0}", numb);

            SendResult(this,new MoveEventArgs(numb));

            if (players.Count == 1)
            {
                Console.WriteLine("Gracz {0} wygral", players[0]);
                return;
            }

            if (players.Count == 0)
            {
                Console.WriteLine("Nikt nie wygral");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Kolejna runda");
            moves.Clear();
            StartGame(this,EventArgs.Empty);
        }
    }

    class InvalidMoveException : Exception
    {
        public InvalidMoveException() {}
        public InvalidMoveException(string msg) : base(msg) {}
        public InvalidMoveException(string msg, Exception e) : base(msg,e) {}
    }

}
