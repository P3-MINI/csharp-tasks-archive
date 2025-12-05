//#define SEQUENTIAL_NAIVE
//#define PARALLEL_NAIVE
#define PARALLEL_THROTTLED

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace solution
{
    public class SimpleClient
    {
        private readonly ISimpleServer server;

        public SimpleClient(ISimpleServer server)
        {
            this.server = server;
        }

        public async Task<double> SumResponsesAsync(IEnumerable<SimpleRequest> requests)
        {
#if SEQUENTIAL_NAIVE

            double sum = 0.0;
            foreach (var request in requests)
            {
                var response = await server.Get(request);
                sum += response.OutputValue;
            }
            return sum;
#endif

#if PARALLEL_NAIVE

            var tasks = requests.Select(request => server.Get(request)).ToList();
            await Task.WhenAll(tasks);
            return tasks.Select(x => x.Result.OutputValue).Sum();

#endif

#if PARALLEL_THROTTLED

            var requestsQueue = new Queue<SimpleRequest>(requests);

            var currentTasks = new List<Task<SimpleResponse>>();
            int sum = 0;

            while (requestsQueue.Count != 0 || currentTasks.Count != 0)
            {
                while (requestsQueue.Count != 0 && (server.MaxConcurrentRequests < 0 || currentTasks.Count < server.MaxConcurrentRequests))
                {
                    var request = requestsQueue.Dequeue();
                    var task = server.Get(request);
                    currentTasks.Add(task);
                }

                var completedTask = await Task.WhenAny(currentTasks);

                try
                {
                    var response = await completedTask;
                    sum += response.OutputValue;
                }
                catch (SimpleException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    currentTasks.Remove(completedTask);
                }
            }
            return sum;
#endif
        }

    }
}
