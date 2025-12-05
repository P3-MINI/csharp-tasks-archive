#define STAGE_1
#define STAGE_2
#define STAGE_3
#define STAGE_4

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab12
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Lab12");
#if STAGE_1
            Console.WriteLine("Stage 1 - Parallel 1 point");
            var lines = new List<string>();
            PopulateList(lines);
            var messages = new BlockingCollection<string>();

            Parallel.ForEach(lines, (line, _, i) =>
            {
                messages.Add($"Worker {i}, words count: {line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Length}");
            });

            foreach (var message in messages)
            {
                Console.WriteLine(message);
            }
            Console.WriteLine($"Total lines count {messages.Count}");
            Console.WriteLine($"First read line: {messages.First()}");
            Console.WriteLine($"Last read line: {messages.Last()}");

            var primesCount = 0;
            var mutex = new Mutex();
            var sb = new StringBuilder();
            Parallel.For(2, 1001, i =>
            {
                var flag = true;
                for (int j = 2; j * j <= i; j++)
                {
                    if (i % j == 0)
                    {
                        flag = false;
                        break;
                    }
                }

                if (flag)
                {
                    mutex.WaitOne();
                    primesCount++;
                    sb.Append($"{i} ");
                    mutex.ReleaseMutex();
                }
            });

            Console.WriteLine($"List of prime numbers: {sb}");
            Console.WriteLine($"Total primes count: {primesCount}");
#endif
#if STAGE_2
            Console.WriteLine("Stage 2 - Logger: 2 points");
            var logger = new Logger();
            var colorSink = new ColorLoggerSink();
            var conditionalSink = new ConditionalLoggerSink((x, y) => y == Severity.Normal && x.Length < 20);

            logger.AttachSink(colorSink);
            logger.HandleMessage("Ala has a cat", Severity.Normal);
            logger.HandleMessage("Warning Error", Severity.High);
            logger.AttachSink(conditionalSink);
            logger.HandleMessage("Ala has a cat, sack full of sand 23 Error", Severity.Critical);
            logger.HandleMessage("Ala has a cat, sack full of sand 23 Error", Severity.Normal);
            logger.DetachSink(colorSink);
            logger.DetachSink(colorSink);
            logger.AttachSink(conditionalSink);
            logger.HandleMessage("Nothing to do here, nothing to worry about", Severity.Debug);
            logger.HandleMessage("Nothing to do here", Severity.Debug);
            logger.DetachSink(conditionalSink);
            logger.DetachSink(conditionalSink);
#endif
#if STAGE_3
            Console.WriteLine("Stage 3 - File input: 2 points");
            logger.AttachSink(colorSink);
            var sourceA = new FileSource("./text.txt");
            var sourceB = new RandomSource(10, 20);
            logger.AttachInputSource(sourceA);
            logger.AttachInputSource(sourceB);
            logger.ProcessNextLines(5);
            logger.DetachInputSource(sourceA);
            logger.DetachInputSource(sourceB);
#endif
        }
        private static void PopulateList(List<string> lines)
        {
            lines.AddRange(File.ReadAllLines("./text.txt"));
        }
    }
}
