#define SEQUENTIAL_SYNC
// #define SEQUENTIAL_ASYNC
// #define PARALLEL_NAIVE
// #define ERROR_HANDLING

using System.Collections.Generic;
using System.Threading.Tasks;

namespace _3_async
{
    public class SimpleClient
    {
        private readonly IServer server;

        public SimpleClient(IServer server)
        {
            this.server = server;
        }



#if SEQUENTIAL_SYNC
        public Task<double> SumResponses(IEnumerable<SimpleRequest> requests)
        {
            double sum = 0.0;

            foreach (var request in requests)
            {
                var response = server.Get(request).Result;
                sum += response.OutputValue;
            }
            return Task.FromResult(sum);
        }
#endif

#if SEQUENTIAL_ASYNC
        public async Task<double> SumResponses(IEnumerable<SimpleRequest> requests)
        {
            // required to add async keyword to method declaration in order to use await keyword
            double sum = 0.0;
            foreach (var request in requests)
            {
                var response = await server.Get(request);
                sum += response.OutputValue;
            }
            return sum;
        }
#endif

#if PARALLEL_NAIVE
        public async Task<double> SumResponses(IEnumerable<SimpleRequest> requests)
        {
            var tasks = new List<Task<SimpleResponse>>();

            foreach (var request in requests)
            {
                var task = server.Get(request);
                tasks.Add(task);
            }

            var responses = await Task.WhenAll(tasks);

            double sum = 0.0;
            foreach (var response in responses)
            {
                sum += response.OutputValue;
            }

            return sum;
        }
#endif

#if ERROR_HANDLING
        // helper method that in case of failure it will return fake response with value set to '0'
        private static async Task<SimpleResponse> HandleResponse(Task<SimpleResponse> responseTask)
        {
            try
            {
                return await responseTask;
            }
            catch (SimpleException)
            {
                return new SimpleResponse
                {
                    OutputValue = 0
                };
            }
        }

        public async Task<double> SumResponses(IEnumerable<SimpleRequest> requests)
        {
            var tasks = new List<Task<SimpleResponse>>();

            foreach (var request in requests)
            {
                var task = server.Get(request);
                // we're using helper method that will swallow exception
                tasks.Add(HandleResponse(task));
            }

            var responses = await Task.WhenAll(tasks);

            double sum = 0.0;

            foreach (var response in responses)
            {
                sum += response.OutputValue;
            }
            return sum;
        }
#endif
    }
}
