#define VERBOSE

using System;
using System.Threading;
using System.Threading.Tasks;

namespace _3_async
{
    public class SimpleServer : IServer
    {
        public int MaxConcurrentRequests { get; private set; }

        protected readonly int msProcessTime;
        protected readonly int msJitter;
        private readonly int logicThreshold;
        private int concurrentRequests;
        private int rId;

        public SimpleServer(int msProcessTime = 500, int msJitter = 200, int maxConcurrentRequests = -1, int logicThreshold = 90)
        {
            MaxConcurrentRequests = maxConcurrentRequests;
            this.msProcessTime = msProcessTime;
            this.msJitter = msJitter;
            this.logicThreshold = logicThreshold;
        }

        public async Task<SimpleResponse> Get(SimpleRequest request)
        {
            await Task.Yield();
            var requestId = Interlocked.Increment(ref rId);
            try
            {
#if VERBOSE
                Console.WriteLine($"Request {requestId} started: {request.FunctionName}({request.InputValue})");
#endif
                int requestsCount = Interlocked.Increment(ref concurrentRequests);
                if (MaxConcurrentRequests > 0 && requestsCount > MaxConcurrentRequests)
                {
                    throw new InvalidOperationException("Too many concurrent requests. Please try again later");
                }

                int output = await ServerLogic(request.FunctionName, request.InputValue);

                return new SimpleResponse
                {
                    OutputValue = output
                };
            }
            finally
            {
#if VERBOSE
                Console.WriteLine($"Request {requestId} finished");
#endif
                Interlocked.Decrement(ref concurrentRequests);
            }
        }

        protected async Task<int> ServerLogic(string functionName, int input)
        {
            await Task.Delay(new Random().Next(msJitter) + msProcessTime);
            if (-logicThreshold <= input && input <= logicThreshold)
            {
                switch (functionName)
                {
                    case "square": return input * input;
                    case "abs": return Math.Abs(input);
                    case "identity": return input;
                    default: throw new ArgumentException(null, nameof(functionName));
                }
            }
            throw new SimpleException($"Server couldn't compute {functionName} of {input}");
        }
    }
}
