#define STAGE_1
#define STAGE_2
#define STAGE_3
#define STAGE_4
#define STAGE_5

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace solution
{
    public class Program
    {
        private static readonly Random rng = new Random(42);
        public static void Main(string[] args)
        {
            TaskScheduler.UnobservedTaskException += (sender, eventArgs) =>
            {
                Print("Unobserved exception", ConsoleColor.Red);
                Print($"{eventArgs?.Exception}", ConsoleColor.Red);
            };
#if STAGE_1
            Print("### STAGE 1 ###");

            Print("Easy passwords");
            AssertGroup("Sequential - Baseline recovery should succeed", () =>
                TestPasswordRecovery("seq", "e2fc714c4727ee9395f324cd2e7f331f", 4, "abcd", shouldRecoverPassword: true)
            );
            AssertGroup("Sequential - Baseline recovery should failed", () =>
                TestPasswordRecovery("seq", "e2fc714c4727ee9395f324cd2e7f331f", 4, "abce", shouldRecoverPassword: false)
            );

            AssertGroup("Parallel   - Baseline recovery should succeed", () =>
                TestPasswordRecovery("parallel", "e2fc714c4727ee9395f324cd2e7f331f", 4, "abcd", shouldRecoverPassword: true)
            );
            AssertGroup("Parallel   - Baseline recovery should fail", () =>
                TestPasswordRecovery("parallel", "e2fc714c4727ee9395f324cd2e7f331f", 4, "abce", shouldRecoverPassword: false)
            );

            Print("Medium passwords");
            AssertGroup("Sequential - Password is recovered", () =>
                TestPasswordRecovery("seq", "794fd8df6686e85e0d8345670d2cd4ae", 8, "abcd", shouldRecoverPassword: true)
            , 2000);

            AssertGroup("Parallel - Password is recovered", () =>
                TestPasswordRecovery("parallel", "794fd8df6686e85e0d8345670d2cd4ae", 8, "abcd", shouldRecoverPassword: true)
            , 2000);
#endif

#if STAGE_2
            Print("### STAGE 2 ###");

            Print("Hard passwords");

            AssertGroup("Sequential - Password is recovered", () =>
                TestPasswordRecovery("seq", "d78b6f30225cdc811adfe8d4e7c9fd34", 4, "qwertyuiopasdfghjklzxcvbnm", shouldRecoverPassword: true)
            , 5000);

            AssertGroup("Parallel - Password is recovered", () =>
               TestPasswordRecovery("parallel", "d78b6f30225cdc811adfe8d4e7c9fd34", 4, "qwertyuiopasdfghjklzxcvbnm", shouldRecoverPassword: true)
           , 5000);

            Print("Very hard passwords");
            AssertGroup("Sequential - Password is recovered", () =>
                TestPasswordRecovery("seq", "fd9ab41e47a9ef4f6477a8a000bf404f", 5, "qwertyoasdfghjklzxcvbnm", shouldRecoverPassword: true)
            , 15000);
            AssertGroup("Parallel   - Password is recovered", () =>
                TestPasswordRecovery("parallel", "fd9ab41e47a9ef4f6477a8a000bf404f", 5, "qwertyoasdfghjklzxcvbnm", shouldRecoverPassword: true)
            , 15000);

#endif

#if STAGE_3
            Print("### STAGE 3 ###");

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
                var task = simpleClient.SumResponsesAsync(simpleRequests);
                bool ok = true;

                ok &= AssertTimeLimit(task, 5000).Result;
                ok &= Assert("Computed sum should be 17",
                    () => 17 == task.Result);
                return ok;
            }, true, true);

            Assert("Stable server #2 - parallel", () =>
            {
                var server = new SimpleServer();
                var simpleClient = new SimpleClient(server);
                var requests = GenerateRequests(15);
                var task = simpleClient.SumResponsesAsync(requests);
                bool ok = true;
                ok &= AssertTimeLimit(task, 5000).Result;
                ok &= Assert("Computed sum should be 1389",
                    () => 1389 == task.Result);
                return ok;
            }, true, true);
            GenerateRequests(35);
#else
            GenerateRequests(35);
            GenerateRequests(15);
#endif

#if STAGE_4
            Print("### STAGE 4 ###");

            Assert("Stable server #3 - throttling disabled", () =>
            {
                var server = new SimpleServer(maxConcurrentRequests: -1);
                var simpleClient = new SimpleClient(server);
                var requests = GenerateRequests(5);
                var task = simpleClient.SumResponsesAsync(requests);
                bool ok = true;
                ok &= AssertTimeLimit(task, 1000).Result;
                ok &= Assert("Computed sum should be 1234",
                    () => 1234 == task.Result);
                return ok;
            }, true, true);

            Assert("Stable server #4 - throttling - enough to handle all requests concurrently", () =>
            {
                var server = new SimpleServer(maxConcurrentRequests: 10);
                var simpleClient = new SimpleClient(server);
                var requests = GenerateRequests(5);
                var task = simpleClient.SumResponsesAsync(requests);
                bool ok = true;
                ok &= AssertTimeLimit(task, 1000).Result;
                ok &= Assert("Computed sum should be 821",
                    () => 821 == task.Result);
                return ok;
            }, true, true);

            Assert("Stable server #5 - throttling - queueing is needed", () =>
            {
                var server = new SimpleServer(maxConcurrentRequests: 5);
                var simpleClient = new SimpleClient(server);
                var requests = GenerateRequests(15);
                var task = simpleClient.SumResponsesAsync(requests);
                bool ok = true;
                ok &= AssertTimeLimit(task, 3000).Result;
                ok &= Assert("Computed sum should be 6029",
                    () => 6029 == task.Result);
                return ok;
            }, true, true);
#else
             GenerateRequests(25);
#endif

#if STAGE_5
            Print("### STAGE 5 ###");

            Assert("Faulty server #1 - maxConcurrentRequests (disabled)", () =>
            {
                var server = new SimpleServer(maxConcurrentRequests: -1, logicThreshold: 90);
                var simpleClient = new SimpleClient(server);
                var requests = GenerateRequests(5, 100);
                var task = simpleClient.SumResponsesAsync(requests);
                bool ok = true;
                ok &= AssertTimeLimit(task, 5000).Result;
                ok &= Assert("Computed sum should be 3340",
                    () => 3340 == task.Result);
                return ok;
            }, true, true);

            Assert("Faulty server #2 - maxConcurrentRequests (5)", () =>
            {
                var server = new SimpleServer(maxConcurrentRequests: 5, logicThreshold: 10);
                var simpleClient = new SimpleClient(server);
                var requests = GenerateRequests(10, 100);
                var task = simpleClient.SumResponsesAsync(requests);
                bool ok = true;
                ok &= AssertTimeLimit(task, 5000).Result;
                ok &= Assert("Computed sum should be 8",
                    () => 8 == task.Result);
                return ok;
            }, true, true);



            Assert("Faulty server #3 - maxConcurrentRequests (1)", () =>
            {
                var server = new SimpleServer(maxConcurrentRequests: 1, logicThreshold: 25);
                var simpleClient = new SimpleClient(server);
                var requests = GenerateRequests(5, 100);
                var task = simpleClient.SumResponsesAsync(requests);
                bool ok = true;
                ok &= AssertTimeLimit(task, 5000).Result;
                ok &= Assert("Computed sum should be 26",
                    () => 26 == task.Result);
                return ok;
            }, true, true);

#endif
            GC.Collect();
        }

        private static IEnumerable<SimpleRequest> GenerateRequests(int count, int range = 50)
        {
            string[] operations = "abs,identity,square".Split(',');
            return Enumerable.Range(0, count).Select(x =>
            {
                return new SimpleRequest
                {
                    FunctionName = operations[rng.Next(operations.Length)],
                    InputValue = rng.Next(-range, range)
                };
            }).ToArray();
        }
#if STAGE_1
        private static bool TestPasswordRecovery(string method, string hash, int passwordLength, string alphabet, bool shouldRecoverPassword, string hashAlgorithm = "md5")
        {
            Print($"Starting recovering password for ({hash}) [{hashAlgorithm}]. Password length: {passwordLength}. Alphabet: {alphabet}");
            string password = method == "seq"
                ? PasswordRecoveryTool.RecoverPasswordSequential(hash, passwordLength, alphabet)
                : PasswordRecoveryTool.RecoverPasswordParallel(hash, passwordLength, alphabet);
            Print($"Recovery finished. Recovered password: [{(password ?? "<NULL>")}]");

            bool isOk = true;
            if (shouldRecoverPassword)
            {
                if (Assert("Password should be recovered", () => password != null))
                {
                    isOk &= Assert($"Password length should be {passwordLength}", () => passwordLength == password.Length);
                    isOk &= Assert($"Password characters should be within {alphabet}", () => password.All(letter => alphabet.Contains(letter)));
                    string hash2 = PasswordRecoveryTool.GetHash(HashAlgorithm.Create(hashAlgorithm), password);
                    isOk &= Assert($"Recovered password hash should be equal to original hash", () => hash2 == hash);
                    if (hash2 != hash)
                    {
                        Print($"Recovered password hash is ({hash2})");
                    }
                }
                else
                {
                    isOk = false;
                }
            }
            else
            {
                isOk &= Assert("Password should not be recovered", () => password == null);
            }

            return isOk;
        }
#endif




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
            catch (AggregateException ex)
            {
                Print(ex?.Message, ConsoleColor.DarkMagenta);
                if (ex.InnerExceptions != null)
                {
                    foreach (var innerException in ex.InnerExceptions)
                    {
                        Print(innerException?.Message, ConsoleColor.DarkMagenta);
                    }
                }
                return false;
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

        public static bool AssertGroup(string message, Func<bool> condition, int? msTimeout = null)
        {
            var sw = new Stopwatch();
            sw.Start();
            try
            {
                Print(message, ConsoleColor.Cyan);
                bool result = msTimeout.HasValue
                    ? Assert(message, condition, msTimeout.Value).Result
                    : condition();
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
                Print($"Elapsed time - {sw.ElapsedMilliseconds} [ms]", ConsoleColor.Yellow);
                Print("\n");
            }
        }

        private static async Task<bool> Assert(string message, Func<bool> condition, int msTimeout, bool printDuration = false)
        {
            var timeoutTask = Task.Delay(msTimeout);
            var taskTask = Task.Run(() =>
            {
                try { return condition(); }
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
            else
            {
                Print($"Finished in less than {msTimeout} ms", ConsoleColor.Green);
            }
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
                case ConsoleColor.DarkCyan: prefix = "END   "; break;
                default: prefix = "      "; break;
            }
            Console.WriteLine($"{prefix} {message}");
            Console.ForegroundColor = oldColor;
        }


    }
}
