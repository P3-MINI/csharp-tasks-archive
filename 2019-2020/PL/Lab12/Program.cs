
using System;

namespace PZ12
{

    public class MoveEventArgs : EventArgs
    {
        public readonly int move;
        public MoveEventArgs (int m) { move=m; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            GameMaster gm = new GameMaster();
            gm.AddPlayer(new Player());
            gm.AddPlayer(new Player());
            gm.AddPlayer(new Player());
            gm.AddPlayer(new Player());
            gm.RaiseStartGame();
        }
    }

}
