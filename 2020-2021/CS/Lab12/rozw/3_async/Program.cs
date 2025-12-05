#define STAGE_1 // 
#define STAGE_2 // exceptions

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace _3_async
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TaskScheduler.UnobservedTaskException += (sender, eventArgs) =>
            {
                Console.WriteLine("Unobserved exception");
                Console.WriteLine(eventArgs?.Exception);
            };

#if STAGE_1
            Print("### STAGE 1 ###");

            var simpleRequests = new[]
            {
                new SimpleRequest { FunctionName = "abs", InputValue = 5 },
                new SimpleRequest { FunctionName = "abs", InputValue = -4 },
                new SimpleRequest { FunctionName = "identity", InputValue = 3 },
                new SimpleRequest { FunctionName = "square", InputValue = 2 },
                new SimpleRequest { FunctionName = "identity", InputValue = 1 }
            };

            Assert("Stable server - simple requests", () =>
            {
                var server = new SimpleServer();
                var simpleClient = new SimpleClient(server);
                var task = simpleClient.SumResponses(simpleRequests);
                Console.WriteLine("Stable server - simple requests - flow returned to main");
                bool ok = true;

                ok &= AssertTimeLimit(task, 5000).Result;
                ok &= Assert("Computed sum should be 17",
                    () => 17 == task.Result);
                return ok;
            }, true, true);

#endif

#if STAGE_2
            Print("### STAGE 2 ###");

            Assert("Faulty server #1", () =>
            {
                var server = new SimpleServer(logicThreshold: 4);
                var simpleClient = new SimpleClient(server);
                var task = simpleClient.SumResponses(simpleRequests);
                bool ok = true;
                ok &= AssertTimeLimit(task, 2000).Result;
                ok &= Assert("Computed sum should be 12",
                    () => 12 == task.Result);
                return ok;
            }, true, true);
#endif
            GC.Collect();
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

        private static async Task<bool> AssertTimeLimit(Task condition, int msTimeout)
        {
            var timeoutTask = Task.Delay(msTimeout);
            var taskTask = condition;
            if (timeoutTask == await Task.WhenAny(new[] { timeoutTask, taskTask }))
            {
                Print($"Did not finish in {msTimeout} ms", ConsoleColor.Yellow);
                return false;
            }
            Print($"Finished in less than {msTimeout} ms", ConsoleColor.Green);
            return true;
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
