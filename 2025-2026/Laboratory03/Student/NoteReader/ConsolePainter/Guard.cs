using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ConsolePainter
{
    internal static class Guard
    {
        static Mutex mutex = new();
        static (int x, int y) pos = new(0, 0);
        public static void Begin()
        {
            mutex.WaitOne();
            pos = Console.GetCursorPosition();
        }
        public static void End()
        {
            Console.SetCursorPosition(pos.x, pos.y);
            mutex.ReleaseMutex();
        }
        public static bool FileReadAvailable(string path)
        {
            try
            {
                using var _ = File.Open(path, FileMode.Open, FileAccess.Read);
            }
            catch (IOException)
            {
                return false;
            }
            return true;
        }
    }
}
