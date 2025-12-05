using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Play
{
    internal class FrameTimer
    {
        Stopwatch stopwatch = new Stopwatch();
        Stopwatch totalStopwatch = new Stopwatch();
        double[] frametimes = new double[50];
        int frame = 0;
        public double Time { get; private set; }
        public FrameTimer()
        {
            totalStopwatch.Start();
        }
        public void StartFrame()
        {
            stopwatch.Start();
        }
        public void EndFrame()
        {
            stopwatch.Stop();
            frametimes[frame % frametimes.Length] = stopwatch.Elapsed.TotalSeconds;
            Time = totalStopwatch.Elapsed.TotalSeconds;
            stopwatch.Reset();

            Console.Title = $"{(int)(frametimes.Length / frametimes.Sum())}FPS";
            frame++;
        }
    }
}
