
using System;

namespace PZ12
{
 
    public class Player
    {
        private const int START_POINT = 3;
        private const int POINT_FOR_WIN = 2;
        private static Random r = new Random(665);
        private static int nextId;

        private int myNumber;
        private int points = START_POINT;

        public readonly int PlayerId;

        public event EventHandler<MoveEventArgs> MoveMade;
        public event EventHandler<EventArgs> GameOver;

        public Player()
        {
            PlayerId = ++nextId;
        }

        public void MakeMove(object sender, EventArgs e)
        {
            bool isMoveOk = false;
            if (points > 0)
            {
                while (!isMoveOk)
                {
                    myNumber = r.Next(GameMaster.MINVAL, GameMaster.MAXVAL); 
                    try
                    {
                        //wysyłamy eventa do GameMastera z naszym id i ruchem
                        MoveMade(this,new MoveEventArgs(myNumber));
                        isMoveOk = true;
                    }                        
                    catch (InvalidMoveException ex)  //jeśli ruch jest niepoprawny lub powtarza się to dostaniemy wyjątek i musimy poprawić ruch
                    {
                        Console.WriteLine(ex.Message+" Wybierz ponownie");
                        isMoveOk = false;
                    }
                }
            }
        }

        public void GetResult(object sender, MoveEventArgs e)
        {
            if (e.move == myNumber)
            {
                Console.WriteLine("Gracz {0} wygrywa", PlayerId);
                points += POINT_FOR_WIN;
                return;
            }
            points--;
            if (points < 0)
                GameOver(this,EventArgs.Empty);
        }

    }

}
