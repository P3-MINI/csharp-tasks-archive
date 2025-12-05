#define STAGE_1 // primes
// #define STAGE_2 // aggregating integers
// #define STAGE_3 // queue
// #define STAGE_4 // string builder

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _2_parallel
{
    class Program
    {
        static void Main(string[] args)
        {
#if STAGE_1
            var primeTests = new List<(int limit, int noOfPrimes)>
            {
                (10, 4),
                (100, 25),
                (1_000, 168),
                (10_000, 1229),
                (100_000, 9592),
                (200_000, 17984),
            };

            foreach (var (limit, noOfPrimes) in primeTests)
            {
                Assert($"All primes below {limit}", () =>
                {
                    var res = true;
                    res &= Assert("Seq", () =>
                    {
                        var r = PrimeNumberChecker.AreNumberPrimesSequential(Enumerable.Range(1, limit).ToArray());
                        return r.Count(x => x) == noOfPrimes;
                    }, printDuration: true);

                    res &= Assert("Parallel", () =>
                    {
                        var r = PrimeNumberChecker.AreNumberPrimesParallel(Enumerable.Range(1, limit).ToArray());
                        return r.Count(x => x) == noOfPrimes;
                    }, printDuration: true);

                    return res;
                }, printHeader: true);
                Console.WriteLine();
            }
#endif 

#if STAGE_2
            Print("### STAGE 2 ###");
            {
                var sum_parallel = 0L;
                var sum_parallel_lock = 0L;
                var sum_parallel_interlocked = 0L;
                var lock_obj = new object();

                TimeIt(() =>
                {
                    Parallel.ForEach(Enumerable.Range(0, 10_000_000), i =>
                    {
                        sum_parallel += i;
                    });
                }, nameof(sum_parallel));

                TimeIt(() =>
                {
                    Parallel.ForEach(Enumerable.Range(0, 10_000_000), i =>
                    {
                        lock (lock_obj) { sum_parallel_lock += i; }
                    });
                }, nameof(sum_parallel_lock));

                TimeIt(() =>
                {
                    Parallel.ForEach(Enumerable.Range(0, 10_000_000), i =>
                    {
                        Interlocked.Add(ref sum_parallel_interlocked, i);
                    });
                }, nameof(sum_parallel_interlocked));

                Console.WriteLine($"{nameof(sum_parallel),-30}{sum_parallel}");
                Console.WriteLine($"{nameof(sum_parallel_lock),-30}{sum_parallel_lock}");
                Console.WriteLine($"{nameof(sum_parallel_interlocked),-30}{sum_parallel_interlocked}");
            }
#endif
#if STAGE_3
            Print("### STAGE 3 ###");
            {
                // without setting capacity it may (probably will) throw exception during resizing
                var queue = new Queue<int>(10_000_000); 
                var queue_lock = new Queue<int>(10_000_000);
                var queue_concurrent = new ConcurrentQueue<int>();
                var lock_obj = new object();

                TimeIt(() =>
                {
                    Parallel.ForEach(Enumerable.Range(0, 10_000_000), i =>
                    {
                        queue.Enqueue(i);
                    });
                }, nameof(queue));

                TimeIt(() =>
                {
                    Parallel.ForEach(Enumerable.Range(0, 10_000_000), i =>
                    {
                        lock (lock_obj) { queue_lock.Enqueue(i); }
                    });
                }, nameof(queue_lock));

                TimeIt(() =>
                {
                    Parallel.ForEach(Enumerable.Range(0, 10_000_000), i =>
                    {
                        queue_concurrent.Enqueue(i);
                    });
                }, nameof(queue_concurrent));

                Console.WriteLine($"{nameof(queue),-30}{queue.Count}");
                Console.WriteLine($"{nameof(queue_lock),-30}{queue_lock.Count}");
                Console.WriteLine($"{nameof(queue_concurrent),-30}{queue_concurrent.Count}");
            }
#endif

#if STAGE_4
            Print("### STAGE 4 ###");
            string a = "";
            StringBuilder a_sb = new StringBuilder();

            TimeIt(() =>
            {
                for (var i = 0; i < 200_000; i++)
                {
                    a += "a";
                };
            }, nameof(a));

            TimeIt(() =>
            {
                for (var i = 0; i < 200_000; i++)
                {
                    a_sb.Append("a");
                };
            }, nameof(a_sb));

#endif
        }

        public static void TimeIt(Action action, string actionName)
        {
            var sw = new Stopwatch();
            sw.Start();
            action();
            sw.Stop();
            Console.WriteLine($"{actionName,-30}Elapsed time [ms]: {sw.ElapsedMilliseconds}");
        }

        public static bool Assert(string message, Func<bool> condition, bool printDuration = false, bool printHeader = false)
        {
            var sw = new Stopwatch();
            sw.Start();
            try
            {
                if (printHeader)
                {
                    Print(message, ConsoleColor.Cyan);
                }
                bool result = condition();
                if (result)
                {
                    Print(message, ConsoleColor.Green);
                }
                else
                {
                    Print(message, ConsoleColor.Red);
                }
                return result;
            }
            catch (Exception ex)
            {
                Print(ex?.Message, ConsoleColor.DarkMagenta);
                if (ex?.InnerException != null)
                {
                    Print(ex?.InnerException.Message, ConsoleColor.DarkMagenta);
                }
                return false;
            }
            finally
            {
                sw.Stop();
                if (printDuration)
                {
                    Print($"Elapsed time - {sw.ElapsedMilliseconds} [ms]", ConsoleColor.Yellow);
                }
            }
        }

        private static async Task<bool> Assert(string message, Func<bool> condition, int msTimeout, bool printDuration = false)
        {
            var timeoutTask = Task.Delay(msTimeout);
            var taskTask = Task.Run(() =>
            {
                try { return Assert(message, condition, printDuration); }
                catch (OperationCanceledException ex)
                {
                    Console.WriteLine(ex);
                    return false;
                }
                finally { }
            });
            if (timeoutTask == await Task.WhenAny(new[] { timeoutTask, taskTask }))
            {
                Print($"{message} - Did not finish in {msTimeout} ms", ConsoleColor.Yellow);
                return false;
            }
            else
            {
                Print($"Finished in less than {msTimeout} ms", ConsoleColor.Green);
            }
            return await taskTask;
        }

        private static void Print(string message, ConsoleColor color = ConsoleColor.White)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            string prefix;
            switch (color)
            {
                case ConsoleColor.Green: prefix = "OK    "; break;
                case ConsoleColor.Red: prefix = "FAIL  "; break;
                case ConsoleColor.Cyan: prefix = "START "; break;
                default: prefix = "      "; break;
            }
            Console.WriteLine($"{prefix} {message}");
            Console.ForegroundColor = oldColor;
        }
    }
}
